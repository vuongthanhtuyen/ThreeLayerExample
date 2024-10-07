<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="AuthorDetail.aspx.cs" Inherits="WebformLayer.AuthorDetail" %>


<asp:Content ID="contentAsset" ContentPlaceHolderID="contentAsset" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <style>
        .gradient-custom-2 {
            /* fallback for old browsers */
            background: #fbc2eb;
            /* Chrome 10-25, Safari 5.1-6 */
            background: -webkit-linear-gradient(to right, rgba(251, 194, 235, 1), rgba(166, 193, 238, 1));
            /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
            background: linear-gradient(to right, rgba(251, 194, 235, 1), rgba(166, 193, 238, 1))
        }


        .card {
            transition: transform 0.3s ease;
        }

        .card:hover {
            transform: translateY(-10px);
        }

        .remove-link {
            text-decoration: none;
            color: black;
        }

        .remove-link:hover {
            color: black;
        }
    </style>

    <section class="h-100 gradient-custom-2">
        <div class="container py-5 h-100">
            <div class="row d-flex justify-content-center">
                <div class="col col-lg-9 col-xl-8">
                    <div class="card">
                        <div class="rounded-top text-white d-flex flex-row" style="background-color: #000; height: 200px;">
                            <div class="ms-4 mt-5 d-flex flex-column" style="width: 150px;">

                                <asp:Image ID="userImage" runat="server" alt="Generic placeholder image" 
                                    cssClass="img-fluid img-thumbnail mt-4 mb-2" style="width: 150px; z-index: 1" />
                            
                            </div>
                            <div class="ms-3" style="margin-top: 130px;">
                                <h5>
                                    <asp:Label ID="lblFullName" CssClass="text-light" runat="server" Text="Vương Thanh Tuyền"></asp:Label></h5>
                                <p>
                                    <asp:Label ID="lblEmail" runat="server" Text="vuongthanhtuyen@13579@gmail.com"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="p-4 text-black bg-body-tertiary">
                            <div class="d-flex justify-content-end text-center py-1 text-body">
                                <div>
                                    <asp:Label ID="lblTongLuotXem" CssClass="mb-1 h5" runat="server" Text="23"></asp:Label>
                                    <p class="small text-muted mb-0">Lượt xem</p>
                                </div>
                                <div class="px-3">
                                    <asp:Label ID="lblTongBaiViet" CssClass="mb-1 h5" runat="server" Text="200"></asp:Label>
                                    <p class="small text-muted mb-0">Số bài viết</p>
                                </div>
                            </div>
                        </div>
                        <div class="card-body p-4 text-black">
                            <div class="mb-5  text-body">
                                <p class="lead fw-normal mb-0">Về tác giả</p>
                                <div class="p-4 bg-body-tertiary">
                                    <asp:Label ID="lblAdress" CssClass="font-italic mb-1" runat="server" Text="Địa chỉ: 79 Mai Thị Dõng"></asp:Label> <br />
                                    <asp:Label ID="lblDecription" CssClass="font-italic mb-1" runat="server" Text="Mô tả"></asp:Label>
                                </div>
                            </div>
                     
                            <div class="d-flex justify-content-between align-items-center mb-4 text-body">
                                <p class="lead fw-normal mb-0">Bài viết của tác giả</p>
                                <p class="mb-0">
                                    <asp:Button ID="ShowAll" CssClass="text-muted" runat="server" Text="Xem thêm" OnClick="ShowAll_Click" />
                            </div>
                            <div class="row g-2">
                                <asp:Literal ID="lbtPost" runat="server"></asp:Literal>
                            </div>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
