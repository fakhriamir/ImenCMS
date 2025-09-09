using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
namespace NewsService
{
	public partial class MyAccess : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (ADAL.A_CheckData.PageAccess(this.Page.ToString()))
				Response.Redirect("/Logins");
			if (!IsPostBack)
				UpdateGrid();
		}
		void UpdateGrid()
		{
            PageIDDL.DataSource = ADAL.A_ViewData.MyDR("select * from ( SELECT MenuChildID, '" + GetGlobalResourceObject("resource", "HaveNot").ToString() + "' + RTRIM(ChildStr) AS ChildStr FROM MenuChilds WHERE (MenuID = - 1) AND (Type = 0) AND (MenuChildID IN (SELECT PageID FROM AccessUnit WHERE (UnitID = " + ADAL.A_CheckData.GetUnitID() + "))) UNION SELECT MenuChilds_1.MenuChildID, RTRIM(Menu.MenuStr) + ' - ' + RTRIM(MenuChilds_1.ChildStr) AS ChildStr FROM MenuChilds AS MenuChilds_1 INNER JOIN Menu ON MenuChilds_1.MenuID = Menu.MenuID WHERE (MenuChilds_1.Type = 0) AND (MenuChilds_1.MenuChildID IN (SELECT PageID FROM AccessUnit AS AccessUnit_1 WHERE (UnitID = " + ADAL.A_CheckData.GetUnitID() + ")))) as bb order by bb.ChildStr");
			PageIDDL.DataBind();

			AccessTypeIDDL.DataSource = ADAL.A_ViewData.MyDR("SELECT AccessTypeID, Name FROM AccessTypes WHERE  unitid=" + ADAL.A_CheckData.GetUnitID() + " order by AccessTypeID");
			AccessTypeIDDL.DataBind();

			AccessTypeID1DL.DataSource = ADAL.A_ViewData.MyDR("SELECT AccessTypeID, Name FROM AccessTypes  WHERE  unitid=" + ADAL.A_CheckData.GetUnitID() + " order by AccessTypeID");
			AccessTypeID1DL.DataBind();

		}
		protected void SaveBTN_Click(object sender, EventArgs e)
		{
			if (ADAL.A_CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Insert))
			{
				Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "AddAccessText").ToString(), "", true);
				return;
			}
			Tools.Tools.Alert(Page, "");
			string RetVal = ADAL.A_ExecuteData.CNTData("SELECT COUNT(*) FROM Access WHERE (AccessTypeID = " + AccessTypeIDDL.SelectedValue + ") AND (PageID = " + PageIDDL.SelectedValue + ")");
			if (RetVal != "0")
			{
                Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "AddPermissionMsg2").ToString());
				return;
			}
			int Ins = 0;
			if (InsertCB.Checked)
				Ins = 1;
			int EditAccess = 0;
			if (EditCB.Checked)
				EditAccess = 1;
			int DelAccess = 0;
			if (DelCB.Checked)
				DelAccess = 1;
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@accesstypeid", AccessTypeIDDL.SelectedValue);
			SP.AddWithValue("@pageid", PageIDDL.SelectedValue);
			SP.AddWithValue("@Edit", EditAccess);
			SP.AddWithValue("@Del", DelAccess);
			SP.AddWithValue("@Ins", Ins);
			ADAL.A_ExecuteData.AddData("INSERT INTO Access (accesstypeid ,pageid,Edit,Del,ins) VALUES ( @AccessTypeid,@pageid,@Edit,@Del,@Ins)", SP);
			Tools.Tools.Alert(Page,GetGlobalResourceObject("resource", "AddPermissionMsg").ToString() + PageIDDL.SelectedValue);
		}
		protected void ViewBTN_Click(object sender, EventArgs e)
		{
			ViewDR.DataSource = ADAL.A_ViewData.MyDR("SELECT AccessTypes.Name AS type, Access.AccessID,Access.ins, Access.del,Access.edit,AccessTypes.AccessTypeID, MenuChilds.ChildStr FROM Access INNER JOIN AccessTypes ON Access.AccessTypeID = AccessTypes.AccessTypeID INNER JOIN MenuChilds ON Access.PageID = MenuChilds.MenuChildID WHERE (Access.AccessTypeID = " + AccessTypeID1DL.SelectedValue + ") ORDER BY AccessTypes.AccessTypeID ");
			ViewDR.DataBind();
		}
		protected void ViewDR_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			string ComArg = e.CommandArgument.ToString();
			if (e.CommandName == "UP")
			{
				//Tools.Tools.SortFiled(Tools.Tools.MySortType.UP, ComArg, "Contacts", "ContactID");
			}
			else if (e.CommandName == "DOWN")
			{
				//Tools.Tools.SortFiled(Tools.Tools.MySortType.Down, ComArg, "Contacts", "ContactID");
			}
			else if (e.CommandName == "DEL")
			{
				if (ADAL.A_CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Del))
				{
					Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "DelAccessText").ToString(), "", true);
					return;
				}
				else
					ADAL.A_ExecuteData.ExecData("delete from Access where AccessID=" + ComArg);
			}
			else if (e.CommandName == "EDIT")
			{
				if (ADAL.A_CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Edit))
				{
					Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "EditAccessText").ToString(), "", true);
					return;
				}

			}
			UpdateGrid();
		}
	}
}
