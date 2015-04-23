using System;
using System.Collections.Generic;

/// <summary>
/// Business logic loyer for Order
/// Webdiyer(www.webdiyer.com) 2006-11-27
/// </summary>
public sealed class OrderInfo
{
    private OrderInfo() { }

    /// <summary>
    /// ��ȡҪ��ҳ�ĵ�ǰҳ�ļ�¼
    /// </summary>
    /// <param name="startIndex">��ǰҳҪ��ʾ�ļ�¼����ʼ����</param>
    /// <param name="pageSize">ÿҳҪ��ʾ�ļ�¼��</param>
    /// <returns>��ǰҳҪ��ʾ�ļ�¼����</returns>
    public static List<Order> GetPagedOrders(int startIndex, int pageSize)
    {
        int endIndex = startIndex + pageSize - 1; //��ǰҳҪ��ʾ�ļ�¼�Ľ�������
        return OrderData.GetPagedOrders(startIndex, endIndex);
    }

    /// <summary>
    /// ��ȡҪ��ҳ�ļ�¼��������������AspNetPager��RecordCount���Ե�ֵ
    /// </summary>
    /// <returns></returns>
    public static int GetOrderCount()
    {
        return OrderData.GetOrderCount();
    }
}
