<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ImageButtons_Default" MasterPageFile="~/navpage.master" Title="AspNetPager��ҳʾ����ʹ��ͼƬ��ť"%>

<asp:Content runat="server" ID="content1" ContentPlaceHolderID="main">
    <div>��ʾ����ʾ�����AspNetPager��ҳ�ؼ���ʹ��ͼƬ��ť
    </div><br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" HeaderStyle-BackColor="#CCCCFF" AlternatingRowStyle-BackColor="#eaeaea" RowStyle-BackColor="#FAF3DC">
            <Columns>
                <asp:BoundField DataField="orderid" HeaderText="�������" />
                <asp:BoundField DataField="orderdate" HeaderText="��������" DataFormatString="{0:d}" />
                <asp:BoundField DataField="companyname" HeaderText="��˾����" />
                <asp:BoundField DataField="employeename" HeaderText="��Ա����" />
            </Columns>
    </asp:GridView>
    <webdiyer:aspnetpager id="AspNetPager1" runat="server" horizontalalign="Center"
        pagingbuttontype="Image" width="100%" ImagePath="../images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif" 
        DisabledButtonImageNameExtension="g" CpiButtonImageNameExtension="r" PagingButtonSpacing="10px" ButtonImageAlign="left"
        OnPageChanged="AspNetPager1_PageChanged"></webdiyer:aspnetpager>
</asp:Content>