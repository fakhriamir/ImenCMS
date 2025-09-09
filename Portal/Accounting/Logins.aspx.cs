using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Tools;

namespace Portal.Accounting
{
	public partial class Logins : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Tools.Tools.SetPageSeo(Page, "Accounting/Logins.aspx");
			Tools.Tools.SetPageHit(this.Page.ToString(), this.Page.ClientQueryString);
		}
		protected void LoginBTN_Click(object sender, EventArgs e)
		{

			if (SecurityImageTB.Text != Tools.Tools.SecurityImageCode)
			{
				Tools.Tools.Alert(Page, "کد امنیتی صحیح نمی باشد");
				return;
			}

			if (ViewState["myLoginCNT"] == null || ViewState["myLoginCNT"].ToString().Trim() == "")
				ViewState["myLoginCNT"] = 0;

			ViewState["myLoginCNT"] = (Tools.Tools.ConvertToInt32(ViewState["myLoginCNT"]) + 1);
			//if (UserTB.Text != "amir")
			//{
			//    MessLB.Text = "سایت در حال به روز رسانی می باشد";
			//    return;
			//}
			if (Tools.Tools.ConvertToInt32(ViewState["myLoginCNT"]) > 3)
			{
				Tools.Tools.Alert(Page,"شما بيش از حد مجاز تلاش براي ورود داشته ايد");
				return;
			}
			if (LoginUser(LoginUserTB.Text, LoginPassTB.Text))
				Response.Redirect("/Accounting/default.aspx");
			else
				Tools.Tools.Alert(Page,"نام کاربر و رمز عبور صحيح نمي باشد");
		}
		public bool LoginUser(string UserName, string Pass)
		{
			//HttpContext.Current.Session.Abandon();
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@UserName", UserName);
			SP.AddWithValue("@Pass",Tools.Tools.MyEncry(Pass));
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT AccountingUserID,State FROM  AccountingUser WHERE (UserName = @UserName) AND (Pass = @Pass)", SP);
			if (MyRead.Read())
			{
				if (Tools.MyCL.MGInt(MyRead, 1) == 2)
				{
					Tools.Tools.Alert(Page, "کاربر گرامی، کاربر شما بلوک شده است با پشتیبان تماس بگیرید");
					return false;
				}
				HttpCookie httpCookie1 = new HttpCookie("MyAccountingUserID");
				//httpCookie1.Name=;
				httpCookie1.Value = MyClass.MyEncry(Tools.MyCL.MGInt(MyRead, 0).ToString());
				httpCookie1.Expires = DateTime.Now.AddHours(20);
				Response.SetCookie(httpCookie1);

				HttpCookie httpCookie2 = new HttpCookie("MyAccountingCompanyID");				
				httpCookie2.Value = MyClass.MyEncry(DAL.ExecuteData.CNTData("SELECT TOP (1) AccountingCompanyID  FROM AccountingCompany WHERE (AccountingUserID = " + Tools.MyCL.MGInt(MyRead, 0).ToString() + ")").ToString());
				httpCookie2.Expires = DateTime.Now.AddHours(20);
				Response.SetCookie(httpCookie2);
				
				MyRead.Close();
				MyRead.Dispose();
				return true;
			}
			MyRead.Close();
			MyRead.Dispose();
			return false;
		}
	}
}