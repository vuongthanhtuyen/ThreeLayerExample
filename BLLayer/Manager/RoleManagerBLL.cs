using DALLayer;
using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Manager
{
    public class RoleManagerBLL
    {

        // thêm role // cưnngs
        // Set quyền cho role đó + permissin
        // Set menu cho quyền và permision cho quyền

        public static RoleCollection GetAllRole()
        {
            return new RoleController().FetchAll();
        }

        public static Role Insert(Role role)
        {
            return new RoleController().Insert(role);
        }
        public static Role Update(Role role)
        {
            return new RoleController().Update(role);
        }
        public static bool Delete(int roleId)
        {

            // Xóa liên kết khóa ngoại với bảng UserRoleDetail
            new Delete().From(UserRoleDetail.Schema)
                .Where(UserRoleDetail.RoleIdColumn).IsEqualTo(roleId)
                .Execute();

            // Xóa liên kết khóa ngaoij với bảng RoleMenuPermissionDetail
            new Delete().From(RoleMenuPermissionDetail.Schema)
                .Where(RoleMenuPermissionDetail.RoleIdColumn).IsEqualTo(roleId)
                .Execute();

            // Xóa role
            return new RoleController().Delete(roleId);
        }

        public static Role GetRoleById(int id)
        {
            return new RoleController().FetchByID(id).SingleOrDefault();
        }


        public static Role IsExistsRoleMa(string roleMa)
        {
            Select select = new Select();
            select.From(Role.Schema).Where(Role.MaColumn).IsEqualTo(roleMa);
            return select.ExecuteSingle<Role>();

        }
        public static Role IsExistsRoleMaUpdate(string roleMa, int roleId)
        {
            // Lấy ra role có mã giống nhưng khác Id --> nếu nó trả về role
            Select select = new Select();
            select.From(Role.Schema).Where(Role.MaColumn).IsEqualTo(roleMa)
                .And(Role.IdColumn).IsNotEqualTo(roleId);
            return select.ExecuteSingle<Role>();

        }
        public static int CheckRoleWithUser(int roleId)
        {
            UserRoleDetailCollection roleDetails = new Select().From<UserRoleDetail>().Where(UserRoleDetail.Columns.Id).IsEqualTo(roleId)
                                            .ExecuteAsCollection<UserRoleDetailCollection>();

            return roleDetails.Count;

        }
        public static int CheckRoleWithMenu(int roleId)
        {
            RoleMenuPermissionDetailCollection roleMenuPermissionDetails = new Select()
                .From<RoleMenuPermissionDetail>().Where(RoleMenuPermissionDetail.Columns.RoleId)
                .IsEqualTo(roleId).ExecuteAsCollection<RoleMenuPermissionDetailCollection>();

            return roleMenuPermissionDetails.Count;

        }

        public static List<MenuPermissionDetail> GetAllMenuAndPermistion(int roleId)
        {
            //string sql = string.Format(@"select m.Id as 'MenuId', m.Name as 'MenuName',p.Id as 'PermissionId', p.Name as 'PermissionName',CASE 
            //    when (select COUNT(*) from Role as r
            //    inner join Role_Menu_Permission_Detail as rmp on r.Id = rmp.RoleId
            //    where r.Id = {0} and rmp.PermissionId = p.Id  and rmp.MenuId = m.Id) = 1 then 1
            //    else 0
            //    end as IsRoleHavePermission from menu as m ,Permission as p

            //    order by m.Id, p.Id",roleId);


            string sql = string.Format(@"
                SELECT 
                    m.Id AS 'MenuId', 
                    m.Name AS 'MenuName', 
                    p.Id AS 'PermissionId', 
                    p.Name AS 'PermissionName',
                    CASE 
                        WHEN EXISTS (
                            SELECT 1 
                            FROM Role r
                            INNER JOIN Role_Menu_Permission_Detail rmp 
                                ON r.Id = rmp.RoleId
                            WHERE r.Id = {0} 
                            AND rmp.PermissionId = p.Id  
                            AND rmp.MenuId = m.Id
                        ) THEN 1
                        ELSE 0
                    END AS IsRoleHavePermission
                FROM Menu m
                CROSS JOIN Permission p
                ORDER BY m.Id, p.Id", roleId);

            return new InlineQuery().ExecuteTypedList<MenuPermissionDetail>(sql);

        }

        public static int UpdateAllMenuPermission(List<MenuPermissionDetail> menuPermissionDetails, int roleId)
        {
            try
            {
                foreach (var menuPermissionDetail in menuPermissionDetails)
                {
                    if (menuPermissionDetail.IsRoleHavePermission == 1)
                    {
                        var sectlect = new Select().From(RoleMenuPermissionDetail.Schema)
                             .Where(RoleMenuPermissionDetail.MenuIdColumn).IsEqualTo(menuPermissionDetail.MenuId)
                             .And(RoleMenuPermissionDetail.PermissionIdColumn).IsEqualTo(menuPermissionDetail.PermissionId)
                             .And(RoleMenuPermissionDetail.RoleIdColumn).IsEqualTo(roleId)
                             .ExecuteSingle<RoleMenuPermissionDetail>();

                        if (sectlect == null)
                        {
                            RoleMenuPermissionDetail roleMenuPermissionDetail = new RoleMenuPermissionDetail()
                            {
                                MenuId = menuPermissionDetail.MenuId,
                                PermissionId = menuPermissionDetail.PermissionId,
                                RoleId = roleId
                            };
                            new RoleMenuPermissionDetailController().Insert(roleMenuPermissionDetail);
                        }
                    }// CMS
                    else
                    {
                        var select = new Select().From(RoleMenuPermissionDetail.Schema)
                             .Where(RoleMenuPermissionDetail.MenuIdColumn).IsEqualTo(menuPermissionDetail.MenuId)
                             .And(RoleMenuPermissionDetail.PermissionIdColumn).IsEqualTo(menuPermissionDetail.PermissionId)
                             .And(RoleMenuPermissionDetail.RoleIdColumn).IsEqualTo(roleId)
                             .ExecuteSingle<RoleMenuPermissionDetail>();
                        if (select != null)
                        {
                            new RoleMenuPermissionDetailController().Delete(select.Id);

                        }

                    }
                }
                return 1;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
