using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace Portal.Automation
{
	public partial class ViewLetter : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Tools.Automation.CheckAccess("automation_ViewLetter.aspx");
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

			if (Request.QueryString["RefID"] != null && Request.QueryString["RefID"].Trim() != "")
			{
				SqlParameterCollection SP = new SqlCommand().Parameters;
				SP.AddWithValue("@OfficeReferenceID", Tools.Tools.ConvertToInt32(Request.QueryString["RefID"]));
				DAL.ExecuteData.AddData("UPDATE OfficeReference  SET ViewDate = GetDATE() WHERE (OfficeReferenceID = @OfficeReferenceID)",SP);
			}
			if (!IsPostBack)
				UpdateDR();
		}
		private void UpdateDR()
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;			
			SP.AddWithValue("@officeletterid", Tools.Tools.ConvertToInt32(Request.QueryString["ID"]));
			SP.AddWithValue("@unitid", Tools.Tools.GetViewUnitID);
			ViewDR.DataSource = DAL.ViewData.MyDT("SELECT OfficeLetter.OfficeLetterID, OfficeLetter.Body, OfficeLetter.BodyIMG, OfficeLetter.Comm, OfficeLetter.Date, OfficeLetter.SignerPersonalID, OfficeLetter.SupplierPersonalID, OfficeLetter.SenderPersonalID, GuestInfo.Name + ' ' + GuestInfo.Family AS SignerName, GuestInfo_1.Name + ' ' + GuestInfo_1.Family AS SenderName, OfficeLetter.Type FROM OfficeLetter INNER JOIN GuestInfo ON OfficeLetter.SignerPersonalID = GuestInfo.GuestID INNER JOIN GuestInfo AS GuestInfo_1 ON OfficeLetter.SenderPersonalID = GuestInfo_1.GuestID WHERE (OfficeLetter.OfficeLetterID=@officeletterid) AND (OfficeLetter.UnitID =@unitid)",SP);
			ViewDR.DataBind();
			ReferenceDR.DataSource = DAL.ViewData.MyDT("SELECT OfficeReference.OfficeReferenceID, OfficeReference.OfficePriorityID, OfficeReference.Paraph, OfficeReference.PerParaph, OfficeReference.Date, OfficePriority.Name AS OfficePriorityName, OfficeReferenceType.Name AS OfficeReferenceTypeName, OfficeReference.ToPersonalID, GuestInfo.Name + ' ' + GuestInfo.Family AS ToName, GuestInfo_1.Name + ' ' + GuestInfo_1.Family AS SenderName FROM OfficeReference INNER JOIN GuestInfo ON OfficeReference.ToPersonalID = GuestInfo.GuestID INNER JOIN GuestInfo AS GuestInfo_1 ON OfficeReference.SenderPersonalID = GuestInfo_1.GuestID INNER JOIN OfficeReferenceType ON OfficeReference.OfficeReferenceTypeID = OfficeReferenceType.OfficeReferenceTypeID INNER JOIN OfficePriority ON OfficeReference.OfficePriorityID = OfficePriority.OfficePriorityID WHERE (OfficeReference.OfficeLetterID = @officeletterid) AND (OfficeReference.UnitID = " + Tools.Tools.GetViewUnitID + ") order by OfficeReference.OfficeReferenceid desc", SP);
			ReferenceDR.DataBind();
		}
		public string GetBodyDisplay(Object InText)
		{
			if (InText == null)
				return "none";
			if ( InText.ToString().Trim()=="")
				return "none";
			return "";
		}
		public string GetBodyImageDisplay(Object InText)
		{
			if (InText == null)
				return "none";
			if (InText.ToString().Trim() == "")
				return "none";
			return "";
		}
		public string GetPerParaph(string Per,string UserID)
		{
			if(UserID==DAL.CheckData.CheckTokenGuestUserID().ToString())
				return "<br />پاراف خصوصی: "+Per;
			else
				return "";
		}
		
	}
}