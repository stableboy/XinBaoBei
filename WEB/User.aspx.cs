using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
// using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Redirect("Login.aspx?Redirect=User.aspx");

        // 强制检查用户是否有效，失效重登陆
        DLL.User loginUser = Session["dsUser"] as DLL.User;
        if (loginUser == null)
        {
            //Response.Redirect(string.Format("Login.aspx?Redirect={0}", Request.Url.ToString()));
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

    private void GridViewBind()
    {
        //DLL.User user = Session["dsUser"] as DLL.User;

        //int a = user.Power;

        //string u = user.Code;

        ////string connStr = ConfigurationManager.AppSettings["SqlConnStr"];

        //string SqlStr = "select * from T_User where 1=1 ";

        //if (a < 2) 
        //{ 
        //    SqlStr += " and Code ='" + u + "' "; 
        //}
        //else if (!string.IsNullOrEmpty(txtCode.Text))
        //{
        //    SqlStr += " and Code ='" + txtCode.Text.Trim() + "' ";
        //}


        //DataSet ds = new DataSet();

        try
        {
            //SqlConnection conn = new SqlConnection(connStr);
            //if (conn.State.ToString() == "Closed") conn.Open();
            //SqlDataAdapter da = new SqlDataAdapter(SqlStr, conn);
            //da.Fill(ds, "T_Position");
            //if (conn.State.ToString() == "Open") conn.Close();

            //DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
            //sqlHelper.DbHelper.Fill(ds, CommandType.Text, SqlStr, null);

            DataSet ds = GetEntities();

            GridView.DataSource = ds.Tables[0].DefaultView;
            GridView.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write("数据库错误，错误原因：" + ex.Message);
            Response.End();
        }
    }

    private DataSet GetEntities()
    {

        DataSet ds = new DataSet();

        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);

        DLL.User user = Session["dsUser"] as DLL.User;

        int curPower = user.Power; 

        string curUserCode = user.Code;

        //string sql = "select * from T_User where 1=0 ";

        //if (a < 2) 
        //{ 
        //    sql += " or Code ='" + u + "' "; 
        //}
        //else if (!string.IsNullOrEmpty(txtCode.Text))
        //{
        //    sql += " or Code like  '%" + txtCode.Text.Trim() + "%' or name like '%" + txtCode.Text.Trim() + "%' ";
        //}

        string sql = "select * from T_User where 1=1 ";

        string searchText = txtCode.Text;
        if (!string.IsNullOrEmpty(searchText))
        {
            sql += " and Code like  '%" + searchText.Trim() + "%' or name like '%" + searchText.Trim() + "%' ";
        }

        // 小于2 ，就是 1或者空，那么只能看到自己
        if (curPower < 2)
        {
            sql += " and Code ='" + curUserCode + "' ";
        }
        else
        {
            sql += " and Power <= " + curPower;
        }
       

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
        string strType = ((DropDownList)GridView.Rows[e.RowIndex].FindControl("DDType")).SelectedValue;
        string pw = ((TextBox)GridView.Rows[e.RowIndex].FindControl("txtPw")).Text;


        DLL.User user = Session["dsUser"] as DLL.User;
        int curPower = user.Power;
        int po = 0;

        if (strType == "查询") { po = 1; };
        if (strType == "维护") { po = 2; };
        if (strType == "管理") { po = 3; };

        // 新增大于当前权限的，报错！
        if (po > curPower)
        {
            Response.Write("<script>alert('身份不可以大于当前用户身份！');</script>");
            return;
        }

        //string connStr = ConfigurationManager.AppSettings["SqlConnStr"];
        string SqlStr = string.Empty;
        // 密码非空，才更新密码
        if (!string.IsNullOrEmpty(pw))
            //if (PubClass.IsNull(pw))
        {
            pw = Common.CommonHelper.GetMD5(pw);

            SqlStr = "update T_User set  name='" + name + "' , type='" + strType + "',Power=" + po.ToString() 
                + "  , Password='" + pw + "' where id=" + id;
        }
        // 密码是空，不更新密码
        else
        {
            SqlStr = "update T_User set  name='" + name + "' , type='" + strType + "',Power=" + po.ToString() + " where id=" + id;
        }




        try
        {
            //SqlConnection conn = new SqlConnection(connStr);
            //if (conn.State.ToString() == "Closed") conn.Open();
            //SqlCommand comm = new SqlCommand(SqlStr, conn);
            //comm.ExecuteNonQuery();
            //comm.Dispose();
            //if (conn.State.ToString() == "Open") conn.Close();


            DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
            sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text,SqlStr);

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

        string id = GridView.DataKeys[e.RowIndex].Values[0].ToString();
        string SqlStr = "delete from T_User where id=" + id;


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
  

    protected void A_Click(object sender, EventArgs e)
    {
        Response.Redirect("PowerSet.aspx");
    }


    protected void S_Click(object sender, EventArgs e)
    {
        DataSet ds = GetEntities();
        GridView.DataSource = ds;
        GridView.DataBind();
      
    }
}