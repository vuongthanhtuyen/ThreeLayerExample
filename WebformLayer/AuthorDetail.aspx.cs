using BLLayer.Manager;
using DALLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebformLayer
{
    public partial class AuthorDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Binding();
            }
        }

        private void Binding()
        {
            int userId = string.IsNullOrEmpty(Request.QueryString["id"]) ? 0 : int.Parse(Request.QueryString["id"]);
            if (userId > 0)
            {
                var user = UserManagerBLL.UserGetById(userId);
                List<PostAndAuthorName> postList = new List<PostAndAuthorName>();
                int totalRow = 0;
                postList = PostManagerBLL.GetAllPostAndAuthorName(5, 1, user.FullName, null, out totalRow);
                int totalView = postList.Select(x => x.ViewCount).Sum();
                string postListShow = "";
                if (postList.Count > 0)
                {
                    foreach (var post in postList)
                    {
                        postListShow += string.Format($@"<a href=""DetailPost?id={post.Id}"" class=""remove-link"">
                                    <div class=""card mb-3"">
                                        <div class=""row no-gutters"">
                                            <div class=""col-md-4"">
                                                <img src=""Administration/Upload/{post.ImageUrl}"" class=""card-img"" alt=""..."">
                                            </div>
                                            <div class=""col-md-8"">
                                                <div class=""card-body"">
                                                    <h5 class=""card-title"">{post.Name}</h5>
                                                    <p class=""card-text"">{post.Description}</p>
                                                    <p class=""card-text""><small class=""text-muted"">{post.DatetimeCreate.ToShortDateString()}</small></p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </a>");
                    }
                    lbtPost.Text = postListShow;
                    lblAdress.Text = "Địa chỉ: " + user.Address;
                    lblDecription.Text = user.Description;
                    lblEmail.Text = user.Email;
                    lblFullName.Text = user.FullName;
                    lblTongBaiViet.Text = totalRow.ToString();
                    lblTongLuotXem.Text = totalView.ToString();
                    userImage.ImageUrl = "Administration/Upload/UploadedAvatars/"+ user.AvataUrl;
                }
            }
        }
    }
}