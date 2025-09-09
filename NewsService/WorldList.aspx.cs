using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
namespace NewsService
{
	public partial class WorldList : System.Web.UI.Page
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
			ViewDR.DataSource = ADAL.A_ViewData.MyDR("SELECT CategoryWord.CategoryWordID, CategoryWord.CategoryID, CategoryWord.Word, CategoryWord.CNT, CategoryWord.Weight, Category.Name  FROM CategoryWord INNER JOIN Category ON CategoryWord.CategoryID = Category.CategoryID  ORDER BY CategoryWord.CategoryID"); 
			ViewDR.DataBind();	
			
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
					ADAL.A_ExecuteData.DeleteData("delete from CategoryWord where CategoryWordID=" + ComArg);
			
			}
			else if (e.CommandName == "EDIT")
			{
				/*return;
				if(ADAL.A_CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Edit))
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
				
				MyRead.Close(); MyRead.Dispose();*/
			}
			UpdateGrid();
		}
	}
}