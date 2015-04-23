using System;
using System.Collections.Generic;
// using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AboutUs : System.Web.UI.Page
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
        if (!IsPostBack)//判断页面是否首次被加载
        {
            LoadData();
        }
       

    }


    private void LoadData()
    {
        string selectCommand = "SELECT id,aboutUs_Content FROM hbh_aboutus ";

        DataTable dtblDiscuss = new DataTable();
        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
        sqlHelper.DbHelper.Fill(dtblDiscuss, CommandType.Text, selectCommand, null);

        if (dtblDiscuss != null
            && dtblDiscuss.Rows != null
            && dtblDiscuss.Rows.Count > 0
            )
        {
            DataRow row = dtblDiscuss.Rows[0];

            if (row != null)
            {
                txtMessage.Text = row["aboutUs_Content"].ToString();
                txtID.Text = row["id"].ToString();
            }
        }

    }

    protected void btnSav_Click(object sender, EventArgs e)
    {
        if (PubClass.IsNull(txtMessage.Text))
        {
            Response.Write(string.Format("<script>alert('关于我们不可为空！');</script>", Request.Url.ToString()));
            txtMessage.Focus();
            return;
        }


        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
        if (!PubClass.IsNull(this.txtID.Text))
        {
            if (!PubClass.IsNull(this.txtMessage.Text))
            {


                string sql = string.Format("update hbh_aboutus set aboutus_content='{0}' where ID={1}"
                    , txtMessage.Text, txtID.Text.Trim()
                    );

                int row = sqlHelper.DbHelper.ExecuteNonQuery(CommandType.Text, sql);

                if (row > 0)
                {
                    Response.Write("<script>alert('成功！');</script>");
                }
                else
                {
                    Response.Write("<script>alert('失败！');</script>");
                    //Response.Write(string.Format("<script>alert('页面异常！');window.location.href='{0}';window.parent.document.getElementById('fMenuTree').src='MenuTree.aspx?TreeType=2&Selected={1}';</script>"
                    //    , Request.Url.ToString(), string.Format("{0}-{1}", ExtendMethod.GetPageParam(this, "TreeType"), this.txtParamID.Text)
                    //    ));
                    return;
                }
            }
        }
        else
        {
            string sql = string.Format("insert hbh_aboutus (aboutus_content) VALUES('{0}' ) ;   select @@IDENTITY  "
               , txtMessage.Text
               );

            object result = sqlHelper.DbHelper.ExecuteScalar(CommandType.Text, sql);

            long id = PubClass.GetLong(result);

            if (id > 0)
            {
                Response.Write(string.Format("<script>alert('成功！');</script>"));          //    Request.Url.ToString()
            }
        }

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (!PubClass.IsNull(txtID.Text))
        {
            string id = txtID.Text;
            //string connStr = ConfigurationManager.AppSettings["SqlConnStr"];
            string SqlStr = "delete from hbh_aboutus where id=" + id;


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

                Response.Write("<script>alert('删除成功！');</script>");

            }
            catch (Exception ex)
            {
                Response.Write("数据库错误，错误原因：" + ex.Message);
                Response.End();
            }
        }
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {

        if (fudImportExcel.HasFile == false)//HasFile用来检查FileUpload是否有指定文件
        {
            Response.Write("<script>alert('请您选择文件')</script> ");
            return;//当无文件时,返回
        }
        //string IsXls = System.IO.Path.GetExtension(fudImportExcel.FileName).ToString().ToLower();//System.IO.Path.GetExtension获得文件的扩展名
        //if (IsXls != ".xls")
        //{
        //    Response.Write("<script>alert('只可以选择Excel文件')</script>");
        //    return;//当选择的不是Excel文件时,返回
        //}
        string filename = fudImportExcel.FileName;              //获取Execle文件名  DateTime日期函数
        string fileRelativeName = ("upfiles\\") + filename;
        string savePath = Server.MapPath(fileRelativeName);//Server.MapPath 获得虚拟服务器相对路径
        fudImportExcel.SaveAs(savePath);                        //SaveAs 将上传的文件内容保存在服务器上

    }
}