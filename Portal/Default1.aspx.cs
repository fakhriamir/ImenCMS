using System; using DAL;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
namespace Portal
{
	public partial class Default_1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Tools.Tools.SetPageHit(this.Page.ToString(), this.Page.ClientQueryString);
			System.Web.HttpBrowserCapabilities browser = Request.Browser;
			if (browser.Browser.ToLower() == "ie" && Convert.ToInt64(browser.MajorVersion) <= 6)
				Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "", string.Format("alert('{0}');", "نسخه جدید سايت از مرورگر شما پشتیبانی نمی کند شما می توانید از فایرفاکس یا مروگر اکپلورر ورژن 6 به بعد استفاده نمایید"), true);

			SqlDataReader MyRead;
			System.Data.SqlClient.SqlParameterCollection SP = new System.Data.SqlClient.SqlCommand().Parameters;
			Default.Adv.PageID = 1;

			string MyDefaultType = "-6";
			if (Global.ASCXPages == null)
				LoadDefaultItems();
			else if (Global.ASCXPages.Length == 0)
				LoadDefaultItems();
			else if (Global.ASCXPages[5].Trim() == "")
				LoadDefaultItems();
			SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);
			//try
			//{
			//    int GDT = -1;
			//    int.TryParse(MyDefaultType, out GDT);
			//    if (GDT == -1)
			//    {
			//        string US = "-6";
			//        if (US.Trim() == "")
			//        {
			//            US = "0";
			//            Logging.ErrorLog(sender.ToString(), "MyDef555---" + Tools.Tools.GetViewUnitID + "---", Server.GetLastError().Message + "_" + Server.GetLastError().Source + "_" + Server.GetLastError().Data + "_" + Server.GetLastError().InnerException + "_" + Server.GetLastError().StackTrace);
			//        }
			//        MyDefaultType = US;
			//    }
			//}
			//catch
			//{
			//    Logging.ErrorLog(sender.ToString(), "MyDef88---" + MyDefaultType + "---", Server.GetLastError().Message + "_" + Server.GetLastError().Source + "_" + Server.GetLastError().Data + "_" + Server.GetLastError().InnerException + "_" + Server.GetLastError().StackTrace);
			//}
			if (Tools.Tools.ConvertToInt32(MyDefaultType) < 0)
			{
				string CTemplate = "";
				MyRead = ViewData.MyDR1("SELECT Template FROM DefaultUnitCustom WHERE (DefaultUnitCustomID =" + (Tools.Tools.ConvertToInt32(MyDefaultType) * -1) + ")", null);
				if (MyRead.Read())
				{
					CTemplate = Tools.MyCL.MGStr(MyRead, 0);
				}
				MyRead.Close();
				if (CTemplate == "")
					return;
				//Panel panel = new Panel(); 
				while (CTemplate != "")
				{
					int stind = CTemplate.IndexOf("#");
					if (stind == -1)
					{
						CustomPH.Controls.Add(new LiteralControl(CTemplate));
						CTemplate = "";
						continue;
					}
					CustomPH.Controls.Add(new LiteralControl(CTemplate.Substring(0, stind)));
					CTemplate = CTemplate.Substring(stind + 1);
					int enind = CTemplate.IndexOf("#");
					string ASCXName = CTemplate.Substring(0, enind).Replace("#", "");
					CustomPH.Controls.Add(LoadControl("Def/" + ASCXName));
					CTemplate = CTemplate.Substring(enind + 1);
				}
			}
			else
			{
				//Response.Write("2" + CheckData.GetUnitSetting("DefType", Tools.Tools.GetViewUnitID) + "2");
				MyDefaultType = Tools.Tools.GetSetting(368, ""); 
				if (MyDefaultType == "")
					MyDefaultType = CheckData.GetUnitSetting("DefType", Tools.Tools.GetViewUnitID); 
				MyRead = ViewData.MyDR1("SELECT D1, D21, D22, D23, D311, D312, D313, D314, D315, D316, D321, D322, D323, D324, D325, D326, D331, D332, D333, D334, D335, D336, D41, D42,D12,D13,D14,D15  FROM DefaultUnit WHERE (UnitID = @UnitID) and type=" + MyDefaultType, SP);
				if (MyRead.Read())
				{
					if (MyDefaultType != "3")
					{
						//if (Tools.MyCL.MGInt(MyRead, 0) != 0)//d0
						//    DL12.Controls.Add(LoadControl("Default/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead,0)]));
						if (Tools.MyCL.MGInt(MyRead, 24) != 0)//d12
							DL12.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 24)]));
						//string MyDefType=CheckData.GetUnitSetting("DefType",Tools.Tools.GetViewUnitID);
						if (Tools.MyCL.MGInt(MyRead, 25) != 0)//d13
							DL12.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 25)]));
						if (Tools.MyCL.MGInt(MyRead, 26) != 0)//d14
							DL12.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 26)]));
						if (Tools.MyCL.MGInt(MyRead, 27) != 0 && MyDefaultType != "0")//d15
							DL12.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 27)]));
						if (Tools.MyCL.MGInt(MyRead, 1) != 0)//d21
						{
							if (MyDefaultType == "0")
								DL21.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 1)]));
							else if (MyDefaultType == "1")
								DL21A1.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 1)]));
							else if (MyDefaultType == "2")
								DL21A2.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 1)]));
						}
						if (Tools.MyCL.MGInt(MyRead, 2) != 0)//d22
						{
							if (MyDefaultType == "0")
								DL22.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 2)]));
							else if (MyDefaultType == "2")
								DL22A2.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 2)]));
						}
						if (Tools.MyCL.MGInt(MyRead, 3) != 0)//d23
						{
							if (MyDefaultType == "0")
								DL23.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 3)]));
							else if (MyDefaultType == "1")
								DL23A1.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 3)]));
							//else if (MyDefType == "2")
							//  DL23a.Controls.Add(LoadControl("Default/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 3)]));
						}
						if (Tools.MyCL.MGInt(MyRead, 4) != 0)//d311
							DL311.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 4)]));
						if (Tools.MyCL.MGInt(MyRead, 5) != 0)//d312
							DL311.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 5)]));
						if (Tools.MyCL.MGInt(MyRead, 6) != 0)//d313
							DL311.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 6)]));
						if (Tools.MyCL.MGInt(MyRead, 7) != 0)//d314
							DL311.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 7)]));
						if (Tools.MyCL.MGInt(MyRead, 8) != 0)//d315
							DL311.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 8)]));
						if (Tools.MyCL.MGInt(MyRead, 9) != 0 && MyDefaultType != "0")//d316
							DL311.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 9)]));
						if (Tools.MyCL.MGInt(MyRead, 10) != 0)//d321
							DL321.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 10)]));
						if (Tools.MyCL.MGInt(MyRead, 11) != 0)//d322
							DL321.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 11)]));
						if (Tools.MyCL.MGInt(MyRead, 12) != 0)//d323
							DL321.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 12)]));
						if (Tools.MyCL.MGInt(MyRead, 13) != 0)//d324
							DL321.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 13)]));
						if (Tools.MyCL.MGInt(MyRead, 14) != 0)//d325
							DL321.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 14)]));
						if (Tools.MyCL.MGInt(MyRead, 15) != 0 && MyDefaultType != "0")//d326
							DL321.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 15)]));
						if (Tools.MyCL.MGInt(MyRead, 16) != 0)//d331
							DL331.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 16)]));
						if (Tools.MyCL.MGInt(MyRead, 17) != 0)//d332
							DL331.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 17)]));
						if (Tools.MyCL.MGInt(MyRead, 18) != 0)//d333
							DL331.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 18)]));
						if (Tools.MyCL.MGInt(MyRead, 19) != 0)//d334
							DL331.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 19)]));
						if (Tools.MyCL.MGInt(MyRead, 20) != 0)//d335
							DL331.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 20)]));
						if (Tools.MyCL.MGInt(MyRead, 21) != 0 && MyDefaultType != "0")//d336
							DL331.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 21)]));
					}
					else if (MyDefaultType == "3")
					{
						if (Tools.MyCL.MGInt(MyRead, 24) != 0)//d12
							D3_12.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 24)]));
						if (Tools.MyCL.MGInt(MyRead, 2) != 0)//d22
							D3_22.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 2)]));
						if (Tools.MyCL.MGInt(MyRead, 21) != 0)//d336
							D3_336.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 21)]));
						if (Tools.MyCL.MGInt(MyRead, 15) != 0)//d326
							D3_336.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 15)]));
						if (Tools.MyCL.MGInt(MyRead, 9) != 0)//d316
							D3_336.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 9)]));
						if (Tools.MyCL.MGInt(MyRead, 1) != 0)//d21
							D3_336.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 1)]));
						if (Tools.MyCL.MGInt(MyRead, 4) != 0)//d311
							D3_311.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 4)]));
						if (Tools.MyCL.MGInt(MyRead, 5) != 0)//d312
							D3_311.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 5)]));
						if (Tools.MyCL.MGInt(MyRead, 6) != 0)//d313
							D3_311.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 6)]));
						if (Tools.MyCL.MGInt(MyRead, 7) != 0)//d314
							D3_311.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 7)]));
						if (Tools.MyCL.MGInt(MyRead, 8) != 0)//d315
							D3_315.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 8)]));
						if (Tools.MyCL.MGInt(MyRead, 10) != 0)//d321
							D3_321.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 10)]));
						if (Tools.MyCL.MGInt(MyRead, 11) != 0)//d322
							D3_321.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 11)]));
						if (Tools.MyCL.MGInt(MyRead, 12) != 0)//d323
							D3_321.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 12)]));
						if (Tools.MyCL.MGInt(MyRead, 13) != 0)//d324
							D3_321.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 13)]));
						if (Tools.MyCL.MGInt(MyRead, 14) != 0)//d325
							D3_321.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 14)]));
						if (Tools.MyCL.MGInt(MyRead, 16) != 0)//d331
							D3_331.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 16)]));
						if (Tools.MyCL.MGInt(MyRead, 17) != 0)//d332
							D3_331.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 17)]));
						if (Tools.MyCL.MGInt(MyRead, 18) != 0)//d333
							D3_331.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 18)]));
						if (Tools.MyCL.MGInt(MyRead, 19) != 0)//d334
							D3_331.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 19)]));
						if (Tools.MyCL.MGInt(MyRead, 20) != 0)//d335
							D3_331.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 20)]));
						if (Tools.MyCL.MGInt(MyRead, 22) != 0)//d41
							D3_41.Controls.Add(LoadControl("/Def/" + Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 22)]));
					}
				}
			}
			MyRead.Close(); MyRead.Dispose();
			MyRead = ViewData.MyDR1("SELECT Title, Keyword, Description FROM MetaKey  WHERE (UnitID = " + Tools.Tools.GetViewUnitID + ") AND (PageName = 'default.aspx') AND (LangID = " + Tools.Tools.LangID + ")", null);
			if (MyRead.Read())
			{
				Tools.Tools.SetTitle(this, Tools.MyCL.MGStr(MyRead, 0), true);
				Tools.Tools.SetMetaTag(Page, Tools.Tools.MetaTags.Keywords, Tools.MyCL.MGStr(MyRead, 1));
				Tools.Tools.SetMetaTag(Page, Tools.Tools.MetaTags.Description, Tools.MyCL.MGStr(MyRead, 2));
			}
			MyRead.Close(); MyRead.Dispose();
		}
		private void LoadDefaultItems()
		{
			Global.ASCXPages = new string[250];
			SqlDataReader MyRead = ViewData.MyDR1("SELECT PageName, DefaultItemID FROM DefaultItem ORDER BY DefaultItemID", null);
			while (MyRead.Read())
			{
				Global.ASCXPages[Tools.MyCL.MGInt(MyRead, 1)] = Tools.MyCL.MGStr(MyRead, 0);
			}
			MyRead.Close(); MyRead.Dispose();
		}
	}
}