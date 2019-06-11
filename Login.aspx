<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebStore.Login" %>

<asp:Content ID="Loginhead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Login" ContentPlaceHolderID="main" runat="server">
    <div class="breadcumb_area bg-img" style="background-image: url(img/breadcumb.jpg)">
        <div class="container h-100">
            <div class="row h-100 align-items-center">
                <div class="col-12">
                    <div class="page-title text-center">
                        <h2>Login</h2>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="checkout_area">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12 col-md-6">
                    <div class="checkout_details_area mt-50 clearfix form-control">

                        <div class="cart-page-heading mb-30 text-center">
                            <h5>User Details</h5>
                        </div>

                        <div class="row">
                            <div class="col-12 mb-3">
                                <b>Username:</b>
                                <asp:TextBox ID="UName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUName" ControlToValidate="Uname" runat="server" ErrorMessage="Enter Username" ForeColor="Crimson">Enter Username</asp:RequiredFieldValidator>
                            </div>


                            <div class="col-12 mb-3">
                                <b>Password:</b>
                                <asp:TextBox ID="Pass" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPass" ControlToValidate="Pass" runat="server" ErrorMessage="Enter Password" Text="Enter Password" ForeColor="Crimson"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-12 mb-3 text-center">
                                <asp:CheckBox ID="dev" runat="server"/>
                                <b>Are you a developer?</b>
                            </div>

                            <div class="col-12 mb-3">
                                <asp:Label ID="Status" runat="server"></asp:Label>
                                <asp:Button ID="Submit" runat="server" OnClick="Submit_Click" Text="Submit" CssClass=" btn-secondary form-control" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-md-6 col-lg-5 ml-lg-auto">
                    <div class="order-details-confirmation mt-50">
                        <div class="cart-page-heading">

                            <div class="col-12 mb-3 form-control">
                                New to Web Store? <a href="Register.aspx" style="font-size: 100%">Register</a> Now
                            </div>

                            <div class="col-12 mb-3 form-control">
                                <a href="Forgot.aspx" style="font-size: 100%">Forgot</a> your password?
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
