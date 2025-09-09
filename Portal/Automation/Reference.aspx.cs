using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Automation
{
	public partial class Reference : System.Web.UI.Page
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
				ViewDR();
		}

		private void ViewDR()
		{
			officeletteridTB.Text = Tools.Automation.GetLetterNo(Tools.Tools.ConvertToInt32(Request.QueryString["ID"]).ToString());
			Tools.Automation.FillSenderAccess(RecieverDL, DAL.CheckData.CheckTokenGuestUserID().ToString());
			PeriorityDL.DataSource = DAL.ViewData.MyDT("Select OfficePriorityID, Name from OfficePriority where unitid=" + Tools.Tools.GetViewUnitID + " order by sort");
			PeriorityDL.DataBind();
			OfficeReferenceTypeDL.DataSource = DAL.ViewData.MyDT("Select OfficeReferenceTypeID, Name from OfficeReferenceType  where unitid=" + Tools.Tools.GetViewUnitID + " order by sort");
			OfficeReferenceTypeDL.DataBind();
		}
		protected void SaveBTN_Click(object sender, EventArgs e)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@officeletterid",Tools.Tools.ConvertToInt32( Request.QueryString["ID"]));
			SP.AddWithValue("@senderpersonalid", DAL.CheckData.CheckTokenGuestUserID());
			SP.AddWithValue("@topersonalid", RecieverDL.SelectedValue);
			SP.AddWithValue("@officepriorityid", PeriorityDL.SelectedValue);
			SP.AddWithValue("@paraph", paraphTB.Text.Trim());
			SP.AddWithValue("@perparaph", perparaphTB.Text.Trim());
			SP.AddWithValue("@unitid", Tools.Tools.GetViewUnitID);
			SP.AddWithValue("@officereferencetypeid", OfficeReferenceTypeDL.SelectedValue);
			//DAL.ExecuteData.CNTData("SELECT COUNT(*) AS Expr1  FROM OfficeReference WHERE (SenderPersonalID = @senderpersonalid) AND (ToPersonalID = @topersonalid) AND (OfficeLetterID = @officeletterid)", SP);

			DAL.ExecuteData.AddData("INSERT INTO OfficeReference (officeletterid ,senderpersonalid ,topersonalid ,officepriorityid ,paraph ,perparaph,unitid ,officereferencetypeid ) VALUES ( @officeletterid ,@senderpersonalid ,@topersonalid ,@officepriorityid ,@paraph ,@perparaph ,@unitid ,@officereferencetypeid )", SP);
			Tools.Tools.Alert(Page, "ارجاع مورد نظر با موفقیت ثبت شد");
			perparaphTB.Text = "";
			paraphTB.Text = "";
		}
	}
}