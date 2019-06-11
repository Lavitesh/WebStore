<%@ Page Title="" Language="C#" MasterPageFile="~/User/Cus/Customer.Master" AutoEventWireup="true" CodeBehind="AddMoney.aspx.cs" Inherits="WebStore.User.Cus.AddMoney"%>
<%@ MasterType VirtualPath="~/User/Cus/Customer.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <asp:UpdatePanel runat="server" >
        <ContentTemplate><!-- For now it is just to show that we are adding money later i will add a payment gateway here.... -->
            <div class="col-12 col-md-7 col-lg-6 ml-lg-auto" style="margin:10% 25% 0  0">
                <div class="order-details-confirmation">
                    <div class="cart-page-heading">
                        <h5 class="text-center">Confirm credentials and add amount</h5>          
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
                            <b>Enter amount:</b>
                            <asp:TextBox ID="Amount" runat="server" CssClass="form-control" TextMode="Number" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAmount" ControlToValidate="Amount" runat="server" ErrorMessage="Enter Username">Enter Username</asp:RequiredFieldValidator></br>
                            <asp:RangeValidator ID="rvAmount" Type="Double" ControlToValidate="Amount" runat="server" MinimumValue="0" ErrorMessage="Enter amount more than 0 and less than 1000" MaximumValue="1000"></asp:RangeValidator>
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
