<%@ Page Language="C#" MasterPageFile="~/NavPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CustomInfoSection_Default" Title="AspNetPagerʾ����ʹ���Զ�����Ϣ��" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">
    <div>��ʾ����ʾ���ʹ��AspNetPager��ҳ�ؼ����Զ�����Ϣ����ʾ�Զ����ҳ��Ϣ
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
    <webdiyer:aspnetpager id="AspNetPager1" runat="server" onpagechanged="AspNetPager1_PageChanged"
        showcustominfosection="Left" width="100%" CustomInfoHTML="��%CurrentPageIndex%ҳ����%PageCount%ҳ����ҳ��ʾ%PageSize%��" PageIndexBoxStyle="width:19px"></webdiyer:aspnetpager>
</asp:Content>

