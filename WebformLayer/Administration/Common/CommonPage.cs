using DALLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WebformLayer.Administration.LeftNavbar;

namespace WebformLayer.Administration.Common
{
    public class CommonPage : System.Web.UI.Page
    {
        public virtual string MenuMa { get; set; }

        public CheckPermistion checkPermistion = new CheckPermistion();

       
  

    }
}