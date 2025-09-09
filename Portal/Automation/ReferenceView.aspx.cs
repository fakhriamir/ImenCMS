using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Portal.Automation
{
	public partial class ReferenceView : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Tools.Automation.CheckAccess("automation_NewLetter.aspx");
			if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim() == "")
			{
				Tools.Tools.Alert(Page, "لینک مورد نظر معتبر نمی باشد");
				return;
			}
			if (Tools.Tools.ConvertToInt32(Request.QueryString["ID"]) < 0)
			{
				Tools.Tools.Alert(Page, "لینک مورد نظر معتبر نمی باشد");
				return;
			}
			Tools.Automation.CheckUserLetterAccess(Request.QueryString["ID"]);
			if (!IsPostBack)
				UpdateDR();
		}
		private void UpdateDR()
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@officeletterid", Request.QueryString["ID"]);
			SP.AddWithValue("@unitid", Tools.Tools.GetViewUnitID);
			ViewDR.DataSource = DAL.ViewData.MyDT("SELECT officereferenceid,officeletterid,senderpersonalid,topersonalid,officepriorityid,paraph,perparaph,date,viewdate,enddate,unitid,type,officereferencetypeid FROM OfficeReference where officeletterid=@officeletterid and  unitid=@unitid order by officeletterid desc", SP);
			ViewDR.DataBind();
		}
		public string GetPerParaph(string Per, string UserID,string SenderUID)
		{
			if (UserID == DAL.CheckData.CheckTokenGuestUserID().ToString() || SenderUID == DAL.CheckData.CheckTokenGuestUserID().ToString())
				return "<br /><b>پاراف خصوصی: </b>" + Per;
			else
				return "";
		}
	}
}