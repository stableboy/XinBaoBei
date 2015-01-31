<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>鑫宝贝教育专家</title>
    <link rel="stylesheet" href="resources/css/reset.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="resources/css/style.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="resources/css/invalid.css" type="text/css" media="screen" />
    <script type="text/javascript" src="resources/scripts/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="resources/scripts/simpla.jquery.configuration.js"></script>
    <script type="text/javascript" src="resources/scripts/facebox.js"></script>
    <script type="text/javascript" src="resources/scripts/jquery.wysiwyg.js"></script>
    <script type="text/javascript" src="resources/scripts/jquery.datePicker.js"></script>
    <script type="text/javascript" src="resources/scripts/jquery.date.js"></script>
    <script language="javascript" type="text/javascript">
        function dyniframesize(down) {
            var pTar = null;
            var leftName = "sidebar";
            var left = null;
            if (document.getElementById) {
                pTar = document.getElementById(down);
                left = document.getElementById(leftName);
            }
            else {
                eval('pTar = ' + down + ';');
                eval('left = ' + leftName + ';');
            }
            if (pTar && !window.opera) {
                //begin resizing iframe 
                pTar.style.display = "block"
//                if (pTar.contentDocument && pTar.contentDocument.body.offsetHeight) {
//                    //ns6 syntax 
//                    pTar.height = pTar.contentDocument.body.offsetHeight + 20;
//                    pTar.width = pTar.contentDocument.body.scrollWidth + 20;
//                }
//                else if (pTar.Document && pTar.Document.body.scrollHeight) {
//                    //ie5+ syntax 
//                    pTar.height = pTar.Document.body.scrollHeight;
//                    pTar.width = pTar.Document.body.scrollWidth;
                //                }
                // Login页面,则页面外层展开 (Login里body里不包含margin)
                if (pTar.contentDocument
                // && pTar.contentDocument.documentElement.scrollHeight
                    && pTar.contentDocument.URL
                    && pTar.contentDocument.URL.indexOf("Login.aspx") > 0
                    ) {
                    //ns6 syntax firefox ; body中不包含margin
                    pTar.height = pTar.contentDocument.documentElement.scrollHeight + 20;
                    pTar.width = pTar.contentDocument.documentElement.scrollWidth + 20;
                }
                else if (pTar.contentDocument && pTar.contentDocument.body.scrollHeight) {
                    //ns6 syntax 
                    pTar.height = pTar.contentDocument.body.scrollHeight + 20;
                    pTar.width = pTar.contentDocument.body.scrollWidth + 20;
                }
                else if (pTar.Document && pTar.Document.body.scrollHeight) {
                    //ie5+ syntax 
                    pTar.height = pTar.Document.body.scrollHeight;
                    pTar.width = pTar.Document.body.scrollWidth;
                }

                if (document.body.clientHeight
                    && document.body.clientHeight > pTar.height
                    ) {
                    pTar.height = document.body.clientHeight;
                }
            }
        }
    </script>

    <style type="text/css">
        #ifm
        {
            width: 100%;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function logout() {
            if (confirm("您确定要退出吗?"))
                window.top.location = "Login.aspx";
            return;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body-wrapper">
            <div id="sidebar">
                <div id="sidebar-wrapper" style="position:fixed;">
                    <h1 id="sidebar-title"><a href="#">
                    </a>
                     <%--                       <br />
                                                                    <br />
                        <asp:Label ID="lblUser" runat="server" Text="" Font-Size="Medium" Font-Bold="true"></asp:Label></div>
                        <br />
                        <strong><a id="HyperLink3" onclick="logout()" href="javascript:window.opener=null;%20window.close();">注销</a></strong>
 --%>
                    </h1>
                    <div id="profile-links">

                        <br />
                        <br />
                        <asp:DropDownList ID="ddlSetOfBook" runat="server" Enabled="false" 
                            Width="110" Font-Size="Small"
                            >
                            <asp:ListItem Value="0" Text="正式" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="测试(20141106备份)"></asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <br />
                        <div>
                            <asp:Label ID="lblUser" runat="server" Text="" Font-Size="Medium" Font-Bold="true" ForeColor="#ffcc00"></asp:Label>
                        </div>
                        <br />
                        <%--<strong><a style="font-size:medium" id="HyperLink3" onclick="logout()" href="#">注销</a></strong>--%>
                        <asp:Button ID="btnLogout" runat="server" Text="注销" onclick="btnLogout_Click" 
                            BackColor="#2C2C2C" BorderColor="#CCCCCC" BorderStyle="Outset" 
                            BorderWidth="2px" Font-Bold="True" Font-Size="Medium" ForeColor="#CCCCCC" 
                            ToolTip="注销"  />
                        <br />
                    </div>
                    <ul id="main-nav">
                        <li><a href="#" class="nav-top-item no-submenu">首页 </a></li>
                        <li><a href="#" class="nav-top-item no-submenu">人员信息维护 </a>
                            <ul>
                                <li><a href="Position.aspx" target="ifm">人员信息维护</a></li>
                                <li><a href="ShowPosition.aspx" target="ifm">人员信息查询</a></li>
                            </ul>
                        </li>
                        <li><a href="#" class="nav-top-item no-submenu">目录维护 </a>
                            <ul>
                                <li><a href="Menu.aspx" target="ifm">目录维护</a></li>
                            </ul>
                        </li>

                        <li><a href="#" class="nav-top-item no-submenu">问题维护 </a>
                            <ul>
                                <li><a href="QuestionTreeView.aspx" target="ifm">问题维护</a></li>           <%--Question.aspx--%>
                            </ul>
                        </li>
                        <li><a href="#" class="nav-top-item no-submenu">答案维护 </a>
                            <ul>
                                <li><a href="AnswerTreeView.aspx" target="ifm">答案维护</a></li>               <%--Solution.aspx--%>
                            </ul>
                        </li>
                        <li><a href="#" class="nav-top-item no-submenu">问题查询及展现 </a>
                            <ul>
                                <li><a href="Show.aspx" target="ifm">问题查询及展现</a></li>
                                <li><a href="MainSearch.aspx?UserGrade=2" target="ifm">问题查询及展现(2)</a></li>
                                <%--<li><asp:LinkButton ID="lbMainSearch" runat="server" target="ifm">问题查询及展现(2)</asp:LinkButton><li>--%>
                            </ul>
                        </li>

                        <li><a href="#" class="nav-top-item no-submenu">用户表及登陆权限设置 </a>
                            <ul>
                                <li><a href="PowerSet.aspx" target="ifm">新增用户</a></li>
                                <li><a href="User.aspx" target="ifm">用户表及登陆权限设置</a></li>
                            </ul>
                        </li>

                        <%--<li><a href="#" class="nav-top-item no-submenu">登陆测试</a>
                            <ul>
                                <li><a href="Login.aspx" target="ifm">登陆测试</a></li>
                            </ul>
                        </li>--%>

                    </ul>
                    <div id="messages" style="display: none">
                    </div>
                </div>
            </div>
            <%----%><div id="main-content" style="margin: 0px 0px 0px 260px;padding: 40px 0px 0px;"  >
                <div class="content-box" >
                    <div class="content-box-header">
                        <iframe frameborder="0" marginheight="0" marginwidth="0" scrolling="auto" id="ifm" name="ifm" 
                            onload="javascript:dyniframesize('ifm');"
                            ></iframe>
                    </div>
                </div>
            </div><%----%>
        </div>
                        <%--<iframe frameborder="0" marginheight="0" marginwidth="0" scrolling="auto" id="Iframe1" name="ifm" 
                            onload="javascript:dyniframesize('ifm');"
                            ></iframe>--%>
    </form>
</body>

</html>
