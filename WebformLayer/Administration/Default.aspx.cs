using DALLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebformLayer.Administration
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //List<MenuPermisstion> menuPermisstions = new List<MenuPermisstion>();
            //menuPermisstions = (List<MenuPermisstion>)Session["MenuPermission"] ?? null;
            //string a = "";
            //if (menuPermisstions != null && menuPermisstions.Count > 0)
            //{
            //    foreach (var menu in menuPermisstions)
            //    {
            //        a = a + menu.MenuMa + "- " + menu.PermissionMa + "<br/>";
            //    }
            //}
            //else
            //{
            //    a = "Không truy cập được menu permission";
            //}


            //Label1.Text = a;
        }
        //protected void btnSeachFor_click(object sender, EventArgs e)
        //{

        //}
        //protected void btnTestUpfile_Click(object sender, EventArgs e)
        //{

        //   //lblhResult

        //}

        //private string UploadFile(FileUpload fileUpload)
        //{
        //    string result;
        //    if (fileUpload.PostedFile != null && fileUpload.PostedFile.ContentLength > 0)
        //    {
        //        try
        //        {
        //            string FileName = fileUpload.FileName.ToString();
        //            string FileSaveLocation = @"D:\SweetSoft\SelfLearning\Code\ThreeLayerExample\WebformLayer\Administration\Upload\" + FileName;
        //            if (System.IO.File.Exists(FileSaveLocation))
        //            {
        //                return result = "Tên file đã tồn tại trong cơ sở dữ liệu";
        //            }
        //            else
        //            {
        //                fileUpload.PostedFile.SaveAs(FileSaveLocation);
        //                //Image1.ImageUrl = "Upload/" + FileName;
        //                return result  = fileUpload.FileName.ToString() + "Gửi file thành công";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return result  = ex.Message;
        //        }
        //    }
        //    else
        //    {
        //        return result = "Không có file gửi lên";
        //    }
        //}
    }
}