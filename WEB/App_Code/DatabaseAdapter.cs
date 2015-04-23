using System;
using System.Collections.Generic;
// using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
///DatabaseAdapter 的摘要说明
/// </summary>
public class DatabaseAdapter
{
    private const DatabaseTypeEnum DbType = DatabaseTypeEnum.MySql;

    //public DatabaseHelper DbHelper { get; set; }

    private DatabaseHelper dbHelper;

    public DatabaseHelper DbHelper
    {
        get { return dbHelper; }
        set { dbHelper = value; }
    }
    

    private SetOfBookType sobType = SetOfBookType.Offical;

	public DatabaseAdapter(Page page) 
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//

        DLL.User loginUser = page.Session["dsUser"] as DLL.User;
        sobType = loginUser != null ? loginUser.SetOfBook : SetOfBookType.Offical;

        SetDatabaseHelper(sobType);
	}

    public DatabaseAdapter(SetOfBookType sobType)
    {
        SetDatabaseHelper(sobType);
    }

    private void SetDatabaseHelper(SetOfBookType sobType)
    {
        switch (DbType)
        {
            case DatabaseTypeEnum.MySql:
                {
                    DbHelper = new MySqlHelper(sobType);
                }
                break;
            case DatabaseTypeEnum.SqlServer:
                {
                    DbHelper = new SqlServerHelper(sobType);
                }
                break;
            default:
                {
                    throw new Exception("没有定义有效的数据库类型!");
                }
                break;
        }
    }
}