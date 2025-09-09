using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
namespace Tools
{
	public class Calender
	{
		public static string[] FarMonth = { "", "فروردين", "ارديبهشت", "خرداد", "تير", "مرداد", "شهريور", "مهر", "آبان", "آذر", "دي", "بهمن", "اسفند" };
		public static string[] AraMonth = { "", "محرم", "صفر", "ربيع الاول", "ربيع الثاني", "جمادي الاول", "جمادي الثاني", "رجب", "شعبان", "رمضان", "شوال", "ذي القعده", "ذي الحجه" };
		public static string[] EngMonth = { "", "ژانويه", "فوريه", "مارس", "آوريل", "مي", "ژوئن", "جولاي", "آگوست", "سپتامبر", "اكتبر", "نوامبر", "دسامبر" };
		public static string[] DayNames = { "يکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" };
		public static DateTime ChangeTime(DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
		{
			return new DateTime(
				dateTime.Year,
				dateTime.Month,
				dateTime.Day,
				hours,
				minutes,
				seconds,
				milliseconds,
				dateTime.Kind);
		}
		public static string MyPDate()
		{
			PersianCalendar MyPCalc = new PersianCalendar();
			DateTime MyDate = DateTime.Now;
			return MyPCalc.GetYear(MyDate).ToString() + "/" + CheckLength(MyPCalc.GetMonth(MyDate).ToString()) + "/" + CheckLength(MyPCalc.GetDayOfMonth(MyDate).ToString());
		}
		public static string MyPDate(DateTime MyDT)
		{
			PersianCalendar MyPCalc = new PersianCalendar();
			DateTime MyDate = MyDT;
			return MyPCalc.GetYear(MyDate).ToString() + "/" + MyPCalc.GetMonth(MyDate) + "/" + MyPCalc.GetDayOfMonth(MyDate);
		}
		public static string MyPDate(string MyDT)
		{
			if (MyDT.Equals(null) || MyDT.Trim() == "")
				return "";
			PersianCalendar MyPCalc = new PersianCalendar();
			DateTime MyDate = Convert.ToDateTime(MyDT);
			return MyPCalc.GetYear(MyDate).ToString() + "/" + MyPCalc.GetMonth(MyDate) + "/" + MyPCalc.GetDayOfMonth(MyDate);
		}
		public static string MyPDate(object MyDT)
		{
			if (MyDT.Equals(null) || MyDT.ToString().Trim() == "")
				return "";
			PersianCalendar MyPCalc = new PersianCalendar();
			DateTime MyDate = Convert.ToDateTime(MyDT);
			return MyPCalc.GetYear(MyDate).ToString() + "/" + MyPCalc.GetMonth(MyDate) + "/" + MyPCalc.GetDayOfMonth(MyDate);
		}
		public static string GetTime(DateTime MyDT)
		{
			//return MyDT.Hour + ":" + MyDT.Minute + " ";
			return String.Format("{0:T}", MyDT);
		}
		public static string GetTime(string MyDT)
		{
			//return MyDT.Hour + ":" + MyDT.Minute + " ";
			return GetTime(Convert.ToDateTime(MyDT));
		}
		public static string MyADate(string MyDT)
		{
			HijriCalendar MyPCalc = new HijriCalendar();
			DateTime MyDate = Convert.ToDateTime(MyDT);
			return MyPCalc.GetYear(MyDate).ToString() + "/" + MyPCalc.GetMonth(MyDate) + "/" + MyPCalc.GetDayOfMonth(MyDate);

		}

		public static string MyPDateTime(string MyDT)
		{
			if (MyDT.Trim() == "")
				return "";
			PersianCalendar MyPCalc = new PersianCalendar();
			DateTime MyDate = Convert.ToDateTime(MyDT);
			return MyPCalc.GetYear(MyDate).ToString() + "/" + MyPCalc.GetMonth(MyDate) + "/" + MyPCalc.GetDayOfMonth(MyDate) + "-" + MyDate.Hour + ":" + MyDate.Minute;
		}
		public static int[] GetPersianDate(DateTime MyDate)
		{
			int[] MyOut = new int[3];
			PersianCalendar MyPCalc = new PersianCalendar();
			MyOut[0] = MyPCalc.GetYear(MyDate);
			MyOut[1] = MyPCalc.GetMonth(MyDate);
			MyOut[2] = MyPCalc.GetDayOfMonth(MyDate);
			return MyOut;
		}
		public static int[] GetPersianDateTime(DateTime MyDate)
		{
			int[] MyOut = new int[5];
			PersianCalendar MyPCalc = new PersianCalendar();
			MyOut[0] = MyPCalc.GetYear(MyDate);
			MyOut[1] = MyPCalc.GetMonth(MyDate);
			MyOut[2] = MyPCalc.GetDayOfMonth(MyDate);
			MyOut[3] = MyPCalc.GetHour(MyDate);
			MyOut[4] = MyPCalc.GetMinute(MyDate);
			return MyOut;
		}
		public static int[] GetPersianDateTime(object MyDate)
		{
			return GetPersianDateTime(Convert.ToDateTime(MyDate));
		}
		public static int[] GetArabicDate(DateTime MyDate)
		{
			int[] MyOut = new int[3];
			MyDate = MyDate.AddDays(Tools.ConvertToInt32(Tools.GetSetting(344, "0")));
			HijriCalendar MyPCalc = new HijriCalendar();
			MyOut[0] = MyPCalc.GetYear(MyDate);
			MyOut[1] = MyPCalc.GetMonth(MyDate);
			MyOut[2] = MyPCalc.GetDayOfMonth(MyDate);
			return MyOut;
		}
		public static int[] GetAdminArabicDate(DateTime MyDate)
		{
			int[] MyOut = new int[3];
			MyDate = MyDate.AddDays(Tools.ConvertToInt32(Tools.GetAdminSetting(344, "0")));
			HijriCalendar MyPCalc = new HijriCalendar();
			MyOut[0] = MyPCalc.GetYear(MyDate);
			MyOut[1] = MyPCalc.GetMonth(MyDate);
			MyOut[2] = MyPCalc.GetDayOfMonth(MyDate);
			return MyOut;
		}
		public static int[] GetEngDate(DateTime MyDate)
		{
			int[] MyOut = new int[3];
			MyOut[0] = MyDate.Year;
			MyOut[1] = MyDate.Month;
			MyOut[2] = MyDate.Day;
			return MyOut;
		}
		public static string GetCurPersianDate()
		{
			int[] MyMon = GetPersianDate(DateTime.Now);
			return "امروز " + DayNames[(int)DateTime.Now.DayOfWeek] + "، " + MyMon[2] + " " + FarMonth[MyMon[1]] + " " + MyMon[0];
		}
		public static string GetPersianDateNumber()
		{
			int[] MyMon = GetPersianDate(DateTime.Now);
			return MyMon[2] + "/" + MyMon[1] + "/" + MyMon[0];
		}
		public static string GetPersianDateNumber(DateTime MyDate)
		{
			int[] MyMon = GetPersianDate(MyDate);
			return MyMon[2] + "/" + MyMon[1] + "/" + MyMon[0];
		}
		public static string GetCurArabicDate()
		{
			int[] MyMon = GetArabicDate(DateTime.Now);
			return "  " + MyMon[2] + " " + AraMonth[MyMon[1]] + " " + MyMon[0];
		}
		public static string GetCurEngDate()
		{
			int[] MyMon = GetEngDate(DateTime.Now);
			return " " + MyMon[2] + " " + EngMonth[MyMon[1]] + " " + MyMon[0];
		}
		public static string GetMiladiDate(long iYear, long iMonth, long iDay)
		{
			return jdn_civil(persian_jdn(iYear, iMonth, iDay), iYear, iMonth, iDay);
		}
		public static string jdn_civil(long jdn, long iyear, long imonth, long iday)
		{
			long l;
			//long k;
			long n;
			long i;
			long j;

			if (jdn > 2299160)
			{
				l = jdn + 68569;
				n = ((4 * l) / 146097);
				l = l - ((long)((146097 * n + 3) / 4));
				i = ((4000 * (l + 1)) / 1461001);
				l = l - ((long)((1461 * i) / 4)) + 31;
				j = ((80 * l) / 2447);
				iday = l - ((long)((2447 * j) / 80));
				l = (j / 11);
				imonth = j + 2 - 12 * l;
				iyear = 100 * (n - 49) + i + l;
				return iyear.ToString() + "/" + imonth + "/" + iday;
			}
			else
				return jdn_julian(jdn, iyear, imonth, iday);
		}
		public static string jdn_julian(long jdn, long iYear, long iMonth, long iDay)
		{
			long l;
			long k;
			long n;
			long i;
			long j;

			j = jdn + 1402;
			k = ((j - 1) / 1461);
			l = j - 1461 * k;
			n = ((long)((l - 1) / 365)) - ((long)(l / 1461));
			i = l - 365 * n + 30;
			j = ((80 * i) / 2447);
			iDay = i - ((long)((2447 * j) / 80));
			i = (j / 11);
			iMonth = j + 2 - 12 * i;
			iYear = 4 * k + n + i - 4716;
			return iYear.ToString() + "/" + iMonth + "/" + iDay;
		}
		public static long persian_jdn(long iYear, long iMonth, long iDay)
		{
			const int PERSIAN_EPOCH = 1948321; // The JDN of 1 Farvardin 1
			long epbase;
			long epyear;
			long mdays;
			if (iYear >= 0)
				epbase = iYear - 474;
			else
				epbase = iYear - 473;
			epyear = 474 + (epbase % 2820);
			if (iMonth <= 7)
				mdays = ((long)(iMonth) - 1) * 31;
			else
				mdays = ((long)(iMonth) - 1) * 30 + 6;

			return (long)(iDay) + mdays + (int)(((epyear * 682) - 110) / 2816) + (epyear - 1) * 365 + (int)(epbase / 2820) * 1029983 + (PERSIAN_EPOCH - 1);
		}
		public static DateTime PersianToEnglish(object Year, object Mon, object Day, object Hour, object Minute)
		{
			System.Globalization.PersianCalendar persianCal = new System.Globalization.PersianCalendar();
			if (Tools.ConvertToInt32( Hour) == 24)
			{ 
				Hour = 23;
				Minute = 59;
			}
			DateTime GregorianDate = persianCal.ToDateTime(Convert.ToInt32(Year), Convert.ToInt32(Mon), Convert.ToInt32(Day), Convert.ToInt32(Hour), Convert.ToInt32(Minute), 0, 0);

			return GregorianDate;
		}

		////////////  ***************   By Gheysar   *****************

		public static DateTime PersianToEnglishCheck(string date, string Hour = "0", string Min = "0")
		{
			DateTime MyDate;
			if (date.Substring(4, 1) == "/")
				MyDate = PersianToEnglish(date.Substring(0, 4), date.Substring(5, 2), date.Substring(8, 2), Hour, Min);
			else
				MyDate = PersianToEnglish(date.Substring(6, 4), date.Substring(3, 2), date.Substring(0, 2), Hour, Min);

			return MyDate;
		}

		public static DateTime PersianToEnglishCheck2(string input)
		{
			DateTime MyDate;
			string pattern = @"\d{4}/\d{2}\/\d{2}\/";
			Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
			MatchCollection matches = rgx.Matches(input);
			if (matches.Count > 0)
				MyDate = PersianToEnglish(input.Substring(0, 4), input.Substring(5, 2), input.Substring(8, 2), 0, 0);
			else
				MyDate = PersianToEnglish(input.Substring(6, 4), input.Substring(3, 2), input.Substring(0, 2), 0, 0);
			return MyDate;
		}

		public static string MyPDateCheck(string MyDT)
		{
			PersianCalendar MyPCalc = new PersianCalendar();
			DateTime MyDate = Convert.ToDateTime(MyDT);
			return MyPCalc.GetYear(MyDate).ToString() + "/" + CheckLength(MyPCalc.GetMonth(MyDate).ToString()) + "/" + CheckLength(MyPCalc.GetDayOfMonth(MyDate).ToString());
		}
		private static string CheckLength(string _in)
		{
			if (_in.Length == 1)
				return "0" + _in;
			return _in;
		}

		public static DateTime CheckDateTime(string year, string mon, string day, string hour, string min)
		{
			DateTime MyCurDate = PersianToEnglish(year, mon, day, hour, min);
			//if (MyCurDate < DateTime.Now)
			//	return false;
			//Mysenddate = MyCurDate;
			return MyCurDate;
		}


		public static string GetFreeDateTitle(DateTime dateTime)
		{
			dateTime = ChangeTime(dateTime, 0, 0, 0, 0);
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@Date", dateTime);
			string OutText = DAL.ExecuteData.CNTDataStr("SELECT Name  FROM ProjectCalenderFreeDay WHERE (UnitID = " + MyClass.GetViewUnitID + ") AND (Date = @Date)",SP);
			if (OutText != "")
				OutText = " - " + OutText;
			return OutText;
		}
		public static bool GetFreeDate(DateTime dateTime)
		{
			dateTime = ChangeTime(dateTime, 0, 0, 0, 0);
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@Date", dateTime);
			string OutText = DAL.ExecuteData.CNTDataStr("SELECT Name  FROM ProjectCalenderFreeDay WHERE (UnitID = " + MyClass.GetViewUnitID + ") AND (Date = @Date)", SP);
			if (OutText != "")
				return true;
			return false;
		}

		public static DateTime GetFirstDayOfWeek
		{
			get
			{
				DateTime dt = DateTime.Now;
				while (dt.DayOfWeek != DayOfWeek.Saturday) dt = dt.AddDays(+1);
				return dt;
			}
		
		}
		public static DateTime GetEndDayOfWeek
		{
			get
			{
				DateTime dt = DateTime.Now;
				while (dt.DayOfWeek != DayOfWeek.Friday) dt = dt.AddDays(+1);
				return dt;
			}
		
		}

		public static string ReverseDate(string myDate)
		{
			if (myDate == "")
				return "";
			if (myDate.Substring(2, 1) == "/")//15/09/1390
				return myDate.Substring(6, 4) + "/" + myDate.Substring(3, 2) + "/" + myDate.Substring(0, 2);
			return myDate;
		}
	}
}