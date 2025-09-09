using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Accounting
{
	public partial class Coding : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (Accounting.AccountingUserID == -1)
				Response.Redirect("/Accounting/Logins.aspx");
			Tools.Tools.SetPageSeo(Page, "Accounting/Company.aspx");
			Tools.Tools.SetPageHit(this.Page.ToString(), this.Page.ClientQueryString);
			if (!IsPostBack)
				UpdateGrid();
			
		}
		void UpdateGrid()
		{
			ViewDR.DataSource = DAL.ViewData.MyDT("SELECT AccountingCodingID, AccountingCompanyID, AccountingUserID, Name, Code, [Level] FROM AccountingCoding where AccountingCompanyID="+ Accounting.AccountingCompanyID +" and AccountingUserID=" + Accounting.AccountingUserID + " order by [Level]");
			ViewDR.DataBind();

			LeveLDL.DataSource = DAL.ViewData.MyDT("select 'شاخه اصلی' as name, '' as  [LEVEL] union SELECT name, LTRIM(RTRIM([LEVEL])) FROM AccountingCoding where AccountingCompanyID="+ Accounting.AccountingCompanyID +" and AccountingUserID="+ Accounting.AccountingUserID +" order by [Level]");
			LeveLDL.DataBind();
		}
		string GetNextLev(string CurLev)
		{
			int Myl = Tools.Tools.ConvertToInt32(CurLev.Substring(CurLev.Length - 2, 2));
			Myl = Myl + 1;
			string RetItem = Myl.ToString();
			if (RetItem.Length == 1)
				RetItem = "1" + RetItem;
			return RetItem;
		}
		protected void SaveBTN_Click(object sender, System.EventArgs e)
		{
			if (nameTB.Text.Trim() == "")
			{
				Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "FillStarItem").ToString());
				return;
			}
			string MyLev = "";
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT TOP (1) AccountingCodingID, Name, [Level]  FROM AccountingCoding  WHERE ([Level] LIKE '" + LeveLDL.SelectedValue.Trim() + "%') AND (LEN([Level]) = " + ((LeveLDL.SelectedValue.Trim().Length) + 2) + ") and AccountingCompanyID=" + Accounting.AccountingCompanyID + " and AccountingUserID=" + Accounting.AccountingUserID + "  ORDER BY [Level] DESC");
			if (MyRead.Read())
				MyLev = LeveLDL.SelectedValue.Trim() + GetNextLev(Tools.MyCL.MGStr(MyRead, 2));
			else
				MyLev = LeveLDL.SelectedValue.Trim() + "10";
			MyRead.Close(); MyRead.Dispose();

			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@name", nameTB.Text.Trim());
			SP.AddWithValue("@AccountingCompanyID", Accounting.AccountingCompanyID);
			SP.AddWithValue("@AccountingUserID", Accounting.AccountingUserID);
			if (CodeTB.Text.Trim() == "")
			{
				int code=0;
				string ParLev = LeveLDL.SelectedValue;
				if(ParLev=="")//شاخه اصلی
				{
					int id = DAL.ExecuteData.CNTData("SELECT TOP (1) Code  FROM AccountingCoding  WHERE AccountingCompanyID="+ Accounting.AccountingCompanyID +" and AccountingUserID=" + Accounting.AccountingUserID + " AND (LEN([Level]) = 2) ORDER BY Code DESC");
					if(id==0)
						code=10;
					else
						code = id+1;
				}
				else
				{
					int id = DAL.ExecuteData.CNTData("SELECT TOP (1) Code  FROM AccountingCoding  WHERE AccountingCompanyID=" + Accounting.AccountingCompanyID + " and AccountingUserID=" + Accounting.AccountingUserID + " AND [level] like '" + ParLev + "%' and (LEN([Level]) = " + ((ParLev.Length) +2).ToString() + ") ORDER BY Code DESC");
					if (id == 0)
						code = 10;
					else
						code = id + 1;
				}
				SP.AddWithValue("@Code", code);
			}
			else
			{
				SP.AddWithValue("@Code", CodeTB.Text);
				int RepCount = DAL.ExecuteData.CNTData("SELECT count(*)  FROM AccountingCoding  WHERE ([Level] LIKE '" + LeveLDL.SelectedValue.Trim() + "%') AND (LEN([Level]) = " + ((LeveLDL.SelectedValue.Trim().Length) + 2) + ") and AccountingCompanyID=" + Accounting.AccountingCompanyID + " and AccountingUserID=" + Accounting.AccountingUserID + " and code=@Code  ",SP);
				if(RepCount!=0)
				{
					Tools.Tools.Alert(Page,"کد وارد شده تکراری می باشد");
					return;
				}
				SP.AddWithValue("@Code", CodeTB.Text);
			}

			if (ViewState["AccountingCodingS"] != null && ViewState["AccountingCodingS"].ToString().Trim() != "")
			{
				
				string CurLev = DAL.ExecuteData.CNTDataStr("SELECT [Level]  FROM AccountingCoding WHERE (AccountingCodingID = " + ViewState["AccountingCodingS"].ToString() + ")");
				if(LeveLDL.SelectedValue==CurLev.Substring(0,CurLev.Length-1))
					SP.AddWithValue("@level", CurLev);
				DAL.ExecuteData.AddData("update AccountingCoding set AccountingUserID=@AccountingUserID,AccountingCompanyID=@AccountingCompanyID,name = @name,level = @level,Code=@Code where AccountingCodingID=" + ViewState["AccountingCodingS"].ToString().Trim(), SP);
			}
			else
			{
				SP.AddWithValue("@level", MyLev);
				DAL.ExecuteData.AddData("INSERT INTO AccountingCoding (name ,level ,code,AccountingCompanyID,AccountingUserID ) VALUES ( @name ,@level ,@Code,@AccountingCompanyID,@AccountingUserID)", SP);
			}
			ViewState["AccountingCodingS"] = null;
			UpdateGrid();
			nameTB.Text = "";
			CodeTB.Text = "";
		}
		public string GetParentCode(string CurID)
		{
			string CurLev = CurID ;//DAL.ExecuteData.CNTDataStr("SELECT [Level]  FROM AccountingCoding WHERE (AccountingCodingID = " + CurID + ")");
			string RetSrt = "";
			for (int i = 0; i < (CurLev.Length/2); i++)
			{
				RetSrt = RetSrt+ DAL.ExecuteData.CNTDataStr("SELECT code  FROM AccountingCoding WHERE ([Level] = '" + CurLev.Substring(0,((i+1)*2)) + "')");
			}
			return RetSrt;
		}
		protected void ViewDR_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			string ComArg = e.CommandArgument.ToString();
			//if (e.CommandName == "UP")
			//	Tools.Tools.SortFiled(Tools.Tools.MySortType.UP, ComArg, "NewsCategory", "NewsCategoryID");
			//else if (e.CommandName == "DOWN")
			//	Tools.Tools.SortFiled(Tools.Tools.MySortType.Down, ComArg, "NewsCategory", "NewsCategoryID");
			//else
				if (e.CommandName == "DEL")
			{

				DAL.ExecuteData.DeleteData("delete from AccountingCoding where AccountingCodingID=" + ComArg);
			}
			else if (e.CommandName == "EDIT")
			{

				LeveLDL.DataSource = DAL.ViewData.MyDT("select 'شاخه اصلی' as name, '' as  [LEVEL] union SELECT name, LTRIM(RTRIM([LEVEL])) FROM AccountingCoding where AccountingCompanyID=" + Accounting.AccountingCompanyID + " and AccountingUserID=" + Accounting.AccountingUserID + " order by [Level]");
				LeveLDL.DataBind();

				SqlDataReader MyRead = DAL.ViewData.MyDR1("select AccountingCodingID,name,level,Code from AccountingCoding where AccountingCodingID=" + ComArg);
				if (MyRead.Read())
				{


					ViewState["AccountingCodingS"] = Tools.MyCL.MGInt(MyRead, 0).ToString().Trim();
					nameTB.Text = Tools.MyCL.MGStr(MyRead, 1);
					if (Tools.MyCL.MGStr(MyRead, 2).Length != 2)
						LeveLDL.SelectedValue = Tools.MyCL.MGStr(MyRead, 2).Trim().Substring(0, (Tools.MyCL.MGStr(MyRead, 2).Length) - 2);
					else
						LeveLDL.SelectedValue = "";
					CodeTB.Text = Tools.MyCL.MGStr(MyRead, 3);
				}
				MyRead.Close(); MyRead.Dispose();
			}
			UpdateGrid();
		}
	}
}