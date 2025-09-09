using System.Web;
using System.Web.UI.WebControls;
using DAL;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Tools
{
	public class Archive
	{
		public static void FillSenderAccess(DropDownList MyDL, string UserID)
		{
			MyDL.Items.Clear();
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
		public static void FillAllPeople(DropDownList MyDL, string UserID)
		{
			MyDL.Items.Clear();

			//SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT RTRIM(GuestInfo.Name) + ' ' + GuestInfo.Family AS Name, GuestInfo.GuestID, UnitChart.Name AS UnitChartName FROM UnitChart LEFT OUTER JOIN GuestInfo ON UnitChart.UnitChartID = GuestInfo.UnitChartID WHERE ((UnitChart.[Level] LIKE '" + Level.Substring(0, (Level.Length - 2)) + "%') or (LEN(UnitChart.[Level]) = " + Level.Length + ")) and (UnitChart.UnitID = 66)  ");
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT RTRIM(GuestInfo.Name) + ' ' + GuestInfo.Family AS Name, GuestInfo.GuestID, UnitChart.Name AS UnitChartName FROM UnitChart LEFT OUTER JOIN GuestInfo ON UnitChart.UnitChartID = GuestInfo.UnitChartID WHERE (UnitChart.UnitID = " + MyClass.GetViewUnitID + ") ");

			while (MyRead.Read())
			{
				MyDL.Items.Add(new ListItem(MyCL.MGStr(MyRead, 2) + '-' + MyCL.MGStr(MyRead, 0), MyCL.MGInt(MyRead, 1).ToString()));
			}
			MyRead.Close(); MyRead.Dispose();
		}
		public static void CheckAccess(string PageName)
		{
			if (DAL.CheckData.CheckTokenGuestUserID() > 0)
			{
				string UID = DAL.CheckData.CheckTokenGuestUserID().ToString();
				string AccessTypeIDs = MyClass.MyDecry(HttpContext.Current.Request.Cookies["GuestUserAccessID"].Value);
				if (AccessTypeIDs.IndexOf(",-5,") == -1)
					HttpContext.Current.Response.Redirect("/Members/NoAccess");
			}
			else
			{
				if (PageName == "")
					HttpContext.Current.Response.Redirect("/Members/MemberLogin.aspx?r=Archive");
				else
					HttpContext.Current.Response.Redirect("/Members/MemberLogin.aspx?r=" + PageName);
			}
		}
		public static string GetMeetingStatus(string MeetingID)
		{
			return DAL.ExecuteData.CNTDataStr("SELECT Title  FROM MeetingStatus WHERE (MeetingStatusID =" + MeetingID + ")");
		}
		public static string GetMeetingPlace(string placeID)
		{
			return DAL.ExecuteData.CNTDataStr("SELECT [Address] FROM  MeetingPlace Where MeetingPlaceID=" + placeID);
		}
		public static string GetparticipantName(string MeetingID, int StatusID)
		{
			string mm = "";
			SqlDataReader dr = DAL.ViewData.MyDR1("SELECT  GuestInfo.Name, GuestInfo.Family FROM  MeetingParticipant INNER JOIN GuestInfo ON MeetingParticipant.GuestID = GuestInfo.GuestID  Where MeetingParticipant.MeetingID=" + MeetingID + " AND MeetingParticipant.Status=" + StatusID, null, true);
			while (dr.Read())
				mm += MyCL.MGStr(dr, 0) + " " + MyCL.MGStr(dr, 1) + " , ";
			dr.Close();
			dr.Dispose();
			return mm;

		}
		public static void FillArchive(DropDownList MyDL)
		{
			MyDL.Items.Clear();
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT ArchiveID, Name  FROM Archive  WHERE (UnitID = " + MyClass.GetViewUnitID + ") ORDER BY Sort ");
			while (MyRead.Read())
			{
				MyDL.Items.Add(new ListItem( MyCL.MGStr(MyRead,1), MyCL.MGInt(MyRead, 0).ToString()));
			}
			MyRead.Close(); MyRead.Dispose();
		}
		public static void FillArchiveCategory(DropDownList MyDL)
		{
			MyDL.Items.Clear();
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT ArchiveCategoryID, Name, [Level] FROM ArchiveCategory WHERE (UnitID = " + MyClass.GetViewUnitID + ") ORDER BY [Level]");
			while (MyRead.Read())
			{
				MyDL.Items.Add(new ListItem(MyClass.GetLevelTab(MyCL.MGStr(MyRead,2),"-") + MyCL.MGStr(MyRead, 1), MyCL.MGInt(MyRead, 0).ToString()));
			}
			MyRead.Close(); MyRead.Dispose();
		}
		public static void FillArchiveType(DropDownList MyDL)
		{
			MyDL.Items.Clear();
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT ArchiveTypeID, Name  FROM ArchiveType  WHERE (UnitID = " + MyClass.GetViewUnitID + ") ORDER BY Sort ");
			while (MyRead.Read())
			{
				MyDL.Items.Add(new ListItem( MyCL.MGStr(MyRead,1), MyCL.MGInt(MyRead, 0).ToString()));
			}
			MyRead.Close(); MyRead.Dispose();
		}

		public static bool CheckFileAccess(string FileID)
		{
			return true;
		}

		public static void CheckAccessFileAccess(int ArchiveFileID,string NeedAccessID)
		{
			if (DAL.ExecuteData.CNTData("SELECT GuestID  FROM ArchiveFile WHERE (ArchiveFileID = " + ArchiveFileID + ")") == DAL.CheckData.CheckTokenGuestUserID())
				return;
			string AccessIDs = DAL.ExecuteData.CNTDataStr("SELECT Access  FROM ArchiveFileAccess WHERE (ArchiveFileID =" + ArchiveFileID + ") AND (GuestID = " + DAL.CheckData.CheckTokenGuestUserID() + ") AND (Disable = 0)");
			if (AccessIDs == "")
				HttpContext.Current.Response.Redirect("/Members/NoAccess");
			string[] NeedAccessIDs = Regex.Split(NeedAccessID, ",");
			for (int i = 0; i < NeedAccessIDs.Length; i++)
			{
				if (AccessIDs.IndexOf(NeedAccessIDs[i].ToString()) != -1)
					return;
			}
				HttpContext.Current.Response.Redirect("/Members/NoAccess");


		}

        public static string GetFileAddress(string GuID)
        {
            string FileAddress = Tools.GetSetting(417) + GuID.Substring(0, 2) + "/" + GuID.Substring(2, 2) + "/" + GuID.Substring(4, 2);
            FileAddress = FileAddress.Replace("/", @"\");
            return FileAddress;
        }
	}
}