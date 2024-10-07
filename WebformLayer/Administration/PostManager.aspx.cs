using BLLayer.Manager;
using DALLayer;
using SweetCMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebformLayer.Administration.LeftNavbar;
using WebformLayer.Administration.MasterAdminPage;


namespace WebformLayer.Administration
{
    public partial class PostEdit : System.Web.UI.Page
    {

        private const string MenuMa = "Quan-ly-bai-viet";
        CheckPermistion CheckPermistion = new CheckPermistion();

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

            var useCollection = new List<PostAndAuthorName>();
            if (!int.TryParse(hdPageIndex.Value, out pagindex))
            {
                pagindex = 1; // Gán giá trị mặc định nếu hdPageIndex.Value không hợp lệ
            }


            useCollection = PostManagerBLL.GetAllPostAndAuthorName(10, pagindex, txtSearch.Text, null, out totalPost);

            GetPaging(totalPost, pagindex);


            lbltotal.Text = totalPost.ToString() + " bài viết";
            GridViewPost.DataSource = useCollection;
            GridViewPost.DataBind();
        }

        private void LoadData()
        {
            List<MenuPermisstion> menuPermisstionChild = new List<MenuPermisstion>();

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

                btnOpenModal.Visible = CheckPermistion.CheckPermission(menuPermisstionChild, MenuMa, CheckPermistion.Them);
                bool isSua = CheckPermistion.CheckPermission(menuPermisstionChild, MenuMa, CheckPermistion.Sua);
                bool isXoa = CheckPermistion.CheckPermission(menuPermisstionChild, MenuMa, CheckPermistion.Xoa);


                foreach (GridViewRow row in GridViewPost.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        // Tìm LinkButton trong hàng
                        LinkButton btnEdit = (LinkButton)row.FindControl("btnEdit");
                        LinkButton lblCategoryEdit = (LinkButton)row.FindControl("lblCategoryEdit");
                        LinkButton btnDelete = (LinkButton)row.FindControl("btnDelete");

                        // Đổi trạng thái bật/tắt của nút (toggle)
                        btnEdit.Visible = isSua;
                        if (!isSua) lblCategoryEdit.CssClass = "text-muted text-decoration-none";
                        lblCategoryEdit.Enabled = isSua;
                        btnDelete.Visible = isXoa;
                    }

                }

            }
        }

        protected void HiddenButton_Click(object sender, EventArgs e)
        {
            LoadData();
        }



        protected void btnRoleAdd_Click(object sender, EventArgs e)
        {
            Post postAdd = new Post();
            lblAddErrorMessage.Text = "";
            if (string.IsNullOrEmpty(txtAddName.Text.Trim()) || txtAddName.Text.Trim().Length <= 3)
            {
                lblAddErrorMessage.Text = "Tiêu đề không được quá ngắn <br />";
            }
            if (string.IsNullOrEmpty(txtAddDescription.Text.Trim()) || txtAddDescription.Text.Trim().Length <= 3)
            {
                lblAddErrorMessage.Text = lblAddErrorMessage.Text + "Mô tả bài viết quá ngắn<br /> ";
            }
            if (string.IsNullOrEmpty(txtAddContent.Text.Trim()) || txtAddContent.Text.Trim().Length <= 3)
            {
                lblAddErrorMessage.Text = lblAddErrorMessage.Text + "Nội dung bài viết quá ngắn <br />";
            }
            if (string.IsNullOrEmpty(uploadAddImge.FileName))
            {
                lblAddErrorMessage.Text = lblAddErrorMessage.Text + "Vui lòng thêm ảnh cho bài viết <br />";
            }

            if (lblAddErrorMessage.Text.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                return;
            }
            if (UploadFile(uploadAddImge) == null)
            {
                lblAddErrorMessage.Text = "File ảnh đã tồn tại trong dữ liệu, vui lòng đổi tên khác";
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                return;
            }
            else
            {
                if (PostManagerBLL.IsNameExists(txtAddName.Text) != null)
                {
                    lblAddErrorMessage.Text = @"Bài viết có tiêu đề '" + txtAddName.Text + "' đã tồn tại trong cơ sỡ dữ liệu.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenModal", "openModal();", true);
                    return;
                }
                else
                {
                    postAdd.Name = txtAddName.Text;
                    postAdd.Description = txtAddDescription.Text;
                    postAdd.Content = txtAddContent.Text;
                    postAdd.DatetimeCreate = DateTime.Now;
                    postAdd.AuthorID = int.Parse(Session["userId"].ToString());
                    postAdd.ViewCount = 0;
                    postAdd.Active = true;

                    postAdd.ImageUrl = uploadAddImge.FileName;
                    postAdd = PostManagerBLL.Insert(postAdd);

                    //int result = _userBLL.UserAdd(user);

                    if (postAdd != null)
                    {
                        lblResult.Text = "Thêm mới bài viết thành công";
                        ClearForm();
                        lblAddErrorMessage.Text = "";
                        //List<int> selectedCategories = new List<int>();
                        //foreach (ListItem item in lstCategory.Items)
                        //{
                        //    if (item.Selected)
                        //    {
                        //        CategoryDetail categoryDetail = new CategoryDetail()
                        //        {
                        //            CategoryId = int.Parse(item.Value),
                        //            PostId = postAdd.Id
                        //        };
                        //        CategoryDetailManagerBLL.InsertCategory(categoryDetail);  // Hoặc item.Text để lấy tên
                        //    }
                        //}
                        // truyền vào sset tinh
                        LoadData();
                    }
                    else
                    {
                        lblResult.Text = "Thêm mới thất bại";
                    }
                }
            }
        }

        private string UploadFile(FileUpload fileUpload)
        {

            string FileName = fileUpload.FileName.ToString();
            string FileSaveLocation = @"~/Administration/Upload/" + FileName;
            FileSaveLocation = Server.MapPath(FileSaveLocation);

            if (System.IO.File.Exists(FileSaveLocation))
            {
                return null; //result = "Tên file đã tồn tại trong cơ sở dữ liệu";
            }
            else
            {
                fileUpload.PostedFile.SaveAs(FileSaveLocation);
                return FileName; // fileUpload.FileName.ToString() + "Gửi file thành công";
            }

        }
        private bool RemoveFileUpload(string fileName)
        {
            string FileSaveLocation = @"~/Administration/Upload/" + fileName;
            FileSaveLocation = Server.MapPath(FileSaveLocation);
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
        private void ClearForm()
        {
            txtSearch.Text = "";
            hdPageIndex.Value = "";

            txtAddName.Text = "";
            txtAddDescription.Text = "";
            txtAddContent.Text = "";
        }

        protected void GridViewUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editPost")
            {
                //int idPost = Convert.ToInt32(e.CommandArgument);
                string[] args = e.CommandArgument.ToString().Split(',');

                // Lấy giá trị Id và Name
                int idPost = Convert.ToInt32(args[0]);
                string nameAuthorPost = args[1];


                var post = PostManagerBLL.GetPostById(idPost);
                if (post == null)
                {
                    lblResult.Text = "Post không tồn tại";

                }
                else
                {
                    txtEditIdPost.Text = post.Id.ToString();
                    txtEditName.Text = post.Name;
                    txtEditDescription.Text = post.Description;
                    txtEditContent.Text = post.Content;
                    lblAuthorName.Text = nameAuthorPost;
                    lblViewCount.Text = post.ViewCount.ToString();
                    txtEditDatetimeCreate.Text = ((DateTime)post.DatetimeCreate).ToString("yyyy-MM-dd");

                    chkEditActive.Checked = post.Active ?? false;
                    imageShow.ImageUrl = post.ImageUrl != null ? "Upload/" + post.ImageUrl : "Upload/default.png" ;
                    
                    string script = "openEdit();";

                    // Đăng ký đoạn mã JavaScript
                    ScriptManager.RegisterStartupScript(this, GetType(), "openEditModal", script, true);

                }
            }
        }
        protected void btnDeletePost_Click(object sender, EventArgs e)
        {

            int roleId = int.Parse(hdnPostId.Value); // Lấy giá trị userId từ HiddenField

            if (PostManagerBLL.GetPostById(roleId) == null)
            {
                lblResult.Text = "Bài viết không tồn tại";
                return;
            }
            else
            {
                if (PostManagerBLL.Delete(roleId))
                {
                    lblResult.Text = "Đã xóa bài viết";
                    BindGrid();
                    return;
                }
                else
                {
                    lblResult.Text = "Xóa bài viết không thành công";
                }


            }

        }
        protected void btnPostEdit_Click(object sender, EventArgs e)
        {
            lblResult.Text = "";
            int postId = int.Parse(txtEditIdPost.Text);
            var postUpdate = PostManagerBLL.GetPostById(postId);

            if (uploadImageEdit.HasFile)
            {
                string fileName = UploadFile(uploadImageEdit);
                if (fileName == null)
                {
                    lblEditError.Text = "File ảnh đã tồn tại trong dữ liệu, vui lòng đổi tên khác";
                    ScriptManager.RegisterStartupScript(this, GetType(), "openEditModal", "openEdit();", true);
                    return;
                }
                else
                {
                    RemoveFileUpload(postUpdate.ImageUrl);
                    postUpdate.ImageUrl = uploadImageEdit.FileName;

                }
            }
            postUpdate.Name = txtEditName.Text;
            postUpdate.Description = txtEditDescription.Text;
            postUpdate.Content = txtEditContent.Text;
            postUpdate.Active = chkEditActive.Checked ? true : false;

            DateTime updateDateCreate;
            if (DateTime.TryParseExact(txtEditDatetimeCreate.Text, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out updateDateCreate))
            {
                postUpdate.DatetimeCreate = updateDateCreate;
            }
            else
            {
                lblResult.Text = "Không ép kiểu được datetime";
                return;
            }
            postUpdate = PostManagerBLL.Update(postUpdate);                                 // Thực hiện việc xóa user bằng userId
            LoadData();
            lblResult.Text = "Update thành công";
            return;

        }

        protected void btnCategoryEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnkPost = (LinkButton)sender;

            // Lấy giá trị CommandArgument, đây chính là Id của đối tượng
            string postId = lnkPost.CommandArgument;
            txtEditCategoryPostId.Text = postId;

            var roleOfUsers = PostManagerBLL.GetAllCategoryByPostId(int.Parse(postId));
            GridViewCategory.DataSource = roleOfUsers;
            GridViewCategory.DataBind();

            string script = "openCategoryEditModal();";

            // Đăng ký đoạn mã JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "openCategoryEditModal", script, true);
        }

        protected void btnThemMoiPost(object sender, EventArgs e)
        {
            //List<Category> categories = PostManagerBLL.GetAllCategory();
            //lstCategory.DataSource = categories;
            //lstCategory.DataTextField = "Name";  // Tên danh mục hiển thị
            //lstCategory.DataValueField = "Id";   // Giá trị Id của danh mục
            //lstCategory.DataBind();


            string script = "openModal();";

            // Đăng ký đoạn mã JavaScript
            ScriptManager.RegisterStartupScript(this, GetType(), "openModal", script, true);
        }

        protected void btnCategoryEditSave_Click(object sender, EventArgs e)
        {


            List<CategoryPostId> listNewUpdate = new List<CategoryPostId>();

            foreach (GridViewRow MenuPermissionUpdate in GridViewCategory.Rows)
            {

                if (MenuPermissionUpdate != null)
                {
                    Label lblCategoryId = (Label)MenuPermissionUpdate.FindControl("lblCategoryId");// Lấy hàng thứ 3 (index 2)
                    CheckBox chkIsHaveCategory = (CheckBox)MenuPermissionUpdate.FindControl("chkIsHaveCategory");// Lấy hàng thứ 3 (index 2)

                    if (!string.IsNullOrEmpty(lblCategoryId.Text))
                    {



                        CategoryPostId newUpdate = new CategoryPostId()
                        {
                            Id = int.Parse(lblCategoryId.Text),
                            IsHaveCategory = chkIsHaveCategory.Checked ? 1 : 0
                        };
                        listNewUpdate.Add(newUpdate);

                    }
                    else
                    {
                        lblResult.Text = "Không gửi được dữ liệu lên máy chủ";
                        return;
                    }
                }
                else
                {
                    lblResult.Text = "Không thể kiểm tra dữ liệu trong hàng";
                    return;
                }
            }

            int roleId = int.Parse(txtEditCategoryPostId.Text);
            int result = PostManagerBLL.UpdateCategoryByPostId(listNewUpdate, roleId);
            if (result > 0)
            {

                lblResult.Text = "Chỉnh sửa thành công";
                LoadData();
            }
            else
                lblResult.Text = "Chỉnh sửa không thành công";

        }

        protected void btnSeachFor_click(object sender, EventArgs e)
        {

            hdPageIndex.Value = 1.ToString();

            LoadData();
            //BindGrid(PostManagerBLL.GetAllPostAndAuthorName(null, null, txtSearch.Text, null));

        }

        private void PostPaging()
        {
            //BindGrid(PostManagerBLL.geta);
        }




    }
}