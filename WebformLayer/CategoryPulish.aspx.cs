﻿using BLLayer.Manager;
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
                                    <h5 class=""m-3"">{post.Name} </h5>
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
                CategoryName.Text = string.Format($"<a href=\"Default.aspx\" class=\"text-decoration-none text-dark\">Trang chủ</a> / <a href=\"#\" class=\"text-decoration-none text-dark\"> {categoryName} </a> ");

            }
        }
    }
}