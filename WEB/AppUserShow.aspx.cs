using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;
public partial class AppUserShow : System.Web.UI.Page
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
            if (!PubClass.IsNull(strID))
            {

                this.txtID.Text = strID;

                LoadData();

            }

            txtJudgeUser.Focus();
        }
    }

    protected void btnSav_Click(object sender, EventArgs e)
    {
        DLL.User loginUser = Session["dsUser"] as DLL.User;
        if (loginUser == null)
        {
            Response.Redirect(string.Format("Login.aspx?Redirect={0}", Request.Url.ToString()));
            return;
        }

        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);

        if (!PubClass.IsNull(this.txtID.Text))
        {

            string sql = string.Format("update hbh_user set account='{0}',passwd='{1}',name='{2}',sex='{3}' ,birthday ='{4}',region ='{5}',address ='{6}',tel ='{7}',pic ='{8}',ModifiedBy='{9}',ModifiedOn='{10}',SysVersion=sysVersion+1,judge_user_Account ='{11}' where ID='{12}'"
                    , txtAccount.Text.Trim(), this.txtPassword.Text.Trim(), this.txtName.Text.Trim(), txtSex.Text.Trim(), txtBirthday.Text.Trim(), txtRegion.Text.Trim(),
                    txtAddress.Text.Trim(), txtTel.Text.Trim(), txtPic.ImageUrl.Trim(), loginUser.ID.ToString() + "_" + loginUser.Code + "_" + loginUser.Name,DateTime.Now, txtJudgeUser.Text.Trim(), txtID.Text.Trim()
                    );

            int row = sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, sql);

            if (row > 0)
            {
                Response.Write(string.Format("<script>alert('成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='AppUser.aspx?TreeType=1&Selected={1}';</script>"
                              , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), this.txtID.Text)
                              ));
            }

            else
            {
                Response.Write(string.Format("<script>alert('页面异常！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='AppUser.aspx?TreeType=1&Selected={1}';</script>"
                              , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), this.txtID.Text)
                              ));
                return;
            }
        }
        else
        {
            //这里是否加上judge_user_account
            string sql = string.Format("insert into hbh_User (account,passwd,name,sex,birthday,region,address,tel,pic,CreatedOn,createdby,judge_user_account,sysversion) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}');  select @@IDENTITY ; ",
         txtAccount.Text.Trim(), txtPassword.Text.Trim(), txtName.Text.Trim(), txtSex.Text.Trim(), txtBirthday.Text.Trim(), txtRegion.Text.Trim(), txtAddress.Text.Trim(), txtTel.Text.Trim(), txtPic.ImageUrl.Trim(),DateTime.Now,loginUser.ID.ToString() + "_" + loginUser.Code + "_" + loginUser.Name,txtJudgeUser.Text.Trim(),0);

            object result = sqlHelper.DbHelper.ExecuteScalar(CommandType.Text, sql);

            long id = PubClass.GetLong(result);

            if (id > 0)
            {
                Response.Write(string.Format("<script>alert('成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='AppUser.aspx?TreeType=1&Selected={1}';</script>"
                              , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), id.ToString())
                              ));
            }

        }

    }
    private void SetValues(string selectSQL)
    {
        DataTable dtblDiscuss = new DataTable();
        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
        sqlHelper.DbHelper.Fill(dtblDiscuss, CommandType.Text, selectSQL, null);

        if (dtblDiscuss != null
            && dtblDiscuss.Rows != null
            && dtblDiscuss.Rows.Count > 0
            )
        {
            DataRow row = dtblDiscuss.Rows[0];

            if (row != null)
            {
                txtID.Text = row["ID"].ToString();
                txtAccount.Text = row["account"].ToString();
                txtPassword.Text = row["passwd"].ToString();
                txtName.Text = row["name"].ToString();
                txtSex.Text = row["sex"].ToString();
                txtBirthday.Text = Convert.ToDateTime(row["birthday"].ToString()).ToString("yyyy-MM-dd");
                txtRegion.Text = row["region"].ToString();
                txtAddress.Text = row["address"].ToString();
                txtTel.Text = row["tel"].ToString();
                txtPic.ImageUrl = row["pic"].ToString();

                txtJudgeUser.Text = row["judge_user_account"].ToString();

            }
        }
        else
        {

        }
    }
    private void LoadData()
    {
        string id = txtID.Text.Trim();

        // 清空ID，防止ID查不到，错误更新；或者目录传过来的，也更新成问题ID
        txtID.Text = string.Empty;
        if (!PubClass.IsNull(id))
        {
            string where = string.Format("where id='{0}'", id);

            string selectCommand = string.Format(@"SELECT id,account,passwd,name,sex,birthday,region,address,tel,pic,judge_user_account FROM hbh_user {0}", where);
            //string conString = ConfigurationManager.AppSettings["SqlConnStr"];

            SetValues(selectCommand);
          
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.txtID.Text = string.Empty;

        txtAccount.Text = string.Empty;
        txtPassword.Text = string.Empty;
        txtName.Text = string.Empty;
        txtSex.Text = string.Empty;
        txtBirthday.Text = DateTime.Now.ToShortDateString();
        txtRegion.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtTel.Text = string.Empty;
        txtPic.ImageUrl = string.Empty;
        txtJudgeUser.Text = string.Empty;

        this.txtJudgeUser.Focus();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (!PubClass.IsNull(txtID.Text))
        {
            string id = txtID.Text;
            //string connStr = ConfigurationManager.AppSettings["SqlConnStr"];
            string SqlStr = "delete from hbh_user where id=" + id;


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

                Response.Write(string.Format("<script>alert('删除成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='AppUser.aspx?TreeType=0&Selected={1}';</script>", "AppUserShow.aspx"
                    , string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), this.txtID.Text)));
            }
            catch (Exception ex)
            {
                Response.Write("数据库错误，错误原因：" + ex.Message);
                Response.End();
            }
        }
    }
    private const string HOST_NAME = "211.149.198.209";
    private const string PORT = "8012";
    private const string FULL_URL = "http://" + HOST_NAME + ":" + PORT;
    private const string Parmer = "&Account={0}";   // &pwd={1}";//这里等手机Web服务更新布置后这里改成只有一个参数的URL。
    //private const string API_GetUser = FULL_URL + "/HttpPost.aspx?Method=GetUser";
    private const string API_GetUser = FULL_URL + "/HttpPost.aspx?Method=SelectUser";

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        DLL.User loginUser = Session["dsUser"] as DLL.User;
        //if (loginUser == null)
        //{
        //    Response.Redirect(string.Format("Login.aspx?Redirect={0}", Request.Url.ToString()));
        //    return;
        //}
        //调用web服务，取得数据库数据   
        string judgeuser_account = this.txtJudgeUser.Text.Trim();
        if (string.IsNullOrEmpty(judgeuser_account) || string.IsNullOrEmpty(this.txtJudgeUser.Text.Trim()))
        {
            return;

        }
        //string full_Parme = string.Format(Parmer, judgeuser_account, "123456");//这里等手机Web服务更新布置后这里改成只有一个参数的URL。
        string full_Parme = string.Format(Parmer, judgeuser_account);   // , "123456");//这里等手机Web服务更新布置后这里改成只有一个参数的URL。

        string url = API_GetUser + full_Parme;
        System.Net.HttpWebRequest webrequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);

        HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();//请求连接,并反回数据

        Stream stream = webresponse.GetResponseStream();//把返回数据转换成流文件

        StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("gb2312"));// Encoding.UTF8);
        string resultJson = sr.ReadToEnd();
        sr.Close();
        stream.Close();

        PostResult result = Newtonsoft.Json.JsonConvert.DeserializeObject<PostResult>(resultJson) as PostResult;
        if (result != null && !string.IsNullOrEmpty(result.ResultJson))
        {
            string entityJson = result.ResultJson.Substring(1, result.ResultJson.Length - 2);
            Judge_User entity = Newtonsoft.Json.JsonConvert.DeserializeObject<Judge_User>(entityJson) as Judge_User;
            if (entity != null)
            {
                //insert/update进hbh_user 表
                string where = string.Format("where judge_user_account='{0}'", judgeuser_account);
                //查询是否存在当前关联账号的记录，有则更新，没有则新增
                string selectCommand = string.Format(@"SELECT * FROM hbh_user {0}", where);

                DataTable dtblDiscuss = new DataTable();
                DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
                sqlHelper.DbHelper.Fill(dtblDiscuss, CommandType.Text, selectCommand, null);

                if (dtblDiscuss != null
                    && dtblDiscuss.Rows != null
                    && dtblDiscuss.Rows.Count > 0
                    )
                {
                    string sql = string.Format("update hbh_user set account='{0}',passwd='{1}',name='{2}',sex='{3}' ,birthday ='{4}',region ='{5}',address ='{6}',tel ='{7}',pic ='{8}',ModifiedBy='{9}',ModifiedOn='{10}',SysVersion=sysVersion+1,judge_user_Account ='{11}' where judge_user_Account='{10}'"
                    , entity.Account, entity.Passwd, entity.Name, entity.Sex, entity.Birthday, entity.Region, entity.Address, entity.Tel, entity.Pic, loginUser.ID.ToString() + "_" + loginUser.Code + "_" + loginUser.Name, DateTime.Now, txtJudgeUser.Text.Trim(), txtJudgeUser.Text.Trim()
                    );
                   
                    int row = sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, sql);
                    
                    if (row > 0)
                    {
                        Response.Write(string.Format("<script>alert('成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='AppUser.aspx?TreeType=1&Selected={1}';</script>"
                                      , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), this.txtID.Text)
                                      ));
                    }

                    else
                    {
                        Response.Write(string.Format("<script>alert('页面异常！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='AppUser.aspx?TreeType=1&Selected={1}';</script>"
                                      , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), this.txtID.Text)
                                      ));
                        return;
                    }
                }
                else
                {
                   
                    string sql = string.Format("insert into hbh_User (account,passwd,name,sex,birthday,region,address,tel,pic,judge_user_account,CreatedOn,createdby,sysversion) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}');  select @@IDENTITY ; ",
                entity.Account, entity.Passwd, entity.Name, entity.Sex, entity.Birthday, entity.Region, entity.Address, entity.Tel, entity.Pic, txtJudgeUser.Text.Trim(),DateTime.Now, loginUser.ID.ToString() + "_" + loginUser.Code + "_" + loginUser.Name,0);

                    object resultID = sqlHelper.DbHelper.ExecuteScalar(CommandType.Text, sql);

                    long id = PubClass.GetLong(resultID);

                    if (id > 0)
                    {
                        Response.Write(string.Format("<script>alert('成功！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='AppUser.aspx?TreeType=1&Selected={1}';</script>"
                                      , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), id.ToString())
                                      ));
                    }

                }

            }
        }
        //新增或更新完成后重新加载数据，显示。
        if (!string.IsNullOrEmpty(judgeuser_account))
        {
            string where = "";
            string selectSQL = "SELECT id,account,passwd,name,sex,birthday,region,address,tel,pic,judge_user_account FROM hbh_user {0}";
            where = string.Format("where judge_user_account = '{0}'", this.txtJudgeUser.Text.Trim());

            selectSQL = string.Format(selectSQL, where);
            SetValues(selectSQL);

        }
       
    }

}