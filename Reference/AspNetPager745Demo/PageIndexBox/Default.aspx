<%@ Page Language="C#" MasterPageFile="~/NavPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="PageIndexBox_Default" Title="AspNetPager��ҳʾ����ʹ��ҳ������" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">
    <div>AspNetPager��ҳ�ؼ������û������ѡ��Ҫ��ת��ҳ��������ʾ����ʾʹ�øù��ܡ�<br />
    ����������ã�ShowPageIndexBox="Always��Auto" PageIndeBoxType="DropDownList��TextBox"��Ĭ��ֵΪ TextBox��
    </div><br />
        <asp:DataList ID="DataList1" runat="server"  RepeatDirection="Horizontal" RepeatColumns="2" Width="100%">
        <ItemStyle Width="50%"/>
<ItemTemplate>
������ţ�<%#DataBinder.Eval(Container.DataItem,"orderid")%>&nbsp;&nbsp;&nbsp;&nbsp;
�������ڣ�<font color="red"><%#DataBinder.Eval(Container.DataItem,"orderdate","{0:d}")%></font><br>
��˾���ƣ�<%#DataBinder.Eval(Container.DataItem,"companyname")%><br>
��Ա������<%#DataBinder.Eval(Container.DataItem,"employeename")%><br>
<hr>
</ItemTemplate>
        </asp:DataList>
    <webdiyer:aspnetpager id="AspNetPager1" runat="server" horizontalalign="Center" onpagechanged="AspNetPager1_PageChanged" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="ת����" TextAfterPageIndexBox="ҳ"  PageSize="16"
        width="100%"></webdiyer:aspnetpager>
    <asp:RadioButtonList ID="rbl_boxtype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbl_boxtype_SelectedIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow">
    <asp:ListItem>�ı�������</asp:ListItem>
    <asp:ListItem Selected="true">������ѡ��</asp:ListItem>
    </asp:RadioButtonList>
</asp:Content>

