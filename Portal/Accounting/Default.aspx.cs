using System;
using System.Web;

namespace Portal.Accounting
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			
			if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToLower() == "ex")
			{
				Tools.Tools.CookieRemove("MyAccountingUserID");
				Tools.Tools.CookieRemove("MyAccountingCompanyID");
				HttpCookie httpCookie1 = new HttpCookie("MyAccountingUserID");
				httpCookie1.Value = Tools.MyClass.MyEncry("-1");
				httpCookie1.Expires = DateTime.Now.AddHours(3);
				Response.SetCookie(httpCookie1);
				Response.Redirect("/Accounting/");
			}
			Tools.Tools.SetPageSeo(Page, "Accounting/Default.aspx");
			Tools.Tools.SetPageHit(this.Page.ToString(), this.Page.ClientQueryString);
		
		}
	}
}