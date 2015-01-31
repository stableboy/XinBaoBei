using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class PagedRepeater_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int totalOrders = (int)SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "P_GetOrderNumber");
            AspNetPager1.RecordCount = totalOrders;
            //bindData(); //ʹ��url��ҳ��ֻ���ڷ�ҳ�¼���������а����ݼ��ɣ�������Page_Load�а󶨣�����ᵼ�����ݱ�������
        }
    }

    void bindData()
    {
        Repeater1.DataSource = SqlHelper.ExecuteReader(CommandType.StoredProcedure, ConfigurationManager.AppSettings["pagedSPName"],
            new SqlParameter("@startIndex", AspNetPager1.StartRecordIndex),
            new SqlParameter("@endIndex", AspNetPager1.EndRecordIndex));
        Repeater1.DataBind();
    }

    protected void AspNetPager1_PageChanged(object src, EventArgs e)
    {
        bindData();
    }
}
