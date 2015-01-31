<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainSearch.aspx.cs" Inherits="MainSearch" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

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
    <%--<link rel="stylesheet" href="hbhres/css/style.css" type="text/css" media="screen" />--%>
    <link rel="stylesheet" href="resources/css/invalid.css" type="text/css" media="screen" />
    <script type="text/javascript" src="resources/scripts/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="resources/scripts/simpla.jquery.configuration.js"></script>
    <script type="text/javascript" src="resources/scripts/facebox.js"></script>
    <script type="text/javascript" src="resources/scripts/jquery.wysiwyg.js"></script>
    <script type="text/javascript" src="resources/scripts/jquery.datePicker.js"></script>
    <script type="text/javascript" src="resources/scripts/jquery.date.js"></script>

    <script  language="javascript" type="text/javascript">
        // function changedExpandFold(link, foldDiv, expandDiv) {
        function changedExpandFold(link) {
            if (link) {
                var text = link.innerHTML;
//                var expand = document.getElementById(expandDiv);
//                var fold = document.getElementById(foldDiv);

//                var expand = expandDiv;
//                var fold = foldDiv;

                var txtUserGrade = document.getElementById("txtUserGrade");

                if (txtUserGrade) {

                    if (txtUserGrade.value >= "2") {

                        var fold = link.parentElement.parentElement.parentElement.children[1].children[0];
                        var expand = link.parentElement.parentElement.parentElement.children[1].children[1];

                        if (text == "展开答案") {
                            if (expand
                        && fold
                        ) {
                                //                        expand.hidden = false;
                                //                        fold.hidden = true;
                                expand.style.display = "block";
                                fold.style.display = "none";
                                link.innerHTML = "收起答案";
                            }
                        }
                        else if (text == "收起答案") {
                            if (expand
                        && fold
                        ) {
                                //                        expand.hidden = true;
                                //                        fold.hidden = false;
                                expand.style.display = "none";
                                fold.style.display = "block";
                                link.innerHTML = "展开答案";
                            }
                        }
                    } else {
                        // alert("无法查看,需申请 VIP 用户！");
                        // showModalDialog("Dialog.htm", "提示", "height=300,width=500,toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no,status=no");
                        alertWin("系统提示","无法查看,需成为 VIP 用户！","300","150");
                    }
                }
            }
            return false;
        }

        //代码来源于  http://www.cnblogs.com/sixiweb/archive/2009/03/21/1418169.html

        function alertWin(title, msg, w, h) {
            var titleheight = "22px"; // 提示窗口标题高度  
            var bordercolor = "LightBlue"; // "#666699"; // 提示窗口的边框颜色
            var titlecolor = "White";   // "#FFFFFF"; // 提示窗口的标题颜色
            var titlebgcolor = "LightBlue";   // "#666699"; // 提示窗口的标题背景色 
            var bgcolor = "#FFFFFF";  //  "#FFFFFF"; // 提示内容的背景色 

            var iWidth = document.documentElement.clientWidth;
            var iHeight = document.documentElement.clientHeight;
            var bgObj = document.createElement("div");
            bgObj.style.cssText = "position:absolute;left:0px;top:0px;width:" + iWidth + "px;height:" + Math.max(document.body.clientHeight, iHeight) + "px;filter:Alpha(Opacity=30);opacity:0.3;background-color:#000000;z-index:101;";
            document.body.appendChild(bgObj);

            var msgObj = document.createElement("div");
            // msgObj.style.cssText = "position:absolute;font:11px '宋体';top:" + (iHeight - h) / 2 + "px;left:" + (iWidth - w) / 2 + "px;width:" + w + "px;height:" + h + "px;text-align:center;border:1px solid " + bordercolor + ";background-color:" + bgcolor + ";padding:1px;line-height:22px;z-index:102;";
            msgObj.style.cssText = "position:absolute;font:11px '宋体';top:150px;left:150px;width:" + w + "px;height:" + h + "px;text-align:center;border:1px solid " + bordercolor + ";background-color:" + bgcolor + ";padding:1px;line-height:22px;z-index:102;";
            document.body.appendChild(msgObj);

            var table = document.createElement("table");
            msgObj.appendChild(table);
            table.style.cssText = "margin:0px;border:0px;padding:0px;text-align:center;vertical-align:middle;";
            table.cellSpacing = 0;
            var tr = table.insertRow(-1);
            var titleBar = tr.insertCell(-1);
            titleBar.style.cssText = "width:100%;height:" + titleheight + "px;text-align:left;padding:3px;margin:0px;font:bold 13px '宋体';color:" + titlecolor + ";border:1px solid " + bordercolor + ";cursor:move;background-color:" + titlebgcolor;
            titleBar.style.paddingLeft = "10px";
            titleBar.innerHTML = title;
            var moveX = 0;
            var moveY = 0;
            var moveTop = 0;
            var moveLeft = 0;
            var moveable = false;
            var docMouseMoveEvent = document.onmousemove;
            var docMouseUpEvent = document.onmouseup;
            titleBar.onmousedown = function () {
                var evt = getEvent();
                moveable = true;
                moveX = evt.clientX;
                moveY = evt.clientY;
                moveTop = parseInt(msgObj.style.top);
                moveLeft = parseInt(msgObj.style.left);

                document.onmousemove = function () {
                    if (moveable) {
                        var evt = getEvent();
                        var x = moveLeft + evt.clientX - moveX;
                        var y = moveTop + evt.clientY - moveY;
                        if (x > 0 && (x + w < iWidth) && y > 0 && (y + h < iHeight)) {
                            msgObj.style.left = x + "px";
                            msgObj.style.top = y + "px";
                        }
                    }
                };
                document.onmouseup = function () {
                    if (moveable) {
                        document.onmousemove = docMouseMoveEvent;
                        document.onmouseup = docMouseUpEvent;
                        moveable = false;
                        moveX = 0;
                        moveY = 0;
                        moveTop = 0;
                        moveLeft = 0;
                    }
                };
            }

            var closeBtn = tr.insertCell(-1);
            closeBtn.style.cssText = "cursor:pointer; padding:2px;background-color:" + titlebgcolor;
            closeBtn.innerHTML = "<span style='font-size:15pt; color:" + titlecolor + ";'>×</span>";
            closeBtn.onclick = function () {
                document.body.removeChild(bgObj);
                document.body.removeChild(msgObj);
            }
            var msgBox = table.insertRow(-1).insertCell(-1);
            msgBox.style.cssText = "font:10pt '宋体';text-align:center;vertical-align:middle;padding:20px;";
            msgBox.colSpan = 2;
            // msgBox.margin-top=30;
            msgBox.innerHTML = msg;

            var okButton = table.insertRow(-1).insertCell(-1);
            okButton.style.cssText = "font:10pt '宋体';text-align:center;vertical-align:middle;padding:0px;";
            // okButton.style.cssText = "width:100%;height:" + titleheight + "px;text-align:left;padding:3px;margin:0px;font:bold 13px '宋体';color:" + titlecolor + ";";
            okButton.colSpan = 2;
            // msgBox.margin-top=30;
            okButton.innerHTML = "<input type='button' id='btnClose' value='关闭' />";
            okButton.onclick = function () {
                document.body.removeChild(bgObj);
                document.body.removeChild(msgObj);
            }
            var btn = $("#btnClose");
            if (btn) {
                btn.focus();
            }


            // 获得事件Event对象，用于兼容IE和FireFox 
            function getEvent() {
                return window.event || arguments.callee.caller.arguments[0];
            }
        }
    </script>
</head>

<body>

    <form id="form1" runat="server">
        <asp:TextBox ID="txtUserGrade" runat="server" style="display:none" />
        <div id="main-content"  style="margin: -2px 0px 0px -1px;padding: 0px 0px 0px;">
        <%--style="position:fixed;width=auto;margin: -202px 0px 0px -1px;padding: 0px 0px 0px;"--%>
            <div class="content-box">
                <div class="content-box-header">
                    <ul class="content-box-tabs">
                        <li><a href="#tab1" class="default-tab">问题查询及展现</a></li>
                        
                    </ul>
                    <div class="clear"></div>
                </div>
                <div class="bulk-actions align-left">
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <div>
                        <table >
                        

                            <thead>
                            </thead>
                            <tr>
                                <td>
                                
                                    <div class="form_row">
                                     <%--style="width:700"--%>
                                        <%--<asp:Button ID="btnSea" class="form_submit" runat="server" Text="搜索" OnClick="S_Click" />--%>
                                        <label>年龄段</label>
                                        <asp:DropDownList ID="ddlAgeGroup" runat="server" CssClass="form_select" Width="270">
                                            <asp:ListItem Value="0 ~ 2岁"></asp:ListItem>
                                            <asp:ListItem Value="3 ~ 6岁"></asp:ListItem>
                                            <asp:ListItem Value="7 ~ 12岁"></asp:ListItem>
                                            <asp:ListItem Value="13 ~ 20岁"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form_row">
                                        <asp:Button ID="btnSea" class="form_submit" runat="server" Text="搜索" OnClick="S_Click" />
                                     <%--style="width:750"--%>
                                        <%--
                                        <label>问题关键字</label>
                                        <asp:TextBox ID="txtKeyWords" runat="server" class="form_input" MultiLine="false" 
                                                Alignment="0" Width="180"></asp:TextBox>
                                        --%>

                                        <%--<label>&nbsp&nbsp&nbsp&nbsp&nbsp问题描述</label>--%>
                                        <label>问题描述</label>
                                        <asp:TextBox ID="txtQuestionText" runat="server" class="form_input" MultiLine="false" 
                                                Alignment="0" Width="180"
                                                ></asp:TextBox>
                                    </div>


                                    <%--<div class="form_row">
                                        <asp:Button ID="btnSea" class="form_submit" runat="server" Text="搜索" OnClick="S_Click" />
                                    </div>--%>

                                </td>
                            </tr>

                            </table>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div>
        <%--<div class="divScroll">--%>
            <%--<asp:DataPager ID="dpQuestion" runat="server" PagedControlID="lvQuestion" >
                <Fields>
                    <asp:numericpagerfield ButtonCount="10" NextPageText="..." 
                        PreviousPageText="..." />
                    <asp:nextpreviouspagerfield FirstPageText="First" LastPageText="Last" 
                        NextPageText="Next" PreviousPageText="Previous" />
                </Fields>
            </asp:DataPager>--%>
            
            <table width="85%">
                <%--<tr style="padding-bottom:10px;">
                    <div style="padding-bottom:10px;">
                        <webdiyer:AspNetPager id="pagerQuestion" runat="server" horizontalalign="Center" 
                            onpagechanged="pagerQuestion_PageChanged"
                            showcustominfosection="Left" width="100%"  PageIndexBoxStyle="width:19px"
                            CustomInfoHTML="每页%PageSize%,当前<font color='red'><b>%CurrentPageIndex%</b></font>页,共%PageCount%页&nbsp;&nbsp; 位置 %StartRecordIndex%-%EndRecordIndex%"
                            Enabled="true"
                        
                            >
                        </webdiyer:AspNetPager>
                    </div>
                </tr>--%>
                <tr style="padding-top:10px;">
                    <div style="padding-top:10px;">
                    <asp:ListView ID="lvQuestion" runat="server" >
                    <LayoutTemplate>
                        <ul>
                            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                        </ul>
                    </LayoutTemplate>
                        <ItemTemplate>
                                        
                            <div class="feed-item folding feed-item-hook feed-item-7 " data-type="a" feed-item-a="" data-fid=":c">
                                <div class="feed-item-inner">
                                    <div class="avatar">
                                        <a class="zm-item-link-avatar" data-tip="t$b$19550517" data-original_title="互联网"><%# Eval("Questioner")%>:</a>
                                    </div>
                                    <div class="feed-main">
                                        <%--<div class="source">
                                               <p><%# Eval("KeyWords")%></p> 
                                        </div>--%>
                                        <div class="content">
                                            <div class="entry-body ">
                                                <div class="zm-item-answer-detail">
                                                    <div class="zm-item-answer-author-info">
                                                        <h2 style="font-size:18px;font-family:华文楷体;color:#666;"> <%# Eval("Title")%> </h2>
                                                        <%--<td id="td1" title="问题提出人" style="width:20"><%# Eval("Questioner")%>:</td>--%>
                                                        <%--<td id="tdQuestionTitle" title="问题描述" style="width:20"><%# Eval("Title")%></td>--%>
                                                        <%--<td id="tdKeyWords" title="关键字" style="width:20"><%# Eval("KeyWords")%></td>--%>
                                                    </div>
                                                    <div>
                                                        <div class="feed-answerAvatar">
                                                            <!-- javascript:void(0)  IE点击无响应 -->
                                                           <%-- <li><a href="javascript:void(0)" class="nav-top-item no-submenu" style="color:Blue;text-decoration:underline;" 
                                                                onclick="changedExpandFold(this,divAnswerIntro,divAnswerText);">展开答案</a></li>--%>
                                                                
                                                            <li><a style="color:Blue;text-decoration:underline;" 
                                                                onclick="changedExpandFold(this,divAnswerIntro,divAnswerText);" href="javascript:void(0)"  >展开答案</a></li>
                                                               <%-- href="javascript:void(0)" --%>
                                                        </div>
                                                        <div  class="feed-answerText">
                                                            <%--<div class="zm-item-rich-text">--%>
                                                            <div id="divAnswerIntro" class=" zm-editable-content clearfix" style="display:block;">
                                                                <%--hidden="true"--%>
                                                                 <%--style="display:inherit;"--%>
                                                                <p  style="font-size:12;"><%# Eval("Intro")%></p> 
                                                            </div>
                                                            <div id="divAnswerText" class=" zm-editable-content clearfix" style="display:none;"> 
                                                                <%--hidden="false"--%>
                                                                 <%--style="display:none;"--%>
                                                                <%--<asp:Label ID="lblAnswerText" runat="server" Text="<%# Eval("Intro")%>" />
                                                                <asp:Label ID="lblAnswerIntro" runat="server" Text="<%# Eval("STextDiv")%>" />--%>
                                                        
                                                                <%--<textarea id="txtAnswerText" rows=29><%# Eval("STextDiv")%></textarea>--%>
                                                                <p  style="font-size:12;"><%# Eval("STextDiv")%></p> 

                                                                <%--<textarea  readonly="true" style="height:auto"><%# Eval("SText")%></textarea>--%>
                                                                <%--<td id="tdAnswer" title="解答人" style="width:20;"><%# Eval("Answer")%>:</td>--%>
                                                                <%--<td id="tdAnswerText" title="答案"><%# Eval("SText")%></td>--%>
                                                                <%--<td id="tdAnswerIntro" title="答案简介"><%# Eval("Intro")%></td>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                    </div>

                </tr>
            </table>

            <%--<webdiyer:AspNetPager id="pagerQuestion2" runat="server" horizontalalign="Center" 
                onpagechanged="pagerQuestion2_PageChanged"
                showcustominfosection="Left" width="100%"  PageIndexBoxStyle="width:19px"
                CustomInfoHTML="Size&nbsp;%PageSize%,Page  <font color='red'><b>%CurrentPageIndex%</b></font> of  %PageCount%&nbsp;&nbsp;Record %StartRecordIndex%-%EndRecordIndex%"
                Enabled="true"
                >
            </webdiyer:AspNetPager>--%>
            
            <div style="padding-bottom:10px;">
                <webdiyer:AspNetPager id="pagerQuestion" runat="server" horizontalalign="Center" 
                    onpagechanged="pagerQuestion_PageChanged"
                    showcustominfosection="Left" width="100%"  PageIndexBoxStyle="width:19px"
                    CustomInfoHTML="每页%PageSize%,当前<font color='red'><b>%CurrentPageIndex%</b></font>页,共%PageCount%页&nbsp;&nbsp; 位置 %StartRecordIndex%-%EndRecordIndex%"
                    Enabled="true"
                        
                    >
                </webdiyer:AspNetPager>
            </div>


        </div>

    </form>
</body>

</html>

