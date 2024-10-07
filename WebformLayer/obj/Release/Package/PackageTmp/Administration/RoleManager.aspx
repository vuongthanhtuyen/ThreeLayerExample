<%@ Page Title="Role Manager" Language="C#" MasterPageFile="~/Administration/MasterAdminPage/Admin.Master" AutoEventWireup="true" CodeBehind="RoleManager.aspx.cs" Inherits="WebformLayer.Administration.UserAdd" %>

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
            </div>
        </div>

        <br />
        <asp:GridView ID="GridViewRole" runat="server" AutoGenerateColumns="false"
            CssClass="table table-striped table-bordered" CellPadding="10" CellSpacing="2"
            GridLines="None" OnRowCommand="GridViewRole_RowCommand">

            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <%--                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkRoleDetail" runat="server"
                            Text='<%# Eval("Name") %>'
                            CommandArgument='<%# Eval("Id") %>'
                            OnClick="btnRoleEditDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="Ma" HeaderText="Ma" />
                <asp:TemplateField HeaderText="Active">
                    <ItemTemplate>
                        <asp:CheckBox ID="Active" runat="server"
                            Checked='<%# Eval("Active") %>' Enabled="False" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Hành Động" ItemStyle-Width="35%">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary btn-xs btn-flat"
                            CommandArgument='<%# Eval("Id") %>' CommandName="editRole" ToolTip="Sửa">
                        <span class="fa fa-eye"></span> Sửa
                        </asp:LinkButton>

                        <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger btn-xs btn-flat"
                            data-id='<%# Eval("Id") %>' ToolTip="Modal"
                            OnClientClick="return openDelete(this);">
                        <span class="fa fa-trash"></span> Xóa
                        </asp:LinkButton>

                        <asp:LinkButton ID="btnUpdateRole" runat="server" CssClass="btn btn-primary btn-xs btn-flat"
                            CommandArgument='<%# Eval("Id") %>' CommandName="UpdateRole" ToolTip="Sửa quyền">
                            <span class="fa fa-eye"></span> Sửa quyền
                        </asp:LinkButton>


                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <table class="table">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Name</th>
                            <th>Ma</th>
                            <th>Active</th>
                            <th>Hành Động</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td colspan="5" class="text-center">Không có dữ liệu để hiển thị.</td>
                        </tr>
                    </tbody>
                </table>
            </EmptyDataTemplate>
        </asp:GridView>
    </main>



    <%-- Edit Role Modal --%>
    <div id="myEditModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="closeEdit()">&times;</span>
            <h4>Chỉnh sửa role</h4>
            <asp:Label ID="lblErrorMessageEdit" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
            <div class="row">
                <div class="col-lg-6 justify-content-center align-items-center">
                    <asp:TextBox ID="txtEditRoleId" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>

                    <div class="form-group">
                        <asp:TextBox ID="txtEditRoleName" runat="server" CssClass="form-control form-control-user" placeholder="Tên role"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtEditRoleMa" runat="server" CssClass="form-control form-control-user" placeholder="Mã"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:CheckBox ID="ckbEditActive" runat="server"
                            Checked='<%# Eval("Active") %>' Text=" Active" />
                    </div>
                    <hr>
                    <asp:Button ID="btnEidtRoleSave" runat="server" Text="Sửa" class="btn btn-primary btn-user btn-block" OnClick="btnEditRoleSave_Click" />
                    <asp:Button ID="BtnEditCancel" runat="server" Text="Hủy" class="btn btn-secondary btn-user btn-block" OnClientClick="closeEdit(); return false;" />
                </div>
            </div>
        </div>
    </div>



    <%-- Modal add new role --%>
    <div id="myModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="closeModal()">&times;</span>
            <h4>Thêm mới Role</h4>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
            <div class="row d-flex justify-content-center align-items-center">
                <div class="form-group">
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control form-control-user" placeholder="Tên role"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtMa" runat="server" CssClass="form-control form-control-user" placeholder="Mã"></asp:TextBox>
                </div>
                <hr>
            </div>
            <asp:Button ID="btnRoleAdd" runat="server" Text="Thêm Mới" class="btn btn-primary btn-user btn-block" OnClick="btnRoleAdd_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Hủy" class="btn btn-secondary btn-user btn-block" OnClientClick="closeModal(); return false;" />
        </div>
    </div>


    <%-- Modal delete comfirm --%>
    <div id="confirmDeleteModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="closeDelete(); return false;">&times;</span>
            <h5>Bạn có chắc chắn muốn xóa vai trò này?</h5>
            <asp:Label ID="Label2" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>

            <asp:Button ID="Button3" runat="server" Text="Xóa" class="btn btn-primary btn-user btn-block" OnClick="btnDeleteRole_Click" />
            <asp:Button ID="Button4" runat="server" Text="Hủy" class="btn btn-secondary btn-user btn-block" OnClientClick="closeDelete(); return false;" />
        </div>
    </div>
    <asp:HiddenField ID="hdnRoleId" runat="server" />

    <%-- Modal Edit detail menu role --%>
    <div id="roleDetailEditModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="closeRoleDetailEditModal()">&times;</span>
            <h4>Chỉnh sửa role</h4>
            <asp:Label ID="lblEditmenuPermissonDetail" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>

            <div class="col">
                <div class="row d-flex justify-content-center align-items-center" style="max-height: 400px; overflow-y: auto; overflow-x: hidden;">
                    <asp:TextBox ID="txtEditRoleIDetailMenu" runat="server" CssClass="form-control" placeholder="IdUser" Visible="False"></asp:TextBox>

                    <asp:GridView ID="GridRoleMenuPermission" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-striped" CellPadding="10" CellSpacing="2"
                        GridLines="None"
                        OnRowDataBound="GridRoleMenuPermission_RowDataBound" DataKeyNames="MenuName">

                        <Columns>

                            <asp:TemplateField HeaderText="Menu">
                                <ItemTemplate>
                                    <asp:Label ID="lblMenuName" runat="server" Text='<%# Eval("MenuName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="PermissionName" HeaderText="Quyền" />
                            <asp:TemplateField HeaderText="Trạng thái">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkIsHaveRole" runat="server"
                                        Checked='<%# Eval("IsRoleHavePermission").ToString()=="1" %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="MenuId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblMenuId" runat="server" Text='<%# Eval("MenuId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PermissionId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblPermissionId" runat="server" Text='<%# Eval("PermissionId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                    <hr>
                </div>
            </div>
            <div clas="col-5">
                <div class="row d-flex justify-content-center align-items-center">
                </div>
            </div>
            <asp:Button ID="btnEditRole" runat="server" Text="Sửa" class="btn btn-primary btn-user btn-block" OnClick="btnRoleEditDetailSave_Click" />
            <asp:Button ID="btnCancelEditRole" runat="server" Text="Hủy" class="btn btn-secondary btn-user btn-block" OnClientClick="closeRoleDetailEditModal(); return false;" />
        </div>
    </div>



    <script>
        function openModal() {
            document.getElementById("myModal").style.display = "block";
            document.getElementById("<%= lblResult.ClientID %>").innerText = "";
            return false;
        }
        function closeModal() {
            document.getElementById("myModal").style.display = "none";
            document.getElementById("<%= lblErrorMessage.ClientID %>").innerText = "";

        }

        function openDelete(linkButton) {
            document.getElementById("confirmDeleteModal").style.display = "block";
            var roleId = linkButton.getAttribute('data-id'); // Lấy giá trị từ data-id laf 

            document.getElementById('<%= hdnRoleId.ClientID %>').value = roleId; // Đặt giá trị vào HiddenField
            document.getElementById("<%= lblResult.ClientID %>").innerText = "";

            return false;
        }

        function closeDelete() {
            document.getElementById('<%= hdnRoleId.ClientID %>').value = "";
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
        function openRoleDetailEditModal(id) {
            document.getElementById("<%= lblResult.ClientID %>").innerText = "";
            document.getElementById("roleDetailEditModal").style.display = "block";
            return false;
        }

        function closeRoleDetailEditModal() {
            document.getElementById("roleDetailEditModal").style.display = "none";

        }

    </script>




</asp:Content>
