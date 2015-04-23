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

public partial class SubjectivityQuestion : System.Web.UI.Page
{
    private const string Const_TreeNodeTarget = "fSingleShow";              //      "fMenuShow"

    private const bool IsControlByTreeType = true;

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
      
        string strSelect = ddlSubQuestion.SelectedValue.ToString();
        string selectCommand = string.Empty;
        if (strSelect.Equals( "未回答的"))
        {
            selectCommand = @"select id,sub_Qes_title,question_Content,keywords,messDate,questioner,subQue_Result,aboutAge from HBH_SubQuestion where subQue_Result is null or subQue_Result = ''";
       
        }
        else if (strSelect.Equals( "全部"))
        {
            selectCommand = @"select id,sub_Qes_title,question_Content,keywords,messDate,questioner,subQue_Result,aboutAge from HBH_SubQuestion";
       
        }
        if (string.IsNullOrEmpty(selectCommand))
        {
            ddlSubQuestion.Visible = true;

            btnSearch.Visible = true;

            return null;
        }
        
        DataTable dtblDiscuss = new DataTable();
        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
        sqlHelper.DbHelper.Fill(dtblDiscuss, CommandType.Text, selectCommand, null);

        return dtblDiscuss;
    }

    private void AddTopTreeViewNodes(DataTable treeViewData, int pageTreeType,string parentTreeType)
    {
        DataView view = new DataView(treeViewData);
         foreach (DataRowView row in view)
        {
            TreeNode newNode = new TreeNode(row["sub_Qes_title"].ToString(), string.Format("{0}", row["ID"].ToString()));   //  , row["Code"].ToString());
            newNode.Expanded = false;
            treeMenu.Nodes.Add(newNode);
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
        string url = "SubjectivityQuestionShow.aspx?";
        if (!PubClass.IsNull(url))
        {
            url = url + "ID=" + row["ID"].ToString();
        }
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
        treeMenu.Nodes.Clear();
        if (treeMenu.Nodes != null
            && treeMenu.Nodes.Count > 0
            )
        {
            foreach (TreeNode node in treeMenu.Nodes)
            {
                if (node != null)
                {
                   
                        node.Expanded = false;
                }
            }
        }
       
        int pageTreeType = PubClass.GetInt(ExtendMethod.GetPageParam(this,"TreeType"));

        PopulateTreeView(pageTreeType);
        //ddlSubQuestion.Visible = false;
        //btnSearch.Visible = false;
    }
}

