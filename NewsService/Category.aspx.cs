using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace NewsService
{
	public partial class MyCategory : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (ADAL.A_CheckData.PageAccess(this.Page.ToString()))
				Response.Redirect("/Logins");
			if (!IsPostBack)
				UpdateGrid();
			Admin.SettingID = 0;
		}
		void UpdateGrid()
		{
			ViewDR.DataSource = ADAL.A_ViewData.MyDR("SELECT CategoryID, Name, [Level], UnitID, Sort FROM Category where unitid=" + ADAL.A_CheckData.GetUnitID() + " order by [Level],Sort");
			ViewDR.DataBind();

            LeveLDL.DataSource = ADAL.A_ViewData.MyDR("select 'شاخه اصلی' as name, '' as  [LEVEL] union SELECT name, LTRIM(RTRIM([LEVEL])) FROM Category where unitid=" + ADAL.A_CheckData.GetUnitID());
			LeveLDL.DataBind();
			Tools.Tools.FillLanguageDL(LanguageDL);			
		}
		string GetNextLev(string CurLev)
		{
			int Myl = Tools.Tools.ConvertToInt32(CurLev.Substring(CurLev.Length - 2, 2));
			Myl = Myl + 1;
			string RetItem = Myl.ToString();
			if (RetItem.Length == 1)
				RetItem = "0" + RetItem;
			return RetItem;
		}
		protected void SaveBTN_Click(object sender, System.EventArgs e)
		{
			if (nameTB.Text.Trim() == "")
			{
				Tools.Tools.Alert(Page,GetGlobalResourceObject("resource", "FillStarItem").ToString());
				return;
			}
			string MyLev = "";
			SqlDataReader MyRead = ADAL.A_ViewData.MyDR("SELECT TOP (1) CategoryID, Name, [Level]  FROM Category  WHERE ([Level] LIKE '" + LeveLDL.SelectedValue.Trim() + "%') AND (LEN([Level]) = " + ((LeveLDL.SelectedValue.Trim().Length) + 2) + ") and unitid=" + ADAL.A_CheckData.GetUnitID() + "  ORDER BY [Level] DESC");
			if (MyRead.Read())
				MyLev = LeveLDL.SelectedValue.Trim() + GetNextLev(Tools.MyCL.MGStr(MyRead, 2));
			else
				MyLev = LeveLDL.SelectedValue.Trim() + "01";
			MyRead.Close(); MyRead.Dispose();			
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@name", nameTB.Text.Trim());
			SP.AddWithValue("@UnitID", ADAL.A_CheckData.GetUnitID());
			SP.AddWithValue("@level", MyLev);
			SP.AddWithValue("@LangID", LanguageDL.SelectedValue);
			if (ViewState["NewsCategoryS"] != null && ViewState["NewsCategoryS"].ToString().Trim() != "")
				ADAL.A_ExecuteData.AddData("update Category set LangID=@LangID, name = @name,level = @level where CategoryID=" + ViewState["NewsCategoryS"].ToString().Trim(), SP);
			else
			{
				if (ADAL.A_CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Insert))
				{
					Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "AddAccessText").ToString(), "", true);
					return;
				}
				SP.AddWithValue("@Ord", Tools.Tools.GetEndSortTable("Category", ""));
				ADAL.A_ExecuteData.AddData("INSERT INTO Category (name ,level ,sort,UnitID,LangID ) VALUES ( @name ,@level ,@Ord,@UnitID,@LangID)", SP);
			}
			ViewState["NewsCategoryS"] = null;
			UpdateGrid();
			SaveBTN.Text = GetGlobalResourceObject("resource", "SaveBTNText").ToString();
			nameTB.Text = "";
		}
		protected void ViewDR_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			string ComArg = e.CommandArgument.ToString();
			if (e.CommandName == "UP")
				Tools.Tools.SortFiled(Tools.Tools.MySortType.UP, ComArg, "Category", "CategoryID");
			else if (e.CommandName == "DOWN")
				Tools.Tools.SortFiled(Tools.Tools.MySortType.Down, ComArg, "Category", "CategoryID");
			else if (e.CommandName == "DEL")
			{
				if (ADAL.A_CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Del))
				{
					Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "DelAccessText").ToString(), "", true);
					return;
				}
				else
					ADAL.A_ExecuteData.DeleteData("delete from Category where CategoryID=" + ComArg);
			}
			else if (e.CommandName == "EDIT")
			{
				if (ADAL.A_CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Edit))
				{
					Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "EditAccessText").ToString(), "", true);
					return;
				}
                LeveLDL.DataSource = ADAL.A_ViewData.MyDR("select '" + GetGlobalResourceObject("resource", "MainRoot").ToString() + "' as name, '' as [LEVEL] union SELECT name, rtrim(ltrim([LEVEL])) FROM Category where unitid=" + ADAL.A_CheckData.GetUnitID());
				LeveLDL.DataBind();
				
				SqlDataReader MyRead = ADAL.A_ViewData.MyDR("select Categoryid,name,level from Category where CategoryID=" + ComArg);
				if (!MyRead.Read())
				{
					MyRead.Close(); MyRead.Dispose();
					return;
				}
				ViewState["NewsCategoryS"] = Tools.MyCL.MGInt(MyRead, 0).ToString().Trim();
				nameTB.Text = Tools.MyCL.MGStr(MyRead, 1);
				if (Tools.MyCL.MGStr(MyRead, 2).Length != 2)
					LeveLDL.SelectedValue = Tools.MyCL.MGStr(MyRead, 2).Trim().Substring(0, (Tools.MyCL.MGStr(MyRead, 2).Length) - 2);
				else
					LeveLDL.SelectedValue = "";
				SaveBTN.Text = GetGlobalResourceObject("resource", "SaveBTNEditText").ToString();
				MyRead.Close(); MyRead.Dispose();
			}
			UpdateGrid();
		}
	}
}