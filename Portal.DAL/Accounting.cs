using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Telegram.Bot.Types;
using Newtonsoft.Json;
using System.IO;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Data;
using System.Text;
namespace Tools
{
	public class Accounting
	{
		
	
		
		

		public static bool UserAccessTextType(string TextTypeID, int TelegramUserID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@TelegramGalleryTypeID", TextTypeID);
			int CNT = DAL.ExecuteData.CNTData("SELECT COUNT(*) AS Expr1 FROM TelegramTextType  WHERE (TelegramTextTypeID = @TelegramGalleryTypeID) AND (TelegramUserID =" + TelegramUserID + ")", SP);
			if (CNT == 1)
				return true;
			return false;
		}
		
		public static string[] UserTypeName = { "غیر فعال", "معمولی", "برنزی", "برنزی ویژه", "نقره ای", "نقره ای ویژه", "طلایی", "طلایی ویژه" };
		
		public static string GetUserState(int TelegramUserID)
		{
			return UserTypeName[DAL.ExecuteData.CNTData("SELECT UserType  FROM TelegramUser  WHERE (TelegramUserID = " + TelegramUserID + ")")];
		}
		public static int GetUserTypeID(int TelegramUserID)
		{
			return DAL.ExecuteData.CNTData("SELECT UserType  FROM TelegramUser  WHERE (TelegramUserID = " + TelegramUserID + ")");
		}
		
		public static int GetHaveAccess(MyVar.TelegramPageType MyPage, int UserID)
		{
			switch (MyPage)
			{
				/*case MyVar.TelegramPageType.AutoAnswer:
					return UserTypeAutoAnswer[GetUserTypeID(UserID)];
				case MyVar.TelegramPageType.Pos:
					return UserTypePos[GetUserTypeID(UserID)];
				case MyVar.TelegramPageType.Texts:
					return UserTypeTexts[GetUserTypeID(UserID)];
				case MyVar.TelegramPageType.Contact:
					return UserTypeContact[GetUserTypeID(UserID)];
				case MyVar.TelegramPageType.AllMess:
					return UserTypeAllMess[GetUserTypeID(UserID)];
				case MyVar.TelegramPageType.ViewRecive:
					return UserTypeViewRecive[GetUserTypeID(UserID)];
				case MyVar.TelegramPageType.Vote:
					return UserTypeVote[GetUserTypeID(UserID)];*/
				default:
					return 0;
			}
		}


		public static void FillYearDL(DropDownList YearDL,int CoID,int UID)
		{
			YearDL.DataSource = DAL.ViewData.MyDT("SELECT AccountingYearID, Name  FROM AccountingYear WHERE (AccountingCompanyID = " + CoID + ") and (accountinguserid=" + UID + ") ORDER BY Name DESC");
			YearDL.DataBind();
			Tools.SetDropDownListValue(YearDL,DAL.ExecuteData.CNTDataStr("SELECT TOP (1) AccountingYearID FROM AccountingYear WHERE (AccountingCompanyID = " + CoID + ") and (accountinguserid=" + UID + ") ORDER BY Def DESC"));
		}
	}
}