<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DetailPost.aspx.cs" Inherits="WebformLayer.DetailPost" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>

        .categories ul {
            display: none; /* Ẩn nội dung con */
            list-style-type: none;
            padding-left: 15px;
            margin: 0;
        }

        .active > ul {
            display: block; /* Hiện nội dung con khi active */
        }
    </style>
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet">
    <div class="container pb50 mt-5">
        <div class="row">
            <div class="col-md-9 mb40">
                <article>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Image ID="postImage" runat="server" CssClass="img-fluid mb30"
                                Style="background-color: antiquewhite; height: auto; width: 90%; border: 3px solid #0af; transition: background ease-out 200ms; object-fit: cover; display: block;" />
                            <%--                    <img src="https://bootdey.com/img/Content/bg1.jpg" alt="" class="img-fluid mb30">--%>
                            <div class="post-content">
                                
                               <h3>

                                <asp:Literal ID="postTitle" runat="server"></asp:Literal>
                               </h3>
                                

                                <ul class="post-meta list-inline">
                                    <li class="list-inline-item">
                                        <i class="fa fa-user-circle-o"></i>
                                        <asp:HyperLink ID="postAuthor" runat="server">HyperLink</asp:HyperLink>
                                    </li>
                                    <li class="list-inline-item">
                                        <i class="fa fa-calendar-o"></i>
                                        <asp:HyperLink NavigateUrl="#" ID="postDatetimeCreate" runat="server">HyperLink</asp:HyperLink>
                                    </li>
                                    <li class="list-inline-item">
                                        <i class="fa fa-eye"></i>
                                        <asp:HyperLink NavigateUrl="#" ID="postView" runat="server">HyperLink</asp:HyperLink>
                                    </li>
                                </ul>
                                <h5>
                                    <asp:Literal ID="postDescription" runat="server">
                                        A smart template that works 24/7 for your company
                                    </asp:Literal></h5>
                                <p>
                                    <asp:Literal ID="postContent" runat="server">
                                    </asp:Literal>
                                </p>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <ul class="list-inline share-buttons">
                        <li class="list-inline-item">Share Post:</li>
                        <li class="list-inline-item">
                            <a href="#" class="social-icon-sm si-dark si-colored-facebook si-gray-round">
                                <i class="fa fa-facebook"></i>
                                <i class="fa fa-facebook"></i>
                            </a>
                        </li>
                        <li class="list-inline-item">
                            <a href="#" class="social-icon-sm si-dark si-colored-twitter si-gray-round">
                                <i class="fa fa-twitter"></i>
                                <i class="fa fa-twitter"></i>
                            </a>
                        </li>
                        <li class="list-inline-item">
                            <a href="#" class="social-icon-sm si-dark si-colored-linkedin si-gray-round">
                                <i class="fa fa-linkedin"></i>
                                <i class="fa fa-linkedin"></i>
                            </a>
                        </li>
                    </ul>
                    <hr class="mb40">
                    <h4 class="mb40 text-uppercase font500">About Author</h4>
                    <div class="media mb40">
                        <i class="d-flex mr-3 fa fa-user-circle fa-5x text-primary"></i>
                        <div class="media-body">
                            <h5 class="mt-0 font700">Jane Doe</h5>
                            Cras sit amet nibh libero, in gravida nulla. Nulla vel metus scelerisque ante sollicitudin. Cras purus odio, vestibulum in vulputate at, tempus viverra turpis. Fusce condimentum nunc ac nisi vulputate fringilla. Donec lacinia congue felis in faucibus.
                        </div>
                    </div>
                    <hr class="mb40">
                 
                </article>
                <!-- post article-->

            </div>
            <div class="col-md-3 mb40">
                <div class="mb40">
                    <div class="input-group">
                        <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" placeholder="Tìm kiếm..."  aria-describedby="basic-addon2"></asp:TextBox>
                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" CssClass="input-group-addon" runat="server" Text="Tìm" />
                            
                    </div>
                </div>
                <!--/Search-->
                <div runat="server" id="divRelatedArticles">
                    <h4 class="sidebar-title">Bài viết theo từ khóa tìm kiếm</h4>
                    <ul class="list-unstyled">
                    <asp:Literal ID="SearchResult" runat="server">
                    </asp:Literal>                    

                    </ul>
                </div>
                <!--/col-->
                <div class="mb40">
                    <h4 class="sidebar-title">Tất cả danh mục</h4>
                    <ul class="list-unstyled categories">
                        <asp:Literal ID="categoryList" runat="server"></asp:Literal>
                    </ul>
                </div>
                <!--/col-->
                <div>
                    <h4 class="sidebar-title">Bài viết liên quan</h4>
                    <ul class="list-unstyled">

                        <asp:Literal ID="postListShow" runat="server"></asp:Literal>

                    </ul>
                </div>
            </div>
        </div>

        <script>
            const categoryItems = document.querySelectorAll('.categories > li');

            categoryItems.forEach(item => {
                item.addEventListener('click', function (event) {
                    // Ngăn chặn sự kiện click trên liên kết
                    event.stopPropagation();

                    // Đóng/mở nội dung con
                    this.classList.toggle('active');
                });
            });
    </script>
</asp:Content>
