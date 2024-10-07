using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Pulish
{
    public class AuthorPublishBLL
    {
        public static List<User> GetAllAuthor()
        {
            string sql = string.Format(@"select u.Id, u.FullName, u.AvataUrl, u.Description from Users as u");
            return new InlineQuery().ExecuteTypedList<User>(sql);
        }

        public static int GetViewCoutByIdAuthor(int id)
        {
            string sql = string.Format(@"select SUM(post.ViewCount) from users
                left join post on users.Id = Post .AuthorID
                where USERs.Id = {0}
                group by Users.Id", id);

            return new InlineQuery().ExecuteScalar<int>(sql);   
        }
    }
}
