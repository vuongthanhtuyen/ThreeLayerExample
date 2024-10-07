<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="WebformLayer.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        input, select, textarea {
            max-width: none;
        }

        .btn-check:focus + .btn, .btn:focus {
            outline: 0;
            box-shadow: 0 0 0 .25rem rgba(6,187,204,0.25)
        }

        .btn:disabled, .btn.disabled, fieldset:disabled .btn {
            pointer-events: none;
            opacity: .65
        }

        .btn-primary {
            color: #000;
            background-color: #06BBCC;
            border-color: #06BBCC
        }

            .btn-primary:hover {
                color: #000;
                background-color: #2bc5d4;
                border-color: #1fc2d1
            }

            .btn-check:focus + .btn-primary, .btn-primary:focus {
                color: #000;
                background-color: #2bc5d4;
                border-color: #1fc2d1;
                box-shadow: 0 0 0 .25rem rgba(5,159,173,0.5)
            }

            .btn-check:checked + .btn-primary, .btn-check:active + .btn-primary, .btn-primary:active, .btn-primary.active, .show > .btn-primary.dropdown-toggle {
                color: #000;
                background-color: #38c9d6;
                border-color: #1fc2d1
            }

                .btn-check:checked + .btn-primary:focus, .btn-check:active + .btn-primary:focus, .btn-primary:active:focus, .btn-primary.active:focus, .show > .btn-primary.dropdown-toggle:focus {
                    box-shadow: 0 0 0 .25rem rgba(5,159,173,0.5)
                }

            .btn-primary:disabled, .btn-primary.disabled {
                color: #000;
                background-color: #06BBCC;
                border-color: #06BBCC
            }
            .bg-primary {
    background-color: #06BBCC !important;
}
            .text-primary {
    color: #06BBCC !important;
}
    </style>
    <!-- Contact Start -->
    <div class="container-xxl py-5">
        <div class="container">
            <div class="text-center wow fadeInUp" data-wow-delay="0.1s">
                <h6 class="section-title bg-white text-center text-primary px-3">Liên hệ với chúng tôi</h6>
                <h1 class="mb-5">Hỗ trợ 24 / 7</h1>
            </div>
            <div class="row g-4">
                <div class="col-lg-4 col-md-6 wow fadeInUp" data-wow-delay="0.1s">
                    <h5>Thông tin thêm</h5>
                    <p class="mb-4">Dev Example là website giới thiệu về các bài báo tài chính. Chúng tôi là đội ngũ chuyên nghiệp, được đào tạo để phục vụ đưa để những thông tin chất lượng và kịp thời, giúp nhà đầu tư nắm được thông tin và ra quyết định đúng đắng nhất.</p>
                    <div class="d-flex align-items-center mb-3">
                        <div class="d-flex align-items-center justify-content-center flex-shrink-0 bg-primary" style="width: 50px; height: 50px;">
                            <i class="fa fa-map-marker-alt text-white"></i>
                        </div>
                        <div class="ms-3">
                            <h5 class="text-primary">Địa chỉ</h5>
                            <p class="mb-0">79 Mai Thị Dõng</p>
                        </div>
                    </div>
                    <div class="d-flex align-items-center mb-3">
                        <div class="d-flex align-items-center justify-content-center flex-shrink-0 bg-primary" style="width: 50px; height: 50px;">
                            <i class="fa fa-phone-alt text-white"></i>
                        </div>
                        <div class="ms-3">
                            <h5 class="text-primary">Số điện thoại</h5>
                            <p class="mb-0">0327604371</p>
                        </div>
                    </div>
                    <div class="d-flex align-items-center">
                        <div class="d-flex align-items-center justify-content-center flex-shrink-0 bg-primary" style="width: 50px; height: 50px;">
                            <i class="fa fa-envelope-open text-white"></i>
                        </div>
                        <div class="ms-3">
                            <h5 class="text-primary">Email</h5>
                            <p class="mb-0">vuongthanhtuyen.work@gmail.com</p>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6 wow fadeInUp" data-wow-delay="0.3s">
                    <iframe class="position-relative rounded w-100 h-100"
                        src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1159.2094809074185!2d109.17678249726191!3d12.24148164914815!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3170678749018e2f%3A0x4b0e1e18074eb956!2zQ8O0bmcgdHkgY-G7lSBwaOG6p24gU3dlZXRTb2Z0!5e0!3m2!1svi!2s!4v1728291430667!5m2!1svi!2s"
                        frameborder="0" style="min-height: 300px; border: 0;" allowfullscreen="" aria-hidden="false"
                        tabindex="0"></iframe>
                </div>
                <div class="col-lg-4 col-md-12 wow fadeInUp" data-wow-delay="0.5s">
                    <form>
                        <div class="row g-3">
                            <div class="col-md-6">
                                <div class="form-floating">
                                    <input type="text" class="form-control" id="name" placeholder="Your Name">
                                    <label for="name">Your Name</label>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-floating">
                                    <input type="email" class="form-control" id="email" placeholder="Your Email">
                                    <label for="email">Your Email</label>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-floating">
                                    <input type="text" class="form-control" id="subject" placeholder="Subject">
                                    <label for="subject">Subject</label>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-floating">
                                    <textarea class="form-control" placeholder="Leave a message here" id="message" style="height: 150px"></textarea>
                                    <label for="message">Message</label>
                                </div>
                            </div>
                            <div class="col-12">
                                <button class="btn btn-primary w-100 py-3" type="submit">Send Message</button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <!-- Contact End -->


</asp:Content>
