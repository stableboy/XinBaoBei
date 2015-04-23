using System;
using System.Collections.Generic;
// using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

/// <summary>
///ExcelHelper 的摘要说明
/// </summary>
public class ExcelHelper
{
    public const string Const_ExcelIgnoreFlag = "#";

	public ExcelHelper()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    #region 连接Excel  读取Excel数据   并返回DataSet数据集合

    /// <summary>
    /// 连接Excel  读取Excel数据   并返回DataSet数据集合
    /// </summary>
    /// <param name="filepath">Excel服务器路径</param>
    /// <param name="tableName">Excel表名称</param>
    /// <returns></returns>
    public static System.Data.DataSet ExcelSqlConnection(string filepath, string tableName)
    {
        string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";
        OleDbConnection ExcelConn = new OleDbConnection(strCon);
        try
        {
            string strCom = string.Format("SELECT * FROM [Sheet1$]");
            ExcelConn.Open();
            OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, ExcelConn);
            DataSet ds = new DataSet();
            myCommand.Fill(ds, "[" + tableName + "$]");
            ExcelConn.Close();
            return ds;
        }
        catch
        {
            ExcelConn.Close();
            return null;
        }
    }


    /// <summary>
    /// 获得Excel的所有工作簿名
    /// </summary>
    /// <param name="Excel地址"></param>
    /// <returns></returns>
    public static  string[] GetExcelSheetName(string excelFullFile)
    {
        //ExcelFile = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + excelFullFile;
        //OleDbConnection objConn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;Data Source = " + excelFullFile + ";Extended Properties = Excel 12.0;");
        string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelFullFile + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";
        OleDbConnection objConn = new OleDbConnection(strCon);
        DataTable dt = null;
        try
        {
            objConn.Open();
            dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dt == null)
            {
                return null;
            }
        }
        catch (Exception ex)
        {
           throw new Exception("Excle表读取失败!"+ ex.Message);
        }
        finally
        {
            objConn.Close();
        }
        string[] sheetnames = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow row in dt.Rows)
        {
            sheetnames[i] = row["TABLE_NAME"].ToString();
            if (sheetnames[i].Substring(0, 1) != "'")
            {
                sheetnames[i] = sheetnames[i].Remove(sheetnames[i].Length - 1);
            }
            else
            {
                sheetnames[i] = sheetnames[i].Remove(sheetnames[i].Length - 2);
                sheetnames[i] = sheetnames[i].Substring(1);
            }
            i++;

        }

        return sheetnames;
    }

    /// <summary>
    /// 连接Excel  读取Excel数据   并返回DataSet数据集合
    /// </summary>
    /// <param name="filepath">Excel服务器路径</param>
    /// <param name="tableName">Excel表名称</param>
    /// <returns></returns>
    public static System.Data.DataSet ExcelSqlConnection(string filepath, string[] sheets)
    {
        /*
        HDR=Yes，这代表第一行是标题，不做为数据使用；IMEX ( IMport EXport mode )设置
　　IMEX 有三种模式：
　　0 is Export mode
　　1 is Import mode
　　2 is Linked mode (full update capabilities)
　　我这里特别要说明的就是 IMEX 参数了，因为不同的模式代表著不同的读写行为：
　　当 IMEX=1 时为“汇出模式”，这个模式开启的 Excel 档案只能用来做“写入”用途。
　　当 IMEX=1 时为“汇入模式”，这个模式开启的 Excel 档案只能用来做“读取”用途。
　　当 IMEX=2 时为“连结模式”，这个模式开启的 Excel 档案可同时支援“读取”与“写入”用途。
意义如下:
0 ---输出模式;
1---输入模式;
2----链接模式(完全更新能力)
         */
        /*
        无法读取EXCEL中的数据单元格。有数据，但是读出来全是空值。

解决方法：

1.在导入数据连接字符串中，将IMEX=1加入，“Provider=Microsoft.Jet.OLEDB.4.0;Data Source="C:\Data.xls";Extended Properties="Excel 8.0;HDR=Yes;IMEX=1; ”，这样就可以。

注：

“HDR=Yes;”指示第一行中包含列名，而不是数据;

“IMEX=1;”通知驱动程

序始终将“互混”数据列作为文本读取。

两者必须一起使用。

本以为这样就OK了。但在实际使用过程中，这样设置还是不行，查阅了不少资料才发现，原来还有一个注册表里的信息需要修改，这样带能让excel不再使用前8行的内容来确定该列的类型。
         */
        string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";
        OleDbConnection ExcelConn = new OleDbConnection(strCon);
        DataSet ds = new DataSet();
        try
        {
            ExcelConn.Open();
            foreach (string tableName in sheets)
            {
                // Sheet名不可包含忽略字符
                if (!tableName.Contains(Const_ExcelIgnoreFlag))
                {
                    // http://blog.csdn.net/mosliang/article/details/7732444
                    // C# 用数据库读取Excel出现“定义了过多字段”错误的解决方法 
                    /*
                    原因：

Excel总列数是A-IV （255个单位长度)，建立Excel时候，执行了插入操作，会使Excel长度超过255（但列数还是显示A-IV），从而导致读取时提示“定义了过多字段”。用上面的查询语句没有限制K_rt$ 这张表的列数（程序在构造OleDbDataAdapter会加载所有的列数，包括空白列），这样就超过了Excel长度域。

解决方法：

我的K_rt$表有效数据列最长只有10列（其他的是空白列），所以查询条件我就限定在A-K列,如:

OleDbDataAdapter OleDat = new OleDbDataAdapter("select * from [K_rt$A:K]", OleDB);
，这样就不会报”定义了过多的字段”出错。

插入行也应该会出现同样的问题（目前我没遇到过，纯粹猜想，if（不慎猜对）{纯属巧合；买彩票去；}），那么限定行数和列数就可以避免此类问题
                     */
                    string strCom = string.Format("SELECT * FROM [{0}$A:K]", tableName);
                    OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, ExcelConn);
                    myCommand.Fill(ds, tableName);
                }
            }

            //ExcelConn.Close();
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            ExcelConn.Close();
        }
        return ds;
    }

    #endregion
}