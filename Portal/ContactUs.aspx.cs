using System; using DAL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
    public partial class ContactUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			
			Tools.Tools.CheckNotEmpty(HttpContext.GetGlobalResourceObject("resource", "Email").ToString(), "", "", true);
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim() != "")
            {

                TextDB(Tools.Tools.ConvertToInt32(Request.QueryString["ID"]).ToString());
                ItemMV.SetActiveView(TextItem);
            }
            else
            {
                ItemMV.SetActiveView(TitleTop);
                TitleDB();
               
			Tools.Tools.SetPageSeo(Page, "ContactUs.aspx");
			
                //ViewData.MyConnection.Close();
            }
			Tools.Tools.SetPageHit(this.Page.ToString(), this.Page.ClientQueryString);
		
			Default.Adv.PageID = 4;
        }
        void TextDB(string ID)
        {
            System.Data.SqlClient.SqlParameterCollection SP = new System.Data.SqlClient.SqlCommand().Parameters;
            SP.AddWithValue("@ID", ID);
			ContactDG.DataSource = ViewData.MyDT("SELECT * FROM contacts WHERE (contactid= @ID )", SP);
            ContactDG.DataBind();
        }
        void TitleDB()
        {
            PagedDataSource pgitems = new PagedDataSource();
            System.Data.SqlClient.SqlParameterCollection SP = new System.Data.SqlClient.SqlCommand().Parameters;
            SP.AddWithValue("@UnitID",Tools.Tools.GetViewUnitID);
            DataView dv = new DataView(ViewData.MyDT("SELECT * FROM contacts WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=@UnitID)  ORDER BY sort",SP));
            pgitems.DataSource = dv;
            pgitems.AllowPaging = true;
            pgitems.PageSize = 15;
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
		protected void rptPages_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			try
			{
				CurrentPage = (Tools.Tools.ConvertToInt32(e.CommandArgument) - 1);
			}
			catch { }
			if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToLower().IndexOf("type") != -1)
				TitleDB();
			else
				TitleDB();
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
