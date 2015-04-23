﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Question.aspx.cs" Inherits="Question" %>

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
        <div id="main-content" style="margin: 0px;">
            <div class="content-box">
                <div class="content-box-header">
                    <ul class="content-box-tabs">
                        <li><a href="#tab1" class="default-tab">问题维护</a></li>
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
                                                <label>年龄段</label>
                                                <asp:DropDownList ID="ddlAgeGroup" runat="server" CssClass="form_select">
                                                    <asp:ListItem Value="0 ~ 2岁"></asp:ListItem>
                                                    <asp:ListItem Value="3 ~ 6岁"></asp:ListItem>
                                                    <asp:ListItem Value="7 ~ 12岁"></asp:ListItem>
                                                    <asp:ListItem Value="13 ~ 20岁"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form_row">
                                                <label>问题关键字</label>
                                                <asp:TextBox ID="txtKeyWords" runat="server" class="form_input" MultiLine="true" Alignment="0"></asp:TextBox>
                                            </div>

                                            <div class="form_row">
                                                <label>问题标题</label>
                                                <asp:TextBox ID="txtText" runat="server" class="form_input" MultiLine="true" Alignment="0"></asp:TextBox>
                                            </div>

                                            <div class="form_row">
                                                <label>问题描述</label>
                                                <asp:TextBox ID="txtDescription" runat="server" class="form_input" MultiLine="true" Alignment="0"></asp:TextBox>
                                            </div>


                                            <div class="form_row">
                                                <label>问题提出人</label>
                                                <asp:TextBox ID="txtQu" runat="server" class="form_input" MultiLine="true" Alignment="0"></asp:TextBox>
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
