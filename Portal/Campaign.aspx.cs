using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
	public partial class Campaign : System.Web.UI.Page
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
					if (DAL.CheckData.CheckAccess(Tools.MyVar.SiteGuest.Campaign, Tools.Tools.ConvertToInt32(Request.QueryString["ID"].Replace("type", ""))))
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
			Default.Adv.PageID = 24;
		}
		void TextDB(string ID)
		{
			RatePH.Controls.Add(Tools.Tools.LoadControl(Page, "/Def/rate.ascx", (int)Tools.MyVar.SiteRate.Campaign, ViewData.SetRate((int)Tools.MyVar.SiteRate.Campaign, ID), ID));
			CommentPL.Controls.Add(Tools.Tools.LoadControl(Page, "/Def/Comments.ascx", (int)Tools.MyVar.SiteRate.Campaign, ID));
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@ID", ID);
			ExecuteData.ExecData("update Campaign set Hit=hit+1  WHERE Campaignid=@ID", SP);
			ArticleDG.DataSource = ViewData.MyDT("SELECT * FROM Campaign WHERE (UnitID = " + Tools.Tools.GetViewUnitID + ") AND (LangID = " + Tools.Tools.LangID + ") and (CampaignID = @ID) and (Visible=1)", SP);
			ArticleDG.DataBind();
		}
		void TitleDB(int MyType)
		{
			string TypeStr = "";
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);

			if (MyType != 0)
			{
				//SP.AddWithValue("@ArticleTypeID", MyType);
				TypeStr = " "; //AND (ArticleType.articleTypeID = @TypeStr) 
				//PageTitle = DAL.ExecuteData.CNTDataStr("SELECT TOP (1) Name FROM ArticleType WHERE (ArticleTypeID =@ArticleTypeID ) AND (UnitID = @UnitID) AND (LangID =" + Tools.Tools.LangID + ")", SP);
				//Tools.Tools.SetTitle(Page, PageTitle, true);
				//Tools.Tools.SetMetaTag(Page, Tools.Tools.MetaTags.Keywords, PageTitle.Replace(" ", ","));
			}
			//SP.AddWithValue("@TypeStr", MyType);
			PagedDataSource pgitems = new PagedDataSource();
			DataView dv = new DataView(ViewData.MyDT("SELECT *  FROM Campaign WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=@UnitID) and (visible=1)  " + TypeStr + " " + DAL.CheckData.NotLoginGuestFilterID(Tools.MyVar.SiteGuest.Campaign, "Campaignid") + " ORDER BY CampaignID DESC", SP));
			pgitems.DataSource = dv;
			pgitems.AllowPaging = true;
			pgitems.PageSize = Tools.Tools.ConvertToInt32(Tools.Tools.GetSetting(478, "25")); //25;
			if (CurrentPage < 0)
				CurrentPage = 0;
			pgitems.CurrentPageIndex = CurrentPage;
			Tools.Tools.GetPagging(pgitems, rptPages, CurrentPage);
			ArticleTitleDR.DataSource = pgitems;
			ArticleTitleDR.DataBind();
			Tools.Tools.SetPageSeo(Page, "Campaign.aspx");

			BackHref.HRef = "/Campaign/" + GetTypeLink() + "p" + GetPageID(-1) + ".aspx";
			NextHref.HRef = "/Campaign/" + GetTypeLink() + "p" + GetPageID(1) + ".aspx";
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
				object o = this.ViewState["_CampaignPage"];
				if (o == null)
					return 0; // default page index of 0
				else
					return (int)o;
			}
			set
			{
				this.ViewState["_CampaignPage"] = value;
			}
		}

		public string GetPageID(int AddItem)
		{
			int CurPage = CurrentPage + (AddItem + 1);
			if (CurPage < 1)
				return "1";
			return CurPage.ToString();
		}
		public string GetUserID()
		{
			if (DAL.CheckData.GuestUserLoginID() > 0)
				return Tools.Tools.ConvertToBase64(Tools.Tools.MyEncry(DAL.CheckData.GuestUserLoginID().ToString() + "L" + DateTime.Now));
			return "";
		}
		public string GetCampaignCNT(string CampaignID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@CampaignID", CampaignID);
			return DAL.ExecuteData.CNTDataStr("SELECT COUNT(*) AS Expr1  FROM CampaignGuest  WHERE (CampaignID = @CampaignID)", SP);
		}
	
	}
}