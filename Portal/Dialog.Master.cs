using System;
using System.Data.SqlClient;
using DAL;
using System.IO;

namespace Portal
{
	public partial class Dialog : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			SiteCSS.Href = "/Files/" + Tools.Tools.GetViewUnitID + "/" + Tools.Tools.LangSTR + "Site.css";
			BrowserCSS.Href = "/Files/" + Tools.Tools.GetViewUnitID + "/" + Tools.Tools.LangSTR + Tools.Tools.GetBrowserCSSAddress(Request.Browser);
		
		}
	}
}