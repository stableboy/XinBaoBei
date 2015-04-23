<%@ page language="C#" autoeventwireup="true" inherits="SystemConfig, App_Web_zl5hl4c8" %>

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
    <!-- <script type="text/javascript" src="resources/scripts/jquery.datePicker.js"></script> -->
    <script type="text/javascript" src="resources/scripts/jquery.date.js"></script>

</head>

<body>

    <form id="form1" runat="server">
        <div id="main-content"  style="margin: -2px 0px 0px -1px;padding: 0px 0px 0px;">
            <div class="content-box">
                <div class="content-box-header">
                    <ul class="content-box-tabs">
                        <li><a href="#tab1" class="default-tab">App参数配置</a></li>
                    </ul>
                    <div class="clear"></div>
                </div>
                <div class="bulk-actions align-left">
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        

                        <asp:GridView ID="GridView" runat="server" Width="100%" CellPadding="4" ForeColor="#333333"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="15" OnRowCancelingEdit="GridView_RowCancelingEdit"
                            OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView_RowUpdating" OnRowDeleting="GridView_RowDeleting"
                            DataKeyNames="QuestionID" OnPageIndexChanging="GridView_PageIndexChanging"
                            OnRowDataBound="GridView_RowDataBound" GridLines="None"
                                HeaderStyle-Font-Bold="true" style="table-layout:fixed;"
                            >

                            <Columns>
                                <asp:CommandField ControlStyle-Width="1px" Visible="false"
                                     UpdateText="更新" CancelText="取消" EditText="编辑"
                                    ItemStyle-ForeColor="Green" ItemStyle-Width="100px"
                                    ShowDeleteButton="True" ShowEditButton="True" HeaderText="操作"
                                        HeaderStyle-Font-Bold="true"
                                    />
                                    <%-- 
                                    DeleteText="删除" UpdateText="更新" CancelText="取消" EditText="编辑"
                                    ItemStyle-ForeColor="Green" ItemStyle-Width="100px"
                                    ShowDeleteButton="True" ShowEditButton="True" HeaderText="操作"
                                        HeaderStyle-Font-Bold="true" --%>

                                <asp:TemplateField HeaderText="参数分组" HeaderStyle-Font-Bold="true" 
                                    HeaderStyle-Width="70px">
                                    <ItemTemplate>
                                        <%# Eval("ParamGroup")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtGroup" Text='<%# Eval("ParamGroup") %>' runat="server" Width="70px" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="70px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="参数名称" HeaderStyle-Font-Bold="true"
                                        HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <%# Eval("ParamName")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtParamName" Text='<%# Eval("ParamName") %>' runat="server" Width="100px" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="参数值" HeaderStyle-Font-Bold="true"
                                        HeaderStyle-Width="70px">
                                    <ItemTemplate>
                                        <%# Eval("ParamValue") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtParamValue" Text='<%# Eval("ParamValue") %>' runat="server" Width="50px" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="描述" HeaderStyle-Font-Bold="true"
                                        HeaderStyle-Width="120px" >
                                    <ItemTemplate>
                                        <%# Eval("Description")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDescription" Text='<%# Eval("Description") %>' runat="server" Width="120px"
                                        />
                                    </EditItemTemplate>
                                    <ItemStyle Width="120px"/>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="" HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>--%>

                            </Columns>

                            <PagerSettings FirstPageText="" LastPageText="" NextPageText="" PreviousPageText="" />

                        </asp:GridView>

                    </div>

                </div>
            </div>
        </div>
    </form>
</body>

</html>

