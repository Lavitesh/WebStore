<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Forgot.aspx.cs" Inherits="WebStore.Forgot" %>

<asp:Content ID="Forgot" ContentPlaceHolderID="main" runat="server">
    <div class="breadcumb_area bg-img" style="background-image: url(img/breadcumb.jpg)">
        <div class="container h-100">
            <div class="row h-100 align-items-center">
                <div class="col-12">
                    <div class="page-title text-center">
                        <h2>Forgot</h2>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="checkout_area section-padding-80">
        <div class="container">
            <div class="row">
                <div class="order-details-confirmation mt-50 text-center">
                    <div class="cart-page-heading">
                        <h2>Enter User Details</h2>
                    </div>
                    <div class="form-control">
                        <div class="owl-hidden">
                            <asp:Label ID="EnterUName" runat="server" Text="Enter Username" Font-Bold="true"></asp:Label>
                            <asp:TextBox ID="UName" runat="server" CssClass="form-control"></asp:TextBox>  
                            <asp:RequiredFieldValidator ID="rfvUName" ControlToValidate="Uname" runat="server" 
                                ErrorMessage="Enter Username" ForeColor="Crimson">Enter Username</asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <asp:CheckBox ID="Dev" runat="server" /> 
                            <asp:Label ID="UDev" runat="server" Text="Are you a developer?" Font-Bold="true"></asp:Label>
                        </div>
                        <div>
                            <asp:Button ID="Proceed" runat="server" OnClick="Proceed_Click" Text="Proceed" CssClass=" btn-secondary form-control"/>
                        </div>

                        <div id="Proceednext" runat="server">
                            <asp:Label ID="Ques" runat="server" Visible="false" Font-Bold="true"></asp:Label>    
                            <asp:TextBox ID="Ans" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div>
                            <asp:Button ID="Submit" runat="server" OnClick="Submit_Click" Text="Submit" Visible="false" CssClass=" btn-secondary form-control"/>                        
                            <asp:Label ID ="Pass" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div>
        <asp:Label ID="Status" runat="server"></asp:Label>
    </div>


    
    
</asp:Content>
