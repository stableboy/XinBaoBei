<%@ page language="C#" autoeventwireup="true" inherits="SubjectivityQuestionShow_T, App_Web_zl5hl4c8" %>

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
            
            <div class="form_row" ><label style="width:auto; color:Red">提示：问题格式下划线分开,例如：我对孩子的饮食有_方面的问题？</label></div>
            
              
             
            <div class="form_row">
                <label>问题标题</label>
                <asp:TextBox ID="txtTitle" ReadOnly="false"  runat="server" class="form_input" MultiLine="true" Alignment="0"></asp:TextBox>
            </div>

            <div class="form_row">
                <label>问题关键字</label>
                <asp:TextBox ID="txtKeyWords" ReadOnly="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
            </div>

             <div class="form_row" ><label style="width:auto; color:Red">提示：年龄段开始格式x岁x个月x周，例如3岁2个月1周</label></div>
            
            <div class="form_row">
                <label >年龄段开始</label>
               
                <asp:TextBox ID="txtAboutAgeBegin" ReadOnly="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
                        
            </div>
            
             <div class="form_row" ><label style="width:auto; color:Red">提示：年龄段结束格式x岁x个月x周，例如3岁2个月2周</label></div>
             <div class="form_row">
                                
                <label>年龄段结束</label>
               
                <asp:TextBox ID="txtAboutAgeEnd" ReadOnly="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
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
            <div class="clear"></div>
        </div>
    </div>
                        
       
    <asp:TextBox ID="txtID" Visible="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
                         
    
    </form>
</body>
</html>
