using System; using DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
	public partial class Advertise : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Tools.Tools.SetPageHit(this.Page.ToString(),this.Page.ClientQueryString);
			if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim() != "")
			{
				System.Data.SqlClient.SqlParameterCollection SP = new System.Data.SqlClient.SqlCommand().Parameters;
				SP.AddWithValue("@ID",Tools.Tools.ConvertToInt32(Request.QueryString["ID"]));
				ExecuteData.AddData("UPDATE Advertise SET ClickCount = ClickCount + 1 WHERE (AdvertiseID = @ID)", SP);
				string MyUrl = ExecuteData.CNTDataStr("SELECT URL  FROM Advertise WHERE (AdvertiseID = @ID)", SP);
				if (MyUrl == "")
					Response.Redirect("/");
				if (MyUrl.Length<4)
					MyUrl = "/";
				//else if (MyUrl.Substring(0, 4).ToLower() != "http")
				//	MyUrl = "http://"+MyUrl;
				Response.Redirect(MyUrl);
			}
			else
				Response.Redirect("/");
		}
	}
}