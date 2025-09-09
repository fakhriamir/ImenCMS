using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Accounting
{
	public partial class Year : System.Web.UI.Page
	{
		int accountingcompanyid;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (Accounting.AccountingUserID == -1)
				Response.Redirect("/Accounting/Logins.aspx");
			if (Request.QueryString["ID"] == null || Request.QueryString["ID"] == "")
				return;
			accountingcompanyid = Tools.Tools.ConvertToInt32(Request.QueryString["ID"]);
			if (accountingcompanyid <= 0)
				return;
			if (!IsPostBack)
				UpdateGrid();
			
		}

		
		void UpdateGrid()
		{
			ViewDR.DataSource = DAL.ViewData.MyDT("SELECT accountingyearid,accountingcompanyid,accountinguserid,name,startdate,enddate,def FROM AccountingYear where accountinguserid=" + Accounting.AccountingUserID + " and accountingcompanyid=" + accountingcompanyid);
			ViewDR.DataBind();
		}
		protected void SaveBTN_Click(object sender, System.EventArgs e)
		{
			if (nameTB.Text.Trim() == "")
			{
				Tools.Tools.Alert(Page, "نام شرکت را وارد نمایید");
				return;
			}
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@accountingcompanyid", accountingcompanyid);
			SP.AddWithValue("@accountinguserid",Accounting.AccountingUserID);
			SP.AddWithValue("@name", nameTB.Text.Trim());
			SP.AddWithValue("@startdate", startdateTB.Text.Trim());
			SP.AddWithValue("@enddate", enddateTB.Text.Trim());
			SP.AddWithValue("@def", DefCB.Checked?1:0);

			if (ViewState["AccountingYearS"] != null && ViewState["AccountingYearS"].ToString().Trim() != "")
				DAL.ExecuteData.AddData("update AccountingYear set def=@def,accountingcompanyid = @accountingcompanyid,accountinguserid = @accountinguserid,name = @name,startdate = @startdate,enddate = @enddate where AccountingYearID=" + ViewState["AccountingYearS"].ToString().Trim(), SP);
			else
			{
				DAL.ExecuteData.AddData("INSERT INTO AccountingYear (accountingcompanyid ,accountinguserid ,name ,startdate ,enddate,def ) VALUES ( @accountingcompanyid ,@accountinguserid ,@name ,@startdate ,@enddate,@def )", SP);
			}
			ViewState["AccountingYearS"] = null;
			UpdateGrid();
			
			nameTB.Text = "";
			startdateTB.Text = "";
			enddateTB.Text = "";

		}

		protected void ViewDR_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			string ComArg = e.CommandArgument.ToString();
			if (e.CommandName == "DEL")
			{
				DAL.ExecuteData.DeleteData("delete from AccountingYear where AccountingYearID=" + ComArg);
			}
			else if (e.CommandName == "EDIT")
			{
				
				SqlDataReader MyRead = DAL.ViewData.MyDR1("select  accountingyearid,accountingcompanyid,accountinguserid,name,startdate,enddate,def from AccountingYear where AccountingYearID=" + ComArg);

				if (MyRead.Read())
				{
					ViewState["AccountingYearS"] = Tools.MyCL.MGInt(MyRead, 0).ToString().Trim();
					nameTB.Text = Tools.MyCL.MGStr(MyRead, 3);
					startdateTB.Text = Tools.MyCL.MGStr(MyRead, 4);
					enddateTB.Text = Tools.MyCL.MGStr(MyRead, 5);
					DefCB.Checked = Tools.MyCL.MGInt(MyRead, 6) == 1 ? true : false;
				}
				MyRead.Close(); MyRead.Dispose();
				SaveBTN.Text = "ويرايش شود";
			}
		}
	}
}