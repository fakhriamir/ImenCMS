using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace NewsService
{
	public partial class AccessUnit : System.Web.UI.Page
	{
		public string GenScript;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (ADAL.A_CheckData.PageAccess(this.Page.ToString()))
				Response.Redirect("/Logins");
			if(!IsPostBack)
				viewDB();
			//LoadTreeNodes();
		}
		void viewDB()
		{
			string Leve = ADAL.A_ExecuteData.CNTData("SELECT top 1 [Level]  FROM Units  WHERE (UnitID = (SELECT UnitID FROM AdminUsers WHERE (UserID = " + ADAL.A_CheckData.GetUserID() + ")))").Trim();
			UnitDL.DataSource = ADAL.A_ViewData.MyDT("SELECT name, unitid FROM Units where [level] like '" + Leve + "%'");
			UnitDL.DataBind();
			UnitDL_SelectedIndexChanged(null,null);
		}
		void LoadTreeNodes()
		{
			GenScript = "var RootItem=new Array(); var ChildItem=new Array(); ";
            SqlDataReader MyRead = ADAL.A_ViewData.MyDR("SELECT -1 as MenuID,'" + GetGlobalResourceObject("resource", "HaveNot").ToString() + "' as  MenuStr union SELECT MenuID, MenuStr FROM Menu");
			int i = 0;
			while(MyRead.Read())
			{
				int ParentCheckState = 0;
				string ChiledItem = "";
				SqlDataReader MySubRead = ADAL.A_ViewData.MyDR("SELECT MenuChildID, MenuID, ChildStr, (SELECT top 1 PageID FROM AccessUnit WHERE (UnitID = " + UnitDL.SelectedValue + ") AND (PageID = MenuChilds.MenuChildID)) AS HaveAccess FROM MenuChilds  WHERE (MenuID = " + Tools.MyCL.MGInt(MyRead, 0) + ") and type=0 ORDER BY Sort");
				while(MySubRead.Read())
				{
					if (Tools.MyCL.MGInt(MySubRead, 0) != Tools.MyCL.MGInt(MySubRead, 3))
					{
						ChiledItem += "0"+Tools.MyCL.MGInt(MySubRead, 0) + "#" + Tools.MyCL.MGStr(MySubRead, 2) + "$";
						if (ParentCheckState == 1)
							ParentCheckState = 2;
					}
					else
					{
						ChiledItem += "1"+Tools.MyCL.MGInt(MySubRead, 0) + "#" + Tools.MyCL.MGStr(MySubRead, 2) + "$";
						if (ParentCheckState != 2)
							ParentCheckState = 1;
					}
				}
				MySubRead.Close(); MySubRead.Dispose();
				GenScript += " RootItem[" + i + "]='" + ParentCheckState.ToString() + Tools.MyCL.MGInt(MyRead, 0) + "#" + Tools.MyCL.MGStr(MyRead, 1) + "'; ";
				GenScript += " ChildItem[" + i + "]='"+ChiledItem+"';";
				i++;

			}
			MyRead.Close(); MyRead.Dispose();
		}
		protected void UnitDL_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadTreeNodes();
		}
		protected void SaveBTN_Click(object sender, EventArgs e)
		{			
			ADAL.A_ExecuteData.DeleteData("DELETE FROM AccessUnit WHERE (UnitID = "+UnitDL.SelectedValue+")");
			System.Text.RegularExpressions.Regex MySp = new System.Text.RegularExpressions.Regex(",");
			string[] AddItem = MySp.Split(CheckItemTB.Text);
			for (int i = 0; i < AddItem.Length; i++)
			{
				if (AddItem[i].ToLower().IndexOf("m") != -1 || AddItem[i].Trim()=="")
					continue;
				ADAL.A_ExecuteData.AddData("INSERT INTO AccessUnit (UnitID, PageID) VALUES (" + UnitDL.SelectedValue + ","+AddItem[i]+")", null);				
			}
			LoadTreeNodes();
		
		}
	}
}