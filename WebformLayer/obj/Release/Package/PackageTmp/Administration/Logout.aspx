<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administration/MasterAdminPage/Admin.Master" CodeBehind="Logout.aspx.cs" Inherits="WebformLayer.Administration.Logout" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="Content1" runat="server">
    <style>
        .modal {
            z-index: 1050;
        }
        .modal-backdrop {
            z-index: 1040;
        }
        .modal-content{
            width:100%;
        }

        input, select, textarea {
            max-width: none;
        }

    </style>
    <!-- Logout Modal-->
    <div class="modal fade show" id="logoutModal" role="dialog" aria-labelledby="exampleModalLabel"
        aria-hidden="true">
        
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Đăng xuất Không?</h5>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close" onclick="closeLogout()">
                        <span class="close">&times;</span>
                    </button>
                </div>
                <div class="modal-body">Đăng xuất khỏi website sẽ không thể thao tác vào trang quản trị được nữa!</div>
                <div class="modal-footer">
                     <asp:Button ID="btnCancelEditRole" runat="server" Text="Hủy" CssClass="btn btn-secondary btn-user btn-block" width="80px" OnClientClick="closeLogout(); return false;" />
                    <asp:Button ID="btnLogout" CssClass="btn btn-primary" runat="server" Text="Đăng xuất" OnClick="btnLogOut_Click" />

                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">


        function LogOutModal() {
            document.getElementById("logoutModal").style.display = "block";
            return false;
        }

        function closeLogout() {
            document.getElementById("logoutModal").style.display = "none";

        }

    </script>
</asp:Content>
