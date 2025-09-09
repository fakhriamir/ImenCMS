using System;
using System.Web;
using System.Threading;
using System.Globalization;
namespace NewsService
{
    public partial class Logins : System.Web.UI.Page
    {
		private void Page_Load(object sender, System.EventArgs e)
		{
            if (Request.Cookies["MyLanguage"] != null)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Request.Cookies["MyLanguage"].Value);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Request.Cookies["MyLanguage"].Value);
            }
			if (!IsPostBack)
			{
				ADAL.A_ExecuteData.InsertData("INSERT INTO AdminLogs (IP) VALUES ('" + Tools.Tools.GetUserIPAddress() + "')",null,false);
				if (Request.Cookies["MyLanguage"] != null)
				{
					string MyLang = Request.Cookies["MyLanguage"].Value;
					Tools.Tools.SetDropDownListValue(LanguageDL, MyLang);
				}
			}
			//Response.Write(Response.Cookies.Keys.Count+"ddddd");
			//for (int i = 0; i < Response.Cookies.Keys.Count; i++)
			//{
			//    Response.Write(Response.Cookies.Keys[i]+"<br>");				
			//}
			//Request.UrlReferrer
			// if (UserTB.Text == "amir")
			// {
			//PassTB.Text = "ali110ali";
			//LoginBTN_Click(null, null);
			// }
		}
		protected void LoginBTN_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
			//Response.Write("dddddddddddddddd");
			if (ViewState["myLoginCNT"] == null || ViewState["myLoginCNT"].ToString().Trim() == "")
				ViewState["myLoginCNT"] = 0;

			ViewState["myLoginCNT"] = (Tools.Tools.ConvertToInt32(ViewState["myLoginCNT"])+1);
            //if (UserTB.Text != "amir")
            //{
            //    Tools.Tools.Alert(Page,"سایت در حال به روز رسانی می باشد";
            //    return;
            //}
			if (Tools.Tools.ConvertToInt32(ViewState["myLoginCNT"]) > 3)
			{
				Tools.Tools.Alert(Page,"شما بيش از حد مجاز تلاش براي ورود داشته ايد");
				return;
			}
			if (ADAL.A_CheckData.LoginUser(UserTB.Text, PassTB.Text))
			{
				//Response.Write(HttpContext.Current.Request.Cookies["LoginUserID"].Value+"lllllll<br>");
				//Response.Write(HttpContext.Current.Request.Cookies["LoginUnitID"].Value+"uuuuuuu");

				HttpCookie mylang = new HttpCookie("MyLanguage");
				mylang.Value = LanguageDL.SelectedValue;
				mylang.Expires = DateTime.Now.AddYears(1);
				Response.Cookies.Add(mylang);
				Response.Redirect("default.aspx");
			}
			else
				Tools.Tools.Alert(Page,"نام کاربر یا رمز عبور صحيح نمي باشد");
        }
    }
}