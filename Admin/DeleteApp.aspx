<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="DeleteApp.aspx.cs" Inherits="WebStore.Admin.DeleteApp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete this app?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="main" runat="server">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Label ID="Status" runat="server"></asp:Label>
            <asp:GridView ID="GDDel" runat="server" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="False" OnRowCommand="GDDel_RowCommand" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal">
                <Columns>   
                    <asp:BoundField DataField="App" HeaderText="App" />
                    <asp:TemplateField HeaderText="File">
                            <ItemTemplate>
                                <asp:LinkButton ID="AppName" runat="server" CommandArgument='<%# Eval("App") + "/" + Eval("File") %>' CommandName="DownloadNow" Text='<%# Eval("File") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    <asp:BoundField HeaderText="Size" DataField="Size" />
                    <asp:BoundField HeaderText="Type" DataField="Type" />
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="DeleteNow" runat="server" CommandName="DeleteNow" OnClientClick="Confirm()" CommandArgument='<%# Eval("App") %>' >Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#333333" />
                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#487575" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#275353" />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GDDel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
