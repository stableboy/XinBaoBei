<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="PhotoViewer_Default" MasterPageFile="~/navpage.master" Title="AspNetPagerʾ����ͼƬ�������"%>

<asp:Content runat="server" ID="content1" ContentPlaceHolderID="main">
<div>��ʾ����ʾ���ʹ��AspNetPager��ҳ�ؼ�����ͼƬ�������
    </div><br />
<div style="width:100%;text-align:center"><h3>�Ŷ�������ӭ��</h3></div>
<div style="width:100%;height:320px;text-align:center">
<asp:Image runat="server" ID="img1" /></div> 
<div style="width:100%;text-align:center">
    <webdiyer:aspnetpager id="AspNetPager1" runat="server" horizontalalign="Center" onpagechanged="AspNetPager1_PageChanged"
        pagesize="1" showpageindex="False" urlpageindexname="img"   urlpaging="True" width="500px"
        PagingButtonType="Image" ImagePath="../images/" ButtonImageNameExtension="n" ButtonImageExtension=".gif" 
        DisabledButtonImageNameExtension="g" PagingButtonSpacing="18px" ShowCustomInfoSection="Left" 
        CustomInfoHTML="����ͼƬ��<font color='red'>%currentPageIndex%</font>/%pageCount%"></webdiyer:aspnetpager></div>
</asp:Content>