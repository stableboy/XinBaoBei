<%@ Page Title="" Language="C#" MasterPageFile="~/NavPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="PagingButtonLayoutType_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
<style type="text/css">
.code{color:Blue;font-weight:bold}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" Runat="Server">
  ��7.2���𣬿��Խ�AspNetPager��ҳ����Ԫ�أ���ҳ����ҳ����ҳ��βҳ������ҳ������Ƕ���������б�&lt;li&gt;��&lt;span&gt;��ǩ�У��Է���Ӧ��һЩ�Ƚ����еķ�ҳ��ʽ�����Բ鿴��ҳ���htmlԴ�ļ��Ա�PagingButtonLayoutType��ͬ������ֵ����Ĳ�ͬ�������<br />
  
  <h4>��ҳ����Ԫ�ز�Ƕ���������ǩ�У�Ĭ�ϣ���</h4>
    <webdiyer:AspNetPager ID="AspNetPager3" runat="server" RecordCount="299">
    </webdiyer:AspNetPager>
  <hr />
  <h4>����ҳ����Ԫ��Ƕ����&lt;span&gt;��&lt;/span&gt;��ǩ�У�δӦ����ʽ����</h4>
  �������ã�<span class="code">PagingButtonLayoutType="Span"</span><br />
    <webdiyer:AspNetPager ID="AspNetPager2" runat="server" 
        PagingButtonLayoutType="Span" RecordCount="299" UrlPaging="True" UrlPageIndexName="pi" PageIndexBoxType="DropDownList">
    </webdiyer:AspNetPager>
    <hr />
    <h4>����ҳ����Ԫ��Ƕ����&lt;li&gt;��&lt;/li&gt;��ǩ�У�δӦ����ʽ����</h4>
    �������ã�<span class="code">PagingButtonLayoutType="UnorderedList"</span><br />
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" 
        PagingButtonLayoutType="UnorderedList" RecordCount="299" UrlPaging="True">
    </webdiyer:AspNetPager>
  
    </asp:Content>

