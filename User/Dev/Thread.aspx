<%@ Page Title="" Language="C#" MasterPageFile="~/User/Dev/Developer.Master" AutoEventWireup="true" CodeBehind="Thread.aspx.cs" Inherits="WebStore.User.Dev.Thread" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .colortop{
            background-color:maroon;
            color: lightcyan
        } 
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
        .fa-2x{
            font-size: 2em !important;
        }
    </style>
    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete this post?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Label ID="Status" runat="server"></asp:Label>
            <div class="container-fluid">                
                <asp:Label runat="server" ID="ThreadName" CssClass="col-lg-12 form-control btn-dark text-center colortop"></asp:Label>
                <asp:Label ID="emptylist" CssClass="col-lg-12 form-control" runat="server" Visible="false"></asp:Label>                 
                <asp:GridView ID="GDThread" runat="server" AutoGenerateColumns="false" GridLines="None" ShowHeader="false" 
                    OnRowCommand="GDThread_RowCommand" CssClass="container-fluid" AllowPaging="True" PageSize="4" 
                    AllowCustomPaging="False" PageIndex="0" OnDataBound="gD_DataBound" RowStyle-CssClass="border-bottom">
                    <Columns>                
                        <asp:TemplateField>
                            <ItemTemplate>                         
                                <asp:Label ID="Label1" runat="server" CssClass="abc" Text='<%# Eval("UName") %>'></asp:Label>   
                            </ItemTemplate>
                            <ItemStyle CssClass="" />
                        </asp:TemplateField>        
                        <asp:TemplateField HeaderText="Comment">
                            <ItemTemplate>
                                <asp:Label ID="Label2" CssClass="product-desc ghi" runat="server" Text='<%# getThreaddata(Eval("ThreadFileName").ToString()) %>'></asp:Label>
                                <br />
                                <asp:Label ID="Date" runat="server" CssClass="product-desc ghi" Text='<%# "Posted on - " + Eval("DateThread") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="col-lg-11" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>      
                                <asp:LinkButton ID="DeleteRowID" runat="server" CssClass="fa fa-trash fa-2x" CommandName="Removefile" CommandArgument='<%# Eval("ThreadFileName") %>' OnClientClick="Confirm()" Visible='<%# getvisibility(Eval("UName").ToString(),Eval("UType").ToString()) %>'></asp:LinkButton>   
                            </ItemTemplate>
                            <ItemStyle CssClass="col-lg-1" />
                        </asp:TemplateField>                        
                    </Columns>
                    <PagerTemplate>
                        <table class="container-fluid">
                            <tr>
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
                        </table>
                    </PagerTemplate>
                </asp:GridView>
                <div class="container-fluid">
                    <div class="row">
                        <asp:Button ID="LeaveComment" runat="server" CssClass="form-control btn-danger" OnClick="LeaveComment_Click" Text="Leave Comment" />                    
                        <div class="col-lg-10" style="padding:0 0 0 0">                            
                            <asp:TextBox ID="EnterText" runat="server" Visible="false" CssClass="form-control" TextMode="MultiLine" Rows="1"></asp:TextBox>                            
                        </div>
                        <div class="col-lg-2" style="padding:7px 0 0 0">
                            <asp:Button ID="Post" runat="server" CssClass="btn-dark form-control cde" OnClick="Post_Click" Text="POST" Visible="false"/>
                        </div>
                    </div>                    
                    <asp:Label ID="Nocomment" runat="server" Visible="false"></asp:Label>
                </div>
            </div>        
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GDThread" />
            <asp:PostBackTrigger ControlID="LeaveComment" />
            <asp:PostBackTrigger ControlID="Post" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
