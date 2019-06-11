<%@ Page Title="" Language="C#" MasterPageFile="~/User/Cus/Customer.Master" AutoEventWireup="true" CodeBehind="CPanel.aspx.cs" Inherits="WebStore.User.Cus.WebForm1" %>

<asp:Content ID="CPanelmain" ContentPlaceHolderID="main" runat="server">
    <asp:UpdatePanel runat="server" >
        <ContentTemplate>
            <section class="welcome_area bg-img background-overlay" style="background-image: url(../../img/breadcumb.jpg);">
                <div class="container h-100">
                    <div class="row h-100 align-items-center">
                        <div class="col-12">
                            <div class="hero-content">
                                <h6>Web Store</h6>
                                <h2>View all Items</h2>
                                <asp:LinkButton runat="server" PostBackUrl="~/User/Cus/Download.aspx" CssClass="btn essence-btn">View All Applications</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
