<%@ Page Language="C#" MasterPageFile="~/NavPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Clone_Default" Title="AspNetPager��ҳʾ������¡" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">
    <div>��ʾ����ʾʹ������AspNetPager��ҳ�ؼ�Ϊͬһ���ݰ󶨿ؼ����з�ҳ��ֻ������һ��AspNetPager��ҳ�ؼ������Լ��¼����������һ��ҳ�ؼ�ʹ��CloneFrom���Կ�¡�˿ؼ������Լ��¼��������������ظ��������Լ��¼��������<br/>����������ã�<font color="red"><b>CloneFrom="Ҫ��¡��AspNetPager��ҳ�ؼ���ID"</b></font>
    </div><br />
        
        <webdiyer:aspnetpager id="AspNetPager1" runat="server" horizontalalign="Center" PagingButtonSpacing="8px" onpagechanged="AspNetPager1_PageChanged"
            showcustominfosection="Left" urlpaging="True" width="100%" ImagePath="~/images" PagingButtonType="Image" NumericButtonType="Text" NavigationButtonType="image" 
            ButtonImageExtension="gif" ButtonImageNameExtension="n" DisabledButtonImageNameExtension="g" ShowNavigationToolTip="true" UrlPageIndexName="pageindex"></webdiyer:aspnetpager>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
            <Columns>
                <asp:BoundField DataField="orderid" HeaderText="�������" />
                <asp:BoundField DataField="orderdate" HeaderText="��������" DataFormatString="{0:d}" />
                <asp:BoundField DataField="companyname" HeaderText="��˾����" />
                <asp:BoundField DataField="employeename" HeaderText="��Ա����" />
            </Columns>
        </asp:GridView>
        <webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager>
</asp:Content>

