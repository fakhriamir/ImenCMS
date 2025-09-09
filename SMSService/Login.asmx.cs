using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SMSService
{
	/// <summary>
	/// Summary description for Login
	/// </summary>
	[WebService(Namespace = "http://www.ImenCMS.com/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	
	public class Login : System.Web.Services.WebService
	{
		[WebMethod]
		public string LoginUser(string UserName,string Pass,string Key,string Token)
		{
			if (Tools.Tools.MyDecry(Key, SMSClass.KeySTR) == "" || Tools.Tools.MyDecry(Token, SMSClass.KeySTR) == "")
				return "-3";
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@UserName", Tools.Tools.MyDecry(UserName,SMSClass.KeySTR));
			SP.AddWithValue("@Pass", Tools.Tools.MyEncry(Tools.Tools.MyDecry(Pass, SMSClass.KeySTR)));
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT SMS_UserID,State,name,MobileNo FROM  SMS_User WHERE (UserName = @UserName) AND (Pass = @Pass)", SP);
			if (MyRead.Read())
			{
				if (Tools.MyCL.MGInt(MyRead, 1) == 2)
				{
					return "-130";//بلوک شده
				}
				string UserID = Tools.Tools.MyEncry(Tools.MyCL.MGInt(MyRead, 0).ToString() + "{#}" + Tools.MyCL.MGStr(MyRead, 2) + "{#}" + Tools.MyCL.MGStr(MyRead, 3) + "{#}" + GetUserLineNo(Tools.MyCL.MGInt(MyRead, 0)), SMSClass.KeySTR);
			
				MyRead.Close();
				MyRead.Dispose();				
				//httpCookie1.Name=;
				return UserID;
			}
			MyRead.Close();
			MyRead.Dispose();
			return "-2";
		}
		[WebMethod]
		public string GetPublicLine(string Key, string Token)
		{
			if (Tools.Tools.MyDecry(Key, SMSClass.KeySTR) == "" || Tools.Tools.MyDecry(Token, SMSClass.KeySTR) == "")
				return "-3";
			string LineSTR = "";
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT [LineNo]  FROM SMS_LineNo  WHERE (SMS_UserID = -1)");
			while (MyRead.Read())
				LineSTR += "#" + Tools.MyCL.MGStr(MyRead, 0);
			MyRead.Close();
			MyRead.Dispose();

			return LineSTR.TrimStart('#');
		}
		[WebMethod]
		public int GetPostalCodeCNT(int Pcode,string Key, string Token)
		{
			if (Tools.Tools.MyDecry(Key, SMSClass.KeySTR) == "" || Tools.Tools.MyDecry(Token, SMSClass.KeySTR) == "")
				return -3;
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@PhoneNo", Pcode + "%");
			int CNT =DAL.ExecuteData.CNTData("SELECT count(*)  FROM SMS_PostalCode  WHERE (Postcode LIKE @PhoneNo)", SP);
			return CNT;
		}
		[WebMethod]
		public int GetCredit(string Token)
		{
			int UserID = 0;
			int AccessRetVal = SMSClass.CheckAccess(ref UserID, Token);
			if (AccessRetVal < 0)
				return AccessRetVal;
			int SMSActiveCount = Tools.Tools.ConvertToInt32(DAL.ExecuteData.CNTData("SELECT Count  FROM SMS_SMSActive WHERE SMS_UserID=" + UserID));
			int SMSSend = Tools.Tools.ConvertToInt32(DAL.ExecuteData.CNTData("SELECT SUM(SendCount) AS CNT   FROM SMS_SMSLog WHERE SMS_UserID=" + UserID));
			return SMSActiveCount - SMSSend;
		}
		[WebMethod]
		public void ErrorLog(string Error, string Key, string Token)
		{
			if (Tools.Tools.MyDecry(Key, SMSClass.KeySTR) == "" || Tools.Tools.MyDecry(Token, SMSClass.KeySTR) == "")
				return;

			DAL.Logging.ErrorLog("SMS_Program", "SMS_Program", Error);
		
		}
		string GetUserLineNo(int UserID)
		{
			string LineSTR = "";
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT [LineNo]  FROM SMS_LineNo  WHERE (SMS_UserID = "+UserID+")",null,true);
			while (MyRead.Read())
				LineSTR += "#" + Tools.MyCL.MGStr(MyRead, 0);
			MyRead.Close();
			MyRead.Dispose();
			
			return LineSTR.TrimStart('#');
		}
	}
}
