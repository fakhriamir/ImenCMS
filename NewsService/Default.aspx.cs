using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
namespace NewsService
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//Response.Write(HttpContext.Current.Request.Cookies["LoginUserID"].Value + "lllllll<br>");
			//Response.Write(HttpContext.Current.Request.Cookies["LoginUnitID"].Value + "uuuuuuu");
			//return;
			if (ADAL.A_CheckData.GetUserID() == "")
			{
				Response.Redirect("/Logins");
			}
			if (ADAL.A_CheckData.PageAccess("default.aspx"))
			{
				//Response.Write("bbbbbbbbbbbbbbbbbbbbbbbbb");
				Response.Redirect("/Logins");
			}
			if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToLower() == "exit")
			{
				NewsService.Admin.MenuSTR = "";
				ViewState.Clear();
				Session.Abandon();
				if (HttpContext.Current.Request.Cookies["LoginUserID"] != null)
				{
					Tools.Tools.CookieRemove("LoginUserID");
					Response.Cookies.Remove("LoginUserID");
				}
				if (HttpContext.Current.Request.Cookies["LoginUnitID"] != null)
				{
					Tools.Tools.CookieRemove("LoginUnitID");
					Response.Cookies.Remove("LoginUnitID");
				}
				HttpCookie httpCookie1 = new HttpCookie("LoginUserID");
				httpCookie1.Value = Tools.Tools.MyEncry("-1");
				httpCookie1.Expires = DateTime.Now.AddHours(3);
				Response.SetCookie(httpCookie1);
				httpCookie1 = new HttpCookie("LoginUnitID");
				httpCookie1.Value = Tools.Tools.MyEncry("-1");
				httpCookie1.Expires = DateTime.Now.AddHours(3);
				Response.SetCookie(httpCookie1);
				Response.Redirect("/Logins");
			}
            
			if (UnitDL.Items.Count == 0)
			{
                string Leve = ADAL.A_ExecuteData.CNTData("SELECT [Level]  FROM Units  WHERE (UnitID = (SELECT UnitID FROM AdminUsers WHERE (UserID = " + ADAL.A_CheckData.GetUserID() + ")))").Trim();
				UnitDL.Items.Clear();
				SqlDataReader MyRead= ADAL.A_ViewData.MyDR("SELECT Name , UnitID,[level] FROM Units where [level] like '" + Leve + "%' order by [level]");
				while (MyRead.Read())
				{
					UnitDL.Items.Add(new ListItem(Tools.MyCL.MGInt(MyRead, 1).ToString()+"- "+Tools.Tools.GetLevelTab(Tools.MyCL.MGStr(MyRead, 2), "   ") + Tools.MyCL.MGStr(MyRead, 0), Tools.MyCL.MGInt(MyRead, 1).ToString()));
				}
				MyRead.Close(); MyRead.Dispose();
			}
			CreateFoolder();
        	if (!IsPostBack)
				UnitDL.SelectedValue = ADAL.A_CheckData.GetUnitID().ToString();

			
            if(!IsPostBack)
            {
                if (Request.Cookies["MyLanguage"] != null)
                {
                    string MyLang = Request.Cookies["MyLanguage"].Value;
                    Tools.Tools.SetDropDownListValue(LanguageDL, MyLang);
                }
            }
		}

		private void CreateFoolder()
		{
			string strRootFolder = ADAL.A_CheckData.GetFilesRoot();

			if (!Directory.Exists(strRootFolder + "\\"))
			{
				Directory.CreateDirectory(strRootFolder + "\\");
				if (!Directory.Exists(strRootFolder + "\\RandImages"))
					Directory.CreateDirectory(strRootFolder + "\\RandImages");
				if (!Directory.Exists(strRootFolder + "\\ArticlePic"))
					Directory.CreateDirectory(strRootFolder + "\\ArticlePic");
				if (!Directory.Exists(strRootFolder + "\\LinkPic"))
					Directory.CreateDirectory(strRootFolder + "\\LinkPic");
				if (!Directory.Exists(strRootFolder + "\\Sounds"))
					Directory.CreateDirectory(strRootFolder + "\\Sounds");
				if (!Directory.Exists(strRootFolder + "\\Movies"))
					Directory.CreateDirectory(strRootFolder + "\\Movies");
				if (!Directory.Exists(strRootFolder + "\\Software"))
					Directory.CreateDirectory(strRootFolder + "\\Software");
				if (!Directory.Exists(strRootFolder + "\\Templates"))
					Directory.CreateDirectory(strRootFolder + "\\Templates");
				if (!Directory.Exists(strRootFolder + "\\PersonalPic"))
					Directory.CreateDirectory(strRootFolder + "\\PersonalPic");
				if (!Directory.Exists(strRootFolder + "\\Images"))
					Directory.CreateDirectory(strRootFolder + "\\Images");
				if (!Directory.Exists(strRootFolder + "\\Images\\Advertise"))
					Directory.CreateDirectory(strRootFolder + "\\Images\\Advertise");
				if (!Directory.Exists(strRootFolder + "\\Images\\Gallery"))
					Directory.CreateDirectory(strRootFolder + "\\Images\\Gallery");
				if (!Directory.Exists(strRootFolder + "\\Images\\IconLink"))
					Directory.CreateDirectory(strRootFolder + "\\Images\\IconLink");
				if (!Directory.Exists(strRootFolder + "\\Images\\PhotoLink"))
					Directory.CreateDirectory(strRootFolder + "\\Images\\PhotoLink");
				if (!Directory.Exists(strRootFolder + "\\Images\\ProPic"))
					Directory.CreateDirectory(strRootFolder + "\\Images\\ProPic");
				if (!Directory.Exists(strRootFolder + "\\Images\\News"))
					Directory.CreateDirectory(strRootFolder + "\\Images\\News");
				if (!Directory.Exists(strRootFolder + "\\Images\\Forum"))
					Directory.CreateDirectory(strRootFolder + "\\Images\\Forum");
			}
		}
		protected void UnitDL_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ADAL.A_CheckData.GetAccessTypeID() == "1")
			{
				//ADAL.A_CheckData.GetUnitID() = UnitDL.SelectedValue;
				HttpCookie httpCookie1 = new HttpCookie("LoginUnitID", Tools.Tools.MyEncry(UnitDL.SelectedValue));
				httpCookie1.Expires = DateTime.Now.AddDays(3);
				HttpContext.Current.Response.Cookies.Add(httpCookie1);
			}
		}

        protected void LanguageDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            HttpCookie mylang = new HttpCookie("MyLanguage");
            mylang.Value = LanguageDL.SelectedValue;
            mylang.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(mylang);
            Response.Redirect("default.aspx");
        }
	}
}
