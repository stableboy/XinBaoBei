<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppUserShow.aspx.cs" Inherits="AppUserShow" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
                            <asp:Button ID="btnAdd" class="form_submit" runat="server" Text="注册" 
                               
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
                  <table>
                    <tr>
                        <td >
                              <label>关联账号</label>
               
                <asp:TextBox ID="txtJudgeUser" ReadOnly="false" runat="server" class="form_input" MultiLine="false" Alignment="0" Width="410"></asp:TextBox>
            
                        </td><td width="10"></td>
                        <td>
                            <asp:Button ID="btnRefresh" class="form_submit" runat="server" Text="刷新"  OnClick="btnRefresh_Click" />
                        </td>
                     </tr>
                    </table>              
           
            </div>
            <div class="form_row">
                <label>用户名</label>
                <asp:TextBox ID="txtAccount" ReadOnly="false"  runat="server" 
                    class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
            </div>
            <div class="form_row">
                <label>用户密码</label>
                <asp:TextBox ID="txtPassword" ReadOnly="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
            </div>

            <div class="form_row">
                <label >姓名</label>
               
                <asp:TextBox ID="txtName" ReadOnly="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
                        
            </div>
            
            <div class="form_row">
                                
                <label>性别</label>
               
                <asp:TextBox ID="txtSex" ReadOnly="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
            </div>
             <div class="form_row">
                         
                <label>生日</label>
                <asp:TextBox ID="txtBirthday" ReadOnly="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
            </div>
             <div class="form_row">
                                
                <label>区域</label>
               
                <asp:TextBox ID="txtRegion" ReadOnly="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
            </div>
             <div class="form_row">
                                
                <label>地址</label>
               
                <asp:TextBox ID="txtAddress" ReadOnly="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
            </div>
             <div class="form_row">
                                
                <label>电话</label>
               
                <asp:TextBox ID="txtTel" ReadOnly="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
            </div>
             <div class="form_row">
                                
                <label>照片</label>
                 <asp:Image ID="txtPic" runat="server" BorderColor="Black" BorderWidth="1" />
                <%--<asp:TextBox ID="txtPic" ReadOnly="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
           --%> </div>

            <div class="clear"></div>
        </div>
    </div>
    <asp:TextBox ID="txtID" Visible="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
    </form>
</body>
</html>
