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

public partial class MenuTree : System.Web.UI.Page
{
    private const string Const_TreeNodeTarget = "fSingleShow";              //      "fMenuShow"

    private const bool IsControlByTreeType = true;

    #region Disused

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

        if (!Page.IsPostBack)
        {
            int pageTreeType = PubClass.GetInt(ExtendMethod.GetPageParam(this,"TreeType"));

            PopulateTreeView(pageTreeType);

            switch (pageTreeType)
            {
                case (int)MenuTreeTypeEnum.Menu:
                    {
                        ddlAgeGroup.Visible = false;
                        //lblAgeGroup.Visible = false;
                        btnSearch.Visible = false;
                    }
                    break;
                case (int)MenuTreeTypeEnum.Question:
                    {
                        ddlAgeGroup.Visible = true;
                        //lblAgeGroup.Visible = true;
                        btnSearch.Visible = true;
                        UIHelper.LoadAgeGroup(this, ddlAgeGroup);
                    }
                    break;
                case (int)MenuTreeTypeEnum.Answer:
                    {
                        ddlAgeGroup.Visible = true;
                        //lblAgeGroup.Visible = true;
                        btnSearch.Visible = true;
                        UIHelper.LoadAgeGroup(this, ddlAgeGroup);
                    }
                    break;
                default:
                    break;
            }

            string strSelected = Request.QueryString["Selected"];
            if (!PubClass.IsNull(strSelected))
            {
                SetSelect(strSelected, treeMenu.Nodes);
            }
        }
    }

    private bool SetSelect(string strSelected, TreeNodeCollection nodes)
    {
        if (nodes != null
            && nodes.Count > 0
            )
        {
            foreach (TreeNode node in nodes)
            {
                if (node != null
                    )
                {
                    if (
                        !PubClass.IsNull(node.Value)
                        && node.Value == strSelected
                        )
                    {
                        node.Select();
                        return true;
                    }
                    else if (node.ChildNodes != null
                        && node.ChildNodes.Count > 0
                        )
                    {
                        bool success = SetSelect(strSelected, node.ChildNodes);

                        // 如果成功了，就不往下继续查找了;
                        if (success)
                            return true;
                    }
                }
            }
        }
        return false;
    }


    private void PopulateTreeView(int pageTreeType)
    {
        DataTable treeViewData = GetTreeViewData(pageTreeType);
        AddTopTreeViewNodes(treeViewData, pageTreeType, ((int)MenuTreeTypeEnum.Menu).ToString("G0"));  //绑定父节点
    }

    private DataTable GetTreeViewData(int treeType)    //获取数据
    {
        //string selectCommand = "SELECT * FROM T_Menu";  // "SELECT Code,Level,UpLevel,Name FROM T_Menu";

//        string selectCommand = @"SELECT 0 as TreeType,ID,Code,Name,ParentMenu FROM T_Menu 
//union 
//select 1 as TreeType,ID,Code,Text as Name,ParentMenu from T_Question 
//union 
//select 1 as TreeType,ID,AnCode as Code,Intro as Name,Question as ParentMenu from T_Solution ";

        //int treeType = PubClass.GetShort(ExtendMethod.GetPageParam(this,"TreeType"));
        
        string strAge = ddlAgeGroup.SelectedValue.ToString();

        string selectCommand = string.Empty;
        switch (treeType)
        {
            case (int)MenuTreeTypeEnum.Menu:
                {
                    selectCommand = @"SELECT 0 as TreeType,0 as ParentTreeType,ID,Code,Name,ParentMenu,Sequence FROM T_Menu ";
                }
                break;
            case (int)MenuTreeTypeEnum.Question:
                {
                    // 如果年龄段为空，不能查问题、答案
                    if(PubClass.IsNull(strAge))
                    {
                        break;
                    }

                    selectCommand  += @" 
SELECT 0 as TreeType,0 as ParentTreeType,ID,Code,Name,ParentMenu,Sequence FROM T_Menu where AgeGroupName = @Age 
union 
select 1 as TreeType,0 as ParentTreeType,ID,Code,Title as Name,ParentMenu,Sequence from T_Question where AgeGroupName = @Age ";
                }
                break;
            case (int)MenuTreeTypeEnum.Answer:
                {
                    // 如果年龄段为空，不能查问题、答案
                    if(PubClass.IsNull(strAge))
                    {
                        break;
                    }

                    selectCommand += @" 
SELECT 0 as TreeType,0 as ParentTreeType,ID,Code,Name,ParentMenu,Sequence FROM T_Menu where AgeGroupName = @Age 
union 
select 1 as TreeType,0 as ParentTreeType,ID,Code,Title as Name,ParentMenu,Sequence from T_Question where AgeGroupName = @Age 
union 
select 2 as TreeType,1 as ParentTreeType,ID,AnCode as Code,case when ifnull(Intro,'') = '' then concat(left(SText,100),'...') else Intro end as Name,Question as ParentMenu,Sequence from T_Solution where Question in (select T_Question.ID from T_Question where AgeGroupName = @Age) ";
                }
                break;
            default:
                break;
        }

        selectCommand += " order by length(substring_index(Sequence,'.',1)),substring_index(Sequence,'.',1),ParentMenu,Name ";


        //string conString = ConfigurationManager.AppSettings["SqlConnStr"];
        //SqlDataAdapter dad = new SqlDataAdapter(selectCommand, conString);
        //DataTable dtblDiscuss = new DataTable();
        //dad.Fill(dtblDiscuss);

        if (PubClass.IsNull(strAge))
        {
            // strAge = "AgeGroupName";
            selectCommand = selectCommand.Replace("@Age", "AgeGroupName");
        }
        else
        {
            selectCommand = selectCommand.Replace("@Age", string.Format("'{0}'",strAge));
        }

        DataTable dtblDiscuss = new DataTable();
        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
        sqlHelper.DbHelper.Fill(dtblDiscuss, CommandType.Text, selectCommand, null);

        return dtblDiscuss;
    }

    private void AddTopTreeViewNodes(DataTable treeViewData, int pageTreeType,string parentTreeType)
    {
        DataView view = new DataView(treeViewData);
        view.RowFilter = string.Format("(ParentMenu is null or ParentMenu <= 0) and ParentTreeType={0}", parentTreeType); // "ParentMenu is null or ParentMenu<=0 ";    //  "Level ='1'";
        foreach (DataRowView row in view)
        {
            TreeNode newNode = new TreeNode(row["Name"].ToString(), string.Format("{0}-{1}", row["TreeType"].ToString(), row["ID"].ToString()));   //  , row["Code"].ToString());
            newNode.Expanded = true;
            treeMenu.Nodes.Add(newNode);
            AddChildTreeViewNodes(treeViewData, newNode, row["ID"].ToString(), pageTreeType, row["TreeType"].ToString());//绑定子节点

            bool isLeaf = newNode.ChildNodes.Count == 0;

            newNode.NavigateUrl = GetTreeUrl(row, pageTreeType, newNode);
            newNode.Target = Const_TreeNodeTarget;
            
        }

    }

    private void AddChildTreeViewNodes(DataTable treeViewData, TreeNode parentTreeViewNode, string parentMenu
        , int pageTreeType,string parentTreeType)
    {
        //int childCount = 0;

        DataView view = new DataView(treeViewData);
        view.RowFilter = string.Format("ParentMenu={0} and ParentTreeType={1}", parentMenu, parentTreeType);        //   parentTreeViewNode.Value;
        foreach (DataRowView row in view)
        {
            //childCount++;

            TreeNode newNode = new TreeNode(row["Name"].ToString(), string.Format("{0}-{1}", row["TreeType"].ToString(), row["ID"].ToString()));   //  ,row["Code"].ToString());
            newNode.Expanded = false;
            parentTreeViewNode.ChildNodes.Add(newNode);
            //int childs = 
            AddChildTreeViewNodes(treeViewData, newNode, row["ID"].ToString(),pageTreeType, row["TreeType"].ToString());//递归，绑定子节点

            //bool isLeaf = newNode.ChildNodes.Count == 0;

            //newNode.DataItem = row;
            newNode.NavigateUrl = GetTreeUrl(row, pageTreeType, newNode);
            newNode.Target = Const_TreeNodeTarget;
        }

        //return childCount;
    }

    private static string GetTreeUrl(DataRowView row, int pageTreeType, TreeNode newNode)
    {
        int treeType = PubClass.GetShort(row["TreeType"]);

        string url = string.Empty;

        // 第三层为叶子节点，可以增加问题
        //bool isLeaf = newNode.ChildNodes.Count == 0;
        bool isParentLeaf = false;

        // 不严格控制页面传过来的 树类型（目录、问题、答案）；或者 参数数类型与行类型匹配
        bool isLink = !IsControlByTreeType || pageTreeType == treeType;

        //switch (treeType)
        switch (pageTreeType)
        {
            case (int)MenuTreeTypeEnum.Menu:
                {
                    if (
                        isLink
                        )
                    {
                        url = string.Format("MenuAdd.aspx?TreeType={0}&", (int)MenuTreeTypeEnum.Menu);
                    }
                }
                break;
            case (int)MenuTreeTypeEnum.Question:
                {
                    // 叶子节点可能是问题、答案
                    //  || newNode.ChildNodes == null || newNode.ChildNodes.Count == 0 
                    isParentLeaf = (newNode.Depth == 2);

                    if (isLink)
                    {
                        url = string.Format("QuestionShow.aspx?TreeType={0}&",(int)MenuTreeTypeEnum.Question);
                    }
                    else if (isParentLeaf)
                    {
                        url = string.Format("QuestionShow.aspx?TreeType={0}&", (int)MenuTreeTypeEnum.Menu);
                    }
                    else
                    {
                        url = string.Format("MenuAdd.aspx?TreeType={0}&", (int)MenuTreeTypeEnum.Menu);
                    }
                }
                break;
            case (int)MenuTreeTypeEnum.Answer:
                {
                    isParentLeaf = newNode.Depth == 3;

                    if (isLink)
                    {
                        url = string.Format("AnswerShow.aspx?TreeType={0}&", (int)MenuTreeTypeEnum.Answer);
                    }
                    else if (isParentLeaf)
                    {
                        url = string.Format("AnswerShow.aspx?TreeType={0}&", (int)MenuTreeTypeEnum.Question);
                    }
                    else
                    {
                        url = string.Format("MenuAdd.aspx?TreeType={0}&", (int)MenuTreeTypeEnum.Menu);
                    }
                }
                break;
            default:
                if (isLink
                    )
                {
                    url = "MenuAdd.aspx?";
                }
                else
                {
                    url = string.Format("MenuAdd.aspx?TreeType={0}&", (int)MenuTreeTypeEnum.Menu);
                }
                break;
        }

        if (!PubClass.IsNull(url))
        {
            url = url + "ID=" + row["ID"].ToString() + "&Code=" + row["Code"].ToString() + "&Name=" + row["Name"].ToString();
        }
        //return "MenuAdd.aspx?Parent=" + row["ID"].ToString() + "&ParentCode=" + row["Code"].ToString() + "&ParentName=" + row["Name"].ToString();
        //return "MenuAdd.aspx?ID=" + row["ID"].ToString() + "&Code=" + row["Code"].ToString() + "&Name=" + row["Name"].ToString();

        return url;
    }



    protected void treeMenu_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {
        if (e != null
            && e.Node != null
            && e.Node.Depth >= 2
            )
        {

            int treeType = PubClass.GetShort(ExtendMethod.GetPageParam(this,"TreeType"));

            switch (treeType)
            {
                case (int)MenuTreeTypeEnum.Menu:
                    {

                    }
                    break;
                case (int)MenuTreeTypeEnum.Question:
                    {

                    }
                    break;
                case (int)MenuTreeTypeEnum.Answer:
                    {

                    }
                    break;
                default:
                    break;
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strAge = ddlAgeGroup.SelectedValue.ToString();

        bool isMatched = false;
        if (treeMenu.Nodes != null
            && treeMenu.Nodes.Count > 0
            )
        {
            foreach (TreeNode node in treeMenu.Nodes)
            {
                if (node != null)
                {
                    if (node.Text.Contains(strAge))
                    {
                        isMatched = true;
                    }
                    else
                    {
                        node.Expanded = false;
                    }
                }
            }
        }
        if (isMatched)
        {
            return;
        }

        int pageTreeType = PubClass.GetInt(ExtendMethod.GetPageParam(this,"TreeType"));

        PopulateTreeView(pageTreeType);
    }
}

