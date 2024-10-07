using BLLayer.Pulish;
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
                PageLoad();
            }
        }

        private void PageLoad()
        {
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
        }
    }
}