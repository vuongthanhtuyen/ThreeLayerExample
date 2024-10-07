using DALLayer;
using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Pulish
{
    public class PostPulishBLL
    {

        public static PostAndAuthorName GetPostAndAuthorNameById(int id)
        {
            string sql = string.Format(@"SELECT p.*, u.FullName AS AuthorFullName 
                   FROM Post p
                   LEFT JOIN Users u ON p.AuthorID = u.Id
                   WHERE p.Id = {0}", id);

            try
            {
                using (var reader = new InlineQuery().ExecuteReader(sql))
                {
                    if (reader.Read()) // Kiểm tra nếu có dữ liệu
                    {
                        return new PostAndAuthorName()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            ImageUrl = !reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? reader.GetString(reader.GetOrdinal("ImageUrl")) : null,
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            ViewCount = reader.GetInt32(reader.GetOrdinal("ViewCount")),
                            DatetimeCreate = reader.GetDateTime(reader.GetOrdinal("DatetimeCreate")),
                            AuthorFullName = reader.GetString(reader.GetOrdinal("AuthorFullName")),
                            AuthorID = reader.GetInt32(reader.GetOrdinal("AuthorID"))
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                throw new Exception("Có lỗi xảy ra khi lấy dữ liệu bài viết.", ex);
            }

            return null; // Trả về null nếu không tìm thấy
        }

        public static List<CategoryAndPostExtend> GetAllCategoryAndPost()
        {
            string sql = string.Format(@"SELECT p.Id as PostId, c.Id as CategoryId, ISNULL(c.Name,N'Không') as CategoryName, p.Name  as PostName
                        FROM Post AS p
                        LEFT JOIN Category_Detail AS cd ON p.Id = cd.PostId 
                        LEFT JOIN Category AS c ON cd.CategoryId = c.Id
                        ORDER BY 
                            CASE WHEN c.Name IS NULL THEN 1 ELSE 0 END, 
                            c.Name");
            return new InlineQuery().ExecuteTypedList<CategoryAndPostExtend>(sql);
        }
        public static List<Category> GetAllCategory()
        {
            return new CategoryController().FetchAll().Take(5).ToList();
        }


        public static List<Post> GetRelatePost(int id)
        {
            string sql = string.Format(@"select Distinct top(3) p.* from post as p ,Category_Detail as cd where cd.PostId = p.Id 
                and cd.CategoryId in (select CategoryId from Category_Detail where PostId = {0}) and ImageUrl is not null
                and Active = 1
	                order by p.Name;", id);
            return new InlineQuery().ExecuteTypedList<Post>(sql);
        }

        
    }
}
