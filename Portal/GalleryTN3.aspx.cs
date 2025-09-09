using System;
using DAL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
    public partial class GalleryTN3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Tools.Tools.SetPageHit(this.Page.ToString(), this.Page.ClientQueryString);
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim() != "")
            {
                if (Request.QueryString["ID"].ToLower().IndexOf("type") == -1)
                {
                    if (Tools.Tools.ConvertToInt32(Request.QueryString["ID"]) > 1000000)
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
                SqlDataReader MyRead = ViewData.MyDR1("SELECT Title, Keyword, Description FROM MetaKey  WHERE (UnitID = " + Tools.Tools.GetViewUnitID + ") AND (PageName = 'gallery.aspx') AND (LangID = " + Tools.Tools.LangID + ")", null);
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
            if (MyType != 0)
                TypeStr = " AND (Gallery.GalleryTypeID = @TypeStr) ";

            SqlParameterCollection SP = new SqlCommand().Parameters;
            SP.AddWithValue("@TypeStr", MyType);
            SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);
            TitleDR.DataSource =ViewData.MyDT("SELECT Gallery.GalleryID, Gallery.Name, Gallery.name,Gallery.[desc], Gallery.GalleryTypeID, Gallery.UnitID, Gallery.Hit, GalleryType.Name AS Gallerywaretype FROM Gallery INNER JOIN GalleryType ON Gallery.GalleryTypeID = GalleryType.GalleryTypeID WHERE (Gallery.UnitID = @UnitID) and (Gallery.disable=0)  " + TypeStr + " ORDER BY Gallery.GalleryID DESC", SP);
            TitleDR.DataBind();
        }
        protected void lnkNextPage_Click(object sender, EventArgs e)
        {
            CurrentPage = CurrentPage + 1;
            if (Request.QueryString["ID"] == null || Request.QueryString["ID"].ToLower().IndexOf("type") == -1)
                TitleDB(0);
            else
                TitleDB(Tools.Tools.ConvertToInt32(Request.QueryString["ID"].ToLower().Replace("type", "")));
        }
        protected void lnkPreviousPage_Click(object sender, EventArgs e)
        {
            CurrentPage = CurrentPage - 1;
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