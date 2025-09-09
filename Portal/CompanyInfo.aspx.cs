using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
	public partial class CompanyInfo : System.Web.UI.Page
	{
		public string PageTitle = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim() != "")
			{
				if (Request.QueryString["ID"].ToLower().IndexOf("type") == -1)
				{
					if (DAL.CheckData.CheckAccess(Tools.MyVar.SiteGuest.CompanyInfo, Tools.Tools.ConvertToInt32(Request.QueryString["ID"].Replace("type", ""))))
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
				Tools.Tools.SetPageSeo(Page, "CompanyInfo.aspx");
			}
			if (!IsPostBack)
				CurrentPage = -1;


			Tools.Tools.SetPageHit(this.Page.ToString(), this.Page.ClientQueryString);
			Default.Adv.PageID = 2;
			Pages.PlaceSTR = new System.Collections.ArrayList();
			//Pages.PlaceSTR.Add("ArticleType.ascx");
		}
		void TextDB(string ID)
		{
			RatePH.Controls.Add(Tools.Tools.LoadControl(Page, "/Def/rate.ascx", (int)Tools.MyVar.SiteRate.CompanyInfo, ViewData.SetRate((int)Tools.MyVar.SiteRate.CompanyInfo, ID), ID));
			CommentPL.Controls.Add(Tools.Tools.LoadControl(Page, "/Def/Comments.ascx", (int)Tools.MyVar.SiteRate.CompanyInfo, ID));
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@ID", ID);
			ExecuteData.ExecData("update CompanyInfo set Hit=hit+1  WHERE CompanyInfoid=@ID", SP);
			CoInfoDG.DataSource = ViewData.MyDT("SELECT  CompanyInfoID, Title, Des, Adr, Tel, Fax, Email, Site,hit FROM CompanyInfo WHERE (UnitID = " + Tools.Tools.GetViewUnitID + ") AND (LangID = " + Tools.Tools.LangID + ") and (CompanyInfoID = @ID) and (disable=0)", SP);
			CoInfoDG.DataBind();
		}
		void TitleDB(int MyType)
		{
			string TypeStr = "";
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);

			if (MyType != 0)
			{
				SP.AddWithValue("@CompanyInfoTypeID", MyType);
				TypeStr = " AND (CompanyInfoTypeID = @CompanyInfoTypeID) ";
				//PageTitle = DAL.ExecuteData.CNTDataStr("SELECT TOP (1) Name FROM ArticleType WHERE (ArticleTypeID =@ArticleTypeID ) AND (UnitID = @UnitID) AND (LangID =" + Tools.Tools.LangID + ")", SP);
				//Tools.Tools.SetTitle(Page, PageTitle, true);
				//Tools.Tools.SetMetaTag(Page, Tools.Tools.MetaTags.Keywords, PageTitle.Replace(" ", ","));
			}
			if(SearchTB.Text.Trim()!="")
			{
				SP.AddWithValue("@Title", "%"+SearchTB.Text+"%");
				TypeStr += " AND (title like @Title) ";			
			}
			SP.AddWithValue("@TypeStr", MyType);
			PagedDataSource pgitems = new PagedDataSource();
			DataView dv = new DataView(ViewData.MyDT("SELECT  CompanyInfoID, Title, Des, Adr, Tel, Fax, Email, Site,hit FROM CompanyInfo  WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=@UnitID) and (disable=0) " + TypeStr + " " + DAL.CheckData.NotLoginGuestFilterID(Tools.MyVar.SiteGuest.CompanyInfo, "CompanyInfoID") + " ORDER BY title ", SP));
			pgitems.DataSource = dv;
			pgitems.AllowPaging = true;
			pgitems.PageSize = 50; //25;
			if (CurrentPage < 0)
				CurrentPage = 0;
			pgitems.CurrentPageIndex = CurrentPage;
			lnkPreviousPage.Enabled = !pgitems.IsFirstPage;
			lnkNextPage.Enabled = !pgitems.IsLastPage;
			Tools.Tools.GetPagging(pgitems, rptPages, CurrentPage);
			ArticleTitleDR.DataSource = pgitems;
			ArticleTitleDR.DataBind();
		}
		protected void lnkNextPage_Click(object sender, EventArgs e)
		{
			CurrentPage += 1;
			if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToLower().IndexOf("type") != -1)
				TitleDB(Tools.Tools.ConvertToInt32(Request.QueryString["ID"].ToLower().Replace("type", "")));
			else
				TitleDB(0);
		}
		protected void lnkPreviousPage_Click(object sender, EventArgs e)
		{
			CurrentPage -= 1;
			if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToLower().IndexOf("type") != -1)
				TitleDB(Tools.Tools.ConvertToInt32(Request.QueryString["ID"].ToLower().Replace("type", "")));
			else
				TitleDB(0);
		}
		public int CurrentPage
		{
			get
			{
				//Look for current page in ViewState
				object o = this.ViewState["_CompanyInfoPage"];
				if (o == null)
					return 0; // default page index of 0
				else
					return (int)o;
			}
			set
			{
				this.ViewState["_CompanyInfoPage"] = value;
			}
		}
		protected void rptPages_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			try
			{
				CurrentPage = (Tools.Tools.ConvertToInt32(e.CommandArgument) - 1);
			}
			catch { }
			if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToLower().IndexOf("type") != -1)
				TitleDB(Tools.Tools.ConvertToInt32(Request.QueryString["ID"].ToLower().Replace("type", "")));
			else
				TitleDB(0);
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

		protected void SearchBTN_Click(object sender, EventArgs e)
		{
			TitleDB(0);
		
		}
	}
}
