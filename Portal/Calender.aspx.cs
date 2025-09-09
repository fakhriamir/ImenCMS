using System;
using DAL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Portal
{
	public partial class MyCalender : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim() != "")
			{
				TextDB(Tools.Tools.ConvertToInt32(Request.QueryString["ID"]).ToString());
				ItemMV.SetActiveView(TextItem);
			}
			else
			{
				int[] arabd = Tools.Calender.GetArabicDate(DateTime.Now);
				int[] engd = Tools.Calender.GetEngDate(DateTime.Now);
				int[] perd = Tools.Calender.GetPersianDate(DateTime.Now);
				ThisDayLB.Text = "امروز " + perd[2] + " " + Tools.Calender.FarMonth[perd[1]] + " " + perd[0] + "<br>" + arabd[2] + " " + Tools.Calender.AraMonth[arabd[1]] + " " + arabd[0] + " هجري قمري <br> " + engd[2] + " " + Tools.Calender.EngMonth[engd[1]] + " " + engd[0] + " ميلادي <br>";

				ItemMV.SetActiveView(TitleTop);
				TitleDB();

				//ViewData.MyConnection.Close();
			}
			Tools.Tools.SetPageSeo(Page, "Calender.aspx");
			Tools.Tools.SetPageHit(this.Page.ToString(), this.Page.ClientQueryString);

			Default.Adv.PageID = 4;
		}
		void TextDB(string ID)
		{
			System.Data.SqlClient.SqlParameterCollection SP = new System.Data.SqlClient.SqlCommand().Parameters;
			SP.AddWithValue("@ID", ID);
			ContactDG.DataSource = ViewData.MyDT("SELECT * FROM Calender WHERE (id= @ID )", SP);
			ContactDG.DataBind();
			//ViewData.MyConnection.Close();
		}
		void TitleDB()
		{
			PagedDataSource pgitems = new PagedDataSource();
			System.Data.SqlClient.SqlParameterCollection SP = new System.Data.SqlClient.SqlCommand().Parameters;
			SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);
			int[] arabd = Tools.Calender.GetArabicDate(DateTime.Now);
			int[] engd = Tools.Calender.GetEngDate(DateTime.Now);
			int[] perd = Tools.Calender.GetPersianDate(DateTime.Now);


			DataView dv = new DataView(ViewData.MyDT("SELECT id, title,Type, DayType FROM Calender WHERE (Type = 1) AND (Month = " + perd[1] + ") AND (Day = " + perd[2] + ") OR (Type = 2) AND (Month = " + arabd[1] + ") AND (Day = " + arabd[2] + ") OR (Type = 3) AND (Month = " + engd[1] + ") AND (Day = " + engd[2] + ") ORDER BY Type, myOrder", SP));
			pgitems.DataSource = dv;
			pgitems.AllowPaging = true;
			pgitems.PageSize = 15;
			if (CurrentPage < 0)
				CurrentPage = 0;
			pgitems.CurrentPageIndex = CurrentPage;
			//lnkPreviousPage.Enabled = !pgitems.IsFirstPage;
			//lnkNextPage.Enabled = !pgitems.IsLastPage;
			//NowPNoLB.Text = (CurrentPage + 1).ToString();
			ArticleTitleDR.DataSource = pgitems;
			ArticleTitleDR.DataBind();
		}
		protected void lnkNextPage_Click(object sender, EventArgs e)
		{
			CurrentPage += 1;
			TitleDB();
		}
		protected void lnkPreviousPage_Click(object sender, EventArgs e)
		{
			CurrentPage -= 1;
			TitleDB();
		}
		public int CurrentPage
		{
			get
			{
				//Look for current page in ViewState
				object o = this.Session["_CalenderPage"];
				if (o == null)
					return 0; // default page index of 0
				else
					return (int)o;
			}
			set
			{
				this.Session["_CalenderPage"] = value;
			}
		}
	}
}
