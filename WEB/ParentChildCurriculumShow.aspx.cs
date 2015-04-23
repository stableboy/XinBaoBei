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

public partial class ParentChildCurriculumShow : System.Web.UI.Page
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

        }
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

 
    protected void btnSav_Click(object sender, EventArgs e)
    {


        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);

        if (!PubClass.IsNull(this.txtID.Text))
        {

            string sql = string.Format("update HBH_ParentChildCurriculum set stage='{0}' ,aboutAge='{1}',content='{2}' where ID={3}"
                    , txtStage.Text.Trim(),txtAboutAge.Text.Trim(),txtContent.Text.Trim(), txtID.Text.Trim()
                    );

            int row = sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, sql);

            if (row > 0)
            {


                Response.Write(string.Format("<script>alert('成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='ParentChildCurriculum.aspx?TreeType=1&Selected={1}';</script>"
                                  , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), this.txtID.Text)
                                  ));
            }

            else
            {

                Response.Write(string.Format("<script>alert('页面异常！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='ParentChildCurriculum.aspx?TreeType=1&Selected={1}';</script>"
                                  , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), this.txtID.Text)
                                  ));
                return;
            }
        }
        else
        {

            string sql = string.Format("insert into hbh_parentChildCurriculum (stage,content,aboutAge) values('{0}','{1}','{2}');  select @@IDENTITY ; ",
         txtStage.Text.Trim(), txtContent.Text.Trim(), txtAboutAge.Text.Trim());

            object result = sqlHelper.DbHelper.ExecuteScalar(CommandType.Text, sql);

            long id = PubClass.GetLong(result);

            if (id > 0)
            {


                Response.Write(string.Format("<script>alert('成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='ParentChildCurriculum.aspx?TreeType=1&Selected={1}';</script>"
                                  , "ParentChildCurriculumShow.aspx", string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), id.ToString())
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


            string selectCommand = string.Format("SELECT id,content,stage,aboutAge FROM hbh_ParentChildCurriculum where {0} order by sequence", where);
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
                    txtAboutAge.Text = row["aboutAge"].ToString();
                    txtContent.Text = row["content"].ToString();
                    txtStage.Text = row["stage"].ToString();
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

        this.txtAboutAge.Text = string.Empty;
        this.txtContent.Text = string.Empty;

        this.txtStage.Text = string.Empty;

        this.txtStage.Focus();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (!PubClass.IsNull(txtID.Text))
        {
            string id = txtID.Text;
            //string connStr = ConfigurationManager.AppSettings["SqlConnStr"];
            string SqlStr = "delete from hbh_ParentChildCurriculum where id=" + id;


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

                Response.Write(string.Format("<script>alert('删除成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='ParentChildCurriculum.aspx?TreeType=0&Selected={1}';</script>", "ParentChildCurriculumShow.aspx"
                    , string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), this.txtID.Text)));
            }
            catch (Exception ex)
            {
                Response.Write("数据库错误，错误原因：" + ex.Message);
                Response.End();
            }
        }
    }
}