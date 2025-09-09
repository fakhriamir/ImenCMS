using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.ComponentModel;
using Tools;
using Telegram.Bot.Types;
namespace DAL
{
	public class CheckData
	{
		/*public class Guest1User
		{
			public int GuestUserID { get; set; }
			[DefaultValue(null)]
			public string UserName { get; set; }
			public int PCode { get; set; }
			public string Fname { get; set; }
			public string Lname { get; set; }
			public string Email { get; set; }
		}
		*/
        public static bool CheckAccessChart(Tools.MyVar.SiteGuest MyPT, int ID)
        {
            string UID = "", AccessTypeID = "",ChartLevel="";
            if (CheckTokenGuestUserID() > 0)
            {
                UID = CheckTokenGuestUserID().ToString();
                AccessTypeID = MyClass.MyDecry(HttpContext.Current.Request.Cookies["GuestUserAccessID"].Value);
                ChartLevel = DAL.ExecuteData.CNTDataStr("SELECT UnitInfo.[Level] FROM GuestInfo INNER JOIN UnitInfo ON GuestInfo.UnitInfoID = UnitInfo.UnitInfoID  WHERE (GuestInfo.GuestID = "+UID+")"); 
            }
            //if (HttpContext.Current.Request.Cookies["LoginUnitID"] != null)
            //    System.Web.HttpContext.Current.DAL.A_CheckData.GetUnitID() = MyCL.MyDecry(HttpContext.Current.Request.Cookies["LoginUnitID"].Value);
            ////string UID = System.Web.HttpContext.Current.Session["UserID"].ToString();
            int CNT = DAL.ExecuteData.CNTData("SELECT count(*) FROM GuestAccesschart  WHERE (Type = " + (int)MyPT + ") AND (ID = " + ID + ") AND (LangID =" + Tools.Tools.LangID + ") AND (UnitID = " + MyClass.GetViewUnitID + ")");
            if (CNT == 0)
                return true;
            SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT UnitInfoLevel FROM GuestAccesschart  WHERE (Type = " + (int)MyPT + ") AND (ID = " + ID + ") AND (LangID =" + Tools.Tools.LangID + ") AND (UnitID = " + MyClass.GetViewUnitID + ")");
            while (MyRead.Read())
            {
                if (UID == "")
                {
                    MyRead.Close(); MyRead.Dispose();
                    return false;
                }
                else
                {
                    if (MyCL.MGInt(MyRead, 0).ToString() == ChartLevel)
                    {
                        MyRead.Close(); MyRead.Dispose();
                        return true;
                    }
                }
            }
            MyRead.Close(); MyRead.Dispose();
            return false;
        }
	
		public static bool CheckAccess(Tools.MyVar.SiteGuest MyPT, int ID)
		{
			string UID = "", AccessTypeID = "";
			if (CheckTokenGuestUserID() > 0)
			{
				UID = CheckTokenGuestUserID().ToString();
				AccessTypeID = MyClass.MyDecry(HttpContext.Current.Request.Cookies["GuestUserAccessID"].Value);
			}
			//if (HttpContext.Current.Request.Cookies["LoginUnitID"] != null)
			//    System.Web.HttpContext.Current.DAL.A_CheckData.GetUnitID() = MyCL.MyDecry(HttpContext.Current.Request.Cookies["LoginUnitID"].Value);
			////string UID = System.Web.HttpContext.Current.Session["UserID"].ToString();
			int CNT = DAL.ExecuteData.CNTData("SELECT GuestAccessTypeID FROM GuestAccess  WHERE (Type = " + (int)MyPT + ") AND (ID = " + ID + ") AND (LangID =" + Tools.Tools.LangID + ") AND (UnitID = " + MyClass.GetViewUnitID + ")");
			if (CNT == 0)
				return true;
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT GuestAccessTypeID FROM GuestAccess  WHERE (Type = " + (int)MyPT + ") AND (ID = " + ID + ") AND (LangID =" + Tools.Tools.LangID + ") AND (UnitID = " + MyClass.GetViewUnitID + ")");
			while (MyRead.Read())
			{
				if (UID == "")
				{
					MyRead.Close(); MyRead.Dispose();
					return false;
				}
				else
				{
					//if (AccessTypeID.IndexOf(","+MyCL.MGInt(MyRead, 0).ToString()) !=-1 )
					if (AccessTypeID.IndexOf("," + MyCL.MGInt(MyRead, 0).ToString()+",") != -1)
					{
						MyRead.Close(); MyRead.Dispose();
						return true;
					}
				}
			}
			MyRead.Close(); MyRead.Dispose();
			return false;
		}
		public static string NotLoginGuestFilterID(Tools.MyVar.SiteGuest MyType, string FieldName, bool ViewUserItem = true)
		{
			string SQLComm = "";
			if (CheckTokenGuestUserID() < 0)
			{
				string SelID = "";
				SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT ID  FROM GuestAccess  WHERE (UnitID = " + MyClass.GetViewUnitID + ") AND (Type = " + (int)MyType + ") AND (LangID = " + Tools.Tools.LangID + ") GROUP BY ID");
				while (MyRead.Read())
					SelID += "," +MyCL.MGInt(MyRead, 0);
				MyRead.Close(); MyRead.Dispose();
				if (SelID != "")
					SQLComm += "and " + FieldName + " not in(" + SelID.Substring(1) + ")";
			}
			else
			{
				if (ViewUserItem)
				{
					string SelID = "";
					string AccessType = DAL.ExecuteData.CNTDataStr("SELECT GuestAccessTypeIDs  FROM  GuestInfo WHERE (GuestID = " + CheckTokenGuestUserID() + ")");
                    SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT ID  FROM GuestAccess  WHERE (UnitID = " + MyClass.GetViewUnitID + ") AND (Type = " + (int)MyType + ") AND (LangID = " + Tools.Tools.LangID + ")  AND (GuestAccessTypeID in (" + AccessType.TrimStart(',').TrimEnd(',') + "))  GROUP BY ID");
					while (MyRead.Read())
						SelID += "," + MyCL.MGInt(MyRead, 0);
					MyRead.Close(); MyRead.Dispose();
					if (SelID != "")
						SQLComm += "and " + FieldName + " in(" + SelID.Substring(1) + ")";
				}
			}
			return SQLComm;
		}
		public static string LoginGuestFilterID(Tools.MyVar.SiteGuest MyType, string FieldName)
		{
			string SQLComm = "";
			if (CheckTokenGuestUserID() > 0)
			{
				string UID = CheckTokenGuestUserID().ToString();
				string AccessTypeID = MyClass.MyDecry(HttpContext.Current.Request.Cookies["GuestUserAccessID"].Value);

				string SelID = "";
				SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT ID  FROM GuestAccess  WHERE (UnitID = " + MyClass.GetViewUnitID + ") and (GuestAccessTypeID in (" + AccessTypeID.TrimStart(',').TrimEnd(',') + ")) AND (Type = " + (int)MyType + ") AND (LangID = " + Tools.Tools.LangID + ") GROUP BY ID");
				while (MyRead.Read())
					SelID += "," + MyCL.MGInt(MyRead, 0);
				MyRead.Close(); MyRead.Dispose();
				if (SelID != "")
					SQLComm += " and " + FieldName + " in(" + SelID.Substring(1) + ")";
			}
			return SQLComm;
		}
		public static string GetFilesRoot(bool isFolder = false)
		{
			if (isFolder)
				return HttpContext.Current.Server.MapPath("/Files") + "\\" + MyClass.GetViewUnitID + "";
			return "/Files/" + MyClass.GetViewUnitID + "";
		}
		public static int CheckTokenGuestUserID()
		{
			if (HttpContext.Current.Request.Cookies["GuestUserID"] == null)
				return -1;
			string KeyStr = MyClass.MyDecry(HttpContext.Current.Request.Cookies["GuestUserID"].Value);
			if (KeyStr.IndexOf("@") == -1)
				return -1;
			try
			{
				DateTime MyD = Convert.ToDateTime(KeyStr.Substring(KeyStr.IndexOf("@") + 1));
				TimeSpan TS = MyD - DateTime.Now;

				int LoginTime = Tools.Tools.ConvertToInt32(Tools.Tools.GetSetting(367, "30"));
				int comp = TS.Minutes;
				if (comp > LoginTime)
					return -1;
				if (comp < -LoginTime)
					return -1;

				HttpCookie httpCookie1 = new HttpCookie("GuestUserID", MyClass.MyEncry(KeyStr.Substring(0, KeyStr.IndexOf("@")) + "@" + DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM")));
				httpCookie1.Expires = DateTime.Now.AddMinutes(Tools.Tools.ConvertToInt32(Tools.Tools.GetSetting(367, "30")));
				httpCookie1.Domain = Tools.Tools.GetSetting(346, "");
				HttpContext.Current.Response.Cookies.Add(httpCookie1);

				return Tools.Tools.ConvertToInt32(KeyStr.Substring(0, KeyStr.IndexOf("@")));
			}
			catch
			{
				return -1;
			}
		}
		public static Boolean CheckGuestUserLogin()
		{
			if (HttpContext.Current.Request.Cookies["GuestUserID"] != null)
			{
				try
				{
					string UID = CheckTokenGuestUserID().ToString();
					return true;
				}
				catch
				{
					return false;
				}
			}
			else
				return false;
		}
		public static string GetUserLogonState()
		{
			//return "";
			int LoginUserID = GuestUserLoginID();
			if (LoginUserID <= 0)
				return "";
			else
			{
				string FName = DAL.ExecuteData.CNTDataStr("SELECT RTRIM(LTRIM(Name)) + ' ' + Family AS Family  FROM GuestInfo WHERE (GuestID = " + LoginUserID + ")");
				if (FName.Trim() == "")
					return "";
				return "<div class=\"UserStateDiv\"><span id=\"UserLoginSatteName\">" + FName + "</span>  <a href=\"/Members/MemberInfo.aspx\" class=\"UserHome\">" + Tools.Tools.MyGetGlobalResourceObject("resource", "UserPage").ToString() + "</a>&nbsp;&nbsp; <a href=\"/Members/MemberLogin-EX.aspx\" class=\"UserExit\">" + Tools.Tools.MyGetGlobalResourceObject("resource", "Exit").ToString() + "</a></div>";
			}
		}
		public static string UserLogonStateCustome()
		{
			int LoginUserID = GuestUserLoginID();
			if (LoginUserID <= 0)
				return Tools.Tools.GetSetting(481);
			else
			{
				string FName = DAL.ExecuteData.CNTDataStr("SELECT RTRIM(LTRIM(Name)) + ' ' + Family AS Family  FROM GuestInfo WHERE (GuestID = " + LoginUserID + ")");
				return Tools.Tools.GetSetting(480).Replace("#ImageSRC#",GetImageSRC(LoginUserID.ToString())).Replace("#GuestID#",LoginUserID.ToString()).Replace("#Name#",FName).Replace("#NameLink#", "<a href=\"/Members/MemberInfo.aspx\" class=\"UserHome\">"+FName+"</a>").Replace("#Exit#", "<a href=\"/Members/MemberLogin-EX.aspx\" class=\"UserExit\">" + Tools.Tools.MyGetGlobalResourceObject("resource", "Exit").ToString() + "</a>").Replace("#UserPage#","<a href=\"/Members/MemberInfo.aspx\" class=\"UserHome\">" + Tools.Tools.MyGetGlobalResourceObject("resource", "UserPage").ToString() + "</a>");
			}
		}
		public static string GetImageSRC(string UserID)
		{
			if (System.IO.File.Exists(DAL.CheckData.GetFilesRoot(true) + @"\Images\Members\" + UserID + ".jpg"))
				return DAL.CheckData.GetFilesRoot() + "/Images/Members/" + UserID + ".jpg";

			return "/Images/Users.png";
		}
		public static string GetUserLogonName()
		{
			//return "";
			int LoginUserID = GuestUserLoginID();
			if (LoginUserID <= 0)
				return "";
			else
			{
				string FName = DAL.ExecuteData.CNTDataStr("SELECT RTRIM(LTRIM(Name)) + ' ' + Family AS Family  FROM GuestInfo WHERE (GuestID = " + LoginUserID + ")");
				if (FName.Trim() == "")
					return "";
				return FName;
			}
		}
		public static int GuestUserLoginID()
		{
			if (HttpContext.Current.Request.Cookies["GuestUserID"] != null)
			{
				try
				{
					return CheckTokenGuestUserID();
				}
				catch
				{
					return 0;
				}
			}
			else
				return 0;
		}
		public static int GuestLoginUser(string UserName, string Pass)
		{
			//HttpContext.Current.Session.Abandon();
			SqlConnection MyConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PortalConnectionStr"].ConnectionString);
			SqlCommand MyComm = new SqlCommand("", MyConnection);
			MyConnection.Open();
			HttpCookie httpCookie1;
			MyComm.CommandText = "SELECT GuestID,GuestAccessTypeIDs, Name,Disable FROM GuestInfo WHERE (UserName = @UserName) AND (Pass = @Pass) and (unitid=" + MyClass.GetViewUnitID + ")";
			MyComm.Parameters.Add("@UserName", SqlDbType.Char, 32);
			MyComm.Parameters.Add("@Pass", SqlDbType.Char, 32);
			MyComm.Parameters["@UserName"].Value = UserName;
			MyComm.Parameters["@Pass"].Value = MyClass.MyEncry(Pass);
			SqlDataReader MyRead = MyComm.ExecuteReader();
			if (MyRead.Read())
			{
				int Disable = MyCL.MGInt(MyRead, 3);
				if (Disable == 0)
				{
					httpCookie1 = new HttpCookie("GuestUserID", MyClass.MyEncry(MyCL.MGInt(MyRead, 0).ToString() + "@" + DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM")));
					httpCookie1.Expires = DateTime.Now.AddHours(3.0);
					httpCookie1.Domain = Tools.Tools.GetSetting(346, "");
					HttpContext.Current.Response.Cookies.Add(httpCookie1);
					HttpCookie httpCookie2 = new HttpCookie("GuestUserAccessID", MyClass.MyEncry(MyCL.MGStr(MyRead, 1).ToString()));
					httpCookie2.Expires = DateTime.Now.AddDays(3.0);
					httpCookie2.Domain = Tools.Tools.GetSetting(346, "");
					HttpContext.Current.Response.Cookies.Add(httpCookie2);
					MyRead.Close();
					MyRead.Dispose();
					MyComm.Parameters.Clear();
					MyConnection.Close();
					return 0;
				}
				else
					return Disable;
			}
			MyRead.Close(); MyRead.Dispose();
			MyRead.Dispose();
			MyComm.Parameters.Clear();
			MyConnection.Close();
			return -1;
		}
		public static Boolean GuestLoginUser(int UserID)
		{
			HttpContext.Current.Session.Abandon();
			SqlConnection MyConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PortalConnectionStr"].ConnectionString);
			SqlCommand MyComm = new SqlCommand("", MyConnection);
			MyConnection.Open();
			HttpCookie httpCookie1;
			MyComm.CommandText = "SELECT GuestID,GuestAccessTypeIDs, Name FROM GuestInfo WHERE GuestID=@GuestID and (unitid=" + MyClass.GetViewUnitID + ") and   (Disable = 0)";
			MyComm.Parameters.Add("@GuestID", SqlDbType.Char, 32);
			MyComm.Parameters["@GuestID"].Value = UserID;
			SqlDataReader MyRead = MyComm.ExecuteReader();
			if (MyRead.Read())
			{
				httpCookie1 = new HttpCookie("GuestUserID", MyClass.MyEncry(MyCL.MGInt(MyRead, 0).ToString() + "@" + DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM")));
				httpCookie1.Expires = DateTime.Now.AddHours(3.0);
				httpCookie1.Domain = Tools.Tools.GetSetting(346, "");
				HttpContext.Current.Response.Cookies.Add(httpCookie1);
				HttpCookie httpCookie2 = new HttpCookie("GuestUserAccessID", MyClass.MyEncry(MyCL.MGStr(MyRead, 1).ToString()));
				httpCookie2.Expires = DateTime.Now.AddDays(3.0);
				httpCookie2.Domain = Tools.Tools.GetSetting(346, "");
				HttpContext.Current.Response.Cookies.Add(httpCookie2);
				MyRead.Close();
				MyRead.Dispose();
				MyComm.Parameters.Clear();
				MyConnection.Close();
				return true;
			}
			MyRead.Close(); MyRead.Dispose();
			MyRead.Dispose();
			MyComm.Parameters.Clear();
			MyConnection.Close();
			return false;
		}
		public static string GetUnitSetting(string FiledName, object UnitID)
		{
			return GetUnitSetting(FiledName, UnitID.ToString());
		}
		public static string GetUnitSetting(string FiledName, string UnitID)
		{
			SqlConnection MyConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PortalConnectionStr"].ConnectionString);
			SqlCommand MyComm = new SqlCommand("", MyConnection);

			if (UnitID == null || UnitID.Trim() == "")
				return "";
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@UnitID", UnitID);

			MyComm.CommandText = "SELECT " + FiledName + "  FROM UnitSetting  WHERE (UnitID = @UnitID)";
			MyComm.Parameters.AddWithValue("@FiledName", FiledName);
			MyComm.Parameters.AddWithValue("@UnitID", UnitID);
			MyConnection.Open();
			object OutText = MyComm.ExecuteScalar();
			if (OutText == null)
				return "";
			return OutText.ToString();
		}
		public static string GetUnitSetting(string FiledName, object UnitID, string MyText)
		{
			return GetUnitSetting(FiledName, UnitID.ToString(), MyText);
		}
		public static string GetUnitSetting(string FiledName, string UnitID, string MyText)
		{
			SqlConnection MyConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PortalConnectionStr"].ConnectionString);
			SqlCommand MyComm = new SqlCommand("", MyConnection);

			if (UnitID == null || UnitID.Trim() == "")
				return "";
			MyComm.CommandText = "SELECT " + FiledName + "  FROM UnitSetting  WHERE (UnitID = @UnitID )";
			//MyComm.Parameters.AddWithValue("@FiledName", FiledName);
			MyComm.Parameters.AddWithValue("@UnitID", UnitID);
			MyConnection.Open();
			string OutText = "";
			try
			{
				OutText = MyComm.ExecuteScalar().ToString().Trim();
			}
			catch { }
			MyConnection.Close();
			if (OutText == "")
				return "";
			return MyText + OutText;
		}
		public static string GetUnitSetting(string FiledName, string UnitID, string MyFText, string MyEndText)
		{
			SqlConnection MyConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PortalConnectionStr"].ConnectionString);
			SqlCommand MyComm = new SqlCommand("", MyConnection);

			if (UnitID == null || UnitID.Trim() == "")
				return "";
			MyComm.CommandText = "SELECT " + FiledName + "  FROM UnitSetting  WHERE (UnitID = " + UnitID + ")";
			MyConnection.Open();
			string OutText = "";
			try
			{
				OutText = MyComm.ExecuteScalar().ToString().Trim();
			}
			catch { }
			MyConnection.Close();
			if (OutText == "")
				return "";
			return MyFText + OutText + MyEndText;
		}
		public static string GetUnitSetting(string FiledName, object UnitID, string MyFText, string MyEndText)
		{
			return GetUnitSetting(FiledName, UnitID.ToString(), MyFText, MyEndText);
		}
	}
}
