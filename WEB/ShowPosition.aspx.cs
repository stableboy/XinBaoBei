using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
// using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ShowPosition : System.Web.UI.Page
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

        //DLL.User user = Session["dsUser"] as DLL.User;

        //int a = user.Power;

        //string c = user.Code.ToString();

        DataSet ds = new DataSet();

        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);

        string sql = "select concat(left(Intro,5),'...')  as IntroSimple,T_Position.* from T_Position where 1=1 ";

        //if (a == 1) { sql += " and code ='" + c + "' "; }

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

        string SqlStr = "Select concat(left(Intro,5),'...')  as IntroSimple,T_Position.* from T_Position";

        DataSet ds = new DataSet();

        try
        {
            //SqlConnection conn = new SqlConnection(connStr);
            //if (conn.State.ToString() == "Closed") conn.Open();
            //SqlDataAdapter da = new SqlDataAdapter(SqlStr, conn);
            //da.Fill(ds, "T_Position");
            //if (conn.State.ToString() == "Open") conn.Close();

            DataTable dtblDiscuss = new DataTable();
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
        //string code = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtCode")).Text;
        string name = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtName")).Text;
        //string type = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtType")).Text;
        //string sex = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtSex")).Text;
        string sex = ((DropDownList)GridView.Rows[e.RowIndex].FindControl("DDSex")).SelectedValue;
        string level = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtLevel")).Text;
        string address = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtAddress")).Text;
        string intro = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtIntro")).Text;
        //string SqlStr = "update T_Position set  name='" + name + "' , type='" + type + "'  , sex='" + sex + "' , level='" + level + "' , address='" + address + "' , intro='" + intro + "' where id=" + id;
        string SqlStr = "update T_Position set  name='" + name + "' , sex='" + sex + "' , level='" 
                + level + "' , address='" + address + "' , intro='" + intro + "' where id=" + id;

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
        
        //GridView.Rows[e.RowIndex].Attributes.Add("onclick", "return confirm('你确认要删除吗?')");

        string id = GridView.DataKeys[e.RowIndex].Values[0].ToString();
        string SqlStr = "delete from T_Position where id=" + id;


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
  
}