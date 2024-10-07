<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Administration/MasterAdminPage/Admin.Master" CodeBehind="UserProfileManager.aspx.cs" Inherits="WebformLayer.Administration.UserProfileManager" %>


<asp:Content ID="AddneBoostrap" ContentPlaceHolderID="AddneBoostrap" runat="server">

    <link href="../assets/css/UserProfile.css" rel="stylesheet" />
</asp:Content>


<%--<asp:Content ID="contentAvatar" ContentPlaceHolderID="contentAvatar" runat="server">
    <asp:Image ID="imgAvatarSmall" runat="server" CssClass="img-profile rounded-circle" />
</asp:Content>--%>


<asp:Content ID="BodyContent" ContentPlaceHolderID="Content1" runat="server">

    <style>
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



    <div class="container rounded bg-white mb-1">
        <div class="row">
            <div class="col-md-3 border-right">
                <div class="d-flex flex-column align-items-center text-center p-3 py-5">
                    <div class="custom-container">
                        <asp:FileUpload ID="fileUploadAvatar" runat="server" onchange="previewImage(this)" Style="display: none;" />

                        <asp:Image
                            onclick="triggerUpload()"
                            CssClass="rounded-circle mt-5 avatar custom-image"
                            ID="avatarImage"
                            Style="background-color: antiquewhite; height: 200px; width: 200px; border: 3px solid #0af; border-radius: 50%; transition: background ease-out 200ms; object-fit: cover; display: block; margin: 0 auto;"
                            runat="server"
                            ImageUrl="https://st3.depositphotos.com/15648834/17930/v/600/depositphotos_179308454-stock-illustration-unknown-person-silhouette-glasses-profile.jpg" />

                        <div class="custom-emoji" onclick="triggerUpload()" style="cursor: pointer;">
                            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-pen" viewBox="0 0 16 16">
                                <path d="m13.498.795.149-.149a1.207 1.207 0 1 1 1.707 1.708l-.149.148a1.5 1.5 0 0 1-.059 2.059L4.854 14.854a.5.5 0 0 1-.233.131l-4 1a.5.5 0 0 1-.606-.606l1-4a.5.5 0 0 1 .131-.232l9.642-9.642a.5.5 0 0 0-.642.056L6.854 4.854a.5.5 0 1 1-.708-.708L9.44.854A1.5 1.5 0 0 1 11.5.796a1.5 1.5 0 0 1 1.998-.001m-.644.766a.5.5 0 0 0-.707 0L1.95 11.756l-.764 3.057 3.057-.764L14.44 3.854a.5.5 0 0 0 0-.708z" />
                            </svg>
                        </div>
                    </div>

                    <asp:Label CssClass="font-weight-bold mt-2" ID="lblFullName" runat="server" Text="Label"></asp:Label>
                    <asp:Label CssClass="text-black-50" ID="lblEmail" runat="server" Text="Label"></asp:Label>
                    <asp:Label CssClass="text-primary" ID="lblMessage" runat="server" Text=""></asp:Label>
                    <br />
                    <span></span>
                </div>
            </div>
            <div class="col-md-5 border-right">
                <div class="p-3 py-5">
                    <div class="d-flex justify-content-between align-items-center mb-1">
                        <h4 class="text-right">Trang cá nhân</h4>
                    </div>

                    <div class="row mt-3">

                        <div class="col-md-12">
                            <asp:HiddenField ID="hiddeniId" runat="server" />
                            <label class="labels">Họ và tên</label><asp:TextBox ID="txtFullname" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <label class="labels">Email</label><asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <label class="labels">Số điện thoại</label><asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <label class="labels">Địa chỉ</label><asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <label class="labels">Ngày sinh</label><asp:TextBox ID="txtNgaySinh" runat="server" CssClass="form-control" TextMode="DateTime"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <label class="labels">Username</label><asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div> 
                        <div class="col-md-12">
                            <label class="labels">Mô tả</label><asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100px"></asp:TextBox>
                        </div>

                    </div>

                </div>
            </div>
            <div class="col-md-3">
                <div class="p-3 py-5">
                    <%--<div class="d-flex justify-content-between align-items-center experience"><span>Chỉnh sửa trang cá nhân</span><span class="border px-3 p-1 add-experience"><i class="fa fa-plus"></i>&nbsp;Chỉnh sửa</span></div>--%>
                    <div class="d-flex justify-content-between align-items-center experience">
                        <div class="text-center">
                            <asp:Button ID="btnLuuLai" runat="server" Text="Lưu lại" CssClass="btn btn-primary profile-button" OnClick="btnLuuLai_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Thoát" CssClass="btn btn-primary profile-button" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>

                <br>
            </div>
        </div>
    </div>



    <script type="text/javascript">
        function triggerUpload() {
            document.getElementById('<%= fileUploadAvatar.ClientID %>').click();
        }

        function previewImage(fileUpload) {
            const file = fileUpload.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    document.getElementById('<%= avatarImage.ClientID %>').src = e.target.result;
                }
                reader.readAsDataURL(file);
            }
        }
</script>

</asp:Content>
