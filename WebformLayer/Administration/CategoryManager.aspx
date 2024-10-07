<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administration/MasterAdminPage/Admin.Master" CodeBehind="CategoryManager.aspx.cs" Inherits="WebformLayer.Administration.CategoryManager" %>

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
                <button class="btn btn-primary btn-sm btn-flat padding-fa mr-4" id="btnOpenModal" type="button"
                    onclick="openModal()" runat="server">
                    <i class="fa fa-plus"></i>Thêm mới</button>
                <asp:Label ID="lblResult" CssClass="text-info" runat="server" Text=""></asp:Label>
                <asp:Label ID="lbltotal" CssClass=".text-primary float-right" runat="server" Text=""></asp:Label>
            </div>
        </div>

        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:HiddenField ID="hdPageIndex" runat="server" />
                <asp:GridView ID="GridViewCategory" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-striped table-bordered" CellPadding="10" CellSpacing="2"
                    GridLines="None" OnRowCommand="GridViewCategory_RowCommand">

                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" />
                        <asp:BoundField DataField="Name" ItemStyle-Width="20%" HeaderText="Tiêu đề" />
                        <asp:BoundField DataField="Ma" HeaderText="Mã" />
                        <asp:BoundField DataField="Description" ItemStyle-Width="20%" HeaderText="Mô tả" />
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:CheckBox ID="IsAvtive" runat="server"
                                    Checked='<%# Eval("Active") %>' Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Hành Động">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary btn-xs btn-flat"
                                    CommandArgument='<%# Eval("Id") %>' CommandName="editCategory" ToolTip="Sửa">
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
                                    <th>Id</th>
                                    <th>Tiêu đề</th>
                                    <th>Mã</th>
                                    <th>Mô tả</th>
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
                        <h4>Chỉnh sửa danh mục</h4>
                        <asp:Label ID="lblEditError" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
                        <div class="row">
                            <div class="col-lg-12 justify-content-center align-items-center" style="max-height: 500px; overflow-y: auto; overflow-x: hidden;">
                                <asp:TextBox ID="txtEditId" runat="server" CssClass="form-control" placeholder="IdPost" Visible="false"></asp:TextBox>

                                <div class="form-group d-flex align-items-center">

                                    <asp:Label ID="Label3" runat="server" CssClass="pb-2 mr-2" Text="Tiêu đề: " Style="white-space: nowrap; min-width: 80px;"></asp:Label>
                                    <asp:TextBox ID="txtEditName" runat="server" CssClass="form-control form-control-user flex-grow-1" placeholder="Tiêu đề"></asp:TextBox>
                                </div>
                                <div class="form-group d-flex align-items-center">
                                    <asp:Label ID="Label5" runat="server" CssClass="pb-2 mr-2" Text="Mã: " Style="white-space: nowrap; min-width: 80px;"></asp:Label>

                                    <asp:TextBox ID="txtEditMa" runat="server" CssClass="form-control form-control-user" placeholder="Mã"></asp:TextBox>
                                </div>
                                <div class="form-group d-flex align-items-center ">
                                    <asp:Label ID="Label4" runat="server" CssClass="pb-2 mr-2" Text="Mô tả: " Style="white-space: nowrap; min-width: 80px;"></asp:Label>

                                    <asp:TextBox ID="txtEditDescription" runat="server" CssClass="form-control form-control-user" placeholder="Mô tả"  Height="200px" TextMode="MultiLine"></asp:TextBox>
                                </div>


                                <div class="form-group d-flex align-items-center">
                                    <asp:Label ID="Label7" runat="server" CssClass="pb-2 mr-2" Text="Active: " Style="white-space: nowrap; min-width: 80px;"></asp:Label>
                                    <asp:CheckBox ID="chkEditActive" runat="server" CssClass="ckeckboxActive" Width="250px" />
                                </div>
                                <hr>
                               
                                <hr />

                                <asp:Button ID="Button1" runat="server" Text="Sửa" class="btn btn-primary btn-user btn-block" OnClick="btnCategoryEdit_Click" />
                                <asp:Button ID="Button2" runat="server" Text="Hủy" class="btn btn-secondary btn-user btn-block" OnClientClick="closeEdit(); return false;" />
                            </div>
                        </div>
                    </div>

                </div>

            </ContentTemplate>

        </asp:UpdatePanel>


    </main>


    <style>
        .modal-content-editPost {
            background-color: white;
            margin: auto;
            padding: 20px;
            border: 1px solid #888;
            width: 60%;
            max-height: 90%;
        }

        input, select, textarea {
            max-width: none;
        }

        ul.pagination li {
            cursor: pointer;
        }

        /*        ul.pagination li :hover {

        }*/
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
                        <asp:Label ID="Label10" runat="server" CssClass="pb-2 mr-2" Text="Tiêu đề: " Style="white-space: nowrap; min-width: 80px;"></asp:Label>
                        <asp:TextBox ID="txtAddName" runat="server" CssClass="form-control form-control-user flex-grow-1" placeholder="Tiêu đề"></asp:TextBox>
                    </div>
                    <div class="form-group d-flex align-items-center">
                        <asp:Label ID="lbladdma" runat="server" CssClass="pb-2 mr-2" Text="Mã:  " Style="white-space: nowrap; min-width: 80px;"></asp:Label>
                        <asp:TextBox ID="txtAddMa" runat="server" CssClass="form-control form-control-user flex-grow-1" placeholder="Mã"></asp:TextBox>
                    </div>

                    <div class="form-group d-flex align-items-center ">
                        <asp:Label ID="Label11" runat="server" CssClass="pb-2 mr-2" Text="Mô tả: " Style="white-space: nowrap; min-width: 80px;"></asp:Label>

                        <asp:TextBox ID="txtAddDescription" runat="server" CssClass="form-control form-control-user" placeholder="Mô tả" TextMode="MultiLine"></asp:TextBox>
                    </div>
                   

                    <div class="form-group d-flex align-items-center">
                        <asp:Label ID="Label14" runat="server" CssClass="pb-2 mr-2" Text="Active: " Style="white-space: nowrap; min-width: 80px;"></asp:Label>
                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="ckeckboxActive" Width="250px" />
                    </div>
                    <hr />
                    <asp:Button ID="btnUserAdd" runat="server" Text="Thêm Mới" class="btn btn-primary btn-user btn-block" OnClick="btnCategoryAdd_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Hủy" class="btn btn-secondary btn-user btn-block" OnClientClick="closeModal(); return false;" />
                </div>
            </div>
        </div>
    </div>


    <%-- Modal delete comfirm --%>
    <div id="confirmDeleteModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="closeDelete(); return false;">&times;</span>
            <h5>Bạn có chắc chắn muốn bài viết này?</h5>
            <asp:Label ID="Label2" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>

            <asp:Button ID="Button3" runat="server" Text="Xóa" class="btn btn-primary btn-user btn-block" OnClick="btnDeleteCategory_Click" />
            <asp:Button ID="Button4" runat="server" Text="Hủy" class="btn btn-secondary btn-user btn-block" OnClientClick="closeDelete(); return false;" />
        </div>
    </div>
    <asp:HiddenField ID="hdnDeleteId" runat="server" />



    <div class="align-items-center" style="justify-content: center; position: absolute;">
        <nav aria-label="...">
            <ul class="pagination">
                <asp:Literal ID="ltlPaging" runat="server"></asp:Literal>
            </ul>
        </nav>
    </div>
    <asp:Button ID="hiddenButtonPaging" runat="server" Text="Hidden Button" OnClick="HiddenButtonPaging_Click" Style="display: none;" />

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

            document.getElementById('<%= hdnDeleteId.ClientID %>').value = postId; // Đặt giá trị vào HiddenField
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

    </script>
</asp:Content>
