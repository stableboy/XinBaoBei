<%@ Page Language="C#" MasterPageFile="~/NavPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="NavigationButtons_Default" Title="AspNetPager��ҳʾ������ҳ��ť����Ч��" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">
<div>��ʾ����ʾAspNetPager���ְ�ť���ԣ�ͨ��������Щ���Կ��Կ���AspNetPager ��ʾ����ʾ��Щ��ҳ������ť��
</div><br />
    <webdiyer:aspnetpager id="AspNetPager1" runat="server" RecordCount="288"
    Width="100%"></webdiyer:aspnetpager>
    <p>���ԣ�</p>
    <asp:CheckBox ID="ck_disbtn" runat="server" Checked="True" Text="ShowDisabledButtons" AutoPostBack="True" /><br />
    <asp:CheckBox ID="ck_firstlast" runat="server" Checked="True" Text="ShowFirstLast" AutoPostBack="True" /><br />
    <asp:CheckBox ID="ck_prevnext" runat="server" Checked="True" Text="ShowPrevNext" AutoPostBack="True" /><br />
    <asp:CheckBox ID="ck_pageindex" runat="server" Checked="True" Text="ShowPageIndex" AutoPostBack="True" />
</asp:Content>

