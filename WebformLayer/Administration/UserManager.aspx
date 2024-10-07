<%@ Page Title="User List" Language="C#" MasterPageFile="~/Administration/MasterAdminPage/Admin.Master" AutoEventWireup="true" CodeBehind="UserManager.aspx.cs" Inherits="WebformLayer.Administration.UserManager" %>

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
        <asp:GridView ID="GridViewUser" runat="server" AutoGenerateColumns="false"
            CssClass="table table-striped table-bordered" CellPadding="10" CellSpacing="2"
            GridLines="None" OnRowCommand="GridViewUser_RowCommand">

            <Columns>
                <asp:BoundField DataField="FullName" HeaderText="FullName" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" />
                <asp:BoundField DataField="Username" HeaderText="Username" />
                <asp:BoundField DataField="Dob" HeaderText="BirthDay" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />

                <asp:TemplateField HeaderText="Role">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkRole" runat="server"
                            Text='<%# Eval("RoleName") %>'
                            CommandArgument='<%# Eval("Id") %>'
                            OnClick="btnRoleEdit_Click">

                        </asp:LinkButton>

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Hành Động">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDetail" runat="server" CssClass="btn btn-primary btn-xs btn-flat"
                            CommandArgument='<%# Eval("Id") %>' CommandName="detail" ToolTip="Sửa">
                            <span class="fa fa-eye"></span> Sửa
                        </asp:LinkButton>
                        <%--                        <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger btn-xs btn-flat" 
                            CommandArgument='<%# Eval("Id") %>' CommandName="delete_user" ToolTip="Xóa">
                            <span class="fa fa-trash"></span> Delete
                        </asp:LinkButton>--%>
                        <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger btn-xs btn-flat"
                            data-id='<%# Eval("Id") %>' ToolTip="Modal"
                            OnClientClick="return openDelete(this);">
                            <span class="fa fa-trash"></span> Xóa
                        </asp:LinkButton>

<%--                        <asp:LinkButton ID="btnUpdateRole" runat="server" CssClass="btn btn-primary btn-xs btn-flat"
                            CommandArgument='<%# Eval("Id") %>' CommandName="UpdateRole" ToolTip="Sửa quyền">
                            <span class="fa fa-eye"></span> Sửa quyền
                        </asp:LinkButton>--%>



                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </main>



    <%-- Edit User Modal --%>
    <div id="myEditModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="closeEdit()">&times;</span>
            <h4>Trang chỉnh sửa user</h4>
            <asp:Label ID="Label1" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
            <div class="row">
                <div class="col-lg-6 justify-content-center align-items-center">
                    <asp:TextBox ID="txtIdUser" runat="server" CssClass="form-control" placeholder="IdUser" Visible="false"></asp:TextBox>

                    <div class="form-group">
                        <asp:TextBox ID="txtEditFullname" runat="server" CssClass="form-control form-control-user" placeholder="Họ và tên"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtEditEmail" runat="server" TextMode="Email" CssClass="form-control form-control-user" placeholder="Email"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="TxtEditPhone" runat="server" CssClass="form-control form-control-user" placeholder="Số điện thoại"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtEditDob" runat="server" TextMode="Date" CssClass="form-control form-control-user" placeholder="Ngày sinh"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtEditUsername" runat="server" CssClass="form-control form-control-user" placeholder="Username"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtEditPassword" runat="server" TextMode="Password" CssClass="form-control form-control-user" placeholder="Password"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:CheckBox ID="chkEditActive" runat="server" CssClass="form-control ckeckboxActive" Text="Hoạt động" />
                    </div>
                    <hr>
                    <asp:Button ID="Button1" runat="server" Text="Sửa" class="btn btn-primary btn-user btn-block" OnClick="btnUserEdit_Click" />
                    <asp:Button ID="Button2" runat="server" Text="Hủy" class="btn btn-secondary btn-user btn-block" OnClientClick="closeEdit(); return false;" />
                </div>
            </div>


        </div>
    </div>


    <%-- Edit Role Modal --%>
    <div id="roleEditModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="closeRoleEditModal()">&times;</span>
            <h4>Chỉnh sửa role</h4>
            <div class="col">
                <div class="row d-flex justify-content-center align-items-center">
                    <asp:TextBox ID="txtEditRoleIdUser" runat="server" CssClass="form-control" placeholder="IdUser" Visible="false"></asp:TextBox>

                    <asp:GridView ID="GridViewRole" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-striped" CellPadding="10" CellSpacing="2"
                        GridLines="None">

                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="IdRole" />
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Ma" HeaderText="Ma" />
                            <asp:TemplateField HeaderText="IsHaveRole">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkIsHaveRole" runat="server"
                                        Checked='<%# Eval("IsHaveRole").ToString() == "1" %>' />
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
            <asp:Button ID="btnEditRole" runat="server" Text="Sửa" class="btn btn-primary btn-user btn-block" OnClick="btnRoleEditSave_Click" />
            <asp:Button ID="btnCancelEditRole" runat="server" Text="Hủy" class="btn btn-secondary btn-user btn-block" OnClientClick="closeRoleEditModal(); return false;" />
        </div>
    </div>


    <%-- Modal add new user --%>
    <div id="myModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="closeModal()">&times;</span>
            <h4>Trang thêm mới một user</h4>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>
            <div class="row d-flex justify-content-center align-items-center">
                <div class="form-group">
                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control form-control-user" placeholder="Họ và tên"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control form-control-user" placeholder="Email"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control form-control-user" placeholder="Số điện thoại"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtDob" runat="server" TextMode="Date" CssClass="form-control form-control-user" placeholder="Ngày sinh"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control form-control-user" placeholder="Username"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control form-control-user" placeholder="Password"></asp:TextBox>
                </div>
                <hr>
            </div>
            <asp:Button ID="btnUserAdd" runat="server" Text="Thêm Mới" class="btn btn-primary btn-user btn-block" OnClick="btnUserAdd_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Hủy" class="btn btn-secondary btn-user btn-block" OnClientClick="closeModal(); return false;" />
        </div>
    </div>


    <%-- Modal delete comfirm --%>
    <div id="confirmDeleteModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="closeDelete(); return false;">&times;</span>
            <h5>Bạn có chắc chắn muốn xóa người dùng này?</h5>
            <asp:Label ID="Label2" runat="server" CssClass="text-danger pb-2" Text=""></asp:Label>

            <asp:Button ID="Button3" runat="server" Text="Xóa" class="btn btn-primary btn-user btn-block" OnClick="btnDeleteUser_Click" />
            <asp:Button ID="Button4" runat="server" Text="Hủy" class="btn btn-secondary btn-user btn-block" OnClientClick="closeDelete(); return false;" />
        </div>
    </div>
    <asp:HiddenField ID="hdnUserId" runat="server" />

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
            var userId = linkButton.getAttribute('data-id'); // Lấy giá trị từ data-id laf 

            document.getElementById('<%= hdnUserId.ClientID %>').value = userId; // Đặt giá trị vào HiddenField
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
        function openRoleEditModal(id) {
            document.getElementById("<%= lblResult.ClientID %>").innerText = "";
            document.getElementById("roleEditModal").style.display = "block";
            return false;
        }

        function closeRoleEditModal() {
            document.getElementById("roleEditModal").style.display = "none";

        }
    </script>

</asp:Content>
