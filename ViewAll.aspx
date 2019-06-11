<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ViewAll.aspx.cs" Inherits="WebStore.ViewAll" %>

<asp:Content ID="ViewAllhead" ContentPlaceHolderID="head" runat="server">
    <style>
        .custom-style{
            font-size : 16px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="ViewAll" ContentPlaceHolderID="main" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="GDDownloads" runat="server" AutoGenerateColumns="False" OnRowCommand="GDDownloads_RowCommand" 
                ShowHeader="False" GridLines="None" CssClass="card-body" RowStyle-CssClass="card" ClientIDMode="Static" AllowPaging="True" 
                PageSize="5" AllowCustomPaging="False" PageIndex="0" OnDataBound="gD_DataBound" >                
                <Columns>
                    <asp:TemplateField HeaderText="Icon">
                        <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" 
                                    Text='<%# "~/Apps/"+Eval("AppName")+"/img/"+Eval("Icon") %>'                                    
                                    Tooltip='<%# Eval("Icon") %>'></asp:TextBox>
                            </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Image CssClass="card-img align-content-center" ID="ImageIcon" runat="server" 
                                Height="200px" Width="200px" AlternateText='<%# Eval("Icon") %>' 
                                ImageUrl='<%# "~/Apps/" + Eval("AppName") + "/img/" + Eval("Icon") %>' />
                        </ItemTemplate>
                        <ItemStyle CssClass="card-header" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AppName">
                        <ItemTemplate>
                            <asp:LinkButton CssClass="card-link" ID="AppName" runat="server" 
                                Text='<%# Eval("AppName") %>' CommandArgument ='<%# Eval("AppName") %>' 
                                CommandName="DownloadNow">LinkButton</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle CssClass="card-footer" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Size" DataField="Size" DataFormatString="{0} Kb"> <ItemStyle CssClass="card-footer" /></asp:BoundField>
                    <asp:BoundField HeaderText="Extension" DataField="Extension" ><ItemStyle CssClass="card-footer" /></asp:BoundField>
                    <asp:BoundField HeaderText="Description" DataField="Description" ><ItemStyle CssClass="card-footer" /></asp:BoundField>
                    <asp:BoundField HeaderText="Price" DataField="Price" DataFormatString="₹ {0}"><ItemStyle CssClass="card-footer" /></asp:BoundField>
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
            <asp:PostBackTrigger ControlID="GDDownloads"/>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
