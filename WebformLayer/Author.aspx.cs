using BLLayer.Manager;
using BLLayer.Pulish;
using DALLayer;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebformLayer
{
    public partial class Author : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }


        private void BindGrid(int pagindex = 1)
        {
            if (!int.TryParse(hdPageIndex.Value, out pagindex))
            {
                pagindex = 1; // Gán giá trị mặc định nếu hdPageIndex.Value không hợp lệ
            }

            List<User> authors = new List<User>();
            authors = AuthorPublishBLL.GetAllAuthor();
            string listshow = "";
            foreach (User author in authors)
            {
                listshow += string.Format($@"<div class=""col-md-3 mt-4"">
                <div class=""card profile-card-5"">
                    <a href=""AuthorDetail.aspx?id={author.Id}"" class=""remove-link"">    
                        <div class=""card-img-block"">
                            <img class=""card-img-top"" src=""Administration/Upload/UploadedAvatars/{author.AvataUrl}"" alt=""Card image cap"">
                        </div>
                        <div class=""card-body pt-0"">
                            <h5 class=""card-title"">{author.FullName}</h5>
                            <p class=""card-text"">
                                {author.Description}
                            </p>
                        </div>
                    </a>
                </div>
            </div>");
            }

            ltlProfile.Text = listshow;

            //PagingUserControl.GetPaging(authors.Count, pagindex);

        }
        protected void HiddenButton_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}