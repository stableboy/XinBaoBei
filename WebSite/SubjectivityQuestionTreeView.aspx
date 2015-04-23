<%@ page language="C#" autoeventwireup="true" inherits="SubjectivityQuestionTreeView, App_Web_azy4xpnp" %>

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
        <div id="main-content" style="margin: -2px 0px 0px -1px;padding: 0px 0px 0px;">
        <div style="margin: 0px;">
            <div class="content-box">
                <div class="content-box-header">
                    <ul class="content-box-tabs">
                        <li><a href="#tab1" class="default-tab">主观问题回答</a></li>
                    </ul>
                    <div class="clear"></div>
                </div>

                <table border="0" width="100%" cellpadding="0">
                    <tr>
                        <td align="left" valign="top" style="width: 35%;">
                            <iframe frameborder="0" id="fMenuTree" name="fMenuTree" src="SubjectivityQuestion.aspx?TreeType=1" width="100%" height="450px" scrolling="auto"></iframe>
                        </td>
                        <td align="right" valign="top" style="width: 65%;">
                            <iframe id="fSingleShow" name="fSingleShow" frameborder="0" src="SubjectivityQuestionShow.aspx" width="100%" height="450px" scrolling="auto"></iframe>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

    </form>
</body>
</html>
