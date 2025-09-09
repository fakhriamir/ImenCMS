using System; using DAL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
	public partial class Gallery : System.Web.UI.Page
	{
		string PageTitle = "";
		protected void Page_Load(object sender, EventArgs e)
		{
			Tools.Tools.SetPageHit(this.Page.ToString(),this.Page.ClientQueryString);
			if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim() != "")
			{
				if (Request.QueryString["ID"].ToLower().IndexOf("type") == -1)
				{
					if (Tools.Tools.ConvertToInt32(Request.QueryString["ID"]) > 10000000)
						return;
					TextDB(Tools.Tools.ConvertToInt32(Request.QueryString["ID"]).ToString());
					ItemMV.SetActiveView(TextItem);
				}
				else//GalleryType
				{
					ItemMV.SetActiveView(TitleTop);

					TitleDB(Tools.Tools.ConvertToInt32(Request.QueryString["ID"].ToLower().Replace("type", "")));
				}
			}
			else
			{
				ItemMV.SetActiveView(TitleTop);
				TitleDB(0);
				SqlDataReader MyRead = ViewData.MyDR1("SELECT Title, Keyword, Description FROM MetaKey  WHERE (UnitID = " +Tools.Tools.GetViewUnitID + ") AND (PageName = 'gallery.aspx') AND (LangID = " + Tools.Tools.LangID + ")", null);
				if (MyRead.Read())
				{
					Tools.Tools.SetTitle(this, Tools.MyCL.MGStr(MyRead, 0), true);
					Tools.Tools.SetMetaTag(Page, Tools.Tools.MetaTags.Keywords, Tools.MyCL.MGStr(MyRead, 1));
					Tools.Tools.SetMetaTag(Page, Tools.Tools.MetaTags.Description, Tools.MyCL.MGStr(MyRead, 2));
				}
				MyRead.Close(); MyRead.Dispose();
				//ViewData.MyConnection.Close();
			}
			Default.Adv.PageID = 5;
			Pages.PlaceSTR = new System.Collections.ArrayList();
			Pages.PlaceSTR.Add("GalleryType.ascx");
		}
		void TextDB(string ID)
		{
			RatePH.Controls.Add(LoadControl("/Def/rate.ascx", (int)Tools.MyVar.SiteRate.Gallery, ViewData.SetRate((int)Tools.MyVar.SiteRate.Gallery, ID), ID));
			CommentPL.Controls.Add(LoadControl("/Def/Comments.ascx", (int)Tools.MyVar.SiteRate.Gallery, ID));
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@ID", ID);
			SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);
			ExecuteData.ExecData("update Gallery set Hit=hit+1  WHERE Galleryid=@ID", SP);
			TextDG.DataSource = ViewData.MyDT("SELECT Gallery.Name, GalleryType.Name AS TypeName,Gallery.[desc], Gallery.name,Gallery.unitid, Gallery.Hit  FROM Gallery INNER JOIN GalleryType ON Gallery.GalleryTypeID = GalleryType.GalleryTypeID  WHERE (Gallery.UnitID = @UnitID) and (Gallery.disable=0) and (Gallery.GalleryID = @ID)", SP);
			TextDG.DataBind();
		}
		void TitleDB(int MyType)
		{
			string TypeStr = "";
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@TypeStr", MyType);
			SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);
			if (MyType != 0)
			{
				TypeStr = " AND (Gallery.GalleryTypeID = @TypeStr) ";
				 PageTitle = DAL.ExecuteData.CNTDataStr("SELECT TOP (1) Name FROM GalleryType  WHERE (GalleryTypeid  =@TypeStr ) AND (UnitID = @UnitID) AND (LangID =" + Tools.Tools.LangID + ")", SP);
				Tools.Tools.SetTitle(Page, PageTitle, true);
				Tools.Tools.SetMetaTag(Page, Tools.Tools.MetaTags.Keywords, PageTitle.Replace(" ", ","));
			}
				PagedDataSource pgitems = new PagedDataSource();
			DataView dv = new DataView(ViewData.MyDT("SELECT Gallery.GalleryID, Gallery.Name, Gallery.name,Gallery.[desc], Gallery.GalleryTypeID, Gallery.UnitID, Gallery.Hit, GalleryType.Name AS Gallerywaretype FROM Gallery INNER JOIN GalleryType ON Gallery.GalleryTypeID = GalleryType.GalleryTypeID WHERE (Gallery.UnitID = @UnitID) and (Gallery.disable=0)  " + TypeStr + " ORDER BY Gallery.GalleryID DESC", SP));
			pgitems.DataSource = dv;
			pgitems.AllowPaging = true;
			pgitems.PageSize = 21;
			if (CurrentPage < 0)
				CurrentPage = 0; 
			pgitems.CurrentPageIndex = CurrentPage;
			lnkPreviousPage.Enabled = !pgitems.IsFirstPage;
			lnkNextPage.Enabled = !pgitems.IsLastPage;
			Tools.Tools.GetPagging(pgitems, rptPages, CurrentPage);
			TitleDR.DataSource = pgitems;
			TitleDR.DataBind();
		}
		protected void lnkNextPage_Click(object sender, EventArgs e)
		{
			CurrentPage =CurrentPage+ 1;
			if (Request.QueryString["ID"] == null || Request.QueryString["ID"].ToLower().IndexOf("type") == -1)
				TitleDB(0);
			else
				TitleDB(Tools.Tools.ConvertToInt32(Request.QueryString["ID"].ToLower().Replace("type", "")));
		}
		protected void lnkPreviousPage_Click(object sender, EventArgs e)
		{
			CurrentPage =CurrentPage- 1;
			if (Request.QueryString["ID"] == null || Request.QueryString["ID"].ToLower().IndexOf("type") == -1)
				TitleDB(0);
			else
				TitleDB(Tools.Tools.ConvertToInt32(Request.QueryString["ID"].ToLower().Replace("type", "")));
		}
		public int CurrentPage
		{
			get
			{
				//Look for current page in ViewState
				object o = this.ViewState["_GalleryWarePage"];
				if (o == null)
					return 0; // default page index of 0
				else
					return (int)o;
			}
			set
			{
				this.ViewState["_GalleryWarePage"] = value;
			}
		}
		private UserControl LoadControl(string UserControlPath, params object[] constructorParameters)
		{
			List<Type> constParamTypes = new List<Type>();
			foreach (object constParam in constructorParameters)
			{
				constParamTypes.Add(constParam.GetType());
			}

			UserControl ctl = Page.LoadControl(UserControlPath) as UserControl;

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

		//protected void BackBTN_Click(object sender, EventArgs e)
		//{

		//    if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
		//    {
		//        if (Tools.Tools.ConvertToInt32(Request.QueryString["ID"]) > 1000000)
		//            return;
		//        int GG = Tools.Tools.ConvertToInt32(Request.QueryString["ID"]) - 1;
		//        int CNT = 0;
		//        if (GG == 0)
		//            GG = 25;
		//        bool bb = true;
		//        while (bb)
		//        {
		//            CNT++;
		//            SqlParameterCollection SP = new SqlCommand().Parameters;
		//            SP.AddWithValue("@GG", GG);
		//            string ss = ExecuteData.CNTData("SELECT COUNT(*) AS Expr1  FROM Gallery  WHERE (GalleryID =@GG)", SP).ToString();
		//            if (CNT < 150)
		//                bb = false;
		//            if (ss != "0")
		//                bb = false;
		//            else
		//            {
		//                if (GG == 0)
		//                    GG = 25;
		//                GG = GG - 1;
		//            }
		//        }
		//        Response.Redirect("Gallery-" + GG + ".aspx");
		//    }
		//    else
		//    {

		//    }

		//}
		//protected void NextBTN_Click(object sender, EventArgs e)
		//{

		//    if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
		//    {
		//        if (Tools.Tools.ConvertToInt32(Request.QueryString["ID"]) > 1000000)
		//            return;

		//        int GG = Tools.Tools.ConvertToInt32(Request.QueryString["ID"]) + 1;
		//        if (GG == 0)
		//            GG = 1;
		//        bool bb = true;
		//        int CNT = 0;
		//        while (bb)
		//        {
		//            CNT++;
		//            SqlParameterCollection SP = new SqlCommand().Parameters;
		//            SP.AddWithValue("@GG", GG);
		//            string ss = ExecuteData.CNTData("SELECT COUNT(*) AS Expr1  FROM Gallery  WHERE (GalleryID = @GG)", SP).ToString();
		//            if (CNT < 150)
		//                bb = false;
		//            if (ss != "0")
		//                bb = false;
		//            else
		//            {
		//                if (GG == 0)
		//                    GG = 25;
		//                GG = GG + 1;
		//            }
		//        }
		//        Response.Redirect("Gallery-" + GG + ".aspx");
		//    }
		//}
		public string GetGalleryTitle(string DefTitle)
		{
			if (PageTitle.Trim()!="")
				return PageTitle;
	
			return DefTitle;
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
	}
}