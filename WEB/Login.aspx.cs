using System;
using System.Collections.Generic;
using System.Data;
// using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using DLL;


public partial class Login : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e) 
    {
        if (!this.IsPostBack)
        {
            // 强制检查用户是否有效，失效重登陆
            DLL.User loginUser = Session["dsUser"] as DLL.User;
            if (loginUser != null)
            {
                // 转到主页
                string strRedirect = ExtendMethod.GetPageParam(this,"Redirect");

                if (PubClass.IsNull(strRedirect))
                {
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    Response.Redirect(strRedirect);
                }
                return;
            }
            else
            {
                this.txtUserName.Focus();
            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        SetOfBookType setOfBook = GetSetOfBook(); 

        DataSet ds = new DataSet();

        DatabaseAdapter sqlHelper = new DatabaseAdapter(setOfBook);

        string sql = string.Format("select * from T_User where Code ='{0}' or Name = '{0}'", txtUserName.Text.Trim());

        sqlHelper.DbHelper.Fill(ds, CommandType.Text, sql, null);

        DLL.User user = null;

        if (ds != null
            && ds.Tables != null
            && ds.Tables[0] != null
            && ds.Tables[0].Rows != null
            && ds.Tables[0].Rows.Count > 0
            )
        {
            user = DLL.User.GetEntityFromDataRow(ds.Tables[0].Rows[0]);

            if (user != null)
            {
                // 若选择测试，则为测试帐套，否则为正式帐套
                user.SetOfBook = setOfBook;
            }
        }

        string Password = CommonHelper.GetMD5(txtPassWord.Text.Trim());

        if (user == null)
        {
            this.txtUserName.Focus();
            Response.Write("<script>alert('无此用户！');</script>");
            return;
        }

        else if (user.PassWord != CommonHelper.GetMD5(txtPassWord.Text.Trim()))
        {
            this.txtPassWord.Focus();
            Response.Write("<script>alert('密码错误！');</script>");
            return;
        }

        else if (user.PassWord == CommonHelper.GetMD5(txtPassWord.Text.Trim()))
        {

            // 将用户名存到Session

            Session["dsUser"] = user;

            //DLL.User u = GetUserByUser(user);

            //Session["u"] = u;

            // 转到主页
            string strRedirect = ExtendMethod.GetPageParam(this,"Redirect");

            if (PubClass.IsNull(strRedirect))
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                Response.Redirect(strRedirect);
            }
        }
    }

    private DLL.User GetUserByUser(DLL.User user)
    {
        SetOfBookType setOfBook = GetSetOfBook(); 

        DLL.User u = null;

        if (user != null)
        {

            DatabaseAdapter sqlHelper = new DatabaseAdapter(setOfBook);

            string sql = "select * from T_User where 1=0 ";

            if (!string.IsNullOrEmpty(user.Code))
            {

                sql += string.Format(" or Code = '{0}'", user.Code);

            }


            DataSet ds = new DataSet();

            sqlHelper.DbHelper.Fill(ds, CommandType.Text, sql, null);

            if (

                ds != null && ds.Tables != null

                && ds.Tables[0] != null

                && ds.Tables[0].Rows != null

                && ds.Tables[0].Rows.Count > 0

                )
            {

                u = DLL.User.GetEntityFromDataRow(ds.Tables[0].Rows[0]);

            }

        }

        return u;

    }

    // 若选择测试，则为测试帐套，否则为正式帐套
    /// <summary>
    /// 若选择测试，则为测试帐套，否则为正式帐套
    /// </summary>
    /// <returns></returns>
    private SetOfBookType GetSetOfBook()
    {
        return ddlSetOfBook.SelectedValue == "1" ? SetOfBookType.Test : SetOfBookType.Offical;
    }
}
