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

public partial class Message : System.Web.UI.Page
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
        
        string strAboutAgeBegin = this.txtAboutAgeBegin.Text.Trim();
        string strAboutAgeEnd = this.txtAboutAgeEnd.Text.Trim();
        string selectCommand = string.Empty;
        selectCommand = @"select id,user_id,message_Content,date_format(messDate,'%Y-%c-%d %H:%i:%s') as messDate ,message_Title,isRead,poster_id,subhead,aboutAgeBegin,aboutAgeEnd from hbh_Message";
       
        if (string.IsNullOrEmpty(selectCommand))
        {
            //ddlSubQuestion.Visible = true;

            //btnSearch.Visible = true;

            return null;
        }
        
        DataTable dtblDiscuss = new DataTable();
        DatabaseAdapter sqlHelper = new DatabaseAdapter(this);
        sqlHelper.DbHelper.Fill(dtblDiscuss, CommandType.Text, selectCommand, null);

        if (string.IsNullOrEmpty(strAboutAgeBegin) || string.IsNullOrEmpty(strAboutAgeEnd))
        {
            
            return dtblDiscuss;
        }
        else
        {
            DataTable showDT = new DataTable();
            sqlHelper.DbHelper.Fill(showDT, CommandType.Text, selectCommand, null);

            //where aboutAgeEnd <= '0岁0个月4周第7天' and aboutAgeBegin >='0岁0个月1周第1天'
            string where = "aboutAgeEnd <= '" + strAboutAgeEnd + "' and aboutAgeBegin >='" + strAboutAgeBegin + "'";
            selectCommand = @"select id,user_id,message_Content,date_format(messDate,'%Y-%c-%d %H:%i:%s') as messDate ,message_Title,isRead,poster_id,subhead,aboutAgeBegin,aboutAgeEnd from hbh_Message where {0};";
            selectCommand = string.Format(selectCommand, where);

           // DataTable showDT = new DataTable();
            //showDT.Rows = new DataRowCollection();
            showDT = dtblDiscuss;
            List<string> ids = new List<string>();
            for (int i =0 ;i<dtblDiscuss.Rows.Count ;i++)//DataRow row in dtblDiscuss.Rows)
             {
                 DataRow row = dtblDiscuss.Rows[i];
                 string rowAboutAgeBegin = row["aboutAgeBegin"].ToString();
                 string rowAboutAgeEnd = row["aboutAgeEnd"].ToString();
                 if (string.IsNullOrEmpty(rowAboutAgeBegin) || string.IsNullOrEmpty(rowAboutAgeEnd))
                 {
                     dtblDiscuss.Rows.Remove(row);
                     continue;
                 }
                 else
                 {
                    string begin = GetYMWDByAboutAge(rowAboutAgeBegin);
                    string end = GetYMWDByAboutAge(rowAboutAgeEnd);
                    string txtBegin = GetYMWDByAboutAge(this.txtAboutAgeBegin.Text.Trim());
                    string txtEnd = GetYMWDByAboutAge(this.txtAboutAgeEnd.Text.Trim());
                    if (string.Compare(end, txtEnd, false) <= 0 && string.Compare(begin, txtBegin, false) >= 0)
                    {
                        string id = row["id"].ToString();
                        ids.Add(id);
                       // dtblDiscuss.Rows.IndexOf(row);
                    }
                   
                 }
             }
            
                for (int i=0;i<dtblDiscuss.Rows.Count;i++)//in dtblDiscuss.Rows)
                {
                    DataRow dr = dtblDiscuss.Rows[i];
                    if (!ids.Contains(dr["id"].ToString()))
                    {
                        dtblDiscuss.Rows.Remove(dr);
                        i--;
                    }
                    
                }
            



        }
        return dtblDiscuss;
       
    }

    private static string GetYMWDByAboutAge(string ymwd)
    {
        if (string.IsNullOrEmpty(ymwd))
        {
            return "";
        }

        string year = ""; string month = "";
        string newString = "";
        string[] Y = ymwd.Split('岁');
        if (Y != null && Y.GetLength(0) == 2)
        {
            year = Y[0];
            if (year.Length < 2)
            {
                year = "0" + year;
            }
            string otherY = Y[1];
            if (!string.IsNullOrEmpty(otherY))
            {
                string[] M = otherY.Split('个');
                if (M != null && M.GetLength(0) == 2)
                {
                    month = M[0];
                    if (month.Length < 2)
                    {
                        month = "0" + month;
                    }
                     string otherM = M[1];
                     newString = year + "岁" + month + "个" + otherM;
                   
                    

                }

            }
        }
       
        return newString;
    }

    private void AddTopTreeViewNodes(DataTable treeViewData, int pageTreeType,string parentTreeType)
    {
        DataView view = new DataView(treeViewData);
         foreach (DataRowView row in view)
        {
            TreeNode newNode = new TreeNode(row["aboutAgeBegin"].ToString()+"---"+row["aboutAgeEnd"].ToString(), string.Format("{0}", row["ID"].ToString()));   //  , row["Code"].ToString());
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
        string url = "MessageShow.aspx?";
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

