using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebformLayer.Administration
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string script = "LogOutModal();";

            // Đăng ký đoạn mã JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "LogOutModal", script, true);

        }
        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session["IdUser"] = 0;
            Session["MenuPermission"] = null;
            Response.Redirect("~/Administration/Login.aspx", false);
        }
    }
}