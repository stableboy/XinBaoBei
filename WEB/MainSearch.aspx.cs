using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
// using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class MainSearch : System.Web.UI.Page
{
    private const char Const_KeySplitFlag = ' ';
    private const int Const_PageSize = 5;

    protected void Page_Load(object sender, EventArgs e)
    {
        // 强制检查用户是否有效，失效重登陆
        //DLL.User loginUser = Session["dsUser"] as DLL.User;
        //if (loginUser == null)
        //{
        //    Response.Redirect(string.Format("Login.aspx?Redirect={0}", Request.Url.ToString()));
        //    return;
        //}

        // GridView.Attributes.Add("style", "table-layout:fixed");

        if (!IsPostBack)
        {
            pagerQuestion.PageSize = Const_PageSize;
            pagerQuestion.RecordCount = 0;
            //pagerQuestion2.PageSize = Const_PageSize;
            //pagerQuestion2.RecordCount = 0;

            UIHelper.LoadAgeGroup(this, ddlAgeGroup);

            string userGrade = ExtendMethod.GetPageParam(this,"UserGrade");

            //// vip用户，可以展开
            //if (userGrade == "2")
            //{ 
            //    // 可以展开     
            //}
            txtUserGrade.Text = userGrade;

            //this.txtKeyWords.Focus();
            this.txtQuestionText.Focus();

            //dpQuestion.PageSize = Const_PageSize;
        }
    }

    //private void DatasBind()
    //{
    //    DataSet ds = GetEntities();
    //    GridView.DataSource = ds;
    //    //this.AspNetPager1.RecordCount = ds.Tables[0].Rows.Count;
    //    PagedDataSource pds = new PagedDataSource();
    //    pds.AllowPaging = true;
    //    //pds.PageSize = AspNetPager1.PageSize;
    //    // pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
    //    pds.DataSource = ds.Tables[0].DefaultView;

    //    //lvQuestion.DataSource = ds;

    //}

    private DataSet GetEntities(int pageSize,int pageIndex)
    {
        if (pagerQuestion.CurrentPageIndex != pageIndex)
        {
            pagerQuestion.CurrentPageIndex = pageIndex;
        }
        //if (pagerQuestion2.CurrentPageIndex != pageIndex)
        //{
        //    pagerQuestion2.CurrentPageIndex = pageIndex;
        //}

        DataSet ds = new DataSet();

        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);

        string a = " 1=1 ";

        string strAge = ddlAgeGroup.SelectedValue.ToString();

        //string KeyWords = txtKeyWords.Text.ToString();
        string strKeyWords = txtQuestionText.Text.ToString();
        string questionText = txtQuestionText.Text.ToString();

        a += " and a.AgeGroupName = '" + strAge + "' ";

        ////a += " and (a.KeyWords like  '%" + KeyWords + "%'" + " or a.Text like  '%" + KeyWords + "%')";
        //if (!PubClass.IsNull(KeyWords))
        //{
        //    string[] keys = KeyWords.Split(Const_KeySplitFlag);

        //    if (keys.Length > 0)
        //    {
        //        a += " and (1=0 ";

        //        foreach (string strKey in keys)
        //        {
        //            if (!PubClass.IsNull(strKey))
        //            {
        //                a += " or a.KeyWords like  '%" + strKey + "%'";
        //            }
        //        }

        //        a += ")";
        //    }
        //}

        //// replace(a.Title,'宝宝','<font color=''#FF0000''>宝宝</font>') as Title
        //if (!PubClass.IsNull(questionText))
        //{
        //    string[] keys = questionText.Split(Const_KeySplitFlag);

        //    if (keys.Length > 0)
        //    {
        //        a += " and (1=0 ";

        //        foreach (string strKey in keys)
        //        {
        //            if (!PubClass.IsNull(strKey))
        //            {
        //                a += " or a.Title like  '%" + strKey + "%'";

        ////                sbTitle.Append(string.Format("replace(a.Title,'{0}','<font color=''#FF0000''>{0}</font>')", strKey));
        //            }
        //        }

        //        a += ")";
        //    }
        //}


        // replace(a.Title,'宝宝','<font color=''#FF0000''>宝宝</font>') as Title
        //StringBuilder sbTitle = new StringBuilder();
        string strKeywordTitle = "a.Title";
        // ifnull(a.KeyWords,'') as KeyWords
        string strKeywordKeyword = "ifnull(a.KeyWords,'')";

        List<string> lstReplaceKey = new List<string>();
        if (!PubClass.IsNull(questionText))
        {
            string[]  keys = questionText.Split(Const_KeySplitFlag);

            if (keys.Length > 0)
            {
                a += " and (1=0 ";

                foreach (string strKey in keys)
                {
                    if (!PubClass.IsNull(strKey))
                    {
                        a += " or a.KeyWords like  '%" + strKey + "%'";
                        a += " or a.Title like  '%" + strKey + "%'";

                        strKeywordTitle = strKeywordTitle.Replace("a.Title"
                            , string.Format("replace(a.Title,'{0}','<font color=''#FF0000''>{0}</font>')", strKey)
                            );
                        strKeywordKeyword = strKeywordKeyword.Replace("ifnull(a.KeyWords,'')"
                            , string.Format("replace(ifnull(a.KeyWords,''),'{0}','<font color=''#FF0000''>{0}</font>')", strKey)
                            );

                        if (!lstReplaceKey.Contains(strKey))
                        {
                            lstReplaceKey.Add(strKey);
                        }
                    }
                }

                a += ")";
            }
        }

        //if (sbTitle.Length == 0)
        //{
        //    sbTitle.Append("a.Title");
        //}

        //string sql = string.Format("select a.ID,a.Code,a.AgeGroupName,a.Text,a.KeyWords,a.Questioner,a.Memo,b.AnCode,b.QuCode,b.SText,b.Answer,b.Memo from T_Question a inner join T_Solution b on a.id=b.Question where {0} ", a);
//        string sql = string.Format(@"
//select a.ID QuestionID,a.Code,a.AgeGroupName,a.Title,a.KeyWords,a.Questioner,a.Memo,-1 AnswerID,'' AnCode,'' QuCode,'' as Intro,'' SText,'' Answer,'' Memo 
//from T_Question a
//where {0}
//union all 
//select a.ID QuestionID,'' Code,'' AgeGroupName,'' Title,'' KeyWords,'' Questioner,'' Memo,b.ID AnswerID,b.AnCode,b.QuCode,b.Intro,b.SText,b.Answer,b.Memo 
//from T_Question a inner join T_Solution b on a.id=b.Question
//where {0}
//order by QuestionID,AnswerID
//", a);
        // replace(a.Title,'宝宝','<font color=''#FF0000''>宝宝</font>') as Title
//        string sql = string.Format(@"
//select a.ID QuestionID,a.Code,a.AgeGroupName,{1} as Title,{2} as KeyWords,a.Questioner,a.Memo,b.ID AnswerID,b.AnCode,b.QuCode,case when ifnull(Intro,'') = '' then concat(left(replace(b.SText,'\r\n','<br><br>'),100),'...') else replace(Intro,'\r\n','<br><br>') end as Intro,b.SText,b.Answer,b.Memo,replace(replace(replace(b.SText,'\r\n','<br><br>'),char(10),'<br>'),char(13),'<br>') STextDiv 
//from T_Question a inner join T_Solution b on a.id=b.Question
//where {0}
//order by QuestionID,AnswerID
//", a, strKeywordTitle, strKeywordKeyword);
        //replace(b.SText,'\r\n','<br><br>') STextDiv

        string sql = string.Format("call {0}('{1}','{2}',{3},{4})", "hbh_proc_selectByKeywords"
                        , strAge, strKeyWords , pageSize ,pageIndex
                        );

        sqlHelper.DbHelper.Fill(ds, CommandType.Text, sql, null);

        // 对关键字进行颜色处理
        if (lstReplaceKey != null
            && lstReplaceKey.Count > 0
            )
        {
            if (ds != null
                && ds.Tables != null
                && ds.Tables.Count > 0
                )
            {
                DataTable table = ds.Tables[0];

                List<string> lstReplaceFields = new List<string>();
                lstReplaceFields.Add("Title");
                //lstReplaceFields.Add("KeyWords");
                //lstReplaceFields.Add("Intro");
                //lstReplaceFields.Add("SText");
                //lstReplaceFields.Add("STextDiv");

                //string replaceField = "Title";
                if (table != null
                    && table.Columns != null
                    //&& table.Columns.Contains(replaceField)
                    && lstReplaceFields.Count > 0
                    && table.Rows != null
                    && table.Rows.Count > 0
                    )
                {
                    foreach (DataRow row in table.Rows)
                    {
                        foreach (string replaceField in lstReplaceFields)
                        {
                            if (row != null
                                && table.Columns.Contains(replaceField)
                                && row[replaceField] != null
                                )
                            {
                                foreach (string key in lstReplaceKey)
                                {
                                    string replacedString = string.Format("<font color='#FF0000'>{0}</font>", key);
                                    row[replaceField] = row[replaceField].ToString().Replace(key, replacedString);
                                }
                            }
                        }
                    }
                }
            }
        }

        return ds;
    }

    public DataTable query(string strsql)
    {

        //string SqlConnStr = ConfigurationManager.ConnectionStrings["SqlConnStr"].ConnectionString;

        //SqlConnection conn = new SqlConnection(SqlConnStr);

        //SqlDataAdapter sda = new SqlDataAdapter(strsql, conn);

        //DataTable dt = new DataTable();

        //sda.Fill(dt);

        DataTable dt = new DataTable();
        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
        sqlHelper.DbHelper.Fill(dt, CommandType.Text, strsql, null);

        return dt;

    }

    //protected void btnSea_Click(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();

    //    GridViewBind();

    //    this.txtKeyWords.Focus();
    //}

    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // GridView.PageIndex = e.NewPageIndex;
        GridViewBind();
    }

    private void GridViewBind()
    {
        //string connStr = ConfigurationManager.AppSettings["SqlConnStr"];

        string SqlStr = "select a.ID,a.Code,a.AgeGroupName,a.Title,a.KeyWords,a.Questioner,a.Memo,b.AnCode,b.QuCode,SText as bt,b.Answer,b.Memo from T_Question a inner join T_Solution b on a.id=b.AnCode";

        DataSet ds = new DataSet();

        try
        {
            //SqlConnection conn = new SqlConnection(connStr);
            //if (conn.State.ToString() == "Closed") conn.Open();
            //SqlDataAdapter da = new SqlDataAdapter(SqlStr, conn);
            //da.Fill(ds,SqlStr);
            //if (conn.State.ToString() == "Open") conn.Close();

            DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
            sqlHelper.DbHelper.Fill(ds, CommandType.Text, SqlStr, null);

            // GridView.DataSource = ds.Tables[0].DefaultView;
            // GridView.DataBind();

            lvQuestion.DataSource = ds.Tables[0].DefaultView;
            lvQuestion.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write("数据库错误，错误原因：" + ex.Message);
            Response.End();
        }
    }

    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ////判断是否数据行
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    //找到GridView控件的第12个单元格中的 按钮 并给他添加 按钮事件 点击删除时提示 是否删除 
        //    ((Button)e.Row.Cells[2].Controls[0]).Attributes.Add("onclick", string.Format("javascript:return confirm('您是要删除" + e.Row.Cells[2].Text + " 么')"));
        //}

        //判断是否数据行
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            UIHelper.SetDeleteConfirm(e, string.Format(" {0} 的答案", e.Row.Cells[5].Text));
        }
    }

    protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        // GridView.EditIndex = e.NewEditIndex;
        GridViewBind();
    }
    protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        // GridView.EditIndex = -1;
        GridViewBind();
    }

    //protected void GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    string id = GridView.DataKeys[e.RowIndex].Values[0].ToString();
    //    string keywords = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtKeyWords")).Text;
    //    string text = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtText")).Text;
    //    string que = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtQuestioner")).Text;
    //    string stext = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtSText")).Text;
    //    string answer = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtAnswer")).Text;
    //    string SqlStr = "update T_Solution set Answer='" + answer + "',stext ='" + stext + "' from T_Question a inner join T_Solution b on a.id=b.AnCode where b.id=" + id;

    //    try
    //    {
    //        //string connStr = ConfigurationManager.AppSettings["SqlConnStr"];
    //        //SqlConnection conn = new SqlConnection(connStr);
    //        //if (conn.State.ToString() == "Closed") conn.Open();
    //        //SqlCommand comm = new SqlCommand(SqlStr, conn);
    //        //comm.ExecuteNonQuery();
    //        //comm.Dispose();
    //        //if (conn.State.ToString() == "Open") conn.Close();

    //        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
    //        sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, SqlStr);

    //        GridView.EditIndex = -1;
    //        GridViewBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write("数据库错误，错误原因：" + ex.Message);
    //        Response.End();
    //    }
    //}


    //protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{

    //    GridView.Rows[e.RowIndex].Attributes.Add("onclick", "return confirm('你确认要删除吗?')");

    //    string id = GridView.DataKeys[e.RowIndex].Values[0].ToString();
    //    string SqlStr = "delete from T_Question a inner join T_Solution b on a.id=b.AnCode where b.id=" + id;


    //    try
    //    {
    //        //string connStr = ConfigurationManager.AppSettings["SqlConnStr"];
    //        //    SqlConnection conn = new SqlConnection(connStr);
    //        //    if (conn.State.ToString() == "Closed") conn.Open();
    //        //    SqlCommand comm = new SqlCommand(SqlStr, conn);
    //        //    comm.ExecuteNonQuery();
    //        //    comm.Dispose();
    //        //    if (conn.State.ToString() == "Open") conn.Close();

    //        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
    //        sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, SqlStr);

    //        GridView.EditIndex = -1;
    //        GridViewBind();
    //        Response.Write("<script language=javascript>alert('删除成功！')</script>");
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write("数据库错误，错误原因：" + ex.Message);
    //        Response.End();
    //    }
    //}
    private void ToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    public void S_Click(object sender, EventArgs e)
    {
        DataSet ds = GetEntities(Const_PageSize,0);

        //GridView.DataSource = ds;
        //GridView.DataBind();

        if (ds != null
            && ds.Tables != null
            && ds.Tables.Count > 0
            )
        {
            lvQuestion.DataSource = ds.Tables[0];
            lvQuestion.DataBind();

            //dpQuestion.TotalRowCount = 0;

            if (ds.Tables.Count >= 2)
            {
                int totalCount = PubClass.GetInt(ds.Tables[1].Rows[0]["TotalCount"]);

                pagerQuestion.CurrentPageIndex = 1;
                pagerQuestion.RecordCount = totalCount;
                //pagerQuestion2.CurrentPageIndex = 1;
                //pagerQuestion2.RecordCount = totalCount;
            }
        }

        //this.txtKeyWords.Focus();
        this.txtQuestionText.Focus();
    }

    protected void pagerQuestion_PageChanged(object sender, EventArgs e)
    {
        DataSet ds = GetEntities(Const_PageSize,pagerQuestion.CurrentPageIndex);

        //GridView.DataSource = ds;
        //GridView.DataBind();

        if (ds != null
            && ds.Tables != null
            && ds.Tables.Count > 0
            )
        {
            lvQuestion.DataSource = ds.Tables[0];
            lvQuestion.DataBind();

        }
    }

    //protected void pagerQuestion2_PageChanged(object sender, EventArgs e)
    //{
    //    DataSet ds = GetEntities(Const_PageSize, pagerQuestion2.CurrentPageIndex);

    //    //GridView.DataSource = ds;
    //    //GridView.DataBind();

    //    if (ds != null
    //        && ds.Tables != null
    //        && ds.Tables.Count > 0
    //        )
    //    {
    //        lvQuestion.DataSource = ds.Tables[0];
    //        lvQuestion.DataBind();

    //    }
    //}
}


#region Disused

//protected void Page_Load(object sender, EventArgs e)
//{

//}

//public void S_Click(object sender, EventArgs e)
//{
//    DataSet ds = GetEntities();

//    GridView.DataSource = ds;

//    GridView.DataBind();

//}


//private DataSet GetEntities()
//{


//    DataSet ds = new DataSet();

//    SqlSeverHelper sqlHelper = new SqlSeverHelper();

//    string a = " 1=1";

//    string Age = ddlAgeGroup.SelectedValue.ToString();



//    string KeyWords = txtKeyWords.Text.ToString();

//    a += " and AgeGroupName = '" + Age + "' and KeyWords like  '%" + KeyWords + "%'";

//    string sql = string.Format("select * from T_Question a inner join T_Solution b on a.Code=b.AnCode where {0} ", a);

//    sqlHelper.Fill(ds, CommandType.Text, sql, null);

//    return ds;
//}

#endregion


