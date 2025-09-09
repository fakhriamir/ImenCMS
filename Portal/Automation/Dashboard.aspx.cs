using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
namespace Portal.Automation
{
	public partial class Dashboard : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Tools.Automation.CheckAccess("automation_Dashboard.aspx");
			if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim().ToLower() == "follow")
			{
				UpdateGrid(1);
				
			}
			else if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim().ToLower() == "work")//پوشه های کاری
			{
				WorkDiv.Visible = true;
				if (!IsPostBack)
				{
					SqlParameterCollection SP = new SqlCommand().Parameters;
					SP.AddWithValue("@UserID", DAL.CheckData.CheckTokenGuestUserID());
					SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);
					WorkDL.DataSource = DAL.ViewData.MyDT("SELECT officeworkfolderid,name FROM OfficeWorkFolder where Guestid=@UserID and unitid=@UnitID order by sort", SP);
					WorkDL.DataBind();
					WorkDL_SelectedIndexChanged(null, null);
				}				
			}
			else if (!IsPostBack)
				UpdateGrid();
		}
		void UpdateGrid(int TypeFilter=0)
		{
			DataView dv;
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@Type", TypeFilter);
			dv = new DataView(DAL.ViewData.MyDT("SELECT OfficeReference.OfficeReferenceID,OfficeReference.Paraph,OfficeReference.OfficeLetterID,GuestInfo.Name, GuestInfo.Family, OfficeReference.Date, OfficeReferenceType.Name AS OfficeReferenceTypeMane, OfficeLetterSubject.Name AS OfficeLetterSubjectName, OfficeLetterType.Name AS OfficeLetterTypeName, OfficePriority.Name AS OfficePriorityName FROM OfficeReference INNER JOIN OfficePriority ON OfficeReference.OfficePriorityID = OfficePriority.OfficePriorityID INNER JOIN OfficeLetter ON OfficeReference.OfficeLetterID = OfficeLetter.OfficeLetterID INNER JOIN OfficeLetterSubject ON OfficeLetter.OfficeLetterSubjectID = OfficeLetterSubject.OfficeLetterSubjectID INNER JOIN OfficeLetterType ON OfficeLetter.OfficeLetterTypeID = OfficeLetterType.OfficeLetterTypeID INNER JOIN GuestInfo ON OfficeReference.SenderPersonalID = GuestInfo.GuestID INNER JOIN OfficeReferenceType ON OfficeReference.OfficeReferenceTypeID = OfficeReferenceType.OfficeReferenceTypeID WHERE (OfficeReference.ToPersonalID = " + DAL.CheckData.CheckTokenGuestUserID() + ") AND (OfficeReference.Type = @Type) ORDER BY OfficeReference.OfficeReferenceID DESC ", SP));
			PagedDataSource pgitems = new PagedDataSource(); 
			pgitems.DataSource = dv;
			pgitems.AllowPaging = true;
			pgitems.PageSize = 20;
			if (CurrentPage < 0)
				CurrentPage = 0;
			pgitems.CurrentPageIndex = CurrentPage;
			lnkPreviousPage.Enabled = !pgitems.IsFirstPage;
			lnkNextPage.Enabled = !pgitems.IsLastPage;
			Tools.Tools.GetPagging(pgitems, rptPages, CurrentPage);
			ViewDR.DataSource = pgitems;
			ViewDR.DataBind();
		}
		protected void ViewDR_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			string ComArg = e.CommandArgument.ToString();
			if (e.CommandName == "DEL")
			{
				//if (true)//DAL.CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Del))
				//{
				//	Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "DelAccessText").ToString(), "", true);
				//	return;
				//}
				//else
					DAL.ExecuteData.DeleteData("delete from OfficeLetter where OfficeLetterID=" + ComArg);
					UpdateGrid();
			}
		}
		protected void lnkNextPage_Click(object sender, EventArgs e)
		{
			CurrentPage += 1;
			UpdateGrid();
		}
		protected void lnkPreviousPage_Click(object sender, EventArgs e)
		{
			CurrentPage -= 1;
			UpdateGrid();
		}
		int CurrentPage
		{
			get
			{
				//Look for current page in ViewState
				object o = this.Session["LettersS"];
				if (o == null)
					return 0; // default page index of 0
				else
					return (int)o;
			}
			set
			{
				this.Session["LettersS"] = value;
			}
		}
		protected void rptPages_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			try
			{
				CurrentPage = (Tools.Tools.ConvertToInt32(e.CommandArgument) - 1);
			}
			catch { }
			UpdateGrid();
		}
		protected void rptPages_ItemCreated(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType != ListItemType.Separator)
			{
				if (e.Item.DataItem == null)
					return;
				if (e.Item.DataItem.ToString() == (CurrentPage + 1).ToString())
					e.Item.DataItem = "<font color=red>" + e.Item.DataItem + "</font>";
			}
		}

		protected void WorkDL_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateGrid((Tools.Tools.ConvertToInt32(WorkDL.SelectedValue)*-1));
		}

	}
}