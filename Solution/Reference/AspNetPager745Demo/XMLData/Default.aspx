<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="XMLData_Default" MasterPageFile="~/navpage.master" Title="AspNetPager��ҳʾ����XML���ݷ�ҳ"%>
<asp:Content runat="server" ID="content1" ContentPlaceHolderID="main">
<div>��ʾ����ʾ���ʹ��AspNetPager��ҳ�ؼ��Ա�����XML�ļ��е����ݽ��з�ҳ
    </div><br />
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
    <webdiyer:aspnetpager id="AspNetPager1" runat="server" horizontalalign="Center" onpagechanged="AspNetPager1_PageChanged"
       UrlPaging="true" width="100%"></webdiyer:aspnetpager>
    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>

</asp:Content>