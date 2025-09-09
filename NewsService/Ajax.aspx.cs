using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsService
{
	public partial class Ajax : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["MyType"] != null && Tools.Tools.GetSubstring(Request.QueryString["MyType"], 0, 3) == "Sub")
			{
				SaveSubject(Request.QueryString["MyType"].Substring(3));
				return;
			}
			else if (Request.QueryString["MyType"] != null && Tools.Tools.GetSubstring(Request.QueryString["MyType"], 0, 3) == "ECO")
			{
				EditAutoNews(Request.QueryString["MyType"].Substring(3));
				return;
			}
		}
		void EditAutoNews(string Req)
		{
			if (Req.IndexOf("-") == -1)
			{
				Response.Write("error");
				return;
			}
			//Req = Req.Substring(1);
			int NewsID = Tools.Tools.ConvertToInt32(Req.Substring(0, Req.IndexOf("-")));
			string Eve = Req.Substring(Req.IndexOf("-") + 1);
			if (NewsID == -1 )
			{
				Response.Write("error");
				return;
			}
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@NewsID", NewsID);
			//SP.AddWithValue("@CatID", CatID);
			if(Eve=="DEL")
				ADAL.A_ExecuteData.DeleteData("delete from news WHERE (NewsID = @NewsID)", SP);
			else if(Eve=="OK")
				ADAL.A_ExecuteData.AddData("UPDATE news  SET CategoryID =AutoCategoryID WHERE (NewsID = @NewsID)", SP);

			
			Response.Write("OK" + NewsID);
		}
		void SaveSubject(string Req)
		{
			if (Req.IndexOf("-") == -1)
			{
				Response.Write("error");
				return;
			}
			Req = Req.Substring(1);
			int NewsID =Tools.Tools.ConvertToInt32(Req.Substring(0,Req.IndexOf("-")));
			int CatID = Tools.Tools.ConvertToInt32(Req.Substring(Req.IndexOf("-") + 1));
			if(NewsID==-1 || CatID==-1)
			{
				Response.Write("error");
				return;
			}
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@NewsID", NewsID);
			SP.AddWithValue("@CatID", CatID);

			ADAL.A_ExecuteData.AddData("UPDATE news  SET CategoryID =@CatID WHERE (NewsID = @NewsID)", SP);
			Response.Write("OK" +NewsID);
		}

	}
}