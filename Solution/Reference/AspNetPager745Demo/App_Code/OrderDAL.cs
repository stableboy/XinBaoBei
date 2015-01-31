using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

/// <summary>
/// Data access layer
/// Webdiyer(www.webdiyer.com) 2006-11-27
/// </summary>
public sealed class OrderData
{
	private OrderData(){}

    /// <summary>
    /// ��ȡҪ��ҳ�ļ�¼����
    /// </summary>
    /// <returns></returns>
    public static int GetOrderCount()
    {
        return (int)SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "P_GetOrderNumber");
    }

    /// <summary>
    /// ��ȡҪ��ҳ�ĵ�ǰҳ�ļ�¼
    /// </summary>
    /// <param name="startIndex">��ǰҳҪ��ʾ�ļ�¼����ʼ����</param>
    /// <param name="endIndex">��ǰҳҪ��ʾ�ļ�¼�Ľ�������</param>
    /// <returns>��ǰҳҪ��ʾ�ļ�¼����</returns>
    public static List<Order> GetPagedOrders(int startIndex, int endIndex)
    {
        SqlDataReader reader = SqlHelper.ExecuteReader(CommandType.StoredProcedure, System.Configuration.ConfigurationManager.AppSettings["pagedSPName"],
            new SqlParameter("@startIndex", startIndex),
            new SqlParameter("@endIndex", endIndex));
        List<Order> orders = new List<Order>();
        while (reader.Read())
        {
            orders.Add(new Order(reader.GetInt32(0),reader.GetDateTime(1),reader.GetString(2),reader.GetString(3),reader.GetString(4)));
        }
        reader.Close();
        return orders;
    }
}
