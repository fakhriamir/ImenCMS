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
	public partial class CategoryEdit : System.Web.UI.Page
	{
		public string CatOptions = "";
		protected void Page_Load(object sender, EventArgs e)
		{
			if (ADAL.A_CheckData.PageAccess(this.Page.ToString()))
				Response.Redirect("/Logins");
			if (!IsPostBack)
				UpdateGrid();
			Admin.SettingID = 0;
		}
		void UpdateGrid()
		{
			//ViewDR.DataSource = ADAL.A_ViewData.MyDR("SELECT TOP (10000) NewsID, ID, Title, Matn, Link, Lead, DT, tar, NewsRefID, Type, Ref, CategoryID  FROM News ORDER BY NewsID DESC");
			//ViewDR.DataBind();
			NewsCatDR.DataSource =  ADAL.A_ViewData.MyDT("SELECT Category.CategoryID, derivedtbl_1.CNT, Category.Name FROM (SELECT CategoryID, COUNT(*) AS CNT FROM News WHERE (CategoryID <> 0) GROUP BY CategoryID) AS derivedtbl_1 RIGHT OUTER JOIN Category ON derivedtbl_1.CategoryID = Category.CategoryID");
			NewsCatDR.DataBind();
			CatOptions = "\"0\":\"انتخاب\",";
			SqlDataReader MyRead = ADAL.A_ViewData.MyDR("SELECT CategoryID, Name,[level] FROM Category ORDER BY [Level],Sort");
			while (MyRead.Read())
			{
				if (Tools.MyCL.MGStr(MyRead, 2).Length > 2)
				{
					string subLevel = ADAL.A_ExecuteData.CNTData("SELECT Name  FROM Category WHERE ([Level] = '" + Tools.MyCL.MGStr(MyRead, 2).Substring(0,2) + "')");
					CatOptions += "\"" + Tools.MyCL.MGInt(MyRead, 0) + "\":\"" + subLevel+"-"+Tools.MyCL.MGStr(MyRead, 1) + "\",";
				}
				else
					CatOptions += "\"" + Tools.MyCL.MGInt(MyRead, 0) + "\":\"" + Tools.MyCL.MGStr(MyRead, 1) + "\",";
			}
			MyRead.Close(); MyRead.Dispose();
			CatOptions = CatOptions.TrimEnd(',');

			SqlParameterCollection SP = new SqlCommand().Parameters;
			string SStr = "";
			if (SearchTB.Text.Trim() != "")
			{
				SP.AddWithValue("@ST", Tools.Tools.SetSearchWord(SearchTB.Text.Trim().Replace("ی", "ي").Replace("ک", "ك")));
				SStr = " and (CONTAINS(Title, @ST) OR CONTAINS(Matn, @ST) OR CONTAINS(Lead, @ST)) ";
			}
			PagedDataSource pgitems = new PagedDataSource();
			DataView dv = new DataView(ADAL.A_ViewData.MyDT("SELECT TOP (10000) NewsID, ID, Title, Matn, Link, Lead, DT, tar, NewsRefID, Type, Ref, CategoryID   FROM News where CategoryID =0 " + SStr + " ORDER BY NewsID DESC",SP));
			pgitems.DataSource = dv;
			pgitems.AllowPaging = true;
			pgitems.PageSize = 30;
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

		protected void SearchBTN_Click(object sender, EventArgs e)
		{
			UpdateGrid();
		}
		public string GetImageTag(string InText)
		{
			ArrayList myimage = Tools.Tools.GetImageAddress(InText);
			string OutText = "";
			for (int i = 0; i < myimage.Count; i++)
			{
				OutText += "<img src=\""+myimage[i]+"\">";
			}
			return HttpUtility.HtmlEncode( OutText);
		}
	}
}