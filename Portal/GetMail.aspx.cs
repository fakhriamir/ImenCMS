using System;
using System.Data.SqlClient;
namespace Portal
{
	public partial class GetMail : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["em"] != null && Request.QueryString["em"].Trim() != "")
			{
				string ReqMail = Request.QueryString["em"];
				if (Tools.Tools.ValidateEmail(ReqMail))
				{
					SqlParameterCollection SP = new SqlCommand().Parameters;
					SP.AddWithValue("@Email", ReqMail);
					DAL.ExecuteData.AddData("INSERT INTO Subscribe (Email, UnitID, SubscribeTypeID, LangID,name)  VALUES (@Email,44,36,1,'')", SP); 
				}
			}
		}
	}
}