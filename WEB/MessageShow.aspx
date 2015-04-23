<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageShow.aspx.cs" Inherits="MessageShow" %>

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
    
    <div class="content-box-content">
        <div class="form">
            <div class="form_row">
                <table>
                    <tr>
                        <td style="width:75%">
                            <label>当前选择</label>
                        </td>
                        <td>
                            <asp:Button ID="btnSav" class="form_submit" runat="server" Text="保存" OnClick="btnSav_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnAdd" class="form_submit" runat="server" Text="新增" 
                               
                                OnClick="btnAdd_Click" />
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
                <label>消息标题</label>
                <asp:TextBox ID="txtMessageTitle" ReadOnly="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
            </div>
             <div class="form_row">
                <label style="width:auto; color:Red">提示:用于存储消息类型，例如：系统消息、个人消息等信息</label>
                </div>
                 <div class="form_row">
                <label >消息类型</label>
                <asp:TextBox ID="txtSubHead" ReadOnly="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
            </div>

            <div class="form_row">
                <label>消息内容</label>
                <asp:TextBox ID="txtMessageContent" ReadOnly="false"  runat="server" class="form_input" MultiLine="true" Alignment="0"
                    TextMode="MultiLine" Height="100" 
                ></asp:TextBox>
            </div>
            <div class="form_row" >
                <label style="width:auto; color:Red">提示:年龄段开始，例如：3岁2个月1周第1天</label>
                </div>
             <div class="form_row">
                <label>年龄段开始</label>
                <asp:TextBox ID="txtAboutAgeBegin" ReadOnly="false"  runat="server" class="form_input" MultiLine="false" Alignment="0"
                   
                ></asp:TextBox>
            </div>
             <div class="form_row" >
                <label style="width:auto; color:Red">提示:年龄段结束，例如：3岁2个月4周第3天</label>
                </div>
            <div class="form_row">
                <label>年龄段结束</label>
                <asp:TextBox ID="txtAboutAgeEnd" ReadOnly="false"  runat="server" class="form_input" MultiLine="false" Alignment="0"
                   
                ></asp:TextBox>
            </div>
               <div class="form_row">
                <table>
                   
                    <tr>
                        <td align="right" style="width:80%;text-align:right" >
                           
                            <asp:FileUpload ID="fudImportExcel" runat="server" />
                            
                        </td>
                        <td align="left"  style="text-align:left" >
                            <asp:Button ID="btnImport" class="form_submit" runat="server" Text="导入" 
                                    onclick="btnImport_Click" />
                        </td>
                    </tr>
                </table>
            </div>
              <div class="form_row">
              
                <asp:TextBox ID="txtUserID" ReadOnly="true" Visible="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
           <asp:TextBox ID="txtUserCode" ReadOnly="true" Visible="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
            <asp:TextBox ID="txtUserName" ReadOnly="true" Visible="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
           
            </div>

            <%-- <div class="form_row">
                <label>手机用户</label><!--这里需要查出用户列表,后面修改吧，暂时先这样-->
                 <asp:DropDownList ID="ddlUsers" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUser_ChangeClick">
                 <asp:ListItem Value="---" Selected="True"></asp:ListItem>
                 </asp:DropDownList>
            </div>--%>
           
            <div class="clear"></div>
        </div>
    </div>
                        
       
    <asp:TextBox ID="txtPosterID" Visible="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
       <asp:TextBox ID="txtID" Visible="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
                      
    
    </form>
</body>
</html>
