<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="UrlRewriting_Default" MasterPageFile="~/navpage.master" Title="AspNetPager��ҳʾ��-Url��д"%>

<asp:Content runat="server" ID="content1" ContentPlaceHolderID="main">
<div>��ʾ����ʾ��ʹ��AspNetPager��url��ҳ��ʽʱ��дurl��<br />��Ҫ���õ���������У�<font color="red"><b>EnableUrlRewriting="true" UrlRewritePattern="../Url��д/��{0}ҳ.aspx"</b></font>��
<br /><strong>ע�⣺����EnableUrlRewriting="true"���Զ�����UrlPaging="true"��Ĭ��Ϊfalse���������ٵ������ø����Ե�ֵ��</strong>
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
        <webdiyer:aspnetpager id="AspNetPager1" runat="server" horizontalalign="Center" width="100%" ShowPageIndexBox="Always"
        EnableUrlRewriting="true" UrlRewritePattern="../Url��д/��{0}ҳ.aspx" OnPageChanged="AspNetPager1_PageChanged" NumericButtonTextFormatString="-{0}-"></webdiyer:aspnetpager>
    
</asp:Content>