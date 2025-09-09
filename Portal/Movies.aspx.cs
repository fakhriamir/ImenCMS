using System; using DAL;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
    public partial class Movies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
		{
			Tools.Tools.SetPageHit(this.Page.ToString(),this.Page.ClientQueryString);
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim() != "")
            {

				if (Request.QueryString["ID"].ToLower().IndexOf("type") == -1)
                {
                    TextDB(Tools.Tools.ConvertToInt32(Request.QueryString["ID"]).ToString());
                    ItemMV.SetActiveView(TextItem);
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
                SqlDataReader MyRead = ViewData.MyDR1("SELECT Title, Keyword, Description FROM MetaKey  WHERE (UnitID = " +Tools.Tools.GetViewUnitID + ") AND (PageName = 'movies.aspx') AND (LangID = " + Tools.Tools.LangID + ")", null);
                if (MyRead.Read())
                {
                    Tools.Tools.SetTitle(this, Tools.MyCL.MGStr(MyRead, 0), true);
                    Tools.Tools.SetMetaTag(Page, Tools.Tools.MetaTags.Keywords, Tools.MyCL.MGStr(MyRead, 1));
                    Tools.Tools.SetMetaTag(Page, Tools.Tools.MetaTags.Description, Tools.MyCL.MGStr(MyRead, 2));
                }
                MyRead.Close(); MyRead.Dispose();
                //ViewData.MyConnection.Close();
            }
			Default.Adv.PageID = 8;
			Pages.PlaceSTR = new System.Collections.ArrayList();
			Pages.PlaceSTR.Add("MovieType.ascx");
        }
        void TextDB(string ID)
        {
			RatePH.Controls.Add(LoadControl("/Def/rate.ascx", (int)Tools.MyVar.SiteRate.Movies, ViewData.SetRate((int)Tools.MyVar.SiteRate.Movies, ID), ID));
			CommentPL.Controls.Add(LoadControl("/Def/Comments.ascx", (int)Tools.MyVar.SiteRate.Movies, ID));
            SqlParameterCollection SP = new SqlCommand().Parameters;
            SP.AddWithValue("@ID", ID);
            ExecuteData.ExecData("update Movie set Hit=hit+1  WHERE Movieid=@ID",SP);
			MovieDG.DataSource = ViewData.MyDT("SELECT Movie.*, MovieType.Name AS MovieType FROM Movie INNER JOIN MovieType ON Movie.MovieTypeID = MovieType.MovieTypeID WHERE (MovieID = @ID)  and Movie.disable=0", SP);
            MovieDG.DataBind();
        }
        void TitleDB(int MyType)
        {
            string TypeStr = "";
            if (MyType != 0)
                TypeStr = " AND (MovieType.movieTypeID = @TypeStr) ";
            SqlParameterCollection SP = new SqlCommand().Parameters;
            SP.AddWithValue("@TypeStr", MyType);
            SP.AddWithValue("@UnitID",Tools.Tools.GetViewUnitID);

            PagedDataSource pgitems = new PagedDataSource();
			DataView dv = new DataView(ViewData.MyDT("SELECT Movie.*, MovieType.Name AS MovieType FROM Movie INNER JOIN MovieType ON Movie.MovieTypeID = MovieType.MovieTypeID WHERE (Movie.LangID = " + Tools.Tools.LangID + ") and (Movie.unitid=@UnitID)  and Movie.disable=0 " + TypeStr + "  ORDER BY Movieid DESC", SP));
            pgitems.DataSource = dv;
            pgitems.AllowPaging = true;
            pgitems.PageSize = 24;
			if (CurrentPage < 0)
				CurrentPage = 0; pgitems.CurrentPageIndex = CurrentPage;
            lnkPreviousPage.Enabled = !pgitems.IsFirstPage;
            lnkNextPage.Enabled = !pgitems.IsLastPage;
			Tools.Tools.GetPagging(pgitems, rptPages, CurrentPage);
            TitleDG.DataSource = pgitems;
            TitleDG.DataBind();
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
				object o = this.Session["_ArticlePage"];
                if (o == null)
                    return 0; // default page index of 0
                else
                    return (int)o;
            }
            set
            {
				this.Session["_ArticlePage"] = value;
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
		public string GetMovieAddress(string Address)
		{
			if (Address.ToLower().IndexOf("http:/") == -1)
				return "/Files/" + Tools.Tools.GetViewUnitID + "/Movies/" + Address;
			return Address;
		}
    }
}
