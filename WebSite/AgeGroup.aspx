<%@ page language="C#" autoeventwireup="true" inherits="AgeGroup, App_Web_tcrbxzfv" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        <div class="form">
        
            
            
            <div class="form_row">
                <table>
                    <tr align="right">
                        <td style="width:80%"></td>
                        <td>
                            <asp:Button ID="btnNew" class="form_submit" runat="server" Text="新增" OnClick="btnNew_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnSave" class="form_submit" runat="server" Text="保存" OnClick="btnSave_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnDelete" class="form_submit" runat="server" Text="删除" 
                                OnClientClick="return confirm('确认删除吗？')"
                                OnClick="btnDelete_Click" />
                        </td>
                    </tr>
                </table>
            </div>
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
                <label>编码</label>
                <asp:TextBox ID="txtCode" runat="server" class="form_input"></asp:TextBox>
            </div>
            <div class="form_row">
                <label>名称</label>
                <asp:TextBox ID="txtName" runat="server" class="form_input"></asp:TextBox>
            </div>

            <div class="clear"></div>

            
            <asp:TextBox ID="txtID" runat="server" Visible="false" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>


        </div>

    </form>
</body>
</html>
