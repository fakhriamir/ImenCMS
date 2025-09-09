using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Automation
{
	public partial class WorkFolder : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Tools.Automation.CheckAccess("automation_Dashboard.aspx");
			if (!IsPostBack)
				UpdateGrid();
		}
		void UpdateGrid()
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@UserID", DAL.CheckData.CheckTokenGuestUserID());
			SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);
			ViewDR.DataSource = DAL.ViewData.MyDT("SELECT officeworkfolderid,name,guestid,unitid,sort FROM OfficeWorkFolder where Guestid=@UserID and unitid=@UnitID order by sort", SP);
			ViewDR.DataBind();
		}
		protected void SaveBTN_Click(object sender, System.EventArgs e)
		{
			if (nameTB.Text.Trim() == "")
			{
				Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "FillStarItem").ToString());
				return;
			}
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@name", nameTB.Text.Trim());
			SP.AddWithValue("@guestid", DAL.CheckData.CheckTokenGuestUserID());
			SP.AddWithValue("@unitid", Tools.Tools.GetViewUnitID);

			if (ViewState["OfficeWorkFolderS"] != null && ViewState["OfficeWorkFolderS"].ToString().Trim() != "")
				DAL.ExecuteData.AddData("update OfficeWorkFolder set name = @name where OfficeWorkFolderID=" + ViewState["OfficeWorkFolderS"].ToString().Trim(), SP);
			else
			{
				SP.AddWithValue("@sort", Tools.Tools.GetEndSortTable("OfficeWorkFolder"));
				DAL.ExecuteData.AddData("INSERT INTO OfficeWorkFolder (name ,guestid ,unitid ,sort ) VALUES ( @name ,@guestid ,@unitid ,@sort )", SP);
			}
			ViewState["OfficeWorkFolderS"] = null;
			UpdateGrid();
			SaveBTN.Text = "dfgsdfgsdfgdfgd";//GetGlobalResourceObject("resource", "SaveBTNText").ToString();
			nameTB.Text = "";
		}
		protected void ViewDR_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			string ComArg = e.CommandArgument.ToString();
			if (e.CommandName == "UP")
			{
				Tools.Tools.SortFiledview(Tools.Tools.MySortType.UP, ComArg, "OfficeWorkFolder", "OfficeWorkFolderID", "sort", "", " guestid=" + DAL.CheckData.CheckTokenGuestUserID());
			}
			else if (e.CommandName == "DOWN")
			{
				Tools.Tools.SortFiledview(Tools.Tools.MySortType.Down, ComArg, "OfficeWorkFolder", "OfficeWorkFolderID", "sort", "", " guestid=" + DAL.CheckData.CheckTokenGuestUserID());
			}
			else if (e.CommandName == "DEL")
			{
				DAL.ExecuteData.DeleteData("delete from OfficeWorkFolder where OfficeWorkFolderID=" + ComArg);
			}
			else if (e.CommandName == "EDIT")
			{
				SqlDataReader MyRead = DAL.ViewData.MyDR1("select  officeworkfolderid,name,guestid,unitid,sort from OfficeWorkFolder where OfficeWorkFolderID=" + ComArg);

				if (!MyRead.Read())
				{
					MyRead.Close(); MyRead.Dispose();
					return;
				}
				ViewState["OfficeWorkFolderS"] = Tools.MyCL.MGInt(MyRead, 0).ToString().Trim();
				nameTB.Text = Tools.MyCL.MGStr(MyRead, 1);

				SaveBTN.Text = "ويرايش شود";
				MyRead.Close(); MyRead.Dispose();
			}
		}
	}
}