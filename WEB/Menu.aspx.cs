using System;
using System.Collections.Generic;
// using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

// using GrantDAL;
// using GrantBLL;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

public partial class Menu : System.Web.UI.Page
{
    #region Disuse

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    //if (!IsPostBack)
    //    //{
    //    //    DataTable dtFather = exe_Table("server=localhost;user id=sa;password=123456;database=Baby",
    //    //        "select code,name FROM T_menu where level=1");

    //    //    foreach (DataRow dr in dtFather.Rows)
    //    //    {

    //    //        TreeNode tn = new TreeNode();
    //    //        tn.Value = dr["code"].ToString();
    //    //        tn.Text = dr["name"].ToString();
    //    //        treeMenu.Nodes.Add(tn);
    //    //        DataTable dtChild = exe_Table("server=localhost;user id=sa;password=123456;database=Baby",
    //    //            "select code,name from T_menu where uplevel=" + dr["code"].ToString());
    //    //        foreach (DataRow drChild in dtChild.Rows)
    //    //        {
    //    //            TreeNode tnChild = new TreeNode();
    //    //            tnChild.Value = drChild["code"].ToString();//节点的Value值，一般为数据库的id值
    //    //            tnChild.Text = drChild["name"].ToString();//节点的Text，节点的文本显示
    //    //            tn.ChildNodes.Add(tnChild);
    //    //        }

    //    //    }
    //    //}
    //}



    /////// <summary>
    /////// 取出数据库中数据，生成DataTable
    /////// </summary>
    /////// <param name="str_Con">数据库连接</param>
    /////// <param name="str_Cmd">sql语句</param>
    /////// <returns></returns>
    ////private DataTable exe_Table(string str_Con, string str_Cmd)
    ////{
    ////    DataSet ds = new DataSet();
    ////    using (SqlConnection conn = new SqlConnection(str_Con))
    ////    {
    ////        using (SqlDataAdapter oda = new SqlDataAdapter(str_Cmd, conn))
    ////        {
    ////            conn.Open();
    ////            oda.Fill(ds);
    ////        }
    ////    }
    ////    return ds.Tables[0];
    ////}
    //private void CreateTreeView()
    //{
    //    string connetion = "Data Source=. ;Initial Catalog=MIS_New;Integrated Security=True";
    //    using (SqlConnection cn = new SqlConnection(connetion))
    //    {
    //        cn.Open();
    //        SqlDataAdapter da = new SqlDataAdapter("select * from QuHua", cn); DataTable dt = new DataTable(); da.Fill(dt);
    //        //首先把第一级的行政区划取出生成TreeView的节点                  //作为递归运算的入口 
    //        CreateTreeViewRecursive(treeMenu.Nodes, dt, 0);
    //    }
    //}
    //private void CreateTreeViewRecursive(TreeNodeCollection nodes, DataTable dataSource, int parentid)
    //{
    //    string filter;
    //    filter = string.Format("parentid={0}", parentid); DataRow[] drarr = dataSource.Select(filter); TreeNode node;
    //    foreach (DataRow dr in drarr)
    //    {
    //        node = new TreeNode();
    //        node.Text = (string)dr["name"]; node.Tag = (int)dr["id"]; nodes.Add(node);
    //        CreateTreeViewRecursive(node.Nodes, dataSource, (int)node.Tag);
    //    }
    //}

    //private void PopulateTreeView()
    //{
    //    DataTable treeViewData = GetTreeViewData();
    //    AddTopTreeViewNodes(treeViewData);  //绑定父节点
    //}
    //private DataTable GetTreeViewData()    //获取数据
    //{
    //    string selectCommand = "SELECT * FROM T_Menu";  // "SELECT Code,Level,UpLevel,Name FROM T_Menu";
    //    string conString = ConfigurationManager.AppSettings["SqlConnStr"];
    //    SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
    //    DataTable dtblDiscuss = new DataTable();
    //    dad.Fill(dtblDiscuss);
    //    return dtblDiscuss;
    //}
    //private void AddTopTreeViewNodes(DataTable treeViewData)
    //{
    //    DataView view = new DataView(treeViewData);
    //    view.RowFilter = "ParentMenu is null or ParentMenu<=0 ";    //  "Level ='1'";
    //    foreach (DataRowView row in view)
    //    {
    //        TreeNode newNode = new TreeNode(row["Name"].ToString(), row["Code"].ToString());
    //        newNode.NavigateUrl = "MenuAdd.aspx?Parent=" + row["ID"].ToString() + "&ParentCode=" + row["Code"].ToString() + "&ParentName=" + row["Name"].ToString();

    //        treeMenu.Nodes.Add(newNode);
    //        AddChildTreeViewNodes(treeViewData, newNode, row["ID"].ToString());//绑定子节点
    //    }

    //}
    //private void AddChildTreeViewNodes(DataTable treeViewData, TreeNode parentTreeViewNode, string parentMenu)
    //{
    //    DataView view = new DataView(treeViewData);
    //    view.RowFilter = "ParentMenu=" + parentMenu;        //   parentTreeViewNode.Value;
    //    foreach (DataRowView row in view)
    //    {
    //        TreeNode newNode = new TreeNode(row["Code"].ToString(), row["Name"].ToString());
    //        newNode.NavigateUrl = "MenuAdd.aspx?Parent=" + row["ID"].ToString() + "&ParentCode=" + row["Code"].ToString() + "&ParentName=" + row["Name"].ToString();
    //        parentTreeViewNode.ChildNodes.Add(newNode);
    //        AddChildTreeViewNodes(treeViewData, newNode, row["ID"].ToString());//递归，绑定子节点
    //    }
    //}


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        // 强制检查用户是否有效，失效重登陆
        DLL.User loginUser = Session["dsUser"] as DLL.User;
        if (loginUser == null)
        {
            Response.Redirect(string.Format("Login.aspx?Redirect={0}", Request.Url.ToString()));
            return;
        }

        //if (!Page.IsPostBack)
        //    PopulateTreeView();


        if (!Page.IsPostBack)
        {
            DLL.User user = Session["dsUser"] as DLL.User;

            int a = user.Power;

            if (a < 2) { Response.Redirect("Nothing.aspx"); }

        }
    }

    protected void btnSav_Click(object sender, EventArgs e)
    {
        //SqlSeverHelper sqlHelper = new SqlSeverHelper();

        //string sql = "insert T_Menu (Code,Name,Level,ParentMenu) VALUES('" + txtCode.Text.Trim() + "','" + txtName.Text.Trim() + "','" + txtLevel.Text.Trim() + "','" + txtParentMenuID.Text.Trim() + "' )";

        //int row = sqlHelper.ExecuteNonQuery(CommandType.Text, sql);

        //if (row > 0)
        //{

        //    Response.Write("<script>alert('成功！');window.location.href='MenuAdd.aspx'</script>");

        //}
    }
}

