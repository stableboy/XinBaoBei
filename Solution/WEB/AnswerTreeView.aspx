<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnswerTreeView.aspx.cs" Inherits="QuestionTreeView" %>

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
</head>
<body>
    <form id="form1" runat="server">
        <div id="main-content"  style="margin: -2px 0px 0px -1px;padding: 0px 0px 0px;">
            <div class="content-box">
                <div class="content-box-header">
                    <ul class="content-box-tabs">
                        <li><a href="#tab1" class="default-tab">答案维护</a></li>
                    </ul>
                    <div class="clear"></div>
                </div>

                <table border="0" width="100%" cellpadding="0">
                    <tr>
                        <td align="left" valign="top" style="width: 35%;">
                            <iframe frameborder="0" id="fMenuTree" name="fMenuTree" src="MenuTree.aspx?TreeType=2" 
                                width="100%" height="450px" scrolling="auto"
                                ></iframe>
                        </td>
                        <td align="right" valign="top" style="width: 65%;">
                            <iframe id="fSingleShow" name="fSingleShow" frameborder="0" src="AnswerShow.aspx" 
                                width="100%" height="450px" scrolling="auto"
                                ></iframe>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

    </form>
</body>
</html>
