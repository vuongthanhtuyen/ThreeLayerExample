using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebformLayer.UserControlPublish
{
    public partial class PagingUser : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void GetPaging(int totalPost, int pageIndex = 1, int pageSize = 10)
        {
            string a = "";
            int countPage;
            if (totalPost % pageSize != 0)
            {
                countPage = totalPost / pageSize + 1;
            }
            else
            {
                countPage = totalPost / pageSize;
            }

            a += string.Format(@"<li class=""page-item {0}"">
                        <a class=""page-link "" id =""pagePrivious"">Previous</a>
                    </li>", pageIndex == 1 ? "disabled" : "");



            for (int count = 1; count <= countPage; count++)
            {
                a += string.Format(@"<li class=""page-item {0}"" {2} >
                        <a data-index=""{1}"" class=""page-link"">{1}</a>
                    </li>", pageIndex == count ? "active" : "", count, pageIndex == count ? "aria-current=\"page\"" : "");
            }

            a += string.Format(@"<li class=""page-item {0}"">
                        <a class=""page-link"" id =""pageNext"">Next</a>
                    </li>", pageIndex == countPage ? "disabled" : "");


            ltlPaging.Text = a;

        }
    }
}