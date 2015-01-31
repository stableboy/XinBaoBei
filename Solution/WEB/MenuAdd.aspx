<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuAdd.aspx.cs" Inherits="MenuAdd" %>

<!DOCTYPE html>

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
        function OnCodeChanged() {

                var code = $("#txtCode").val();
                var txtName = document.getElementById('txtName');

                var ageGroup = $("#ddlAgeGroup").val();

                var parentMenu = $("#txtParentMenuName").val();

            // 顶层目录,联动,名称=编码+年龄段
            if (parentMenu == undefined
            || parentMenu == ""
            ) {
                txtName.value = code + "（" + ageGroup + "）";
            }
            // 其他目录,联动,名称=编码
            else {
                txtName.value = code;
            }
        }

        function OnSequenceChanged() {

            var seq = $("#txtSequence").val();
            var code = $("#txtCode").val();
            var txtName = document.getElementById('txtName');

//            var ageGroup = $("#ddlAgeGroup").val();

//            var parentMenu = $("#txtParentMenuName").val();

            // 联动,名称=序号 + " " + 编码
            txtName.value = seq + " " + code;
        }
    </script>
</head>

<body>
    <form id="form1" runat="server">

        <div class="form">

            <div class="form_row">

                <table>
                    <tr>
                        <td style="width:80%">
                            <label>当前选择目录</label>
                            <asp:TextBox ID="txtParamName" runat="server" Visible="true" Width="200" class="form_input" MultiLine="false" Alignment="0" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSav" class="form_submit" runat="server" Text="保存" OnClick="btnSav_Click" />
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
                <label>序号</label>
                <asp:TextBox ID="txtSequence" runat="server" class="form_input" MultiLine="true" Alignment="0" onChange="OnSequenceChanged()"></asp:TextBox>
            </div>

            <div class="form_row">
                <label>目录编码</label>
                <%--<asp:TextBox ID="txtCode" runat="server" class="form_input" MultiLine="true" Alignment="0" onChange="OnCodeChanged()"></asp:TextBox> --%>
                <asp:TextBox ID="txtCode" runat="server" class="form_input" MultiLine="true" Alignment="0" onChange="OnSequenceChanged()"></asp:TextBox>
            </div>

            <div class="form_row">
                <label>目录名称</label>
                <asp:TextBox ID="txtName" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
            </div>

            <div class="form_row">
                <label>上层目录</label>
                <asp:TextBox ID="txtParentMenuName" ReadOnly="true" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
            </div>

            <div class="form_row">
                <label>年龄段</label>
                <%--<asp:DropDownList ID="ddlAgeGroup" runat="server" CssClass="form_select" onChange="OnCodeChanged()">
                    <asp:ListItem Value="0 ~ 2岁"></asp:ListItem>
                    <asp:ListItem Value="3 ~ 6岁"></asp:ListItem>
                    <asp:ListItem Value="7 ~ 12岁"></asp:ListItem>
                    <asp:ListItem Value="13 ~ 20岁"></asp:ListItem>
                </asp:DropDownList>--%>
                <asp:DropDownList ID="ddlAgeGroup" runat="server" CssClass="form_select">
                    <asp:ListItem Value="0 ~ 2岁"></asp:ListItem>
                    <asp:ListItem Value="3 ~ 6岁"></asp:ListItem>
                    <asp:ListItem Value="7 ~ 12岁"></asp:ListItem>
                    <asp:ListItem Value="13 ~ 20岁"></asp:ListItem>                    
                </asp:DropDownList>
            </div>
            
            <div class="form_row">
                <table>
                    <tr align="right">
                        <td style="width:80%"></td>
                        <td align="right">
                            <asp:Button ID="btnAddChild" class="form_submit" runat="server" Text="增加子目录"
                                 OnClick="btnAddChild_Click" />
                        </td>
                        <td align="right">
                           <%-- <asp:Button ID="btnAnswer" class="form_submit" runat="server" Text="提问题" 
                                onclick="btnAnswer_Click" />--%>
                        
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width:80%" style="text-align:right" >
                            <asp:TextBox ID="txtSheets" runat="server" Visible="False"></asp:TextBox>
                            <asp:FileUpload ID="fudImportExcel" runat="server"   />
                            
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
                
             
                            <%--<label>级别</label>     --%>
        <asp:TextBox ID="txtLevel" runat="server" Visible="false" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>

        <%--<label  Visible="false">上级</label>           --%>
        <asp:TextBox ID="txtUpLevel" runat="server" Visible="false" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>

        <%--<label  Visible="false">上层目录ID</label>           --%>
        <asp:TextBox ID="txtParentMenuID" runat="server" Visible="false" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>

        <asp:TextBox ID="txtID" runat="server" Visible="false" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>

        <asp:TextBox ID="txtParamID" runat="server" Visible="false" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>
        
        <asp:TextBox ID="txtAgeGroupOriginal" ReadOnly="true" Visible="false" runat="server" class="form_input" MultiLine="false" Alignment="0"></asp:TextBox>

    </form>
</body>

</html>
