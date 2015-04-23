<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PowerSet.aspx.cs" Inherits="PowerSet" %>

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
    <!-- <script type="text/javascript" src="resources/scripts/jquery.datePicker.js"></script> -->
    <script type="text/javascript" src="resources/scripts/jquery.date.js"></script>

</head>

<body>

    <form id="form1" runat="server">
        <div id="main-content"  style="margin: -2px 0px 0px -1px;padding: 0px 0px 0px;">
            <div class="content-box">
                <div class="content-box-header">
                    <ul class="content-box-tabs">
                        <li><a href="#tab1" class="default-tab">用户表及登陆权限设置</a></li>
                    </ul>
                    <div class="clear"></div>
                </div>

                <div class="bulk-actions align-left"></div>

                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table>
                            <tr>
                                <td>
                                    <div id="Div1" class="tabcontent">
                                        <div class="form">

                                            <div class="form_row">
                                                <label>编号</label>
                                                <asp:TextBox ID="txtCode" runat="server" class="form_input"></asp:TextBox>
                                            </div>

                                            <div class="form_row">
                                                <label>名称</label>
                                                <asp:TextBox ID="txtName" runat="server" class="form_input"></asp:TextBox>
                                            </div>

                                            <div class="form_row">
                                                <label>身份</label>
                                                <asp:DropDownList ID="DDType" runat="server" CssClass="form_select">
                                                    <asp:ListItem Text="查询" Value="查询"></asp:ListItem>
                                                    <asp:ListItem Text="维护" Value="维护"></asp:ListItem>
                                                    <asp:ListItem Text="管理" Value="管理"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form_row">
                                                <label>密码</label>
                                                <asp:TextBox ID="txtPw" runat="server" class="form_input" TextMode="Password"></asp:TextBox>
                                            </div>

                                            <div class="form_row">
                                                <asp:Button ID="btnSav" class="form_submit" runat="server" Text="保存" OnClick="btnSav_Click" />
                                            </div>
                                            <div class="clear"></div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>

</html>
