<%@ Page Language="C#" Theme="Classic" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="PagedRepeater_Default" MasterPageFile="~/navpage.master" Title="AspNetPagerʾ����Repeater��ҳʾ��" %>

<asp:Content runat="server" ContentPlaceHolderID="main">
    <div>��ʾ����ʾ���ʹ��AspNetPager��ҳ�ؼ���Repeater�ؼ����з�ҳ
    </div><br />
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" UrlPaging="true" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Go To Page: " HorizontalAlign="right" PageSize="12" OnPageChanged="AspNetPager1_PageChanged" EnableTheming="true">
        </webdiyer:AspNetPager>
        <asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
        <table width="100%" border="1" cellspacing="0" cellpadding="4" style="border-collapse:collapse">
        <tr style="backGround-color:#CCCCFF"><th style="width:15%">�������</th><th style="width:15%">��������</th><th style="width:30%">��˾����</th><th style="width:20%">�ͻ����</th><th style="width:20%">��Ա����</th></tr>
        </HeaderTemplate>
        <ItemTemplate>
        <tr style="background-color:#FAF3DC">
        <td><%#DataBinder.Eval(Container.DataItem,"orderid")%></td>
        <td><%#DataBinder.Eval(Container.DataItem,"orderdate","{0:d}")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "companyname")%></td>
        <td><%#DataBinder.Eval(Container.DataItem,"customerid")%></td>
        <td><%#DataBinder.Eval(Container.DataItem,"employeename")%></td>
        </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
        <tr style="background-color:#eaeaea">
        <td><%#DataBinder.Eval(Container.DataItem,"orderid")%></td>
        <td><%#DataBinder.Eval(Container.DataItem,"orderdate","{0:d}")%></td>
        <td><%#DataBinder.Eval(Container.DataItem, "companyname")%></td>
        <td><%#DataBinder.Eval(Container.DataItem,"customerid")%></td>
        <td><%#DataBinder.Eval(Container.DataItem,"employeename")%></td>
        </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
        </table>
        </FooterTemplate>
        </asp:Repeater>
        <webdiyer:AspNetPager ID="AspNetPager2" runat="server" CloneFrom="AspNetPager1">
        </webdiyer:AspNetPager>
</asp:Content>