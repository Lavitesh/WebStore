<%@ Page Title="" Language="C#" MasterPageFile="~/User/Cus/Customer.Master" AutoEventWireup="true" CodeBehind="DownloadApp.aspx.cs" Inherits="WebStore.User.Cus.DownloadApp" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .abc{
            text-transform:initial!important;
            font-family:'Buxton Sketch' !important;
            font-size:30px!important;
            overflow-wrap: break-word;
        }
        .def{
            max-height:300px;
            max-width:500px;
            min-height:200px;
            min-width:200px;
        }

        .ghi{
            text-transform:initial!important;
            font-family:'Buxton Sketch' !important;
            font-size:18px!important;
            overflow-wrap: break-word;
        }
        
    </style>
    <script src="../../js/Reviewtemp.js"></script>    
    <script type = "text/javascript">
        function Confirm() {

            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure to pay for the app?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
   <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <section>
                <asp:Label ID="Status" runat="server"></asp:Label>
                <div class="single_product_details_area d-flex align-items-center col-12 form-control">
                    <asp:Image ID="Iconimage" runat="server" CssClass="def" />
                    <div class="single_product_desc clearfix">
                        <asp:Label ID="Extension" runat="server"></asp:Label>
                        <asp:Label ID="AppName" runat="server"></asp:Label>
                        <p class="product-price"><asp:Label ID="Price" runat="server"></asp:Label></p>
                        <p class="product-desc"><asp:Label ID="Size" runat="server"></asp:Label></p>
                        <p class="product-desc"><asp:Label ID="Description" runat="server" CssClass="abc"></asp:Label></p>
                        <div class="cart-fav-box d-flex align-items-center">
                            <asp:Button ID="DownloadNow" runat="server" OnClick="DownloadNow_Click" Text="Download" Visible="false" CssClass="btn essence-btn" />
                        </div>
                    </div>
                </div>
                <div class="container-fluid">
                    <div class="row">                        
                        <div class="col-md-8">
                            <div class="text-center">
                                <label class="abc">Reviews</label>
                            </div>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="col-xl-12"
                                 RowStyle-CssClass="form-control" ShowHeader="false" ShowFooter="false" GridLines="None">
                                <Columns>
                                    <asp:TemplateField HeaderText="User">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" CssClass="product-desc abc" Text='<%# Eval("UName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Review">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" CssClass="product-desc ghi" runat="server" Text='<%# getReview("~/Apps/" + Session["AppName"].ToString() + "/Review/" + Eval("ReviewFile")) %>'></asp:Label>
                                            <br />
                                            <asp:Label ID="Date" runat="server" CssClass="product-desc ghi" Text='<%# "Posted on - " + Eval("DateReview") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass=" col-lg-12" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="row form-control" />
                            </asp:GridView>                              
                            <asp:Label ID="RevNotAvail" runat="server" Visible="false" CssClass="abc"></asp:Label>
                        </div> 
                        <div class="col-md-4">
                            <asp:TextBox ID="EnterText" runat="server" Visible="false" CssClass="form-control" TextMode="MultiLine" Rows="5" Columns="32"></asp:TextBox>
                            <asp:Button ID="LeaveReview" runat="server" CssClass="form-control btn-danger" OnClick="LeaveReview_Click" Text="Leave Review" />
                            <asp:Label ID="Noreview" runat="server" Visible="false"></asp:Label>
                            <asp:Button ID="Save" runat="server" CssClass="form-control btn-dark" OnClick="Save_Click" Text="Save Review" Visible="false"/>
                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="DownloadNow" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
                    <%--<div class="col-8">
                            <table class="col-12">
                                <th class="abc text-center">Reviews</th>
                                <tr class="row form-control">
                                    <td class="col-2"><asp:Label ID="uname" runat="server" CssClass="product-desc abc"></asp:Label></td><!-- will change to user image-->
                                    <td class="col-10">
                                        <asp:Label ID="ReviewText" runat="server" CssClass="product-desc abc"></asp:Label><br />
                                        <asp:Label ID="Dateid" runat="server" CssClass="product-desc abc"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>--%>