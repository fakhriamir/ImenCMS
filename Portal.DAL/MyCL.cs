using System;
using System.Data.OleDb;
using System.Data.SqlClient;
namespace Tools
{
	public class MyCL
	{
		public class TelegramUserInfo
		{
			public string UserName { get; set; }
			public int RobotID { get; set; }
			public string FirstName { get; set; }
			public TelegramUserInfo(string Firstname="",string Username="", int RobotiD=0)
			{
				UserName = Username;
				FirstName=Firstname;
				RobotID = RobotiD;
			}
		}
		public static int MGInt(SqlDataReader MyRead, int ID)
		{
			if (MyRead.IsDBNull(ID))
				return 0;
			else
				return Tools.ConvertToInt32(MyRead.GetValue(ID));
		}
		public static decimal MGDecimal(SqlDataReader MyRead, int ID)
		{
			if (MyRead.IsDBNull(ID))
				return 0;
			else
				return Tools.ConvertTodecimal(MyRead.GetValue(ID));
		}
		public static int MGInt(SqlDataReader MyRead, string TitleSelect)
		{
			int ID = MyRead.GetOrdinal(TitleSelect);
			if (MyRead.IsDBNull(ID))
				return 0;
			else
				return Tools.ConvertToInt32(MyRead.GetValue(ID));
		}
		static public string MGStr(SqlDataReader Myred, int ID)
		{
			if (!Myred.IsDBNull(ID))
				return Myred.GetString(ID).Replace("ي", "ی").Replace("ي", "ی").Replace("ي", "ی").Replace("ي", "ی").Replace("ك", "ک").Replace("ك", "ک").Replace("ك", "ک").Trim();
			else
				return "";
		}
        static public string MGUID(SqlDataReader Myred, int ID)
        {
            if (!Myred.IsDBNull(ID))
                return Myred.GetValue(ID).ToString().Trim();
            else
                return "";
        }
        public static string MGStr(SqlDataReader MyRead, string TitleSelect)
		{
			int ID = MyRead.GetOrdinal(TitleSelect);
			if (!MyRead.IsDBNull(ID))
				return MyRead.GetString(ID).Replace("ي", "ی").Trim();
			else
				return "";
		}
		static public string MGGuID(SqlDataReader Myred, int ID)
		{
			if (!Myred.IsDBNull(ID))
				return Myred.GetGuid(ID).ToString();//.Replace("ي", "ي").Trim();
			else
				return "";
		}
		static public string MGMon__(SqlDataReader Myred, int ID)
		{
			if (!Myred.IsDBNull(ID))
				return Myred.GetSqlMoney(ID).ToString();//.Replace("ي", "ي").Trim();
			else
				return "";
		}
		static public DateTime MGDTDT(SqlDataReader Myred, int ID)
		{
			if (!Myred.IsDBNull(ID))
				return Myred.GetDateTime(ID);
			else
				return DateTime.Now;
		}
		static public string MGDT(SqlDataReader Myred, int ID)
		{
			if (!Myred.IsDBNull(ID))
				return Myred.GetDateTime(ID).ToString().Trim();
			else
				return "";
		}
		public static bool MGBool(SqlDataReader My11Read, int ID)
		{
			return My11Read.GetBoolean(ID);
		}
	
		public static long MGLong(SqlDataReader MyRead, int ID)
		{
			if (MyRead.IsDBNull(ID))
				return 0;
			else
				return Tools.ConvertToInt64(MyRead.GetValue(ID));
		}
		public static string MGBin(SqlDataReader MyRead, int ID)
		{
			if (MyRead.IsDBNull(ID))
				return "0";
			else
				return MyRead.GetValue(ID).ToString();
		}
		public static string MGval(SqlDataReader MyRead, int ID)
		{
			if (MyRead.IsDBNull(ID))
				return "";
			else
				return MyRead.GetValue(ID).ToString();
		}
		public static string MGVal(OleDbDataReader MyRead, string TitleSelect)
		{
			int ID = MyRead.GetOrdinal(TitleSelect);
			if (!MyRead.IsDBNull(ID))
				return MyRead.GetValue(ID).ToString().Replace("ي", "ی").Trim();
			else
				return "";
		}
	}
}