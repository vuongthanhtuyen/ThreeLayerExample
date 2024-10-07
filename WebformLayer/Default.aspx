<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebformLayer._Default" %>




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
                <div class="row g-4">

                    <asp:Literal ID="ltlpostList" runat="server">

                    </asp:Literal>

                    

                </div>
            </div>
        </div>
        <!-- Service End -->


        <!-- About End -->

    </main>

</asp:Content>
