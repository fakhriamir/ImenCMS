using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Automation
{
	public partial class LetterAction : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Tools.Automation.CheckAccess("automation_NewLetter.aspx");
			if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim() == "")
			{
				Tools.Tools.Alert(Page, "لینک مورد نظر معتبر نمی باشد");
				return;
			}
			if (Tools.Tools.ConvertToInt32(Request.QueryString["ID"]) < 0)
			{
				Tools.Tools.Alert(Page, "لینک مورد نظر معتبر نمی باشد");
				return;
			}
			Tools.Automation.CheckUserLetterAccess(Request.QueryString["ID"]);
			if (!IsPostBack)
				UpdateGrid();
		}
		void UpdateGrid()
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);
			SP.AddWithValue("@OfficeLetterID", Tools.Tools.ConvertToInt32(Request.QueryString["ID"]));
			ViewDR.DataSource = DAL.ViewData.MyDT("SELECT officeletteractionid,officeletterid,guestid,text,unitid,date FROM OfficeLetterAction where OfficeLetterID=@OfficeLetterID and   unitid=@UnitID order by officeletteractionid desc", SP);
			ViewDR.DataBind();
		}
		protected void SaveBTN_Click(object sender, System.EventArgs e)
		{
			if (textTB.Text.Trim() == "")
			{
				Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "FillStarItem").ToString());
				return;
			}
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@officeletterid", Tools.Tools.ConvertToInt32(Request.QueryString["ID"]));
			SP.AddWithValue("@guestid", DAL.CheckData.CheckTokenGuestUserID());
			SP.AddWithValue("@text", textTB.Text.Trim());
			SP.AddWithValue("@unitid", Tools.Tools.GetViewUnitID);
			if (ViewState["OfficeLetterActionS"] != null && ViewState["OfficeLetterActionS"].ToString().Trim() != "")
				DAL.ExecuteData.AddData("update OfficeLetterAction set officeletterid = @officeletterid,guestid = @guestid,text = @text,unitid = @unitid where OfficeLetterActionID=" + ViewState["OfficeLetterActionS"].ToString().Trim(), SP);
			else
			{
				DAL.ExecuteData.AddData("INSERT INTO OfficeLetterAction (officeletterid ,guestid ,text ,unitid ) VALUES ( @officeletterid ,@guestid ,@text ,@unitid  )", SP);
			}
			ViewState["OfficeLetterActionS"] = null;
			UpdateGrid();
			SaveBTN.Text = "اضافه شود";// GetGlobalResourceObject("resource", "SaveBTNText").ToString();
			textTB.Text = "";
		}
		protected void ViewDR_ItemCommand(object source, RepeaterCommandEventArgs e)
		{/*
			string ComArg = e.CommandArgument.ToString();
			return;
			if(e.CommandName == "DEL")
			{				
				DAL.ExecuteData.DeleteData("delete from OfficeLetterAction where OfficeLetterActionID=" + ComArg);
			}
			else if(e.CommandName == "EDIT")
			{
				SqlDataReader MyRead = DAL.ViewData.MyDR1("select officeletteractionid,officeletterid,guestid,text,unitid,date from OfficeLetterAction where OfficeLetterActionID=" + ComArg);

				if (MyRead.Read())
				{
					ViewState["OfficeLetterActionS"] = Tools.MyCL.MGInt(MyRead, 0).ToString().Trim();
					textTB.Text = Tools.MyCL.MGStr(MyRead, 3);
					SaveBTN.Text = "ويرايش شود";
				}

				MyRead.Close(); MyRead.Dispose();
			}*/
		}
	}
}