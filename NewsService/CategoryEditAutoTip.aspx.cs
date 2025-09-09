using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsService
{
	public partial class CategoryEditAutoTip : System.Web.UI.Page
	{
		public string CatOptions = "";
		protected void Page_Load(object sender, EventArgs e)
		{
			if (ADAL.A_CheckData.PageAccess(this.Page.ToString()))
				Response.Redirect("/Logins");
			if (!IsPostBack)
			{ 
				UpdateGrid();
				CurrentPage = 0;
			}
			Admin.SettingID = 0;
		}
		void UpdateGrid(int catFilter = 0)
		{
			//ViewDR.DataSource = ADAL.A_ViewData.MyDR("SELECT TOP (10000) NewsID, ID, Title, Matn, Link, Lead, DT, tar, NewsRefID, Type, Ref, CategoryID  FROM News ORDER BY NewsID DESC");
			//ViewDR.DataBind();
			NewsCatDR.DataSource = ADAL.A_ViewData.MyDT("SELECT Category.CategoryID, derivedtbl_1.CNT, Category.Name FROM (SELECT AutoCategoryID, COUNT(*) AS CNT FROM News WHERE (AutoCategoryID <> 0) GROUP BY AutoCategoryID) AS derivedtbl_1 RIGHT OUTER JOIN Category ON derivedtbl_1.AutoCategoryID = Category.CategoryID");
			NewsCatDR.DataBind();
			if (!IsPostBack)
			{
				CatDL.DataSource = ADAL.A_ViewData.MyDT("SELECT CategoryID, Name FROM Category ORDER BY [Level],Sort");
				CatDL.DataBind();
			}
			CatOptions = "\"0\":\"انتخاب\",";
			SqlDataReader MyRead = ADAL.A_ViewData.MyDR("SELECT CategoryID, Name,[level] FROM Category ORDER BY [Level],Sort");
			while (MyRead.Read())
			{
				if (Tools.MyCL.MGStr(MyRead, 2).Length > 2)
				{
					string subLevel = ADAL.A_ExecuteData.CNTData("SELECT Name  FROM Category WHERE ([Level] = '" + Tools.MyCL.MGStr(MyRead, 2).Substring(0, 2) + "')");
					CatOptions += "\"" + Tools.MyCL.MGInt(MyRead, 0) + "\":\"" + subLevel + "-" + Tools.MyCL.MGStr(MyRead, 1) + "\",";
				}
				else
					CatOptions += "\"" + Tools.MyCL.MGInt(MyRead, 0) + "\":\"" + Tools.MyCL.MGStr(MyRead, 1) + "\",";
			}
			MyRead.Close(); MyRead.Dispose();
			CatOptions = CatOptions.TrimEnd(',');
			string WhereComm = " AutoCategoryID !=0 ";
			if (catFilter != 0)
				WhereComm = "AutoCategoryID ="+CatDL.SelectedValue;
			PagedDataSource pgitems = new PagedDataSource();
			DataView dv = new DataView(ADAL.A_ViewData.MyDT("SELECT TOP (1000) NewsID, ID, Title, Matn, Link, Lead, DT, tar, NewsRefID, Type, Ref, CategoryID,AutoCategoryID FROM News where " + WhereComm + " and categoryid=0"));// ORDER BY NewsID"));
			pgitems.DataSource = dv;
			pgitems.AllowPaging = true;
			pgitems.PageSize = 20; 
			if (catFilter != 0)
				pgitems.PageSize = 100;
			if (CurrentPage < 0)
				CurrentPage = 0;
			pgitems.CurrentPageIndex = CurrentPage;
			lnkPreviousPage.Enabled = !pgitems.IsFirstPage;
			lnkNextPage.Enabled = !pgitems.IsLastPage;
			Tools.Tools.GetPagging(pgitems, rptPages, CurrentPage);
			ViewDR.DataSource = pgitems;
			ViewDR.DataBind();
		}
		int CurrentPage
		{
			get
			{
				//Look for current page in ViewState
				object o = this.Session["EditCatPAge"];
				if (o == null)
					return 0; // default page index of 0
				else
					return (int)o;
			}
			set
			{
				this.Session["EditCatPAge"] = value;
			}
		}
		protected void lnkNextPage_Click(object sender, EventArgs e)
		{
			CurrentPage += 1;
			UpdateGrid();
		}
		protected void lnkPreviousPage_Click(object sender, EventArgs e)
		{
			CurrentPage -= 1;
			UpdateGrid();
		}
		protected void rptPages_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			try
			{
				CurrentPage = (Tools.Tools.ConvertToInt32(e.CommandArgument) - 1);
			}
			catch { }
			UpdateGrid();
		}
		protected void rptPages_ItemCreated(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType != ListItemType.Separator)
			{
				if (e.Item.DataItem == null)
					return;
				if (e.Item.DataItem.ToString() == (CurrentPage + 1).ToString())
					e.Item.DataItem = "<font color=red>" + e.Item.DataItem + "</font>";
			}
		}
		protected void UpdateAuto_Click(object sender, EventArgs e)
		{
			//Tools.WordList.ClearAutoCat();
			Tools.WordList.ClearCategoryWord();
			List<List<Tools.MyVar.CatList>> WordCatList = new List<List<Tools.MyVar.CatList>>();

			DataTable MyDT = ADAL.A_ViewData.MyDT("SELECT CategoryID, Name FROM Category");
			foreach (DataRow row in MyDT.Rows) // Loop over the rows.
			{
				DataTable MyCatNews = ADAL.A_ViewData.MyDT("SELECT NewsID, ID, Title, Matn, Link, Lead, DT, tar, NewsRefID, Type, Ref, CategoryID, AutoCategoryID from news where categoryid=" + row["CategoryID"]);

				string[] AllNewsCat = new string[MyCatNews.Rows.Count];
				int Index = 0;
				foreach (DataRow Newsrow in MyCatNews.Rows)
				{
					AllNewsCat[Index] = Newsrow["Title"] + " " + Newsrow["Title"] + " " + Newsrow["Title"] + " " + Newsrow["Title"] + " " + Newsrow["Title"] + " " + Newsrow["Lead"] + " " + Newsrow["Lead"] + " " + Newsrow["Lead"] + " " + Newsrow["Matn"];
					Index++;
				}
				string[] AllWordCat;
				int[] AllCountCat;

				Tools.WordList.GetAllWord(AllNewsCat, out  AllWordCat, out AllCountCat);
				List<Tools.MyVar.CatList> myCL = new List<Tools.MyVar.CatList>();
				//Dictionary<string,int> catwo=new Dictionary<string,int>();
				for (int i = 0; i < AllWordCat.Length; i++)
				{
					//ADAL.A_ExecuteData.AddData("INSERT INTO CategoryWord(CategoryID, Word, CNT) VALUES (" + row["CategoryID"] + ",'" + AllwordCat[i] + "'," + AllcountCat[i] + ")");
					//catwo.Add(AllwordCat[i], AllcountCat[i]);
					myCL.Add(new Tools.MyVar.CatList()
					{
						CatID = Tools.Tools.ConvertToInt32(row["CategoryID"]),
						Count = AllCountCat[i],
						Name = AllWordCat[i]
					});
				}
				myCL.Sort(Tools.WordList.WordComprision);
				double percentGet = 15;
				int countget = (int)(percentGet * myCL.Count / 100);
				myCL.RemoveRange(countget, myCL.Count - countget);
				//for (int i = 0; i < myCL.Count; i++)
				//{
				//	ADAL.A_ExecuteData.AddData("INSERT INTO CategoryWord(CategoryID, Word, CNT) VALUES (" + row["CategoryID"] + ",'" + myCL[i].Name + "'," + myCL[i].Count+ ")");
				//}
				WordCatList.Add(myCL);

			}

			List<string> blackList = new List<string>();
			for (int i = 0; i < WordCatList.Count; i++)
			{
				foreach (var item in WordCatList[i])
				{
					for (int j = i + 1; j < WordCatList.Count; j++)
					{
						var adff = from c in WordCatList[j] where c.Name == item.Name select c;
						if (adff.Count() > 0)
						{
							if (!blackList.Contains(item.Name)) blackList.Add(item.Name);
							break;
						}
					}
				}
			}

			foreach (var blackItem in blackList)
			{
				foreach (var listWork in WordCatList)
				{
					var delItem = from c in listWork
								  where c.Name == blackItem
								  select c;
					foreach (var item in delItem)
					{
						listWork.Remove(item);
						break;
					}

				}
			}
			for (int i = 0; i < WordCatList.Count; i++)
			{
				List<Tools.MyVar.CatList> myListIteliCat = WordCatList[i];
				int SumNum = Tools.WordList.GetSumWordCategory(ref myListIteliCat);

			}
			for (int b = 0; b < WordCatList.Count; b++)
			{
				for (int i = 0; i < WordCatList[b].Count; i++)
				{
					ADAL.A_ExecuteData.AddData("INSERT INTO CategoryWord(CategoryID, Word, CNT,Weight) VALUES (" + WordCatList[b][i].CatID + ",'" + WordCatList[b][i].Name + "'," + WordCatList[b][i].Count + "," + WordCatList[b][i].Weight + ")");
				}
			}
			Tools.Tools.Alert(Page, "لیست کلمات ساخته شد");
		}

		protected void SignAnother_Click(object sender, EventArgs e)
		{
			List<int> MyCatID = new List<int>();
			DataTable MyDT = ADAL.A_ViewData.MyDT("SELECT CategoryID FROM Category");
			MyCatID.Add(-1);
			foreach (DataRow row in MyDT.Rows) // Loop over the rows.
			{
				MyCatID.Add(Tools.Tools.ConvertToInt32( row["CategoryID"]));
			}
			MyDT.Dispose();
			List<Tools.MyVar.CatList> MyCatWord = new List<Tools.MyVar.CatList>();
			MyDT = ADAL.A_ViewData.MyDT("SELECT CategoryWordID, CategoryID, Word, CNT, Weight FROM CategoryWord");
			foreach (DataRow row in MyDT.Rows) // Loop over the rows.
			{
				MyCatWord.Add(new Tools.MyVar.CatList()
					{
						CatID=Tools.Tools.ConvertToInt32( row["CategoryID"]),
						Count =Tools.Tools.ConvertToInt32(row["CNT"]),
						Name =row["Word"].ToString().Trim(),
						Weight = Tools.Tools.ConvertToDouble(row["Weight"])
					});
			}
			MyDT.Dispose();

			MyDT = ADAL.A_ViewData.MyDT("SELECT TOP (50) NewsID, ID, Title, Matn, Link, Lead, DT, tar, NewsRefID, Type, Ref, CategoryID, AutoCategoryID  FROM News  WHERE (CategoryID = 0) AND (AutoCategoryID = 0)  ORDER BY NewsID DESC ");
			foreach (DataRow row in MyDT.Rows) // Loop over the rows.
			{
				string Txt = row["Title"] + " " + row["Matn"] + " " + row["Lead"];
				int catid= Tools.WordList.SignCatWord(Txt, MyCatWord, MyCatID);
				ADAL.A_ExecuteData.ExecData("UPDATE News SET AutoCategoryID = " + catid + " WHERE (NewsID = " + row["NewsID"] + ")");
			}
			UpdateGrid();
			Tools.Tools.Alert(Page, "50 خبر با موفقیت اضلفه شد");
		}
		public string GetImageTag(string InText)
		{
			ArrayList myimage = Tools.Tools.GetImageAddress(InText);
			string OutText = "";
			for (int i = 0; i < myimage.Count; i++)
			{
				OutText += "<img src=\"" + myimage[i] + "\">";
			}
			return HttpUtility.HtmlEncode(OutText);
		}

		protected void SignAllBTN_Click(object sender, EventArgs e)
		{
			List<int> MyCatID = new List<int>();
			DataTable MyDT = ADAL.A_ViewData.MyDT("SELECT CategoryID FROM Category");
			MyCatID.Add(-1);
			foreach (DataRow row in MyDT.Rows) // Loop over the rows.
			{
				MyCatID.Add(Tools.Tools.ConvertToInt32(row["CategoryID"]));
			}
			MyDT.Dispose();
			List<Tools.MyVar.CatList> MyCatWord = new List<Tools.MyVar.CatList>();
			MyDT = ADAL.A_ViewData.MyDT("SELECT CategoryWordID, CategoryID, Word, CNT, Weight FROM CategoryWord");
			foreach (DataRow row in MyDT.Rows) // Loop over the rows.
			{
				MyCatWord.Add(new Tools.MyVar.CatList()
				{
					CatID = Tools.Tools.ConvertToInt32(row["CategoryID"]),
					Count = Tools.Tools.ConvertToInt32(row["CNT"]),
					Name = row["Word"].ToString().Trim(),
					Weight = Tools.Tools.ConvertToDouble(row["Weight"])
				});
			}
			MyDT.Dispose();

			MyDT = ADAL.A_ViewData.MyDT("SELECT TOP (100000) NewsID, ID, Title, Matn, Link, Lead, DT, tar, NewsRefID, Type, Ref, CategoryID, AutoCategoryID  FROM News  WHERE (CategoryID = 0) AND (AutoCategoryID = 0)  ORDER BY NewsID DESC ");
			foreach (DataRow row in MyDT.Rows) // Loop over the rows.
			{
				string Txt = row["Title"] + " " + row["Matn"] + " " + row["Lead"];
				int catid = Tools.WordList.SignCatWord(Txt, MyCatWord, MyCatID);
				ADAL.A_ExecuteData.ExecData("UPDATE News SET AutoCategoryID = " + catid + " WHERE (NewsID = " + row["NewsID"] + ")");
			}
			UpdateGrid();
		}

		protected void ResetAllSignBTN_Click(object sender, EventArgs e)
		{
			ADAL.A_ExecuteData.ExecData("UPDATE News SET AutoCategoryID = 0 WHERE (AutoCategoryID!=0)");
		
		}

		protected void CatDL_Click(object sender, EventArgs e)
		{
			UpdateGrid(1);
		}
	}
}