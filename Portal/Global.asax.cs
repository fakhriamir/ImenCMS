using System; 
using DAL;
using System.Web;
using System.Threading;
using System.Globalization;
namespace Portal
{
    public class Global : System.Web.HttpApplication
    {
		public static string[] ASCXPages = new string[0];
		public static string MyDefaultType
		{
			get
			{
				string RetVat = Tools.Tools.GetSetting(368, "");
				if (RetVat == "")
					RetVat = CheckData.GetUnitSetting("DefType", Tools.Tools.GetViewUnitID);
				if (RetVat == "") 
					RetVat = "0";
				return RetVat;
			}
		}
		//public static string[] CadrBeforTitle = new string[20], CadrAfterTitle = new string[20], CadrAfterBody = new string[20], CadrAfterFooter = new string[20], CadrTemplate = new string[20];
		protected void Application_Start(object sender, EventArgs e)
		{
            //net.web2ls.www.SiteCheck MySC = new net.web2ls.www.SiteCheck();
            //bool a = MySC.AddSiteURL("APPST", ConfigurationManager.AppSettings["PortalKey"]);
            //Application["MySiteID"] = "";
            //try
            //{
            //    Tools.MyCL.MyDecry(ConfigurationManager.AppSettings["PortalKey"]);
            //    Application.Add("ValidKey", true);
            //}
            //catch
            //{
            //    Application.Add("ValidKey", false);
            //    //Response.Redirect("NoAccess.aspx");
            //}
		}
        protected void Session_Start(object sender, EventArgs e)
        {
			Session.Timeout = 100;
			Session["LangID"] = "";
			Session["ShopingCard"] = "";
			Session["PageDir"] = "";
            Session["ShopingDVD"] = "";
			Session["SMSUserID"] = null;
	    }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
			if (!Tools.Tools.CheckIPBlook(Tools.Tools.GetUserIPAddress()))
				Response.Redirect("/IPBlock.htm");
            //if (!(bool)Application["ValidKey"])
            //    Context.RewritePath("NoAccess.aspx");
			//Application["MySiteID"] = "1";
            //if (null ==Tools.Tools.GetViewUnitID)
            //   Tools.Tools.GetViewUnitID = "";
			//if (Request.Cookies["MyLanguage"] == null)
			//{
			//	HttpCookie mylang = new HttpCookie("MyLanguage");
			//	mylang.Value = Tools.Tools.GetSetting(366,"fa-ir");
			//	mylang.Expires = DateTime.Now.AddYears(1);
			//	Response.Cookies.Add(mylang);
			//}
			if (Request.Cookies["MyLanguage"] != null)
			{
				try
				{
					Thread.CurrentThread.CurrentUICulture = new CultureInfo(Request.Cookies["MyLanguage"].Value);
					//Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Request.Cookies["MyLanguage"].Value);
				}
				catch {
					Thread.CurrentThread.CurrentUICulture = new CultureInfo("fa-ir");
					//Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("fa-ir");
				}
			}
            //String strCurrentPath = Request.Path.ToLower();
            //string strCustomPath = "";
            //strCurrentPath = strCurrentPath.Replace("'", "").Replace("\"", "");
            //if (strCurrentPath.IndexOf("-") >= 0)
            //{
            //    string FP = strCurrentPath.Substring(0, strCurrentPath.LastIndexOf("/") + 1);
            //    string EP = strCurrentPath.Substring(strCurrentPath.LastIndexOf("/") + 1).Replace(".aspx", "").Replace("-", ".aspx?ID=");
            //    strCustomPath = FP + EP;
            //}
            //else
            //    strCustomPath = strCurrentPath;
            //Context.RewritePath(strCustomPath);
        }
	    protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }
        protected void Application_Error(object sender, EventArgs e)
        {

			if (Server.GetLastError().Message.IndexOf("does not exist") != -1 && Server.GetLastError().Message.IndexOf("The file") != -1)
			{
				Logging.ErrorLog(sender.ToString(), "notfound", Server.GetLastError().Message + "{$$$}" + Server.GetLastError().Source + "{$$$}" + Server.GetLastError().Data + "{$$$}" + Server.GetLastError().InnerException + "{$$$}" + Server.GetLastError().StackTrace + "{$$$}" + Server.GetLastError().TargetSite + "{$$$}");
                Response.Redirect("/NotFound-0.aspx?b=" + sender.ToString());
			}
			else if (Server.GetLastError().Message.ToLower().IndexOf("potentially dangerous Request".ToLower()) != -1)
			{
				Logging.ErrorLog(sender.ToString(), "attack", "UnitID"+ Tools.Tools.GetViewUnitID + "-" +Server.GetLastError().Message + "{$$$}" + Server.GetLastError().Source + "{$$$}" + Server.GetLastError().Data + "{$$$}" + Server.GetLastError().InnerException + "{$$$}" + Server.GetLastError().StackTrace + "{$$$}" + Server.GetLastError().TargetSite + "{$$$}");
				Response.Redirect("/NotFound-2.aspx");
			}
			else
			{
				Logging.ErrorLog(sender.ToString(), "Error1", "UnitID" + Tools.Tools.GetViewUnitID + "-" +Server.GetLastError().Message + "{$$$}" + Server.GetLastError().Source + "{$$$}" + Server.GetLastError().Data + "{$$$}" + Server.GetLastError().InnerException + "{$$$}" + Server.GetLastError().StackTrace + "{$$$}" + Server.GetLastError().TargetSite + "{$$$}");
				Response.Redirect("/NotFound-1.aspx");
			}
        }
        protected void Session_End(object sender, EventArgs e)
        {

        }
        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}