using DAL;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace Tools
{
	public class Project
	{
		public static void CheckAccess(string PageName)
		{
			if (DAL.CheckData.CheckTokenGuestUserID() > 0)
			{
				string UID = DAL.CheckData.CheckTokenGuestUserID().ToString();
				string AccessTypeIDs = MyClass.MyDecry(HttpContext.Current.Request.Cookies["GuestUserAccessID"].Value);
				if (AccessTypeIDs.IndexOf(",-2,") == -1)
					HttpContext.Current.Response.Redirect("/Members/NoAccess");
			}
			else
			{
				if (PageName == "")
					HttpContext.Current.Response.Redirect("/Members/MemberLogin.aspx?r=Project");
				else
					HttpContext.Current.Response.Redirect("/Members/MemberLogin.aspx?r=" + PageName);
			}
		}
		public static void FillUserAccess(DropDownList MyDL, string UserID)
		{
			MyDL.Items.Clear();
			string Level = ExecuteData.CNTDataStr("SELECT [Level]  FROM UnitChart  WHERE (UnitChartID = (SELECT UnitChartID FROM GuestInfo WHERE (GuestID = " + UserID + ")))");
			if (Level == "")
				return;
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT RTRIM(GuestInfo.Name) + ' ' + GuestInfo.Family AS Name, GuestInfo.GuestID, UnitChart.Name AS UnitChartName  FROM UnitChart LEFT OUTER JOIN GuestInfo ON UnitChart.UnitChartID = GuestInfo.UnitChartID  WHERE ( (UnitChart.[Level] LIKE '" + Level + "%') AND (LEN(UnitChart.[Level]) = " + (Level.Length + 2) + ") OR (UnitChart.[Level] LIKE '" + Level.Substring(0, (Level.Length - 2)) + "%') AND (LEN(UnitChart.[Level]) = " + Level.Length + " OR LEN(UnitChart.[Level]) = " + (Level.Length - 2) + ") ) and UnitChart.unitid=" + Tools.GetViewUnitID); 
			
			while (MyRead.Read())
			{
				MyDL.Items.Add(new ListItem(MyCL.MGStr(MyRead, 2) + '-' + MyCL.MGStr(MyRead, 0), MyCL.MGInt(MyRead, 1).ToString()));
			}
			MyRead.Close(); MyRead.Dispose();
			
			SqlDataReader MyData = DAL.ViewData.MyDR1("SELECT GuestInfo.GuestID, GuestInfo.Name, GuestInfo.Family FROM OfficeSenderAccess INNER JOIN GuestInfo ON OfficeSenderAccess.ToID = GuestInfo.GuestID WHERE (OfficeSenderAccess.FromID = " + UserID + ") AND (OfficeSenderAccess.UnitID = " + Tools.GetViewUnitID + ")");
			while (MyData.Read())
			{
				MyDL.Items.Add(new ListItem(MyCL.MGStr(MyData, 1) + ' ' + MyCL.MGStr(MyData, 2), MyCL.MGInt(MyData, 0).ToString()));
			}
			MyData.Close(); MyData.Dispose();
		}
		public static void FillActivity_Access___(DropDownList MyDL, string UserID)
		{
			MyDL.Items.Clear();
				string Level = ExecuteData.CNTDataStr("SELECT [Level]  FROM UnitChart  WHERE (UnitChartID = (SELECT UnitChartID FROM GuestInfo WHERE (GuestID = " + UserID + ")))");
				if (Level == "")
					return;
				SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT RTRIM(GuestInfo.Name) + ' ' + GuestInfo.Family AS Name, GuestInfo.GuestID, UnitChart.Name AS UnitChartName FROM UnitChart LEFT OUTER JOIN GuestInfo ON UnitChart.UnitChartID = GuestInfo.UnitChartID WHERE (UnitChart.[Level] LIKE '" + Level.Substring(0, (Level.Length - 2)) + "%') AND (LEN(UnitChart.[Level]) = " + Level.Length + " OR LEN(UnitChart.[Level]) =" + (Level.Length - 2) + ") ");
				while (MyRead.Read())
				{
					MyDL.Items.Add(new ListItem(MyCL.MGStr(MyRead, 2) + '-' + MyCL.MGStr(MyRead, 0), MyCL.MGInt(MyRead, 1).ToString()));
				}
				MyRead.Close(); MyRead.Dispose();
			
		}
		public static void FillActivityAccess(DropDownList MyDL, string UserID)
		{
			MyDL.Items.Clear();
			MyDL.Items.Add(new ListItem("ندارد","0"));
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT Title, ProjectActivityID  FROM ProjectActivity  WHERE (UserID = " + UserID + ") OR (ExecutingGuestID = " + UserID + ")  and unitid=" + Tools.GetViewUnitID);
			while (MyRead.Read())
			{
				MyDL.Items.Add(new ListItem(MyCL.MGStr(MyRead, 0) , MyCL.MGInt(MyRead, 1).ToString()));
			}
			MyRead.Close(); MyRead.Dispose();

		}
		public static void FillProjectAccess(DropDownList MyDL, string UserID)
		{
			MyDL.Items.Clear();
			MyDL.Items.Add(new ListItem("انتخاب","0"));
			string Level = ExecuteData.CNTDataStr("SELECT [Level]  FROM UnitChart  WHERE (UnitChartID = (SELECT UnitChartID FROM GuestInfo WHERE (GuestID = " + UserID + ")))");
			if (Level == "")
				return;
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT ProjectID, Name from project  WHERE (UnitChartLevel LIKE '" + Level + "%') and unitid="+Tools.GetViewUnitID);
			while (MyRead.Read())
			{
				MyDL.Items.Add(new ListItem(MyCL.MGStr(MyRead, 1) , MyCL.MGInt(MyRead, 0).ToString()));
			}
			MyRead.Close(); MyRead.Dispose();

		}
		public static object GetParentTreeAdmin(string Name, string Lev)
		{
			if (Lev.Length == 2)
				return Name;
			string WhereComm = "";
			for (int i = 1; i <= (((Lev.Length)/2)); i++)
			{
				WhereComm += "or [Level] = '" + Lev.Substring(0, (i * 2)) + "' ";
			}
			string OutText="";
			SqlDataReader MyRead = ADAL.A_ViewData.MyDR("SELECT Name  FROM Project  WHERE ("+WhereComm.Substring(2)+") AND (UnitID = " + ADAL.A_CheckData.GetUnitID() + ")"); 
			while (MyRead.Read())
			{
				OutText += "- " + MyCL.MGStr(MyRead, 0);
			}
			MyRead.Close(); MyRead.Dispose();
			if(OutText.Trim()!="")
				return OutText.Substring(1);
			return "";
		}

		public static string GetProjectName(string ProjectID)
		{
			return DAL.ExecuteData.CNTDataStr("SELECT Name  FROM Project WHERE (ProjectID =" + ProjectID + ") and unitid=" + Tools.GetViewUnitID);
		}
		public static object GetGuestName(string GuestID)
		{
			//return GuestID;
			if (GuestID == "" || GuestID == "0")
				return "";
			string OutText = "";
			string[] GID = Regex.Split(GuestID, ",");
			for (int i = 0; i < GID.Length; i++)
				OutText += DAL.ExecuteData.CNTDataStr("SELECT RTRIM(LTRIM(Name)) + ' ' + RTRIM(LTRIM(Family)) AS Expr1  FROM GuestInfo  WHERE (GuestID = " + GID[i] + ")")+"#";

			return OutText.TrimEnd('#').Replace("#","<br />");
		}
		public static object GetProjectPriority(string PriorityID)
		{
			return DAL.ExecuteData.CNTDataStr("SELECT Name FROM ProjectPriority WHERE (UnitID = " + Tools.GetViewUnitID+ ") AND (Priority = " + PriorityID + ")");
		}

		public static void CheckUserProjectActivityAccess(string ProjectActivityID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@GuestID", DAL.CheckData.CheckTokenGuestUserID().ToString());
			SP.AddWithValue("@ProjectActivityID", ProjectActivityID);
			int CNT = DAL.ExecuteData.CNTData("SELECT count(*) FROM ProjectActivity WHERE (ProjectActivityID=@ProjectActivityID) and ((UserID =@GuestID) OR (ExecutingGuestID = @GuestID))", SP);
			if (CNT > 0)
				return;
		}
		public static object GetTicletAuthority(string Address = "/Ticketing/AddTicket")
		{
			if (DAL.CheckData.CheckTokenGuestUserID() < 0)
				return Tools.GetSetting(412, "") + Address;
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT GuestID, PCode, Name, Family, UnitID, MobileNo  FROM GuestInfo  WHERE (GuestID = " + DAL.CheckData.CheckTokenGuestUserID() + ")");
			string au = "";
			if(MyRead.Read())
				au="?au=" + Tools.ConvertToBase64(Tools.MyEncry(MyCL.MGInt(MyRead, 0) + "{" +Tools.CharToASCII(MyCL.MGInt(MyRead, 1) + "-" + MyCL.MGStr(MyRead, 2) + " " + MyCL.MGStr(MyRead, 3)) + "{" + MyCL.MGInt(MyRead, 4) + "{" + MyCL.MGStr(MyRead, 5)));
			MyRead.Close(); MyRead.Dispose();
			return Tools.GetSetting(412, "") + Address + au;
		}
		public static string[] WorkState = { "", "", "اقدام و خاتمه کار", "خاتمه کار" };
		public static string GetMeetingRecordState(string ProjectActivityIDs)
		{
			if (ProjectActivityIDs == "" || ProjectActivityIDs == "0")
				return "";
			string OutText = "";
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT ProjectActivity.ProjectActivityID, GuestInfo.Name, GuestInfo.Family, ProjectActivity.Type, ProjectActivity.STDate, ProjectActivity.EndDate  FROM ProjectActivity INNER JOIN GuestInfo ON ProjectActivity.ExecutingGuestID = GuestInfo.GuestID WHERE (ProjectActivity.ProjectActivityID IN (" + ProjectActivityIDs + "))");
			while(MyRead.Read())
			{
				OutText += "" + MyCL.MGStr(MyRead, 1) + " " + MyCL.MGStr(MyRead, 2) + " وضعیت:" + WorkState[MyCL.MGInt(MyRead, 3)] + " تاریخ شروع:" + Calender.MyPDate(MyCL.MGDT(MyRead, 4))+"<br/>";
			}
			MyRead.Close();
			MyRead.Dispose();
			return OutText;//.TrimEnd('#').Replace("#", "<br />");
		}
	}
}