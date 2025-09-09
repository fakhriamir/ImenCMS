using System;
using System.Data.SqlClient;
using DAL;
using System.Web;
using Tools;

namespace Portal.Accounting
{
	public partial class Register : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Tools.Tools.SetPageSeo(Page, "Accounting/Register.aspx");
			Tools.Tools.SetPageHit(this.Page.ToString(), this.Page.ClientQueryString);
		}
		protected void SaveBTN_Click(object sender, EventArgs e)
		{
			if (!PolicyCB.Checked)
			{
				Tools.Tools.Alert(Page, "قوانین و مقررات سایت را تایید نمایید", "", true);
				return;
			}
			//if (SecurityImageTB.Text != Tools.Tools.SecurityImageCode)
			//{
			//    Tools.Tools.Alert(Page,  "کد امنیتی صحیح نمی باشد", "", true);
			//    return;
			//}
			if (MobileNoTB.Text.Trim().Length!=11)
			{
				Tools.Tools.Alert(Page, "شماره موبایل وارد شده معتبر نمی باشد", "", true);
				return;
			}
			if (usernameTB.Text.Trim() == "" || passTB.Text.Trim() == "" || emailTB.Text.Trim() == ""||MobileNoTB.Text==""||nameTB.Text=="")
			{
				Tools.Tools.Alert(Page, "موارد ستاره دار را تکميل نماييد", "", true);
				return;
			}
			if (passTB.Text.Length < 5)
			{
				Tools.Tools.Alert(Page, "حداقل کاراکتر رمز عبور 5 حرف می باشد", "", true);
				return;
			}
			if (passTB.Text != pass1TB.Text)
			{
				Tools.Tools.Alert(Page, "رمز عبور و تکرار آن با هم مطابقت ندارد", "", true);
				return;
			}
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@username", usernameTB.Text.Trim());
			SP.AddWithValue("@pass", Tools.Tools.MyEncry(passTB.Text));
			SP.AddWithValue("@email", emailTB.Text.Trim());
			SP.AddWithValue("@name", nameTB.Text.Trim());
			SP.AddWithValue("@MobileNo", MobileNoTB.Text.Trim());
			SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);
			if (ExecuteData.CNTData("SELECT COUNT(*) AS Expr1 FROM AccountingUser WHERE (MobileNo = @MobileNo) and (unitid=@UnitID)", SP) != 0)
			{
				Tools.Tools.Alert(Page, "موبایل وارد شده تکراریست", "", true);
				return;
			}
			if (ExecuteData.CNTData("SELECT COUNT(*) AS Expr1 FROM AccountingUser WHERE (UserName = @username) and (unitid=@UnitID)", SP) != 0)
			{
				Tools.Tools.Alert(Page, "نام کاربر شما تکراری می باشد", "", true);
				return;
			}
			if (ExecuteData.CNTData("SELECT COUNT(*) AS Expr1 FROM AccountingUser WHERE (email = @email)  and (unitid=@UnitID)", SP) != 0)
			{
				Tools.Tools.Alert(Page, "ایمیل وارد شده تکراری می باشد", "", true);
				return;
			}
			ExecuteData.AddData("INSERT INTO AccountingUser (username ,pass ,email ,name ,unitid,MobileNo,EndDate ) VALUES ( @username ,@pass ,@email ,@name,@UnitID,@MobileNo,DATEADD(month, 1, GETDATE()) )", SP);
			int ActivCode = DAL.ExecuteData.CNTData("select IDENT_CURRENT('AccountingUser')");
			Tools.Tools.Alert(Page, "عضویت با موفقیت به پایان رسید کد فعال سازی به موبایل شما پیامک شد", "", true);
			HttpCookie httpCookie1 = new HttpCookie("MyAccountingUserID");
			httpCookie1.Value = MyClass.MyEncry(ActivCode.ToString());
			httpCookie1.Expires = DateTime.Now.AddHours(20);
			Response.SetCookie(httpCookie1);			
			ActivCode = (ActivCode * 111 * 3);
			try
			{
				Tools.SMS.SendUserSMS("سلام به www.ImenCMS.com خوش آمدید.\nکد فعال سازی:\"" + ActivCode+"\"", MobileNoTB.Text, 5,0, true, Page);
			}
			catch
			{
				Tools.Tools.Alert(Page,"کد فعال سازی برای شما ارسال نشد. در بخش فعال سازی جهت ارسال اقدام نمایید.");
			}
			Response.Redirect("/Accounting");
			usernameTB.Text = "";
			passTB.Text = "";
			pass1TB.Text = "";
			emailTB.Text = "";
			nameTB.Text = "";
			MobileNoTB.Text = "";
		}
	}
}