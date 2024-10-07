using DALLayer;
using SubSonic;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Manager
{
    public class PostManagerBLL
    {
        public static PostCollection GetAll()
        {
            return new PostController().FetchAll();
        }
        public static List<PostAndAuthorName> GetAllPostAndAuthorName(int? PageSize, int? PageIndex, string Key, bool? ASC, out int rowsCount)
        {
            rowsCount = 0;
            StoredProcedure sp = SPs.SearchPagingPost(PageSize, PageIndex, Key, ASC, out rowsCount);
            //if (sp.OutputValues.Count > 0)
            //    rowsCount = Convert.ToInt32(sp.OutputValues[0]);

            DataSet ds = sp.GetDataSet();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (sp.OutputValues.Count > 0)
                    rowsCount = Convert.ToInt32(sp.OutputValues[0]);

            }

            return sp.ExecuteTypedList<PostAndAuthorName>();
        }

        public static bool Delete(int postId)
        {
            new Delete().From(CategoryDetail.Schema).
                Where(CategoryDetail.PostIdColumn).IsEqualTo(postId);

            return new PostController().Delete(postId);
        }
        public static Post GetPostById(int PostId)
        {
            return new PostController().FetchByID(PostId).SingleOrDefault();
        }

        public static Post Insert(Post post)
        {
            return new PostController().Insert(post);
        }
        public static Post Update(Post post)
        {
            return new PostController().Update(post);
        }

        public static Post IsNameExists(string postName)
        {
            var select = new Select().From(Post.Schema)
                .Where(Post.NameColumn).IsEqualTo(postName)
                .ExecuteSingle<Post>();
            return select;
        }

        public static List<CategoryPostId> GetAllCategoryByPostId(int postId)
        {
            string sql = string.Format(@"Select c.Id, c.Name, c.Ma,
                case
	                when cd.PostId is not null then 1
	                else 0
                end as IsHaveCategory
                from Category as c
                left join Category_Detail as cd on cd.CategoryId = c.Id AND cd.PostId = {0}", postId);
            return new InlineQuery().ExecuteTypedList<CategoryPostId>(sql);
        }

        public static int UpdateCategoryByPostId(List<CategoryPostId> categories, int postId)
        {
            try
            {
                if (categories != null && categories.Count > 0)
                {
                    List<CategoryDetail> select = new Select().From(CategoryDetail.Schema).ExecuteTypedList<CategoryDetail>();
                    foreach (var category in categories)
                    {
                        if (category.IsHaveCategory == 1)
                        {
                            bool isExist = false;
                            foreach (var item in select)
                            {
                                if (item.CategoryId == category.Id && item.PostId == postId)
                                {
                                    isExist = true;
                                    break;
                                }
                            }
                            if (!isExist)
                            {
                                CategoryDetail detail = new CategoryDetail()
                                {
                                    PostId = postId,
                                    CategoryId = category.Id,
                                };
                                new CategoryDetailController().Insert(detail);
                            }

                        }
                        else
                        {
                            int categoryDetailId = -1;
                            foreach (var item in select)
                            {
                                if (item.CategoryId == category.Id && item.PostId == postId)
                                {
                                    categoryDetailId = item.Id;
                                    break;
                                }
                            }
                            if (categoryDetailId > 0)
                            {
                                new CategoryDetailController().Delete(categoryDetailId);
                            }
                        }
                    }


                }
                return 1;
            }
            catch
            {
                return -1;
            }



        }


        public static List<Category> GetAllCategory()
        {
            return new CategoryController().FetchAll().GetList();
        }

        



    }


}
