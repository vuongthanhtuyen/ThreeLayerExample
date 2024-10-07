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
using WebformLayer.UserControlPublish;

namespace WebformLayer
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
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

            PostListUserControl.LoadPost(postList);

            PagingUserControl.GetPaging(totalRow, pagindex);

        }


        protected void HiddenButton_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}