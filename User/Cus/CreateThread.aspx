<%@ Page Title="" Language="C#" MasterPageFile="~/User/Cus/Customer.Master" AutoEventWireup="true" CodeBehind="CreateThread.aspx.cs" Inherits="WebStore.User.Cus.CreateThread" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="breadcumb_area bg-img" style="background-image: url(../../img/breadcumb.jpg);">
                <div class="container h-100">
                    <div class="row h-100 align-items-center">
                        <div class="col-12">
                            <div class="page-title text-center">
                                <h2>Start a new discussion</h2>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="checkout_area section-padding-80">
                <div class="container form-control">
                    <div class="row">
                        <div class="col-12">
                            <div class="checkout_details_area mt-50 clearfix">

                                <div class="cart-page-heading mb-30">
                                    <h5><asp:Label ID="ggf" runat="server"></asp:Label></h5>
                                </div>

                                <div class="row">
                                    <div class="col-12 mb-3">
                                        <b>Enter Name of Discussion</b>
                                        <asp:TextBox ID="ThreadName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvThreadName" ControlToValidate="ThreadName"
                                            ErrorMessage="Enter discussion Name." runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                    
                                    <div class="col-12 mb-3">
                                        <b>Date: </b>
                                        <asp:Label ID="ThreadStartDate" runat="server"></asp:Label>
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
