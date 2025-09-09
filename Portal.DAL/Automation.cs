using System.Web.UI.WebControls;
using Tools;
using DAL;
using System.Data.SqlClient;
using System.Web;
using System;
using ActiveUp.Net.Mail;
namespace Tools
{
	public class Automation
	{
		public static void FillSenderAccess(DropDownList MyDL,string UserID,bool ISInLetter=false)
		{
			MyDL.Items.Clear();
			if (ISInLetter)
			{
				SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT OfficeCompanyID * - 1 AS OfficeCompanyID, Name, BossName FROM OfficeCompany WHERE (UnitID = " + Tools.GetViewUnitID + ") ORDER BY Sort ");
				while (MyRead.Read())
				{
					MyDL.Items.Add(new ListItem(MyCL.MGStr(MyRead, 1) + '-' + MyCL.MGStr(MyRead, 2), MyCL.MGInt(MyRead, 0).ToString()));
				}
				MyRead.Close(); MyRead.Dispose();
			}
			else
			{
				string Level = ExecuteData.CNTDataStr("SELECT [Level]  FROM UnitChart  WHERE (UnitChartID = (SELECT UnitChartID FROM GuestInfo WHERE (GuestID = " + UserID + ")))");
				if (Level == "")
					return;
				//SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT RTRIM(GuestInfo.Name) + ' ' + GuestInfo.Family AS Name, GuestInfo.GuestID, UnitChart.Name AS UnitChartName FROM UnitChart LEFT OUTER JOIN GuestInfo ON UnitChart.UnitChartID = GuestInfo.UnitChartID WHERE ((UnitChart.[Level] LIKE '" + Level.Substring(0, (Level.Length - 2)) + "%') or (LEN(UnitChart.[Level]) = " + Level.Length + ")) and (UnitChart.UnitID = 66)  ");
				SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT RTRIM(GuestInfo.Name) + ' ' + GuestInfo.Family AS Name, GuestInfo.GuestID, UnitChart.Name AS UnitChartName FROM UnitChart LEFT OUTER JOIN GuestInfo ON UnitChart.UnitChartID = GuestInfo.UnitChartID WHERE (UnitChart.[Level] LIKE '" + Level.Substring(0, (Level.Length - 2)) + "%') AND (LEN(UnitChart.[Level]) = " + ((Level.Length) - 2) + " OR LEN(UnitChart.[Level]) = " + Level.Length + ") AND (UnitChart.UnitID = " + MyClass.GetViewUnitID + ") OR (UnitChart.[Level] LIKE '" + Level + "%') AND (UnitChart.UnitID = " + MyClass.GetViewUnitID + ")  ");
				
				while (MyRead.Read())
				{
					MyDL.Items.Add(new ListItem(MyCL.MGStr(MyRead, 2) + '-' + MyCL.MGStr(MyRead, 0), MyCL.MGInt(MyRead, 1).ToString()));
				}
				MyRead.Close(); MyRead.Dispose();
			}

			SqlDataReader MyData = DAL.ViewData.MyDR1("SELECT GuestInfo.GuestID, GuestInfo.Name, GuestInfo.Family FROM OfficeSenderAccess INNER JOIN GuestInfo ON OfficeSenderAccess.ToID = GuestInfo.GuestID WHERE (OfficeSenderAccess.FromID = "+UserID+") AND (OfficeSenderAccess.UnitID = " + Tools.GetViewUnitID + ")");
			while (MyData.Read())
			{
				MyDL.Items.Add(new ListItem(MyCL.MGStr(MyData, 1) + ' ' + MyCL.MGStr(MyData, 2), MyCL.MGInt(MyData, 0).ToString()));
			}
			MyData.Close(); MyData.Dispose();

		}
		public static void CheckAccess(string PageName)
		{
			if (DAL.CheckData.CheckTokenGuestUserID() > 0)
			{
				string UID = DAL.CheckData.CheckTokenGuestUserID().ToString();
				string AccessTypeIDs = MyClass.MyDecry(HttpContext.Current.Request.Cookies["GuestUserAccessID"].Value);
				if (AccessTypeIDs.IndexOf(",-1,") == -1)
					HttpContext.Current.Response.Redirect("/Members/NoAccess");
			}
			else
			{
				if (PageName == "")
					HttpContext.Current.Response.Redirect("/Members/MemberLogin.aspx?r=automation");
				else
					HttpContext.Current.Response.Redirect("/Members/MemberLogin.aspx?r=" + PageName);
			}
		}
		public static int SetSign(string LetterID)
		{			
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@GuestID", DAL.CheckData.CheckTokenGuestUserID().ToString());
			SP.AddWithValue("@OfficeLetterID", LetterID);
			SP.AddWithValue("@UnitID", MyClass.GetViewUnitID);
			int CNT = DAL.ExecuteData.CNTData("SELECT COUNT(*) FROM OfficeLetterSign where OfficeLetterID=@OfficeLetterID and GuestID=@GuestID and UnitID=@UnitID and type=0", SP);
			if (CNT != 0)
				return -1;
			DAL.ExecuteData.AddData("INSERT INTO OfficeLetterSign (OfficeLetterID, GuestID, UnitID) VALUES (@OfficeLetterID, @GuestID, @UnitID)",SP);
			return 0;
		}
		public static string GetLetterNo(string LetterID)
		{
			string SettingFormat = Tools.GetSetting(369,"#YY#-#MM#/#LID#");
			SettingFormat = SettingFormat.Replace("#LID#",LetterID);
			int[] cal =  Calender.GetPersianDate(DateTime.Now);
			SettingFormat = SettingFormat.Replace("#YYYY#",cal[0].ToString());
			SettingFormat = SettingFormat.Replace("#MM#", cal[1].ToString());
			SettingFormat = SettingFormat.Replace("#DD#", cal[2].ToString());
			SettingFormat = SettingFormat.Replace("#YY#", cal[0].ToString().Substring(2));
			return SettingFormat;
		}
		public static void CheckUserLetterAccess(string LetterID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@GuestID", DAL.CheckData.CheckTokenGuestUserID().ToString());
			SP.AddWithValue("@OfficeLetterID", LetterID);
			int CNT = DAL.ExecuteData.CNTData("SELECT count(*) FROM OfficeReference WHERE (OfficeLetterID=@OfficeLetterID) and ((SenderPersonalID =@GuestID) OR (ToPersonalID = @GuestID))", SP);
			if (CNT > 0)
				return;
			int CNT1 = DAL.ExecuteData.CNTData("SELECT count(*) FROM OfficeLetter WHERE (OfficeLetterID=@OfficeLetterID) and ( (SupplierPersonalID = @GuestID) OR (SenderPersonalID = @GuestID) OR (SignerPersonalID = @GuestID) OR (RecieverPersonalID = @GuestID))", SP);
			if (CNT1 > 0)
				return;

			HttpContext.Current.Response.Redirect("/Members/NoAccess");
		}
		public static string GetGuestName(string GuestID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@GuestID", GuestID);
			string CNT = DAL.ExecuteData.CNTDataStr("SELECT        RTRIM(LTRIM(Name)) + ' ' + RTRIM(LTRIM(Family)) AS name FROM            GuestInfo  WHERE        (GuestID = @GuestID)", SP);
			return CNT;
		}
		public static object GetMoveLetterOption()
		{
			//<option value="-1">انتخاب</option>
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@UserID", DAL.CheckData.CheckTokenGuestUserID());
			SP.AddWithValue("@UnitID", MyClass.GetViewUnitID);
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT officeworkfolderid,name FROM OfficeWorkFolder where Guestid=@UserID and unitid=@UnitID order by sort", SP);
			string OutStr = "";
			while (MyRead.Read())
			{
				OutStr += "<option value=\"-" + MyCL.MGInt(MyRead, 0) + "\">" + MyCL.MGStr(MyRead, 1) + "</option>";
			}
			MyRead.Close(); MyRead.Dispose();
			return OutStr;
		}
		public static void ECERecive()
		{
			MessageCollection mc = new MessageCollection();
			if (Tools.GetSetting(403) == "2")
				mc = ReciveMail.ECEReciveMailImap();
			else if (Tools.GetSetting(403) == "1")
				mc = ReciveMail.ECEReciveMailPop3();
			else
				return;
			if (mc.Count <= 0)
				return;
		}
	}
}


