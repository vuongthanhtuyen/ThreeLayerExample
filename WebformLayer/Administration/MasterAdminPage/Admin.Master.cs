using BLLayer.Manager;
using DALLayer;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebformLayer.Administration.MasterAdminPage
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        //public string SearchText
        //{
        //    get { return txtSearch.Text; }
        //}

        public string SearchType { get; set; }  
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["UserId"] = 1; // set để test
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/Administration/Login.aspx", false);
                }
                else
                {
                    try
                    {
                        int userId = int.Parse(Session["UserId"].ToString());
                        List<MenuCheckByUser> list = new List<MenuCheckByUser>();
                        list = LoginManagerBLL.MenuCheckByUser(userId);
                        List<ParentMenuInfo> parentMenu = list.Select(menu => new ParentMenuInfo
                        {
                            ParentMenu = menu.ParentMenu,
                            ParentMa = menu.ParentMa
                        }).Distinct().ToList();

                        string a = "";
                        foreach (var parent in parentMenu)
                        {
                            a = a + string.Format(@" <li class=""nav-item"">
                            <a class=""nav-link collapsed"" href=""#"" data-toggle=""collapse"" data-target=""#{1}""
                                aria-expanded=""true"" aria-controls=""{1}"">
                                <i class=""fas fa-fw fa-cog""></i>
                                <span>{0}</span>
                            </a>
                            <div id=""{1}"" class=""collapse"" aria-labelledby=""headingTwo"" data-parent=""#accordionSidebar"">
                                <div class=""bg-white py-2 collapse-inner rounded"">
                                    <h6 class=""collapse-header"">{0}</h6>", parent.ParentMenu, parent.ParentMa);
                            foreach (var menu in list)
                            {
                                if (menu.ParentMenu == parent.ParentMenu)
                                {
                                    a = a + string.Format(@"<a class=""collapse-item"" href=""{0}"">{1}</a>", menu.Url, menu.MenuName);
                                }
                            }
                            a = a + string.Format(@" </ div >
                                    </ div >
                                </ li >");
                        }
                        LeftNavbars.Text = a;


                        var user = UserManagerBLL.UserGetById(userId);
                        nameUser.Text = user.FullName;
                        imgAvatarSmall.ImageUrl = user.AvataUrl == null ? "~/assets/img/undraw_profile.svg" : @"~/Administration/Upload/UploadedAvatars/" + user.AvataUrl;


                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("~/Administration/Login.aspx", false);
                    }
                    
                }
            }


        }
        protected void btnSeachFor_click(object sender, EventArgs e)
        {

        }


    }
}