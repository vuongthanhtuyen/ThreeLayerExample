<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administration/MasterAdminPage/Admin.Master" CodeBehind="PostManager.aspx.cs" Inherits="WebformLayer.Administration.PostEdit" %>



<asp:Content ID="ctSearch" ContentPlaceHolderID="ctSearch" runat="server">
    <asp:TextBox ID="txtSearch"
        placeholder="Search for..."
        aria-label="Search"
        aria-describedby="basic-addon2"
        CssClass="form-control bg-light border-0 small"
        runat="server"></asp:TextBox>

    <div class="input-group-append">
        <asp:Button CssClass="btn btn-primary fas fa-search fa-sm" ID="btnSearchFor" runat="server" Text="Tìm kiếm" OnClick="btnSeachFor_click" />
    </div>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="Content1" runat="server">
    <style>
        input, select, textarea {
            max-width: none;
        }
    </style>

    <main>
        <div>
            <div class="col-xs-12 padding-none header-controls-right">
                <span class="notifications"></span>
                <asp:Button CssClass="btn btn-primary btn-sm btn-flat padding-fa mr-4" ID="btnOpenModal" OnClick="btnThemMoiPost" runat="server" Text="Thêm mới" />


                <asp:Label ID="lblResult" CssClass="text-info" runat="server" Text=""></asp:Label>
                <asp:Label ID="lbltotal" CssClass=".text-primary float-right" runat="server" Text=""></asp:Label>

            </div>
        </div>

        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:HiddenField ID="hdPageIndex" runat="server" />
                <asp:GridView ID="GridViewPost" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-striped table-bordered" CellPadding="10" CellSpacing="2"
                    GridLines="None" OnRowCommand="GridViewUser_RowCommand">

                    <Columns>

                        <asp:TemplateField HeaderText="Danh mục" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblCategoryEdit" runat="server"
                                    Text='<%# Eval("CategoryName") %>'
                                    CommandArgument='<%# Eval("Id") %>'
                                    OnClick="btnCategoryEdit_Click">
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="CategoryName" HeaderText="Danh mục" ItemStyle-Width="15%" />--%>
                        <asp:BoundField DataField="Name" ItemStyle-Width="20%" HeaderText="Tiêu đề" />
                        <%--<asp:BoundField DataField="Description" HeaderText="Mô tả" />--%>
                        <%--<asp:BoundField DataField="Content" HeaderText="Nội dung" />--%>
                        <asp:BoundField DataField="AuthorFullName" HeaderText="Tác giả" />
                        <asp:BoundField DataField="ViewCount" HeaderText="Số lượt xem" />
                        <asp:BoundField DataField="DatetimeCreate" HeaderText="Ngày tạo" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:CheckBox ID="IsAvtive" runat="server"
                                    Checked='<%# Eval("Active") %>' Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Hành Động">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary btn-xs btn-flat"
                                    CommandArgument='<%# Eval("Id") + "," + Eval("AuthorFullName") %>' CommandName="editPost" ToolTip="Sửa">
                            <span class="fa fa-eye"></span> Sửa
                                </asp:LinkButton>

                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger btn-xs btn-flat"
                                    data-id='<%# Eval("Id") %>' ToolTip="Modal"
                                    OnClientClick="return openDelete(this);">
                            <span class="fa fa-trash"></span> Xóa
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table class="table table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Danh mục</th>
                                    <th>Tiêu đề</th>
                                    <th>Tác giả</th>
                                    <th>Số lượt xem</th>
                                    <th>Ngày tạo</th>
                                    <th>Active</th>
                                    <th>Hành Động</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td colspan="7" class="text-center">Không có dữ liệu để hiển thị.</td>
                                </tr>
                            </tbody>
                        </table>
                    </EmptyDataTemplate>
                </asp:GridView>


                <%-- Edit Post Modal --%>
                <div id="myEditModal" class="modal">
                    <div class="modal-content-editPost">
                        <span class="close" onclick="closeEdit()">&times;</span>
                        <h4>Chỉnh sửa bài viết</h4>
                        <asp:Label ID="lblEditError" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
                        <div class="row" style="overflow-y: auto; overflow-x: hidden;">
                            <div class="col-lg-7 justify-content-center align-items-center" style="max-height: 500px;">
                                <asp:TextBox ID="txtEditIdPost" runat="server" CssClass="form-control" placeholder="IdPost" Visible="false"></asp:TextBox>

                                <div class="form-group d-flex align-items-center">

                                    <asp:Label ID="Label3" runat="server" CssClass="pb-2 mr-2" Text="Tiêu đề: " Style="white-space: nowrap; min-width: 80px;"></asp:Label>
                                    <asp:TextBox ID="txtEditName" runat="server" CssClass="form-control form-control-user flex-grow-1" placeholder="Tiêu đề"></asp:TextBox>
                                </div>

                                <div class="form-group d-flex align-items-center ">
                                    <asp:Label ID="Label4" runat="server" CssClass="pb-2 mr-2" Text="Mô tả: " Style="white-space: nowrap; min-width: 80px;"></asp:Label>

                                    <asp:TextBox ID="txtEditDescription" runat="server" CssClass="form-control form-control-user" placeholder="Mô tả" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="form-group d-flex align-items-center">
                                    <asp:Label ID="Label5" runat="server" CssClass="pb-2 mr-2" Text="Nội dung: " Style="white-space: nowrap; min-width: 80px;"></asp:Label>

                                    <asp:TextBox ID="txtEditContent" runat="server" CssClass="form-control form-control-user" placeholder="Nội dung" Height="200px" TextMode="MultiLine"></asp:TextBox>
                                </div>

                                <div class="form-group d-flex align-items-center">
                                    <asp:Label ID="Label6" runat="server" CssClass="pb-2 mr-2" Text="Ngày tạo: " Style="white-space: nowrap; min-width: 80px;"></asp:Label>

                                    <asp:TextBox ID="txtEditDatetimeCreate" runat="server" TextMode="Date" CssClass="form-control form-control-user" placeholder="Ngày tạo"></asp:TextBox>
                                </div>
                                <div class="form-group d-flex align-items-center">
                                    <asp:Label ID="Label7" runat="server" CssClass="pb-2 mr-2" Text="Active: " Style="white-space: nowrap; min-width: 80px;"></asp:Label>
                                    <asp:CheckBox ID="chkEditActive" runat="server" CssClass="ckeckboxActive" Width="250px" />
                                </div>


                                <hr>
                                <div class="form-group">
                                    <asp:Label ID="lblhienthi1" runat="server" CssClass="text-dark pb-2" Text="Tác giả: "></asp:Label>
                                    <asp:Label ID="lblAuthorName" runat="server" CssClass="text-danger pb-2" Text="" Font-Bold="True"></asp:Label>
                                    <br />
                                    <asp:Label ID="Label8" runat="server" CssClass="text-dark pb-2" Text="Tổng số view: "></asp:Label>

                                    <asp:Label ID="lblViewCount" runat="server" CssClass="text-danger pb-2" Text="" Font-Italic="True"></asp:Label>
                                </div>
                                <hr />

                            </div>
                            <div class="col-lg-4 justify-content-center align-items-center">

                                <%--<asp:Label ID="Label" runat="server" CssClass="pb-2 mr-2" Text="Ảnh:  " Style="white-space: nowrap; min-width: 80px;"></asp:Label>--%>
                                <%--<asp:FileUpload ID="uploadImageEdit" runat="server" />--%>
                                <%--<asp:Image ID="imageShow" CssClass="mt-5 ml-5" runat="server" />--%>


                                <div class="custom-container">
                                    <asp:FileUpload ID="uploadImageEdit" runat="server" onchange="previewImage(this)" Style="display: none;" />

                                    <asp:Image
                                        onclick="triggerUpload()"
                                        CssClass="custom-image m-2"
                                        ID="imageShow"
                                        Style="background-color: antiquewhite; height: auto; width: 300px; border: 3px solid #0af; transition: background ease-out 200ms; object-fit: cover; display: block;"
                                        runat="server"
                                        ImageUrl="https://st3.depositphotos.com/15648834/17930/v/600/depositphotos_179308454-stock-illustration-unknown-person-silhouette-glasses-profile.jpg" />

                                    <div class="custom-emoji" onclick="triggerUpload()" style="cursor: pointer;">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-pen" viewBox="0 0 16 16">
                                            <path d="m13.498.795.149-.149a1.207 1.207 0 1 1 1.707 1.708l-.149.148a1.5 1.5 0 0 1-.059 2.059L4.854 14.854a.5.5 0 0 1-.233.131l-4 1a.5.5 0 0 1-.606-.606l1-4a.5.5 0 0 1 .131-.232l9.642-9.642a.5.5 0 0 0-.642.056L6.854 4.854a.5.5 0 1 1-.708-.708L9.44.854A1.5 1.5 0 0 1 11.5.796a1.5 1.5 0 0 1 1.998-.001m-.644.766a.5.5 0 0 0-.707 0L1.95 11.756l-.764 3.057 3.057-.764L14.44 3.854a.5.5 0 0 0 0-.708z" />
                                        </svg>
                                    </div>


                                </div>
                                <div class="m-2 d-inline  ">
                                    <asp:Button ID="btnEditPost" runat="server" Text="Sửa" class="btn btn-primary btn-user btn-block" OnClick="btnPostEdit_Click" />
                                    <asp:Button ID="Button2" runat="server" Text="Hủy" class="btn btn-secondary btn-user btn-block" OnClientClick="closeEdit(); return false;" />

                                </div>
                            </div>
                        </div>

                    </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnEditPost" />
                <%--                <asp:PostBackTrigger ControlID="btnCategoryEdit" />--%>
            </Triggers>

        </asp:UpdatePanel>


    </main>



    <%-- Modal EditCategory --%>
    <div id="categoryEditModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="closeCateroryEditModal()">&times;</span>
            <h4>Thêm danh mục cho bài viết</h4>
            <%--            <asp:Label ID="lblEditCategoryDetail" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>--%>
            <div class="col">
                <div class="row d-flex justify-content-center align-items-center">
                    <asp:TextBox ID="txtEditCategoryPostId" runat="server" CssClass="form-control" placeholder="IdUser" Visible="false"></asp:TextBox>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridViewCategory" runat="server" AutoGenerateColumns="false"
                                CssClass="table table-striped" CellPadding="10" CellSpacing="2"
                                GridLines="None">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategoryId" runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="Id" HeaderText="Id" />--%>
                                    <asp:BoundField DataField="Name" HeaderText="Tên danh mục" />
                                    <asp:TemplateField HeaderText="Chọn danh mục">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkIsHaveCategory" runat="server"
                                                Checked='<%# Eval("IsHaveCategory").ToString() == "1" %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <hr>
                </div>
            </div>

            <asp:Button ID="btnCategoryEdit" runat="server" Text="Sửa" class="btn btn-primary btn-user btn-block" OnClick="btnCategoryEditSave_Click" />
            <asp:Button ID="btnCancelEditRole" runat="server" Text="Hủy" class="btn btn-secondary btn-user btn-block" OnClientClick="closeCateroryEditModal(); return false;" />
        </div>
    </div>
    <asp:HiddenField ID="hdfEditCategoryPostId" runat="server" />


    <style>
        .modal-content-editPost {
            background-color: white;
            margin: auto;
            padding: 20px;
            border: 1px solid #888;
            width: 80%;
        }

        input, select, textarea {
            max-width: none;
        }

        ul.pagination li {
            cursor: pointer;
        }

        .custom-container {
            position: relative;
            display: inline-block; /* Đảm bảo nó chỉ chiếm không gian cần thiết */
            text-align: center; /* Căn giữa nội dung bên trong */
        }

        .custom-image {
            display: block; /* Để loại bỏ khoảng trống bên dưới hình ảnh */
            margin: 0 auto; /* Căn giữa hình ảnh */
        }

        .custom-emoji {
            position: absolute;
            bottom: 20px; /* Điều chỉnh vị trí theo chiều dọc */
            right: 15px; /* Điều chỉnh vị trí theo chiều ngang */
            background-color: #fff; /* Màu nền cho biểu tượng */
            border-radius: 50%; /* Làm cho nó hình tròn */
            padding: 5px; /* Thêm một chút khoảng cách */
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.3); /* Thêm bóng cho biểu tượng */
        }
    </style>






    <%-- Modal add new post --%>
    <div id="myModal" class="modal">
        <div class="modal-content-editPost">
            <span class="close" onclick="closeModal()">&times;</span>
            <h4>Thêm bài viết mới</h4>
            <asp:Label ID="lblAddErrorMessage" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
            <div class="row">
                <div class="col-lg-12 justify-content-center align-items-center" style="max-height: 500px; overflow-y: auto; overflow-x: hidden;">

                    <div class="form-group d-flex align-items-center">
                        <asp:FileUpload ID="uploadAddImge" runat="server" />
                    </div>

                    <div class="form-group d-flex align-items-center">
                        <asp:Label ID="Label10" runat="server" CssClass="pb-2 mr-2" Text="Tiêu đề: " Style="white-space: nowrap; min-width: 80px;"></asp:Label>
                        <asp:TextBox ID="txtAddName" runat="server" CssClass="form-control form-control-user flex-grow-1" placeholder="Tiêu đề"></asp:TextBox>
                    </div>

                    <div class="form-group d-flex align-items-center ">
                        <asp:Label ID="Label11" runat="server" CssClass="pb-2 mr-2" Text="Mô tả: " Style="white-space: nowrap; min-width: 80px;"></asp:Label>

                        <asp:TextBox ID="txtAddDescription" runat="server" CssClass="form-control form-control-user" placeholder="Mô tả" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="form-group d-flex align-items-center">
                        <asp:Label ID="Label12" runat="server" CssClass="pb-2 mr-2" Text="Nội dung: " Style="white-space: nowrap; min-width: 80px;"></asp:Label>

                        <asp:TextBox ID="txtAddContent" runat="server" CssClass="form-control form-control-user" Height="200px" placeholder="Nội dung" TextMode="MultiLine"></asp:TextBox>
                    </div>

                    <div class="form-group d-flex align-items-center">
                        <asp:Label ID="Label14" runat="server" CssClass="pb-2 mr-2" Text="Active: " Style="white-space: nowrap; min-width: 80px;"></asp:Label>
                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="ckeckboxActive" Width="250px" />
                    </div>
                    <hr />
                    <asp:Button ID="btnUserAdd" runat="server" Text="Thêm Mới" class="btn btn-primary btn-user btn-block" OnClick="btnRoleAdd_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Hủy" class="btn btn-secondary btn-user btn-block" OnClientClick="closeModal(); return false;" />
                </div>
            </div>
        </div>
    </div>


    <%-- Modal delete comfirm --%>
    <div id="confirmDeleteModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="closeDelete(); return false;">&times;</span>
            <h5>Bạn có chắc chắn muốn xóa bài viết này?</h5>
            <asp:Label ID="Label2" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>

            <asp:Button ID="Button3" runat="server" Text="Xóa" class="btn btn-primary btn-user btn-block" OnClick="btnDeletePost_Click" />
            <asp:Button ID="Button4" runat="server" Text="Hủy" class="btn btn-secondary btn-user btn-block" OnClientClick="closeDelete(); return false;" />
        </div>
    </div>
    <asp:HiddenField ID="hdnPostId" runat="server" />



    <div class="align-items-center" style="justify-content: center; position: absolute;">
        <nav aria-label="...">
            <ul class="pagination">

                <asp:Literal ID="ltlPaging" runat="server"></asp:Literal>


            </ul>
        </nav>
    </div>
    <asp:Button ID="hiddenButtonPaging" runat="server" Text="Hidden Button" OnClick="HiddenButton_Click" Style="display: none;" />


    <script>
        function openModal() {
            document.getElementById("myModal").style.display = "block";
            document.getElementById("<%= lblResult.ClientID %>").innerText = "";
            return false;
        }
        function closeModal() {
            document.getElementById("myModal").style.display = "none";
            document.getElementById("<%= lblAddErrorMessage.ClientID %>").innerText = "";

        }

        function openDelete(linkButton) {
            document.getElementById("confirmDeleteModal").style.display = "block";
            var postId = linkButton.getAttribute('data-id'); // Lấy giá trị từ data-id laf 

            document.getElementById('<%= hdnPostId.ClientID %>').value = postId; // Đặt giá trị vào HiddenField
            document.getElementById("<%= lblResult.ClientID %>").innerText = "";

            return false;
        }

        function closeDelete() {
            document.getElementById("confirmDeleteModal").style.display = "none";

        }

        function openEdit() {
            document.getElementById("<%= lblResult.ClientID %>").innerText = "";
            document.getElementById("myEditModal").style.display = "block";
            return false;
        }

        function closeEdit() {
            document.getElementById("myEditModal").style.display = "none";

        }
        function openCategoryEditModal(id) {
            document.getElementById("<%= lblResult.ClientID %>").innerText = "";
            document.getElementById("categoryEditModal").style.display = "block";
            return false;
        }

        function closeCateroryEditModal() {
            document.getElementById("categoryEditModal").style.display = "none";

        }

        function triggerUpload() {
            document.getElementById('<%= uploadImageEdit.ClientID %>').click();
        }

        function previewImage(fileUpload) {
            const file = fileUpload.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    document.getElementById('<%= imageShow.ClientID %>').src = e.target.result;
                }
                reader.readAsDataURL(file);
            }
        }


    </script>
    <script src="https://code.jquery.com/jquery-3.7.1.min.js" integrity="sha256-/JqT3SQfawRcv/BIHPThkBvs0OEvtFFmqPF/lYI/Cxo=" crossorigin="anonymous"></script>
    <script>

        $(".page-link").click(function () {
            var pageIndex = 0;
            if ($(this).attr('id') === 'pagePrivious') {
                pageIndex = parseInt($("#<%= hdPageIndex.ClientID %>").val()) - 1;
                $("#<%= hdPageIndex.ClientID %>").val(pageIndex); // Cập nhật giá trị
                $("#<%= hiddenButtonPaging.ClientID %>").click(); // Gọi sự kiện click trên nút ẩn
                return; // Ngừng hàm sau khi đã thực thi
            }
            if ($(this).attr('id') === 'pageNext') {
                pageIndex = parseInt($("#<%= hdPageIndex.ClientID %>").val()) + 1;
                $("#<%= hdPageIndex.ClientID %>").val(pageIndex); // Cập nhật giá trị
                $("#<%= hiddenButtonPaging.ClientID %>").click(); // Gọi sự kiện click trên nút ẩn
                return; // Ngừng hàm sau khi đã thực thi
            }
            else {
                pageIndex = $(this).data('index');
                $("#<%= hdPageIndex.ClientID %>").val(pageIndex); // Cập nhật giá trị
                $("#<%= hiddenButtonPaging.ClientID %>").click(); // Gọi sự kiện click trên nút ẩn
                return; // Ngừng hàm sau khi đã thực thi
            }
        });

    </script>
</asp:Content>
