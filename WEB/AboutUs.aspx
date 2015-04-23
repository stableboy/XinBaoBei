<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AboutUs.aspx.cs" Inherits="AboutUs" %>

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
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="main-content" style="margin: -2px 0px 0px -1px;padding: 0px 0px 0px;">
        <div style="margin: 0px;">
            <div class="content-box">
                <div class="content-box-header">
                    <ul class="content-box-tabs">
                        <li><a href="#tab1" class="default-tab">关于我们</a></li>
                    </ul>
                    <div class="clear"></div>
                </div>

                <table border="0" width="100%" cellpadding="0">
                    <tr align="left">
                        <td valign="top" class="style1">
                                <asp:Button  ID="btnSav" class="form_submit" runat="server" Text="保存" OnClick="btnSav_Click" />
                            
                         </td>
                         <td>
                         <asp:TextBox ID="txtID" runat="server"  MultiLine="false" Alignment="0" ReadOnly="true" Visible="false"></asp:TextBox>
                   
                                
                         </td>
                    </tr>
                    <tr>
                         <td align="right" valign="top" class="style1">
                          <asp:TextBox ID="txtMessage" runat="server" AutoPostBack="true" class="form_input" MultiLine="true"  Alignment="0"
                        TextMode="MultiLine" Height="350px" Width="100%" ></asp:TextBox>

                           
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width:80%" style="text-align:right" >
                            <asp:TextBox ID="txtSheets" runat="server" Visible="False"></asp:TextBox>
                            <asp:FileUpload ID="fudImportExcel" runat="server"   />
                            
                        </td>
                        <td align="left"  style="text-align:left" >
                            <asp:Button ID="btnImport" class="form_submit" runat="server" Text="上传" 
                                    onclick="btnImport_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>

    </form>
</body>
</html>
