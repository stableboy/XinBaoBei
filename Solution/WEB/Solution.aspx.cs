using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemSql;

public partial class Solution : System.Web.UI.Page
{
    #region Disused

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    GridView.Attributes.Add("style", "table-layout:fixed");

    //    DataSet ds = GetEntities();

    //    GridView.DataSource = ds;

    //    GridView.DataBind();
    //}
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

    //}
    //private DataSet GetEntities()
    //{

    //    DataSet ds = new DataSet();

    //    SqlSeverHelper sqlHelper = new SqlSeverHelper();

    //    string a = " 1=1";

    //    string sql = string.Format("select * from T_Question where {0} ", a);

    //    sqlHelper.Fill(ds, CommandType.Text, sql, null);

    //    return ds;
    //}
      
    //protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)        
    //{
               
    //    int index = Convert.ToInt32(e.CommandArgument);
                       
    //    GridViewRow row = GridView.Rows[index];
                    
    //    string key =row.Cells[1].Text;

    //    string text=row.Cells[3].Text;

    //    string keyword=row.Cells[2].Text;
           
    //    Session["key"] = key;

    //    Session["text"] = text;

    //    Session["keyword"] = keyword;
                     
    //    Response.Redirect("SolutionA.aspx");
      
    //}

    //protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridView.PageIndex = e.NewPageIndex;
    //    GetEntities();
    //}
    //protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    ////判断是否数据行
    //    //if (e.Row.RowType == DataControlRowType.DataRow)
    //    //{
    //    //    //找到GridView控件的第12个单元格中的 按钮 并给他添加 按钮事件 点击删除时提示 是否删除 
    //    //    ((Button)e.Row.Cells[2].Controls[0]).Attributes.Add("onclick", string.Format("javascript:return confirm('您是要删除" + e.Row.Cells[2].Text + " 么')"));
    //    //}
    //}
    //protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    GridView.EditIndex = e.NewEditIndex;
    //    GetEntities();
    //}
    //protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    GridView.EditIndex = -1;
    //    GetEntities();
    //}
    //protected void GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    string id = GridView.DataKeys[e.RowIndex].Values[0].ToString();
    //    string code = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtCode")).Text;
    //    string keywords = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtKeyWords")).Text;
    //    string text = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtText")).Text;
    //    string questioner = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtQuestioner")).Text;
    //    string theDate = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtTheDate")).Text;
    //    string connStr = ConfigurationManager.AppSettings["SqlConnStr"];
    //    string SqlStr = "update T_Question set   keywords='" + keywords + "'  , text='" + text + "' , questioner='" + questioner + "' , theDate='" + theDate + "' where id=" + id;

    //    try
    //    {
    //        SqlConnection conn = new SqlConnection(connStr);
    //        if (conn.State.ToString() == "Closed") conn.Open();
    //        SqlCommand comm = new SqlCommand(SqlStr, conn);
    //        comm.ExecuteNonQuery();
    //        comm.Dispose();
    //        if (conn.State.ToString() == "Open") conn.Close();

    //        GridView.EditIndex = -1;
    //        GetEntities();
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
    //    string connStr = ConfigurationManager.AppSettings["SqlConnStr"];
    //    string SqlStr = "delete from T_Question where id=" + id;


    //    try
    //    {
    //        SqlConnection conn = new SqlConnection(connStr);
    //        if (conn.State.ToString() == "Closed") conn.Open();
    //        SqlCommand comm = new SqlCommand(SqlStr, conn);
    //        comm.ExecuteNonQuery();
    //        comm.Dispose();
    //        if (conn.State.ToString() == "Open") conn.Close();

    //        GridView.EditIndex = -1;
    //        GetEntities();
    //        Response.Write("<script language=javascript>alert('删除成功！')</script>");
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write("数据库错误，错误原因：" + ex.Message);
    //        Response.End();
    //    }
    //}
    //private void ToolStripMenuItem_Click(object sender, EventArgs e)
    //{

    //}

    //private DataSet GetEntities()
    //{

    //    DataSet ds = new DataSet();

    //    SqlSeverHelper sqlHelper = new SqlSeverHelper();

    //    string a = " 1=1";

    //    string sql = string.Format("select * from T_Question where {0} ", a);

    //    sqlHelper.Fill(ds, CommandType.Text, sql, null);

    //    return ds;
    //}

    #endregion

     protected void Page_Load(object sender, EventArgs e)
    {
        // 强制检查用户是否有效，失效重登陆
        DLL.User loginUser = Session["dsUser"] as DLL.User;
        if (loginUser == null)
        {
            Response.Redirect(string.Format("Login.aspx?Redirect={0}", Request.Url.ToString()));
            return;
        }

        GridView.Attributes.Add("style", "table-layout:fixed");

        if (!IsPostBack)
        {
            GridViewBind();
        }
    }

    private void DatasBind()
    {
        DataSet ds = GetEntities();
        GridView.DataSource = ds;
      //this.AspNetPager1.RecordCount = ds.Tables[0].Rows.Count;
        PagedDataSource pds = new PagedDataSource();
        pds.AllowPaging = true;
      //pds.PageSize = AspNetPager1.PageSize;
     // pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;
        pds.DataSource = ds.Tables[0].DefaultView;

    }

    private DataSet GetEntities()
    {

        DataSet ds = new DataSet();

        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);

        string sql = "select * from T_Question where 1=1 ";

        //if (!string.IsNullOrEmpty(txtDeptCode.Text))
        //{
        //    sql += " and DeptCode ='" + txtDeptCode.Text.Trim() + "' ";
        //}

        //if (!string.IsNullOrEmpty(txtCode.Text))
        //{
        //    sql += " and Code ='" + txtCode.Text.Trim() + "' ";
        //}

        //if (!string.IsNullOrEmpty(txtAdr.Text))
        //{
        //    sql += " and Adress ='" + txtAdr.Text.Trim() + "' ";
        //}

        sqlHelper.DbHelper.Fill(ds, CommandType.Text, sql, null);

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

    protected void btnSea_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();

        GridViewBind();

    }

    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView.PageIndex = e.NewPageIndex;
        GridViewBind();
    }
   
    private void GridViewBind()
    {
        //string connStr = ConfigurationManager.AppSettings["SqlConnStr"];
       
        string SqlStr = "Select * from T_Question";

        DataSet ds = new DataSet();

        try
        {
            //SqlConnection conn = new SqlConnection(connStr);
            //if (conn.State.ToString() == "Closed") conn.Open();
            //SqlDataAdapter da = new SqlDataAdapter(SqlStr, conn);
            //da.Fill(ds, "T_Position");
            //if (conn.State.ToString() == "Open") conn.Close();


            DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
            sqlHelper.DbHelper.Fill(ds, CommandType.Text, SqlStr, null);

            GridView.DataSource = ds.Tables[0].DefaultView;
            GridView.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write("数据库错误，错误原因：" + ex.Message);
            Response.End();
        }
    }

    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //判断是否数据行
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            UIHelper.SetDeleteConfirm(e, e.Row.Cells[2].Text);
        }
    }

    protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView.EditIndex = e.NewEditIndex;
        GridViewBind();
    }

    protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView.EditIndex = -1;
        GridViewBind();
    }

    protected void GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string id = GridView.DataKeys[e.RowIndex].Values[0].ToString();
        string code = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtCode")).Text;
        string keywords = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtKeyWords")).Text;
        string text = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtText")).Text;
        string questioner = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtQuestioner")).Text;
        string theDate = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtTheDate")).Text;
        string SqlStr = "update T_Question set   keywords='" + keywords + "'  , text='" + text + "' , questioner='" + questioner + "' , theDate='" + theDate + "' where id=" + id;

        try
        {
        //string connStr = ConfigurationManager.AppSettings["SqlConnStr"];
        //    SqlConnection conn = new SqlConnection(connStr);
        //    if (conn.State.ToString() == "Closed") conn.Open();
        //    SqlCommand comm = new SqlCommand(SqlStr, conn);
        //    comm.ExecuteNonQuery();
        //    comm.Dispose();
            //    if (conn.State.ToString() == "Open") conn.Close();

            DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
            sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, SqlStr);

            GridView.EditIndex = -1;
            GridViewBind();
        }
        catch (Exception ex)
        {
            Response.Write("数据库错误，错误原因：" + ex.Message);
            Response.End();
        }
    }

    protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        GridView.Rows[e.RowIndex].Attributes.Add("onclick", "return confirm('你确认要删除吗?')");

        string id = GridView.DataKeys[e.RowIndex].Values[0].ToString();
        string SqlStr = "delete from T_Question where id=" + id;


        try
        {
        //string connStr = ConfigurationManager.AppSettings["SqlConnStr"];
        //    SqlConnection conn = new SqlConnection(connStr);
        //    if (conn.State.ToString() == "Closed") conn.Open();
        //    SqlCommand comm = new SqlCommand(SqlStr, conn);
        //    comm.ExecuteNonQuery();
        //    comm.Dispose();
            //    if (conn.State.ToString() == "Open") conn.Close();

            DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
            sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, SqlStr);

            GridView.EditIndex = -1;
            GridViewBind();
            Response.Write("<script language=javascript>alert('删除成功！')</script>");
        }
        catch (Exception ex)
        {
            Response.Write("数据库错误，错误原因：" + ex.Message);
            Response.End();
        }
    }

    private void ToolStripMenuItem_Click(object sender, EventArgs e)
    {
     
    }

    protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void GridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
      

        //    Session["text"] = text;

        //    Session["keyword"] = keyword;
        int i=e.NewSelectedIndex;
        string id = GridView.DataKeys[e.NewSelectedIndex].Values[0].ToString();
        string code = ((Label)GridView.Rows[e.NewSelectedIndex].FindControl("lblCode")).Text;
        string keywords = ((Label)GridView.Rows[e.NewSelectedIndex].FindControl("lblKeyWords")).Text;
        string text = ((Label)GridView.Rows[e.NewSelectedIndex].FindControl("lblText")).Text;
        string questioner = ((Label)GridView.Rows[e.NewSelectedIndex].FindControl("lblQuestioner")).Text;
        string theDate = ((Label)GridView.Rows[e.NewSelectedIndex].FindControl("lblTheDate")).Text;

        Session["keyword"] = keywords;

        Session["code"] = code;

        Session["text"] = text;

        Session["key"] = id;

        Response.Redirect("SolutionA.aspx");    

    }
}