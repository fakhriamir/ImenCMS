using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsService
{
	public partial class Admin : System.Web.UI.MasterPage
	{
		public string MyFrame = "home.htm", MyVal = "",HelpAddress="", ToolTip = "";
		public static string MenuSTR = "", MyBackHref = "";
		public static int SettingID = 0;
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (ADAL.A_CheckData.GetUserID() == "")
				return;
			if (MyBackHref != "")
			{
				BackHref.HRef = MyBackHref;
				MyBackHref = "";
				BackIMG.Visible = true;
			}
			if (SettingID != 0)
			{
				SettingHref.HRef = "/SettingUnits.aspx?ID="+SettingID;
				SettingID = 0;
                
				SettingIMG.Visible = true;
			}
            PageCSS.Href = "/page" + Tools.Tools.LangSTR + ".css";
		    HelpAddress = "";
			if (File.Exists(Server.MapPath("help") + "\\" + this.Page.ToString().ToLower().Replace("asp.", "").Replace("_aspx", ".htm")))
			{
				HelpAddress = "/help/" + this.Page.ToString().ToLower().Replace("asp.", "").Replace("_aspx", ".htm");
			}
			//if (MenuSTR != "")
			//	return;
			int MenuID = 0;
			MenuSTR = "";
			SqlDataReader MyRead = ADAL.A_ViewData.MyDR("SELECT Menu.MenuStr" + Tools.Tools.GetLangName + ", MenuChilds.ChildStr" + Tools.Tools.GetLangName + ", MenuChilds.ChildHref, Menu.MenuID,Menu.pic FROM Menu INNER JOIN MenuChilds ON Menu.MenuID = MenuChilds.MenuID INNER JOIN Access INNER JOIN AdminUsers ON Access.AccessTypeID = AdminUsers.AccesstypeID ON MenuChilds.MenuChildID = Access.PageID WHERE (MenuChilds.MenuID <> - 1) AND (AdminUsers.UserID =" + ADAL.A_CheckData.GetUserID() + ") ORDER BY Menu.[Order], Menu.MenuID,MenuChilds.sort");
			while (MyRead.Read())
			{
				if (MenuID != Tools.MyCL.MGInt(MyRead, 3))
				{
					MenuSTR += "</ul><h3><img src=\"/Imgs/arrow_menu"+Tools.Tools.LangSTR+".png\"> " + Tools.MyCL.MGStr(MyRead, 0) + "</h3><ul>";
					
					//MenuSTR += "[\"" + Tools.MyCL.MGStr(MyRead, 0).Replace("'", "&#39;") + "\", \"\", \"" + Tools.MyCL.MGStr(MyRead, 4).Replace("'", "&#39;").Replace("\"", "") + "\", \"\", \"\", \"" + Tools.MyCL.MGStr(MyRead, 0).Replace("'", "&#39;").Replace("\"", "") + "\", , \"0\"],";
					MenuID = Tools.MyCL.MGInt(MyRead, 3);
				}
				MenuSTR += "<li><a href=\"/" + Tools.MyCL.MGStr(MyRead, 2) + "\">" + Tools.MyCL.MGStr(MyRead, 1) + "</a></li>";
			}
			MenuSTR = "<ul>"+MenuSTR.Substring(5)+"</ul>";
			MyRead.Close(); MyRead.Dispose();
		
		}
	}
}