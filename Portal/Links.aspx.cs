using System; using DAL;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
    public partial class Links : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			if (Tools.Tools.GetSetting(492, "1") == "0")
				Response.Redirect("/");
			Tools.Tools.SetPageHit(this.Page.ToString(),this.Page.ClientQueryString);
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim() != "")
            {
				if (Request.QueryString["ID"].ToLower().IndexOf("type") == -1)
				{
					if (DAL.CheckData.CheckAccess(Tools.MyVar.SiteGuest.Links, Tools.Tools.ConvertToInt32(Request.QueryString["ID"].Replace("type", ""))))
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
					TitleDB(Tools.Tools.ConvertToInt32(Request.QueryString["ID"].Replace("type", "")));
				}
            }
            else
            {
                ItemMV.SetActiveView(TitleTop);
                TitleDB(0);
                SqlDataReader MyRead = ViewData.MyDR1("SELECT Title, Keyword, Description FROM MetaKey  WHERE (UnitID = " +Tools.Tools.GetViewUnitID + ") AND (PageName = 'links.aspx') AND (LangID = " + Tools.Tools.LangID + ")", null);
                if (MyRead.Read())
                {
                    Tools.Tools.SetTitle(this, Tools.MyCL.MGStr(MyRead, 0), true);
                    Tools.Tools.SetMetaTag(Page, Tools.Tools.MetaTags.Keywords, Tools.MyCL.MGStr(MyRead, 1));
                    Tools.Tools.SetMetaTag(Page, Tools.Tools.MetaTags.Description, Tools.MyCL.MGStr(MyRead, 2));
                }
                MyRead.Close(); MyRead.Dispose();
                //ViewData.MyConnection.Close();
            }
			Default.Adv.PageID = 6;
			Pages.PlaceSTR = new System.Collections.ArrayList();
			Pages.PlaceSTR.Add("LinkType.ascx"); 
        }
        void TextDB(string ID)
        {
			RatePH.Controls.Add(LoadControl("/Def/rate.ascx", (int)Tools.MyVar.SiteRate.Links, ViewData.SetRate((int)Tools.MyVar.SiteRate.Links, ID), ID));
			CommentPL.Controls.Add(LoadControl("/Def/Comments.ascx", (int)Tools.MyVar.SiteRate.Links, ID));
            SqlParameterCollection SP = new SqlCommand().Parameters;
            SP.AddWithValue("@ID", ID);
            ExecuteData.ExecData("update link set Hit=hit+1  WHERE linkid=@ID",SP);
			//SqlDataReader MyRead = ViewData.MyDR("SELECT Link.unitID,Link.LinkID, Link.Name, Link.Text, Link.Address, Link.ImgAddress, Link.Hit, LinkType.Name AS TypeName FROM Link INNER JOIN LinkType ON Link.LinkTypeID = LinkType.LinkTypeID  WHERE (Link.linkid = @ID) ", SP);
			TextDG.DataSource = ViewData.MyDT("SELECT Link.unitID,Link.LinkID, Link.Name, Link.Text, Link.Address, Link.ImgAddress, Link.Hit, LinkType.Name AS TypeName FROM Link INNER JOIN LinkType ON Link.LinkTypeID = LinkType.LinkTypeID  WHERE (Link.linkid = @ID) ", SP); //MyRead;
            TextDG.DataBind();
			//MyRead.Close(); MyRead.Dispose();
            //ViewData.MyConnection.Close();
        }
        void TitleDB(int MyType)
        {
            string TypeStr = "";
            if (MyType != 0)
                TypeStr = " AND (LinkType.linkTypeID =@TypeStr) ";

            SqlParameterCollection SP = new SqlCommand().Parameters;
            SP.AddWithValue("@TypeStr", MyType);
            SP.AddWithValue("@UnitID",Tools.Tools.GetViewUnitID);
            PagedDataSource pgitems = new PagedDataSource();
			DataView dv = new DataView(ViewData.MyDT("SELECT Link.unitID,Link.LinkID, Link.Name, Link.Text, Link.Address, Link.ImgAddress, Link.Hit, LinkType.Name AS TypeName FROM Link INNER JOIN LinkType ON Link.LinkTypeID = LinkType.LinkTypeID WHERE (link.UnitID = @UnitID) " + DAL.CheckData.NotLoginGuestFilterID(Tools.MyVar.SiteGuest.Links, "Link.unitID") + " " + TypeStr + " " + DAL.CheckData.NotLoginGuestFilterID(Tools.MyVar.SiteGuest.Links, "link.LinkID") + " ORDER BY link.LinkID DESC", SP));
            pgitems.DataSource = dv;
            pgitems.AllowPaging = true;
            pgitems.PageSize = 25;
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
     			object o = this.Session["_LinksPage"];
                if (o == null)
                    return 0;
                else
                    return (int)o;
            }
            set
            {
				this.Session["_LinksPage"] = value;
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
    }
}
