using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
namespace NewsService
{
	public partial class AccessTypes : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (ADAL.A_CheckData.PageAccess(this.Page.ToString()))
				Response.Redirect("/Logins");
			Admin.MyBackHref = "Access.aspx";
			if (!IsPostBack)
				UpdateGrid();
		}
		protected void SaveBTN_Click(object sender, EventArgs e)
		{

			if (nameTB.Text.Trim() == "")
			{
				Tools.Tools.Alert(Page,GetGlobalResourceObject("resource", "FillStarItem").ToString());
				return;
			}
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@name", nameTB.Text.Trim());
			SP.AddWithValue("@unitid", ADAL.A_CheckData.GetUnitID().ToString());
			if (Tools.Tools.GetSessen("AccessTypesS").ToString().Trim() != "")
				ADAL.A_ExecuteData.AddData("update AccessTypes set name = @name where AccessTypeID=" + Tools.Tools.GetSessen("AccessTypesS"), SP);
			else
			{
				if (ADAL.A_CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Insert))
				{
					Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "AddAccessText").ToString(), "", true);
					return;
				}
				ADAL.A_ExecuteData.AddData("INSERT INTO AccessTypes (name,unitid ) VALUES ( @name,@unitid )", SP);
			}

			Tools.Tools.SetSessen("AccessTypesS", "");
			UpdateGrid();
			SaveBTN.Text = GetGlobalResourceObject("resource", "SaveBTNText").ToString();
			nameTB.Text = "";
		}
		void UpdateGrid()
		{
			ViewDR.DataSource = ADAL.A_ViewData.MyDR("SELECT accesstypeid,name FROM AccessTypes WHERE (AccessTypeID <> 1) and unitid=" + ADAL.A_CheckData.GetUnitID());
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
					ADAL.A_ExecuteData.DeleteData("delete from AccessTypes where AccessTypeID=" + ComArg);
			}
			else if (e.CommandName == "EDIT")
			{
				if (ADAL.A_CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Edit))
				{
					Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "EditAccessText").ToString(), "", true);
					return;
				}
				SqlDataReader MyRead = ADAL.A_ViewData.MyDR("select accesstypeid,name from AccessTypes where AccessTypeID=" + ComArg);
				if (!MyRead.Read())
				{
					MyRead.Close(); MyRead.Dispose();
					return;
				}
				Tools.Tools.SetSessen("AccessTypesS", Tools.MyCL.MGInt(MyRead, 0).ToString().Trim());
				nameTB.Text = Tools.MyCL.MGStr(MyRead, 1);

				SaveBTN.Text = GetGlobalResourceObject("resource", "SaveBTNEditText").ToString();
				MyRead.Close(); MyRead.Dispose();
			}
			UpdateGrid();
		}
	}
}