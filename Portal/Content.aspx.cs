using System; 
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
	public partial class Content : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
		{
			
			
			Tools.Tools.SetPageSeo(Page, "ContactUs.aspx");
			Tools.Tools.SetPageHit(this.Page.ToString(), this.Page.ClientQueryString);
		
			int MyTextID=0;
            if (int.TryParse(Request.QueryString["ID"], out MyTextID))
                Portal.Default.PageType.TextID = MyTextID;
            if (MyTextID!=0)
            {
				if(DAL.CheckData.CheckAccess(Tools.MyVar.SiteGuest.Page ,MyTextID))
					UDR(Tools.Tools.ConvertToInt32(Request.QueryString["ID"]).ToString());
				else
					Response.Redirect("/Members/MemberLogin.aspx");
            }
            else
            {              
                Response.Redirect("/");
            }
			Default.Adv.PageID = 9;
		
			if (Tools.Tools.GetSetting(53) == "1" || Tools.Tools.GetSetting(53) == "")
			{
				if(Pages.PlaceSTR ==null)
				Pages.PlaceSTR = new System.Collections.ArrayList();
				Pages.PlaceSTR.Add("PageType.ascx");
			}
			//else
			//    Pages.PlaceSTR.Clear();
        }
        void UDR(string ID)
        {
               int MyTextID=0;
               if (!int.TryParse(Request.QueryString["ID"], out MyTextID))
                   return;
			RatePH.Controls.Add(LoadControl("/Def/rate.ascx", (int)Tools.MyVar.SiteRate.Page, DAL.ViewData.SetRate((int)Tools.MyVar.SiteRate.Page, ID), ID));
            SqlParameterCollection SP = new SqlCommand().Parameters;
            SP.AddWithValue("@GID", ID);
            SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);
			TextDG.DataSource = DAL.ViewData.MyDT("SELECT Texts, Name FROM Texts WHERE (LangID =" + Tools.Tools.LangID + ") AND (TextID = @GID) and (UnitID=@UnitID) and (disable=0)", SP);
            TextDG.DataBind();

			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT PH0, PH1, PH2, PH3, PH4, PH5, PH6,PHL  FROM TextPH  WHERE (TextID = @GID) and (UnitID=@UnitID)", SP);
			if (MyRead.Read())
			{
				if (Tools.MyCL.MGInt(MyRead,0) != 0)//d0
					PH0.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 0)]));
				if (Tools.MyCL.MGInt(MyRead, 1) != 0)//d0
					PH1.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 1)]));
				if (Tools.MyCL.MGInt(MyRead, 2) != 0)//d0
					PH1.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 2)]));
				if (Tools.MyCL.MGInt(MyRead, 3) != 0)//d0
					PH3.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 3)]));
				if (Tools.MyCL.MGInt(MyRead, 4) != 0)//d0
					PH3.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 4)]));
				if (Tools.MyCL.MGInt(MyRead, 5) != 0)//d0
					PH5.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 5)]));
				if (Tools.MyCL.MGInt(MyRead, 6) != 0)//d0
					PH6.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead,6 )]));
				if (Tools.MyCL.MGInt(MyRead, 7) != 0)//d0
				{
					if (Pages.PlaceSTR == null) Pages.PlaceSTR = new System.Collections.ArrayList();
					Pages.PlaceSTR.Add(Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 7)]);
				}
			}
			MyRead.Close();
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
               // throw new MemberAccessException("The requested constructor was not found on : " + ctl.GetType().BaseType.ToString());
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
