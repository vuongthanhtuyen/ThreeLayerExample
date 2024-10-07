using DALLayer;
using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Manager
{
    public class LoginManagerBLL
    {
        public static LoginReturnId Login(string username, string password)
        {
            Select select = new Select();
            select.From(User.Schema).Where(User.UsernameColumn).IsEqualTo(username);
            select.And(User.PasswordColumn).IsEqualTo(password);

     
                // Giả sử bạn có một phương thức để tạo đối tượng Login từ Id
                return select.ExecuteSingle<LoginReturnId>();

            // Hoặc xử lý khác nếu không tìm thấy người dùng
        }
        public static List<MenuCheckByUser> MenuCheckByUser (int userId)
        {
            StoredProcedure sp = SPs.GetAllMenuAndPermissionByIdUser(userId);
            return sp.ExecuteTypedList<MenuCheckByUser>();
        }
        
        public static List<MenuPermisstion> GetListMenuPermisstionByUser (int userId)
        {
            string sql = string.Format(@"select DISTINCT   m.Ma as 'MenuMa', p.Ma as 'PermissionMa' from menu as m
                inner join Role_Menu_Permission_Detail as rmp on m.Id = rmp.MenuId
                inner join Permission as p on p.Id = rmp.PermissionId
                inner join User_Role_Detail as  rd on rd.RoleId = rmp.RoleId
                where rd.UserId  = {0}
                group by m.Ma , p.Ma 
                ", userId);
            return new InlineQuery().ExecuteTypedList<MenuPermisstion>(sql);

        }



        

        //public static User UserGetByUserName(string username)
        //{
        //    Select select = new Select();
        //    select.From(User.Schema);
        //    select.Where(User.UsernameColumn).IsEqualTo(username);
        //    return select.ExecuteSingle<User>();
        //}
    }
}
