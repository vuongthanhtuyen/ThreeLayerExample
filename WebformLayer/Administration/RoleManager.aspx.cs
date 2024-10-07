using BLLayer;
using BLLayer.Manager;
using DALLayer;
using Microsoft.Ajax.Utilities;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebformLayer.Administration.Common;
using WebformLayer.Administration.LeftNavbar;

namespace WebformLayer.Administration
{
    public partial class UserAdd : CommonPage
    {

        //private static readonly UserBLL _userData = new UserBLL();
        public override string MenuMa { get; set; } = "Quan-ly-quyen";
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();

        }

        protected void btnSeachFor_click(object sender, EventArgs e)
        {
            //LoadData();
        }
        private void LoadData()
        {            
            
            List<MenuPermisstion> menuPermisstionChild = new List<MenuPermisstion>();

            btnOpenModal.Visible = checkPermistion.CheckPermission(menuPermisstionChild, MenuMa, CheckPermistion.Them);

            if ((List<MenuPermisstion>)Session["MenuPermission"] != null)
            {
                menuPermisstionChild = checkPermistion.GetMenuPermisstions(Session["MenuPermission"], MenuMa);
            }
            else
            {
                Response.Redirect("~/Administration/Login.aspx", false);
                return;
            }
            if (menuPermisstionChild != null && menuPermisstionChild.Count > 0)
            {
                if (checkPermistion.CheckPermission(menuPermisstionChild, MenuMa, CheckPermistion.Xem))
                {
                    BindGrid();
                }
                else
                {
                    Response.Redirect("~/Administration/Default.aspx", false);
                    return;
                }
                if (!checkPermistion.CheckPermission(menuPermisstionChild, MenuMa, CheckPermistion.Them))
                {
                    btnOpenModal.Visible = false;
                }
                if (!checkPermistion.CheckPermission(menuPermisstionChild, MenuMa, CheckPermistion.Sua))
                {
                    foreach (GridViewRow row in GridViewRole.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            // Tìm LinkButton trong hàng
                            LinkButton lnkRole = (LinkButton)row.FindControl("btnUpdateRole");
                            LinkButton btnDetail = (LinkButton)row.FindControl("btnEdit");
                            if (lnkRole != null && btnDetail != null)
                            {
                                lnkRole.Visible = false;
                                btnDetail.Visible = false;
                            }
                        }
                    }
                    //lnkRole

                }
                if (!checkPermistion.CheckPermission(menuPermisstionChild, MenuMa, CheckPermistion.Xoa))
                {
                    foreach (GridViewRow row in GridViewRole.Rows)
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

        private void BindGrid()
        {
            var ConllectRole = RoleManagerBLL.GetAllRole();
            GridViewRole.DataSource = ConllectRole;
            GridViewRole.DataBind();
        }
        private void BindGridMenuPermision()
        {
            var ConllectRole = RoleManagerBLL.GetAllRole();
            GridViewRole.DataSource = ConllectRole;
            GridViewRole.DataBind();
        }

        private void ClearForm()
        {
            txtName.Text = "";
            txtMa.Text = "";
        
        }
        protected void btnRoleAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Role role = new Role();
                lblErrorMessage.Text = "";
                if (string.IsNullOrEmpty(txtName.Text.Trim()) || txtName.Text.Trim().Length <= 1)
                {
                    lblErrorMessage.Text = "Tên role không được bỏ trống <br />";
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                }
                if (string.IsNullOrEmpty(txtMa.Text.Trim()) || txtMa.Text.Trim().Length <= 1 || string.IsNullOrWhiteSpace(txtMa.Text))
                {
                    lblErrorMessage.Text = lblErrorMessage.Text + " Mã không hợp lệ <br /> ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                }

                if (lblErrorMessage.Text.Length <= 0)
                {
                    if (RoleManagerBLL.IsExistsRoleMa(txtMa.Text) != null)
                    {
                        lblErrorMessage.Text = "Mã role này tồn tại trong cơ sở dữ liệu";
                        ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);

                    }
                    else
                    {
                        role.Name = txtName.Text;
                        role.Ma = txtMa.Text;
                        role.Active = true;


                        role = RoleManagerBLL.Insert(role);
                        //int result = _userBLL.UserAdd(user);

                        if (role != null)
                        {
                            lblResult.Text = "Thêm mới thành công user";
                            ClearForm();
                            LoadData();
                            lblErrorMessage.Text = "";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CloseModal", "closeModal();", true);
                            return;
                        }
                        else
                        {
                            lblResult.Text = "Thêm mới thất bại";
                            return;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                lblResult.Text = "Thêm mới thất bại " + ex.Message;


            }
        }




        protected void btnEditRoleSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblErrorMessageEdit.Text = "";

                int roleId = int.Parse(txtEditRoleId.Text);

                if (string.IsNullOrEmpty(txtEditRoleName.Text.Trim()) || txtEditRoleName.Text.Trim().Length <= 1)
                {
                    lblErrorMessageEdit.Text = "Tên role không được bỏ trống <br />";
                }
                if (string.IsNullOrEmpty(txtEditRoleMa.Text.Trim()) || txtEditRoleMa.Text.Trim().Length <= 1 || string.IsNullOrWhiteSpace(txtEditRoleMa.Text))
                {
                    lblErrorMessageEdit.Text = lblErrorMessageEdit.Text + " Mã không hợp lệ <br /> ";
                }


                if (lblErrorMessageEdit.Text.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "openEdit", "openEdit();", true);
                    return;

                }
                if (RoleManagerBLL.IsExistsRoleMaUpdate(txtEditRoleMa.Text, roleId) != null)
                {
                    lblErrorMessageEdit.Text = lblErrorMessageEdit.Text + "Tên này đã tồn tại trong cơ sở dữ liệu";
                    ScriptManager.RegisterStartupScript(this, GetType(), "openEdit", "openEdit();", true);
                    return;

                }
                else
                {
                    var updateRole = RoleManagerBLL.GetRoleById(roleId);

                    if (updateRole == null)
                    {
                        lblErrorMessageEdit.Text = "Lỗi không thấy tìm thấy role để chỉnh sửa";
                        ScriptManager.RegisterStartupScript(this, GetType(), "openEdit", "openEdit();", true);
                        return;

                    }

                    else
                    {
                        updateRole.Ma = txtEditRoleMa.Text;
                        updateRole.Name = txtEditRoleName.Text;
                        updateRole.Active = ckbEditActive.Checked ? true : false;
                        updateRole = RoleManagerBLL.Update(updateRole);
                        if (updateRole != null)
                        {
                            lblResult.Text = "Cập nhật role thành công";
                            LoadData();
                            return;
                        }
                        lblResult.Text = "Cập nhật role không thành công";
                        return;

                    }

                }

            }
            catch (Exception ex)
            {
                lblResult.Text = "Lỗi : " + ex.Message;
            }
        }


        protected void GridViewRole_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int roleId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "editRole")
            {
                var roleEdit = RoleManagerBLL.GetRoleById(roleId);
                if (roleEdit == null)
                {
                    lblResult.Text = "Không tìm thấy role này, vui lòng thử lại sau";
                    return;
                }
                else
                {
                    txtEditRoleId.Text = roleEdit.Id.ToString();
                    txtEditRoleName.Text = roleEdit.Name.ToString();
                    txtEditRoleMa.Text = roleEdit.Ma.ToString();
                    ckbEditActive.Checked = roleEdit.Active ?? false;

                    string script = "openEdit();";

                    // Đăng ký đoạn mã JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "openEditModal", script, true);

                }
            }
            else if (e.CommandName == "UpdateRole")
            {

                txtEditRoleIDetailMenu.Text = roleId.ToString();

                var roleOfUsers = RoleManagerBLL.GetAllMenuAndPermistion(roleId);
                GridRoleMenuPermission.DataSource = roleOfUsers;
                GridRoleMenuPermission.DataBind();

                string script = "openRoleDetailEditModal();";

                // Đăng ký đoạn mã JavaScript
                ScriptManager.RegisterStartupScript(this, GetType(), "openRoleDetailEditModal", script, true);

            }
        }
        protected void btnDeleteRole_Click(object sender, EventArgs e)
        {
            int roleId = int.Parse(hdnRoleId.Value); // Lấy giá trị userId từ HiddenField

            if (RoleManagerBLL.GetRoleById(roleId) == null)
            {
                lblResult.Text = "Role không tồn tại";
                return;
            }

            else
            {
                RoleManagerBLL.Delete(roleId);
                lblResult.Text = "Đã xóa role";
                LoadData();
                return;

            }

        }

        protected void btnRoleEditDetail_Click(object sender, EventArgs e)
        {
            LinkButton lnkRole = (LinkButton)sender;

            // Lấy giá trị CommandArgument, đây chính là Id của đối tượng
            string roleId = lnkRole.CommandArgument;
            txtEditRoleIDetailMenu.Text = roleId;

            var roleOfUsers = RoleManagerBLL.GetAllMenuAndPermistion(int.Parse(roleId));
            GridRoleMenuPermission.DataSource = roleOfUsers;
            GridRoleMenuPermission.DataBind();

            string script = "openRoleDetailEditModal();";

            // Đăng ký đoạn mã JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "openRoleDetailEditModal", script, true);
        }

        protected void btnRoleEditDetailSave_Click(object sender, EventArgs e)
        {

            List<MenuPermissionDetail> listNewUpdate = new List<MenuPermissionDetail>();

            foreach (GridViewRow MenuPermissionUpdate in GridRoleMenuPermission.Rows)
            {

                if (MenuPermissionUpdate != null)
                {
                    Label lblMenuId = (Label)MenuPermissionUpdate.FindControl("lblMenuId");// Lấy hàng thứ 3 (index 2)
                    Label lblPermissionId = (Label)MenuPermissionUpdate.FindControl("lblPermissionId");// Lấy hàng thứ 3 (index 2)
                    CheckBox chkIsHaveRole = (CheckBox)MenuPermissionUpdate.FindControl("chkIsHaveRole");// Lấy hàng thứ 3 (index 2)

                    if (!string.IsNullOrEmpty(lblMenuId.Text) && !string.IsNullOrEmpty(lblPermissionId.Text))
                    {

                        // If tồn tại rồi thì thôi- chưa thì thêm
                        // Nếu cái nào null thì remove ra, 
                        MenuPermissionDetail newUpdate = new MenuPermissionDetail()
                        {
                            MenuId = int.Parse(lblMenuId.Text),
                            PermissionId = int.Parse(lblPermissionId.Text),
                            IsRoleHavePermission = chkIsHaveRole.Checked ? 1 : 0
                        };
                        listNewUpdate.Add(newUpdate);

                    }
                    else
                    {
                        lblEditmenuPermissonDetail.Text = "Không gửi được dữ liệu lên máy chủ";
                        return;
                    }
                }
                else
                {
                    lblEditmenuPermissonDetail.Text = "Không thể kiểm tra dữ liệu trong hàng";
                    return;
                }
            }

            int roleId = int.Parse(txtEditRoleIDetailMenu.Text);
            int result = RoleManagerBLL.UpdateAllMenuPermission(listNewUpdate, roleId);
            if (result > 0)
            {
                lblResult.Text = "Chỉnh sửa thành công";
            }
            else
                lblResult.Text = "Chỉnh sửa không thành công";

        }

        protected void GridRoleMenuPermission_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int rowIndex = GridRoleMenuPermission.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRowCurrent = GridRoleMenuPermission.Rows[rowIndex];
                GridViewRow gvPreviousRow = GridRoleMenuPermission.Rows[rowIndex + 1];
                Label lblMenuName = (Label)gvPreviousRow.FindControl("lblMenuName"); // Thay "lblMenuName" bằng ID của control bạn sử dụng
                string previousMenuName = lblMenuName.Text;

                Label lblMenuNameCurrent = (Label)gvRowCurrent.FindControl("lblMenuName"); // Thay "lblMenuName" bằng ID của control bạn sử dụng
                string MenuNameCurrent = lblMenuNameCurrent.Text;



                if (MenuNameCurrent == previousMenuName)
                {
                    if (gvPreviousRow.Cells[0].RowSpan < 2)
                    {
                        gvRowCurrent.Cells[0].RowSpan = 2;
                    }
                    else
                    {
                        gvRowCurrent.Cells[0].RowSpan =
                                gvPreviousRow.Cells[0].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[0].Visible = false;

                }
            }



        }


    }
}