<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Position.aspx.cs" Inherits="Position" %>

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
    
    <script type="text/javascript">
        function show(ff, url, imgID) {
            alert('value: ' + document.getElementById('fuPicture').value);
            alert('files.length: ' + document.getElementById('fuPicture').files.length);
            alert('fileName: ' + document.getElementById('fuPicture').files.item(0).fileName);
            alert('fileSize: ' + document.getElementById('fuPicture').files.item(0).fileSize);
            alert('dataurl: ' + document.getElementById('fuPicture').files.item(0).getAsDataURL());
            alert('data: ' + document.getElementById('fuPicture').files.item(0).getAsBinary());
            alert('datatext: ' + document.getElementById('fuPicture').files.item(0).getAsText("utf-8")); 

            var firefoxUrl = ff.files.item(0).getAsDataURL();
            if (firefoxUrl) {
                document.getElementById(imgID).src = firefoxUrl;
            } else {
                document.getElementById(imgID).src = url;
            }
        }
    </script>

</head>

<body>

    <form id="form1" runat="server">
        <div id="main-content"  style="margin: -2px 0px 0px -1px;padding: 0px 0px 0px;">
            <div class="content-box">
                <div class="content-box-header">
                    <ul class="content-box-tabs">
                        <li><a href="#tab1" class="default-tab">人员信息维护</a></li>
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
                                        <div class="form" style="width:1000;">

                                            <div class="form_row">
                                                <label>编号</label>
                                                <asp:TextBox ID="txtCode" runat="server" class="form_input" Width="180" Alignment="0"></asp:TextBox>
                                                
                                                <label >名称</label>
                                                <asp:TextBox ID="txtName" runat="server" class="form_input" Width="200" Alignment="0"></asp:TextBox>
                                            </div>

                                            <%--<div class="form_row">
                                                <label>身份</label>
                                                <asp:TextBox ID="txtType" runat="server" class="form_input" MultiLine="true" Alignment="0"></asp:TextBox>
                                            </div>--%>

                                            <div class="form_row">
                                                <label>性别</label>
                                                <asp:DropDownList ID="DDSex" runat="server" CssClass="form_select">
                                                    <asp:ListItem Value="男"></asp:ListItem>
                                                    <asp:ListItem Value="女"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form_row">
                                                <label>等级</label>
                                                <asp:TextBox ID="txtLevel" runat="server" class="form_input" MultiLine="true" Alignment="0"></asp:TextBox>
                                            </div>

                                            <div class="form_row">
                                                <label>地址</label>
                                                <asp:TextBox ID="txtAddress" runat="server" class="form_input" MultiLine="true" Alignment="0"></asp:TextBox>
                                            </div>

                                            <%--<div class="form_row">
                                                <label>图片</label>
                                                <asp:FileUpload ID="fuPicture" runat="server" 
                                                    onchange="show(this,this.value,'imgPicture')"
                                                    />
                                                <asp:Image ID="imgPicture" runat="server" Width="120" Height="120"  />
                                            </div>--%>

                                            <div class="form_row">
                                                <label>简介</label>
                                                <asp:TextBox ID="txtIntro" runat="server" class="form_input" MultiLine="true" 
                                                    TextMode="MultiLine" Alignment="0" Height="100"></asp:TextBox>
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
