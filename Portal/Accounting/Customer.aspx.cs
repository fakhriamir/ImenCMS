using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Accounting
{
	public partial class Customer : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (Accounting.AccountingUserID == -1)
				Response.Redirect("/Accounting/Logins.aspx");
			Tools.Tools.SetPageSeo(Page, "Accounting/Customer.aspx");
			Tools.Tools.SetPageHit(this.Page.ToString(), this.Page.ClientQueryString);
			if (!IsPostBack)
				UpdateGrid();
		}
		void UpdateGrid()
		{
			ViewDR.DataSource = DAL.ViewData.MyDT("SELECT accountingcustomerid,accountinguserid,accountingcompanyid,type,name,family,accountingcodingid,melicode,tel,fax,mobile,website,email,reagent,ecocode,sabtcode FROM AccountingCustomer where accountingcompanyid=" + Accounting.AccountingCompanyID + " and accountinguserid="+Accounting.AccountingUserID);
			ViewDR.DataBind();
			accountingcodingDL.DataSource = DAL.ViewData.MyDT("SELECT AccountingCodingID, Name  FROM AccountingCoding  where accountingcompanyid=" + Accounting.AccountingCompanyID + " and accountinguserid=" + Accounting.AccountingUserID + " ORDER BY [Level]");
			accountingcodingDL.DataBind();
		}
		protected void SaveBTN_Click(object sender, System.EventArgs e)
		{
			if (nameTB.Text.Trim() == "" || familyTB.Text.Trim() == "")
			{
				Tools.MyClass.Alert(Page, GetGlobalResourceObject("resource", "FillStarItem").ToString());
				return;
			}
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@accountinguserid", Accounting.AccountingUserID);
			SP.AddWithValue("@accountingcompanyid", Accounting.AccountingCompanyID);
			SP.AddWithValue("@type", TypeDL.SelectedValue);
			SP.AddWithValue("@matter", MatterDL.SelectedValue);
			SP.AddWithValue("@name", nameTB.Text.Trim());
			SP.AddWithValue("@family", familyTB.Text.Trim());
			SP.AddWithValue("@accountingcodingid", accountingcodingDL.SelectedValue);
			SP.AddWithValue("@melicode", melicodeTB.Text.Trim());
			SP.AddWithValue("@tel", telTB.Text.Trim());
			SP.AddWithValue("@fax", faxTB.Text.Trim());
			SP.AddWithValue("@mobile", mobileTB.Text.Trim());
			SP.AddWithValue("@website", websiteTB.Text.Trim());
			SP.AddWithValue("@email", emailTB.Text.Trim());
			SP.AddWithValue("@reagent", reagentTB.Text.Trim());
			SP.AddWithValue("@ecocode", ecocodeTB.Text.Trim());
			SP.AddWithValue("@sabtcode", sabtcodeTB.Text.Trim()); 
			if (ViewState["AccountingCustomerS"] != null && ViewState["AccountingCustomerS"].ToString().Trim() != "")
				DAL.ExecuteData.AddData("update AccountingCustomer set matter=@matter, accountinguserid = @accountinguserid,accountingcompanyid = @accountingcompanyid,type = @type,name = @name,family = @family,accountingcodingid = @accountingcodingid,melicode = @melicode,tel = @tel,fax = @fax,mobile = @mobile,website = @website,email = @email,reagent = @reagent,ecocode = @ecocode,sabtcode = @sabtcode where AccountingCustomerID=" + ViewState["AccountingCustomerS"].ToString().Trim(), SP);
			else
			{
				DAL.ExecuteData.AddData("INSERT INTO AccountingCustomer (accountinguserid ,accountingcompanyid ,type ,name ,family ,accountingcodingid ,melicode ,tel ,fax ,mobile ,website ,email ,reagent ,ecocode ,sabtcode,matter ) VALUES ( @accountinguserid ,@accountingcompanyid ,@type ,@name ,@family ,@accountingcodingid ,@melicode ,@tel ,@fax ,@mobile ,@website ,@email ,@reagent ,@ecocode ,@sabtcode ,@matter)", SP);
			}	

			ViewState["AccountingCustomerS"] = null;
			UpdateGrid();
			
			nameTB.Text = "";
			familyTB.Text = "";
			melicodeTB.Text = "";
			telTB.Text = "";
			faxTB.Text = "";
			mobileTB.Text = "";
			websiteTB.Text = "";
			emailTB.Text = "";
			reagentTB.Text = "";
			ecocodeTB.Text = "";
			sabtcodeTB.Text = "";
		}

		protected void ViewDR_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			string ComArg = e.CommandArgument.ToString();
			if (e.CommandName == "UP")
			{
				//Tools.MyClass.SortFiled(Tools.MyClass.MySortType.UP, ComArg, "Contacts", "ContactID");
			}
			else if (e.CommandName == "DOWN")
			{
				//Tools.MyClass.SortFiled(Tools.MyClass.MySortType.Down, ComArg, "Contacts", "ContactID");
			}
			else if (e.CommandName == "DEL")
			{
				
					DAL.ExecuteData.DeleteData("delete from AccountingCustomer where AccountingCustomerID=" + ComArg);
			}
			else if (e.CommandName == "EDIT")
			{
				SqlDataReader MyRead = DAL.ViewData.MyDR1("select accountingcustomerid,accountinguserid,accountingcompanyid,type,name,family,accountingcodingid,melicode,tel,fax,mobile,website,email,reagent,ecocode,sabtcode,matter from AccountingCustomer where AccountingCustomerID=" + ComArg);
				if (MyRead.Read())
				{
					ViewState["AccountingCustomerS"] = Tools.MyCL.MGInt(MyRead, 0).ToString().Trim();
					//accountinguseridTB.Text = Tools.MyCL.MGInt(MyRead, 1).ToString();
					//accountingcompanyidTB.Text = Tools.MyCL.MGInt(MyRead, 2).ToString();
					Tools.Tools.SetDropDownListValue(TypeDL, Tools.MyCL.MGInt(MyRead, 3));
				
					nameTB.Text = Tools.MyCL.MGStr(MyRead, 4);
					familyTB.Text = Tools.MyCL.MGStr(MyRead, 5);
					Tools.Tools.SetDropDownListValue(accountingcodingDL,Tools.MyCL.MGInt(MyRead, 6).ToString());
					melicodeTB.Text = Tools.MyCL.MGStr(MyRead, 7);
					telTB.Text = Tools.MyCL.MGStr(MyRead, 8);
					faxTB.Text = Tools.MyCL.MGStr(MyRead, 9);
					mobileTB.Text = Tools.MyCL.MGStr(MyRead, 10);
					websiteTB.Text = Tools.MyCL.MGStr(MyRead, 11);
					emailTB.Text = Tools.MyCL.MGStr(MyRead, 12);
					reagentTB.Text = Tools.MyCL.MGStr(MyRead, 13);
					ecocodeTB.Text = Tools.MyCL.MGStr(MyRead, 14);
					sabtcodeTB.Text = Tools.MyCL.MGStr(MyRead, 15);
					Tools.Tools.SetDropDownListValue(MatterDL, Tools.MyCL.MGInt(MyRead, 16).ToString());
				
				}
				MyRead.Close(); MyRead.Dispose();
				SaveBTN.Text = "ويرايش شود";
			}
		}
	}
}