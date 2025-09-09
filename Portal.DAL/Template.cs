using System;
using System.Collections;
using System.Data.SqlClient;
using DAL;
namespace Tools
{
	public class Template
	{
		public enum TemplateItem {UnitID,LangID,DefBeforTitle,DefAfterTitle,DefAfterSubTitle,DefAfterBody,DefAfterFooter}
		public enum CadrItem {UnitID,LangID,CadrTemplate,CadrBeforTitle,CadrAfterTitle,CadrAfterBody,CadrAfterFooter }
		public static ArrayList TemplateArray = new ArrayList();
		public static ArrayList CadrArray = new ArrayList();
		public static string GetCadr(CadrItem MyItem, int MyThemID, string MorLink="")
		{
			//if (File.Exists(CheckData.GetFilesRoot(true) + "\\t.t"))
			//{
			//    File.Delete(CheckData.GetFilesRoot(true) + "\\t.t");
			//    CadrArrar.Clear();
			//    TemplateArray.Clear();
			//}
            if (CadrArray.Count > 3000)
                CadrArray.Clear();
			string CurTemplate = FindCadrValue(MyItem, CadrArray, MyClass.GetViewUnitID, Tools.LangID,MyThemID);
			if (CurTemplate == "")
			{
				AddCadrArray(MyClass.GetViewUnitID, Tools.LangID);
				CurTemplate = FindCadrValue(MyItem, CadrArray, MyClass.GetViewUnitID, Tools.LangID, MyThemID);
			}
			return CurTemplate.Replace("<*MoreLink*>", MorLink).Replace("{@}","") ;
		}
		private static string FindCadrValue(CadrItem MyItem, ArrayList MyArray, string UnitID, string LangID, int MyThemID)
		{
			for (int i = 0; i < MyArray.Count; i++)
			{
				if (((Object[])MyArray[i])[0].ToString() == UnitID && ((Object[])MyArray[i])[1].ToString() == LangID && ((Object[])MyArray[i])[2].ToString() == ((int)MyItem).ToString() && ((Object[])MyArray[i])[3].ToString() == MyThemID.ToString())
				{
					return ((Object[])MyArray[i])[4].ToString();
				}
			}
			return "";
		}
		private static bool FindCadrAddTemplate(ArrayList MyArray, string UnitID, string LangID, string MyThemID)
		{
			try
			{
				if (MyArray == null)
					return false;

				for (int i = 0; i < MyArray.Count; i++)
				{
					if (((Object[])MyArray[i])[0].ToString() == UnitID && ((Object[])MyArray[i])[1].ToString() == LangID && ((Object[])MyArray[i])[3].ToString() == MyThemID.ToString())
					{
						return true;
					}
				}
				return false;
			}
			catch
			{
				//Logging.ErrorLog(ee.ToString(), "FakhriTemplateError",MyArray[0].ToString()+"&&&"+ ee.Message + "{$$$}" + ee.Source + "{$$$}" + ee.Data + "{$$$}" +ee.InnerException + "{$$$}" + ee.StackTrace + "{$$$}" + ee.TargetSite + "{$$$}");
				return false;
			}
		}
		private static void AddCadrArray(string UnitID, string LangID)
		{
			System.Data.SqlClient.SqlParameterCollection SP = new System.Data.SqlClient.SqlCommand().Parameters;
			SP.AddWithValue("@UnitID", MyClass.GetViewUnitID);
			string[] CadrTemplate = new string[20];
			string CurTemplateID = Tools.GetSetting(357, "-1");
			if(CurTemplateID=="-1")
				CurTemplateID = DAL.ExecuteData.CNTDataStr("SELECT TemplateID  FROM UnitSetting WHERE (UnitID = @UnitID)", SP);
			if (FindCadrAddTemplate(CadrArray, UnitID, LangID, CurTemplateID))
			    return;
			SqlDataReader MyCadrDR = ViewData.MyDR1("SELECT CadrID, Template  FROM TemplateCadr WHERE (TemplateID = " + CurTemplateID + ") ", null, true);
			while (MyCadrDR.Read())
				CadrTemplate[MyCL.MGInt(MyCadrDR, 0)] = MyCL.MGStr(MyCadrDR, 1).Replace("<%=MoreLink%>", "<*MoreLink*>");
			MyCadrDR.Close(); MyCadrDR.Dispose();
			for (int i = 0; i < CadrTemplate.Length; i++)
			{
				if (CadrTemplate[i] != null && CadrTemplate[i].Trim() != "")
				{
					int CTIndex = CadrTemplate[i].IndexOf("<%=CadrTitle%>");
					int CBIndex = CadrTemplate[i].IndexOf("<%=CadrBody%>");
					int CFIndex = CadrTemplate[i].IndexOf("<%=CadrFooter%>");
					if (CBIndex == -1 || CTIndex == -1)
					{
						CadrArray.Add(new Object[] { UnitID, LangID, (int)CadrItem.CadrBeforTitle, i, "{@}" });
						CadrArray.Add(new Object[] { UnitID, LangID, (int)CadrItem.CadrAfterTitle, i, "{@}" });
						CadrArray.Add(new Object[] { UnitID, LangID, (int)CadrItem.CadrAfterBody, i, "{@}" });
						CadrArray.Add(new Object[] { UnitID, LangID, (int)CadrItem.CadrAfterFooter, i, "{@}" });
						return;
					}
					string CadrBeforTitle = CadrTemplate[i].Substring(0, CTIndex);
					CadrArray.Add(new Object[] { UnitID, LangID, (int)CadrItem.CadrBeforTitle, i, CadrBeforTitle });
					string CadrAfterTitle = CadrTemplate[i].Substring(CTIndex + 14, CBIndex - 14 - CTIndex);
					CadrArray.Add(new Object[] { UnitID, LangID, (int)CadrItem.CadrAfterTitle, i, CadrAfterTitle });
					if (CFIndex != -1)
					{
						string CadrAfterBody = CadrTemplate[i].Substring(CBIndex + 13, CFIndex - 13 - CBIndex);
						CadrArray.Add(new Object[] { UnitID, LangID, (int)CadrItem.CadrAfterBody, i, CadrAfterBody });
						string CadrAfterFooter = CadrTemplate[i].Substring(CFIndex + 15);
						CadrArray.Add(new Object[] { UnitID, LangID, (int)CadrItem.CadrAfterFooter, i, CadrAfterFooter });
					}
					else
					{
						string CadrAfterBody = CadrTemplate[i].Substring(CBIndex + 13);
						CadrArray.Add(new Object[] { UnitID, LangID, (int)CadrItem.CadrAfterBody, i, CadrAfterBody });
					}
				}
				else
				{
					CadrArray.Add(new Object[] { UnitID, LangID, (int)CadrItem.CadrBeforTitle, i, "{@}" });
					CadrArray.Add(new Object[] { UnitID, LangID, (int)CadrItem.CadrAfterTitle, i, "{@}" });
					CadrArray.Add(new Object[] { UnitID, LangID, (int)CadrItem.CadrAfterBody, i, "{@}" });
					CadrArray.Add(new Object[] { UnitID, LangID, (int)CadrItem.CadrAfterFooter, i, "{@}" });
				}
			}
		}
		public static string GetTemplate(TemplateItem MyItem)
		{
			string CurTemplate = FindArrayValue(MyItem, TemplateArray, MyClass.GetViewUnitID, Tools.LangID);
			if (CurTemplate == "")
			{
				AddTemplateArray(MyClass.GetViewUnitID, Tools.LangID);
				CurTemplate = FindArrayValue(MyItem, TemplateArray, MyClass.GetViewUnitID, Tools.LangID);
			}
            if(CurTemplate.IndexOf("##ShopingCartCNT##") !=-1)
            {
                CurTemplate = CurTemplate.Replace("##ShopingCartCNT##", Shop.GetShopingCNT().ToString());
            }
			return CurTemplate.Replace("{@}", "").Replace("<*GetCurArabicDate*>", Calender.GetCurArabicDate()).Replace("<*GetCurEnglishDate*>", Calender.GetCurEngDate()).Replace("<*GetCurDate*>", Calender.GetCurPersianDate()).Replace("##GetUserState##", DAL.CheckData.GetUserLogonState()).Replace("##UserStateCustome##", DAL.CheckData.UserLogonStateCustome()); 
		}
		private static void AddTemplateArray(string UnitID, string LangID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@UnitID", MyClass.GetViewUnitID);
			string CurTemplateID = Tools.GetSetting(357, "-1");
			if (CurTemplateID == "-1")
				CurTemplateID = DAL.ExecuteData.CNTDataStr("SELECT TemplateID  FROM UnitSetting WHERE (UnitID = @UnitID)", SP);
			if (CurTemplateID == "")
				return;
			string BodyTemplate = ExecuteData.CNTDataStr("SELECT Template FROM Template WHERE (UnitID = @UnitID) and templateid="+CurTemplateID, SP);
			//ViewData.MyConnection.Close();
			//BodyTemplate = BodyTemplate;//.Replace("<*GetCurDate*>", Calender.GetCurPersianDate()).Replace("##GetUserState##", DAL.CheckData.GetUserLogonState());
			BodyTemplate = BodyTemplate.Replace("#UnitID#", MyClass.GetViewUnitID);
			int CTIndex = BodyTemplate.IndexOf("<%=SiteTitle%>");
			int CSIndex = BodyTemplate.IndexOf("<%=SiteSubTitle%>");
			int CCIndex = BodyTemplate.IndexOf("<%=SiteBody%>");
			int CFIndex = BodyTemplate.IndexOf("<%=SiteFooter%>");
			if (CTIndex == -1 || CSIndex == -1 || CCIndex == -1 || CFIndex == -1)
			{
				// Response.Write("قالب سايت شما مشکل دارد");
				return;
			}
			string SiteTitle =  ExecuteData.CNTDataStr("select unitname from unitsetting where unitid=" + MyClass.GetViewUnitID, null);
			string SiteSubTitle = ExecuteData.CNTDataStr("select unitsubname from unitsetting where unitid=" + MyClass.GetViewUnitID, null);
			string DefBeforTitle = BodyTemplate.Substring(0, CTIndex) + SiteTitle;
			TemplateArray.Add(new Object[] { UnitID, LangID, (int)TemplateItem.DefBeforTitle,DefBeforTitle });
			string DefAfterTitle = BodyTemplate.Substring(CTIndex + 14, CSIndex - 14 - CTIndex) + SiteSubTitle;
			TemplateArray.Add(new Object[] { UnitID, LangID, (int)TemplateItem.DefAfterTitle, DefAfterTitle });
			string DefAfterSubTitle = BodyTemplate.Substring(CSIndex + 17, CCIndex - 17 - CSIndex);
			TemplateArray.Add(new Object[] { UnitID, LangID, (int)TemplateItem.DefAfterSubTitle, DefAfterSubTitle });
			string DefAfterBody = BodyTemplate.Substring(CCIndex + 13, CFIndex - 13 - CCIndex);
			TemplateArray.Add(new Object[] { UnitID, LangID, (int)TemplateItem.DefAfterBody, DefAfterBody });
			string DefAfterFooter = BodyTemplate.Substring(CFIndex + 15);
			TemplateArray.Add(new Object[] { UnitID, LangID, (int)TemplateItem.DefAfterFooter, DefAfterFooter });
		}
		private static string FindArrayValue(TemplateItem MyItem, ArrayList MyArr, string UnitID, string LangID)
		{
			if (MyArr == null)
				return "";
			for (int i = 0; i < MyArr.Count; i++)
			{
				if (((Object[])MyArr[i])[0].ToString() ==UnitID && ((Object[])MyArr[i])[1].ToString()==LangID&& ((Object[])MyArr[i])[2].ToString()==((int)MyItem).ToString())
				{
					return ((Object[])MyArr[i])[3].ToString();
				}
			}
			return "";
		}
		public static string GetArrayCount()
		{
			return "TemplateArray:" + TemplateArray.Count + "<br>CadrArrar" + CadrArray.Count;
		}
	}
}