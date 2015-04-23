using System;
using System.Collections.Generic;
using System.Data;
// using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SolutionA : System.Web.UI.Page
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

        txtKeyWord.Text = Session["keyword"].ToString();

        txtText.Text = Session["text"].ToString();
       
    }



    protected void btnSav_Click(object sender, EventArgs e)
    {
        string id = Session["key"].ToString();

        string code = Session["code"].ToString();

        txtKeyWord.Text = Session["keyword"].ToString();

        txtText.Text = Session["text"].ToString();

        string time =DateTime.Now.ToString();

        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);

        DLL.User user = Session["dsUser"] as DLL.User;

        if (string.IsNullOrEmpty(txtAnser.Text))
        
        {
        
            txtAnser.Text = user.Name;
        
        }

        string sql = "insert T_Solution (ancode,qucode,Stext,answer,thedate) VALUES('" + id + "','" + code + "','" + txtAns.Text + "','" + txtAnser.Text + "','" + time + "' )";

        int row = sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, sql);

        if (row > 0)
        {

            Response.Write("<script>alert('成功！');window.location.href='Solution.aspx'</script>");

        }
    }
}