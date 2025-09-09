using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace Tools
{
	public class Meeting
	{
		public static void FillSenderAccess(DropDownList MyDL, string UserID)
		{
			MyDL.Items.Clear();
			string Level = ExecuteData.CNTDataStr("SELECT [Level]  FROM UnitChart  WHERE (UnitChartID = (SELECT UnitChartID FROM GuestInfo WHERE (GuestID = " + UserID + ")))");
			if (Level == "")
				return;
			//SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT RTRIM(GuestInfo.Name) + ' ' + GuestInfo.Family AS Name, GuestInfo.GuestID, UnitChart.Name AS UnitChartName FROM UnitChart LEFT OUTER JOIN GuestInfo ON UnitChart.UnitChartID = GuestInfo.UnitChartID WHERE ((UnitChart.[Level] LIKE '" + Level.Substring(0, (Level.Length - 2)) + "%') or (LEN(UnitChart.[Level]) = " + Level.Length + ")) and (UnitChart.UnitID = 66)  ");
            SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT RTRIM(GuestInfo.Name) + ' ' + GuestInfo.Family AS Name, GuestInfo.GuestID, UnitChart.Name AS UnitChartName FROM UnitChart LEFT OUTER JOIN GuestInfo ON UnitChart.UnitChartID = GuestInfo.UnitChartID WHERE (UnitChart.[Level] LIKE '" + Level.Substring(0, (Level.Length - 2)) + "%') AND (LEN(UnitChart.[Level]) = " + ((Level.Length) - 2) + " OR LEN(UnitChart.[Level]) = " + Level.Length + ") AND (UnitChart.UnitID = " + MyClass.GetViewUnitID + ") OR (UnitChart.[Level] LIKE '" + Level + "%') AND (UnitChart.UnitID = " + MyClass.GetViewUnitID + ") Order By GuestInfo.Family ");

			while (MyRead.Read())
			{
				MyDL.Items.Add(new ListItem(MyCL.MGStr(MyRead, 0) + '-' + MyCL.MGStr(MyRead, 2), MyCL.MGInt(MyRead, 1).ToString()));
			}
			MyRead.Close(); MyRead.Dispose();
		}
		public static void FillSenderAccess(CheckBoxList MyDL, string UserID)
		{
			MyDL.Items.Clear();
			string Level = ExecuteData.CNTDataStr("SELECT [Level]  FROM UnitChart  WHERE (UnitChartID = (SELECT UnitChartID FROM GuestInfo WHERE (GuestID = " + UserID + ")))");
			if (Level == "")
				return;
			//SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT RTRIM(GuestInfo.Name) + ' ' + GuestInfo.Family AS Name, GuestInfo.GuestID, UnitChart.Name AS UnitChartName FROM UnitChart LEFT OUTER JOIN GuestInfo ON UnitChart.UnitChartID = GuestInfo.UnitChartID WHERE ((UnitChart.[Level] LIKE '" + Level.Substring(0, (Level.Length - 2)) + "%') or (LEN(UnitChart.[Level]) = " + Level.Length + ")) and (UnitChart.UnitID = 66)  ");
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT RTRIM(GuestInfo.Name) + ' ' + GuestInfo.Family AS Name, GuestInfo.GuestID, UnitChart.Name AS UnitChartName FROM UnitChart LEFT OUTER JOIN GuestInfo ON UnitChart.UnitChartID = GuestInfo.UnitChartID WHERE (UnitChart.[Level] LIKE '" + Level.Substring(0, (Level.Length - 2)) + "%') AND (LEN(UnitChart.[Level]) = " + ((Level.Length) - 2) + " OR LEN(UnitChart.[Level]) = " + Level.Length + ") AND (UnitChart.UnitID = " + MyClass.GetViewUnitID + ") OR (UnitChart.[Level] LIKE '" + Level + "%') AND (UnitChart.UnitID = " + MyClass.GetViewUnitID + ") Order By GuestInfo.Family ");

			while (MyRead.Read())
			{
				MyDL.Items.Add(new ListItem(MyCL.MGStr(MyRead, 0) + '-' + MyCL.MGStr(MyRead, 2), MyCL.MGInt(MyRead, 1).ToString()));
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
        public static void A_FillAllPeople(DropDownList MyDL, string UserID)
        {
            MyDL.Items.Clear();
            SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT RTRIM(GuestInfo.Name) + ' ' + GuestInfo.Family AS Name, GuestInfo.GuestID, UnitChart.Name AS UnitChartName FROM UnitChart LEFT OUTER JOIN GuestInfo ON UnitChart.UnitChartID = GuestInfo.UnitChartID WHERE (UnitChart.UnitID = " + ADAL.A_CheckData.GetUnitID() + ") ");

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
				if (AccessTypeIDs.IndexOf(",-4,") == -1)
					HttpContext.Current.Response.Redirect("/Members/NoAccess");
			}
			else
			{
				if (PageName == "")
					HttpContext.Current.Response.Redirect("/Members/MemberLogin.aspx?r=meeting");
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
            if (placeID == "0")
                return "دیگر مکان ها";
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
	}
}