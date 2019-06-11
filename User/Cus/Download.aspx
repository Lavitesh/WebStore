<%@ Page Title="" Language="C#" MasterPageFile="~/User/Cus/Customer.Master" AutoEventWireup="true" CodeBehind="Download.aspx.cs" Inherits="WebStore.User.Cus.Download" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .custom-style{
            font-size : 16px !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="GDDownloadsCus" runat="server" AutoGenerateColumns="False" OnRowCommand="GDDownloadsCus_RowCommand"
                ShowHeader="False" GridLines="None" CssClass="card-body" RowStyle-CssClass="card" ClientIDMode="Static" AllowPaging="True" 
                PageSize="4" AllowCustomPaging="False" PageIndex="0" OnDataBound="gD_DataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Icon">
                        <ItemTemplate>
                            <asp:Image ID="ImageIcon" runat="server" Height="200px" Width="200px" CssClass="card-img align-content-center"
                                AlternateText='<%# "~/" + Eval("AppName") + "/img/" + Eval("Icon") %>' 
                                ImageUrl='<%# "~/Apps/" + Eval("AppName") + "/img/" + Eval("Icon") %>' />
                        </ItemTemplate>
                        <ItemStyle CssClass="card-header" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AppName">
                        <ItemTemplate>
                            <asp:LinkButton CssClass="card-link" ID="AppName" runat="server" Text='<%# Eval("AppName") %>' 
                                CommandArgument ='<%# Eval("AppName") %>' CommandName="DownloadNow">LinkButton</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle CssClass="card-footer" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Size" DataField="Size" > <ItemStyle CssClass="card-footer" /></asp:BoundField>
                    <asp:BoundField HeaderText="Extension" DataField="Extension" > <ItemStyle CssClass="card-footer" /></asp:BoundField>
                    <asp:BoundField HeaderText="Description" DataField="Description" > <ItemStyle CssClass="card-footer" /></asp:BoundField>
                    <asp:BoundField HeaderText="Price" DataField="Price" > <ItemStyle CssClass="card-footer" /></asp:BoundField>
                </Columns>
                
                <PagerTemplate>
                            <tr style="width:100%; display:table">
                                <td>
                                    <asp:LinkButton ID="PreviousPage" runat="server" OnClick="PreviousPage_Click" CssClass="custom-style">
                                        Previous
                                    </asp:LinkButton>
                                </td>
                                <td>
                                    <asp:DropDownList ID="PageDropDownList" AutoPostBack="true" CssClass="fa-pull-right"
                                        OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged" runat="server" />
                                </td>
                                <td class="dropdown">
                                    <asp:Label ID="CurrentPageLabel" runat="server" CssClass="custom-style" />
                                </td>
                                <td style="text-align:right">
                                    <asp:LinkButton ID="NextPage" runat="server" OnClick="NextPage_Click" CssClass="custom-style">
                                        Next
                                    </asp:LinkButton>
                                </td>
                            </tr>
                </PagerTemplate>
                <RowStyle CssClass="card" />
            </asp:GridView>
            <asp:Label ID="Status" runat="server"></asp:Label>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GDDownloadsCus" />
        </Triggers>
    </asp:UpdatePanel> 
</asp:Content>
