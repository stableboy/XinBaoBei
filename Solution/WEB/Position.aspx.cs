using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using DLL;

public partial class Position : System.Web.UI.Page
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

        DLL.User user = Session["dsUser"] as DLL.User;

        int a = user.Power;

        if (a < 2 ) { Response.Redirect("Nothing.aspx"); }

    }

    public DataTable query(string strsql)
    {

        //string SqlConnStr = ConfigurationManager.AppSettings["SqlConnStr"];

        //SqlConnection conn = new SqlConnection(SqlConnStr);

        //SqlDataAdapter sda = new SqlDataAdapter(strsql, conn);

        //DataTable dt = new DataTable();

        //sda.Fill(dt);

        DataTable dt = new DataTable();
        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
        sqlHelper.DbHelper.Fill(dt, CommandType.Text, strsql, null);

        return dt;

    }

    protected void btnSav_Click(object sender, EventArgs e)
    {
        string c = this.txtCode.Text;

        string strsql = string.Format("select * from T_Position where Code ='{0}'", c);

        DataTable dts = new DataTable();

        dts = query(strsql);

        if (dts.Rows.Count> 0)
        {
            Response.Write("<script>alert('编码重复！');window.location.href='Position.aspx'</script>");
        }

        else if (string.IsNullOrEmpty(txtCode.Text.Trim()))
        {

            Response.Write("<script>alert('编码不可为空！');window.location.href='Position.aspx'</script>");

            return;

        }

        else {

            DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
             
            string a ;
            a=DDSex.SelectedValue;
            //string sql = "insert T_Position (Code,Name,Type,Sex,Level,Address,Intro) VALUES('" 
            //    + txtCode.Text.Trim() + "','" + txtName.Text.Trim() + "','" + CommonHelper.GetString(txtType.Text.Trim()) 
            //    + "','" + DDSex.SelectedValue + "','" + CommonHelper.GetString(txtLevel.Text.Trim())
            //    + "','" + CommonHelper.GetString(txtAddress.Text.Trim()) + "','" + CommonHelper.GetString(txtIntro.Text.Trim()) + "' )";
            string sql = "insert T_Position (Code,Name,Sex,Level,Address,Intro) VALUES('"
                + txtCode.Text.Trim() + "','" + txtName.Text.Trim() // + "','" + CommonHelper.GetString(txtType.Text.Trim())
                + "','" + DDSex.SelectedValue + "','" + CommonHelper.GetString(txtLevel.Text.Trim())
                + "','" + CommonHelper.GetString(txtAddress.Text.Trim()) + "','" + CommonHelper.GetString(txtIntro.Text.Trim()) + "' )";


            int row = sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, sql);

            if (row > 0)
            {

                Response.Write("<script>alert('成功！');window.location.href='ShowPosition.aspx'</script>");

            }
        
        }

    }


}