using System; using DAL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Portal
{
    public partial class Matchs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
		{
			Tools.Tools.SetPageHit(this.Page.ToString(),this.Page.ClientQueryString);
            ViewDR();
			Default.Adv.PageID = 7;
        }
        void ViewDR()
        {
            System.Data.SqlClient.SqlParameterCollection SP = new System.Data.SqlClient.SqlCommand().Parameters;
            SP.AddWithValue("@UnitID",Tools.Tools.GetViewUnitID);
			TextDG.DataSource = ViewData.MyDT("SELECT MatchID, Question,jayeze FROM Match WHERE (UnitID = @UnitID) AND (State = 2) ", SP); 
            TextDG.DataBind();
        }
        public static string GetMatchCNT(string MatchID)
        {
            try
            {
                SqlParameterCollection SP = new SqlCommand().Parameters;
                SP.AddWithValue("@MatchID", MatchID);
                return ExecuteData.CNTData("SELECT COUNT(*) FROM MatchAnswer WHERE (MatchID = @MatchID)", SP).ToString();
            }
            catch
            {
                return "0";
            }
        }
        public static string Winnersname(string MatchID)
        {
            string OutText = "";
            SqlParameterCollection SP = new SqlCommand().Parameters;
            //System.wTools.Tools.GetViewUnitID = "";
            SP.AddWithValue("@MatchID", MatchID);
            SqlDataReader MyRead = ViewData.MyDR1("SELECT Name  FROM MatchAnswer  WHERE (MatchID = @MatchID) AND (Winner = 1) ", SP);
            while (MyRead.Read())
                OutText += "&nbsp;-&nbsp;" + Tools.MyCL.MGStr(MyRead, 0);
            MyRead.Close(); MyRead.Dispose();
            //ViewData.MyConnection.Close();
            return OutText.Substring(7);
        }
    }
}