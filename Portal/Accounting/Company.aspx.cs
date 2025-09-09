using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Accounting
{
	public partial class CompanyInfo : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (Accounting.AccountingUserID== -1)
				Response.Redirect("/Accounting/Logins.aspx");
			Tools.Tools.SetPageSeo(Page, "Accounting/Company.aspx");
			Tools.Tools.SetPageHit(this.Page.ToString(), this.Page.ClientQueryString);
			if (!IsPostBack)
				UpdateGrid();
		}
		void UpdateGrid()
		{
			ViewDR.DataSource = DAL.ViewData.MyDR1("SELECT accountingcompanyid,accountinguserid,name,address,postalcode,melicode,ecocode,sabtcode,telno,ceoname,ceomobile,ceoemail,website,email FROM AccountingCompany where accountinguserid=" + Accounting.AccountingUserID);
			ViewDR.DataBind();
		}
		protected void SaveBTN_Click(object sender, System.EventArgs e)
		{
			if(nameTB.Text.Trim()=="")
			{
				Tools.Tools.Alert(Page,"نام شرکت را وارد نمایید");
				return;
			}
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@accountinguserid", Accounting.AccountingUserID);
			SP.AddWithValue("@name", nameTB.Text.Trim());
			SP.AddWithValue("@address", addressTB.Text.Trim());
			SP.AddWithValue("@postalcode", postalcodeTB.Text.Trim());
			SP.AddWithValue("@melicode", melicodeTB.Text.Trim());
			SP.AddWithValue("@ecocode", ecocodeTB.Text.Trim());
			SP.AddWithValue("@sabtcode", sabtcodeTB.Text.Trim());
			SP.AddWithValue("@telno", telnoTB.Text.Trim());
			SP.AddWithValue("@ceoname", ceonameTB.Text.Trim());
			SP.AddWithValue("@ceomobile", ceomobileTB.Text.Trim());
			SP.AddWithValue("@ceoemail", ceoemailTB.Text.Trim());
			SP.AddWithValue("@website", websiteTB.Text.Trim());
			SP.AddWithValue("@email", emailTB.Text.Trim());
			if (nameTB.Text.Trim() == "")
			{
				Tools.MyClass.Alert(Page, GetGlobalResourceObject("resource", "FillStarItem").ToString());
				return;
			}
			if (ViewState["AccountingCompanyS"] != null && ViewState["AccountingCompanyS"].ToString().Trim() != "")
				DAL.ExecuteData.AddData("update AccountingCompany set accountinguserid = @accountinguserid,name = @name,address = @address,postalcode = @postalcode,melicode = @melicode,ecocode = @ecocode,sabtcode = @sabtcode,telno = @telno,ceoname = @ceoname,ceomobile = @ceomobile,ceoemail = @ceoemail,website = @website,email = @email where AccountingCompanyID=" + ViewState["AccountingCompanyS"].ToString().Trim(), SP);
			else
			{
				DAL.ExecuteData.AddData("INSERT INTO AccountingCompany (accountinguserid ,name ,address ,postalcode ,melicode ,ecocode ,sabtcode ,telno ,ceoname ,ceomobile ,ceoemail ,website ,email ) VALUES ( @accountinguserid ,@name ,@address ,@postalcode ,@melicode ,@ecocode ,@sabtcode ,@telno ,@ceoname ,@ceomobile ,@ceoemail ,@website ,@email )", SP);
			}

			
			ViewState["AccountingCompanyS"] = null;
			UpdateGrid();
			//SaveBTN.Text = GetGlobalResourceObject("resource", "SaveBTNText").ToString();
			nameTB.Text = "";
			addressTB.Text = "";
			postalcodeTB.Text = "";
			melicodeTB.Text = "";
			ecocodeTB.Text = "";
			sabtcodeTB.Text = "";
			telnoTB.Text = "";
			ceonameTB.Text = "";
			ceomobileTB.Text = "";
			ceoemailTB.Text = "";
			websiteTB.Text = "";
			emailTB.Text = "";

		}

		protected void ViewDR_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			string ComArg = e.CommandArgument.ToString();
			
			if (e.CommandName == "DEL")
			{
				
					DAL.ExecuteData.DeleteData("delete from AccountingCompany where AccountingCompanyID=" + ComArg);
			}
			else if (e.CommandName == "EDIT")
			{
				
				SqlDataReader MyRead = DAL.ViewData.MyDR1("select  accountingcompanyid,accountinguserid,name,address,postalcode,melicode,ecocode,sabtcode,telno,ceoname,ceomobile,ceoemail,website,email from AccountingCompany where AccountingCompanyID=" + ComArg);

				if (!MyRead.Read())
					return;
				ViewState["AccountingCompanyS"] = Tools.MyCL.MGInt(MyRead, 0).ToString().Trim();
				nameTB.Text = Tools.MyCL.MGStr(MyRead, 2);
				addressTB.Text = Tools.MyCL.MGStr(MyRead, 3);
				postalcodeTB.Text = Tools.MyCL.MGStr(MyRead, 4);
				melicodeTB.Text = Tools.MyCL.MGStr(MyRead, 5);
				ecocodeTB.Text = Tools.MyCL.MGStr(MyRead, 6);
				sabtcodeTB.Text = Tools.MyCL.MGStr(MyRead, 7);
				telnoTB.Text = Tools.MyCL.MGStr(MyRead, 8);
				ceonameTB.Text = Tools.MyCL.MGStr(MyRead, 9);
				ceomobileTB.Text = Tools.MyCL.MGStr(MyRead, 10);
				ceoemailTB.Text = Tools.MyCL.MGStr(MyRead, 11);
				websiteTB.Text = Tools.MyCL.MGStr(MyRead, 12);
				emailTB.Text = Tools.MyCL.MGStr(MyRead, 13);
				MyRead.Close(); MyRead.Dispose();
				SaveBTN.Text = "ويرايش شود";
			}
		}
	}
}