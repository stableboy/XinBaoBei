using System;
using System.Collections.Generic;
// using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DLL;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 强制检查用户是否有效，失效重登陆
        DLL.User loginUser = Session["dsUser"] as DLL.User;
        //if (loginUser == null)
        //{
        //    Response.Redirect(string.Format("Login.aspx?Redirect={0}", Request.Url.ToString()));
        //    return;
        //}
        if (loginUser != null)
        {
            lblUser.Text = "欢迎你!  " + loginUser.Name;
            ddlSetOfBook.SelectedValue = ((int)loginUser.SetOfBook).ToString();
            ddlSetOfBook.ToolTip = ddlSetOfBook.SelectedItem != null ? ddlSetOfBook.SelectedItem.Text : string.Empty ;

            //lbMainSearch.PostBackUrl = string.Format("MainSearch.aspx?UserGrade={0}",user.Power);
        }
        else
        {
            //Response.Redirect("Login.aspx");
            Response.Redirect(string.Format("Login.aspx?Redirect={0}", Request.Url.ToString()));
        }

        //if (!this.IsPostBack)
        //{
        //    //DLL.User user = Session["dsUser"] as DLL.User;

        //    if (loginUser != null)
        //    {
        //        lblUser.Text = "欢迎你!  " + loginUser.Name;
        //        ddlSetOfBook.SelectedValue = ((int)loginUser.SetOfBook).ToString();

        //        //lbMainSearch.PostBackUrl = string.Format("MainSearch.aspx?UserGrade={0}",user.Power);
        //    }
        //    else
        //    {
        //        Response.Redirect("Login.aspx");
        //    }
        //    //lbMainSearch.Attributes["target "] = "ifm";  
        //}
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["dsUser"] = null;
        
        Response.Redirect("Login.aspx");
    }
}