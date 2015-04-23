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
using System.Data.OleDb;
using System.Text;

public partial class MessageShow : System.Web.UI.Page
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
        txtPosterID.Text = loginUser.ID.ToString();
        if (!this.IsPostBack)
        {

            string strID = Request.QueryString["ID"];
            if (!PubClass.IsNull(strID))
            {

                this.txtID.Text = strID;

                LoadData();

            }

        }
        //SetDs();
        //if (!this.IsPostBack)
        //{
        //    LoadAppUser();
        //}
    }

    DataSet ds = new DataSet();
    private DataSet SetDs()
    {
        string selectCommand = "select id,account,name from judge_user";
        string conString = ConfigurationManager.AppSettings["SqlConnStr"];

        MySqlHelper mysqlHelper = new MySqlHelper(SetOfBookType.NuoHeTest);



        mysqlHelper.Fill(ds, CommandType.Text, selectCommand);

        return ds;
    }
    #region 暂时没有用了
    //private void LoadAppUser()
    //{
    //    SetDs();
    //        if (ds != null
    //               && ds.Tables != null
    //               && ds.Tables.Count > 0
    //               )
    //        {
    //            DataTable dt = ds.Tables[0];
    //            if (dt != null && dt.Rows != null && dt.Rows.Count > 0  )
    //            {
    //                List<string> dataSource = new List<string>();
    //                foreach (DataRow dr in dt.Rows)
    //                {
    //                    dataSource.Add(dr["account"].ToString());
    //                    ddlUsers.Items.Add(dr["account"].ToString());
    //                }
                   
    //                ddlUsers.DataSource = dataSource;
                    
    //            }
    //        }
       
    //}

 //protected void ddlUser_ChangeClick(object sender, EventArgs e)
    //{ 
    //     if (ds != null
    //               && ds.Tables != null
    //               && ds.Tables.Count > 0
    //               )
    //        {
    //            DataTable dt = ds.Tables[0];
    //            if (dt != null && dt.Rows != null && dt.Rows.Count > 0  )
    //            {
    //               foreach(DataRow dr in dt.Rows)
    //               {
    //                   if(dr["account"].ToString().Equals( ddlUsers.SelectedValue))
    //                   {
    //                       txtUserCode.Text = dr["account"].ToString();
    //                       txtUserID.Text = dr["id"].ToString();
    //                       txtUserName.Text = dr["name"].ToString();
    //                   }
    //               }
    //            }
    //        }
    //}

    #endregion

   
    protected void btnSav_Click(object sender, EventArgs e)
    {
        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);

        if (!PubClass.IsNull(this.txtID.Text))
        {

            string sql = string.Format("update hbh_message set message_Content='{0}',message_Title='{1}',poster_id='{2}',subhead='{3}',aboutAgeBegin='{4}',aboutAgeEnd='{5}' where ID={6}"
                    , this.txtMessageContent.Text.Trim(), this.txtMessageTitle.Text.Trim(), this.txtPosterID.Text.Trim(),this.txtSubHead.Text.Trim(),this.txtAboutAgeBegin.Text.Trim(),this.txtAboutAgeEnd.Text.Trim(), txtID.Text.Trim()
                    );

            int row = sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, sql);

            if (row > 0)
            {


                Response.Write(string.Format("<script>alert('成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='Message.aspx?TreeType=1&Selected={1}';</script>"
                                  , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), this.txtID.Text)
                                  ));
            }

            else
            {

                Response.Write(string.Format("<script>alert('页面异常！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='Message.aspx?TreeType=1&Selected={1}';</script>"
                                  , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), this.txtID.Text)
                                  ));
                return;
            }
        }
        else
        {
          
            string sql = string.Format("insert hbh_message (message_title,message_Content,messDate,user_id,poster_id,subhead,aboutAgeBegin,aboutAgeEnd) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}' ) ;   select @@IDENTITY  "
                   , txtMessageTitle.Text.Trim(), txtMessageContent.Text.Trim(), DateTime.Now, txtUserID.Text.Trim(), txtPosterID.Text.Trim(), txtSubHead.Text.Trim(), txtAboutAgeBegin.Text.Trim(), txtAboutAgeEnd.Text.Trim()
                   );

            object result = sqlHelper.DbHelper.ExecuteScalar(CommandType.Text, sql);

            long id = PubClass.GetLong(result);

            if (id > 0)
            {
                Response.Write(string.Format("<script>alert('成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='Message.aspx?TreeType=1&Selected={1}';</script>"
                               ,"MessageShow.aspx", string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), this.txtID.Text)
                               ));
            }

        }
    }
    //后面查看消息用
    private void LoadData()
    {
        string id = txtID.Text.Trim();

        // 清空ID，防止ID查不到，错误更新；或者目录传过来的，也更新成问题ID
        txtID.Text = string.Empty;
        if (!PubClass.IsNull(id))
        {
            //推送消息者只能看到自己推送的消息
            string where = string.Format("id={0}", id);


            string selectCommand = string.Format("SELECT id,message_title,message_content,messDate,user_id,poster_id,isRead,subhead,aboutAgeBegin,aboutAgeEnd FROM hbh_message where {0}", where);
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
                    txtID.Text = row["id"].ToString();
                    txtPosterID.Text = row["poster_id"].ToString();
                    txtMessageTitle.Text = row["message_title"].ToString();
                    //txtCode.Text = row["Code"].ToString();
                    txtMessageContent.Text = row["message_content"].ToString();
                    txtUserID.Text = row["user_id"].ToString();
                    txtSubHead.Text = row["subhead"].ToString();
                    txtAboutAgeBegin.Text = row["aboutAgeBegin"].ToString();
                    txtAboutAgeEnd.Text = row["aboutAgeEnd"].ToString();
                   
                }
            }
            else
            { 
                
            }
        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        
        this.txtAboutAgeBegin.Text = string.Empty;
        this.txtAboutAgeEnd.Text = string.Empty;
        this.txtMessageContent.Text = string.Empty;
        this.txtMessageTitle.Text = string.Empty;
        this.txtPosterID.Text = string.Empty;
        this.txtSubHead.Text = string.Empty;

        this.txtMessageTitle.Focus();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (!PubClass.IsNull(txtID.Text))
        {
            string id = txtID.Text;
            //string connStr = ConfigurationManager.AppSettings["SqlConnStr"];
            string SqlStr = "delete from hbh_Message where id=" + id;


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

                Response.Write(string.Format("<script>alert('删除成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='Message.aspx?TreeType=0&Selected={1}';</script>", "MessageShow.aspx"
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
                            string week = "";//记录上条记录的周
                            string MW = ""; //记录上条记录的月份+周
                            int QestingSequence = 1;//记录当前记录是本周的第几天推送消息
                            int inDay = 4;//存储天数
                            for (int i = 0; i < curCount; i++)
                            {
                                DataRow row = table.Rows[preBatchIndex + i];

                                TableTypeParameter[] curParamArr = new TableTypeParameter[5];

                                //当本记录的月分为空时表明单元格合并了取不到数据。那本记录的月分=上条记录的月份。
                                if (!string.IsNullOrEmpty(row[0].ToString()))
                                {
                                    month = row[0].ToString();
                                }
                                //当本记录的周为空时表明单元格合并了取不到数据。那本记录的周=上条记录的周。
                                if (!string.IsNullOrEmpty(row[1].ToString()))
                                {
                                    week = row[1].ToString();
                                    if (!string.IsNullOrEmpty(week))
                                    {
                                        if (week.Equals("第一周") || week.Equals("第1周"))
                                        {
                                            week = "1周";
                                        }
                                        else if (week.Equals("第二周") || week.Equals("第2周"))
                                        {
                                            week = "2周";
                                        }
                                        else if (week.Equals("第三周") || week.Equals("第3周"))
                                        {
                                            week = "3周";
                                        }
                                        else if (week.Equals("第四周") || week.Equals("第4周"))
                                        {
                                            week = "4周";
                                        }

                                    }
                                }
                                if (!string.IsNullOrEmpty(MW))
                                {
                                    if (MW.Equals(month + week))
                                    {

                                        QestingSequence += inDay;
                                    }
                                    else
                                    {
                                        QestingSequence = 1;
                                        MW = month + week;
                                    }
                                }
                                else
                                    MW = month + week;
                                //string sql = "insert into hbh_t_importMessageData (GUID,aboutAgeBegin,aboutAgeEnd,message_Title,message_content,messDate)values(?,?,?,?,?,now());";



                                int endDay = 0;//时间段结束


                                if (QestingSequence == 1)
                                {
                                    endDay = QestingSequence + 3;
                                }
                                else
                                {
                                    endDay = QestingSequence + 2;
                                }

                                curParamArr[0] = new TableTypeParameter("GUID", strGuid);
                                curParamArr[1] = new TableTypeParameter("aboutAgeBegin", strTableName + month + week + "第" + QestingSequence + "天");
                                curParamArr[2] = new TableTypeParameter("aboutAgeEnd", strTableName + month + week + "第" + endDay + "天");

                                string row_title = "";
                                if (string.IsNullOrEmpty(row[2].ToString().Trim()))
                                    row_title = "";
                                else
                                    row_title = row[2].ToString().Trim();
                                  
                                string row_content = "";
                                if (string.IsNullOrEmpty(row[3].ToString().Trim()))
                                    row_content = "";
                                else
                                    row_content = row[3].ToString().Trim();


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

               
                string strProcName = "hbh_proc_ImportMessageData";

                // 为了效率，改为存储过程
                //string updateText = string.Format(updateSql, strGuid);
                string updateText = string.Format("call {0}('{1}');", strProcName, strGuid);

                DatabaseAdapter sqlHelper2 = new DatabaseAdapter(this);
                sqlHelper2.DbHelper.ExecuteNonQuery(CommandType.Text, updateText);
            }
        }
        catch (Exception ex)
        {
            Response.Write(string.Format("<script>alert('{0}')</script> ", ex.Message));
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


            Response.Write(string.Format("<script>alert('Excel表导入成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='Message.aspx?TreeType=1&Selected={1}';</script>"
                                   , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), this.txtID.Text)
                                   ));

        }
        catch (Exception ex)
        { 
            Response.Write(string.Format("<script>alert('{0}');</script>",ex.Message));
        }
    }

}