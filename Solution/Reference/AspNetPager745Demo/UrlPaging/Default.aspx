<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="UrlPaging_Default" MasterPageFile="~/navpage.master" Title="AspNetPager��ҳʾ����Url��ҳ"%>

<asp:Content runat="server" ContentPlaceHolderID="main">
    <div>��ʾ����ʾ���ʹ��AspNetPager��ҳ�ؼ�ͨ��Url���з�ҳ��<br/>����������ã�<font color="red"><b>UrlPaging="true"</b></font>
    </div><br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
            <Columns>
                <asp:BoundField DataField="orderid" HeaderText="�������" />
                <asp:BoundField DataField="orderdate" HeaderText="��������" DataFormatString="{0:d}" />
                <asp:BoundField DataField="companyname" HeaderText="��˾����" />
                <asp:BoundField DataField="employeename" HeaderText="��Ա����" />
            </Columns>
        </asp:GridView>
        <webdiyer:aspnetpager id="AspNetPager1" runat="server" horizontalalign="Center" PagingButtonSpacing="8px" onpagechanged="AspNetPager1_PageChanged"
            showcustominfosection="Right" urlpaging="True" width="100%" ImagePath="~/images" PagingButtonType="Image" NumericButtonType="Text" NavigationButtonType="image" 
            ButtonImageExtension="gif" ButtonImageNameExtension="n" DisabledButtonImageNameExtension="g" ShowNavigationToolTip="true" UrlPageIndexName="pageindex"></webdiyer:aspnetpager>
        
    </asp:Content>
