<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PagerControl.ascx.cs" Inherits="UserControl_PagerControl" %>

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
    <webdiyer:aspnetpager id="AspNetPager1" runat="server" horizontalalign="Center" onpagechanged="AspNetPager1_PageChanged"
        showcustominfosection="Left" width="100%"  PageIndexBoxStyle="width:19px"
        CustomInfoHTML="Page  <font color='red'><b>%CurrentPageIndex%</b></font> of  %PageCount%&nbsp;&nbsp;Order %StartRecordIndex%-%EndRecordIndex%"></webdiyer:aspnetpager>