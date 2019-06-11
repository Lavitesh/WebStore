<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Thread.aspx.cs" Inherits="WebStore.Thread" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .colortop{
            background-color:maroon;
            color: lightcyan
        } 
        .abc{
            text-transform:initial!important;
            font-family:'Buxton Sketch' !important;
            overflow-wrap: break-word;
        }

        .px30{
            font-size:30px!important;
        }
        .px18{
            font-size:18px!important;
        }
        .px24{
            font-size:24px!important;
        }

        .def{
            max-height:300px;
            max-width:500px;
            min-height:200px;
            min-width:200px;
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
                <asp:Label runat="server" ID="ThreadName" CssClass="col-md-12 form-control btn-dark text-center colortop"></asp:Label>
                <asp:Label ID="emptylist" CssClass="col-md-12 form-control" runat="server" Visible="false"></asp:Label>                 
                <asp:GridView ID="GDThread" runat="server" AutoGenerateColumns="false" GridLines="None" ShowHeader="false" 
                    OnRowCommand="GDThread_RowCommand" CssClass="container-fluid" AllowPaging="True" PageSize="4" 
                    AllowCustomPaging="False" PageIndex="0" OnDataBound="gD_DataBound">
                    <Columns>                        
                        <asp:TemplateField HeaderText="Comment">
                            <ItemTemplate>
                                <asp:Label ID="Label2" CssClass="product-desc abc px24" runat="server" Text='<%# getThreaddata(Eval("ThreadFileName").ToString()) %>'></asp:Label>
                                <br />
                                <asp:Label ID="Date" runat="server" CssClass="product-desc abc px18" Text='<%# "Posted on - " + Eval("DateThread") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="col-md-10" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>                         
                                <asp:Label ID="Label1" runat="server" CssClass="abc px30" Text='<%# Eval("UName") %>'></asp:Label>                                   
                                <asp:Label runat="server" CssClass="abc px18" Text='<%# "(" + getUserType(Eval("UType").ToString()) + ")" %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="col-md-2 text-center" />
                        </asp:TemplateField>                      
                    </Columns>
                    <PagerTemplate>
                        <table class="container-fluid">
                            <tr style="width: 100%; display: table">
                                <td>
                                    <asp:LinkButton ID="PreviousPage" runat="server" OnClick="PreviousPage_Click">
                                            Previous
                                    </asp:LinkButton>
                                </td>
                                <td>
                                    <asp:DropDownList ID="PageDropDownList" AutoPostBack="true" CssClass="fa-pull-right"
                                         OnSelectedIndexChanged="PageDropDownList_SelectedIndexChanged" runat="server" />
                                </td>
                                <td class="dropdown">
                                    <asp:Label ID="CurrentPageLabel" runat="server"/>
                                </td>
                                <td style="text-align: right">
                                    <asp:LinkButton ID="NextPage" runat="server" OnClick="NextPage_Click">
                                            Next
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </PagerTemplate>
                </asp:GridView>                
            </div>        
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GDThread" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
