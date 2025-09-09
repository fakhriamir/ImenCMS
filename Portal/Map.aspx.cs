using System; using DAL;
using System.Web.UI;
using System.Data.SqlClient;
namespace Portal
{
	public partial class Map : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Tools.Tools.SetPageHit(this.Page.ToString(),this.Page.ClientQueryString);



			if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
			{
				string ReqID = Request.QueryString["ID"];
				if (ReqID.ToLower().IndexOf("o") != -1)
					UpdateDR(ReqID.Substring(1));
				else
				{
					Portal.Default.Map MyMap = ((Default.Map)Page.LoadControl("/Def/Map.ascx"));
					MyMap.CityID = Tools.Tools.ConvertToInt32(Request.QueryString["ID"]);
					MapPH.Controls.Add((Control)MyMap);
                    if (Tools.Tools.ConvertToInt32(ReqID) != -1)
                    {
                        SqlParameterCollection SP = new SqlCommand().Parameters;
                        SP.AddWithValue("@OstanID", Tools.Tools.ConvertToInt32(ReqID));
                        UpdateDR(DAL.ExecuteData.CNTData("SELECT OstanId FROM Shahr WHERE (ShahrID = @OstanID) ", SP).ToString());
                    }
				}
			}
		}
		void UpdateDR(string Filter)
		{
			if (Tools.Tools.ConvertToInt32(Filter) == -1)
				return;
			SqlParameterCollection SP = new SqlCommand().Parameters;
			string WhereComm = "";
			if (Filter != "")
			{
				SP.AddWithValue("@OstanID", Filter);
				WhereComm = " and unitinfo.Ostan=@OstanID ";
			}
			SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);
			CityDR.DataSource = ViewData.MyDT("SELECT UnitInfo.City, Shahr.Name FROM Shahr INNER JOIN UnitInfo ON Shahr.ShahrID = UnitInfo.City  WHERE (UnitInfo.XPos <> '') and (UnitInfo.unitid=@UnitID) "+WhereComm+"  GROUP BY UnitInfo.City, Shahr.Name ",SP);
			CityDR.DataBind();
		}
	}
}