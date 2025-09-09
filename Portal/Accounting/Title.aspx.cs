using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Accounting
{
	public partial class Title : System.Web.UI.Page
	{
		public string OptionItems, TafzilItems;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (Accounting.AccountingUserID == -1)
				Response.Redirect("/Accounting/Logins.aspx");
			Tools.Tools.SetPageSeo(Page, "Accounting/Title.aspx");
			Tools.Tools.SetPageHit(this.Page.ToString(), this.Page.ClientQueryString);
			if(!IsPostBack)
			{
				FillData();
			}
		}

		private void FillData()
		{
			OptionItems="";
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT AccountingCodingID, Name, Code, [Level] FROM AccountingCoding WHERE (AccountingCompanyID = " + Accounting.AccountingCompanyID + ") and (accountinguserid=" + Accounting.AccountingUserID + ") ORDER BY [Level]");
			while (MyRead.Read())
			{
				string Level = Tools.MyCL.MGStr(MyRead, 3);
				string Cod = GetParentCode(Level);
				if(Level.Length==2)
				{
					OptionItems += "</optgroup><optgroup label=\"" + Cod +"-"+ Tools.MyCL.MGStr(MyRead, 1) + "\">";
				}
				else
				{
					OptionItems += " <option value=\"" + Tools.MyCL.MGInt(MyRead, 0) + "\">" + Cod + "-" + Tools.MyCL.MGStr(MyRead, 1) + "</option>";
				}
			}
			OptionItems = OptionItems.Substring(11) + "</optgroup>";
			MyRead.Close(); MyRead.Dispose();
			//////////////////////////////////////////////////////////////
			TafzilItems = "";
			int oldtype = -1;
			string[] typename = { "حقیقی", "حقوقی", "کارمند", "کالا" };
			MyRead = DAL.ViewData.MyDR1("SELECT AccountingCustomerID, Type, Name, Family FROM AccountingCustomer WHERE (AccountingCompanyID = " + Accounting.AccountingCompanyID + ") and (accountinguserid=" + Accounting.AccountingUserID + ") ORDER BY type");
			while (MyRead.Read())
			{
				int curtype = Tools.MyCL.MGInt(MyRead, 1);
				
				if(oldtype!=curtype)
				{
					TafzilItems += "</optgroup><optgroup label=\"" + typename[curtype] + "\">";
					TafzilItems += " <option value=\"" + Tools.MyCL.MGInt(MyRead, 0) + "\">" + Tools.MyCL.MGInt(MyRead, 0) + "-" + Tools.MyCL.MGStr(MyRead, 2) + " " + Tools.MyCL.MGStr(MyRead, 3) + "</option>";
				}
				else
				{
					TafzilItems += " <option value=\"" + Tools.MyCL.MGInt(MyRead, 0) + "\">" + Tools.MyCL.MGInt(MyRead, 0) + "-" + Tools.MyCL.MGStr(MyRead, 2) +" "+ Tools.MyCL.MGStr(MyRead, 3) + "</option>";
				}
				oldtype = curtype;
			}
			TafzilItems = TafzilItems.Substring(11) + "</optgroup>";
			MyRead.Close(); MyRead.Dispose();
			/////////////////////////////////////////////////////
			Tools.Accounting.FillYearDL(YearDL,Accounting.AccountingCompanyID,Accounting.AccountingUserID);
		}
		public string GetParentCode(string CurID)
		{
			string CurLev = CurID;//DAL.ExecuteData.CNTDataStr("SELECT [Level]  FROM AccountingCoding WHERE (AccountingCodingID = " + CurID + ")");
			string RetSrt = "";
			for (int i = 0; i < (CurLev.Length / 2); i++)
			{
				RetSrt = RetSrt + DAL.ExecuteData.CNTDataStr("SELECT code  FROM AccountingCoding WHERE ([Level] = '" + CurLev.Substring(0, ((i + 1) * 2)) + "')");
			}
			return RetSrt;
		}

		protected void SaveBTN_Click(object sender, System.EventArgs e)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@accountinguserid", Accounting.AccountingUserID);
			SP.AddWithValue("@accountingcompanyid", Accounting.AccountingCompanyID);
			SP.AddWithValue("@titleno", titlenoTB.Text.Trim());
			SP.AddWithValue("@date",Tools.Calender.PersianToEnglishCheck(datefaTB.Text));
			SP.AddWithValue("@datefa", datefaTB.Text.Trim());
			SP.AddWithValue("@titleno1", titleno1TB.Text.Trim());
			SP.AddWithValue("@atfno", atfnoTB.Text.Trim());
			SP.AddWithValue("@type", typeTB.Text.Trim());
			SP.AddWithValue("@state", 1);
			SP.AddWithValue("@description", descriptionTB.Text.Trim());
			DAL.ExecuteData.AddData("INSERT INTO AccountingTitle (accountingcompanyid ,accountinguserid ,titleno ,date ,datefa ,titleno1 ,atfno ,type ,state ,description ) VALUES ( @accountingcompanyid ,@accountinguserid ,@titleno ,@date ,@datefa ,@titleno1 ,@atfno ,@type ,@state ,@description )", SP);
			int curID = DAL.ExecuteData.CNTData("select IDENT_CURRENT('AccountingTitle')");
			string[] mytxt = Regex.Split(TempTB.Text, "{#}");
			int b = 0;
			for (int i = 0; i < mytxt.Length; i++)
			{
				if (i + 4 > mytxt.Length)
					continue;
				SP = new SqlCommand().Parameters;
				SP.AddWithValue("@accountingtitleid", curID);
				SP.AddWithValue("@accountinguserid", Accounting.AccountingUserID);
				SP.AddWithValue("@accountingcompanyid", Accounting.AccountingCompanyID);
				SP.AddWithValue("@sort", ++b);
				SP.AddWithValue("@accountingcodingid", mytxt[i]);
				SP.AddWithValue("@accountingcustomerid", mytxt[i+1]);
				SP.AddWithValue("@Sharh", mytxt[i + 2]);
				SP.AddWithValue("@bedehkar", mytxt[i+3]);
				SP.AddWithValue("@bestankar", mytxt[i+4]);
				SP.AddWithValue("@refno", mytxt[i+5]);
				SP.AddWithValue("@refdate",Tools.Calender.PersianToEnglishCheck( mytxt[i+6]));
				SP.AddWithValue("@refdatefa", mytxt[i+6]);
				i = i + 6;
				DAL.ExecuteData.AddData("INSERT INTO AccountingTitleItem (accountingtitleid ,accountingcompanyid ,accountinguserid ,sort ,accountingcodingid ,accountingcustomerid ,bedehkar ,bestankar ,refno ,refdate ,refdatefa ) VALUES ( @accountingtitleid ,@accountingcompanyid ,@accountinguserid ,@sort ,@accountingcodingid ,@accountingcustomerid ,@bedehkar ,@bestankar ,@refno ,@refdate ,@refdatefa )", SP);
			}

			titlenoTB.Text = "";			
			datefaTB.Text = "";
			titleno1TB.Text = "";
			atfnoTB.Text = "";
			typeTB.Text = "";
			descriptionTB.Text = "";

		}

		protected void ServerSaveTempBTN_Click(object sender, EventArgs e)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@accountinguserid", Accounting.AccountingUserID);
			SP.AddWithValue("@accountingcompanyid", Accounting.AccountingCompanyID);
			SP.AddWithValue("@titleno", titlenoTB.Text.Trim());
			SP.AddWithValue("@date", Tools.Calender.PersianToEnglishCheck(datefaTB.Text));
			SP.AddWithValue("@datefa", datefaTB.Text.Trim());
			SP.AddWithValue("@titleno1", titleno1TB.Text.Trim());
			SP.AddWithValue("@atfno", atfnoTB.Text.Trim());
			SP.AddWithValue("@type", typeTB.Text.Trim());
			SP.AddWithValue("@state",0);
			SP.AddWithValue("@description", descriptionTB.Text.Trim());
			DAL.ExecuteData.AddData("INSERT INTO AccountingTitle (accountingcompanyid ,accountinguserid ,titleno ,date ,datefa ,titleno1 ,atfno ,type ,state ,description ) VALUES ( @accountingcompanyid ,@accountinguserid ,@titleno ,@date ,@datefa ,@titleno1 ,@atfno ,@type ,@state ,@description )", SP);
			int curID = DAL.ExecuteData.CNTData("select IDENT_CURRENT('AccountingTitle')");
			string[] mytxt = Regex.Split(TempTB.Text, "{#}");
			int b = 0;
			for (int i = 0; i < mytxt.Length; i++)
			{
				if (i + 4 > mytxt.Length)
					continue;
				SP = new SqlCommand().Parameters;
				SP.AddWithValue("@accountingtitleid", curID);
				SP.AddWithValue("@accountinguserid", Accounting.AccountingUserID);
				SP.AddWithValue("@accountingcompanyid", Accounting.AccountingCompanyID);
				SP.AddWithValue("@sort", ++b);
				SP.AddWithValue("@accountingcodingid", mytxt[i]);
				SP.AddWithValue("@accountingcustomerid", mytxt[i + 1]);
				SP.AddWithValue("@Sharh", mytxt[i + 2]);
				SP.AddWithValue("@bedehkar", mytxt[i + 3]);
				SP.AddWithValue("@bestankar", mytxt[i + 4]);
				SP.AddWithValue("@refno", mytxt[i + 5]);
				SP.AddWithValue("@refdate", Tools.Calender.PersianToEnglishCheck(mytxt[i + 6]));
				SP.AddWithValue("@refdatefa", mytxt[i + 6]);
				i = i + 6;
				DAL.ExecuteData.AddData("INSERT INTO AccountingTitleItem (accountingtitleid ,accountingcompanyid ,accountinguserid ,sort ,accountingcodingid ,accountingcustomerid ,bedehkar ,bestankar ,refno ,refdate ,refdatefa ) VALUES ( @accountingtitleid ,@accountingcompanyid ,@accountinguserid ,@sort ,@accountingcodingid ,@accountingcustomerid ,@bedehkar ,@bestankar ,@refno ,@refdate ,@refdatefa )", SP);
			}

			titlenoTB.Text = "";
			datefaTB.Text = "";
			titleno1TB.Text = "";
			atfnoTB.Text = "";
			typeTB.Text = "";
			descriptionTB.Text = "";
		}
	}
}