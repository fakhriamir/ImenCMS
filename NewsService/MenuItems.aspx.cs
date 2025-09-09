using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace NewsService
{
	public partial class MenuItems : System.Web.UI.Page
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
			ViewDR.DataSource = ADAL.A_ViewData.MyDR("SELECT MenuChilds.MenuChildID, MenuChilds.ChildStr, MenuChilds.ChildHref, Menu.MenuStr FROM MenuChilds LEFT OUTER JOIN Menu ON MenuChilds.MenuID = Menu.MenuID ORDER BY MenuChilds.MenuID, MenuChilds.MenuChildID ");
			ViewDR.DataBind();

            MenuIDDL.DataSource = ADAL.A_ViewData.MyDR("SELECT     - 1 AS menuid, '" + GetGlobalResourceObject("resource", "HaveNot").ToString() + "' AS menustr  UNION  SELECT     MenuID, MenuStr  FROM         Menu ");
			MenuIDDL.DataBind();
			
		}

		protected void SaveBTN_Click(object sender, EventArgs e)
		{
			
			if (childstrTB.Text.Trim() == "" || childhrefTB.Text.Trim() == "")
			{
				Tools.Tools.Alert(Page,GetGlobalResourceObject("resource", "FillStarItem").ToString());
				return;
			}
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@menuid", MenuIDDL.SelectedValue);
			SP.AddWithValue("@childstr", childstrTB.Text.Trim());
			SP.AddWithValue("@childhref", childhrefTB.Text.Trim());
			if (ViewState["MenuChildsS"] != null && ViewState["MenuChildsS"].ToString().Trim() != "")
				ADAL.A_ExecuteData.AddData("update MenuChilds set menuid = @menuid,childstr = @childstr,childhref = @childhref where MenuChildID=" + ViewState["MenuChildsS"].ToString().Trim(), SP);
			else
			{
				if (ADAL.A_CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Insert))
				{
					Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "AddAccessText").ToString(), "", true);
					return;
				}
				ADAL.A_ExecuteData.AddData("INSERT INTO MenuChilds (menuid ,childstr ,childhref ) VALUES ( @menuid ,@childstr ,@childhref )", SP);
			}
			ViewState["MenuChildsS"] = null;
			UpdateGrid();
			SaveBTN.Text = GetGlobalResourceObject("resource", "SaveBTNText").ToString();
			childstrTB.Text = "";
			childhrefTB.Text = "";
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
					ADAL.A_ExecuteData.DeleteData("delete from MenuChilds where MenuChildID=" + ComArg);

			}
			else if (e.CommandName == "EDIT")
			{
				if (ADAL.A_CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Edit))
				{
					Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "EditAccessText").ToString(), "", true);
					return;
				}
				SqlDataReader MyRead = ADAL.A_ViewData.MyDR("select menuchildid,menuid,childstr,childhref from MenuChilds where MenuChildID=" + ComArg);
				if (!MyRead.Read())
				{
					MyRead.Close(); MyRead.Dispose();
					return;
				}
				ViewState["MenuChildsS"] = Tools.MyCL.MGInt(MyRead, 0).ToString().Trim();
				MenuIDDL.SelectedValue = Tools.MyCL.MGInt(MyRead, 1).ToString();
				childstrTB.Text = Tools.MyCL.MGStr(MyRead, 2);
				childhrefTB.Text = Tools.MyCL.MGStr(MyRead, 3);

				SaveBTN.Text = GetGlobalResourceObject("resource", "SaveBTNEditText").ToString();
				MyRead.Close(); MyRead.Dispose();
			}
			UpdateGrid();
		}
	}
}