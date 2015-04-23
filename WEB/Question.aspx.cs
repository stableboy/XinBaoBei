using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
// using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemSql;

public partial class Question : System.Web.UI.Page
{
    //sql_str sqlInsert = new sql_str();
    protected void Page_Load(object sender, EventArgs e)
    {
        // 强制检查用户是否有效，失效重登陆
        DLL.User loginUser = Session["dsUser"] as DLL.User;
        if (loginUser == null)
        {
            Response.Redirect(string.Format("Login.aspx?Redirect={0}", Request.Url.ToString()));
            return;
        }

        DLL.User user = Session["dsUser"] as DLL.User;

        int a = user.Power;

        if (a < 2) { Response.Redirect("Nothing.aspx"); }
    }
    protected void btnSav_Click(object sender, EventArgs e)
    {


        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);

             DateTime.Now.ToString();

             string Code =  GenerateReceiveNO();

             DLL.User user = Session["dsUser"] as DLL.User;

             if (string.IsNullOrEmpty(txtQu.Text))
             {
                 txtQu.Text = user.Name;
             }

             string sql = "insert T_Question (Code,AgeGroupName,KeyWords,Text,Questioner,TheDate) VALUES('" + Code + "','" + ddlAgeGroup.SelectedValue + "','" + txtKeyWords.Text.Trim() + "','" + txtText.Text.Trim() + "','" + txtQu.Text.Trim() + "','" + DateTime.Now.ToString() + "' )";

            int row = sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, sql);

            if (row > 0)
            {

                Response.Write("<script>alert('成功！');window.location.href='Question.aspx'</script>");

            }

        }


    private string GenerateReceiveNO()// 查询数据库最大记录数生成最大编号
    {
        //String strConn = System.Configuration.ConfigurationSettings.AppSettings["SqlConnStr"];
        DateTime dtNow = DateTime.Now;
        string strNO = "";
        string strNO1 = dtNow.Year.ToString() + dtNow.Month.ToString("00") + dtNow.Day.ToString("00");
        string strSQL = "Select MAXID  From [S_MaxId] Where MaxId like '" + strNO1 + "%'";
        DataSet ds = new DataSet(); ;
        //ds = sqlInsert.GetDataSet(strSQL);

        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
        sqlHelper.DbHelper.Fill(ds, CommandType.Text, strSQL, null);


        if (ds.Tables[0].Rows.Count >= 1)
        {
            string iMaxId = ds.Tables[0].Rows[0]["MAXID"].ToString();
            int Id = Convert.ToInt32(iMaxId.Substring(8)) + 1;
            //TxtPCName.Text = iMaxId + "|" + Id.ToString() + "|" + (Id <= 999).ToString();
            strNO = dtNow.Year.ToString() + dtNow.Month.ToString("00") + dtNow.Day.ToString("00") + ((Id <= 999) ? Id.ToString("000") : Id.ToString());
            //sqlInsert.SQLExecuteNonQuery("update [S_MaxId] set MaxId='" + strNO + "' where  MaxId like  '" + iMaxId + "'");
            string sql1 = "update [S_MaxId] set MaxId='" + strNO + "' where  MaxId like  '" + iMaxId + "'";
            int isSuccess = ExecuteSql(sql1);
        }
        else
        {
            strNO = dtNow.Year.ToString() + dtNow.Month.ToString("00") + dtNow.Day.ToString("00") + "001";
            //sqlInsert.SQLExecuteNonQuery("insert into  [S_MaxId](MaxId) values ('" + strNO + "')");
            string sql = "insert into  [S_MaxId](MaxId) values ('" + strNO + "')";
            int isSuccess = ExecuteSql(sql);
        }

        return strNO;
    }


    #region 执行SQL语句，返回影响的记录数

    /// <summary>
    /// 执行SQL语句，返回影响的记录数
    /// </summary>
    /// <param name="SQLString">SQL语句</param>
    /// <returns>影响的记录数</returns>
    public int ExecuteSql(string SQLString, params TableTypeParameter[] cmdParms)
    {
        //String strConn = System.Configuration.ConfigurationSettings.AppSettings["SqlConnStr"];

        //using (SqlConnection connection = new SqlConnection(strConn))
        //{
        //    using (SqlCommand cmd = new SqlCommand())
        //    {
        //        try
        //        {
        //            PrepareCommand(cmd, connection, null, SQLString, cmdParms);
        //            int rows = cmd.ExecuteNonQuery();
        //            cmd.Parameters.Clear();
        //            return rows;
        //        }
        //        catch (System.Data.SqlClient.SqlException e)
        //        {
        //            throw e;
        //        }
        //    }
        //}

        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
        
        return sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, SQLString, cmdParms);
    }



    //private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
    //{
    //    if (conn.State != ConnectionState.Open)
    //        conn.Open();
    //    cmd.Connection = conn;
    //    cmd.CommandText = cmdText;
    //    if (trans != null)
    //        cmd.Transaction = trans;
    //    cmd.CommandType = CommandType.Text;//cmdType;
    //    if (cmdParms != null)
    //    {


    //        foreach (SqlParameter parameter in cmdParms)
    //        {
    //            if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
    //                (parameter.Value == null))
    //            {
    //                parameter.Value = DBNull.Value;
    //            }
    //            cmd.Parameters.Add(parameter);
    //        }
    //    }
    //}
    #endregion

    



    }
