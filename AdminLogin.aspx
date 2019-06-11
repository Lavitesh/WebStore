<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="WebStore.AdminLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <asp:UpdatePanel runat="server" >
        <ContentTemplate>
            <div class="col-12 col-md-6 col-lg-5 ml-lg-auto" style="margin:10% 30% 0  0">
                <div class="order-details-confirmation mt-50">
                    <div class="cart-page-heading">
                        <h5>Login</h5>          
                        <div class="col-12 mb-3">
                            <b>Username:</b>
                            <asp:TextBox ID="UName" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUName" ControlToValidate="Uname" runat="server" ErrorMessage="Enter Username">Enter Username</asp:RequiredFieldValidator>
                        </div>

                        <div class="col-12 mb-3">
                            <b>Password:</b>
                            <asp:TextBox ID="Pass" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPass" ControlToValidate="Pass" runat="server" ErrorMessage="Enter Password" Text="Enter Password"></asp:RequiredFieldValidator>
                        </div>

                        <div class="col-12 mb-3">
                            <asp:Button ID="Submit" runat="server" CssClass=" btn-danger form-control" OnClick="Submit_Click" Text="Submit" />
                        </div>

                        <div class="col-12 mb-3">
                            <asp:Label ID="Status" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
