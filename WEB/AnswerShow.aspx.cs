using System;
using System.Collections.Generic;
// using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class QuestionShow : System.Web.UI.Page
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
                this.txtParamID.Text = strID;
                this.txtID.Text = strID;

                string strName = Request.QueryString["Name"];
                // this.txtParentMenuName.Text = strName;
                this.txtParamName.Text = strName;

                //string strAgeGroup = Request.QueryString["AgeGroup"];

                //LoadData();

                int pageTreeType = PubClass.GetInt(ExtendMethod.GetPageParam(this,"TreeType"), (int)MenuTreeTypeEnum.Answer);

                switch (pageTreeType)
                {
                    case (int)MenuTreeTypeEnum.Answer:
                        {
                            LoadData();
                        }
                        break;
                    case (int)MenuTreeTypeEnum.Question:
                        {
                            LoadQuestion();
                        }
                        break;
                    default:
                        {
                            LoadData();
                        }
                        break;
                }
            }
            else
            {
                //btnAddChild.Visible = false;
            }

            //txtText.Focus();
            txtIntro.Focus();
        }
    }

    private void LoadQuestion()
    {
        string id = txtID.Text.Trim();

        // 清空ID，防止ID查不到，错误更新；或者目录传过来的，也更新成问题ID
        txtID.Text = string.Empty;
        if (!PubClass.IsNull(id))
        {
            string where = string.Format(" question.ID={0} ", id);


            string selectCommand = string.Format("select question.* from T_Question question where {0}", where);
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

                if (row != null)
                {
                    txtQuestion.Text = row["ID"].ToString();
                    txtQuestionTitle.Text = row["Title"].ToString();
                    txtAgeGroupName.Text = row["AgeGroupName"].ToString();

                }
            }
        }
    }


    protected void btnSav_Click(object sender, EventArgs e)
    {
        if (PubClass.IsNull(txtText.Text))
        {
            Response.Write(string.Format("<script>alert('答案不可为空！');</script>", Request.Url.ToString()));
            txtText.Focus();
            return;
        }


        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);

        if (!PubClass.IsNull(this.txtID.Text))
        {
            // 修改选择目录
            if (this.txtID.Text.Equals(this.txtParamID.Text))
            {
                string sql = string.Format("update T_Solution set SText='{0}',Intro='{1}',Question='{2}',Memo='{3}',Answer='{4}' where ID={5}"
                    , txtText.Text.Trim(), txtIntro.Text.Trim(), this.txtQuestion.Text.Trim(), this.txtMemo.Text.Trim(), txtAnswer.Text.Trim()
                    , txtID.Text.Trim()
                    );

                int row = sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, sql);

                if (row > 0)
                {
                    //Response.Write("<script>alert('成功！');window.src='MenuAdd.aspx'</script>");
                    Response.Write(string.Format("<script>alert('成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='MenuTree.aspx?TreeType=2&Selected={1}';</script>"
                        , Request.Url.ToString(),  string.Format("{0}-{1}", ExtendMethod.GetPageParam(this,"TreeType"), this.txtParamID.Text)
                        ));
                }
            }
            else
            {
                Response.Write(string.Format("<script>alert('页面异常！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='MenuTree.aspx?TreeType=2&Selected={1}';</script>"
                    , Request.Url.ToString(),  string.Format("{0}-{1}", ExtendMethod.GetPageParam(this,"TreeType"), this.txtParamID.Text)
                    ));
                return;
            }
        }
        // 新增子目录
        else
        {
            // SText='{0}',Text='{1}',Question='{2}',Memo='{3}',Answer='{4}'

            //string sql = "insert T_Solution (Code,Name,Level,ParentMenu) VALUES('" + txtCode.Text.Trim() + "','" + txtName.Text.Trim() + "','" + txtLevel.Text.Trim() + "','" + txtParentMenuID.Text.Trim() + "' )";
            string sql = string.Format("insert T_Solution (SText,Intro,Question,Memo,Answer) VALUES('{0}','{1}','{2}','{3}','{4}' ) ;   select @@IDENTITY  "
                , txtText.Text.Trim(), txtIntro.Text.Trim(), txtQuestion.Text.Trim(), this.txtMemo.Text.Trim(), this.txtAnswer.Text.Trim()
                );

            object result = sqlHelper.DbHelper.ExecuteScalar(CommandType.Text, sql);

            long id = PubClass.GetLong(result);

            if (id > 0)
            {
                Response.Write(string.Format("<script>alert('成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='MenuTree.aspx?TreeType=2&Selected={1}';</script>"
                    , Request.Url.ToString()            //  , string.Format("AnswerShow.aspx?ID={0}&Code={1}&=Name{2}", id, Request.QueryString["Code"], Request.QueryString["Name"])
                    , string.Format("{0}-{1}", ExtendMethod.GetPageParam(this,"TreeType"), this.txtParamID.Text)
                    ));          //    Request.Url.ToString()
            }
        }
    }

    private void LoadData()
    {
        if (!PubClass.IsNull(this.txtID.Text))
        {
            string where = string.Format("answer.ID={0}", this.txtID.Text);


            string selectCommand = string.Format("SELECT answer.*,question.Code as QuestionCode,question.Title as QuestionTitle,menu.AgeGroupName FROM T_Solution answer left join T_Question question on answer.question = question.ID left join T_Menu menu on question.ParentMenu = menu.ID where {0}", where);
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

                if (row != null)
                {
                    txtText.Text = row["SText"].ToString();
                    txtAgeGroupName.Text = row["AgeGroupName"].ToString();
                    txtQuestion.Text = row["Question"].ToString();
                    txtQuestionTitle.Text = row["QuestionTitle"].ToString();

                    // txtText.Text = row["SText"].ToString();
                    txtAnswer.Text = row["Answer"].ToString();
                    txtMemo.Text = row["Memo"].ToString();
                    txtIntro.Text = row["Intro"].ToString();
                }
            }
        }
    }

    protected void btnAddQuestion_Click(object sender, EventArgs e)
    {
        // 问题，都是同级问题；所以目录、年龄段都不会变
        //this.txtParentMenuID.Text = this.txtParamID.Text;
        //this.txtParentMenuName.Text = this.txtParamName.Text;

        this.txtID.Text = string.Empty;

        this.txtText.Text = string.Empty;
        this.txtMemo.Text = string.Empty;
        this.txtIntro.Text = string.Empty;

        //this.txtText.Focus();
        txtIntro.Focus();
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
            string SqlStr = "delete from T_Solution where id=" + id;


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

                //Response.Write(string.Format("<script>alert('删除成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='MenuTree.aspx?TreeType=0&Selected={1}';</script>", "MenuAdd.aspx"
                //    , string.Format("{0}-{1}", ExtendMethod.GetPageParam(this,"TreeType"), this.txtParamID.Text)));
                    Response.Write(string.Format("<script>alert('删除成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='MenuTree.aspx?TreeType=2&Selected={1}';</script>"
                        , "AnswerShow.aspx",  string.Format("{0}-{1}", ExtendMethod.GetPageParam(this,"TreeType"), this.txtParamID.Text)
                    ));
            }
            catch (Exception ex)
            {
                Response.Write("数据库错误，错误原因：" + ex.Message);
                Response.End();
            }
        }
    }
}