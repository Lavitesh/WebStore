<%@ Page Title="" Language="C#" MasterPageFile="~/User/Dev/Developer.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="WebStore.User.Dev.Upload" %>

<asp:Content ID="Uploadhead" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Uploadmain" ContentPlaceHolderID="main" runat="server">    
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="breadcumb_area bg-img" style="background-image: url(../../img/breadcumb.jpg);">
                <div class="container h-100">
                    <div class="row h-100 align-items-center">
                        <div class="col-12">
                            <div class="page-title text-center">
                                <h2>Upload</h2>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="checkout_area section-padding-80">
                <div class="container form-control">
                    <div class="row">
                        <div class="col-12 col-md-6">
                            <div class="checkout_details_area mt-50 clearfix">

                                <div class="cart-page-heading mb-30">
                                    <h5>Application Info</h5>
                                </div>

                                <div class="row">
                                    <div class="col-12 mb-3">
                                        <b>Enter Name of the Application:</b>
                                        <asp:TextBox ID="AppName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAppName" ControlToValidate="AppName"
                                            ErrorMessage="Enter Application Name." runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                    
                                    <div class="col-12 mb-3">
                                        <b>Price:</b>
                                        <asp:TextBox ID="Price" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPrice" ControlToValidate="Price"
                                            ErrorMessage="Set Price(if free then enter 0)." runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-12 mb-3">
                                        <b>Description:</b>
                                        <asp:TextBox ID="Description" runat="server" TextMode="MultiLine" Rows="5" Columns="50" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDescription" ControlToValidate="Description"
                                            ErrorMessage="Enter Description." runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 col-md-6 col-lg-5 ml-lg-auto">
                            <div class="order-details-confirmation mt-50">
                                <div class="cart-page-heading">
                                    <h5>Upload Files</h5>
                                    <div>
                                        <b>Category:</b>
                                        <asp:ListBox ID="Category" runat="server" SelectionMode="Multiple" DataSourceID="CategoryDataSource" DataTextField="CatName" DataValueField="CatID" CssClass="close form-control list-group-item-action"></asp:ListBox>
                                        <asp:SqlDataSource ID="CategoryDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [CategoryMaster]"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="rfvCategory" ControlToValidate="Category"
                                            ErrorMessage="Select atleast one category." runat="server"></asp:RequiredFieldValidator>
                                        <br />
                                        <b>Select File:</b>
                                        <asp:FileUpload ID="FControl" runat="server" CssClass="form-control"/>
                                        <asp:RequiredFieldValidator ID="rfvFControl" ControlToValidate="FControl"
                                            ErrorMessage="Select Application File." runat="server"></asp:RequiredFieldValidator>
                                        <br />
                                        <b>Icon:</b>
                                        <asp:FileUpload ID="IControl" runat="server" CssClass="form-control"/>
                                        <asp:RequiredFieldValidator ID="rfvControl" ControlToValidate="AppName"
                                            ErrorMessage="Select Application Image." runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 mb-3">
                            <asp:Label ID="Status" runat="server"></asp:Label>
                            <asp:Button ID="Submit" runat="server" OnClick="Submit_Click" Text="Submit" CssClass=" btn-secondary form-control"/>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Submit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
