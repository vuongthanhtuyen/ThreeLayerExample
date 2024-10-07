﻿<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Author.aspx.cs" Inherits="WebformLayer.Author" %>

<%@ Register Src="~/UserControlPublish/PagingUser.ascx" TagPrefix="uc1" TagName="PagingUser" %>

<asp:Content ID="contentAsset" ContentPlaceHolderID="contentAsset" runat="server">
    <link href="assets/css/Author.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <!--Profile Card 5-->
            <asp:Literal ID="ltlProfile" runat="server"></asp:Literal>
        </div>
    </div>


 <asp:HiddenField ID="hdPageIndex" runat="server" />
<div class="d-flex justify-content-center align-items-center" style="width: 100%; position: relative;">
    <nav aria-label="...">
        <ul class="pagination">
            <uc1:PagingUser runat="server" ID="PagingUserControl" />
        </ul>
    </nav>
</div>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
    <script src="https://code.jquery.com/jquery-3.7.1.slim.min.js" integrity="sha256-kmHvs0B+OpCW5GVHUNjv9rOmY0IvSIRcf7zGUDTDQM8=" crossorigin="anonymous"></script>
</asp:Content>
