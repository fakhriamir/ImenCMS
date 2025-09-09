using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Accounting
{
	public partial class Accounting : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//Response.Write(Response.Cookies["MySMSUserID"].Value);
			if (AccountingUserID != -1)
			{
				ViewInfo();				
			}			
			Portal.Default.Adv.PageID = 23;
			AdvertisePH.Controls.Add(LoadControl("/Def/Adv.ascx"));
		}
		private void ViewInfo()
		{
			UserInfo.Visible = true;
			UserMenu.Visible = true;
			string OutText = "<img src='/Images/Users.png' style='width:25px;float:right'>&nbsp;&nbsp; کد کاربری:" + ((AccountingUserID)) + " - ";
			OutText += DAL.ExecuteData.CNTDataStr("SELECT Name  FROM AccountingUser where AccountinguserID=" + AccountingUserID);
			OutText += " خوش آمدید ";
			OutText += "نوع کاربری: " + Tools.Accounting.GetUserState(AccountingUserID);
			OutText += "<div class='AccountingHomeIcon'><a href='/Accounting/Default-EX.aspx'><img src='/Images/Telegram/exit.png' alt='خروج' title='خروج'></a><a href='/Accounting/'><img src='/Images/Telegram/home.png' ></a></div>";
			UserInfo.InnerHtml = OutText;
			if (!IsPostBack)
			{
				FillCompanyDL();
				//if (Accounting.AccountingCompanyID != null && Accounting.AccountingCompanyID != "")
				//	Tools.Tools.SetDropDownListValue(CompanyDL, Accounting.AccountingCompanyID);
				//else
				//	Tools.Tools.CookieWrite("CompanyID", CompanyDL.SelectedValue, 1000);
				//SP.AddWithValue("@AccountingCompanyID", CompanyDL.SelectedValue);
				//YearDL.DataSource = DAL.ViewData.MyDT("SELECT AccountingYearID, Name  FROM AccountingYear  WHERE (AccountingCompanyID = @AccountingCompanyID) AND (AccountingUserID = @AccountingUserID)", SP);
				//YearDL.DataBind();
				//if (Tools.Tools.CookieRead("YearID") != null && Tools.Tools.CookieRead("YearID") != "")
				//	Tools.Tools.SetDropDownListValue(CompanyDL, Tools.Tools.CookieRead("YearID"));
				//else
				//	Tools.Tools.CookieWrite("YearID", YearDL.SelectedValue, 1000);
			}		

		}
		public static int AccountingUserID
		{
			get
			{
				if (HttpContext.Current.Request.Cookies["MyAccountingUserID"] != null)
				{
					int Val = Tools.Tools.ConvertToInt32(Tools.MyClass.MyDecry(HttpContext.Current.Request.Cookies["MyAccountingUserID"].Value));
					if (Val == 0)
						return -1;
					return Val;
				}
				return -1;
			}
		}
		public static int AccountingCompanyID
		{
			get
			{
				if (HttpContext.Current.Request.Cookies["MyAccountingCompanyID"] != null)
				{
					int Val = Tools.Tools.ConvertToInt32(Tools.MyClass.MyDecry(HttpContext.Current.Request.Cookies["MyAccountingCompanyID"].Value));
					if (Val == 0)
						return -1;
					return Val;
				}
				return -1;
			}
			set
			{
				HttpCookie httpCookie2 = new HttpCookie("MyAccountingCompanyID");
				httpCookie2.Value = Tools.MyClass.MyEncry(value.ToString());
				httpCookie2.Expires = DateTime.Now.AddHours(20);
				HttpContext.Current.Response.SetCookie(httpCookie2);
			}
		}
		void FillCompanyDL()
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@AccountingUserID", Accounting.AccountingUserID);
			CompanyDL.DataSource = DAL.ViewData.MyDT("SELECT AccountingCompanyID, Name  FROM AccountingCompany WHERE (AccountingUserID =@AccountingUserID) order by AccountingCompanyID", SP);
			CompanyDL.DataBind();
			Tools.Tools.SetDropDownListValue(CompanyDL, AccountingCompanyID);
		}
		protected void CompanyDL_SelectedIndexChanged(object sender, EventArgs e)
		{
			AccountingCompanyID =Tools.Tools.ConvertToInt32(CompanyDL.SelectedValue);
			//FillCompanyDL();
			Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
			//SqlParameterCollection SP = new SqlCommand().Parameters;
			//SP.AddWithValue("@AccountingUserID", Accounting.AccountingUserID);
			//SP.AddWithValue("@AccountingCompanyID", CompanyDL.SelectedValue);
			//YearDL.DataSource = DAL.ViewData.MyDT("SELECT AccountingYearID, Name  FROM AccountingYear  WHERE (AccountingCompanyID = @AccountingCompanyID) AND (AccountingUserID = @AccountingUserID)", SP);
			//YearDL.DataBind();
		}
		protected void YearDL_SelectedIndexChanged(object sender, EventArgs e)
		{
			Tools.Tools.CookieWrite("YearID", CompanyDL.SelectedValue, 1000);
		}
	}
}