using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using Tools;
namespace NewsService
{
	[WebService(Namespace = "NewsService")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	public class SiteCheck : System.Web.Services.WebService
	{
		[WebMethod]
		public bool AddSiteURL(string SiteName,string Key)
		{
			//SiteCheckConnectionStr
			try
			{
				SqlParameterCollection SP = new SqlCommand().Parameters;
				SP.AddWithValue("@SiteName", SiteName);
				SP.AddWithValue("@SiteKey", MyClass.MyDecry(Key));
				DAL.ExecuteData.AddData("INSERT INTO SiteCheck(SiteName, SiteKey) VALUES (@SiteName,@SiteKey)", SP, "MyNewsConnectionStr");
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
