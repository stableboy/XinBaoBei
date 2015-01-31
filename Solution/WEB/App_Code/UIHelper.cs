using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
///UIHelper 的摘要说明
/// </summary>
public class UIHelper
{
	public UIHelper()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}


    public static void SetDeleteConfirm(GridViewRowEventArgs e,string name)
    {
        foreach (TableCell cell in e.Row.Cells)
        {
            if (cell != null
                && cell.Controls != null
                && cell.Controls.Count > 0
                )
            {
                foreach (Control ctrl in cell.Controls)
                {
                    if (ctrl is LinkButton)
                    {
                        LinkButton link = (LinkButton)ctrl;
                        if (link.Text == "删除")
                        {
                            link.Attributes.Add("onclick", string.Format("javascript:return confirm('您是要删除" + name + " 么')"));
                        }
                    }
                }
            }
        }

        ////找到GridView控件的第12个单元格中的 按钮 并给他添加 按钮事件 点击删除时提示 是否删除 
        //((Button)e.Row.Cells[0].Controls[2]).Attributes.Add("onclick", string.Format("javascript:return confirm('您是要删除" + e.Row.Cells[2].Text + " 么')"));
    }


    public static void LoadAgeGroup(Page part, DropDownList ddlAgeGroup)
    {
        // 加载年龄段        ddlAgeGroup
        string ageSelect = string.Format("SELECT * FROM T_AgeGroup order by length(substring_index(Sequence,'.',1)),substring_index(Sequence,'.',1) ");
        //string conString = ConfigurationManager.AppSettings["SqlConnStr"];
        //SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
        //DataTable dtblDiscuss = new DataTable();
        //dad.Fill(dtblDiscuss);

        DataTable dtAgeGroup = new DataTable();
        DatabaseAdapter sqlHelper2 = new DatabaseAdapter(part);
        sqlHelper2.DbHelper.Fill(dtAgeGroup, CommandType.Text, ageSelect, null);

        if (dtAgeGroup != null
            && dtAgeGroup.Rows != null
            && dtAgeGroup.Rows.Count > 0
            )
        {
            ddlAgeGroup.Items.Clear();

            foreach (DataRow row in dtAgeGroup.Rows)
            {
                if (row != null)
                {
                    ddlAgeGroup.Items.Add(row["Name"].ToString());
                }
            }
        }
    }


    public static string GetString(object obj)
    {
        if (obj != null)
        {
            string old = obj.ToString();

            //string strNew = new String(old.getBytes("gbk"), "utf-8");

            string strNew = old;        //       UTF8ToGB2312(old);

            return strNew;
        }

        return string.Empty;
    }

    public static bool IsNull(string str)
    {
        if (str != null
            && str.Length > 0
            )
        {
            return false;
        }
        return true;
    }

}

public static class ExtendMethod
{
    public static string GetPageParam(this Page page, string paramName)
    {
        string str = page.Request.QueryString[paramName];
        return str;
    }


    public static object GetItemFromSession(this Page page, string strKey)
    {
        string officalKey = GetSobKey(SetOfBookType.Offical, strKey);

        object obj = page.Session[officalKey];

        if (obj == null)
        {
            string testKey = GetSobKey(SetOfBookType.Test, strKey);

            obj = page.Session[testKey];
        }
        return obj;
    }

    public static void SetItemToSession(this Page page, SetOfBookType sobType, string strKey,object objValue)
    {
        string sobKey = GetSobKey(sobType, strKey);
        page.Session[sobKey] = objValue;
    }

    private static string GetSobKey(SetOfBookType sobType,string key)
    {
        return string.Format("{0}-{1}", (int)sobType, key);
    }
}