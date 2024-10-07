using BLLayer.Manager;
using DALLayer;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebformLayer.Administration
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["IdUser"] = 0;
            Session["MenuPermission"] = null;
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text.Length < 1)
                {
                    lblErrorMessage.Text = "Username can't emply";
                    return; 
                }
                else if (txtPassword.Text.Length < 1)
                {
                    lblErrorMessage.Text = "Password can't emply";
                    return;
                }
                else
                {
                    var login = LoginManagerBLL.Login(txtUsername.Text, txtPassword.Text);
                    if (login!=null)
                    {
                        lblErrorMessage.Text = "Result susseess" + login.Id;

                        // Lưu user Id vào session
                        Session["UserId"] = login.Id;
                        // Lưu Quyền của User vào session
                        List<MenuPermisstion> listMenuPermission = LoginManagerBLL.GetListMenuPermisstionByUser(login.Id);
                        Session["MenuPermission"] =listMenuPermission ;
                        Response.Redirect("~/Administration/Default.aspx", false);
                        return;
                    }
                    else
                    {
                        lblErrorMessage.Text = "Username or password incorrect";
                    }
                    //Response.Redirect("default.aspx");
                }
            }
            
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Result " + ex.Message;
                
            }

        }
    }
}