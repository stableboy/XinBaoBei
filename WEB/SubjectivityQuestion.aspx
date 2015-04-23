<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubjectivityQuestion.aspx.cs" Inherits="SubjectivityQuestion" %>

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
    <script type="text/javascript" language="javascript">
        function OnTreeNodeChecked() {
            var ele = window.event.srcElement;
            if (ele.type == 'checkbox') {
                var childrenDivID = ele.id.replace('CheckBox', 'Nodes');
                var div = document.getElementById(childrenDivID);
                if (div == null) return;
                var checkBoxs = div.getElementsByTagName('INPUT');
                for (var i = 0; i < checkBoxs.length; i++) {
                    if (checkBoxs[i].type == 'checkbox')
                        checkBoxs[i].checked = ele.checked;
                }
            }
        }
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <div >
                    <%--style="width:700"--%>
                    <%--<asp:Button ID="btnSea" class="form_submit" runat="server" Text="搜索" OnClick="S_Click" />--%>
                    <%--<asp:Label runat="server" ID="lblAgeGroup" Text="年龄段"></asp:Label>--%>
                    <asp:DropDownList ID="ddlSubQuestion" runat="server" CssClass="form_select"  Width="270">
                        <asp:ListItem Value="---"></asp:ListItem>
                        <asp:ListItem Value="未回答的"></asp:ListItem>
                        <asp:ListItem Value="全部"></asp:ListItem>
                        
                    </asp:DropDownList>
                    <asp:Button runat="server" ID="btnSearch" Text="查询"
                        onclick="btnSearch_Click" />
                </div>
            </tr>
            <tr>
                <asp:TreeView ID="treeMenu" runat="server" SelectedNodeStyle-Font-Bold="true" >
                    <%--ontreenodeexpanded="treeMenu_TreeNodeExpanded"--%>
                    <NodeStyle Font-Size="11" >
                        
                    </NodeStyle>
                </asp:TreeView>
            </tr>
        </table>
    </form>
</body>

</html>
