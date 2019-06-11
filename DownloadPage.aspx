<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DownloadPage.aspx.cs" Inherits="WebStore.DownloadPage" %>

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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>  
            <section>
                <div class="single_product_details_area d-flex align-items-center col-12 form-control">
                    <asp:Label ID="Status" runat="server"></asp:Label>
                    <asp:Image ID="Iconimage" runat="server" CssClass="def" />
                    <div class="single_product_desc clearfix">
                        <asp:Label ID="Extension" runat="server"></asp:Label>
                        <asp:Label ID="AppName" runat="server"></asp:Label>
                        <p class="product-price"><asp:Label ID="Price" runat="server"></asp:Label></p>
                        <p class="product-desc"><asp:Label ID="Size" runat="server"></asp:Label></p>
                        <p class="product-desc"><asp:Label ID="Description" runat="server" CssClass="abc"></asp:Label></p>
                        <div class="cart-fav-box d-flex align-items-center">
                            <asp:Button ID="DownloadNow" runat="server" OnClick="DownloadNow_Click" Visible="false" CssClass="btn essence-btn" />
                        </div>
                    </div>
                </div>
                <div class="container-fluid">
                    <div class="text-center">
                        <label class="abc">Reviews</label>
                    </div>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="col-md-12"
                        RowStyle-CssClass="form-control" ShowHeader="false" ShowFooter="false" GridLines="None">
                        <Columns>
                            <asp:TemplateField HeaderText="User">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" CssClass="product-desc abc" Text='<%# Eval("UName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="col-2" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Review">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" CssClass="product-desc ghi" runat="server" Text='<%# getReview("~/Apps/" + Session["AppName"].ToString() + "/Review/" + Eval("ReviewFile")) %>'></asp:Label>
                                    <br />
                                    <asp:Label ID="Date" runat="server" CssClass="product-desc ghi" Text='<%# "Posted on - " + Eval("DateReview") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="col-10" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="row form-control" />
                    </asp:GridView>
                    <asp:Label ID="RevNotAvail" runat="server" Visible="false" CssClass="abc"></asp:Label>
                </div>
            </section>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="DownloadNow" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
