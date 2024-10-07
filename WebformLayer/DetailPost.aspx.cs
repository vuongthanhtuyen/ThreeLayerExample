using BLLayer.Manager;
using BLLayer.Pulish;
using DALLayer;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebformLayer.UserControlPublish;

namespace WebformLayer
{
    public partial class DetailPost : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int postId = string.IsNullOrEmpty(Request.QueryString["id"]) ? 0 : int.Parse(Request.QueryString["id"]);
                if (postId > 0)
                {
                    var post = PostPulishBLL.GetPostAndAuthorNameById(postId);
                    if (post == null)
                    {
                        Response.Redirect("404.aspx", true);
                    }
                    else
                    {
                        postImage.ImageUrl = string.IsNullOrEmpty(post.ImageUrl) ? "~/Administration/Upload/default.png" : "~/Administration/Upload/" + post.ImageUrl;
                        postAuthor.Text = post.AuthorFullName;
                        postTitle.Text = post.Name;
                        postView.Text = post.ViewCount.ToString();
                        postDescription.Text = post.Description;
                        postContent.Text = post.Content;
                        postDatetimeCreate.Text = post.DatetimeCreate.ToString();
                        postAuthor.NavigateUrl = "AuthorDetail.aspx?id=" + post.AuthorID;
                    }
                    // Bind link category lên 
                    //List<CategoryAndPostExtend> categoryAndPostExtends = PostPulishBLL.GetAllCategoryAndPost();
                    //List<string> categorys = categoryAndPostExtends.Select(a => a.CategoryName).Distinct().ToList();
                    //string categorylist = "";
                    //foreach(var category in categorys)
                    //{
                    //    categorylist += string.Format($@"<li ><a href = ""#"" > {category} </a >
                    //                                    <ul class=""list-unstyled"">");
                    //    foreach(var categoryDetail in categoryAndPostExtends)
                    //    {
                    //        if (categoryDetail.CategoryName == category)
                    //        {
                    //            if(categoryDetail.PostId == postId)
                    //            {
                    //                categorylist += string.Format($@" <li> <a class=""active"" href=""DetailPost.aspx?id={categoryDetail.PostId}"" > {categoryDetail.PostName} </a ></li >");
                    //            }
                    //            else
                    //            {
                    //                categorylist += string.Format($@" <li> <a href=""DetailPost.aspx?id={categoryDetail.PostId}"" > {categoryDetail.PostName} </a ></li >");
                    //            }

                    //        }
                    //    }
                    //    categorylist += "</ul> </li>";
                    //}
                    //categoryList.Text = categorylist;


                    var categoryAndPostExtends = PostPulishBLL.GetAllCategory();
                    string categorylist = "";
                    foreach (var category in categoryAndPostExtends)
                    {
                        categorylist += string.Format($@"<li> <a href=""CategoryPulish.aspx?id={category.Id}&categoryName={category.Name}"" class=""text-decoration-none"">{category.Name}</a> </li>");
                    }
                    categoryList.Text = categorylist;


                    List<Post> postListRelates = new List<Post>();
                    postListRelates = PostPulishBLL.GetRelatePost(postId);
                    string postRelateString = "";
                    foreach (var postRelate in postListRelates)
                    {
                        postRelateString += string.Format($@"<li class=""media m-4"">
                                
                                <div class=""media-body"" >
                                    <h5 class=""mt-0 mb-0""><a href=""DetailPost.aspx?id={postRelate.Id}"">
                                        <img class=""d-flex mr-3 img-fluid"" 
                                        width=""100%"" src=""Administration/Upload/{postRelate.ImageUrl}"" 
                                        alt=""Hình ảnh bài viết"">
                                        {postRelate.Name}</a></h5>
                                    {postRelate.DatetimeCreate.ToString("dd-MM-yyyy")}
                                </div>
                            </li>");
                    }
                    postListShow.Text = postRelateString;
                }
                else
                {
                    Response.Redirect("404.aspx", true);
                }

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text)) 
            {
                List<PostAndAuthorName> postList = new List<PostAndAuthorName>();
                int totalRow = 0;
                postList = PostManagerBLL.GetAllPostAndAuthorName(3, 1, txtSearch.Text, null, out totalRow);

                string postRelateString = "";
                foreach (var postRelate in postList)
                {
                    postRelateString += string.Format($@"<li class=""media m-4"">
                    
                    <div class=""media-body"" >
                        <h5 class=""mt-0 mb-0""><a href=""DetailPost.aspx?id={postRelate.Id}"">
                            <img class=""d-flex mr-3 img-fluid"" 
                            width=""100%"" src=""Administration/Upload/{postRelate.ImageUrl}"" 
                            alt=""Hình ảnh bài viết"">
                            {postRelate.Name}</a></h5>
                        {postRelate.DatetimeCreate.ToString("dd-MM-yyyy")}
                    </div>
                </li>");
                }
                divRelatedArticles.Visible = true;
                SearchResult.Text = string.IsNullOrEmpty(postRelateString) ? "Không có bài viết liên quan đến " + txtSearch.Text : postRelateString;
            }
            else
            {
                SearchResult.Text ="";
                divRelatedArticles.Visible = false;
            }
        }
    }
}