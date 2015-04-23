using System;
using System.Collections.Generic;
// using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HttpPost : System.Web.UI.Page
{
    private const string Const_MethodName = "Method";

    protected void Page_Load(object sender, EventArgs e)
    {
        //Test();

        //Response.ContentEncoding = System.Text.Encoding.GetEncoding("latin1");
        //Response.ContentType = "text/html;charset=GB2312";
        ////Response.HeaderEncoding = System.Text.Encoding.GetEncoding("GB2312"); //  GB2312

        //string strMethod = UIHelper.GetParam(this, Const_MethodName);

        //string result = string.Empty;
        //switch (strMethod)
        //{
        //    case "GetUser":
        //        {
        //            string strUserCode = UIHelper.GetParam(this, "Account");

        //            result = GetUser(strUserCode);

        //        }
        //        break;
        //    default:
        //        {
        //            //// 这里自己输出中文，所以设置中文，否则会因为字符集显示成???
        //            //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312"); 
        //            result = "Invalid Method.";
        //        }
        //        break;
        //}

        //this.Response.Write(result);
        //this.Response.End();
    }

    #region Disuse

    //private string GetUser(string strAccount)
    //{
    //    PostHelper.PostResult result = new PostHelper.PostResult();

    //    string sql = "select id,account,passwd,name,sex,date_format(age,'%Y-%c-%d %H:%i:%s') as age from judge_user where 1=1 {0}; show variables like 'char%';";

    //    if (!UIHelper.IsNull(strAccount))
    //    {
    //        string where = string.Format(" and account ='{0}'", strAccount);
    //        sql = string.Format(sql, where);
    //    }
    //    else
    //    {
    //        sql = string.Format(sql, "");
    //    }

    //    MySqlHelper mysqlHelper = new MySqlHelper(SetOfBookType.Test);

    //    DataSet ds = new DataSet();

    //    try
    //    {
    //        mysqlHelper.Fill(ds, CommandType.Text, sql);

    //        List<Entities.User> list = Entities.User.GetUserFromTable(ds);

    //        result.IsSuccess = true;
    //        if (list != null)
    //        {
    //            string strJson = Newtonsoft.Json.JsonConvert.SerializeObject(list);
    //            result.ResultJson = strJson;
    //        }
    //        else
    //        {
    //            result.Message = "查询结果为空";
    //        }

    //        if (ds.Tables.Count > 1)
    //        {

    //            List<Entities.Characters> list2 = Entities.Characters.GetCharactersFromTable(ds.Tables[1]);

    //            string strJson2 = Newtonsoft.Json.JsonConvert.SerializeObject(list2);
    //            result.ResultJson2 = strJson2;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        result.IsSuccess = false;
    //        result.Message = ex.Message;
    //    }

    //    string strResult = Newtonsoft.Json.JsonConvert.SerializeObject(result);
    //    return strResult;
    //}

    #endregion

    private void Test()
    {

        string methodName = this.Request["Method"];
        switch (methodName)
        {
            case "Test":
                {
                    string result = string.Format("测试:输入内容,{0};测试成功!", methodName);

                    Response.Write(result);
                    Response.End();
                }
                break;
            case "Test2":
                {
                    string result = string.Format("可以了：{0};成功!", methodName);

                    Response.Write(result);
                    Response.End();
                }
                break;
            default:
                {
                    Response.Write("Error,No Params:Test;");
                    Response.End();
                }
                break;
        }
    }
}