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
    public class UserManagerBLL
    {

        public static User InsertUser(User objArticle)
        {
            return new UserController().Insert(objArticle);
        }
        public static UserCollection UserGetAll()
        {
            return new UserController().FetchAll();
        }
        
        public static List<UserGetAllAndRoleName> UserGetAllAndRoleName()
        {
            string sql = string.Format(@"	select u.*, ISNULL(STRING_AGG(r.Name, ', ' ),N'Không có quyền') as N'RoleName' from users u
                                            left join User_Role_Detail rl on u.Id = rl.UserId
                                            left join Role r on r.Id = rl.RoleId
                                            group by u.Id, u.FullName, u.Email, u.Phone, u.Dob, u.Password, u.Active, u.Username, u.AvataUrl, u.DatatimeCreate, u.Address, u.Description
											order by u.DatatimeCreate");
            return new InlineQuery().ExecuteTypedList<UserGetAllAndRoleName>(sql);
        }

        public static User UserGetByUserName(string username)
        {
            Select select = new Select();
            select.From(User.Schema);
            select.Where(User.UsernameColumn).IsEqualTo(username);
            return select.ExecuteSingle<User>();
        }

        public static User UserGetById(int idUser)
        {
            Select select = new Select();
            select.From(User.Schema);
            select.Where(User.IdColumn).IsEqualTo(idUser);
            return select.ExecuteSingle<User>();
        }
        public static User UserCheckAdmin(int idUser)
        {
            Select select = new Select();
            select.From(User.Schema);
            select.InnerJoin(UserRoleDetail.UserIdColumn, User.IdColumn);
            select.InnerJoin(Role.IdColumn, UserRoleDetail.RoleIdColumn);
            select.Where(Role.IdColumn).IsEqualTo(1);
            select.And(User.IdColumn).IsEqualTo(idUser);
            return select.ExecuteSingle<User>();
        }
        public static User UserCheckReferencePost(int idUser)
        {
            
            Select select = new Select();
            select.From(User.Schema);
            select.InnerJoin(Post.AuthorIDColumn, User.IdColumn);
            select.Where(User.IdColumn).IsEqualTo(idUser);
            return select.ExecuteSingle<User>();

        }

        public static bool UserDelete(int idUser)
        {
            new Delete().From(UserRoleDetail.Schema)
                .Where(UserRoleDetail.UserIdColumn).IsEqualTo(idUser).Execute();
            
            return new UserController().Delete(idUser);
        }
        public static User UserUpdate(User user)
        {
            return new UserController().Update(user);
        }

        public static List<UserAndRole> UserGetAllAndRoleName(object idUser)
        {
            string sql = string.Format(@"select r.Id, r.Name, r.Ma,
                case
	                when rd.UserId is not null then 1
	                else 0
                end as IsHaveRole
                from role as r
                left join User_Role_Detail as rd on r.Id = rd.RoleId AND rd.UserId = {0}", idUser);
            return new InlineQuery().ExecuteTypedList<UserAndRole>(sql);
        }

        public static int UpdateRoleForUser(int idUser, string listRoleName)
        {
            StoredProcedure sp = SPs.AddRoleForUser(idUser, listRoleName);
            return sp.Execute();


        }


    }
  

}
