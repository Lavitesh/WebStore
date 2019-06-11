<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebStore.Register" %>

<asp:Content ID="Register" ContentPlaceHolderID="main" runat="server">
    <div class="breadcumb_area bg-img" style="background-image: url(img/breadcumb.jpg)">
        <div class="container h-100">
            <div class="row h-100 align-items-center">
                <div class="col-12">
                    <div class="page-title text-center">
                        <h2>Register</h2>
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
                        <div>
                            <b>Name</b>
                            <asp:TextBox ID="Name" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="Name" ForeColor="Red"
                                ErrorMessage="Please enter your Name">Name is mandatory</asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <b>Username</b>
                            <asp:TextBox ID="UName" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUName" runat="server" ControlToValidate="UName" ForeColor="Red"
                                ErrorMessage="Please enter your Username"></asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <b>Password</b>
                            <asp:TextBox ID="Pass" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPass" runat="server" ControlToValidate="Pass" ForeColor="Red"
                                ErrorMessage="Please enter Password"></asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <b>Enter Security Question</b>
                            <asp:TextBox ID="Ques" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvQues" runat="server" ControlToValidate="Ques" ForeColor="Red"
                                ErrorMessage="Please enter security question"  ></asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <b>Answer</b>
                            <asp:TextBox ID="Ans" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAns" runat="server" ControlToValidate="Ans" ForeColor="Red"
                                ErrorMessage="Please enter Answer" ></asp:RequiredFieldValidator>
                        </div>
                        <div>        
                            <asp:CheckBox ID="Dev" runat="server"></asp:CheckBox>
                            <b>Are you a developer?</b><br/>
                        </div>
                        <div>
                            <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" CssClass=" btn-secondary form-control"></asp:Button>                                    
                            <asp:Label ID="Status" runat="server"></asp:Label>
                        </div> 
                    </div>
                </div>
            </div>        
        </div>
    </div>
</asp:Content>
