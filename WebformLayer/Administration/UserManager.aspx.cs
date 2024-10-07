using Antlr.Runtime.Misc;
using BLLayer;
using BLLayer.Manager;
using DALLayer;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebformLayer.Administration.LeftNavbar;
using static System.Net.Mime.MediaTypeNames;

namespace WebformLayer.Administration
{
    public partial class UserManager : System.Web.UI.Page
    {
        List<MenuPermisstion> menuPermisstionChild = new List<MenuPermisstion>();


        private const string MenuMa = "Quan-ly-User";
        CheckPermistion CheckPermistion = new CheckPermistion();
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((List<MenuPermisstion>)Session["MenuPermission"] != null)
            {
                menuPermisstionChild = CheckPermistion.GetMenuPermisstions(Session["MenuPermission"], MenuMa);
            }
            else
            {
                Response.Redirect("~/Administration/Login.aspx", false);
                return;
            }
            if (menuPermisstionChild != null && menuPermisstionChild.Count > 0)
            {
                if (CheckPermistion.CheckPermission(menuPermisstionChild, MenuMa, CheckPermistion.Xem))
                {
                    BindGrid();
                }
                else
                {
                    Response.Redirect("~/Administration/Default.aspx", false);
                    return;
                }
                if (!CheckPermistion.CheckPermission(menuPermisstionChild, MenuMa, CheckPermistion.Them))
                {
                    btnOpenModal.Visible = false;
                }
                if (!CheckPermistion.CheckPermission(menuPermisstionChild, MenuMa, CheckPermistion.Sua))
                {
                    foreach (GridViewRow row in GridViewUser.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            // Tìm LinkButton trong hàng
                            LinkButton lnkRole = (LinkButton)row.FindControl("lnkRole");
                            LinkButton btnDetail = (LinkButton)row.FindControl("btnDetail");
                            if (lnkRole != null && btnDetail != null)
                            {
                                // Đổi trạng thái bật/tắt của nút (toggle)
                                lnkRole.Enabled = false;
                                lnkRole.CssClass = "text-muted text-decoration-none";
                                //btnDetail.Enabled = false;
                                btnDetail.Visible = false;
                            }
                        }
                    }
                    //lnkRole

                }
                if (!CheckPermistion.CheckPermission(menuPermisstionChild, MenuMa, CheckPermistion.Xoa))
                {
                    foreach (GridViewRow row in GridViewUser.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            // Tìm LinkButton trong hàng
                            LinkButton btnDelete = (LinkButton)row.FindControl("btnDelete");
                            if (btnDelete != null)
                            {
                                btnDelete.Visible = false;
                            }
                        }
                    }

                }


            }
        }

        protected void btnSeachFor_click(object sender, EventArgs e)
        {

        }
        private void BindGrid()
        {

            var useCollection = UserManagerBLL.UserGetAllAndRoleName();
            GridViewUser.DataSource = useCollection;
            GridViewUser.DataBind();
        }

        protected void btnUserAdd_Click(object sender, EventArgs e)
        {
            User user = new User();
            lblErrorMessage.Text = "";
            if (string.IsNullOrEmpty(txtFullName.Text.Trim()) || txtFullName.Text.Trim().Length <= 3)
            {
                lblErrorMessage.Text = "Fullname không được trống hoặc bé hơn 3 ký tự<br />";
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
            }
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()) || txtEmail.Text.Trim().Length <= 3)
            {
                lblErrorMessage.Text = lblErrorMessage.Text + "Email không hợp lệ<br /> ";
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
            }
            if (string.IsNullOrEmpty(txtUsername.Text.Trim()) || txtUsername.Text.Trim().Length <= 3 || 
                txtUsername.Text.Contains(" "))
            {
                lblErrorMessage.Text = lblErrorMessage.Text + "Username không hợp lệ<br />";
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
            }
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()) || txtPassword.Text.Trim().Length < 1)
            {
                lblErrorMessage.Text = lblErrorMessage.Text + "Mật khẩu không hợp lệ<br /> ";
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                
            }
            if (lblErrorMessage.Text.Length <= 0)
            {
                if (UserManagerBLL.UserGetByUserName(txtUsername.Text) != null)
                {
                    lblErrorMessage.Text = "Username đã tồn tại, chọn tên khác ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);

                }
                else
                {
                    user.FullName = txtFullName.Text;
                    user.Email = txtEmail.Text;
                    user.Phone = txtPhoneNumber.Text;
                    user.Username = txtUsername.Text;
                    user.Password = txtPassword.Text;
                    user.Dob = DateTime.Parse(txtDob.Text);

                    user = UserManagerBLL.InsertUser(user);
                    //int result = _userBLL.UserAdd(user);

                    if (user != null)
                    {
                        lblResult.Text = "Thêm mới thành công user";
                        ClearForm();
                        BindGrid();
                        lblErrorMessage.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CloseModal", "closeModal();", true);

                    }
                    else
                    {
                        lblResult.Text = "Thêm mới thất bại";
                    }

                }

            }

        }

        private void ClearForm()
        {
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtPhoneNumber.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtDob.Text = null;
        }

        protected void GridViewUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int userId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "detail")
            {
                var user = UserManagerBLL.UserGetById(userId);
                if (user == null)
                {
                    lblResult.Text = "User không tồn tại :))";
                }
                else
                {
                    txtIdUser.Text = user.Id.ToString();
                    txtEditFullname.Text = user.FullName;
                    txtEditEmail.Text = user.Email;
                    TxtEditPhone.Text = user.Phone;
                    txtEditUsername.Text = user.Username;
                    txtEditPassword.Text = user.Password;
                    txtEditDob.Text = ((DateTime)user.Dob).ToString("yyyy-MM-dd") ;
                    
                    chkEditActive.Checked = user.Active ?? false;

                    string script = "openEdit();";

                    // Đăng ký đoạn mã JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "openEditModal", script, true);

                }
            }
            else if(e.CommandName == "UpdateRole")
            {

                txtEditRoleIdUser.Text = userId.ToString();

                var roleOfUsers = UserManagerBLL.UserGetAllAndRoleName(userId);
                GridViewRole.DataSource = roleOfUsers;
                GridViewRole.DataBind();

                string script = "openRoleEditModal();";

                // Đăng ký đoạn mã JavaScript
                ScriptManager.RegisterStartupScript(this, GetType(), "openRoleEditModal", script, true);

            }
        }
        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            int userId = int.Parse(hdnUserId.Value); // Lấy giá trị userId từ HiddenField
            lblResult.Text = UserDelete(userId);
            BindGrid();                                  // Thực hiện việc xóa user bằng userId
        }
        protected void btnUserEdit_Click(object sender, EventArgs e)
        {
            int idUser = int.Parse(txtIdUser.Text);
            var userUpdate = UserManagerBLL.UserGetById(idUser);
            userUpdate.FullName = txtEditFullname.Text;
            userUpdate.Email = txtEditEmail.Text;
            userUpdate.Phone = TxtEditPhone.Text;
            userUpdate.Username = txtEditUsername.Text;
            DateTime dob;
            if (DateTime.TryParseExact(txtEditDob.Text, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out dob))
            {
                userUpdate.Dob = dob;
            }
       
            if (txtEditPassword.Text.Length > 1)
            {
                userUpdate.Password = txtEditPassword.Text;
            }
            userUpdate.Active = chkEditActive.Checked ? true : false;
            userUpdate = UserManagerBLL.UserUpdate(userUpdate);                                 // Thực hiện việc xóa user bằng userId
            BindGrid();

        }
        protected void btnRoleEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnkRole = (LinkButton)sender;

            // Lấy giá trị CommandArgument, đây chính là Id của đối tượng
            string userId = lnkRole.CommandArgument;
            txtEditRoleIdUser.Text = userId;

            var roleOfUsers = UserManagerBLL.UserGetAllAndRoleName(userId);
            GridViewRole.DataSource = roleOfUsers;
            GridViewRole.DataBind();

            string script = "openRoleEditModal();";

            // Đăng ký đoạn mã JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "openRoleEditModal", script, true);
        }
        protected void btnRoleEditSave_Click(object sender, EventArgs e)
        {
            string listIdRole = "";
            foreach (GridViewRow row in GridViewRole.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Lấy giá trị từ cột "IdRole" (Id)
                    string idRole = row.Cells[0].Text;  // Giả sử cột "IdRole" là cột đầu tiên

                    // Lấy giá trị của checkbox "IsHaveRole"
                    CheckBox chkIsHaveRole = (CheckBox)row.FindControl("chkIsHaveRole");

                    if (chkIsHaveRole != null && chkIsHaveRole.Checked)
                    {
                        bool isHaveRole = chkIsHaveRole.Checked;

                        
                        if (!string.IsNullOrEmpty(listIdRole))
                        {
                            listIdRole += ",";
                        }
                        listIdRole += idRole;
                    }
                }
            }
            if (txtEditRoleIdUser.Text.Length>0)
            {
                if (UserManagerBLL.UpdateRoleForUser(int.Parse(txtEditRoleIdUser.Text), listIdRole) > 0)
                {
                    lblResult.Text = "Cập nhật vai trò thành công";
                    BindGrid();
                }
                else
                {
                    lblResult.Text = "Cập nhật vai trò thất bại";
                }

            }
            txtEditRoleIdUser.Text = "";

        }
        private string UserDelete(int id)
        {
            if(UserManagerBLL.UserGetById(id) == null)
            {
                return "User không tồn tại";
            }
            else if (UserManagerBLL.UserCheckAdmin(id)!=null)
            {
                return "Bạn không thể xóa user là admin";      }
            else if (UserManagerBLL.UserCheckReferencePost(id)!= null)
            {
                return "User này đang là tác giả ";
            }
            else
            {
                var result = UserManagerBLL.UserDelete(id);
                if (result)
                    return "Xóa user thành công";
                else
                    return "Xóa user KHÔNG thành công";
            }
        }


    }
}
