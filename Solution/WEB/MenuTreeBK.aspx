<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuTreeBK.aspx.cs" Inherits="MenuTreeBK" %>

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
        <div id="main-content" style="margin: 0px;">
            <div class="content-box">

                <div class="content-box-header">
                    <ul class="content-box-tabs">
                        <li><a href="#tab1" class="default-tab">目录查询</a></li>

                    </ul>
                    <div class="clear"></div>
                </div>
                <div>
                </div>

            </div>
        </div>
        <asp:TreeView ID="treeMenu" runat="server"></asp:TreeView>
    </form>
</body>

</html>
