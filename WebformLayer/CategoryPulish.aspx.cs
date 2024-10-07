using BLLayer.Manager;
using DALLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebformLayer
{
    public partial class CategoryPulish : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int categoryId = string.IsNullOrEmpty(Request.QueryString["id"]) ? 0 : int.Parse(Request.QueryString["id"]);
            string categoryName = Request.QueryString["categoryName"];
            List<PostAndAuthorName> postList = new List<PostAndAuthorName>();
            int totalRow = 0;
            postList = PostManagerBLL.GetAllPostAndAuthorName(20, 1, categoryName, null, out totalRow);
            if (postList.Count > 0)
            {
               CategoryName.Text = string.Format($"<a href=\"Default.aspx\" class=\"text-decoration-none text-dark\">Trang chủ</a> / <a href=\"#\" class=\"text-decoration-none text-dark\"> {categoryName} </a> ");
                PostListUserControl.LoadPost(postList);
            }
        }
    }
}