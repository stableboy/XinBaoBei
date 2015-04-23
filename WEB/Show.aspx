<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Show.aspx.cs" Inherits="Show" %>

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
                        <li><a href="#tab1" class="default-tab">问题查询及展现</a></li>
                    </ul>
                    <div class="clear"></div>
                </div>
                <div class="bulk-actions align-left">
                </div>
                <div class="content-box-content">
                    <div class="tab-content default-tab" id="tab1">
                        <table>

                            <thead>
                            </thead>
                            <tr>
                                <td>
                                
                                    <div class="form_row" style="width:700">
                                        <asp:Button ID="btnSea" class="form_submit" runat="server" Text="搜索" OnClick="S_Click" />
                                        <label>年龄段</label>
                                        <asp:DropDownList ID="ddlAgeGroup" runat="server" CssClass="form_select" Width="270">
                                            <asp:ListItem Value="0 ~ 2岁"></asp:ListItem>
                                            <asp:ListItem Value="3 ~ 6岁"></asp:ListItem>
                                            <asp:ListItem Value="7 ~ 12岁"></asp:ListItem>
                                            <asp:ListItem Value="13 ~ 20岁"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form_row" style="width:750">
                                        <label>问题关键字</label>
                                        <asp:TextBox ID="txtKeyWords" runat="server" class="form_input" MultiLine="false" Alignment="0" Width="180"></asp:TextBox>
                                        
                                        <label>&nbsp&nbsp&nbsp&nbsp&nbsp问题描述</label>
                                        <asp:TextBox ID="txtQuestionText" runat="server" class="form_input" MultiLine="false" Alignment="0" Width="180"></asp:TextBox>
                                    </div>


                                    <%--<div class="form_row">
                                        <asp:Button ID="btnSea" class="form_submit" runat="server" Text="搜索" OnClick="S_Click" />
                                    </div>--%>
                                </td>
                            </tr>
                        </table>

                        <asp:GridView ID="GridView" runat="server" Width="100%" CellPadding="4" ForeColor="#333333"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="15" OnRowCancelingEdit="GridView_RowCancelingEdit"
                            OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView_RowUpdating" OnRowDeleting="GridView_RowDeleting"
                            DataKeyNames="QuestionID" OnPageIndexChanging="GridView_PageIndexChanging"
                            OnRowDataBound="GridView_RowDataBound" GridLines="None"
                                HeaderStyle-Font-Bold="true" style="table-layout:fixed;"
                            >

                            <Columns>
                                <asp:CommandField ItemStyle-Width="1px" ControlStyle-Width="1px" Visible="false"
                                    />
                                    <%-- 
                                    DeleteText="删除" UpdateText="更新" CancelText="取消" EditText="编辑"
                                    ItemStyle-ForeColor="Green" ItemStyle-Width="100px"
                                    ShowDeleteButton="True" ShowEditButton="True" HeaderText="操作"
                                        HeaderStyle-Font-Bold="true" --%>

                                <asp:TemplateField HeaderText="关键字" HeaderStyle-Font-Bold="true" 
                                    HeaderStyle-Width="70px">
                                    <ItemTemplate>
                                        <%# Eval("KeyWords")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtKeyWords" Text='<%# Eval("KeyWords") %>' runat="server" Width="70px" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="70px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="问题描述" HeaderStyle-Font-Bold="true"
                                        HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <%# Eval("Title")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtQText" Text='<%# Eval("Title") %>' runat="server" Width="100px" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="问题提出人" HeaderStyle-Font-Bold="true"
                                        HeaderStyle-Width="70px">
                                    <ItemTemplate>
                                        <%# Eval("Questioner") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtQuestioner" Text='<%# Eval("Questioner") %>' runat="server" Width="50px" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="答案简介" HeaderStyle-Font-Bold="true"
                                        HeaderStyle-Width="120px" >
                                    <ItemTemplate>
                                        <a href="QuestionTreeView.aspx" target="ifm"><%# Eval("Intro")%></a>
                                        <%--<%# Eval("Intro")%>--%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtIntro" Text='<%# Eval("Intro") %>' runat="server" Width="120px"
                                        />
                                    </EditItemTemplate>
                                    <ItemStyle Width="120px"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="答案" HeaderStyle-Font-Bold="true"
                                        HeaderStyle-Width="320px" ControlStyle-Height="90px">
                                    <ItemTemplate>
                                        <%--<%# Eval("SText")%>--%>
                                        <asp:TextBox ID="txtSText" Text='<%# Eval("SText") %>' runat="server" Width="320px"
                                            TextMode="MultiLine" Height="90px" ReadOnly="true"
                                        />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSText" Text='<%# Eval("SText") %>' runat="server" Width="320px"
                                            TextMode="MultiLine" Height="90px"
                                        />
                                    </EditItemTemplate>
                                    <ItemStyle Width="320px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="解答人" HeaderStyle-Font-Bold="true"
                                        HeaderStyle-Width="70px">
                                    <ItemTemplate>
                                        <%# Eval("Answer")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtAnswer" Text='<%# Eval("Answer") %>' runat="server" Width="50px" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="50px" />
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

