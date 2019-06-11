<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="WebStore.Search" %>


<asp:Content ID="Searchhead" ContentPlaceHolderID="head" runat="server">
    <style>
        .custom-style{
            font-size : 16px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Search" ContentPlaceHolderID="main" runat="server">    
    <asp:Label ID="Status" runat="server"></asp:Label>
    <asp:UpdatePanel runat="server" >
        <ContentTemplate>
                <asp:GridView ID="SearchView" runat="server" AutoGenerateColumns="False" OnRowCommand="SearchView_RowCommand"
                    ShowHeader="False" GridLines="None" CssClass="card-body" RowStyle-CssClass="card" ClientIDMode="Static" AllowPaging="True" 
                    PageSize="5" AllowCustomPaging="False" PageIndex="0" OnDataBound="SearchView_DataBound" >
                    <Columns>
                        <asp:TemplateField HeaderText="Icon">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" 
                                    Text='<%# "~/Apps/"+Eval("AppName")+"/img/"+Eval("Icon") %>' 
                                    Tooltip='<%# Eval("Icon") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Image CssClass="card-img align-content-center" ID="Image1" runat="server" 
                                    Height="200px" Width="200px" AlternateText='<%# Eval("Icon") %>' 
                                    ImageUrl='<%# "~/Apps/"+Eval("AppName")+"/img/"+Eval("Icon") %>' />
                            </ItemTemplate>                            
                            <ItemStyle CssClass="card-header" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="App Name">
                            <ItemTemplate>
                                <asp:LinkButton CssClass="card-link" ID="LinkButton1" runat="server" 
                                    CommandName="GoToApp" CommandArgument='<%# Eval("AppName") %>' 
                                    Text='<%# Eval("AppName") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle CssClass="card-footer" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Extension" HeaderText="Extension"><ItemStyle CssClass="card-footer" /></asp:BoundField>
                        <asp:BoundField DataField="Description" HeaderText="Description"><ItemStyle CssClass="card-footer" /></asp:BoundField>
                        <asp:BoundField DataField="Price" HeaderText="Price"><ItemStyle CssClass="card-footer" /></asp:BoundField>
                    </Columns>
                    <PagerTemplate>
                        <tr style="width: 100%; display: table">
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
                            <td style="text-align: right">
                                <asp:LinkButton ID="NextPage" runat="server" OnClick="NextPage_Click" CssClass="custom-style" CommandArgument='<%# Session["Category"] %>' CommandName='<%# Session["SearchVal"] %>'>
                                        Next
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </PagerTemplate>         
                    <RowStyle CssClass="card" />
                </asp:GridView>
                <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="SearchView"/>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
