<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CategoryPulish.aspx.cs" Inherits="WebformLayer.CategoryPulish" %>

<%@ Register Src="~/UserControlPublish/PostList.ascx" TagPrefix="uc1" TagName="PostList" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

            <div class="container-xxl py-5">
            <div class="container">
                <h4 class="mb-3">
                    <asp:Literal ID="CategoryName" runat="server"></asp:Literal>
                </h4>
                <div class="row g-4">
                    <uc1:PostList runat="server" ID="PostListUserControl" />
                </div>
            </div>
        </div>





</asp:Content>
