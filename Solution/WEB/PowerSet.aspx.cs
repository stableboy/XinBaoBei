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

public partial class PowerSet : System.Web.UI.Page
{
    int curPower = -1;

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

        curPower = user.Power;

        if (curPower < 2) { Response.Redirect("Nothing.aspx"); }
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
        string strType = DDType.SelectedValue;

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

        string c = this.txtCode.Text;

        string strsql = string.Format("select * from T_User where Code ='{0}'", c);

        DataTable dts = new DataTable();

        DataTable dts1 = new DataTable();

        dts = query(strsql);

        if (dts.Rows.Count > 0)
        {
            Response.Write("<script>alert('编码重复！');</script>");
        }

        else if (string.IsNullOrEmpty(txtCode.Text.Trim()) || string.IsNullOrEmpty(txtName.Text.Trim()))
        {

            Response.Write("<script>alert('编码或名称不可为空！');</script>");

            return;

        }

        else
        {

            DatabaseAdapter sqlHelper = new DatabaseAdapter(this);

            if (string.IsNullOrEmpty(txtPw.Text.Trim())) { txtPw.Text = "123456"; }

            string sql = "insert T_User (Code,Name,Type,power,Password) VALUES('" + txtCode.Text.Trim() + "','" + txtName.Text.Trim() + "','" + strType + "','" + po + "','" + CommonHelper.GetMD5(txtPw.Text.Trim()) + "' )";

            int row = sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, sql);

            if (row > 0)
            {

                Response.Write("<script>alert('成功！');window.location.href='User.aspx'</script>");

            }
        }



    }
}