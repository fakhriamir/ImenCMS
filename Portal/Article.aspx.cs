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
    public partial class Article : System.Web.UI.Page
    {
		public string PageTitle = "";
	
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Tools.Tools.GetSetting(493, "1") == "0")
				Response.Redirect("/");
			if (Request.QueryString["Page"] != null && Request.QueryString["Page"].Trim() != "")
			{
				CurrentPage = Tools.Tools.ConvertToInt32(Request.QueryString["Page"]);
				CurrentPage = CurrentPage - 1;
			}
			if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim() != "")
			{
				if (Request.QueryString["ID"].ToLower().IndexOf("type") == -1)
				{
					if (DAL.CheckData.CheckAccess(Tools.MyVar.SiteGuest.Article, Tools.Tools.ConvertToInt32(Request.QueryString["ID"].Replace("type", ""))))
					{
						TextDB(Tools.Tools.ConvertToInt32(Request.QueryString["ID"]).ToString());
						ItemMV.SetActiveView(TextItem);
					}
					else
						Response.Redirect("/Members/MemberLogin");
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
			Pages.PlaceSTR.Add("ArticleType.ascx");
		}        
        void TextDB(string ID)
        {
			RatePH.Controls.Add(Tools.Tools.LoadControl(Page,"/Def/rate.ascx", (int)Tools.MyVar.SiteRate.Article, ViewData.SetRate((int)Tools.MyVar.SiteRate.Article, ID), ID));
			CommentPL.Controls.Add(Tools.Tools.LoadControl(Page,"/Def/Comments.ascx", (int)Tools.MyVar.SiteRate.Article, ID));
            SqlParameterCollection SP = new SqlCommand().Parameters;
            SP.AddWithValue("@ID", ID);
            ExecuteData.ExecData("update Article set Hit=hit+1  WHERE Articleid=@ID",SP);
			ArticleDG.DataSource = ViewData.MyDT("SELECT Article.ArticleID,Article.IMAGE,Article.Title, Article.Chekide, Article.Text, Article.Author, Article.Image, Article.WDate, Article.Hit As hit, ArticleType.Name AS TypeName FROM Article INNER JOIN ArticleType ON Article.ArticleTypeID = ArticleType.ArticleTypeID WHERE (Article.UnitID = " + Tools.Tools.GetViewUnitID + ") AND (Article.LangID = " + Tools.Tools.LangID + ") and (Article.ArticleID = @ID) and (Article.disable=0)", SP);
            ArticleDG.DataBind();
        }
        void TitleDB(int MyType)
        {
            string TypeStr = "";
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);
         
			if (MyType != 0)
			{
				SP.AddWithValue("@ArticleTypeID", MyType);
				TypeStr = " AND (ArticleType.articleTypeID = @TypeStr) ";
				PageTitle = DAL.ExecuteData.CNTDataStr("SELECT TOP (1) Name FROM ArticleType WHERE (ArticleTypeID =@ArticleTypeID ) AND (UnitID = @UnitID) AND (LangID =" + Tools.Tools.LangID + ")", SP);
				Tools.Tools.SetTitle(Page, PageTitle, true);
				Tools.Tools.SetMetaTag(Page, Tools.Tools.MetaTags.Keywords, PageTitle.Replace(" ", ","));
			}
            SP.AddWithValue("@TypeStr", MyType);
            PagedDataSource pgitems = new PagedDataSource();
			DataView dv = new DataView(ViewData.MyDT("SELECT Article.image,Article.ArticleID, Article.Title, Article.Chekide, Article.Image, Article.Hit As hit  FROM Article INNER JOIN ArticleType ON Article.ArticleTypeID = ArticleType.ArticleTypeID WHERE (Article.LangID = " + Tools.Tools.LangID + ") and (article.unitid=@UnitID) and (Article.disable=0) and article.type=0 " + TypeStr + " " + DAL.CheckData.NotLoginGuestFilterID(Tools.MyVar.SiteGuest.Article, "article.articleid") + " ORDER BY articleid DESC", SP));
            pgitems.DataSource = dv;
            pgitems.AllowPaging = true;
			pgitems.PageSize = Tools.Tools.ConvertToInt32(Tools.Tools.GetSetting(438,"25")); //25;
			if (CurrentPage < 0)
				CurrentPage = 0;
            pgitems.CurrentPageIndex = CurrentPage;
          	Tools.Tools.GetPagging(pgitems, rptPages, CurrentPage);
            ArticleTitleDR.DataSource = pgitems;
            ArticleTitleDR.DataBind();
			Tools.Tools.SetPageSeo(Page, "Article.aspx");
		
			BackHref.HRef = "/Article/"+GetTypeLink()+"p" + GetPageID(-1) + "";
			NextHref.HRef = "/Article/" + GetTypeLink() + "p" + GetPageID(1) + "";
           
		
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
				object o = this.ViewState["_ArticlePage"];
                if (o == null)
                    return 0; // default page index of 0
                else
                    return (int)o;
            }
            set
            {
				this.ViewState["_ArticlePage"] = value;
            }
        }       
		
		public string GetPageID(int AddItem)
		{
			int CurPage = CurrentPage + (AddItem + 1);
			if (CurPage < 1)
				return "1";
			return CurPage.ToString();
		}
    }
}
