using System; using DAL;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Drawing.Imaging;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tools;
//using OrgChartGenerator;

namespace Portal
{
    public partial class Charts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			Tools.Tools.SetPageSeo(Page, "Charts.aspx");
			Tools.Tools.SetPageHit(this.Page.ToString(), this.Page.ClientQueryString);
			BuildMenu();
			

			Default.Adv.PageID = 3;
			
        }
		public string MenuStr = "";//"<ul id=\"sample-menu-4\" class=\"sf-menu sf-navbar\">";
		void BuildMenu()
		{
			SqlDataReader MyRead = ViewData.MyDR1("SELECT UnitChartID, UnitID, Name, [Level], LangID  FROM UnitChart WHERE (UnitID = " + Tools.Tools.GetViewUnitID + ") AND (LEN([Level]) = 2)", null);
			while (MyRead.Read())
			{
				MenuStr += "<li><span>" + Tools.MyCL.MGStr(MyRead, 2) + "</span>";
				BuildMenuLevel2(Tools.MyCL.MGStr(MyRead, 3));
				MenuStr += "</li>";
			}
			MyRead.Close(); MyRead.Dispose();
		}
		void BuildMenuLevel2(string Level)
		{
			int CNT = ExecuteData.CNTData("SELECT COUNT(*) FROM UnitChart WHERE (UnitID = " + Tools.Tools.GetViewUnitID + ") AND  ([Level] LIKE '" + Level + "%') AND (LEN([Level]) = " + (Level.Length + 2) + ")", null);
			if (CNT == 0)
				return;
			int LevLen = Level.Length + 2;
			MenuStr += "<ul>";
			SqlDataReader MyRead = ViewData.MyDR1("SELECT UnitChart.Name, UnitChart.[Level], Personal.Name AS PName, Personal.Family, Personal.PersonalID FROM UnitChart LEFT OUTER JOIN Personal ON UnitChart.UnitChartID = Personal.UnitChartID  WHERE (LEN(UnitChart.[Level]) =" + LevLen + ") AND (UnitChart.UnitID = " + Tools.Tools.GetViewUnitID + ") AND (UnitChart.[Level] LIKE N'" + Level + "%')", null, true);
			while (MyRead.Read())
			{
				if (Tools.MyCL.MGStr(MyRead, 2) == "")
					MenuStr += "<li><span>" + Tools.MyCL.MGStr(MyRead, 0) + "</span>";
				else
					MenuStr += "<li><span>" + Tools.MyCL.MGStr(MyRead, 0) + "</span> (<a href='/Personals-" + Tools.MyCL.MGInt(MyRead, 4) + ".aspx'>" + Tools.MyCL.MGStr(MyRead, 3) + "," + Tools.MyCL.MGStr(MyRead, 2) + "</a>)";
				BuildMenuLevel2(Tools.MyCL.MGStr(MyRead, 1));
				MenuStr += "</li>";
			}
			MenuStr += "</ul>";
			MyRead.Close(); MyRead.Dispose();
		}
      /*  private void CreateRootLevel()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT UnitChart_1.[Level], UnitChart_1.Name, Personal.PersonalID  as formname, (SELECT COUNT(*) AS Expr1 FROM UnitChart WHERE     (LEN([Level]) = LEN(UnitChart_1.[Level]) + 2)) AS childCount FROM UnitChart AS UnitChart_1  LEFT OUTER JOIN                        Personal ON UnitChart_1.UnitChartID = Personal.UnitChartID    WHERE (UnitChart_1.UnitID = 2) and len([level])=2", System.Configuration.ConfigurationManager.ConnectionStrings["PortalConnectionStr"].ConnectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CreateNodes(dt, MyTreeView.Nodes);
            
            //ViewData.MyConnection.Close();

        }
        private void CreateNodes(DataTable yourdt, TreeNodeCollection yournode)
        {
            foreach (DataRow dr in yourdt.Rows)
            {

                TreeNode newNode = new TreeNode();

                newNode.Text = dr["Name"].ToString();
                newNode.Value = dr["Level"].ToString();

                if (newNode.NavigateUrl == "")
                {
                    newNode.SelectAction = TreeNodeSelectAction.None;
                }
                if (string.IsNullOrEmpty(dr["formname"].ToString()))
                    newNode.NavigateUrl = "";
                else
                    newNode.NavigateUrl = "~/Personals-" + dr["formname"].ToString() + ".aspx";
                newNode.SelectAction = TreeNodeSelectAction.Expand;

                yournode.Add(newNode);


                newNode.PopulateOnDemand = ((int)dr["childCount"] > 0);

            }
        }
        private void CreateSubLevel(string parentId, TreeNode parentNode)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT UnitChart_1.[Level], UnitChart_1.Name, Personal.PersonalID as formname, (SELECT COUNT(*) AS Expr1 FROM UnitChart WHERE     (LEN([Level]) = LEN(UnitChart_1.[Level]) + 2)) AS childCount FROM UnitChart AS UnitChart_1 LEFT OUTER JOIN Personal ON UnitChart_1.UnitChartID = Personal.UnitChartID WHERE (UnitChart_1.UnitID = 2) and [level] like '" + parentId + "%' and len([level]) =" + (parentId.Length + 2), System.Configuration.ConfigurationManager.ConnectionStrings["PortalConnectionStr"].ConnectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CreateNodes(dt, parentNode.ChildNodes);
        }
        protected void Mytree_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            CreateSubLevel(e.Node.Value, e.Node);
        }*/
    }
}
