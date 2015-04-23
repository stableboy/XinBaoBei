<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>鑫宝贝教育专家</title>

    <link type="text/css" href="css/login1.css" rel="stylesheet" />
    <link type="text/css" href="css/smoothness/jquery-ui-1.7.2.custom.html" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="js/easyTooltip.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.7.2.custom.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <div class="logo">
                <a href="#">
                    <img src="assets/logo.png" alt="" /></a>
            </div>
            <div id="box">
            
                <p class="main">
                    <label>用户名: </label>
                    <asp:TextBox ID="txtUserName" runat="server" TextMode="SingleLine" CssClass="username"
                        placeholder="用户名" />
                    <label>密码: </label>
                    <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password" CssClass="password"
                        placeholder="密码" />
                </p>

                <p class="space">
                    
                    <span>
                        <label>帐套: </label>
                        <asp:DropDownList ID="ddlSetOfBook" runat="server" CssClass="form_select" Enabled=false>
                            <asp:ListItem Value="0" Text="正式" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="测试(20141106备份)"></asp:ListItem>
                        </asp:DropDownList>

                        <asp:Button ID="btnLogin" runat="server" Text="登  录" CssClass="login"
                            OnClick="btnLogin_Click" />
                            <%--OnClientClick="return CheckInput();" --%>
                    </span>
                </p>
            </div>
        </div>
    </form>
</body>
</html>
