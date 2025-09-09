using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
namespace NewsService
{
	public partial class AddMenu : System.Web.UI.Page
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
			SqlDataReader MyRead = ADAL.A_ViewData.MyDR("SELECT menuid,menustr FROM Menu ");
			ViewDR.DataSource = MyRead;
			ViewDR.DataBind();
			MyRead.Close(); MyRead.Dispose();
		}
		protected void SaveBTN_Click(object sender, EventArgs e)
		{
			if (menustrTB.Text.Trim() == "")
			{
				Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "FillStarItem").ToString());
				return;
			}
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@menustr", menustrTB.Text.Trim());

			if (Tools.Tools.GetSessen("MenuS").ToString().Trim() != "")
				ADAL.A_ExecuteData.AddData("update Menu set menustr = @menustr where MenuID=" + Tools.Tools.GetSessen("MenuS").ToString().Trim(), SP);
			else
			{
				if (ADAL.A_CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Insert))
				{
					Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "AddAccessText").ToString(), "", true);
					return;
				}
				ADAL.A_ExecuteData.AddData("INSERT INTO Menu (menustr ) VALUES ( @menustr )", SP);
			}
			Tools.Tools.SetSessen("MenuS", "");
			UpdateGrid();
			SaveBTN.Text = GetGlobalResourceObject("resource", "SaveBTNText").ToString();
			menustrTB.Text = "";
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
					ADAL.A_ExecuteData.DeleteData("delete from Menu where MenuID=" + ComArg);
			}
			else if (e.CommandName == "EDIT")
			{
				if (ADAL.A_CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Edit))
				{
					Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "EditAccessText").ToString(), "", true);
					return;
				}
				SqlDataReader MyRead = ADAL.A_ViewData.MyDR("select menuid,menustr from Menu where MenuID=" + ComArg);
				if (!MyRead.Read())
					return;
				Tools.Tools.SetSessen("MenuS", Tools.MyCL.MGInt(MyRead, 0).ToString().Trim());
				menustrTB.Text = Tools.MyCL.MGStr(MyRead, 1);
				SaveBTN.Text = GetGlobalResourceObject("resource", "SaveBTNEditText").ToString();
				MyRead.Close(); MyRead.Dispose();
			}
			UpdateGrid();
		}
	}
}
