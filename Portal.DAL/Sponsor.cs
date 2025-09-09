using DAL;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace Tools
{
	public class Sponsor
	{
		public static void FillSponsorType(DropDownList MyDL, bool IsAdmin = false)
		{
			MyDL.Items.Clear();			
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SqlDataReader MyRead;
			if(IsAdmin)
			{
				SP.AddWithValue("@UnitID", ADAL.A_CheckData.GetUnitID());
				MyRead= ADAL.A_ViewData.MyDR("SELECT SponsorTypeID, Name FROM SponsorType   WHERE unitid=@UnitID" ,SP);
			}
			else { 
				SP.AddWithValue("@UnitID", Tools.GetViewUnitID);
				MyRead = DAL.ViewData.MyDR1("SELECT SponsorTypeID, Name FROM SponsorType   WHERE unitid=@UnitID", SP);
				MyDL.Items.Add(new ListItem("همه موارد","0"));
			}			 
			while (MyRead.Read())
			{
				MyDL.Items.Add(new ListItem(MyCL.MGStr(MyRead, 1) , MyCL.MGInt(MyRead, 0).ToString()));
			}
			MyRead.Close(); MyRead.Dispose();
		}
		public static void FillSponsorSubject(DropDownList MyDL,  bool IsAdmin = false)
		{
			MyDL.Items.Clear();
			//MyDL.Items.Add(new ListItem("ندارد","0"));
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SqlDataReader MyRead;
			if (IsAdmin)
			{
				SP.AddWithValue("@UnitID", ADAL.A_CheckData.GetUnitID());
				MyRead = ADAL.A_ViewData.MyDR("SELECT SponsorSubjectID, Name FROM SponsorSubject WHERE unitid=@UnitID", SP);
			}
			else
			{
				SP.AddWithValue("@UnitID", Tools.GetViewUnitID);
				MyRead = DAL.ViewData.MyDR1("SELECT SponsorSubjectID, Name FROM SponsorSubject WHERE unitid=@UnitID", SP);
			}
			while (MyRead.Read())
			{
				MyDL.Items.Add(new ListItem(MyCL.MGStr(MyRead, 1), MyCL.MGInt(MyRead, 0).ToString()));
			}
			MyRead.Close(); MyRead.Dispose();
		}
		public static void FillSponsorGuest(DropDownList MyDL, bool IsAdmin = false)
		{
			MyDL.Items.Clear();
			//MyDL.Items.Add(new ListItem("ندارد","0"));
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SqlDataReader MyRead;
			if (IsAdmin)
			{
				SP.AddWithValue("@UnitID", ADAL.A_CheckData.GetUnitID());
				MyRead = ADAL.A_ViewData.MyDR("SELECT GuestID, Name, Family FROM GuestInfo WHERE unitid=@UnitID", SP);
			}
			else
			{
				SP.AddWithValue("@UnitID", Tools.GetViewUnitID);
				MyRead = DAL.ViewData.MyDR1("SELECT GuestID, Name, Family FROM GuestInfo WHERE unitid=@UnitID", SP);
			}
			while (MyRead.Read())
			{
				MyDL.Items.Add(new ListItem(MyCL.MGStr(MyRead, 1) + " " + MyCL.MGStr(MyRead, 2), MyCL.MGInt(MyRead, 0).ToString()));
			}
			MyRead.Close(); MyRead.Dispose();
		}


		public static string GetSumProject(int UserID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@UserID", UserID);
			SP.AddWithValue("@UnitID", Tools.GetViewUnitID);
			return DAL.ExecuteData.CNTData("SELECT SUM(Money) AS Expr1  FROM SponsorGuest WHERE (UnitID = @UnitID) AND (GuestID = @UserID)", SP).ToString();
		}

		public static object GetSumSponsor(int UserID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@UserID", UserID);
			SP.AddWithValue("@UnitID", Tools.GetViewUnitID);
			return DAL.ExecuteData.CNTData("SELECT SUM(Money) AS Expr1  FROM SponsorGuest  WHERE (UnitID = @UnitID) AND (SponsorID IN (SELECT SponsorID FROM Sponsor WHERE (GuestID = @UserID)))", SP).ToString();
		}

		public static object GetProjectCount(int UserID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@UserID", UserID);
			SP.AddWithValue("@UnitID", Tools.GetViewUnitID);
			return DAL.ExecuteData.CNTData("SELECT count(*) FROM Sponsor WHERE (UnitID = @UnitID) AND (GuestID = @UserID)", SP).ToString();
	
		}
		public static string[] SponsorType = { "در حال بررسی", "منتشر شده", "اتمام وقت", "رد شده" };
		public static object GetProjectType(string SponsorID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@SponsorID", SponsorID);
			SP.AddWithValue("@UnitID", Tools.GetViewUnitID);
			return SponsorType[DAL.ExecuteData.CNTData("SELECT type FROM Sponsor WHERE (UnitID = @UnitID) AND (SponsorID = @SponsorID)", SP)];
		}

		public static bool GuestCheckAccessSponser(int UserID, int SponsorID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@SponsorID", SponsorID);
			SP.AddWithValue("@UnitID", Tools.GetViewUnitID);
			SP.AddWithValue("@UserID", UserID);
			int cnt = DAL.ExecuteData.CNTData("SELECT COUNT(*) AS Expr1 FROM Sponsor  WHERE (GuestID = @UserID) AND (SponsorID = @SponsorID) AND (UnitID = @UnitID)",SP);
			if (cnt == 0)
				return false;
			
			return true;
		}
	}
}