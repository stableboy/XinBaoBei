using System;
using System.Collections.Generic;
using System.Data;
// using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using Common;
using System.Data.OleDb;
using System.Text;

public partial class MenuAdd : System.Web.UI.Page
{
    public const string Const_ExcelIgnoreFlag = "#";

    protected void Page_Load(object sender, EventArgs e)
    {
        // 强制检查用户是否有效，失效重登陆
        DLL.User loginUser = Session["dsUser"] as DLL.User;
        if (loginUser == null)
        {
            Response.Redirect(string.Format("Login.aspx?Redirect={0}", Request.Url.ToString()));
            return;
        }

        if (!this.IsPostBack)
        {
            UIHelper.LoadAgeGroup(this, ddlAgeGroup);

            //string strParent = Request.QueryString["Parent"];
            //if (!PubClass.IsNull(strParent))
            //{
            //    this.txtParentMenuID.Text = strParent;

            //    string strParentName = Request.QueryString["ParentName"];
            //    this.txtParentMenuName.Text = strParentName;
            //}
            string strID = Request.QueryString["ID"];
            if (!PubClass.IsNull(strID))
            {
                this.txtParamID.Text = strID;
                this.txtID.Text = strID;

                string strName = Request.QueryString["Name"];
                this.txtParamName.Text = strName;

                LoadData();
            }
            else
            {
                //btnAddChild.Visible = false;
            }

            txtCode.Focus();
        }

        // 设置年龄段只读性
        SetAgeGroupStatus();

        // 设置回答按钮状态
        SetBtnAnswerStatus();
    }

    private void SetBtnAnswerStatus()
    {
        // 暂时不控制，这个可能要判断是否叶子节点，后面再考虑吧
    }

    private void LoadData()
    {
        //LoadAgeGroup();

        if (!PubClass.IsNull(this.txtID.Text))
        {
            string where = string.Format("menu.ID={0}", this.txtID.Text);


            string selectCommand = string.Format("SELECT menu.*,parentMenu.Code as ParentCode,parentMenu.Name as ParentName FROM T_Menu menu left join T_Menu parentMenu on menu.ParentMenu = parentMenu.ID where {0}", where);
            //string conString = ConfigurationManager.AppSettings["SqlConnStr"];
            //SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
            //DataTable dtblDiscuss = new DataTable();
            //dad.Fill(dtblDiscuss);

            DataTable dtblDiscuss = new DataTable();
            DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
            sqlHelper.DbHelper.Fill(dtblDiscuss, CommandType.Text, selectCommand, null);

            if (dtblDiscuss != null
                && dtblDiscuss.Rows != null
                && dtblDiscuss.Rows.Count > 0
                )
            { 
                DataRow row = dtblDiscuss.Rows[0];

                if(row != null)
                {
                    txtCode.Text = row["Code"].ToString();
                    txtName.Text = row["Name"].ToString();
                    txtParentMenuID.Text = row["ParentMenu"].ToString();
                    txtParentMenuName.Text = row["ParentName"].ToString();

                    ddlAgeGroup.SelectedValue = row["AgeGroupName"].ToString();
                    txtAgeGroupOriginal.Text = row["AgeGroupName"].ToString();

                    txtSequence.Text = row["Sequence"].ToString();
                }
            }
        }
    }

    protected void btnSav_Click(object sender, EventArgs e)
    {
        if (PubClass.IsNull(txtCode.Text))
        {
            Response.Write(string.Format("<script>alert('目录编码不可为空！');</script>", Request.Url.ToString()));
            txtCode.Focus();
            return;
        }
        if (PubClass.IsNull(txtName.Text))
        {
            Response.Write(string.Format("<script>alert('目录名称不可为空！');</script>", Request.Url.ToString()));
            txtName.Focus();
            return;
        }


        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);

        if (!PubClass.IsNull(this.txtID.Text))
        {
            // 修改选择目录
            if (this.txtID.Text.Equals(this.txtParamID.Text))
            {
                string sql = string.Format("update T_Menu set Code='{0}',Name='{1}',AgeGroupName='{2}',Sequence='{3}' where ID={4}"
                    , txtCode.Text.Trim(), txtName.Text.Trim(), this.ddlAgeGroup.SelectedValue,this.txtSequence.Text.Trim()
                    , txtID.Text.Trim());

                int row = sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, sql);

                if (row > 0)
                {
                    //Response.Write("<script>alert('成功！');window.src='MenuAdd.aspx'</script>");
                    Response.Write(string.Format("<script>alert('成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='MenuTree.aspx?TreeType=0&Selected={1}';</script>"
                        , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this,"TreeType"), this.txtParamID.Text)));
                }
            }
            else
            {
                Response.Write(string.Format("<script>alert('页面异常！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='MenuTree.aspx?TreeType=0&Selected={1}';</script>"
                    , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this,"TreeType"), this.txtParamID.Text)));
                return;
            }
        }
        // 新增子目录
        else
        {
            //string sql = "insert T_Menu (Code,Name,Level,ParentMenu) VALUES('" + txtCode.Text.Trim() + "','" + txtName.Text.Trim() + "','" + txtLevel.Text.Trim() + "','" + txtParentMenuID.Text.Trim() + "' )";
            string sql = string.Format("insert T_Menu (Code,Name,ParentMenu,AgeGroupName,Sequence) VALUES('{0}','{1}',{2},'{3}','{4}' )"
                , txtCode.Text.Trim(), txtName.Text.Trim()
                , CommonHelper.GetStringWithNull(txtParentMenuID.Text.Trim())
                , this.ddlAgeGroup.SelectedValue
                , this.txtSequence.Text.Trim()
                );

            int row = sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, sql);

            if (row > 0)
            {
                Response.Write(string.Format("<script>alert('成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='MenuTree.aspx?TreeType=0&Selected={1}';</script>"
                    , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this,"TreeType"), this.txtParamID.Text)));
            }
        }
    }

    protected void btnAddChild_Click(object sender, EventArgs e)
    {
        this.txtParentMenuID.Text = this.txtParamID.Text;
        this.txtParentMenuName.Text = this.txtParamName.Text;

        this.txtID.Text = string.Empty;

        this.txtCode.Text = string.Empty;
        this.txtName.Text = string.Empty;

        this.txtSequence.Text = string.Empty;

        this.txtCode.Focus();

        // 防止用户修改过年龄段
        ddlAgeGroup.SelectedValue = txtAgeGroupOriginal.Text;
        // 设置年龄段只读性
        SetAgeGroupStatus();
    }

    // 设置年龄段只读性
    /// <summary>
    /// 设置年龄段只读性
    /// </summary>
    private void SetAgeGroupStatus()
    {
        // 如果顶层目录则年龄段可选择
        long parentMenuID = PubClass.GetLong(this.txtParentMenuID.Text);
        if (parentMenuID <= 0)
        {
            ddlAgeGroup.Enabled = true;
        }
        // 如果非顶层目录则年龄段不可改，跟随父目录年龄段
        else
        {
            ddlAgeGroup.Enabled = false;
        }
    }

    protected void btnAnswer_Click(object sender, EventArgs e)
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (!PubClass.IsNull(txtID.Text))
        {
            string id = txtID.Text;
            //string connStr = ConfigurationManager.AppSettings["SqlConnStr"];
            string SqlStr = "delete from T_Menu where id=" + id;


            try
            {
                //SqlConnection conn = new SqlConnection(connStr);
                //if (conn.State.ToString() == "Closed") conn.Open();
                //SqlCommand comm = new SqlCommand(SqlStr, conn);
                //comm.ExecuteNonQuery();
                //comm.Dispose();
                //if (conn.State.ToString() == "Open") conn.Close();


                DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
                sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, SqlStr);

                Response.Write(string.Format("<script>alert('删除成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='MenuTree.aspx?TreeType=0&Selected={1}';</script>", "MenuAdd.aspx"
                    , string.Format("{0}-{1}", ExtendMethod.GetPageParam(this,"TreeType"), this.txtParamID.Text)));
            }
            catch (Exception ex)
            {
                Response.Write("数据库错误，错误原因：" + ex.Message);
                Response.End();
            }
        }
    }

    // 导入
    /// <summary>
    /// 导入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (fudImportExcel.HasFile == false)//HasFile用来检查FileUpload是否有指定文件
        {
            Response.Write("<script>alert('请您选择Excel文件')</script> ");
            return;//当无文件时,返回
        }
        string IsXls = System.IO.Path.GetExtension(fudImportExcel.FileName).ToString().ToLower();//System.IO.Path.GetExtension获得文件的扩展名
        if (IsXls != ".xls")
        {
            Response.Write("<script>alert('只可以选择Excel文件')</script>");
            return;//当选择的不是Excel文件时,返回
        }
        string filename = fudImportExcel.FileName;              //获取Execle文件名  DateTime日期函数
        string fileRelativeName = ("upfiles\\") + filename;
        string savePath = Server.MapPath(fileRelativeName);//Server.MapPath 获得虚拟服务器相对路径
        fudImportExcel.SaveAs(savePath);                        //SaveAs 将上传的文件内容保存在服务器上

        string[] sheetNames = GetExcelSheetName(savePath);

        StringBuilder sbSelect = new StringBuilder();
        if (sheetNames != null
            && sheetNames.Length > 0
            )
        {
            foreach (string name in sheetNames)
            {
                // sbNames.Append(name).Append(";");
                string strSelect = string.Format("select * from [{0}$] ; ", name);

                sbSelect.Append(strSelect);
            }
        }

        //txtSheets.Text = sbNames.ToString();

        DataSet ds = ExcelSqlConnection(savePath, sheetNames);

        BatchImportDatabase(ds);

        //DataSet ds = ExcelSqlConnection(savePath, filename);           //调用自定义方法
        //DataRow[] dr = ds.Tables[0].Select();            //定义一个DataRow数组
        //int rowsnum = ds.Tables[0].Rows.Count;
        //if (rowsnum == 0)
        //{
        //    Response.Write("<script>alert('Excel表为空表,无数据!')</script>");   //当Excel表为空时,对用户进行提示
        //}
        //else
        //{
        //    for (int i = 0; i < dr.Length; i++)
        //    {
        //        //前面除了你需要在建立一个“upfiles”的文件夹外，其他的都不用管了，你只需要通过下面的方式获取Excel的值，然后再将这些值用你的方式去插入到数据库里面
        //        string title = dr[i]["标题"].ToString();
        //        string linkurl = dr[i]["链接地址"].ToString();
        //        string categoryname = dr[i]["分类"].ToString();
        //        string customername = dr[i]["内容商"].ToString();

        //        //Response.Write("<script>alert('导入内容:" + ex.Message + "')</script>");
        //    }
        //    Response.Write("<script>alert('Excle表导入成功!');</script>");
        //}

        Response.Write("<script>alert('Excle表导入成功!');</script>");
    }

    private void BatchImportDatabase(DataSet ds)
    {
        if (ds != null
            && ds.Tables != null
            && ds.Tables.Count > 0
            )
        {
            Guid guid = Guid.NewGuid();
            string strGuid = guid.ToString();

            string strUserName = string.Empty;
            DLL.User loginUser = Session["dsUser"] as DLL.User;
            if (loginUser == null)
            {
                strUserName = loginUser.Name;
            }

            string sql = "insert into T_ImportData (Sequence,Content,GUID,AgeGroupName,CreatedBy,CreatedOn,KeyWords)values(?,?,?,?,?,now(),?);";

            // 每个表导入一次
            foreach (DataTable table in ds.Tables)
            {
                if (table != null
                    // Sheet名不可为空
                    && !PubClass.IsNull(table.TableName)
                    // Sheet名不可包含忽略字符
                    && !table.TableName.Contains(Const_ExcelIgnoreFlag)
                    // 有行
                    && table.Rows != null
                    && table.Rows.Count > 0
                    && table.Columns != null
                    // 多余两列
                    && table.Columns.Count >= 2
                    )
                {
                    int totalCount = table.Rows.Count;
                    string strTableName = table.TableName;

                    int BatchSize = 2000;

                    int loopNumber = PubClass.GetInt(Math.Ceiling(((decimal)totalCount / BatchSize)));

                    for (int j = 1; j <= loopNumber; j++)
                    {
                        int curCount = Math.Min(totalCount - BatchSize * (j - 1), BatchSize);
                        int preBatchIndex = BatchSize * (j - 1);

                        //TableTypeParameter[][] sqlParamArray = new TableTypeParameter[table.Rows.Count][];
                        TableTypeParameter[][] sqlParamArray = new TableTypeParameter[curCount][];
                        //foreach (DataRow row in table.Rows)
                        //for (int i = 0; i < table.Rows.Count; i++)
                        for (int i = 0; i < curCount; i++)
                        {
                            DataRow row = table.Rows[preBatchIndex + i];

                            TableTypeParameter[] curParamArr = new TableTypeParameter[6];

                            //string sql = "insert into T_ImportData (Sequence,Content,GUID,AgeGroupName,CreatedBy,CreatedOn)values(?,?,?,?,?,now());";
                            curParamArr[0] = new TableTypeParameter("Sequence", row[0]);
                            curParamArr[1] = new TableTypeParameter("Content", row[1]);
                            curParamArr[2] = new TableTypeParameter("GUID", strGuid);
                            curParamArr[3] = new TableTypeParameter("AgeGroupName", strTableName);
                            curParamArr[4] = new TableTypeParameter("CreatedBy", strUserName);
                            if (table.Columns.Count >= 3)
                            {
                                curParamArr[5] = new TableTypeParameter("KeyWords", row[2]);
                            }
                            else
                            {
                                curParamArr[5] = new TableTypeParameter("KeyWords", ""); ;
                            }

                            sqlParamArray[i] = curParamArr;
                        }

                        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
                        sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, sql, sqlParamArray);
                    }
                }

            }

            // ,concat(Content,'(',AgeGroupName,')')
	        // ,(length(Sequence) - length(replace(Sequence,'.',''))) ,length(Sequence),length(replace(Sequence,'.',''))

            string updateSql = @"
-- 年龄段-新增
insert into T_AgeGroup
(
	Code,Name,Memo
)
select 
	distinct AgeGroupName,AgeGroupName,null
from T_ImportData
where GUID = '{0}'
	and AgeGroupName not in (select tb.Code from T_AgeGroup tb)
order by AgeGroupName
;

-- 创建需更新Menu目录的临时表
drop table if exists tmp_NeedUpdateMenu  ;
create temporary table tmp_NeedUpdateMenu (
select T_Menu.ID,imp2.Content as Code,concat(imp2.Sequence,' ',imp2.Content) as Name,imp2.AgeGroupName as AgeGroupName
from T_Menu,T_ImportData imp2
where 1=1
	and T_Menu.Sequence = imp2.Sequence
	and imp2.GUID = '{0}'
	and (length(imp2.Sequence) - length(replace(imp2.Sequence,'.',''))) < 3
	and (T_Menu.Code != imp2.Content 
		or T_Menu.Name != concat(imp2.Sequence,' ',imp2.Content)
		or T_Menu.AgeGroupName != imp2.AgeGroupName
		)
	)
;
-- 目录-更新
update T_Menu,tmp_NeedUpdateMenu imp2
set
	T_Menu.Code = imp2.Code
	,T_Menu.Name = imp2.Name
	,T_Menu.AgeGroupName = imp2.AgeGroupName
where T_Menu.ID = imp2.ID
; xz

-- 目录-新增
insert into T_Menu
(
	Sequence,Code,Name,ParentMenu,AgeGroupName
)
select 
	Sequence,Content,concat(Sequence,' ',Content),null,AgeGroupName
from T_ImportData
where GUID = '{0}'
	and (length(Sequence) - length(replace(Sequence,'.',''))) < 3
	and Sequence not in (select tb.Sequence from T_Menu tb)
order by Sequence
;
-- 更新父目录ID
drop table if exists tmp_menu 
;
create table tmp_menu as select ID,Sequence from T_Menu
;
update T_Menu
set ParentMenu = (select min(parent.ID) from tmp_menu parent 
		where parent.Sequence = substring_index(T_Menu.Sequence,'.'
					,(length(T_Menu.Sequence) - length(replace(T_Menu.Sequence,'.','')))
											)
		)
where ParentMenu is null
;
drop table if exists tmp_menu 
;
-- 问题
insert into T_Question
(
	Sequence,Title,Description,KeyWords,AgeGroupName,ParentMenu
)
select 
	Sequence,Content,Content,KeyWords,AgeGroupName,(select min(parent.ID) from T_Menu parent 
			where parent.Sequence = substring_index(T_ImportData.Sequence,'.'
						,(length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.','')))
												)
			)
from T_ImportData
where GUID = '{0}'
	and (length(Sequence) - length(replace(Sequence,'.',''))) = 3
	and Sequence not in (select tb.Sequence from T_Question tb)
order by Sequence
;
-- 答案
insert into T_Solution
(
	Sequence,SText,Intro,Question
)
select 
	Sequence,Content,null,(select min(parent.ID) from T_Question parent 
			where parent.Sequence = substring_index(T_ImportData.Sequence,'.'
						,(length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.','')))
												)
			)
from T_ImportData
where GUID = '{0}'
	and (length(Sequence) - length(replace(Sequence,'.',''))) > 3
	and Sequence not in (select tb.Sequence from T_Solution tb)
order by Sequence
;
-- 更新年龄段表Sequence
update T_AgeGroup,T_Menu menu
set T_AgeGroup.Sequence = menu.Sequence
where 
	T_AgeGroup.Code = menu.AgeGroupName
	and (length(menu.Sequence) - length(replace(menu.Sequence,'.',''))) = 0
	;
";

/*  // 效率有问题，所以换成增临时表来处理
-- 目录-更新
update T_Menu,T_ImportData imp2
set
    T_Menu.Code = imp2.Content
    ,T_Menu.Name = concat(imp2.Sequence,' ',imp2.Content)
    ,T_Menu.AgeGroupName = imp2.AgeGroupName
where 1=1
    and T_Menu.Sequence = imp2.Sequence
    and imp2.GUID = '{0}'
    and (length(imp2.Sequence) - length(replace(imp2.Sequence,'.',''))) < 3
;
    */
            string strProcName = "hbh_proc_updateImportData";

            // 为了效率，改为存储过程
            //string updateText = string.Format(updateSql, strGuid);
            string updateText = string.Format("call {0}('{1}');", strProcName, strGuid);

            DatabaseAdapter sqlHelper2 = new DatabaseAdapter(this);
            sqlHelper2.DbHelper.ExecuteNonQuery(CommandType.Text, updateText);
        }
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
    public string[] GetExcelSheetName(string excelFullFile)
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
            Response.Write(string.Format("<script>alert('Excle表读取失败!{0}');</script>",ex.Message));
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
        catch(Exception ex)
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
