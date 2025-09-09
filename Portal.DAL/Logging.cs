using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace DAL
{
	public class Logging
	{
		public static void __AddLog(int MyType)
		{
			SqlConnection MyConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PortalConnectionStr"].ConnectionString);
			SqlCommand MyComm = new SqlCommand("", MyConnection);
			MyConnection.Open();
			string UID = ADAL.A_CheckData.GetUserID();
			if (UID == "")
				UID = "-1";
			string PageName = System.Web.HttpContext.Current.ApplicationInstance.Request.FilePath.Substring(1);
			PageName = PageName.Substring(PageName.LastIndexOf("/") + 1);
			string MyIP = Tools.Tools.GetUserIPAddress();
			MyComm.CommandText = "SELECT MenuChildID FROM MenuChilds  WHERE (ChildHref = @PageName)";
			MyComm.Parameters.AddWithValue("@PageName", PageName);
			string PageID = MyComm.ExecuteScalar().ToString().Trim();
			MyComm.Parameters.Clear();
			MyComm.CommandText = "INSERT INTO AdminLogs (IP,userid,pageid,event) VALUES (@MyIP ,@UID,@PageID,@MyType)";
			MyComm.Parameters.AddWithValue("@MyIP", MyIP);
			MyComm.Parameters.AddWithValue("@UID", UID);
			MyComm.Parameters.AddWithValue("@PageID", PageID);
			MyComm.Parameters.AddWithValue("@MyType", MyType);

			MyComm.ExecuteNonQuery();
			MyComm.Parameters.Clear();
			MyConnection.Close();
		}
		public static void ErrorLog(string logSource, string logName, string logText)
		{
			SqlConnection MyConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PortalConnectionStr"].ConnectionString);

			SqlCommand MyComm = new SqlCommand("", MyConnection);
			MyConnection.Open();
			string url = System.Web.HttpContext.Current.ApplicationInstance.Request.Url.PathAndQuery.ToString();
			string ipAddress = Tools.Tools.GetUserIPAddress();// System.Web.HttpContext.Current.ApplicationInstance.Request.UserHostAddress;
			string browser = System.Web.HttpContext.Current.ApplicationInstance.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version;
			//string PageName = System.Web.HttpContext.Current.ApplicationInstance.Request.FilePath.Substring(1);
			//string MyIP = Tools.Tools.GetUserIPAddress();
			MyComm.CommandText = "INSERT INTO ErrorLogs (LogSource, LogName, URL, LogText,IPAddress,Browser) VALUES     (@LogSource,@LogName,@URL,@LogText,@IPAddress,@Browser)";
			MyComm.Parameters.AddWithValue("@LogSource", logSource);
			MyComm.Parameters.AddWithValue("@LogName", logName);
			MyComm.Parameters.AddWithValue("@URL", url);
			MyComm.Parameters.AddWithValue("@LogText", logText);
			MyComm.Parameters.AddWithValue("@IPAddress", ipAddress);
			MyComm.Parameters.AddWithValue("@Browser", browser);
			MyComm.ExecuteNonQuery();
			MyComm.Parameters.Clear();
			MyConnection.Close();
		}

		internal static void SenMailLog(string To, string Subject, string MyBody, string UnitID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@To", To);
			SP.AddWithValue("@Subject", Subject);
			SP.AddWithValue("@MyBody", MyBody);
			SP.AddWithValue("@UnitID", UnitID);
			DAL.ExecuteData.AddData("INSERT INTO EmailLog(Subject, Email, txt, UnitID) VALUES (@Subject,@To,@MyBody,@UnitID)", SP);
		}
	}
}
