<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ForumVisitor.aspx.cs" Inherits="WebStore.ForumVisitor" %>
<asp:Content ID="ForumVisitorhead" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="ForumVisitormain" ContentPlaceHolderID="main" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Label ID="Status" runat="server"></asp:Label>
            <div class="container">
                <asp:LinkButton runat="server" CssClass="col-12 form-control btn-dark" CustomParameter="PD" OnClick="Unnamed_Click">Product Discussions</asp:LinkButton>
                <asp:Label ID="PDempty" CssClass="col-12 form-control" runat="server" Visible="false"></asp:Label>
                <asp:GridView ID="GDForumPD" runat="server" AutoGenerateColumns="false" OnRowCommand="GDForumPD_RowCommand" 
                    ShowHeader="false" ShowFooter="false" CssClass="col-12" RowStyle-CssClass="form-control" GridLines="None">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <i class="fa fa-comments fa-3x"></i>
                            </ItemTemplate>
                            <ItemStyle CssClass="col-1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" Font-Size="100%" Text='<%# Eval("TName") %>' CommandName="GoToThread" CommandArgument='<%# Eval("TName") + "/" + Eval("Type") %>'></asp:LinkButton>
                                <br />
                                <asp:Label runat="server" Font-Size="Smaller" Text='<%# "Messages: " + getmessagescount(Eval("TName").ToString(),"PD") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="col-11" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div runat="server" id="PDLast" />
            </div><br />
            <div class="container">
                <asp:LinkButton runat="server" CustomParameter="CR" CssClass="col-12 form-control btn-dark" OnClick="Unnamed_Click">Customer's Requests</asp:LinkButton>
                <asp:Label ID="CRempty" CssClass="col-12 form-control" runat="server" Visible="false"></asp:Label>
                <asp:GridView ID="GDForumCR" runat="server" AutoGenerateColumns="false" OnRowCommand="GDForumPD_RowCommand" 
                    ShowHeader="false" ShowFooter="false" CssClass="col-12" RowStyle-CssClass="form-control" GridLines="None">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <i class="fa fa-comments fa-3x"></i>
                            </ItemTemplate>
                            <ItemStyle CssClass="col-1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" Font-Size="100%" Text='<%# Eval("TName") %>' CommandName="GoToThread" CommandArgument='<%# Eval("TName") + "/" + Eval("Type") %>'></asp:LinkButton>
                                <br />
                                <asp:Label runat="server" Font-Size="Smaller" Text='<%# "Messages: " + getmessagescount(Eval("TName").ToString(),"CR") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="col-11" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>                
                <div runat="server" id="CRLast" />
            </div><br />
            <div class="container">
                <asp:LinkButton runat="server" CustomParameter="GC" CssClass="col-12 btn-dark form-control" OnClick="Unnamed_Click">General Chats</asp:LinkButton>
                <asp:Label ID="GCempty" CssClass="col-12 form-control" runat="server" Visible="false"></asp:Label>
                <asp:GridView ID="GDForumGC" runat="server" AutoGenerateColumns="false" OnRowCommand="GDForumPD_RowCommand" 
                    ShowHeader="false" ShowFooter="false" CssClass="col-12" RowStyle-CssClass="form-control" GridLines="None">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <i class="fa fa-comments fa-3x"></i>
                            </ItemTemplate>
                            <ItemStyle CssClass="col-1" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" Font-Size="100%" Text='<%# Eval("TName") %>' CommandName="GoToThread" CommandArgument='<%# Eval("TName") + "/" + Eval("Type") %>'></asp:LinkButton>
                                <br />
                                <asp:Label runat="server" Font-Size="smaller" Text='<%# "Messages: " + getmessagescount(Eval("TName").ToString(),"GC") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="col-11" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>                
                <div runat="server" id="GCLast" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GDForumPD" />
            <asp:PostBackTrigger ControlID="GDForumCR" />
            <asp:PostBackTrigger ControlID="GDForumGC" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
