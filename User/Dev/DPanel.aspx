<%@ Page Title="" Language="C#" MasterPageFile="~/User/Dev/Developer.Master" AutoEventWireup="true" CodeBehind="DPanel.aspx.cs" Inherits="WebStore.User.Dev.DPanel" %>

<asp:Content ID="DPanelmain" ContentPlaceHolderID="main" runat="server">
    <asp:UpdatePanel runat="server" >
        <ContentTemplate>
            <section class="welcome_area bg-img background-overlay" style="background-image: url(../../img/breadcumb.jpg);">
                <div class="container h-100">
                    <div class="row h-100 align-items-center">
                        <div class="col-12">
                            <div class="hero-content">
                                <h6>Web Store</h6>
                                <h2>Upload Apps</h2>
                                <asp:LinkButton runat="server" PostBackUrl="~/User/Dev/Upload.aspx" CssClass="btn essence-btn">Upload Applications</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
