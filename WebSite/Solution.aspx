<%@ page language="C#" autoeventwireup="true" inherits="Solution, App_Web_zl5hl4c8" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="style.css" />
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

        <div id="main-content" style="margin: 0px;">

            <div class="content-box">

                <div class="content-box-header">

                    <ul class="content-box-tabs">

                        <li><a href="#tab1" class="default-tab">答案维护</a></li>

                    </ul>

                    <div class="clear"></div>

                </div>

                <div class="bulk-actions align-left"></div>

                <div class="content-box-content">

                    <div class="tab-content default-tab" id="tab1">

                        <table>

                            <thead>

                                <tr>  <asp:GridView ID="GridView" runat="server" Width="100%" CellPadding="4" ForeColor="#333333"
                                        AutoGenerateColumns="False" AllowPaging="True" PageSize="15" OnRowCancelingEdit="GridView_RowCancelingEdit"
                                        OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView_RowUpdating" OnRowDeleting="GridView_RowDeleting"
                                        DataKeyNames="ID" OnPageIndexChanging="GridView_PageIndexChanging"
                                        OnRowDataBound="GridView_RowDataBound" GridLines="None" OnRowCommand="GridView_RowCommand" EnableModelValidation="True" OnSelectedIndexChanging="GridView_SelectedIndexChanging"
                                        HeaderStyle-Font-Bold="true"
                                        >
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" SelectText="回答" ShowDeleteButton="True" ShowEditButton="True" HeaderText="操作" ItemStyle-ForeColor="Green" ItemStyle-Width="100px" 
                                                DeleteText="删除" UpdateText="更新" CancelText="取消" EditText="编辑"
                                                 HeaderStyle-Font-Bold="true"
                                            />
                                            <asp:TemplateField HeaderText="问题编号" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCode" Text='<%# Eval("Code") %>' runat="server" Width="100px" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCode" Text='<%# Eval("Code") %>' runat="server" Width="100px" />
                                                </EditItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="问题关键字" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblKeyWords" Text='<%# Eval("KeyWords")%>' runat="server" Width="100px" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtKeyWords" Text='<%# Eval("KeyWords") %>' runat="server" Width="100px" />
                                                </EditItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="问题描述" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblText" Text='<%# Eval("Text")%>' runat="server" Width="100px" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtText" Text='<%# Eval("Text") %>' runat="server" Width="100px" />
                                                </EditItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="问题提出人" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuestioner" Text='<%# Eval("Questioner") %>' runat="server" Width="100px" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtQuestioner" Text='<%# Eval("Questioner") %>' runat="server" Width="100px" />
                                                </EditItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="问题提出时间" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTheDate" Text='<%# Eval("TheDate")%>' runat="server" Width="100px" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtTheDate" Text='<%# Eval("TheDate") %>' runat="server" Width="100px" />
                                                </EditItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

