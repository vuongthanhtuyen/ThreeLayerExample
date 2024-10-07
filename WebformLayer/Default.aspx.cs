using BLLayer.Manager;
using DALLayer;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebformLayer.Administration.LeftNavbar;

namespace WebformLayer
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }




        private void GetPaging(int totalPost, int pageIndex = 1, int pageSize = 10)
        {
            string a = "";
            int countPage;
            if (totalPost % pageSize != 0)
            {
                countPage = totalPost / pageSize + 1;
            }
            else
            {
                countPage = totalPost / pageSize;
            }

            a += string.Format(@"<li class=""page-item {0}"">
                        <a class=""page-link "" id =""pagePrivious"">Previous</a>
                    </li>", pageIndex == 1 ? "disabled" : "");



            for (int count = 1; count <= countPage; count++)
            {
                a += string.Format(@"<li class=""page-item {0}"" {2} >
                        <a data-index=""{1}"" class=""page-link"">{1}</a>
                    </li>", pageIndex == count ? "active" : "", count, pageIndex == count ? "aria-current=\"page\"" : "");
            }

            a += string.Format(@"<li class=""page-item {0}"">
                        <a class=""page-link"" id =""pageNext"">Next</a>
                    </li>", pageIndex == countPage ? "disabled" : "");


            ltlPaging.Text = a;

        }
        private void BindGrid(int pagindex = 1)
        {
            if (!int.TryParse(hdPageIndex.Value, out pagindex))
            {
                pagindex = 1; // Gán giá trị mặc định nếu hdPageIndex.Value không hợp lệ
            }

            List<PostAndAuthorName> postList = new List<PostAndAuthorName>();
            int totalRow = 0;
            postList = PostManagerBLL.GetAllPostAndAuthorName(10, pagindex, null, null, out totalRow);
            string postListShow = "";
            if (postList.Count > 0)
            {
                foreach (var post in postList)
                {
                    postListShow += string.Format($@"<div class=""col-lg-3 col-sm-6 wow fadeInUp pb-4"" data-wow-delay=""0.1s"" >
                         <a href=""DetailPost.aspx?id={post.Id}"" class=""text-decoration-none text-dark"" onclick>
                             <div class=""service-item text-center pt-3"">
                                 <div class=""p-2"">
                                     <img width=""200px"" height=""200px"" style=""object-fit: cover; border: 2px solid;"" src=""Administration/Upload/{post.ImageUrl ?? "default.png"}"" />
                                     <h5 class=""m-3"">{post.Name}</h5>
                                     <p class=""post-textwrap"">{post.Description}</p>
                                         <div class=""small"">
                                         <span class=""text-dark pb-2"">Tác giả: </span>
                                         <span class=""text-danger pb-2 fw-bold""> {post.AuthorFullName}</span>
                                         <br />
                                         <span class="" text-dark pb-2"">Tổng số view: </span>
                                         <span class=""text-danger pb-2 font-italic "">{post.ViewCount}</span>
                                     </div>
                                 </div>
                             </div>
                         </a>
                     </div>");
                }
                ltlpostList.Text = postListShow;

            }

            GetPaging(totalRow, pagindex);

        }


        protected void HiddenButton_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}