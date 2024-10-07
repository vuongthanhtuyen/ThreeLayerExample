using BLLayer.Manager;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebformLayer.Administration
{
    public partial class UserProfileManager : System.Web.UI.Page
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
            try
            {
                int userId = int.Parse(Session["userId"].ToString());
                var user = UserManagerBLL.UserGetById(userId);

                if (user != null)
                {
                    hiddeniId.Value = user.Id.ToString();
                    lblEmail.Text = user.Email;
                    lblFullName.Text = user.FullName;
                    txtUserName.Text = user.Username;
                    txtFullname.Text = user.FullName;
                    txtEmail.Text = user.Email;
                    txtPhone.Text = user.Phone;
                    txtAddress.Text = user.Address;
                    txtDescription.Text = user.Description;
                    txtNgaySinh.Text = ((DateTime)user.Dob).ToString("yyyy-MM-dd");
                    avatarImage.ImageUrl = user.AvataUrl == null ? "~/assets/img/undraw_profile.svg" : @"~/Administration/Upload/UploadedAvatars/" + user.AvataUrl;
                    //imgAvatarSmall.ImageUrl = user.AvataUrl == null ? "~/assets/img/undraw_profile.svg" : @"~/Administration/Upload/UploadedAvatars/" + user.AvataUrl;

                }

            }
            catch
            {
                Response.Redirect("~/Administration/Login.aspx", false);
            }
        }
        protected void btnLuuLai_Click(object sender, EventArgs e)
        {
            var userUpdate = UserManagerBLL.UserGetById(int.Parse(hiddeniId.Value));
            userUpdate.Username = txtUserName.Text;
            userUpdate.FullName = txtFullname.Text;
            userUpdate.Email = txtEmail.Text;
            userUpdate.Phone = txtPhone.Text;
            userUpdate.Address = txtAddress.Text;
            userUpdate.Dob = DateTime.Parse(txtNgaySinh.Text);

            string a = UploadFile(fileUploadAvatar);
            if (a != null)
            {
                RemoveFileUpload(userUpdate.AvataUrl);
                userUpdate.AvataUrl = a;
            }
            
            userUpdate = UserManagerBLL.UserUpdate(userUpdate);
            lblMessage.Text = userUpdate != null ? "Thành công" : "Thất bại";
            Response.Redirect(Request.Url.AbsoluteUri); // Load lại trang

        }

        protected void btnCancel_Click (object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/Default.aspx", false);
        }
        private string UploadFile(FileUpload fileUpload)
        {
            try
            {
                if (fileUpload == null ||string.IsNullOrEmpty(fileUpload.FileName)) return null;
                string FileName = fileUpload.FileName.ToString();

                string randomString = new string(Enumerable.Range(0, 5)
                    .Select(_ => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"[new Random().Next(62)])
                    .ToArray());

                // Cộng chuỗi ngẫu nhiên vào chuỗi hiện tại
                string resultString = randomString +  FileName;

                string FileSaveLocation = @"~/Administration/Upload/UploadedAvatars/" + resultString;
                FileSaveLocation = Server.MapPath(FileSaveLocation);
                fileUpload.PostedFile.SaveAs(FileSaveLocation);
                return resultString; // fileUpload.FileName.ToString() + "Gửi file thành công";
            }
            catch { return null; }
        }
        private bool RemoveFileUpload(string fileName)
        {
            string FileSaveLocation = @"~/Administration/Upload/UploadedAvatars/" + fileName;
            FileSaveLocation = Server.MapPath(FileSaveLocation);
            if (System.IO.File.Exists(FileSaveLocation))
            {
                try
                {
                    System.IO.File.Delete(FileSaveLocation);
                    return true;
                }
                catch { return false; }
            }
            return false;
        }
        
        private bool CheckEsitFile(string fileName)
        {
            string FileSaveLocation = @"~/Administration/Upload/" + fileName;
            if (System.IO.File.Exists(FileSaveLocation))
            {
                try
                {
                    System.IO.File.Delete(FileSaveLocation);
                    return true;
                }
                catch { }
            }
            return false;
        }

    }
}