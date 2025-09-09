using System;
using DAL;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Collections;
namespace Portal
{
	public partial class AdvanceSearch : System.Web.UI.Page
	{
		public static string SearchSTR = "";
		public string TabID = "7";
		protected void Page_Load(object sender, EventArgs e)
		{

			ClientScript.RegisterStartupScript(this.GetType(), "selecttab", "$('#tabs').tabs({ selected: " + hidLastTab.Value + " });", true);
			Tools.Tools.SetPageHit(this.Page.ToString(), this.Page.ClientQueryString);
			//int SType = 0;
			Default.Adv.PageID = 14;
			Tools.Tools.SetPageSeo(Page, "AdvanceSearch.aspx");
			if (!IsPostBack)
			{
				NewsPeriorityDL.DataSource = ViewData.MyDR1("SELECT * FROM NewsPeriority WHERE UnitID= " + Tools.Tools.GetViewUnitID + " AND LangID = " + Tools.Tools.LangID);
				NewsPeriorityDL.DataBind();
				NewsPeriorityDL.Items.Insert(0, new ListItem(""));

				SubjectFileDL.DataSource = ViewData.MyDR1("Select * From Soft where UnitID= " + Tools.Tools.GetViewUnitID + " AND LangID =" + Tools.Tools.LangID);
				SubjectFileDL.DataBind();
				SubjectFileDL.Items.Insert(0, new ListItem(""));

				ArticleCatDL.DataSource = ViewData.MyDR1("Select * From ArticleType where UnitID= " + Tools.Tools.GetViewUnitID + " AND LangID =" + Tools.Tools.LangID);
				ArticleCatDL.DataBind();
				ArticleCatDL.Items.Insert(0, new ListItem(""));

				MovieSubjectDL.DataSource = ViewData.MyDR1("Select * From MovieType where UnitID= " + Tools.Tools.GetViewUnitID + " AND LangID =" + Tools.Tools.LangID);
				MovieSubjectDL.DataBind();
				MovieSubjectDL.Items.Insert(0, new ListItem(""));

				SoundSubjectDL.DataSource = ViewData.MyDR1("Select * From SoundType where UnitID= " + Tools.Tools.GetViewUnitID + " AND LangID =" + Tools.Tools.LangID);
				SoundSubjectDL.DataBind();
				SoundSubjectDL.Items.Insert(0, new ListItem(""));

				ProductTypeDL.DataSource = ViewData.MyDR1("SELECT ProductSubjectID, Name FROM ProductSubject where UnitID= " + Tools.Tools.GetViewUnitID + " AND LangID =" + Tools.Tools.LangID);
				ProductTypeDL.DataBind();
				ProductTypeDL.Items.Insert(0, new ListItem(""));

				ProcudtCategoryDL.DataSource = ViewData.MyDR1("SELECT ProductCategoryID, Name  FROM ProductCategory  where UnitID= " + Tools.Tools.GetViewUnitID + " AND LangID =" + Tools.Tools.LangID + "  ORDER BY [Level]");
				ProcudtCategoryDL.DataBind();
				ProcudtCategoryDL.Items.Insert(0, new ListItem(""));
			}
			SearchSTR = SearchTB.Text = SearchTB.Text.Trim().Replace("ی", "ي").Replace("ک", "ك");

		}
		protected void SearchBind(string CommandText, SqlParameterCollection SP)
		{
			if (CommandText == null)
			{
				CommandText = MyComm;
				SP = MySP;
			}
			else
				SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);
			//ViewDR.DataSource = ViewData.MyDT(CommandText, SP);
			//ViewDR.DataBind();

			PagedDataSource pgitems = new PagedDataSource();
			DataView dv = new DataView(ViewData.MyDT(CommandText, SP));
			pgitems.DataSource = dv;
			pgitems.AllowPaging = true;
			pgitems.PageSize = 15;
			if (CurrentPage < 0)
				CurrentPage = 0;
			pgitems.CurrentPageIndex = CurrentPage;
			lnkPreviousPage.Enabled = !pgitems.IsFirstPage;
			lnkNextPage.Enabled = !pgitems.IsLastPage;
			Tools.Tools.GetPagging(pgitems, rptPages, CurrentPage);
			ViewDR.DataSource = pgitems;
			ViewDR.DataBind();
			MyComm = CommandText;
			MySP = SP;
		}
		protected void SearchBTN_Click(object sender, EventArgs e)
		{
			CurrentPage = 0;
			TabID = "7";
			SearchItems = "";
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@ST", Tools.Tools.SetSearchWord(SearchTB.Text.Trim()));
            SearchItems = SearchItems + " " + (SearchTB.Text.Trim());


			if (SearchTB.Text.Trim() == "")
			{
				Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "FillItem").ToString());
				return;
			}
			SearchBind("SELECT NewsID as ID, Title, 1 AS SType,Chekide+' '+News AS Body  FROM News  WHERE (CONTAINS(Title, @ST) OR CONTAINS(news, @ST) OR CONTAINS(ref, @ST) OR CONTAINS(Chekide, @ST)) AND (UnitID = @UnitID) " + DAL.CheckData.NotLoginGuestFilterID(Tools.MyVar.SiteGuest.News, "NewsID") + " union SELECT ArticleID as ID, Title, 2 AS SType, Text AS Body  FROM Article  WHERE (CONTAINS(Title, @ST) OR CONTAINS(Text, @ST) OR CONTAINS(Author, @ST) OR CONTAINS(Chekide, @ST)) AND (UnitID = @UnitID) union SELECT MovieID as ID, Name as title, 3 AS SType, Text AS Body  FROM Movie  WHERE (CONTAINS(name, @ST) OR CONTAINS(Text, @ST) OR CONTAINS(MovAddress, @ST)) AND (UnitID = @UnitID)  union SELECT SoundID as ID, Name as title, 4 AS SType, Text AS Body  FROM Sound  WHERE (CONTAINS(name, @ST) OR CONTAINS(Text, @ST) OR CONTAINS(MovAddress, @ST)) AND (UnitID = @UnitID)  union  SELECT SoftID as ID, Name as title, 5 AS SType, Text AS Body  FROM soft  WHERE (CONTAINS(name, @ST) OR CONTAINS(Text, @ST) OR CONTAINS(Address, @ST)) AND (UnitID = @UnitID) union SELECT TextID as ID, name as title,6 AS SType, Texts AS Body  FROM texts  WHERE (CONTAINS(name, @ST) OR CONTAINS(Texts, @ST)) AND (UnitID = @UnitID)", SP);

			Resultlb.Text = DAL.ExecuteData.CNTDataStr("SELECT (SELECT Count(*) FROM News  WHERE (CONTAINS(Title, @ST) OR CONTAINS(news, @ST) OR CONTAINS(ref, @ST) OR CONTAINS(Chekide, @ST)) AND (UnitID = @UnitID)) + (SELECT Count(*) FROM Article  WHERE (CONTAINS(Title, @ST) OR CONTAINS(Text, @ST) OR CONTAINS(Author, @ST) OR CONTAINS(Chekide, @ST)) AND (UnitID = @UnitID)) + (SELECT Count(*) FROM Movie  WHERE (CONTAINS(name, @ST) OR CONTAINS(Text, @ST) OR CONTAINS(MovAddress, @ST)) AND (UnitID = @UnitID))  + (SELECT Count(*)  FROM Sound  WHERE (CONTAINS(name, @ST) OR CONTAINS(Text, @ST) OR CONTAINS(MovAddress, @ST)) AND (UnitID = @UnitID))  +  (SELECT Count(*) FROM soft  WHERE (CONTAINS(name, @ST) OR CONTAINS(Text, @ST) OR CONTAINS(Address, @ST)) AND (UnitID = @UnitID)) + (SELECT Count(*) FROM texts  WHERE (CONTAINS(name, @ST) OR CONTAINS(Texts, @ST)) AND (UnitID = @UnitID))", SP).ToString();
		}
		public string GetHighLight(string InText)
		{
            string[] SA = Regex.Split(SearchItems.Replace("\"","").Replace("+"," ").Replace("  "," ")," ") ;
			InText = Tools.Tools.DeleteTag(InText.Replace("ی", "ي").Replace("ک", "ك")).Replace("\""," ");
			if (InText.Trim() == "")
				return "";
			bool indexo = false;
			for (int i = 0; i < SA.Length; i++)
			{
				if (SA[i].ToString().Trim() == "")
					continue;
				string[] spaceword = Regex.Split(SA[i].ToString().Trim(), " ");
				for (int b = 0; b < spaceword.Length; b++)
				{
					int wind = InText.IndexOf(spaceword[b].ToString().Trim().Replace("ی", "ي").Replace("ک", "ك"));
					if (wind == -1)
						continue;
					wind = wind - 70;
					if (wind < 0)
						wind = 0;
					if (!indexo)
					{
						InText = InText.Substring(wind).Replace(SA[i].ToString().Trim().Replace("ی", "ي").Replace("ک", "ك"), "<span class=\"SearchHighLight\">" + SA[i].ToString().Trim().Replace("ی", "ي").Replace("ک", "ك") + "</span>");
						indexo = true;
					}
					else
						InText = InText.Replace(SA[i].ToString().Trim().Replace("ی", "ي").Replace("ک", "ك"), "<span class=\"SearchHighLight\">" + SA[i].ToString().Trim().Replace("ی", "ي").Replace("ک", "ك") + "</span>");
				}
			}
			return Tools.Tools.SetExplain(InText, 300);
		}
		/*public string Highlight(string input)
		{
			if (input == string.Empty || SearchTB.Text == string.Empty)
			{
				return input;
			}

			string[] sKeywords = searchQuery.Replace("~", String.Empty).Replace("  ", " ").Trim().Split(' ');
			int totalCount = sKeywords.Length + 1;
			string[] sHighlights = new string[totalCount];
			int count = 0;

			input = Regex.Replace(input, Regex.Escape(searchQuery.Trim()), string.Format("~{0}~", count), RegexOptions.IgnoreCase);
			sHighlights[count] = string.Format("<span class=\"highlight\">{0}</span>", searchQuery);
			foreach (string sKeyword in sKeywords.OrderByDescending(s => s.Length))
			{
				count++;
				input = Regex.Replace(input, Regex.Escape(sKeyword), string.Format("~{0}~", count), RegexOptions.IgnoreCase);
				sHighlights[count] = string.Format("<span class=\"highlight\">{0}</span>", sKeyword);
			}

			for (int i = totalCount - 1; i >= 0; i--)
			{
				input = Regex.Replace(input, "\\~" + i + "\\~", sHighlights[i], RegexOptions.IgnoreCase);
			}

			return input;
		}*/
		public static string RemoveHtmlTag(string text)
		{

			String result;
			var regex = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			result = regex.Replace(text, String.Empty);
			result = Regex.Replace(result, "<.*?>", String.Empty);
			int StartIndex = result.IndexOf(text);
			if (StartIndex > -1)
				return result.Substring(200);

			return "";
		}

		protected void NewsSearchBTN_Click(object sender, EventArgs e)
		{
			CurrentPage = 0;
			SearchItems = "";

			TabID = "1";
			string strSqlWhere = ""; string SqlOrder = "";
			SqlParameterCollection SP = new SqlCommand().Parameters;

			if (!String.IsNullOrEmpty(NewsTitleTB.Text))
			{
                SearchItems = SearchItems + " " + (NewsTitleTB.Text.Trim());
				SP.AddWithValue("@NewsTitle", Tools.Tools.SetSearchWord(NewsTitleTB.Text.Trim()));
				strSqlWhere += " AND (CONTAINS(Title, @NewsTitle))";
			}
			if (!String.IsNullOrEmpty(NewsBodyTB.Text))
			{
                SearchItems = SearchItems + " " + (NewsBodyTB.Text.Trim());
				SP.AddWithValue("@NewsBody", Tools.Tools.SetSearchWord(NewsBodyTB.Text.Trim()));
				strSqlWhere += " AND (CONTAINS(News, @NewsBody))";
			}
			if (!String.IsNullOrEmpty(chekidehTB.Text))
			{
                SearchItems = SearchItems + " " + (chekidehTB.Text.Trim());
				SP.AddWithValue("@NewsChekide", Tools.Tools.SetSearchWord(chekidehTB.Text.Trim()));
				strSqlWhere += " AND (CONTAINS(Chekide, @NewsChekide))";
			}
			if (!String.IsNullOrEmpty(NewsRefTB.Text))
			{
                SearchItems = SearchItems + " " + (NewsRefTB.Text.Trim());
				SP.AddWithValue("@NewsRef", Tools.Tools.SetSearchWord(NewsRefTB.Text.Trim()));
				strSqlWhere += " AND (CONTAINS(Ref, @NewsRef))";
			}
			if (NewsPeriorityDL.SelectedValue != "")
			{
				SP.AddWithValue("@PeriorityID", NewsPeriorityDL.SelectedValue);
				strSqlWhere += " AND (NewsPeriorityID=@PeriorityID)";
			}

			string strSqlComm = "SELECT NewsID as ID, Title, 1 AS SType, News AS Body  FROM News ";
			if (strSqlWhere == "")
			{
				Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "FillItem").ToString());
				return;
			}
			else
				strSqlComm = strSqlComm + " where " + strSqlWhere.Substring(4) + " and UnitID=@UnitID  ";
			SqlOrder = " Order By newsid Desc";
			if (NewsHitCHK.Checked == true)
				SqlOrder = " Order By hit Desc";

			SearchBind(strSqlComm + " " + DAL.CheckData.NotLoginGuestFilterID(Tools.MyVar.SiteGuest.News, "NewsID") + SqlOrder, SP);
			Resultlb.Text = DAL.ExecuteData.CNTDataStr("Select Count(*) As result FROM(" + strSqlComm + ") AS aa", SP);

		}
		protected void PageSearchBTN_Click(object sender, EventArgs e)
		{
			CurrentPage = 0;
			SearchItems = "";
			TabID = "2";
			SqlParameterCollection SP = new SqlCommand().Parameters;

			string strSqlWhere = "";
			if (!String.IsNullOrEmpty(PageTitleTB.Text.Trim()))
			{
                SearchItems = SearchItems + " " + (PageTitleTB.Text.Trim());
				SP.AddWithValue("@PageTitle", Tools.Tools.SetSearchWord(PageTitleTB.Text.Trim()));
				strSqlWhere += " AND (CONTAINS(Name, @PageTitle))";
			}
			if (!String.IsNullOrEmpty(PageBodyTB.Text.Trim()))
			{
                SearchItems = SearchItems + " " + (PageBodyTB.Text.Trim());
				SP.AddWithValue("@PageBody", Tools.Tools.SetSearchWord(PageBodyTB.Text.Trim()));
				strSqlWhere += " AND (CONTAINS(Texts, @PageBody))";
			}

			string strSqlComm = "SELECT TextID as ID, name as title,6 AS SType, Texts AS Body  FROM texts";
			if (strSqlWhere == "")
			{
				Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "FillItem").ToString());
				return;
			}
			else
				strSqlComm = strSqlComm + " where " + strSqlWhere.Substring(4) + " and (UnitID=@UnitID)";

			SearchBind(strSqlComm, SP);
			Resultlb.Text = DAL.ExecuteData.CNTDataStr("Select Count(*) As result FROM(" + strSqlComm + ") AS aa", SP);
		}
		protected void SoftSearchBTN_Click(object sender, EventArgs e)
		{
			CurrentPage = 0;
			SearchItems ="";

			TabID = "3";
			SqlParameterCollection SP = new SqlCommand().Parameters;
			string strSqlWhere = ""; string SqlOrder = "";
			if (!String.IsNullOrEmpty(NewsTitleTB.Text))
			{
                SearchItems = SearchItems + " " + (FileTitleTB.Text.Trim());
				SP.AddWithValue("@SoftTitle", Tools.Tools.SetSearchWord(FileTitleTB.Text.Trim()));
				strSqlWhere += " AND (CONTAINS(Name, @SoftTitle))";
			}
			if (!String.IsNullOrEmpty(FileBodyTB.Text))
			{
                SearchItems = SearchItems + " " + (FileBodyTB.Text.Trim());
				SP.AddWithValue("@SoftBody", Tools.Tools.SetSearchWord(FileBodyTB.Text.Trim()));
				strSqlWhere += " AND (CONTAINS(Text, @SoftBody))";
			}
			if (SubjectFileDL.SelectedValue != "")
			{
				SP.AddWithValue("@SoftTypeID", SubjectFileDL.SelectedValue);
				strSqlWhere += " AND (SoftTypeID=@SoftTypeID)";
			}

			string strSqlComm = "SELECT SoftID as ID, Name as Title, 5 AS SType, Text AS Body  FROM soft  ";
			if (strSqlWhere == "")
			{
				Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "FillItem").ToString());
				return;
			}
			else
				strSqlComm = strSqlComm + " where " + strSqlWhere.Substring(4) + " and UnitID=@UnitID  ";
			SqlOrder = " Order By SoftID Desc";
			if (NewsHitCHK.Checked == true)
				SqlOrder = " Order By Hit Desc";
			SearchBind(strSqlComm + SqlOrder, SP);
			Resultlb.Text = DAL.ExecuteData.CNTDataStr("Select Count(*) As result FROM(" + strSqlComm + ") AS aa", SP);


		}
		protected void ArticleSearchBTN_Click(object sender, EventArgs e)
		{
			CurrentPage = 0;
			SearchItems = "";

			TabID = "4";
			SqlParameterCollection SP = new SqlCommand().Parameters;

			string strSqlWhere = ""; string SqlOrder = "";
			if (!String.IsNullOrEmpty(NewsTitleTB.Text))
			{
                SearchItems = SearchItems + " " + (ArticleTitleTB.Text.Trim());
				SP.AddWithValue("@ArticleTitle", Tools.Tools.SetSearchWord(ArticleTitleTB.Text.Trim()));
				strSqlWhere += " AND (CONTAINS(Title, @ArticleTitle))";
			}
			if (!String.IsNullOrEmpty(ArticleBodyTB.Text))
			{
                SearchItems = SearchItems + " " + (ArticleBodyTB.Text.Trim());
				SP.AddWithValue("@ArticleBody", Tools.Tools.SetSearchWord(ArticleBodyTB.Text.Trim()));
				strSqlWhere += " AND (CONTAINS(Text, @ArticleBody))";
			}
			if (!String.IsNullOrEmpty(ArticleChekideTB.Text))
			{
                SearchItems = SearchItems + " " + (ArticleChekideTB.Text.Trim());
				SP.AddWithValue("@ArticleChekide", Tools.Tools.SetSearchWord(ArticleChekideTB.Text.Trim()));
				strSqlWhere += " AND (CONTAINS(Chekide, @ArticleChekide))";
			}
			if (!String.IsNullOrEmpty(ArticleAuthorTB.Text))
			{
                SearchItems = SearchItems + " " + (ArticleAuthorTB.Text.Trim());
				SP.AddWithValue("@ArticleAuthor", Tools.Tools.SetSearchWord(ArticleAuthorTB.Text.Trim()));
				strSqlWhere += " AND (CONTAINS(Author, @ArticleAuthor))";
			}
			if (ArticleCatDL.SelectedValue != "")
			{
				SP.AddWithValue("@ArticleCatID", ArticleCatDL.SelectedValue);
				strSqlWhere += " AND (ArticleTypeID=@ArticleCatID)";
			}

			string strSqlComm = "SELECT ArticleID as ID, Title, 2 AS SType, Text AS Body  FROM Article";
			if (strSqlWhere == "")
			{
				Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "FillItem").ToString());
				return;
			}
			else
				strSqlComm = strSqlComm + " where " + strSqlWhere.Substring(4) + " and UnitID=@UnitID  ";
			SqlOrder = " Order By ArticleID Desc";
			if (NewsHitCHK.Checked == true)
				SqlOrder = " Order By hit Desc";

			SearchBind(strSqlComm + SqlOrder, SP);
			Resultlb.Text = DAL.ExecuteData.CNTDataStr("Select Count(*) As result FROM(" + strSqlComm + ") AS aa", SP);

		}
		protected void SoundSearchBTN_Click(object sender, EventArgs e)
		{
			CurrentPage = 0;
			SearchItems = "";

			TabID = "6";
			SqlParameterCollection SP = new SqlCommand().Parameters;

			string strSqlWhere = "";
			if (!String.IsNullOrEmpty(SoundTitleTB.Text))
			{
                SearchItems = SearchItems + " " + (SoundTitleTB.Text.Trim());
				SP.AddWithValue("@SoundTitle", Tools.Tools.SetSearchWord(SoundTitleTB.Text.Trim()));
				strSqlWhere += " AND (CONTAINS(Name, @SoundTitle))";
			}
			if (!String.IsNullOrEmpty(SoundTextTB.Text))
			{
                SearchItems = SearchItems + " " + (SoundTextTB.Text.Trim());
				SP.AddWithValue("@SoundBody", Tools.Tools.SetSearchWord(SoundTextTB.Text.Trim()));
				strSqlWhere += " AND (CONTAINS(Text, @SoundBody))";
			}
			if (SoundSubjectDL.SelectedValue != "")
			{
				SP.AddWithValue("@SoundTypeID", SoundSubjectDL.SelectedValue);
				strSqlWhere += " AND (SoundTypeID=@SoundTypeID)";
			}
			string strSqlComm = "SELECT SoundID as ID, Name as title, 4 AS SType, Text AS Body  FROM Sound";
			if (strSqlWhere == "")
			{
				Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "FillItem").ToString());
				return;
			}
			else
				strSqlComm = strSqlComm + " where " + strSqlWhere.Substring(4) + " and (UnitID=@UnitID)";

			SearchBind(strSqlComm, SP);
			Resultlb.Text = DAL.ExecuteData.CNTDataStr("Select Count(*) As result FROM(" + strSqlComm + ") AS aa", SP);
		}

		protected void MovieSearchBTN_Click(object sender, EventArgs e)
		{
			CurrentPage = 0;
			SearchItems ="";

			TabID = "5";
			SqlParameterCollection SP = new SqlCommand().Parameters;
			string strSqlWhere = "";
			if (!String.IsNullOrEmpty(MovieTitleTB.Text))
			{
                SearchItems = SearchItems + " " + (MovieTitleTB.Text.Trim());
				SP.AddWithValue("@MovieTitle", Tools.Tools.SetSearchWord(MovieTitleTB.Text.Trim()));
				strSqlWhere += " AND (CONTAINS(Name, @MovieTitle))";
			}
			if (!String.IsNullOrEmpty(MovieTextTB.Text))
			{
                SearchItems = SearchItems + " " + (MovieTextTB.Text.Trim());
				SP.AddWithValue("@MovieTextTB", Tools.Tools.SetSearchWord(MovieTextTB.Text.Trim()));
				strSqlWhere += " AND (CONTAINS(Text, @MovieTextTB))";
			}
			if (MovieSubjectDL.SelectedValue != "")
			{
				SP.AddWithValue("@MovieTypeID", MovieSubjectDL.SelectedValue);
				strSqlWhere += " AND (MovieTypeID=@MovieTypeID)";
			}
			string strSqlComm = "SELECT MovieID as ID, name as title,3 AS SType, Text AS Body  FROM Movie ";
			if (strSqlWhere == "")
			{
				Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "FillItem").ToString());
				return;
			}
			else
				strSqlComm = strSqlComm + " where " + strSqlWhere.Substring(4) + " and (UnitID=@UnitID)";

			SearchBind(strSqlComm, SP);
			Resultlb.Text = DAL.ExecuteData.CNTDataStr("Select Count(*) As result FROM(" + strSqlComm + ") AS aa", SP);

		}
		protected void ProductSearchBTN_Click(object sender, EventArgs e)
		{
			SearchItems = "";

			TabID = "8";
			SqlParameterCollection SP = new SqlCommand().Parameters;
			string strSqlWhere = ""; string SqlOrder = "";
			if (!String.IsNullOrEmpty(ProductTitleTB.Text))
			{
                SearchItems = SearchItems + " " + (ProductTitleTB.Text.Trim());
				SP.AddWithValue("@ProductTitle", Tools.Tools.SetSearchWord(ProductTitleTB.Text.Trim()));
				strSqlWhere += " AND (CONTAINS(Name, @ProductTitle))";
			}
			if (ProcudtCategoryDL.SelectedValue != "")
			{
				SP.AddWithValue("@ProcudtCategory", ProcudtCategoryDL.SelectedValue);
				strSqlWhere += " AND (ProductCategoryID=@ProcudtCategory)";
			}
			if (ProductTypeDL.SelectedValue != "")
			{
				SP.AddWithValue("@ProductSubjectID", ProductTypeDL.SelectedValue);
				strSqlWhere += " AND (ProductSubjectID=@ProductSubjectID)";
			}
			if (!String.IsNullOrEmpty(ProductDetailTB.Text))
			{
				SearchItems= SearchItems+" "+ProductDetailTB.Text.Trim();
				SP.AddWithValue("@ProductDetail", Tools.Tools.SetSearchWord(ProductDetailTB.Text.Trim()));
				strSqlWhere += " AND (CONTAINS(Detail, @ProductDetail))";
			}

			string strSqlComm = "SELECT ProductID as ID,Name as Title, 7 AS SType, Detail AS Body  FROM Product";
			if (strSqlWhere == "")
			{
				Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "FillItem").ToString());
				return;
			}
			else
				strSqlComm = strSqlComm + " where " + strSqlWhere.Substring(4) + " and UnitID=@UnitID  ";

			SearchBind(strSqlComm + SqlOrder, SP);
			Resultlb.Text = DAL.ExecuteData.CNTDataStr("Select Count(*) As result FROM(" + strSqlComm + ") AS aa", SP);
		}
		protected void lnkNextPage_Click(object sender, EventArgs e)
		{
			CurrentPage += 1;
			SearchBind(null, null);
		}
		protected void lnkPreviousPage_Click(object sender, EventArgs e)
		{
			CurrentPage -= 1;
			SearchBind(null, null);
		}
		public int CurrentPage
		{
			get
			{
				//Look for current page in ViewState
				object o = this.Session["_AdvanceSearchPage"];
				if (o == null)
					return 0; // default page index of 0
				else
					return (int)o;
			}
			set
			{
				this.Session["_AdvanceSearchPage"] = value;
			}
		}
		public SqlParameterCollection MySP
		{
			get
			{
				//Look for current page in ViewState
				object o = this.Session["_AdvanceSearchPageSP"];
				if (o == null)
					return null;
				else
					return (SqlParameterCollection)o;
			}
			set
			{
				this.Session["_AdvanceSearchPageSP"] = value;
			}
		}
		public string MyComm
		{
			get
			{
				//Look for current page in ViewState
				object o = this.Session["_AdvanceSearchPageCM"];
				if (o == null)
					return ""; // default page index of 0
				else
					return (string)o;
			}
			set
			{
				this.Session["_AdvanceSearchPageCM"] = value;
			}
		}
		public string SearchItems
		{
			get
			{
				//Look for current page in ViewState
				object o = this.Session["_AdvanceSearchPageAL"];
				if (o == null)
					return null; // default page index of 0
				else
					return (string)o;
			}
			set
			{
				this.Session["_AdvanceSearchPageAL"] = value;
			}
		}
		protected void rptPages_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			try
			{
				CurrentPage = (Tools.Tools.ConvertToInt32(e.CommandArgument) - 1);
			}
			catch { }
			SearchBind(null, null);
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
	}
}
