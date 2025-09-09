using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;
using System.Web;
using System;
using System.Web.UI;
using DAL;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Xml;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.DirectoryServices.AccountManagement;
using System.Security.AccessControl;
using DiskQuotaTypeLibrary;
using System.Collections.Specialized;

namespace Tools
{
	public class Tools
	{
		public static string GetWorkStatus(int inP)
		{
			switch (inP)
			{
                case 0: return "در انتظار";
                case 1: return "اقدام شد";
                case 2: return "لغو شد";
                default: return "";
			}
			
		}
		public static void PostData(Page MyPage, NameValueCollection Inputs, string Url)
		{
			string Method = "post";
			string FormName = "myform";
			MyPage.Response.Clear();
			MyPage.Response.Write("<html><head>");
			MyPage.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
			MyPage.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
			for(int i = 0;i < Inputs.Keys.Count;i++)
			{
				MyPage.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.GetKey(i), Inputs.Get(i)));
			}
			MyPage.Response.Write("</form>");
			MyPage.Response.Write("</body></html>");
			MyPage.Response.End();

		}
		public static string UrlWordReplace(string InText)
		{
			InText = MyClass.SetExplain(ReplaceWords(InText), 50, "");
			InText = InText.Replace("\"", " ").Trim();
			InText = InText.Replace("(", " ");
			InText = InText.Replace(")", " ");
			InText = InText.Replace("-", " ");
			InText = InText.Replace("_", " ");
			InText = InText.Replace("_", " ");
			InText = InText.Replace("”", " ");
			InText = InText.Replace("“", " ");
			InText = InText.Replace("+", " ");
			InText = InText.Replace("؟", " ");
			InText = InText.Replace("!", " ");
		//	InText = InText.Replace("ﹽ", " ");
			InText = InText.Replace("«", " ");
			InText = InText.Replace("»", " ");
			InText = InText.Replace("  ", " ");
			InText = InText.Replace("  ", " ");
			InText = InText.Replace("  ", " ");
			InText = InText.Replace(" ", "-");
		
			//InText = InText.Replace(" ", "-");
			return InText;

		}
		public static string WordOverReplace(string InText)
		{
			InText = InText.Replace("‌", " ");//نیم فاصله
			InText = InText.Replace("َ", " ");
			InText = InText.Replace("ُ", " ");
			InText = InText.Replace("ِ", " ");
			InText = InText.Replace("ّ", " ");
			InText = InText.Replace("\"", " ").Trim();
			InText = InText.Replace("(", " ");
			InText = InText.Replace(")", " ");
			InText = InText.Replace("-", " ");
			InText = InText.Replace("_", " ");
			InText = InText.Replace("_", " ");
			InText = InText.Replace("”", " ");
			InText = InText.Replace("“", " ");
			InText = InText.Replace("؛", " ");
			InText = InText.Replace("+", " ");
			InText = InText.Replace("!", " ");
			InText = InText.Replace("&nbsp;", " ");
			InText = InText.Replace("؟", " ");
			InText = InText.Replace("«", " ");
			InText = InText.Replace("»", " ");
			InText = InText.Replace(":", " ");
			InText = InText.Replace("؛", " ");
			InText = InText.Replace("،", " ");
			InText = InText.Replace("\r\n", " ");
			InText = InText.Replace("  ", " ");
			InText = InText.Replace("  ", " ");
			InText = InText.Replace("  ", " ");
			return InText;
		}
		public static string NumberToString(int Number)
		{
			string[] Yekan = { "", "يك", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" },
				Daheghan = { "", "ده", "بيست", "سي", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" },
				Sadeghan = { "", "يكصد", "دويست", "سيصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" },
				Dah = { "ده", "يازده", "دوازده", "سيزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
			string Tarikh = Number.ToString().Trim();
			string Out = "", ThisOut = "";
			int Len = Tarikh.Length;
			int Loop = 1;
			for (int i = 0; i < Len; i++)
			{
				if (Loop == 1)
					ThisOut = Yekan[Convert.ToInt32(Tarikh.Substring(Len - 1))];
				else if (Loop == 2)
				{
					if (Tarikh.Substring(Len - 2, 1) == "1")
						Out = Dah[Convert.ToInt32(Tarikh.Substring(Len - 1, 1))];
					else
						ThisOut = Daheghan[Convert.ToInt32(Tarikh.Substring(Len - 2, 1))];
				}
				else if (Loop == 3)
					ThisOut = Sadeghan[Convert.ToInt32(Tarikh.Substring(Len - 3, 1))];
				else if (Loop == 4)
					ThisOut = Yekan[Convert.ToInt32(Tarikh.Substring(Len - 4, 1))] + " هزار ";
				if (ThisOut.Trim() != "")
				{
					if (Loop != 1)
						Out = ThisOut + " و " + Out;
					else
						Out = ThisOut + " " + Out;
				}
				ThisOut = "";
				Loop++;
			}
			if (Out.Substring(Out.Length - 3, 3).Trim() == "و")
				Out = Out.Substring(0, Out.Length - 2);
			return Out;
		}
		public static string ReplaceWords(string p,bool br=false)
		{
			p = MyClass.DelTag(p, "div", false, false);
			p = MyClass.DelTag(p, "img", false, false);
			p = MyClass.DelTag(p, "p", false, false);
			if(br)
			{
				p = p.Replace("\r\n"," ");
				p = p.Replace("\n", " ");
				p = p.Replace("\t", " ");
				p = p.Replace("  ", " ");
				p = p.Replace("  ", " ");
			}
			//p = p.Replace(" ", "-");
			p = p.Replace("  ", " ");
			p = p.Replace("  ", " ");
			p = p.Replace("(", "");
			p = p.Replace(")", "");
			p = p.Replace(".", "");
			p = p.Replace("!", "");
			p = p.Replace("@", "");
			p = p.Replace("#", "");
			p = p.Replace("$", "");
			p = p.Replace("%", "");
			p = p.Replace("^", "");
			p = p.Replace("&nbsp;", "");
			p = p.Replace("&", "");
			p = p.Replace("*", "");
			p = p.Replace("_", "");
			p = p.Replace("{", "");
			p = p.Replace("}", "");
			p = p.Replace("[", "");
			p = p.Replace("]", "");
			p = p.Replace("\\", "");
			p = p.Replace("/", "");
			p = p.Replace(":", "");
			p = p.Replace(".", "");
			p = p.Replace("?", "");
			p = p.Replace("#", "");
			p = p.Replace("«", "");
			p = p.Replace("»", "");
			return p;
		}
		public static void __CreateDynamicForm(string FormAction, string FormData)
		{
			ASCIIEncoding encoding = new ASCIIEncoding();
			byte[] data = encoding.GetBytes(FormData);
			// Prepare web request...
			HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(FormAction);
			myRequest.Method = "POST";
			myRequest.ContentType = "application/x-www-form-urlencoded";
			myRequest.ContentLength = data.Length;
			Stream newStream = myRequest.GetRequestStream();
			// Send the data.
			newStream.Write(data, 0, data.Length);
			newStream.Close();
			//HttpContext.Current.Response.Redirect(FormAction);
		}
		public static string SecurityImageCode
		{
			get
			{
				object ImageCode = HttpContext.Current.Session["SecurityImageCode"];
				if (ImageCode == null || ImageCode.ToString().Trim() == "")
					return "";
				else
					return ImageCode.ToString();
			}
			set
			{
				HttpContext.Current.Session["SecurityImageCode"] = value;
			}
		}
		public static void SetPageSeo(Page MyPage, string PageName)
		{
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT Title, Keyword, Description FROM MetaKey  WHERE (UnitID = " + MyClass.GetViewUnitID + ") AND (PageName = '" + PageName + "') AND (LangID = " + Tools.LangID + ")", null);
			if (MyRead.Read())
			{
				MyClass.SetTitle(MyPage, MyCL.MGStr(MyRead, 0), true);
				MyClass.SetMetaTag(MyPage, MyClass.MetaTags.Keywords, MyCL.MGStr(MyRead, 1));
				MyClass.SetMetaTag(MyPage, MyClass.MetaTags.Description, MyCL.MGStr(MyRead, 2));
			}
			MyRead.Close(); MyRead.Dispose();
		}
		public static string PageDir
		{
			get
			{
				if (HttpContext.Current.Request.Cookies["MyLanguage"].Value == null)
				{
					return "rtl";
				}
				string LangStr = HttpContext.Current.Request.Cookies["MyLanguage"].Value.ToLower();
				if (LangStr == "fa-ir")
				{
					return "RTL";
				}
				else if (LangStr == "en-us")
				{
					return "LTR";
				}
				else if (LangStr == "ru-ru")
				{
					return "LTR";
				}
                else if (LangStr == "de-gr")
                {
                    return "LTR";
                }
				else if (LangStr == "ar")
				{
					return "RTL";
				}
				return "RTL";
			}
		}
		public static string PageAlign
		{
			get
			{
				if (HttpContext.Current.Request.Cookies["MyLanguage"].Value == null)
				{
					return "Right";
				}
				string LangStr = HttpContext.Current.Request.Cookies["MyLanguage"].Value.ToLower();
				if (LangStr == "fa-ir")
				{
					return "Right";
				}
				else if (LangStr == "en-us")
				{
					return "Left";
				}
                else if (LangStr == "de-gr")
                {
                    return "Left";
                }
				else if (LangStr == "ru-ru")
				{
					return "Left";
				}
				else if (LangStr == "ar")
				{
					return "Right";
				}
				return "Right";
			}
		}
		public static string LangSTR
		{
			get
			{
				object LangVal = HttpContext.Current.Request.Cookies["MyLanguage"];
				if (LangVal == null)
					return "";
				string LangStr = HttpContext.Current.Request.Cookies["MyLanguage"].Value.ToLower();
				if (LangStr == "fa-ir")
					return "";
				else if (LangStr == "en-us")
					return "en";
				else if (LangStr == "ru-ru")
					return "ru";
                else if (LangStr == "de-gr")
                    return "ru";
				else if (LangStr == "ar")
					return "ar";
				return "";
			}
		}
		public static string LangID
		{
			get
			{
				object LangVal = HttpContext.Current.Request.Cookies["MyLanguage"];
				if (LangVal == null)
					return "1";
				string LangStr = HttpContext.Current.Request.Cookies["MyLanguage"].Value.ToLower();
				if (LangStr == "fa-ir")
					return "1";
				else if (LangStr == "en-us")
					return "2";
				else if (LangStr == "ar")
					return "3";
				else if (LangStr == "de-gr")
					return "4";
				return "1";
			}
		}
        public static string GetLangID(string LangStr)
        {
            LangStr = LangStr.ToLower();
            if (LangStr == "fa-ir")
                return "1";
            else if (LangStr == "en-us")
                return "2";
            else if (LangStr == "ar")
                return "3";
            else if (LangStr == "de-gr")
                return "4";
            return "1";
        }
		public static void FillDL(DropDownList DR, string TableName, string ValueFild, string ViewFiled, string WhereComm, string SelectValue)
		{
			SqlDataReader MyRead = ADAL.A_ViewData.MyDR("SELECT " + ViewFiled + "," + ValueFild + " FROM " + TableName + " " + WhereComm);
			while (MyRead.Read())
			{
				ListItem list = new ListItem();
				list.Text = MyCL.MGStr(MyRead, 0);
				list.Value = MyCL.MGval(MyRead, 1).ToString();
				if (MyCL.MGval(MyRead, 1).ToString() == SelectValue)
					list.Selected = true;
				DR.Items.Add(list);
			}
			MyRead.Close();
		}
		public static string CheckViewRightMenu()
		{
			if (GetSetting(34) == "0" || GetSetting(34).ToLower() == "0px")
				return "none";
			else
				return "";
		}
		public static string GetAdminSettingText(int SettingTextNameID)
		{
			SqlConnection MyConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PortalConnectionStr"].ConnectionString);
			SqlCommand MyComm = new SqlCommand("", MyConnection);
			string UnitID = ADAL.A_CheckData.GetUnitID();
			if (UnitID == null || UnitID.Trim() == "")
				return "";

			MyComm.CommandText = "SELECT texts  FROM SettingText  WHERE (UnitID = @UnitID) and (SettingTextNameID=@SettingTextNameID) and langid=1";
			MyComm.Parameters.AddWithValue("SettingTextNameID", SettingTextNameID);
			MyComm.Parameters.AddWithValue("@UnitID", UnitID);
			MyConnection.Open();
			object OutText = MyComm.ExecuteScalar();
			MyConnection.Close();
			if (OutText == null)
				return "";
			return OutText.ToString();
		}
		public static string GetSettingText(int SettingTextNameID)
		{
			SqlConnection MyConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PortalConnectionStr"].ConnectionString);
			SqlCommand MyComm = new SqlCommand("", MyConnection);
			string UnitID = MyClass.GetViewUnitID;
			if (UnitID == null || UnitID.Trim() == "")
				return "";

			MyComm.CommandText = "SELECT texts  FROM SettingText  WHERE (UnitID = @UnitID) and (SettingTextNameID=@SettingTextNameID) and langid="+Tools.LangID;
			MyComm.Parameters.AddWithValue("SettingTextNameID", SettingTextNameID);
			MyComm.Parameters.AddWithValue("@UnitID", UnitID);
			MyConnection.Open();
			object OutText = MyComm.ExecuteScalar();
			MyConnection.Close();
			if (OutText == null)
				return "";
			return OutText.ToString();
		}
		public static string GetSetting(int SettingUnitNameID)
		{
			SqlConnection MyConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PortalConnectionStr"].ConnectionString);
			SqlCommand MyComm = new SqlCommand("", MyConnection);
			string UnitID = MyClass.GetViewUnitID;
			if (UnitID == null || UnitID.Trim() == "")
				return "";

			MyComm.CommandText = "SELECT value  FROM SettingUnit  WHERE (UnitID = @UnitID) and (SettingUnitNameID=@SettingUnitNameID) and (langid=@langid)";
			MyComm.Parameters.AddWithValue("@SettingUnitNameID", SettingUnitNameID);
			MyComm.Parameters.AddWithValue("@UnitID", UnitID);
			MyComm.Parameters.AddWithValue("@langid", Tools.LangID);
			MyConnection.Open();
			object OutText = MyComm.ExecuteScalar();
			MyConnection.Close();
			if (OutText == null)
				return "";

			return OutText.ToString();
		}
		public static string GetSetting(int SettingUnitNameID, string DefValue)
		{
			string Ret = GetSetting(SettingUnitNameID);
			if (Ret.Trim() != "")
				return Ret;
			return DefValue;
		}
		public static string GetAdminSetting(int SettingUnitNameID)
		{
			SqlConnection MyConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PortalConnectionStr"].ConnectionString);
			SqlCommand MyComm = new SqlCommand("", MyConnection);
			string UnitID = ADAL.A_CheckData.GetUnitID();
			if (UnitID == null || UnitID.Trim() == "")
				return "";

			MyComm.CommandText = "SELECT value FROM SettingUnit WHERE (UnitID = @UnitID) and (SettingUnitNameID=@SettingUnitNameID) and (langid=@langid)";
			MyComm.Parameters.AddWithValue("@SettingUnitNameID", SettingUnitNameID);
			MyComm.Parameters.AddWithValue("@UnitID", UnitID);
			MyComm.Parameters.AddWithValue("@langid", 1);
			MyConnection.Open();
			object OutText = MyComm.ExecuteScalar();
			MyConnection.Close();
			if (OutText == null)
				return "";
			return OutText.ToString();
		}
		public static string GetAdminSetting(int SettingUnitNameID, string DefValue)
		{
			string Ret = GetAdminSetting(SettingUnitNameID);
			if (Ret.Trim() != "")
				return Ret;
			return DefValue;
		}
		public static void GetPagging(PagedDataSource pgitems, Repeater MyrptPages, int CurrentPage,int RepLen = 10)
		{
			if (pgitems.PageCount > 1)
			{
				int ST = 0;
				if (CurrentPage - (RepLen/2) > 0)
					ST = CurrentPage - (RepLen / 2);
				int En = RepLen;
				if (pgitems.PageCount < RepLen)
					En = pgitems.PageCount;
				if (ST + En > pgitems.PageCount)
					En = pgitems.PageCount - ST;
				MyrptPages.Visible = true;
				ArrayList pages = new ArrayList();
				for (int i = ST; i < En + ST; i++)
					pages.Add((i + 1).ToString());
				MyrptPages.DataSource = pages;
				MyrptPages.DataBind();
			}
			else
				MyrptPages.Visible = false;
		}
		public static void SetPageHit(string Pname, string ReqID)
		{
			string UID = MyClass.GetViewUnitID;
			ReqID = ReqID.Replace("ID=", "");
			if (ReqID != "")
				ReqID = "-" + ReqID;
			if (string.IsNullOrEmpty(UID))
				return;
			Pname = Pname.ToLower().Replace("asp.", "").Replace("_aspx", ReqID + ".aspx");
			Pname = Pname.ToLower().Replace("admin_", "");//.Replace("_aspx",".aspx");
			if (Pname.Substring(0, 1) == "/")
				Pname = Pname.Substring(1);
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@PageName", Pname);
			SP.AddWithValue("@UID", UID);
			DAL.ExecuteData.InsertData("insert into hits(page,unitid) values(@PageName,@UID)", SP);
		}
		public static void CookieRemove(string CookieName)
		{
			HttpCookie httpCookie1 = new HttpCookie(CookieName, "null");
			httpCookie1.Expires = DateTime.Now.AddHours(3.0);
			httpCookie1.Domain = Tools.GetSetting(346, "");
			HttpContext.Current.Response.Cookies.Add(httpCookie1);
		}
		public static string CookieRead(string CookieName)
		{
			object hc = HttpContext.Current.Response.Cookies[CookieName].Value;
			if (hc == null || hc.ToString().Trim()=="")
			{
				try
				{
					hc = HttpContext.Current.Request.Cookies[CookieName].Value;
				}
				catch (Exception e)
				{
					Logging.ErrorLog("coocke", "Errorcoo", "UnitID" + Tools.GetViewUnitID + "-" + e.Message + "{$$$}" + e.Source + "{$$$}" + e.Data + "{$$$}" + e.InnerException + "{$$$}" + e.StackTrace + "{$$$}" + e.TargetSite + "{$$$}");

				}
			}
			//HttpContext.Current.Response.Cookies[CookieName].Value;
			if (hc == null)
				return "";
			return hc.ToString();
		}
		
		public static void CookieWrite(string CookieName,string Val,int ExpireDay=100)
		{
			HttpCookie myCookie = new HttpCookie(CookieName);
			myCookie.Value = Val;
			myCookie.Domain = Tools.GetSetting(346, "");
			//myCookie.Path = "";
			if (HttpContext.Current.Request.IsSecureConnection)
				myCookie.Secure = true;
			else
				myCookie.HttpOnly = true;
			//myCookie.Secure = true;
			myCookie.Expires = DateTime.Now.AddDays(ExpireDay);
			HttpContext.Current.Response.Cookies.Add(myCookie);
		}
		public static int ConvertToInt32(object InNumber,int Def=-1)
		{
			int OutNumber = Def;
			int.TryParse(InNumber.ToString(), out OutNumber);
			return OutNumber;
		}
		public static string ConvertToString(object InStr)
		{
			return  InStr.ToString();
		}
		public static long ConvertToInt64(object InNumber)
		{
			long OutNumber = -1;
			long.TryParse(InNumber.ToString(), out OutNumber);
			return OutNumber;
		}
		public static double ConvertToDouble(object InNumber)
		{
			double OutNumber = -1;
			double.TryParse(InNumber.ToString(), out OutNumber);
			return OutNumber;
		}
		public static decimal ConvertTodecimal(object InNumber)
		{
			decimal OutNumber = -1;
			decimal.TryParse(InNumber.ToString(), out OutNumber);
			return OutNumber;
		}
		public static long ConvertToInt64(object InNumber,long DefVal)
		{
			if (ConvertToInt64(InNumber) <= 0)
				return DefVal;
			return ConvertToInt64(InNumber);

		}
		public static string ConvertToBase64(object InputText)
		{
			byte[] encbuff = System.Text.Encoding.UTF32.GetBytes(InputText.ToString());
			return Convert.ToBase64String(encbuff);
		}
		public static string ConvertFromBase64(object InputText)
		{
			try
			{
				byte[] array = Convert.FromBase64String(InputText.ToString());
				return System.Text.Encoding.UTF32.GetString(array);
			}
			catch { return ""; }
		}
		public static ArrayList GetImageAddress(string htmlSource)
		{
			ArrayList MyList = new ArrayList();
			string regexImgSrc = @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";
			MatchCollection matchesImgSrc = Regex.Matches(htmlSource, regexImgSrc, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			foreach (Match m in matchesImgSrc)
			{
				string href = m.Groups[1].Value;
				MyList.Add(href);
			}
			return MyList;
		}
		public static string ConvertUnicodeToString(string inText)
		{
			inText = inText.Replace("\\u0622", "آ");
			inText = inText.Replace("\\u0627", "ا");
			inText = inText.Replace("\\u0628", "ب");
			inText = inText.Replace("\\u067e", "پ");
			inText = inText.Replace("\\u062a", "ت");
			inText = inText.Replace("\\u062b", "ث");
			inText = inText.Replace("\\u062c", "ج");
			inText = inText.Replace("\\u0686", "چ");
			inText = inText.Replace("\\u062d", "ح");
			inText = inText.Replace("\\u062e", "خ");
			inText = inText.Replace("\\u062f", "د");
			inText = inText.Replace("\\u0630", "ذ");
			inText = inText.Replace("\\u0631", "ر");
			inText = inText.Replace("\\u0632", "ز");
			inText = inText.Replace("\\u0698", "ژ");
			inText = inText.Replace("\\u0633", "س");
			inText = inText.Replace("\\u0634", "ش");
			inText = inText.Replace("\\u0635", "ص");
			inText = inText.Replace("\\u0636", "ض");
			inText = inText.Replace("\\u0637", "ط");
			inText = inText.Replace("\\u0638", "ظ");
			inText = inText.Replace("\\u0639", "ع");
			inText = inText.Replace("\\u063a", "غ");
			inText = inText.Replace("\\u0641", "ف");
			inText = inText.Replace("\\u0642", "ق");
			inText = inText.Replace("\\u06a9", "ک");
			inText = inText.Replace("\\u0643", "ک");
			
			inText = inText.Replace("\\u06af", "گ");
			inText = inText.Replace("\\u0644", "ل");
			inText = inText.Replace("\\u0645", "م");
			inText = inText.Replace("\\u0646", "ن");
			inText = inText.Replace("\\u0648", "و");
			inText = inText.Replace("\\u0647", "ه");
			inText = inText.Replace("\\u06cc", "ی");
			inText = inText.Replace("\\u064a", "ی");
			return inText;
		}
		public static void CreateCSSFiles(string Address)
		{
			if (!System.IO.Directory.Exists(Address))
				System.IO.Directory.CreateDirectory(Address.Substring(0, Address.LastIndexOf("\\")));
			string CSSStr = "";
			System.Data.SqlClient.SqlParameterCollection SP = new System.Data.SqlClient.SqlCommand().Parameters;
			SP.AddWithValue("@UnitID", MyClass.GetViewUnitID);
			SqlDataReader MyRead = ViewData.MyDR1("SELECT Template.Style FROM Template INNER JOIN UnitSetting ON Template.TemplateID = UnitSetting.TemplateID WHERE (UnitSetting.UnitID = @UnitID)", SP, true);
			if (MyRead.Read())
				CSSStr = MyCL.MGStr(MyRead, 0);

			MyRead.Close(); MyRead.Dispose();
			CSSStr = CSSStr.Replace("\r\n", " ").Replace("\t", " ");
			System.IO.StreamWriter CSSFile = new System.IO.StreamWriter(Address, false, System.Text.Encoding.UTF8);
			CSSFile.Write(CSSStr);
			CSSFile.Close();
		}
		public static void CreateAdminCSSFiles(int TemplateID, string Address)
		{
			string CSSStr = "";
			System.Data.SqlClient.SqlParameterCollection SP = new System.Data.SqlClient.SqlCommand().Parameters;
			SP.AddWithValue("@UnitID", ADAL.A_CheckData.GetUnitID());
			SqlDataReader MyRead = ViewData.MyDR1("SELECT Style FROM Template WHERE TemplateID=" + TemplateID + " and UnitID = @UnitID", SP, true);
			if (MyRead.Read())
				CSSStr = MyCL.MGStr(MyRead, 0);

			MyRead.Close(); MyRead.Dispose();
			CSSStr = CSSStr.Replace("\r\n", " ").Replace("\t", " ");
			System.IO.StreamWriter CSSFile = new System.IO.StreamWriter(Address, false, System.Text.Encoding.UTF8);
			CSSFile.Write(CSSStr);
			CSSFile.Close();
		}
		public static int ConvertBollToInt(bool p)
		{
			if (p)
				return 1;
			return 0;
		}
		public static string ReplaceWord(string inText)
		{
			return inText.Trim().Replace("ی", "ي").Replace("ک", "ك");
		}

		public static string SetSearchWord(string inWord)
		{
			inWord = inWord.Trim().Replace("ی", "ي").Replace("ک", "ك").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
            if (inWord.IndexOf("\"") == -1)
            {
                inWord = inWord.Replace("+ ", "+");
                inWord = inWord.Replace(" +", "+");
                inWord = inWord.Replace("+", "AND");
                inWord = inWord.Replace(" ", " OR ");
                inWord = inWord.Replace("AND", " AND ");
            }
            else
            {
                inWord = inWord.Replace("\"","").Replace("\"", "").Replace("\"", "");
                inWord = "\""+inWord + "\"";
            }
            return  inWord;
            /*MyClass.CleanTwoSpace(ref inWord);
			string[] strArray2 = inWord.Split(new char[] { ' ' });
			string str2 = "";
			for (int i = 0; i < strArray2.Length; i++)
			{
				str2 = str2 + strArray2[i] + " OR ";
			}
			return str2.TrimEnd(new char[] { ' ', 'O', 'R', ' ' });*/
		}
		public static UserControl LoadControl(System.Web.UI.Page MyPage, string UserControlPath, params object[] constructorParameters)
		{
			List<Type> constParamTypes = new List<Type>();
			foreach (object constParam in constructorParameters)
			{
				constParamTypes.Add(constParam.GetType());
			}
			UserControl ctl = MyPage.LoadControl(UserControlPath) as UserControl;
			// Find the relevant constructor
			ConstructorInfo constructor = ctl.GetType().BaseType.GetConstructor(constParamTypes.ToArray());

			//And then call the relevant constructor
			if (constructor == null)
			{
				throw new MemberAccessException("The requested constructor was not found on : " + ctl.GetType().BaseType.ToString());
			}
			else
			{
				constructor.Invoke(ctl, constructorParameters);
			}
			// Finally return the fully initialized UC
			return ctl;
		}
		public static string MyGetGlobalResourceObject(string MyResource, string MyName)
		{
			object fieldValue = HttpContext.GetGlobalResourceObject(MyResource, MyName);
			return fieldValue == null ? "" : fieldValue.ToString();
		}
		public static string GetLangName
		{
			get
			{
				if (HttpContext.Current.Request.Cookies["MyLanguage"].Value == null)
				{
					return "";
				}
				string LangStr = HttpContext.Current.Request.Cookies["MyLanguage"].Value.ToLower();
				if (LangStr == "fa-ir")
				{
					return "";
				}
				else if (LangStr == "en-us")
				{
					return "EN";
				}
				else if (LangStr == "de-gr")
				{
					return "de";
				}
				else if (LangStr == "ar")
				{
					return "";
				}
				return "";
			}
		}
		public static string GetBrowserCSSAddress(HttpBrowserCapabilities MyBrowser)
		{
			string Address = "";
			if (MyBrowser.Browser.ToLower() == "ie")
			{
				int brover;
				if (MyBrowser.Version.IndexOf(".") != -1)
					brover = ConvertToInt32(MyBrowser.Version.Substring(0, MyBrowser.Version.IndexOf(".")));
				else
					brover = ConvertToInt32(MyBrowser.Version);
				if (brover <= 6)
					Address = "IE6.css";
				else if (brover <= 8)
					Address = "IE8.css";
				else if (brover > 8)
					Address = "IE9.css";
			}
			else if (MyBrowser.Browser.ToLower() == "firefox")
				Address = "firefox.css";
			else if (MyBrowser.Browser.ToLower() == "chrome")
				Address = "chrome.css";
			else if (MyBrowser.IsMobileDevice)
				Address = "Mob.css";
			return Address;
		}
		public static void WriteStr(string Add, string Matn)
		{
			if (File.Exists(Add))
				File.Delete(Add);

			System.IO.StreamWriter CSSFile = new System.IO.StreamWriter(Add, false, System.Text.Encoding.UTF8);
			CSSFile.Write(Matn.Replace("\r\n", " ").Replace("\t", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("{ ", "{").Replace(" } ", "}").Replace("; ", ";").Replace(" {", "{").Replace(": ", ":").Replace("} ", "}"));
			CSSFile.Close();
		}
		public static string UtfToAscii(string value)
		{
			// byte[] utf8Bytes = Encoding.UTF8.GetBytes(value);
			// byte[] asciiBytes = Encoding.Convert(Encoding.UTF8,Encoding.ASCII, utf8Bytes);
			// return Encoding.ASCII.GetString(asciiBytes);
			ASCIIEncoding asciiEncoding = new ASCIIEncoding();
			byte[] bytes = asciiEncoding.GetBytes(value);
			string clean = "";//asciiEncoding.GetString(bytes);
			for (int i = 0; i < bytes.Length; i++)
			{
				clean += "&#" + bytes[i] + ";";
			}
			return clean;
		}
		public static string ASCIIToChar(string InText)
		{
			InText = InText.Replace("&#1576;", "ب");
			InText = InText.Replace("&#1585;", "ر");
			InText = InText.Replace("&#1587;", "س");
			InText = InText.Replace("&#1740;", "ي");
			InText = InText.Replace("&#1578;", "ت");
			InText = InText.Replace("&#1582;", "خ");
			InText = InText.Replace("&#1589;", "ص");
			InText = InText.Replace("&#1605;", "م");
			InText = InText.Replace("&#1593;", "ع");
			InText = InText.Replace("&#1601;", "ف");
			InText = InText.Replace("&#1570;", "آ");
			InText = InText.Replace("&#1575;", "ا");
			InText = InText.Replace("&#1583;", "د");
			InText = InText.Replace("&#1586;", "ز");
			InText = InText.Replace("&#1587;", "س");
			InText = InText.Replace("&#1705;", "ک");
			InText = InText.Replace("&#1606;", "ن");
			InText = InText.Replace("&#1608;", "و");
			InText = InText.Replace("&#1607;", "ه");
			InText = InText.Replace("&#1604;", "ل");
			InText = InText.Replace("&#1662;", "پ");
			InText = InText.Replace("&#1580;", "ج");
			InText = InText.Replace("&#1588;", "ش");
			InText = InText.Replace("&#1592;", "ظ");
			InText = InText.Replace("&#1711;", "گ");
			InText = InText.Replace("&#1581;", "ح");
			InText = InText.Replace("&#1590;", "ض");
			InText = InText.Replace("&#1591;", "ط");
			InText = InText.Replace("&#1579;", "ث");
			InText = InText.Replace("&#1670;", "چ");
			InText = InText.Replace("&#1584;", "ذ");
			InText = InText.Replace("&#1688;", "ژ");
			InText = InText.Replace("&#1602;", "ق");
			InText = InText.Replace("&#1594;", "غ");
			InText = InText.Replace("&nbsp;", " ");
			InText = InText.Replace("&#1574;", "ئ");
			InText = InText.Replace("&#1548;", "،");
			InText = InText.Replace("&#1610;", "ي");
			InText = InText.Replace("&#1603;", "ک");
			return InText;
		}
		public static string CharToASCII(string InText)
		{
			InText = InText.Replace("ب","&#1576;");
			InText = InText.Replace("ر","&#1585;");
			InText = InText.Replace("س","&#1587;");
			InText = InText.Replace("ي","&#1740;");
			InText = InText.Replace("ت","&#1578;");
			InText = InText.Replace("خ","&#1582;");
			InText = InText.Replace("ص","&#1589;");
			InText = InText.Replace("م","&#1605;");
			InText = InText.Replace("ع","&#1593;");
			InText = InText.Replace("ف","&#1601;");
			InText = InText.Replace("آ","&#1570;");
			InText = InText.Replace("ا","&#1575;");
			InText = InText.Replace("د","&#1583;");
			InText = InText.Replace("ز","&#1586;");
			InText = InText.Replace("س","&#1587;");
			InText = InText.Replace("ک","&#1705;");
			InText = InText.Replace("ن","&#1606;");
			InText = InText.Replace("و","&#1608;");
			InText = InText.Replace("ه","&#1607;");
			InText = InText.Replace("ل","&#1604;");
			InText = InText.Replace("پ","&#1662;");
			InText = InText.Replace("ج","&#1580;");
			InText = InText.Replace("ش","&#1588;");
			InText = InText.Replace("ظ","&#1592;");
			InText = InText.Replace("گ","&#1711;");
			InText = InText.Replace("ح","&#1581;");
			InText = InText.Replace("ض","&#1590;");
			InText = InText.Replace("ط","&#1591;");
			InText = InText.Replace("ث","&#1579;");
			InText = InText.Replace("چ","&#1670;");
			InText = InText.Replace("ذ","&#1584;");
			InText = InText.Replace("ژ","&#1688;");
			InText = InText.Replace("ق","&#1602;");
			InText = InText.Replace("غ","&#1594;");
			InText = InText.Replace(" ","&nbsp;");
			InText = InText.Replace("ئ","&#1574;");
			InText = InText.Replace("،","&#1548;");
			InText = InText.Replace("ي","&#1610;");
			InText = InText.Replace("ک","&#1603;");
			return InText;
		}
		public static void GreateCSSView()
		{
			string CurTemplateID = Tools.GetSetting(357, "-1");

			SqlDataReader MyRead = ADAL.A_ViewData.MyDR("SELECT Style, unitid, StyleIE9, StyleIE8, StyleIE6, StyleFF, StyleCh, StyleMob FROM Template WHERE (TemplateID = " + CurTemplateID + ")");
			if (MyRead.Read())
			{
				WriteStr(DAL.CheckData.GetFilesRoot(true) + "\\" + Tools.LangSTR + "Site.css", MyCL.MGStr(MyRead, 0));
				WriteStr(DAL.CheckData.GetFilesRoot(true) + "\\" + Tools.LangSTR + "ie9.css", MyCL.MGStr(MyRead, 2));
				WriteStr(DAL.CheckData.GetFilesRoot(true) + "\\" + Tools.LangSTR + "ie8.css", MyCL.MGStr(MyRead, 3));
				WriteStr(DAL.CheckData.GetFilesRoot(true) + "\\" + Tools.LangSTR + "ie6.css", MyCL.MGStr(MyRead, 4));
				WriteStr(DAL.CheckData.GetFilesRoot(true) + "\\" + Tools.LangSTR + "chrome.css", MyCL.MGStr(MyRead, 6));
				WriteStr(DAL.CheckData.GetFilesRoot(true) + "\\" + Tools.LangSTR + "firefox.css", MyCL.MGStr(MyRead, 5));
				WriteStr(DAL.CheckData.GetFilesRoot(true) + "\\" + Tools.LangSTR + "Mob.css", MyCL.MGStr(MyRead, 7));
			}
			MyRead.Close(); MyRead.Dispose();
		}

		public static string GetLangStr(string LangID)
		{
			if (LangID == "1")
				return "";
			else if (LangID == "2")
				return "en";
			else if (LangID == "3")
				return "ar";
			return "";
		}

		public static bool CheckIPBlook(string IP_Address)
		{
			if (MyClass.GetAppSetting_WebConfig("BlockIP") != "1")
				return true;

			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@IP", IP_Address);
			int CNT = DAL.ExecuteData.CNTData("SELECT COUNT(*) AS Expr1  FROM ErrorLogs WHERE (IPAddress = @IP) AND (Date > DATEADD(minute, - 10, GETDATE()))", SP);
			if (CNT > 30)
				return false;
			return true;
		}
		public static bool CheckFileExtention(string FileType)
		{
			string[] FileAllowAccess = { ".jpg", ".bmp", ".gif", ".png", ".wav", ".wmv", ".3gp", ".mp3", ".bmp", ".pdf", ".zip", ".rar", ".doc", ".docx", ".swf", ".css", ".ppt", ".pptx", ".pps", ".ppsx",".mov",".avi",".flv",".mpeg",".xls",".xlsx",".mdb",".mdbx" };
			FileType = FileType.ToLower();
			for (int i = 0; i < FileAllowAccess.Length; i++)
			{
				if (FileType.IndexOf(FileAllowAccess[i]) != -1)
					return true;
			}
			return false;
		}
		public static string GetUserIPAddress()
		{
			//Request.ServerVariables["REMOTE_ADDR"].ToString()
			string ServerVar = "REMOTE_ADDR";
			if (MyClass.GetAppSetting_WebConfig("ServerVarGetIP") != "")
			{
				ServerVar = MyClass.GetAppSetting_WebConfig("ServerVarGetIP");// "HTTP_X_FORWARDED_FOR";
				object SV =HttpContext.Current.Request.ServerVariables[ServerVar];
				if (SV != null && SV.ToString() != "")
					return SV.ToString();
			}
			return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
		}
		public static string MoneySplitter(string InText)
		{
			int number =ConvertToInt32(InText);
			if (number <=0)
				return InText;
			string OutText = number.ToString("#,##0");
			return OutText;
		}
		public static string MoneySplitter(int number)
		{
			if (number <= 0)
				return number.ToString();
			string OutText = number.ToString("#,##0");
			return OutText;
		}
		public static UserControl LoadControl(Page MyPage,string UserControlPath, string MyID,string MyType)
		{
			List<Type> constParamTypes = new List<Type>();
			//foreach (object constParam in constructorParameters)
			//{
			//	constParamTypes.Add(constParam.GetType());
			//}
			constParamTypes.Add(MyID.GetType());
			constParamTypes.Add(MyType.GetType());
			UserControl ctl = MyPage.LoadControl(UserControlPath) as UserControl;

			// Find the relevant constructor
			ConstructorInfo constructor = ctl.GetType().BaseType.GetConstructor(constParamTypes.ToArray());

			//And then call the relevant constructor
			if (constructor == null)
			{
				// throw new MemberAccessException("The requested constructor was not found on : " + ctl.GetType().BaseType.ToString());
			}
			else
			{
				constructor.Invoke(ctl, new Object[] { MyID, MyType});
			}

			// Finally return the fully initialized UC
			return ctl;
		}


		public static string[] ConvertArrayList(ArrayList MobNo)
		{
			string[] myList = new string[MobNo.Count];
			for (int i = 0; i <MobNo.Count ; i++)
			{
				myList[i] = MobNo[i].ToString(); 
			}
			return myList;
		}

		public static string GetMobileNo(string InMob)
		{
			if(InMob.Trim().Length==10)
				return "0" + InMob;
			InMob = InMob.TrimStart('+').TrimStart('9').TrimStart('8').TrimStart('0');
			return "0"+InMob;
		}

        public static string GetRequestStr(string ReqID)
        {
            if (HttpContext.Current.Request.QueryString[ReqID] != null && HttpContext.Current.Request.QueryString[ReqID].Trim() != "")
            {
                string CurSTR = HttpContext.Current.Request.QueryString[ReqID];
                return CurSTR.Substring(CurSTR.LastIndexOf("/") + 1);
                
            }
            return "";
        }
		public static DateTime ConvertUnixToDatetime(double unixTimeStamp)
		{
			System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dtDateTime;
		}

		public static bool CompareDateTime(DateTime MyD, int LoginTime)
		{
			TimeSpan TS = MyD - DateTime.Now;
			int comp = TS.Minutes;
			if (comp > LoginTime)
				return false;
			if (comp < -LoginTime)
				return false;
			return true;
		}

		public static string GetFileExtention(string FileName)
		{
			if (FileName.LastIndexOf(".") == -1)
				return "";
			return FileName.Substring(FileName.LastIndexOf("."));
		}
		public static string SizeSuffix(Int64 value)
		{
			string[] suffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
			int i = 0;
			decimal dValue = (decimal)value;
			while (Math.Round(dValue / 1024) >= 1)
			{
				dValue /= 1024;
				i++;
			}
			return string.Format("{0:n1} {1}", dValue, suffixes[i]);
		}
		public static double ConvertBytesToMegabytes(long bytes)
		{
			return (bytes / 1024f) / 1024f;
		}
		public static double ConvertBytesToKilobytes(long bytes)
		{
			return (bytes / 1024f);
		}
		public static long GetDirectorySize(string Address)
		{
			if(Directory.Exists(Address))
				return new DirectoryInfo(Address).GetFiles("*.*", SearchOption.AllDirectories).Sum(file => file.Length);
			return 0;
		}
        public static string ReplacetEmotion(string InText) {
            //string[] EmotionCom = { "X(", ":-SS", ":-O", "~^o^~", "~o)", "B-)", ":((", ":((", "^#(^", "=P~", "^O^||3", "(:|", ":D", ":o)", ":-&", "#-o", "L-)", ":x", ":-*", ";;)", "o:-)", ":X", "@};-", ">:)", "X-(", "X_X", ":-j", "[-O<", "#:-S", "*-:)", "X_X", ":X", ":D", "8-}", ":-t", ":-&", "b-(", ">:D<", "X_X", ";)" };
            string[] EmotionCom = { ":agressive:", ":biteMyself:", ":cantBelieveIt:", ":cheer:", ":contrite:", ":cool:", ":cryMouth:", ":cry:", ":dontKnow:", ":drool:", ":eat:", ":gape:", ":glad:", ":grimace:", ":grumpy:", ":headache:", ":heyComeOn:", ":inLove:", ":kiss:", ":look:", ":loveGlad:", ":heart:", ":rose:", ":monster:", ":nerved:", ":ohNo:", ":pfffrt:", ":pleaseNo:", ":powerless:", ":psssst:", ":sacred:", ":sheepish:", ":smile:", ":stupid:", ":tooMuch:", ":uuups:", ":weep:", ":welcome:", ":whyMe:", ":wink:" };
		    string[] EmotionURL = { "agressive", "biteMyself", "cantBelieveIt", "cheer", "contrite", "cool", "cryMouth", "cry", "dontKnow", "drool", "eat", "gape", "glad", "grimace", "grumpy", "headache", "heyComeOn", "inLove", "kiss", "look", "loveGlad", "heart", "rose", "monster", "nerved", "ohNo", "pfffrt", "pleaseNo", "powerless", "psssst", "sacred", "sheepish", "smile", "stupid", "tooMuch", "uuups", "weep", "welcome", "whyMe", "wink" };
			for (int i = 0; i < EmotionCom.Length; i++)
			{
				InText = InText.Replace(EmotionCom[i], "<img src=\"/Images/Emotions/" + EmotionURL[i] + ".png\" class=\"EmotionIMG\" >");
			}
			return InText;
        }
		public static void RestartService(string serviceName)
		{
			var psi = new ProcessStartInfo("net.exe", "stop " + serviceName);
			psi.WindowStyle = ProcessWindowStyle.Hidden;
			psi.UseShellExecute = true;
			psi.WorkingDirectory = Environment.SystemDirectory;
			var st = Process.Start(psi);
			st.WaitForExit();

			psi = new ProcessStartInfo("net.exe", "start " + serviceName);
			psi.UseShellExecute = true;
			psi.WindowStyle = ProcessWindowStyle.Hidden;
			psi.WorkingDirectory = Environment.SystemDirectory;
			st = Process.Start(psi);
			st.WaitForExit();
		}
		public static void SetDropDownListValue(DropDownList MyDL, object SetVal,bool Address=false)
		{
			try
			{
				if(Address)
				{
					if (SetVal.ToString().IndexOf("/") != -1)
						SetVal = SetVal.ToString().Substring(SetVal.ToString().LastIndexOf("/") + 1);
					MyDL.SelectedIndex = MyDL.Items.IndexOf(MyDL.Items.FindByValue(SetVal.ToString()));
				}
				MyDL.SelectedIndex = MyDL.Items.IndexOf(MyDL.Items.FindByValue(SetVal.ToString()));
				//MyDL.Items.FindByValue(SetVal).Selected = true;
				//MyDL.SelectedIndex
				//MyDL.SelectedValue = SetVal;
			}
			catch
			{
			}
		}
		public static string GetLevelTab(string MyLevel, string RetVal = "&nbsp;&nbsp;&nbsp;")
		{
			string OT = "";
			for (int i = 0; i < MyLevel.Trim().Length - 2; i++)
				OT += RetVal;
			return OT;
		}
		public static string GetParentLevelTab(string MyLevel)
		{
			string OT = "";
			if (MyLevel != "0")
				OT += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
			return OT;
		}
		public static string GetSalesState(string MyState)
		{
			if (MyState == "0")
				return "پرداخت نشده";
			else if (MyState == "1")
				return "پرداخت شده اقدام نشده";
			else if (MyState == "2")
				return "اتمام عمليات";
			else
				return "نامشخص";
		}
		public static string GlobalGetViewUnitID
		{
			get
			{
				try
				{
					if (null == System.Web.HttpContext.Current.Session["SiteID"])
					{
						string SiteAddress = HttpContext.Current.Request.Url.Host.ToLower();
						System.Data.SqlClient.SqlParameterCollection SiteP = new System.Data.SqlClient.SqlCommand().Parameters;
						SiteP.AddWithValue("@Addre", SiteAddress);
						string siteUID = "0";
						SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT UnitID,redirect  FROM Site  WHERE (Address = @Addre)", SiteP, true);
						if (MyRead.Read())
						{
							string Redi = MyCL.MGStr(MyRead, 1);
							if (Redi != "")
							{
								MyRead.Close();
								System.Web.HttpContext.Current.Response.Redirect(Redi, true);

							}
							siteUID = MyCL.MGInt(MyRead, 0).ToString();
						}
						else
							siteUID = "59";
						MyRead.Close();
						System.Web.HttpContext.Current.Session["SiteID"] = siteUID;
						return siteUID;
					}
					return System.Web.HttpContext.Current.Session["SiteID"].ToString();
				}
				catch
				{ return ""; }
			}
		}
		public static string GetViewUnitID
		{
			get
			{
				try
				{
					if (System.Web.HttpContext.Current.Session["SiteID"] == null)
					{
						string SiteAddress = HttpContext.Current.Request.Url.Host.ToLower();
						System.Data.SqlClient.SqlParameterCollection SiteP = new System.Data.SqlClient.SqlCommand().Parameters;
						SiteP.AddWithValue("@Addre", SiteAddress);
						string siteUID = "0";
						SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT UnitID,redirect  FROM Site  WHERE (Address = @Addre)", SiteP, true);
						if (MyRead.Read())
						{
							string Redi = MyCL.MGStr(MyRead, 1);
							if (Redi != "")
							{
								MyRead.Close();
								System.Web.HttpContext.Current.Response.Redirect(Redi, true);

							}
							siteUID = MyCL.MGInt(MyRead, 0).ToString();
						}
						else
							siteUID = "44";
						MyRead.Close();
						System.Web.HttpContext.Current.Session["SiteID"] = siteUID;
						return siteUID;
					}
					return System.Web.HttpContext.Current.Session["SiteID"].ToString();
				}
				catch
				{
					return "44";
				}
			}
		}
		public static void CleanTwoSpace(ref string str)
		{
			while (str.Contains("  "))
			{
				str = str.Replace("  ", " ");
			}
		}
		public static string GetSiteStr
		{
			get
			{
				if (System.Web.HttpContext.Current.Session["GetSiteStr"] == null)
				{
					string SiteAddress = HttpContext.Current.Request.Url.Host.ToLower();
					System.Web.HttpContext.Current.Session["GetSiteStr"] = SiteAddress;
					return SiteAddress;
				}
				return System.Web.HttpContext.Current.Session["GetSiteStr"].ToString();
			}
		}
		public static string GetAppSetting_WebConfig(string keyName)
		{
			object webcon = ConfigurationManager.AppSettings[keyName];
			if (webcon == null || webcon.ToString().Trim() == "")
				return "";
			return webcon.ToString().Trim();
		}
		public static bool ValidateURL(string URL)
		{
			string string1 = "http(s)?://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&:]*)?";
			if (Regex.Match(URL, string1).Success)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public static bool ValidateEmail(string EmailAddress)
		{
			if (EmailAddress == "")
				return true;
			string string1 = "\\w+([-+.\']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
			if (Regex.Match(EmailAddress, string1).Success)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public static string SetExplain(string STR, int Count = 50, string EndCharacter = " ...")
		{
			for (int i = 0; i < STR.Length; i++)
			{
				if ((i >= Count) && (STR[i] == ' '))
				{
					return (STR.Substring(0, i) + EndCharacter);
				}
			}
			return STR;
		}
		public static string SetExplainFix(string STR, int Count = 50, string EndCharacter = " ...")
		{
			for (int i = 0; i < STR.Length; i++)
			{
				if (i >= Count)
				{
					return (STR.Substring(0, i) + EndCharacter);
				}
			}
			return STR;
		}
		public static string SetExplainNoSpace(string STR, int Count = 50, string EndCharacter = " ...")
		{
			for (int i = 0; i < STR.Length; i++)
			{
				if ((i >= Count))
				{
					return (STR.Substring(0, i) + EndCharacter);
				}
			}
			return STR;
		}
		public static string SetItemTitle(Page MYPage, string Matn)
		{
			SetTitle(MYPage, Matn, true);
			MyClass.SetMetaTag(MYPage, MyClass.MetaTags.Keywords, Matn.Replace(" ", ","));
			MyClass.SetMetaTag(MYPage, MyClass.MetaTags.Description, Matn);
			return Matn;
		}
		public static string SetItemTitle(Page MYPage, object Matn)
		{
			SetTitle(MYPage, Matn.ToString(), true);
			MyClass.SetMetaTag(MYPage, MyClass.MetaTags.Keywords, Matn.ToString().Replace(" ", ","));
			MyClass.SetMetaTag(MYPage, MyClass.MetaTags.Description, Matn.ToString());
			return Matn.ToString();

		}
		public static void Alert(Page page, string MSG, string Key = "", bool AddScriptTag = true, string Func = "jAlert")
		{
			if (page == null)
				return;
			if (MSG.Trim() != "")
				page.ClientScript.RegisterClientScriptBlock(page.GetType(), Key, string.Format(Func+"(\"{0}\",\"Imen CMS\");", MSG.Replace("'", " ").Replace("\\", "").Replace("\\r\\n", " ").Replace("\\n", " ")), AddScriptTag);
		}
		public static void SetMetaTag(Page ASPXPage, MetaTags Meta, string Content)
		{
			HtmlMeta child = new HtmlMeta();
			child.Name = Meta.ToString();
			child.Content = Content;
			ASPXPage.Header.Controls.Add(child);
		}
		public static void SetTitle(Page ASPXPage, string Title, bool MasterPage)
		{
			if (MasterPage)
			{
				ASPXPage.Master.Page.Title = Title;
			}
			else
			{
				ASPXPage.Title = Title;
			}
		}
		public static void SetSessen(string name, string Val)
		{
			HttpCookie httpCookie1 = new HttpCookie(name, MyEncry(Val));
			if (Val == null || Val == "")
				httpCookie1.Expires = DateTime.Now.AddMinutes(-105);
			else
				httpCookie1.Expires = DateTime.Now.AddMinutes(5);
			HttpContext.Current.Response.Cookies.Add(httpCookie1);
		}
		public static string GetSessen(string name)
		{
			if (HttpContext.Current.Request.Cookies[name] != null)
				return MyDecry(HttpContext.Current.Request.Cookies[name].Value);
			return "";
		}
		public enum MetaTags
		{
			Description = 1,
			Keywords = 2
		}
		//public static string GetCoocke(string Name)
		//{

		//}
		public static string SetRate(int Rate)
		{
			object object1;
			object[] objectArray1;
			object object2;
			object[] objectArray2;
			string string1 = "";
			for (int i1 = 1; (i1 <= 5); i1++)
			{
				if (i1 > Rate)
				{
					object2 = string1;
					objectArray2 = new object[] { object2, "<img onmouseover=\"RateMouseOver(" + i1 + ")\" ID='MyRate" + i1 + "' src=\'/images/th1_start_n.gif\' onclick=\'SetRate(", i1, ")\'>" };
					string1 = string.Concat(objectArray2);
				}
				else
				{
					object1 = string1;
					objectArray1 = new object[] { object1, "<img onmouseover=\"RateMouseOver(" + i1 + ")\" ID='MyRate" + i1 + "' src=\'/images/th1_start.gif\' onclick=\'SetRate(", i1, ")\'>" };
					string1 = string.Concat(objectArray1);
				}
			}
			return "<span onmouseout=\"RateMouseOut(" + Rate + ")\">" + string1 + "</span>";
		}
		public static string RepWord(string InText)
		{
			return RepNumber(InText.Replace("ی", "ي").Replace("ک", "ك"));
		}
		public static string RepNumberToPersian(string InputText)
		{
			return InputText.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴").Replace("5", "۵").Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹");
		}
		public static string RepNumber(string InputText)
		{
			return InputText.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9");
		}
		public static bool GetKadrBorder(string PageName, string MyType, ref int TemplateID)
		{
			PageName = PageName.ToLower();//PageName.ToLower().Replace("asp.", "").Replace("_ascx", ".ascx");
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@PageName", PageName);
			SP.AddWithValue("@MyType", MyType);
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT CadrType FROM DefualtItemKadr  WHERE (PageName = @PageName) AND (DefaultItemType =@MyType) and unitid=" + MyClass.GetViewUnitID, SP);
			if (MyRead.Read())
			{
				if (MyCL.MGInt(MyRead, 0) == 0)
				{
					MyRead.Close(); MyRead.Dispose();
					TemplateID = 0;
					//Logging.ErrorLog("amir", "InfoReif", "falseeee" + "_" + "PN:" + PageName + "  MyType=" + MyType + " temp:" + TemplateID);

					return false;
				}
				else
				{
					TemplateID = MyCL.MGInt(MyRead, 0);
					MyRead.Close(); MyRead.Dispose();
					//Logging.ErrorLog("amir", "InfoReElse", "falseeee" + "_" + "PN:" + PageName + "  MyType=" + MyType + " temp:" + TemplateID);

					return true;
				}
			}
			else
			{
				//Logging.ErrorLog("amir", "InfonotReElse", "falseeee" + "_" + "PN:" + PageName + "  MyType=" + MyType + " temp:" + TemplateID);
				MyRead.Close(); MyRead.Dispose();
				TemplateID = 0;
				return true;
			}
		}
		public static void GetListItem(string MyPath, string UnitID, DropDownList MyDL)
		{
			MyDL.Items.Clear();

			//DropDownList MyDL = new DropDownList();
			if (!Directory.Exists(FileSystemManager() + @"\" + UnitID + @"\" + MyPath))
				return;

			FileInfo[] files = new DirectoryInfo(FileSystemManager() + @"\" + UnitID + @"\" + MyPath).GetFiles();
			//ListItem[] MyLi = new ListItem[files.Length];
			MyDL.Items.Add(new ListItem("بدون آدرس", ""));
			for (int i = 0; i < files.Length; i++)
			{
				//str += "<li class=\"MyLI\"><img height=140 width=180 src=\"Files/" + Global.UnitID + "/RandImages/" + files[i] + "\" /></li>";
				//Str += "variableslide[" + i.ToString() + "] = ['Files/" + Global.UnitID + "/RandImages/" + files[i] + "', '', '']; ";

				//MyLi[i].Text = files[i].Name;
				//MyLi[i].Value = files[i].Name;
				MyDL.Items.Add(new ListItem(files[i].Name, files[i].Name));
			}
			// return MyDL;
		}
		public static string FileSystemManager()
		{
			string strRootFolder = HttpContext.Current.Server.MapPath("/Files/");
			strRootFolder = strRootFolder.Substring(0, strRootFolder.LastIndexOf(@"\")) + @"\";
			return strRootFolder + "\\";
		}
		public static void AddRSSItem(XmlTextWriter writer, string ItemTitle, string ItemLink, string ItemDescription, string ItemAuthor, string ItemPublishDate)
		{
			writer.WriteStartElement("item");
			writer.WriteElementString("title", ItemTitle);
			writer.WriteElementString("link", ItemLink);
			writer.WriteElementString("description", ItemDescription);
			writer.WriteElementString("author", ItemAuthor);
			writer.WriteElementString("publishdate", ItemPublishDate);
			writer.WriteEndElement();
		}
		public static void WriteRSSPrologue(XmlTextWriter writer, string Commnet, string Title, string Link)
		{
			writer.WriteStartDocument();
			writer.WriteComment(Commnet);
			writer.WriteStartElement("rss");
			writer.WriteAttributeString("version", "2.0");
			writer.WriteStartElement("channel");
			writer.WriteElementString("title", Title);
			writer.WriteElementString("link", Link);
		}
		public static void WriteRSSClosing(XmlTextWriter writer)
		{
			writer.WriteEndElement();
			writer.WriteEndElement();
			writer.WriteEndDocument();
			writer.Flush();
			writer.Close();
		}
		public static string TagReplace(string InText, int MyType)
		{

			string OutText = InText;
			OutText = OutText.Replace("&lt;", "<");
			OutText = OutText.Replace("&gt;", ">");
			string[] TnTag = { "SiteTitle", "SiteSubTitle", "SiteBody", "SiteFooter", "CadrTitle", "CadrBody", "CadrFooter", "MoreLink" };
			if (MyType == 1)
			{
				for (int i = 0; i < TnTag.Length; i++)
					OutText = OutText.Replace("<%=" + TnTag[i] + "%>", "<*" + TnTag[i] + "*>");
			}
			else if (MyType == 2)
			{
				for (int i = 0; i < TnTag.Length; i++)
					OutText = OutText.Replace("<*" + TnTag[i] + "*>", "<%=" + TnTag[i] + "%>");
			}
			return OutText;
		}
		public static string CryptSTR = "amir";
		public static string MyEncry(string InText)
		{
			return Encrypt(InText, CryptSTR);
		}
		public static string MyDecry(string InText)
		{
			if (InText == "" || InText == "null")
				return "";
			try
			{
				return Decrypt(InText, CryptSTR);
			}
			catch
			{
				return "";
			}
		}
		public static string MyEncry(string InText, string Key)
		{
			return Encrypt(InText, Key);
		}
		public static string MyDecry(string InText, string Key)
		{
			if (InText == "" || InText == "null")
				return "";
			try
			{
				return Decrypt(InText, Key);
			}
			catch
			{
				return "";
			}
		}
		public static string GetSiteURL()
		{
			string Add = ADAL.A_ExecuteData.CNTData("SELECT TOP (1) Redirect  FROM Site  WHERE (UnitID = " + ADAL.A_CheckData.GetUnitID() + ") ORDER BY Redirect desc");
			if (Add.ToLower().IndexOf("http://") == -1 && Add.ToLower().IndexOf("https://") == -1)
				Add = "http://" + Add;
			return Add;
		}
		public static string GetViewSiteURL()
		{
			string Add = DAL.ExecuteData.CNTDataStr("SELECT TOP (1) Address  FROM Site  WHERE (UnitID = " + GetViewUnitID + ") ORDER BY Redirect");
			if (Add.ToLower().IndexOf("http://") == -1)
				Add = "http://" + Add;
			return Add;
		}
		private static string Encrypt(string str, string key)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(str);
			byte[] buffer2 = MakeMD5(key);
			byte[] rgbKey = Key24bit(buffer2);
			byte[] rgbIV = Key8bit(buffer2);
			TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
			MemoryStream stream = new MemoryStream();
			CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
			stream2.Write(bytes, 0, bytes.Length);
			stream2.Close();
			return Convert.ToBase64String(stream.ToArray());
		}
		private static string Decrypt(string cipherSTR, string key)
		{
			byte[] buffer = Convert.FromBase64String(cipherSTR);
			byte[] buffer2 = MakeMD5(key);
			byte[] rgbKey = Key24bit(buffer2);
			byte[] rgbIV = Key8bit(buffer2);
			byte[] bytes = null;
			TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
			MemoryStream stream = new MemoryStream();
			CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
			stream2.Write(buffer, 0, buffer.Length);
			stream2.Close();
			bytes = stream.ToArray();
			return Encoding.ASCII.GetString(bytes);
		}
		private static byte[] Key24bit(byte[] md5)
		{
			byte[] buffer = new byte[0x18];
			for (int i = 0; i < 0x18; i++)
			{
				if (i < 0x10)
				{
					buffer[i] = md5[i];
				}
				else
				{
					buffer[i] = md5[i - 0x10];
				}
			}
			return buffer;
		}
		private static byte[] Key8bit(byte[] md5)
		{
			byte[] buffer = new byte[8];
			for (int i = 0; i < 8; i++)
			{
				buffer[i] = md5[i];
			}
			return buffer;
		}
		private static byte[] MakeMD5(string variable)
		{
			MD5 md = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.ASCII.GetBytes(variable);
			byte[] inArray = md.ComputeHash(bytes);
			Convert.ToBase64String(inArray);
			return inArray;
		}
		public static string CheckIStEmpty(string Title, string RetVal)
		{
			if (Title.Trim()=="")
				return RetVal;
			return Title;
		}

		public static string CheckNotEmpty(string Title, string Val, string Seperator = "", bool EndBR = false,string EndSTR="")
		{
			if (Val == "" || Val == "0" || Val==null)
				return "";
			if (Seperator == "")
				Seperator = " : ";
            Val = Val + EndSTR;
            if (EndBR)
				Val = Val + "<br>";
			return Title + Seperator + Val;
		}
		public static string GetEndSortTable(string TableName, string FildName = "Sort", string LevCheck = "", string FieldCheck = "", bool UnitIDCheck = true)
		{
			if (string.IsNullOrEmpty(FildName))
				FildName = "Sort";
			string UIDCheck = "";
			if (UnitIDCheck)
			{
				object UID = ADAL.A_CheckData.GetUnitID();
				if (UID == null || UID.ToString() == "")
					UID = GetViewUnitID;
				UIDCheck = " unitid=" + UID + " ";
			}
			string LevComm = "";
			if (LevCheck != "")
				LevComm = " AND (LEN([Level]) = " + LevCheck.Length + ") ";
			//LevComm = " AND ([Level] LIKE '" + LevCheck.Substring(0, (LevCheck.Length - 2)) + "%') AND (LEN([Level]) = " + LevCheck.Length + ") ";
			object OutCMD = ADAL.A_ExecuteData.CNTData("select top 1 " + FildName + " from " + TableName + " where " + UIDCheck + " " + FieldCheck + " " + LevComm + " order by " + FildName + " desc ");
			if (OutCMD == null)
				return "1";
			return (Tools.ConvertToInt32(OutCMD) + 1).ToString();
		}
		public static void SortFiledview(MySortType MST, string ComArg, string TableName, string ID_filed, string OrderField = "Sort", string LevCheck = "", string FiledCheck = "")
		{
			string CurSortNo = ComArg.Substring(0, ComArg.IndexOf("#"));
			string CurID = ComArg.Substring(ComArg.IndexOf("#") + 1);
			if (CurSortNo == "0" && MST == MySortType.UP)
				return;

			if (string.IsNullOrEmpty(OrderField))
				OrderField = "Sort";
			string LevComm = "";

			object UID = ADAL.A_CheckData.GetUnitID();
			if (UID == null || UID.ToString() == "")
				UID = GetViewUnitID;


			if (LevCheck != "")
				LevComm = " AND ([Level] LIKE '" + LevCheck.Substring(0, (LevCheck.Length - 2)) + "%') AND (LEN([Level]) = " + LevCheck.Length + ") ";

			string OtherID = "";
			if (MST == MySortType.UP)
				OtherID = DAL.ExecuteData.CNTDataStr("SELECT TOP (1) " + ID_filed + "  FROM " + TableName + "  WHERE (" + OrderField + " > " + CurSortNo + ") and unitid=" + UID + " " + FiledCheck + " " + LevComm + " ORDER BY " + OrderField + "");
			else
				OtherID = DAL.ExecuteData.CNTDataStr("SELECT TOP (1) " + ID_filed + "  FROM " + TableName + "  WHERE (" + OrderField + " < " + CurSortNo + ") and unitid=" + UID + " " + FiledCheck + " " + LevComm + " ORDER BY " + OrderField + " DESC");
			if (OtherID == "" || OtherID == "0")
				return;
			string SortID = DAL.ExecuteData.CNTDataStr("SELECT " + OrderField + "  FROM " + TableName + "  WHERE " + ID_filed + "=" + OtherID);

			DAL.ExecuteData.ExecData("update " + TableName + " set " + OrderField + "=" + SortID + " where " + ID_filed + "=" + CurID + "  update " + TableName + " set " + OrderField + "=" + CurSortNo + " where " + ID_filed + "=" + OtherID);
		}
		public static void SortFiled(MySortType MST, string ComArg, string TableName, string ID_filed, string OrderField = "Sort", string LevCheck = "", string FiledCheck = "", bool UnitIDCheck = true)
		{
			string CurSortNo = ComArg.Substring(0, ComArg.IndexOf("#"));
			string CurID = ComArg.Substring(ComArg.IndexOf("#") + 1);
			if (CurSortNo == "0" && MST == MySortType.UP)
				return;

			if (string.IsNullOrEmpty(OrderField))
				OrderField = "Sort";
			string UIDCheck = "";
			if (UnitIDCheck)
			{
				object UID = ADAL.A_CheckData.GetUnitID();
				if (UID == null || UID.ToString() == "")
					UID = GetViewUnitID;
				UIDCheck = " and unitid=" + UID + " ";
			}

			string LevComm = "";
			if (LevCheck != "")
				LevComm = " AND ([Level] LIKE '" + LevCheck.Substring(0, (LevCheck.Length - 2)) + "%') AND (LEN([Level]) = " + LevCheck.Length + ") ";

			string OtherID = "";
			if (MST == MySortType.UP)
				OtherID = ADAL.A_ExecuteData.CNTData("SELECT TOP (1) " + ID_filed + "  FROM " + TableName + "  WHERE (" + OrderField + " > " + CurSortNo + ") " + UIDCheck + " " + FiledCheck + " " + LevComm + " ORDER BY " + OrderField + "");
			else
				OtherID = ADAL.A_ExecuteData.CNTData("SELECT TOP (1) " + ID_filed + "  FROM " + TableName + "  WHERE (" + OrderField + " < " + CurSortNo + ") " + UIDCheck + " " + FiledCheck + " " + LevComm + " ORDER BY " + OrderField + " DESC");
			if (OtherID == "" || OtherID == "0")
				return;
			string SortID = ADAL.A_ExecuteData.CNTData("SELECT " + OrderField + "  FROM " + TableName + "  WHERE " + ID_filed + "=" + OtherID);

			ADAL.A_ExecuteData.ExecData("update " + TableName + " set " + OrderField + "=" + SortID + " where " + ID_filed + "=" + CurID + "  update " + TableName + " set " + OrderField + "=" + CurSortNo + " where " + ID_filed + "=" + OtherID);
		}
		public static string DeleteTag(string InText)
		{
			return Regex.Replace(InText, "<.*?>", string.Empty);
		}
		public static string DelTag(string InText, string TagName, bool Br, bool Space, bool myendtag = false)
		{
			//Del Font tag
			TagName = TagName.ToLower();
			string TextTemp = InText.ToLower();
			int stPo = 0, Len = 0;
			int TagSize = TagName.Length;
			while (TextTemp.IndexOf("<" + TagName) != -1)
			{
				int endind = 0;
				stPo = TextTemp.IndexOf("<" + TagName);
				if (myendtag)
					endind = TextTemp.IndexOf("</" + TagName + ">", stPo) - stPo + 1 + TagName.Length + 3;
				Len = TextTemp.IndexOf(">", stPo) - stPo + 1;
				try//if(stPo > Len)
				{
					if (myendtag)
						InText = InText.Remove(stPo, endind);
					else
						InText = InText.Remove(stPo, Len);
					if (Space)
					{
						InText = InText.Insert(stPo, " ");
						stPo = stPo + 1;
					}
					TextTemp = InText.ToLower();
				}
				catch
				{
					break;
					//goto ali;
				}
			}
			while (TextTemp.IndexOf("</" + TagName + ">") != -1)
			{
				stPo = TextTemp.IndexOf("</" + TagName + ">");
				InText = InText.Remove(stPo, TagSize + 3);

				if (Space)
				{
					InText = InText.Insert(stPo, " ");
					stPo = stPo + 1;
				}
				if (Br)
				{
					InText = InText.Insert(stPo, "<br>");
					stPo = stPo + 4;
				}

				TextTemp = InText.ToLower();
			}
			return InText;
		}
		public enum MySortType
		{
			UP, Down
		}
		public static string GetUnitName()
		{
			if (ADAL.A_CheckData.GetUnitID() == "")
				return "";
			return DAL.ExecuteData.CNTDataStr("SELECT Name  FROM Units  WHERE (UnitID = " + ADAL.A_CheckData.GetUnitID() + ")");
		}
		public static void SetDropDownListItem(System.Web.UI.WebControls.DropDownList DL, ArrayList MyFieldArray,bool AddSpace=true)
		{
			DL.Items.Clear();
			DL.Items.Add("");
			for (int i = 0; i < MyFieldArray.Count; i++)
			{
				DL.Items.Add(MyFieldArray[i].ToString());
			}
		}

		public static void DeleteFile(string FileAddress)
		{
			if (File.Exists(FileAddress))
				File.Delete(FileAddress);
		}
		public static string GetValue(string FirstVal, string SecVal)
		{
			if (FirstVal.Trim() != "")
				return FirstVal;
			return SecVal;
		}
		public static void SetListItem(string Addr, System.Web.UI.WebControls.DropDownList MyDL)
		{
			MyDL.Items.Clear();

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Addr);
			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			{
				using (StreamReader reader = new StreamReader(response.GetResponseStream()))
				{
					string html = reader.ReadToEnd();
					var regex = new Regex("<a[^>]*>(.*?)</a>", RegexOptions.IgnoreCase);
					MatchCollection matches = regex.Matches(html);
					if (matches.Count > 0)
					{
						foreach (Match match in matches)
						{
							if (match.Success)
							{
								if (match.Groups[1].ToString().IndexOf("web.config") == -1 && match.Groups[1].ToString().IndexOf("irectory]") == -1)
									MyDL.Items.Add(new ListItem(match.Groups[1].ToString(), match.Groups[1].ToString()));
							}
						}
					}
				}
			}

			//FileInfo[] files = new DirectoryInfo(FileSystemManager() + @"\" + UnitID + @"\" + MyPath).GetFiles();
			////ListItem[] MyLi = new ListItem[files.Length];
			//MyDL.Items.Add(new ListItem("بدون آدرس", ""));
			//for (int i = 0; i < files.Length; i++)
			//{
			//	//str += "<li class=\"MyLI\"><img height=140 width=180 src=\"Files/" + Global.UnitID + "/RandImages/" + files[i] + "\" /></li>";
			//	//Str += "variableslide[" + i.ToString() + "] = ['Files/" + Global.UnitID + "/RandImages/" + files[i] + "', '', '']; ";

			//	//MyLi[i].Text = files[i].Name;
			//	//MyLi[i].Value = files[i].Name;
			//	MyDL.Items.Add(new ListItem(files[i].Name, files[i].Name));
			//}
		}
        public static void FillLanguageDL(DropDownList LanguageDL)
        {
            LanguageDL.DataSource = ADAL.A_ViewData.MyDR("SELECT LangID,Name FROM Language");
            LanguageDL.DataBind();
            try
            {
                LanguageDL.SelectedValue = GetLangID(Tools.GetAdminSetting(366, "1"));//Tools.LangID; //LanguageDL.Items.IndexOf(LanguageDL.Items.FindByText(Tools.LangID));
            }
            catch { }
        }


		public static string GetSubstring(string Str, int StartPo, int Len)
		{
			if (Str.Length < StartPo+Len)
				return "";
			try
			{
				return Str.Substring(StartPo, Len);
			}
			catch { return ""; }
			
		}

		public static float convertKiloToMega(int KiloByte)
		{
			return (float)(KiloByte / 1000f);
		}

		public static bool CheckFileExtention(string Ex, string FileTypes)
		{
			FileTypes = FileTypes.ToLower();
			Ex = Ex.ToLower();
				if (FileTypes.IndexOf(Ex) != -1)
					return true;
			
			return false;
		
		}

		public static string GetFileName(string FileAddress)
		{
			try{
				return FileAddress.Substring(FileAddress.LastIndexOf("/") + 1);
				}
			catch{return "";}
		}
		//فیلد اول متن و فیلد دوم عدد مربوطه
		public static void SetDropDownListItem(DropDownList TypeValDL, string CommandText)
		{
			TypeValDL.Items.Clear();
			SqlDataReader Myread = DAL.ViewData.MyDR1(CommandText);
			while (Myread.Read())
			{
				TypeValDL.Items.Add(new ListItem(MyCL.MGStr(Myread, 1), MyCL.MGInt(Myread, 0).ToString()));
			}
			Myread.Close(); Myread.Dispose();
		}

		public static string MySubString(string InText, int Start, int Len)
		{
			try
			{
				if (InText.Length < Len)
					return "";
				return InText.Substring(Start, Len);
			}
			catch
			{
				return "";
			}

		}

		public static void FillDLWidthArray(ListItemCollection DL, string[] MyArray)
		{
			DL.Clear();
			for (int i = 0; i < MyArray.Length; i++)
				DL.Add(new ListItem(MyArray[i], i.ToString()));
		}

		public static string GetKeyWord(string InText)
		{
			InText = InText.Replace("\"", " ").Trim();
			InText = InText.Replace("(", " ");
			InText = InText.Replace(")", " ");
			InText = InText.Replace("-", " ");
			InText = InText.Replace("_", " ");
			InText = InText.Replace("_", " ");
			InText = InText.Replace("”", " ");
			InText = InText.Replace("“", " ");
			InText = InText.Replace("+", " ");
			InText = InText.Replace(":", " ");
			InText = InText.Replace("\r\n", " ");

			InText = InText.Replace("  ", " ");
			InText = InText.Replace("  ", " ");
			InText = InText.Replace("  ", " ");
			
			ArrayList Myk = new ArrayList();
			string[] keyw = Regex.Split(InText.Replace("  ", " "), " ");
			for (int i = 0; i < keyw.Length; i++)
			{
				if(keyw[i].Length>3)
				{
					bool add = true;
					for (int b = 0; b < Myk.Count; b++)
					{
						if (keyw[i].Trim() == Myk[b].ToString().Trim())
							add = false;
					}
					if (add)
						Myk.Add(keyw[i]);
				}
			}
			string ot = "";
			for (int b = 0; b < Myk.Count; b++)
			{
			ot+=Myk[b]+" , ";
			}
			return ot.Trim().TrimEnd(',');
		}

		internal static string[] SplitText(string SMessage, int p)
		{
			return Regex.Split(SMessage, @"(?<=\G.{"+p+"})");
		}

		public static DateTime ConvertToDateTime(object Date)
		{
			try
			{
				DateTime MyDate = Convert.ToDateTime(Date);
				return MyDate;
			}
			catch
			{
				return DateTime.MinValue ;
			}

		}
		public static bool ActiveDirectoryCreateUser(string Name, string UserName, string Password, string Email = "", string GroupName = "user")
		{
			PrincipalContext ouContex = new PrincipalContext(ContextType.Domain,GetSetting(472));

			UserPrincipal up = new UserPrincipal(ouContex);
			up.SamAccountName = UserName;
			up.Name = Name;
			up.SetPassword(Password);
			up.EmailAddress = Email;
			up.Enabled = true;
			up.PasswordNeverExpires = true;
			//up.AccountExpirationDate
			//up.ExpirePasswordNow();
			up.Save();
			return true;
		}
		public static bool ActiveDirectoryChangePassword(string UserName, string Password)
		{
			using (var context = new PrincipalContext(ContextType.Domain))
			{
				using (var user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, UserName))
				{
					user.SetPassword(Password);
					// or
					//user.ChangePassword("oldPassword", "newpassword");
				}
			}
			return true;
		}
		public static void AddDirectorySecurity(string DirName, string Account, FileSystemRights Rights, AccessControlType ControlType,bool CreateFolder=true,long QuotaSize=0)
		{
			///QuotaSize must set KB
			if (CreateFolder)
			{
				if (!Directory.Exists(DirName))
					Directory.CreateDirectory(DirName);
			}
			// Create a new DirectoryInfo object.
			DirectoryInfo dInfo = new DirectoryInfo(DirName);

			// Get a DirectorySecurity object that represents the 
			// current security settings.
			DirectorySecurity dSecurity = dInfo.GetAccessControl();

			// Add the FileSystemAccessRule to the security settings. 
			dSecurity.AddAccessRule(new FileSystemAccessRule(Account,Rights,ControlType));

			// Set the new access settings.
			dInfo.SetAccessControl(dSecurity);

			if (QuotaSize != 0)
			{
				/*DiskQuotaControlClass diskQuotaCtrl = ContentStorageManager.GetDiskQuotaControl(diskVolume);
				diskQuotaCtrl.UserNameResolution = UserNameResolutionConstants.dqResolveNone;
				DIDiskQuotaUser diskUser = diskQuotaCtrl.AddUser(userName);
				diskUser.QuotaLimit = quota;
				diskUser.QuotaThreshold = quotaThreshold;
				*/
				
				DiskQuotaControl colDiskQuotas = new DiskQuotaControl();
				DIDiskQuotaUser quotaUser = null;
				colDiskQuotas = new DiskQuotaTypeLibrary.DiskQuotaControl();
				colDiskQuotas.Initialize(DirName.Substring(0,DirName.IndexOf("\\")), true);
				quotaUser = colDiskQuotas.FindUser(Account);
				quotaUser.QuotaLimit = QuotaSize*1024*1024;
			
				/*IFsrmQuotaManager FSRMQuotaManager = new FsrmQuotaManagerClass();
				IFsrmQuota Quota = null;
				//Quota.Path = DirName;
				Quota = FSRMQuotaManager.GetQuota(DirName);
				// If there is quota then we just set it to our new size
				Quota.QuotaLimit = QuotaSize;
				Quota.Commit();				*/
			}
		}
		public static void AddMemberUserActiveDirectory(int GuestID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@GuestID", GuestID);
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT GuestID, UserName, Pass, Email, Name, Family  FROM GuestInfo WHERE (GuestID = @GuestID)", SP);
			string UserName = "";
			if(MyRead.Read())
			{
				UserName = MyCL.MGStr(MyRead, 1);
				ActiveDirectoryCreateUser(MyCL.MGStr(MyRead, 4) +" "+MyCL.MGStr(MyRead, 5), MyCL.MGStr(MyRead, 1), MyDecry(MyCL.MGStr(MyRead, 2)), MyCL.MGStr(MyRead, 3), GetSetting(474, ""));
			}
			MyRead.Close(); MyRead.Dispose();
			if(GetSetting(473,"0")!="0")
			{
				AddDirectorySecurity(GetSetting(475, "") + "\\" + UserName, UserName, FileSystemRights.Modify, AccessControlType.Allow, true, ConvertToInt64(GetSetting(473)));
			}
			//473//حجم
		}
		public static string GetQuotaSize(string DirName, string Account)
		{
			DiskQuotaControl colDiskQuotas = new DiskQuotaControl();
			DIDiskQuotaUser quotaUser = null;
			colDiskQuotas = new DiskQuotaTypeLibrary.DiskQuotaControl();
			colDiskQuotas.Initialize(DirName.Substring(0, DirName.IndexOf("\\")), true);
			quotaUser = colDiskQuotas.FindUser(Account);
			return quotaUser.QuotaLimit.ToString();
			
		}
		public static int GetRandomInt(int st = 1000, int end = 9999)
		{
			Random rnd = new Random();
			return rnd.Next(st, end);
		}

		public static string GetPersentage(object p1, object p2)
		{
			if (p1 == null || p2 == null)
				return "0";
			int t1 = Tools.ConvertToInt32(p1, 0);
			int t2 = Tools.ConvertToInt32(p2, 0);
			try { 
			return (((Decimal)t2 / (Decimal)t1)*100).ToString("00.0");
				}
			catch
			{
				return "";
			}
		}
		public static string GetAdvertise(int _PageID)
		{
			if (_PageID == 0)
				_PageID = 1;
			string str = "";
			SqlDataReader MyRead = ViewData.MyDR1("SELECT  AdvertisePage.AdvertisePageID, Advertise.Name, Advertise.URL, Advertise.Files, Advertise.AdvertiseID,Advertise.width, Advertise.height,Advertise.ClickCount, Advertise.ViewCount, Advertise.MAXViewCount, Advertise.MaxClickCount, Advertise.Enable, Advertise.Type  FROM AdvertisePage INNER JOIN Advertise ON AdvertisePage.AdvertiseID = Advertise.AdvertiseID  WHERE     (AdvertisePage.UnitID = " +GetViewUnitID + ") AND (AdvertisePage.PageID = " + _PageID + ") ORDER BY AdvertisePage.Sort", null);
			while (MyRead.Read())
			{
				if (MyCL.MGInt(MyRead, 11) != 1)//Enable Item
					continue;
				int AdverType = MyCL.MGInt(MyRead, 12);
				if (AdverType == 1) //click
				{
					int ClickCount = MyCL.MGInt(MyRead, 7);
					int MaxClickCount = MyCL.MGInt(MyRead, 10);
					if (ClickCount >= MaxClickCount)
						continue;
				}
				else if (AdverType == 2)//view
				{
					int ViewCount = MyCL.MGInt(MyRead, 8);
					int MaxViewCount = MyCL.MGInt(MyRead, 9);
					if (ViewCount >= MaxViewCount)
						continue;
				}
				ExecuteData.AddData("UPDATE Advertise SET ViewCount = ViewCount + 1 WHERE (AdvertiseID = " + MyCL.MGInt(MyRead, 4) + ")", null);
				string extension = System.IO.Path.GetExtension(MyCL.MGStr(MyRead, 3));
				switch (extension)
				{
					case ".jpg":
					case ".gif":
					case ".png":
						if (_PageID == 22)
							str = str + "<a href='" + "/Advertise-" + MyCL.MGInt(MyRead, 4) + ".aspx'><img border='0' src='" + CheckData.GetFilesRoot() + "/Images/Advertise/" + MyCL.MGStr(MyRead, 3) + "' heigth='" +MyCL.MGStr(MyRead, 6) + "' width='" +MyCL.MGStr(MyRead, 5) + "' alt='" +MyCL.MGStr(MyRead, 1) + "'></a>";
						else
							str = str + "<a href='" + "/Advertise-" + MyCL.MGInt(MyRead, 4) + ".aspx'><img style=\"margin-top:10px\" border='0' src='" + CheckData.GetFilesRoot() + "/Images/Advertise/" +MyCL.MGStr(MyRead, 3) + "' heigth='" +MyCL.MGStr(MyRead, 6) + "' width='" +MyCL.MGStr(MyRead, 5) + "' alt='" +MyCL.MGStr(MyRead, 1) + "'></a><br style='line-height:10px'>";
						break;

					default:
						if (extension == ".swf")
						{
							str = (str + string.Format("<OBJECT  style=\"z-index:0;margin-top:10px\" codeBase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0' height='{0}' width='{1}'",MyCL.MGStr(MyRead, 6),MyCL.MGStr(MyRead, 5)) + string.Format("classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000'><PARAM NAME='movie' VALUE='{0}'><PARAM NAME='menu' VALUE='false'>", CheckData.GetFilesRoot() + "/Images/Advertise/" +MyCL.MGStr(MyRead, 3))) + string.Format("<EMBED src='{0}' style=\"z-index:0;\" quality='high' width='{1}' height='{2}' TYPE='application/x-shockwave-flash'", CheckData.GetFilesRoot() + "/Images/Advertise/" +MyCL.MGStr(MyRead, 3),MyCL.MGStr(MyRead, 5),MyCL.MGStr(MyRead, 6)) + "PLUGINSPAGE='http://www.macromedia.com/go/getflashplayer'></EMBED></OBJECT>";
						}
						break;
				}
				str = str + "";
			}
			MyRead.Close(); MyRead.Dispose();
			return str;
		}
	}
}