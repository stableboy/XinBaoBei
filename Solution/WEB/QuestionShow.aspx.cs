using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Common;

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
            if (!string.IsNullOrWhiteSpace(strID))
            {
                this.txtParamID.Text = strID;
                this.txtID.Text = strID;

                string strName = Request.QueryString["Name"];
                // this.txtParentMenuName.Text = strName;
                this.txtParamName.Text = strName;

                //string strAgeGroup = Request.QueryString["AgeGroup"];

                int pageTreeType = PubClass.GetInt(this.GetPageParam("TreeType"), (int)MenuTreeTypeEnum.Question);

                switch (pageTreeType)
                {
                    case (int)MenuTreeTypeEnum.Question:
                        {
                            LoadData();
                        }
                        break;
                    case (int)MenuTreeTypeEnum.Menu:
                        {
                            LoadMenu();
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

            txtKeyWords.Focus();
        }
    }

    private void LoadMenu()
    {
        string id = txtID.Text.Trim();

        // 清空ID，防止ID查不到，错误更新；或者目录传过来的，也更新成问题ID
        txtID.Text = string.Empty;
        if (!string.IsNullOrWhiteSpace(id))
        {
            string where = string.Format("menu.ID={0}", id);


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

                if (row != null)
                {
                    txtParentMenuID.Text = row["ID"].ToString();
                    txtParentMenuName.Text = row["Name"].ToString();
                    txtAgeGroup.Text = row["AgeGroup"].ToString();
                    txtAgeGroupName.Text = row["AgeGroupName"].ToString();

                }
            }
        }
    }


    protected void btnSav_Click(object sender, EventArgs e)
    {
        // txtParentMenuName
        if (string.IsNullOrWhiteSpace(txtParentMenuID.Text))
        {
            Response.Write(string.Format("<script>alert('上层目录不可为空！');</script>", Request.Url.ToString()));
            txtTitle.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtKeyWords.Text))
        {
            Response.Write(string.Format("<script>alert('关键字不可为空！');</script>", Request.Url.ToString()));
            txtKeyWords.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtTitle.Text))
        {
            Response.Write(string.Format("<script>alert('问题标题不可为空！');</script>", Request.Url.ToString()));
            txtTitle.Focus();
            return;
        }


        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);

        if (!string.IsNullOrWhiteSpace(this.txtID.Text))
        {
            // 修改选择目录
            if (this.txtID.Text.Equals(this.txtParamID.Text))
            {
                string sql = string.Format("update T_Question set KeyWords='{0}',Title='{1}',ParentMenu={2},AgeGroupName='{3}',Questioner='{4}',Description='{5}' where ID={6}"
                    , txtKeyWords.Text.Trim(), txtTitle.Text.Trim()
                    , CommonHelper.GetStringWithNull(this.txtParentMenuID.Text.Trim())
                    , txtAgeGroupName.Text.Trim(), txtQuestioner.Text.Trim(), txtDescription.Text.Trim()
                    , txtID.Text.Trim()
                    );

                int row = sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, sql);

                if (row > 0)
                {
                    //Response.Write("<script>alert('成功！');window.src='MenuAdd.aspx'</script>");
                    Response.Write(string.Format("<script>alert('成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='MenuTree.aspx?TreeType=1&Selected={1}';</script>"
                        , Request.Url.ToString(), string.Format("{0}-{1}", this.GetPageParam("TreeType"), this.txtParamID.Text)
                        ));
                }
            }
            else
            {
                Response.Write(string.Format("<script>alert('页面异常！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='MenuTree.aspx?TreeType=1&Selected={1}';</script>"
                    , Request.Url.ToString(), string.Format("{0}-{1}", this.GetPageParam("TreeType"), this.txtParamID.Text)
                    ));
                return;
            }
        }
        // 新增子目录
        else
        {
            // KeyWords='{0}',Text='{1}',ParentMenu='{2}',AgeGroupName='{3}',Questioner='{4}'

            //string sql = "insert T_Question (Code,Name,Level,ParentMenu) VALUES('" + txtCode.Text.Trim() + "','" + txtName.Text.Trim() + "','" + txtLevel.Text.Trim() + "','" + txtParentMenuID.Text.Trim() + "' )";
            string sql = string.Format("insert T_Question (KeyWords,Title,ParentMenu,AgeGroupName,Questioner,Description) VALUES('{0}','{1}',{2},'{3}','{4}','{5}' )"
                , txtKeyWords.Text.Trim(), txtTitle.Text.Trim()
                , CommonHelper.GetStringWithNull(this.txtParentMenuID.Text.Trim())
                , this.txtAgeGroupName.Text
                ,txtQuestioner.Text.Trim(),txtDescription.Text.Trim()
                );

            int row = sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, sql);

            if (row > 0)
            {
                Response.Write(string.Format("<script>alert('成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='MenuTree.aspx?TreeType=1&Selected={1}';</script>"
                    , Request.Url.ToString(), string.Format("{0}-{1}", this.GetPageParam("TreeType"), this.txtParamID.Text)
                    ));
            }
        }
    }

    private void LoadData()
    {
        string id = txtID.Text.Trim();

        // 清空ID，防止ID查不到，错误更新；或者目录传过来的，也更新成问题ID
        txtID.Text = string.Empty;
        if (!string.IsNullOrWhiteSpace(id))
        {
            string where = string.Format("question.ID={0}", id);


            string selectCommand = string.Format("SELECT question.*,parentMenu.Code as ParentCode,parentMenu.Name as ParentName FROM T_Question question left join T_Menu parentMenu on question.ParentMenu = parentMenu.ID where {0}", where);
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
                    txtParentMenuID.Text = row["ParentMenu"].ToString();
                    txtParentMenuName.Text = row["ParentName"].ToString();
                    txtAgeGroup.Text = row["AgeGroup"].ToString();
                    txtAgeGroupName.Text = row["AgeGroupName"].ToString();

                    txtTitle.Text = row["Title"].ToString();
                    txtDescription.Text = row["Description"].ToString();
                    txtQuestioner.Text = row["Questioner"].ToString();
                }
            }
            else
            { 
                
            }
        }
    }

    protected void btnAddQuestion_Click(object sender, EventArgs e)
    {
        // 问题，都是同级问题；所以目录、年龄段都不会变
        //this.txtParentMenuID.Text = this.txtParamID.Text;
        //this.txtParentMenuName.Text = this.txtParamName.Text;

        this.txtID.Text = string.Empty;

        this.txtKeyWords.Text = string.Empty;
        this.txtTitle.Text = string.Empty;
        this.txtDescription.Text = string.Empty;
        // this.txtQuestioner.Text = 

        this.txtKeyWords.Focus();
    }

    protected void btnAnswer_Click(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtID.Text))
        {
            string id = txtID.Text;
            //string connStr = ConfigurationManager.AppSettings["SqlConnStr"];
            string SqlStr = "delete from T_Question where id=" + id;


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
                //    , string.Format("{0}-{1}", this.GetPageParam("TreeType"), this.txtParamID.Text)));
                Response.Write(string.Format("<script>alert('删除成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='MenuTree.aspx?TreeType=1&Selected={1}';</script>"
                    , "QuestionShow.aspx", string.Format("{0}-{1}", this.GetPageParam("TreeType"), this.txtParamID.Text)
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