using BLLayer.Manager;
using DALLayer;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebformLayer.Administration.Common;
using WebformLayer.Administration.LeftNavbar;

namespace WebformLayer.Administration
{
    public partial class CategoryManager : CommonPage
    {
        public override string MenuMa { get; set; } = "Quan-ly-danh-muc";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void GetPaging(int totalPost, int pageIndex = 1, int pageSize = 10)
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
        private void BindGrid(int pagindex = 1)
        {
            int totalPost = 0;

            var useCollection = new List<Category>();
            if (!int.TryParse(hdPageIndex.Value, out pagindex))
            {
                pagindex = 1; // Gán giá trị mặc định nếu hdPageIndex.Value không hợp lệ
            }


            useCollection = CategoryManagerBLL.GetAllSeachAndPaging(10, pagindex, txtSearch.Text, null, out totalPost);

            GetPaging(totalPost, pagindex);


            lbltotal.Text = totalPost.ToString() + " danh mục";
            GridViewCategory.DataSource = useCollection;
            GridViewCategory.DataBind();
        }

        protected void HiddenButtonPaging_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            List<MenuPermisstion> menuPermisstionChild = new List<MenuPermisstion>();

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
                    foreach (GridViewRow row in GridViewCategory.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            // Tìm LinkButton trong hàng
                            LinkButton btnEdit = (LinkButton)row.FindControl("btnEdit");
                            if (btnEdit != null )
                            {
                                // Đổi trạng thái bật/tắt của nút (toggle)
                                btnEdit.Visible = false;
                                //btnEdit.Enabled = false;
                            }
                        }
                    }
                }
                if (!checkPermistion.CheckPermission(menuPermisstionChild, MenuMa, CheckPermistion.Xoa))
                {
                    foreach (GridViewRow row in GridViewCategory.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
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


        protected void btnCategoryAdd_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            lblAddErrorMessage.Text = "";
            if (string.IsNullOrEmpty(txtAddName.Text.Trim()) || txtAddName.Text.Trim().Length <= 3)
            {
                lblAddErrorMessage.Text = "Tiêu đề không được quá ngắn <br />";
            }
            if (string.IsNullOrEmpty(txtAddDescription.Text.Trim()) || txtAddDescription.Text.Trim().Length <= 3)
            {
                lblAddErrorMessage.Text = lblAddErrorMessage.Text + "Mô tả bài viết quá ngắn<br /> ";
            }
            if (string.IsNullOrEmpty(txtAddMa.Text.Trim()) || txtAddMa.Text.Trim().Length <= 3 || txtAddMa.Text.Contains(" "))
            {
                lblAddErrorMessage.Text = lblAddErrorMessage.Text + "Mã không đúng định dạng <br /> ";
            }

            if (lblAddErrorMessage.Text.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                return;

            }
            else
            {
                if (CategoryManagerBLL.IsExistsCategoryMa(txtAddMa.Text) != null)
                {
                    lblAddErrorMessage.Text = @"Mã dạnh mục : " + txtAddMa.Text + " đã tồn tại trong cơ sỡ dữ liệu.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                    return;
                }
                else
                {
                    category.Name = txtAddName.Text;
                    category.Ma = txtAddMa.Text;
                    category.Description = txtAddDescription.Text;
                    category.Active = true;
                    category = CategoryManagerBLL.Insert(category);
                    //int result = _userBLL.UserAdd(user);
                    if (category != null)
                    {
                        lblResult.Text = "Thêm mới bài viết thành công";
                        ClearForm();
                        lblAddErrorMessage.Text = "";
                        LoadData();


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
            txtSearch.Text = "";
            hdPageIndex.Value = "";

            txtAddName.Text = "";
            txtAddDescription.Text = "";
        }

        protected void GridViewCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            int categoryId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "editCategory")
            {
                
                lblEditError.Text = "";
                    var category = CategoryManagerBLL.GetById(categoryId);
               
                    if (category == null)
                    {
                        lblResult.Text = "Danh mục không tồn tại";
                    }
                    
                    else
                    {


                        txtEditId.Text = category.Id.ToString();
                        txtEditName.Text = category.Name;
                        txtEditMa.Text = category.Ma;
                        txtEditDescription.Text = category.Description;
                        chkEditActive.Checked = category.Active ?? false;

                        string script = "openEdit();";
                        // Đăng ký đoạn mã JavaScript
                        ScriptManager.RegisterStartupScript(this, GetType(), "openEditModal", script, true);

                    }
                
            }
        }
        protected void btnDeleteCategory_Click(object sender, EventArgs e)
        {

            int categoryId = int.Parse(hdnDeleteId.Value); // Lấy giá trị userId từ HiddenField

            if (CategoryManagerBLL.GetById(categoryId) == null)
            {
                lblResult.Text = "Danh mục không tồn tại";
                return;
            }
            else
            {
                if (CategoryManagerBLL.Delete(categoryId))
                {
                    lblResult.Text = "Đã xóa danh mục";
                    LoadData();
                    return;
                }
                else
                {
                    lblResult.Text = "Xóa danh mục không thành công";
                }


            }

        }
        protected void btnCategoryEdit_Click(object sender, EventArgs e)
        {

            lblEditError.Text = "";
            if (string.IsNullOrEmpty(txtEditName.Text.Trim()) || txtEditName.Text.Trim().Length <= 3)
            {
                lblEditError.Text = "Tiêu đề không được quá ngắn <br />";
            }
            if (string.IsNullOrEmpty(txtEditDescription.Text.Trim()) || txtEditDescription.Text.Trim().Length <= 3)
            {
                lblEditError.Text = lblAddErrorMessage.Text + "Mô tả bài viết quá ngắn<br /> ";
            }
            if (string.IsNullOrEmpty(txtEditMa.Text.Trim()) || txtEditMa.Text.Trim().Length <= 3 || txtEditMa.Text.Contains(" "))
            {
                lblEditError.Text = lblAddErrorMessage.Text + "Mã không đúng định dạng <br /> ";
            }

            if (lblEditError.Text.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "openEditModal", "openEdit();", true);
                return;

            }
            else
            {
                lblResult.Text = "";
                int categoryId = int.Parse(txtEditId.Text);

                if (CategoryManagerBLL.IsExistsCategoryMaOtherID(txtEditMa.Text, categoryId) != null)
                {
                    lblEditError.Text = @"Mã dạnh mục : " + txtAddMa.Text + " đã tồn tại trong cơ sỡ dữ liệu.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "openEditModal", "openEdit();", true);
                    return;
                }    
                var categoryUpdate = CategoryManagerBLL.GetById(categoryId);

                categoryUpdate.Name = txtEditName.Text;
                categoryUpdate.Description = txtEditDescription.Text;
                categoryUpdate.Ma = txtEditMa.Text;
                categoryUpdate.Active = chkEditActive.Checked ? true : false;

                categoryUpdate = CategoryManagerBLL.Update(categoryUpdate);
                if (categoryUpdate == null)
                {
                    lblResult.Text = "Update Không thành công!";
                }
                else
                {
                    LoadData();
                    lblResult.Text = "Update thành công";
                }
                return;
            }
        }



        protected void btnSeachFor_click(object sender, EventArgs e)
        {

            hdPageIndex.Value = 1.ToString();
            lblResult.Text = txtSearch.Text;
            LoadData();

        }



    }
}