<%@ Page Title="" Language="C#" MasterPageFile="~/User/Dev/Developer.Master" AutoEventWireup="true" CodeBehind="ThreadListDev.aspx.cs" Inherits="WebStore.User.Dev.ThreadListDev" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Label ID="Status" runat="server"></asp:Label>
            <div class="container-fluid">
                <asp:Label runat="server" ID="TypeName" CssClass="col-lg-12 form-control btn-dark text-center"></asp:Label>
                <asp:Label ID="emptylist" CssClass="col-lg-12 form-control" runat="server" Visible="false"></asp:Label>                
                <div runat="server" id="FirstList" />
                <asp:GridView ID="GDThreadList" runat="server" AutoGenerateColumns="false" OnRowCommand="GDThreadList_RowCommand"
                    ShowHeader="false" ShowFooter="false" CssClass="col-lg-12" RowStyle-CssClass="form-control" GridLines="None" 
                    AllowPaging="True" PageSize="4" AllowCustomPaging="False" PageIndex="0" OnDataBound="gD_DataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <i class="fa fa-comments fa-3x"></i>
                            </ItemTemplate>
                            <ItemStyle CssClass="" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" Font-Size="100%" Text='<%# Eval("TName") %>' CommandName="GoToThread" CommandArgument='<%# Eval("TName") + "/" + Eval("Type") %>'></asp:LinkButton>
                                <br />
                                <asp:Label runat="server" Font-Size="Smaller" Text='<%# "Messages: " + getmessagescount(Eval("TName").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="col-lg-12" />
                        </asp:TemplateField>
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
                                <asp:LinkButton ID="NextPage" runat="server" OnClick="NextPage_Click" CssClass="custom-style">
                                        Next
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </PagerTemplate>
                </asp:GridView>
            </div>       
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GDThreadList" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
