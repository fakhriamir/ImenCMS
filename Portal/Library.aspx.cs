using System;
using DAL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
namespace Portal
{
	public partial class Library : System.Web.UI.Page
	{
		public string PageTitle = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["Page"] != null && Request.QueryString["Page"].Trim() != "")
			{
				CurrentPage = Tools.Tools.ConvertToInt32(Request.QueryString["Page"]);
				CurrentPage = CurrentPage - 1;
			}
			if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim() != "")
			{
				if (Request.QueryString["ID"].ToLower().IndexOf("type") == -1)
				{
					if (DAL.CheckData.CheckAccess(Tools.MyVar.SiteGuest.Library, Tools.Tools.ConvertToInt32(Request.QueryString["ID"].Replace("type", ""))))
					{
						TextDB(Tools.Tools.ConvertToInt32(Request.QueryString["ID"]).ToString());
						ItemMV.SetActiveView(TextItem);
					}
					else
						Response.Redirect("/Members/MemberLogin.aspx");
				}
				else
				{
					ItemMV.SetActiveView(TitleTop);
					TitleDB(Tools.Tools.ConvertToInt32(Request.QueryString["ID"].ToLower().Replace("type", "")));
				}
			}
			else
			{
				ItemMV.SetActiveView(TitleTop);
				TitleDB(0);
			}
			if (!IsPostBack)
				CurrentPage = -1;


			Tools.Tools.SetPageHit(this.Page.ToString(), this.Page.ClientQueryString);
			Default.Adv.PageID = 2;
			Pages.PlaceSTR = new System.Collections.ArrayList();
			Pages.PlaceSTR.Add("LibraryType.ascx");
		}
		void TextDB(string ID)
		{
			RatePH.Controls.Add(Tools.Tools.LoadControl(Page, "/Def/rate.ascx", (int)Tools.MyVar.SiteRate.Library, ViewData.SetRate((int)Tools.MyVar.SiteRate.Library, ID), ID));
			CommentPL.Controls.Add(Tools.Tools.LoadControl(Page, "/Def/Comments.ascx", (int)Tools.MyVar.SiteRate.Library, ID));
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@ID", ID);
			ExecuteData.ExecData("update Library set Hit=hit+1  WHERE Libraryid=@ID", SP);
			LibraryDG.DataSource = ViewData.MyDT("SELECT LibraryID,ImgAddress,Title,Publisher, Chekide, Matn, writer,writer1,writer2,writer3,writer4, FileAddress, PublishDate, Hit FROM Library WHERE (Library.UnitID = " + Tools.Tools.GetViewUnitID + ") AND (Library.LangID = " + Tools.Tools.LangID + ") and (Library.LibraryID = @ID) and (Library.disable=0)", SP);
			LibraryDG.DataBind();
		}
		void TitleDB(int MyType)
		{
			string TypeStr = "";
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);

			if (MyType != 0)
			{
				SP.AddWithValue("@LibraryTypeID", MyType);
				TypeStr = " AND (LibraryID in  (select LibraryID from LibraryCategoryItem where LibraryCategoryID=@LibraryTypeID)) ";
				PageTitle = DAL.ExecuteData.CNTDataStr("SELECT TOP (1) Name FROM LibraryCategory WHERE (LibraryCategoryID =@LibraryTypeID ) AND (UnitID = @UnitID) AND (LangID =" + Tools.Tools.LangID + ")", SP);
				Tools.Tools.SetTitle(Page, PageTitle, true);
				Tools.Tools.SetMetaTag(Page, Tools.Tools.MetaTags.Keywords, PageTitle.Replace(" ", ","));
			}
			SP.AddWithValue("@TypeStr", MyType);
			PagedDataSource pgitems = new PagedDataSource();
			DataView dv = new DataView(ViewData.MyDT("SELECT Library.ImgAddress,Library.LibraryID, Library.Title, Library.Chekide, Library.FileAddress, Library.Hit,Publisher, Library.Chekide, Library.Matn, Library.writer,Library.writer1,Library.writer2,Library.writer3,Library.writer4, Library.PublishDate  FROM Library  WHERE (Library.LangID = " + Tools.Tools.LangID + ") and (Library.unitid=@UnitID) and (Library.disable=0) " + TypeStr + " " + DAL.CheckData.NotLoginGuestFilterID(Tools.MyVar.SiteGuest.Library, "Libraryid") + " ORDER BY Libraryid DESC", SP));
			pgitems.DataSource = dv;
			pgitems.AllowPaging = true;
			pgitems.PageSize = Tools.Tools.ConvertToInt32(Tools.Tools.GetSetting(438, "25")); //25;
			if (CurrentPage < 0)
				CurrentPage = 0;
			pgitems.CurrentPageIndex = CurrentPage;
			Tools.Tools.GetPagging(pgitems, rptPages, CurrentPage);
			LibraryTitleDR.DataSource = pgitems;
			LibraryTitleDR.DataBind();
			Tools.Tools.SetPageSeo(Page, "Library.aspx");

			BackHref.HRef = "/Library/" + GetTypeLink() + "p" + GetPageID(-1) + ".aspx";
			NextHref.HRef = "/Library/" + GetTypeLink() + "p" + GetPageID(1) + ".aspx";
		}
		public string GetTypeLink()
		{
			if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToLower().IndexOf("type") != -1)
			{
				return "Type" + Tools.Tools.ConvertToInt32(Request.QueryString["ID"].ToLower().Replace("type", "")) + "/";
			}
			return "";
		}
		public int CurrentPage
		{
			get
			{
				//Look for current page in ViewState
				object o = this.ViewState["_LibraryPage"];
				if (o == null)
					return 0; // default page index of 0
				else
					return (int)o;
			}
			set
			{
				this.ViewState["_LibraryPage"] = value;
			}
		}
		public string GetCategory(string LibraryID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@LibraryID", LibraryID);
			string OutRet = "";
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT LibraryCategoryItem.LibraryCategoryID, LibraryCategory.[Level], LibraryCategory.Name FROM LibraryCategoryItem INNER JOIN LibraryCategory ON LibraryCategoryItem.LibraryCategoryID = LibraryCategory.LibraryCategoryID  WHERE(LibraryCategoryItem.LibraryID = @LibraryID)", SP,true);
			while (MyRead.Read())
			{
				if(Tools.MyCL.MGStr(MyRead,1).Length>2)
				{
					for (int i = 0; i < (Tools.MyCL.MGStr(MyRead, 1).Length/2)-1; i++)
					{
						SqlDataReader MyCat = DAL.ViewData.MyDR1("SELECT LibraryCategoryID, Name, LangID FROM LibraryCategory WHERE ([level]='"+ Tools.MyCL.MGStr(MyRead, 1).Substring(0,(i*2)+2) + "') AND (LangID = 1)");
						if(MyCat.Read())
						{
							OutRet += "<a href=\"/Library/Type"+Tools.MyCL.MGInt(MyCat,0)+ "/" +Tools.Tools.UrlWordReplace( Tools.MyCL.MGStr(MyCat, 1)) + ".aspx\">" + Tools.MyCL.MGStr(MyCat,1)+" </a>> ";
						}
						MyCat.Close();MyCat.Dispose();
					}
				}
				
					OutRet += "<a href=\"/Library/Type" + Tools.MyCL.MGInt(MyRead, 0) + "/" + Tools.Tools.UrlWordReplace(Tools.MyCL.MGStr(MyRead, 2)) + ".aspx\">" + Tools.MyCL.MGStr(MyRead, 2) + " </a><br>";
				//OutRet = OutRet.TrimEnd('>');

			}
			MyRead.Close();MyRead.Dispose();
			return OutRet;
		}
		public string GetPageID(int AddItem)
		{
			int CurPage = CurrentPage + (AddItem + 1);
			if (CurPage < 1)
				return "1";
			return CurPage.ToString();
		}
		public string GetLibraryText(string FA,string TX)
		{
			if (FA != "")
				return "<object data=\"/Files/"+Tools.Tools.GetViewUnitID+"/Library/"+FA+"\" type=\"application/pdf\" width=\"100%\" height=\"100%\"><p> مرورگر شما قابلیت نمایش فایل PDF را ندارد <a href =\"/Files/"+Tools.Tools.GetViewUnitID+"/Library/"+FA+"\">لینک دانلود</a></p></object > ";
			else
				return TX;
		}
	}
}
