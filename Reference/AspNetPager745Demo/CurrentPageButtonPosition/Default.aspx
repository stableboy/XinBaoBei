<%@ Page Language="C#" MasterPageFile="~/NavPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CenterCurrentPageButton_Default" Title="AspNetPagerʾ�������е�ǰҳ������ť" %>
<asp:Content ID="Content1" ContentPlaceHolderID="main" Runat="Server">

    <div>��ʾ����ʾ�������AspNetPager ��ҳ�ظ��ѵ�ǰҳ������ť����������ҳ������ť�е�λ�ã�
    <br />��<font color="red">ע����һ�汾��ͨ������CenterCurrentPageButton="true"ʹ��ǰҳ��ť���еķ�����Ȼ�����ã���CenterCurrentPageButton�����ѱ����Ϊ��ֹ���Ժ�İ汾�н����ٿ��á�</font>��
       </div><br />
        <hr /><b>Ĭ��ֵ����ǰҳ������ťλ�ù̶����䣩</b>��<br />
        <br /><webdiyer:AspNetPager ID="AspNetPager1" runat="server" Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Go To Page: " 
        PageSize="10" RecordCount="299" CurrentPageIndex="19" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ" NextPageText="��ҳ">
        </webdiyer:AspNetPager><br />
        <hr /><b>��ǰҳ������ť��������������ҳ������ť�Ŀ�ͷ</b>��<br />
        ������ʽ���ã�&lt;webdiyer:AspNetPager ID="AspNetPager2" runat="server" <font color="blue"><b>CurrentPageButtonPosition="Beginning"</b></font> ... <br />��̷�ʽ���ã�AspNetPager2.CurrentPageButtonPosition=<font color="blue"><b>Wuqi.Webdiyer.PagingButtonPosition.Beginning</b></font><br />
        <br /><webdiyer:AspNetPager ID="AspNetPager2" runat="server" Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Go To Page: " 
        PageSize="10" RecordCount="299" ShowDisabledButtons="false" CurrentPageButtonPosition="Beginning" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ" NextPageText="��ҳ">
        </webdiyer:AspNetPager><br />
        <hr /><b>��ǰҳ������ť��������������ҳ������ť���м�</b>��<br />
        ������ʽ���ã�&lt;webdiyer:AspNetPager ID="AspNetPager3" runat="server" <font color="blue"><b>CurrentPageButtonPosition="Center"</b></font><br />��̷�ʽ���ã�AspNetPager3.CurrentPageButtonPosition=<font color="blue"><b>Wuqi.Webdiyer.PagingButtonPosition.Center</b></font><br />
        <br /><webdiyer:AspNetPager ID="AspNetPager3" runat="server" Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Go To Page: " 
        PageSize="10" RecordCount="299" CurrentPageButtonPosition="Center" CurrentPageIndex="15" NumericButtonCount="9" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ" NextPageText="��ҳ">
        </webdiyer:AspNetPager><br />
        <hr /><b>��ǰҳ������ť��������������ҳ������ť�Ľ�β</b>��<br />
        ������ʽ���ã�&lt;webdiyer:AspNetPager ID="AspNetPager4" runat="server" <font color="blue"><b>CurrentPageButtonPosition="End"</b></font><br />��̷�ʽ���ã�AspNetPager4.CurrentPageButtonPosition=<font color="blue"><b>Wuqi.Webdiyer.PagingButtonPosition.End</b></font><br />
        <br /><webdiyer:AspNetPager ID="AspNetPager4" runat="server" Width="100%" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList" TextBeforePageIndexBox="Go To Page: " 
        PageSize="10" RecordCount="299" CurrentPageButtonPosition="End" CurrentPageIndex="19" FirstPageText="��ҳ" LastPageText="βҳ" PrevPageText="��ҳ" NextPageText="��ҳ">
        </webdiyer:AspNetPager>
</asp:Content>

