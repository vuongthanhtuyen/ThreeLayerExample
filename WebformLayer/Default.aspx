<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebformLayer._Default" %>

<%@ Register Src="~/UserControlPublish/PostList.ascx" TagPrefix="uc1" TagName="PostListUserControl" %>
<%@ Register Src="~/UserControlPublish/PagingUser.ascx" TagPrefix="uc1" TagName="PagingUser" %>






<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .post-textwrap {
            display: -webkit-box;
            -webkit-line-clamp: 3;
            -webkit-box-orient: vertical;
            overflow: hidden;
        }
    </style>
    <main>

        <!-- Carousel Start -->
        <div class="container-fluid p-0 mb-5">
            <div class="owl-carousel header-carousel position-relative">
                <div class="owl-carousel-item position-relative">
                    <img class="img-fluid" src="Administration/Upload/1.jpg" alt=""  style="object-fit:cover; height:80vh;">
                    <div class="position-absolute top-0 start-0 w-100 h-100 d-flex align-items-center" style="background: rgba(24, 29, 56, .7);">
                        <div class="container">
                            <div class="row justify-content-start">
                                <div class="col-sm-10 col-lg-8">
                                    <h5 class="text-primary text-uppercase mb-3 animated slideInDown">Bài viết hay nhất</h5>
                                    <h1 class="display-3 text-white animated slideInDown">Tin tức thị trường chứng khoán 2024</h1>
                                    <p class="fs-5 text-white mb-4 pb-2">Thị trường chứng khoán và cơ hội thành công cho những con người có kiến thức, kiên trì</p>
                                    <a href="DetailPost?id=22" class="btn btn-primary py-md-3 px-md-5 me-3 animated slideInLeft">Xem thêm</a>
                                    <a href="Administration/Login" class="btn btn-light py-md-3 px-md-5 animated slideInRight">Đăng nhập</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="owl-carousel-item position-relative">
                    <img class="img-fluid" src="Administration/Upload/10.jpg" alt="" style="object-fit:cover; height:80vh;">
                    <div class="position-absolute top-0 start-0 w-100 h-100 d-flex align-items-center" style="background: rgba(24, 29, 56, .7);">
                        <div class="container">
                            <div class="row justify-content-start">
                                <div class="col-sm-10 col-lg-8">
                                    <h5 class="text-primary text-uppercase mb-3 animated slideInDown">Bài viết hay nhất</h5>
                                    <h1 class="display-3 text-white animated slideInDown">Xu Hướng Đầu Tư Công Nghệ</h1>
                                    <p class="fs-5 text-white mb-4 pb-2">Những xu hướng công nghệ ảnh hưởng đến kinh tế.</p>
                                    <a href="DetailPost?id=5" class="btn btn-primary py-md-3 px-md-5 me-3 animated slideInLeft">Xem thêm</a>
                                    <a href="Administration/Login" class="btn btn-light py-md-3 px-md-5 animated slideInRight">Đăng nhập</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Carousel End -->

        <!-- Service Start -->
<div class="container-xxl py-5">
    <div class="container">
        <div class="row g-4 d-flex justify-content-center align-items-center">
            <%--<asp:Literal ID="ltlpostList" runat="server">
            </asp:Literal>--%>


                <uc1:PostListUserControl runat="server" ID="PostListUserControl" />


        </div>
    </div>
</div>


                <asp:HiddenField ID="hdPageIndex" runat="server" />
<div class="d-flex justify-content-center align-items-center" style="width: 100%; position: relative;">
    <nav aria-label="...">
        <ul class="pagination">
            <uc1:PagingUser runat="server" id="PagingUserControl" />
        </ul>
    </nav>
</div>

    <asp:Button ID="hiddenButtonPaging" runat="server" Text="Hidden Button" OnClick="HiddenButton_Click" Style="display: none;" />
    </main>
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
