<%@ Page Language="C#" MasterPageFile="~/NavPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ReverseUrlPageIndex_Default" Title="AspNetPager��ҳʾ����Url�����ҳ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">
    <div>��ʾ����ʾ���ʹ��AspNetPager��ҳ�ؼ�����Url�����ҳ��<br/>����������ã�<font color="red"><b>UrlPaging="true" ReverseUrlPageIndex="true"</b></font>
    <p><strong>ע��</strong>��ʾ���л�����˵�������ʹ���һҳ������ҳ��ʾ��ͬ�ļ�¼�����繲��101�����ݣ�ÿҳ��ʾ10������Ĭ�����һҳ��ֻ��һ�����ݣ�ʹ�ø�ʾ���еķ���������ʹ���һҳͬ����ʾʮ����¼�������ּ�¼��ǰһҳ�ظ���</p>
    </div><br />
        
        <webdiyer:aspnetpager id="AspNetPager1" runat="server" horizontalalign="Center" PagingButtonSpacing="8px" onpagechanged="AspNetPager1_PageChanged"
            showcustominfosection="Right" urlpaging="True" width="100%" ImagePath="~/images" PagingButtonType="Image" NumericButtonType="Text" NavigationButtonType="image" 
            ButtonImageExtension="gif" ButtonImageNameExtension="n" DisabledButtonImageNameExtension="g" ShowNavigationToolTip="true" UrlPageIndexName="page" ReverseUrlPageIndex="true"></webdiyer:aspnetpager>
        <br /><asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
            <Columns>
                <asp:BoundField DataField="orderid" HeaderText="�������" />
                <asp:BoundField DataField="orderdate" HeaderText="��������" DataFormatString="{0:d}" />
                <asp:BoundField DataField="companyname" HeaderText="��˾����" />
                <asp:BoundField DataField="employeename" HeaderText="��Ա����" />
            </Columns>
        </asp:GridView>
        <br />
        <webdiyer:AspNetPager runat="server" ID="AspNetPager2" CloneFrom="aspnetpager1"></webdiyer:AspNetPager>
</asp:Content>

