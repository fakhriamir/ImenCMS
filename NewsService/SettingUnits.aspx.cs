using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
namespace NewsService
{
	public partial class SettingUnits : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (ADAL.A_CheckData.PageAccess(this.Page.ToString()))
				Response.Redirect("/Logins");		
			if (Request.QueryString["ID"] == null || Request.QueryString["ID"] == "")
				return;		
			int SettID;
			if (!int.TryParse(Request.QueryString["ID"], out SettID))
				return;			
			if (!IsPostBack)
			{
				Tools.Tools.FillLanguageDL(LanguageDL);
				
			}
			ViewDB(SettID);
		}
		void ViewDB(int SettingID)
		{
            ItemDR.DataSource = ADAL.A_ViewData.MyDT("SELECT SettingUnitNameTypeID, Name" + Tools.Tools.LangSTR + " as name, Sort FROM SettingUnitNameType ORDER BY Sort, SettingUnitNameTypeID");
            ItemDR.DataBind();
            
			FormPN.Controls.Clear();
            SqlDataReader MyRead = ADAL.A_ViewData.MyDR("SELECT SettingUnitNameID, Name" + Tools.Tools.LangSTR + ", '' as bb, '' as cc, TableName,ValueFeild, TextField, Condition,DIR,Description" + Tools.Tools.LangSTR + "  FROM SettingUnitName WHERE (Type = " + SettingID + ")");
			while (MyRead.Read())
			{
				Label MyLB = new Label();
				string CurentValue = ADAL.A_ExecuteData.GetValue("SELECT Value  FROM SettingUnit WHERE (SettingUnitNameID = " + Tools.MyCL.MGInt(MyRead, 0) + ") AND (UnitID = " + ADAL.A_CheckData.GetUnitID() + ") AND (LangID = " + LanguageDL.SelectedValue + ")");
				MyLB.Text = Tools.MyCL.MGInt(MyRead, 0)+"- "+Tools.MyCL.MGStr(MyRead, 1);
				FormPN.Controls.Add(new LiteralControl("<tr><td align=right>"));
				FormPN.Controls.Add(MyLB);
				FormPN.Controls.Add(new LiteralControl("</td><td align=right>"));
				if (Tools.MyCL.MGStr(MyRead, 4) != "" && Tools.MyCL.MGStr(MyRead, 5) != "" && Tools.MyCL.MGStr(MyRead, 6) != "")
				{
					DropDownList MyDL = new DropDownList();
					MyDL.ID = "MySetting" + Tools.MyCL.MGInt(MyRead, 0);
					MyDL.ClientIDMode = ClientIDMode.Static;
					Tools.Tools.FillDL(MyDL, Tools.MyCL.MGStr(MyRead, 4), Tools.MyCL.MGStr(MyRead, 5), Tools.MyCL.MGStr(MyRead, 6), Tools.MyCL.MGStr(MyRead, 7).Replace("#LangID#",LanguageDL.SelectedValue).Replace("#UnitID#", ADAL.A_CheckData.GetUnitID()), CurentValue);
					FormPN.Controls.Add(MyDL);
				}
				else
				{
					TextBox MyTB = new TextBox();
					MyTB.ID = "MySetting" + Tools.MyCL.MGInt(MyRead, 0);
					MyTB.ClientIDMode = ClientIDMode.Static;
                    MyTB.Attributes["dir"] = Tools.MyCL.MGStr(MyRead, 8);
					MyTB.Text = CurentValue;
					MyTB.MaxLength = 1024;
					FormPN.Controls.Add(MyTB);
				}
				string Description = Tools.MyCL.MGStr(MyRead, 9);
				if(Description=="")
					FormPN.Controls.Add(new LiteralControl("</td></tr>"));
				else
					FormPN.Controls.Add(new LiteralControl("("+Description+")</td></tr>"));
			}
			MyRead.Close();
			//FormPN.Controls.Add(new LiteralControl("</table>"));
		}
		protected void SaveBTN_Click(object sender, EventArgs e)
		{			
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SqlDataReader MyRead = ADAL.A_ViewData.MyDR("SELECT SettingUnitName.SettingUnitNameID, SettingUnitName.Name, SettingUnit.Value, SettingUnit.SettingUnitID  FROM SettingUnitName LEFT OUTER JOIN SettingUnit ON SettingUnitName.SettingUnitNameID = SettingUnit.SettingUnitNameID AND SettingUnit.UnitID = " + ADAL.A_CheckData.GetUnitID() + "  WHERE (SettingUnitName.Type = " + Request.QueryString["ID"] + ")");
			while (MyRead.Read())
			{
				string ParaVal = Request.Params["ctl00$Body$MySetting" + Tools.MyCL.MGInt(MyRead, 0)];
				//if (ParaVal.Trim() == "")
				//	continue;
				SP.AddWithValue("@MyVal", ParaVal);
				string CurentCount = ADAL.A_ExecuteData.CNTData("SELECT count(*)  FROM SettingUnit WHERE (SettingUnitNameID = " + Tools.MyCL.MGInt(MyRead, 0) + ") AND (UnitID = " + ADAL.A_CheckData.GetUnitID() + ") AND (LangID = " + LanguageDL.SelectedValue + ")");

				if (CurentCount != "0")//Update command
					ADAL.A_ExecuteData.AddData("UPDATE SettingUnit SET Value = @MyVal WHERE (SettingUnitID = " + Tools.MyCL.MGInt(MyRead, 3) + ") AND (LangID = " + LanguageDL.SelectedValue + ")", SP);
				else//insert comm
				{
					if (ADAL.A_CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Insert))
					{
						Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "AddAccessText").ToString(), "", true);
						return;
					}
					ADAL.A_ExecuteData.AddData("INSERT INTO SettingUnit (UnitID, SettingUnitNameID, Value,LangID) VALUES (" + ADAL.A_CheckData.GetUnitID() + "," + Tools.MyCL.MGInt(MyRead, 0) + ",@MyVal," + LanguageDL.SelectedValue + ")", SP);
				}
				SP.Clear();
			}
			MyRead.Close();
            Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "SuccessSave").ToString());
		}
		protected void LanguageDL_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Request.QueryString["ID"] == null || Request.QueryString["ID"] == "")
				return;
			int SettID;
			if (!int.TryParse(Request.QueryString["ID"], out SettID))
				return;
			ViewDB(SettID);
		}
	}
}