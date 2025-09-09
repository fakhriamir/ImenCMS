using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
namespace NewsService
{
	public partial class UserAdmin : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (ADAL.A_CheckData.PageAccess(this.Page.ToString()))
				Response.Redirect("/Logins");

			
			if (!IsPostBack)
				UpdateGrid();
		}
		void UpdateGrid()
		{
			string Leve = ADAL.A_ExecuteData.CNTData("SELECT [Level]  FROM Units  WHERE (UnitID = (SELECT UnitID FROM AdminUsers WHERE (UserID = " + ADAL.A_CheckData.GetUserID() + ")))").Trim();
			ViewDR.DataSource = ADAL.A_ViewData.MyDR("SELECT AdminUsers.UserID, AdminUsers.Name, AdminUsers.UserName, AdminUsers.MobNo, AdminUsers.AccesstypeID, AdminUsers.Disable, AccessTypes.Name AS typename, Units.Name AS unitname FROM AdminUsers INNER JOIN AccessTypes ON AdminUsers.AccesstypeID = AccessTypes.AccessTypeID INNER JOIN Units ON AdminUsers.UnitID = Units.UnitID WHERE     (Units.[Level] LIKE '" + Leve + "%') order by AdminUsers.UserID");
			ViewDR.DataBind();
			
			string WhereComm = "WHERE (AccessTypeID <> 1) and unitid="+ADAL.A_CheckData.GetUnitID();
			if (ADAL.A_ExecuteData.CNTData("SELECT AccesstypeID FROM AdminUsers WHERE (UserID = " + ADAL.A_CheckData.GetUserID() + ")") == "1")
				WhereComm = "";

			AccessDL.DataSource = ADAL.A_ViewData.MyDR("SELECT AccessTypeID, Name FROM AccessTypes " + WhereComm);
			AccessDL.DataBind();
			
			UnitDL.DataSource = ADAL.A_ViewData.MyDR("SELECT name, unitid FROM Units where [level] like '" + Leve + "%'");
			UnitDL.DataBind();
			
		}
		protected void SaveBTN_Click1(object sender, EventArgs e)
		{
			if (nameTB.Text.Trim() == "" || usernameTB.Text.Trim() == "" || passTB.Text.Trim() == "" || mobnoTB.Text.Trim() == "")
			{
				Tools.Tools.Alert(Page,GetGlobalResourceObject("resource", "FillStarItem").ToString());
				return;
			}
			
			if (passTB.Text.Length <= 5)
			{
                Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "Character5Msg").ToString());
				return;
			}
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@name", nameTB.Text);
			SP.AddWithValue("@AccesstypeID", AccessDL.SelectedValue);
			SP.AddWithValue("@username", usernameTB.Text.Trim());
			SP.AddWithValue("@pass", Tools.Tools.MyEncry(passTB.Text.Trim()));
			SP.AddWithValue("@access", Tools.Tools.ConvertToInt32(AccessDL.SelectedValue));
			SP.AddWithValue("@mobno", mobnoTB.Text.Trim());
			SP.AddWithValue("@disable", Tools.Tools.ConvertToInt32(disableDL.SelectedValue));
			SP.AddWithValue("@unitid", UnitDL.SelectedValue);
			if (ViewState["LoginsS"] != null && ViewState["LoginsS"].ToString().Trim() != "")
			{
				//if (passTB.Text == "****")
				//	ADAL.A_ExecuteData.AddData("update AdminUsers set name = @name,username = @username,AccesstypeID = @access,mobno = @mobno,unitid = @unitid,disable = @disable where UserID=" + ViewState["LoginsS"].ToString().Trim(), SP);
				//else
					ADAL.A_ExecuteData.AddData("update AdminUsers set name = @name,username = @username,AccesstypeID = @access,mobno = @mobno,unitid = @unitid,disable = @disable where UserID=" + ViewState["LoginsS"].ToString().Trim(), SP);
			}
			else
			{
				if (ADAL.A_CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Insert))
				{
					Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "AddAccessText").ToString(), "", true);
					return;
				}
				if (ADAL.A_ExecuteData.CNTDataint("SELECT COUNT(*) FROM AdminUsers WHERE (UserName = @username)", SP) != 0)
				{
					Tools.Tools.Alert(Page, "نام کاربر وارد شده تکراری می باشد", "", true);
					return;
				}
				ADAL.A_ExecuteData.AddData("INSERT INTO AdminUsers (name ,username ,pass ,AccesstypeID ,mobno ,disable,unitid ) VALUES ( @name ,@username ,@pass ,@access ,@mobno ,@disable,@unitid )", SP);
			}
			ViewState["LoginsS"] = null;
			UpdateGrid();
			SaveBTN.Text = GetGlobalResourceObject("resource", "SaveBTNText").ToString();
            Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "SuccessSave").ToString());
			nameTB.Text = "";
			usernameTB.Text = "";
			passTB.Text = "";
			AccessDL.SelectedIndex = 0;
			mobnoTB.Text = "";
			passTB.Enabled = true;
		}
		public static string GetUserState(bool ST)
		{
			if (ST)
                return Tools.Tools.MyGetGlobalResourceObject("resource", "InActive").ToString();
            return Tools.Tools.MyGetGlobalResourceObject("resource", "Active").ToString();
		}
		protected void ViewDR_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			string ComArg = e.CommandArgument.ToString();
			if (e.CommandName == "UP")
			{
				//Tools.Tools.SortFiled(Tools.Tools.MySortType.UP, ComArg, "Contacts", "ContactID");
			}
			else if (e.CommandName == "DOWN")
			{
				//Tools.Tools.SortFiled(Tools.Tools.MySortType.Down, ComArg, "Contacts", "ContactID");
			}
			else if (e.CommandName == "DEL")
			{
				if (ADAL.A_CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Del))
				{
					Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "DelAccessText").ToString(), "", true);
					return;
				}
				else
					ADAL.A_ExecuteData.DeleteData("delete from AdminUsers where UserID=" + ComArg);
			
			}
			else if (e.CommandName == "EDIT")
			{
				if (ADAL.A_CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Edit))
				{
					Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "EditAccessText").ToString(), "", true);
					return;
				}
				SqlDataReader MyRead = ADAL.A_ViewData.MyDR("select UserID,name,username,pass,AccesstypeID,mobno,disable from AdminUsers where UserID=" + ComArg);
				if (!MyRead.Read())
				{
					MyRead.Close(); MyRead.Dispose();
					return;
				}
				ViewState["LoginsS"] = Tools.MyCL.MGInt(MyRead, 0).ToString().Trim();
				nameTB.Text = Tools.MyCL.MGStr(MyRead, 1);
				usernameTB.Text = Tools.MyCL.MGStr(MyRead, 2);
				passTB.Text =Tools.Tools.MyDecry(Tools.MyCL.MGStr(MyRead, 3));
				passTB.Enabled = false;
				Tools.Tools.SetDropDownListValue(AccessDL,Tools.MyCL.MGInt(MyRead, 4).ToString());
				mobnoTB.Text = Tools.MyCL.MGStr(MyRead, 5);
				disableDL.SelectedValue = Tools.MyCL.MGInt(MyRead, 6).ToString();

				SaveBTN.Text = GetGlobalResourceObject("resource", "SaveBTNEditText").ToString();
				MyRead.Close(); MyRead.Dispose();
			}
			UpdateGrid();
		}
	}
}