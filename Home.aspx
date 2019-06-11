<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebStore.Home" %>

<asp:Content ID="Home" ContentPlaceHolderID="main" runat="server">
    <asp:UpdatePanel runat="server" >
        <ContentTemplate>            
            <section class="welcome_area bg-img background-overlay form-control" style="background-image: url(img/breadcumb.jpg);">
                <div class="container h-100">
                    <div class="row h-100 align-items-center">
                        <div class="col-12">
                            <div class="hero-content">
                                <h6>Web Store</h6>
                                <h2>View Applications</h2>
                                <asp:LinkButton runat="server" PostBackUrl="~/ViewAll.aspx" CssClass="btn essence-btn">View All Applications</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <div class="top_catagory_area section-padding-80 clearfix">
                <div class="container">
                    <div class="row justify-content-center">
                        <div class="col-12 col-sm-6 col-md-3">
                            <div class="single_catagory_area d-flex align-items-center justify-content-center bg-img form-control" style="background-image: url(img/mswindows.jpg);">
                                <div class="catagory-content">
                                    <asp:LinkButton runat="server" OnClick="Unnamed_Click" CustomParameter = "Windows">Windows</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 col-sm-6 col-md-3">
                            <div class="single_catagory_area d-flex align-items-center justify-content-center bg-img form-control" style="background-image: url(img/Android.png);">
                                <div class="catagory-content">
                                    <asp:LinkButton runat="server" OnClick="Unnamed_Click" CustomParameter = "Android">Android</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 col-sm-6 col-md-3">
                            <div class="single_catagory_area d-flex align-items-center justify-content-center bg-img form-control" style="background-image: url(img/games.png);">
                                <div class="catagory-content">
                                    <asp:LinkButton runat="server" OnClick="Unnamed_Click" CustomParameter = "Games">Games</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 col-sm-6 col-md-3">
                            <div class="single_catagory_area d-flex align-items-center justify-content-center bg-img form-control" style="background-image: url(img/Linux.jpg);">
                                <div class="catagory-content">
                                    <asp:LinkButton runat="server" OnClick="Unnamed_Click" CustomParameter = "Linux">Linux</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>