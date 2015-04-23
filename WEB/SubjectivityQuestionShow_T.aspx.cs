using System;
using System.Collections.Generic;
// using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Common;
using System.Text;

public partial class SubjectivityQuestionShow_T : System.Web.UI.Page
{
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

            string strID = Request.QueryString["ID"];
            if (!PubClass.IsNull(strID))
            {
               
                this.txtID.Text = strID;

                LoadData();
                        
            }

            txtKeyWords.Focus();
        }
    }

    protected void btnSav_Click(object sender, EventArgs e)
    {
       

        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);

        if (!PubClass.IsNull(this.txtID.Text))
        {

            string sql = string.Format("update hbh_Sub_Qes_T set question_Title='{0}',aboutAgeBegin='{1}',aboutAgeEnd='{2}',keywords='{3}' ,aboutAge ='{4}' where ID={5}"
                    , txtTitle.Text.Trim(),this.txtAboutAgeBegin.Text.Trim(),this.txtAboutAgeEnd.Text.Trim(),txtKeyWords.Text.Trim(), txtAboutAgeBegin.Text.Trim()+"-"+txtAboutAgeEnd.Text.Trim(),txtID.Text.Trim()
                    );

            int row = sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, sql);

            if (row > 0)
            {
                Response.Write(string.Format("<script>alert('成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='SubjectivityQuestion_T.aspx?TreeType=1&Selected={1}';</script>"
                              , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), this.txtID.Text)
                              ));
            }

            else
            {
                Response.Write(string.Format("<script>alert('页面异常！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='SubjectivityQuestion_T.aspx?TreeType=1&Selected={1}';</script>"
                              , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), this.txtID.Text)
                              ));
                return;
            }
        }
        else 
        {

            string sql = string.Format("insert into hbh_Sub_Qes_T (question_title,keyWords,aboutAgeBegin,aboutAgeEnd,aboutAge) values('{0}','{1}','{2}','{3}','{4}');  select @@IDENTITY ; ",
         txtTitle.Text.Trim(),  txtKeyWords.Text.Trim(),      txtAboutAgeBegin.Text.Trim(),  txtAboutAgeEnd.Text.Trim(),   txtAboutAgeBegin.Text.Trim()+"-"+txtAboutAgeEnd.Text.Trim());

            object result = sqlHelper.DbHelper.ExecuteScalar(CommandType.Text, sql);

            long id = PubClass.GetLong(result);

            if (id > 0)
            {
                Response.Write(string.Format("<script>alert('成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='SubjectivityQuestion_T.aspx?TreeType=1&Selected={1}';</script>"
                              , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), id.ToString())
                              ));
            }
        
        }
        
    }

    private void LoadData()
    {
        string id = txtID.Text.Trim();

        // 清空ID，防止ID查不到，错误更新；或者目录传过来的，也更新成问题ID
        txtID.Text = string.Empty;
        if (!PubClass.IsNull(id))
        {
            string where = string.Format("id={0}", id);


            string selectCommand = string.Format("SELECT id,question_title,keyWords,aboutAgeBegin,aboutAgeEnd FROM HBH_Sub_qes_T where {0}", where);
            string conString = ConfigurationManager.AppSettings["SqlConnStr"];
            
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

                if (row != null)
                {
                    txtID.Text = row["ID"].ToString();
                    txtKeyWords.Text = row["KeyWords"].ToString();
                    //txtCode.Text = row["Code"].ToString();
                    txtTitle.Text = row["question_title"].ToString();
                    txtAboutAgeEnd.Text = row["aboutAgeEnd"].ToString();
                    txtAboutAgeBegin.Text = row["aboutAgeBegin"].ToString();
                }
            }
            else
            { 
                
            }
        }
    }
  
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.txtID.Text = string.Empty;

        this.txtAboutAgeBegin.Text = string.Empty;
        this.txtAboutAgeEnd.Text = string.Empty;

        this.txtKeyWords.Text = string.Empty;
        this.txtTitle.Text = string.Empty;

        this.txtTitle.Focus();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (!PubClass.IsNull(txtID.Text))
        {
            string id = txtID.Text;
            //string connStr = ConfigurationManager.AppSettings["SqlConnStr"];
            string SqlStr = "delete from hbh_Sub_Qes_T where id=" + id;


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

                Response.Write(string.Format("<script>alert('删除成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='SubjectivityQuestion_T.aspx?TreeType=0&Selected={1}';</script>", "SubjectivityQuestionShow_T.aspx"
                    , string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), this.txtID.Text)));
            }
            catch (Exception ex)
            {
                Response.Write("数据库错误，错误原因：" + ex.Message);
                Response.End();
            }
        }
    }
    private void BatchImportDatabase(DataSet ds)
    {
        try
        {
            if (ds != null
                && ds.Tables != null
                && ds.Tables.Count > 0
                )
            {
                string Qes1PutTime = "1";
                string Qes2PutTime = "5";
                Guid guid = Guid.NewGuid();
                string strGuid = guid.ToString();

                string strUserName = string.Empty;
                DLL.User loginUser = Session["dsUser"] as DLL.User;
                if (loginUser == null)
                {
                    strUserName = loginUser.Name;
                }

                string sql = "insert into hbh_t_importData (GUID,aboutAgeBegin,aboutAgeEnd,message_Title,message_content,messDate)values(?,?,?,?,?,now());";

                // 每个表导入一次
                foreach (DataTable table in ds.Tables)
                {
                    if (table != null
                        // Sheet名不可为空
                        && !PubClass.IsNull(table.TableName)
                        // Sheet名不可包含忽略字符
                        && !table.TableName.Contains("#")
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
                            string month = "";//记录上条记录的月份

                            string LastMonth = ""; //记录上条记录的月份
                            int QestingSequence = 1;//记录当前记录是本月的第几周推送
                            int inQty = 2;//存储几周推送一镒
                            for (int i = 0; i < curCount; i++)
                            {
                                DataRow row = table.Rows[preBatchIndex + i];
                                

                                TableTypeParameter[] curParamArr = new TableTypeParameter[5];

                                //当本记录的月分为空时表明单元格合并了取不到数据。那本记录的月分=上条记录的月份。
                                if (!string.IsNullOrEmpty(row[0].ToString().Trim()))
                                {
                                    month = row[0].ToString().Trim();
                                }

                                if (!string.IsNullOrEmpty(LastMonth))
                                {
                                    if (LastMonth.Equals(month))
                                    {

                                        QestingSequence += inQty;
                                    }
                                    else
                                    {
                                        QestingSequence = 1;
                                        LastMonth = month;
                                    }
                                }
                                else
                                    LastMonth = month;
                                //string sql = "insert into hbh_t_importMessageData (GUID,aboutAgeBegin,aboutAgeEnd,message_Title,message_content,messDate)values(?,?,?,?,?,now());";



                                int endDay = QestingSequence + 1;//时间段结束

                                curParamArr[0] = new TableTypeParameter("GUID", strGuid);
                                curParamArr[1] = new TableTypeParameter("aboutAgeBegin", strTableName + month + QestingSequence + "周");
                                curParamArr[2] = new TableTypeParameter("aboutAgeEnd", strTableName + month + endDay + "周");
                                string row_title = "";
                                if (string.IsNullOrEmpty(row[1].ToString().Trim()))
                                    row_title = "";
                                else
                                    row_title = row[1].ToString().Trim();
                                

                                string row_content = "";
                                if (string.IsNullOrEmpty(row[2].ToString().Trim()))
                                    row_content = "";
                                else
                                    row_content = row[2].ToString().Trim();
                              

                                curParamArr[3] = new TableTypeParameter("message_Title", row_title);
                                curParamArr[4] = new TableTypeParameter("message_content", row_content);


                                sqlParamArray[i] = curParamArr;
                                
                              
                            }

                            DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
                            sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, sql, sqlParamArray);
                        }
                    }

                }

                // ,concat(Content,'(',AgeGroupName,')')
                // ,(length(Sequence) - length(replace(Sequence,'.',''))) ,length(Sequence),length(replace(Sequence,'.',''))



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
                string strProcName = "hbh_proc_ImportSubQuestionData";

                // 为了效率，改为存储过程
                //string updateText = string.Format(updateSql, strGuid);
                string updateText = string.Format("call {0}('{1}');", strProcName, strGuid);

                DatabaseAdapter sqlHelper2 = new DatabaseAdapter(this);
                sqlHelper2.DbHelper.ExecuteNonQuery(CommandType.Text, updateText);
            }
        }
        catch (Exception ex)
        {
            Response.Write(string.Format("<script>alert('{0}')</script> ",ex.Message));
        }
    }

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

        string[] sheetNames = ExcelHelper.GetExcelSheetName(savePath);

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
        try
        {
            DataSet ds = ExcelHelper.ExcelSqlConnection(savePath, sheetNames);

            BatchImportDatabase(ds);


            Response.Write(string.Format("<script>alert('Excel导入成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='SubjectivityQuestion_T.aspx?TreeType=1&Selected={1}';</script>"
                              , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), this.txtID.Text)
                              ));
        }
        catch (Exception ex)
        {
            Response.Write(string.Format("<script>alert('{0}');</script>", ex.Message));
        }
    }
}