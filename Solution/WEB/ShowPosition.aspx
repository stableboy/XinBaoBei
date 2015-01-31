<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowPosition.aspx.cs" Inherits="ShowPosition" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script src="js/jquery.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/jquery.tabify.js" type="text/javascript" charset="utf-8"></script>
    <link rel="stylesheet" href="resources/css/reset.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="resources/css/style1.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="resources/css/invalid.css" type="text/css" media="screen" />
    <script type="text/javascript" src="resources/scripts/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="resources/scripts/simpla.jquery.configuration.js"></script>
    <script type="text/javascript" src="resources/scripts/facebox.js"></script>
    <script type="text/javascript" src="resources/scripts/jquery.wysiwyg.js"></script>
    <script type="text/javascript" src="resources/scripts/jquery.datePicker.js"></script>
    <script type="text/javascript" src="resources/scripts/jquery.date.js"></script>

</head>

<body>

    <form id="form1" runat="server">
        <div id="main-content"  style="margin: -2px 0px 0px -1px;padding: 0px 0px 0px;">
            <div class="content-box">
                <div class="content-box-header">
                    <ul class="content-box-tabs">
                        <li><a href="#tab1" class="default-tab">人员信息查询修改</a></li>
                    </ul>
                    <div class="clear"></div>
                </div>
                <div class="bulk-actions align-left"></div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table>

                            <thead>

                                <tr>
                                    <asp:GridView ID="GridView" runat="server" Width="100%" CellPadding="4" ForeColor="#333333"
                                        AutoGenerateColumns="False" AllowPaging="True" PageSize="15" OnRowCancelingEdit="GridView_RowCancelingEdit"
                                        OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView_RowUpdating" 
                                        OnRowDeleting="GridView_RowDeleting"
                                        DataKeyNames="id" OnPageIndexChanging="GridView_PageIndexChanging"
                                        OnRowDataBound="GridView_RowDataBound" GridLines="None"
                                        HeaderStyle-Font-Bold="true"
                                        >

                                        <Columns>
                                            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" HeaderText="操作" ItemStyle-ForeColor="Green" ItemStyle-Width="100px" 
                                                DeleteText="删除" UpdateText="更新" CancelText="取消" EditText="编辑"
                                                 HeaderStyle-Font-Bold="true"
                                            />
                                            <asp:TemplateField HeaderText="编号" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <%# Eval("Code")%>
                                                </ItemTemplate>
                                                <%--<EditItemTemplate>
                                                    <asp:TextBox ID="txtCode" Text='<%# Eval("Code") %>' runat="server" Width="100px" />
                                                </EditItemTemplate>--%>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="姓名" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <%# Eval("Name")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtName" Text='<%# Eval("Name") %>' runat="server" Width="100px" />
                                                </EditItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="身份" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <%# Eval("Type") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtType" Text='<%# Eval("Type") %>' runat="server" Width="100px" />
                                                </EditItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="性别" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <%# Eval("Sex")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <%--<asp:TextBox ID="txtSex" Text='<%# Eval("Sex") %>' runat="server" Width="140px" />--%>
                                                    <asp:DropDownList ID="DDSex" runat="server" SelectedValue='<%# Bind("Sex") %>' >        <%--CssClass="form_select"--%>
                                                        <asp:ListItem Value="男"></asp:ListItem>
                                                        <asp:ListItem Value="女"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="等级" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <%# Eval("Level")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtLevel" Text='<%# Eval("Level") %>' runat="server" Width="100px" />
                                                </EditItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="地址" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <%# Eval("Address")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtAddress" Text='<%# Eval("Address") %>' runat="server" Width="100px" />
                                                </EditItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="简介" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <%# Eval("IntroSimple")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtIntro" Text='<%# Eval("Intro") %>' runat="server" Width="100px"
                                                                TextMode="MultiLine" Height="50" />
                                                </EditItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </tr>
                            </thead>

                        </table>
                    </div>

                </div>
            </div>

        </div>
    </form>
</body>

</html>

