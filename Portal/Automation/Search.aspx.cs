using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Portal.Automation
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LetterTypeDL.DataSource = DAL.ViewData.MyDT("SELECT OfficeLetterTypeID, Name FROM OfficeLetterType");
                LetterTypeDL.DataBind();
            }
        }
        private void TitleDB() {
            //DateTime dt1, dt2;
            SqlParameterCollection SP = new SqlCommand().Parameters;

            string strSqlWhere = "";//CONTAINS(Texts, @ST)
            if (!String.IsNullOrEmpty(LetterNoTB.Text))
            {
                SP.AddWithValue("@LetterID", Tools.Tools.SetSearchWord(LetterNoTB.Text.Trim()));
                strSqlWhere += " AND (CONTAINS(OfficeReference.OfficeLetterID,@LetterID)) ";
            }
            if (!String.IsNullOrEmpty(LetterSubjectTB.Text))
            {
                SP.AddWithValue("@Subject", Tools.Tools.SetSearchWord(LetterSubjectTB.Text.Trim()));
                strSqlWhere += " AND (CONTAINS(OfficeLetterSubject.Name,@Subject)) ";
            }
            if (LetterTypeDL.SelectedValue != "")
            {
                SP.AddWithValue("@LetterType", Tools.Tools.SetSearchWord(LetterTypeDL.SelectedValue));
                strSqlWhere += " AND (CONTAINS(OfficeLetterType.Name,@LetterType)) ";
            }
            if (!String.IsNullOrEmpty(SenderTB.Text))
            {
                SP.AddWithValue("@Sender", Tools.Tools.SetSearchWord(SenderTB.Text.Trim()));
                strSqlWhere += " AND (CONTAINS(GuestInfo.Family,@Sender)) ";
            }
            if (LetterTypeDL.SelectedValue !="")
            {
                SP.AddWithValue("@Type", LetterTypeDL.SelectedValue);
                strSqlWhere+=" AND (OfficeReference.Type=@Type) ";
            }
            if (!String.IsNullOrEmpty(FromDateTB.Text) && !String.IsNullOrEmpty(ToDateTB.Text))
            {
                //dt1 = Tools.Calender.PersianToEnglish(DDl_Year.SelectedValue, DDL_Mounth.SelectedValue, DDL_Day.SelectedValue, 0, 0);
                //dt2 = Tools.Calender.PersianToEnglish(DDl_Year0.SelectedValue, DDL_Mounth0.SelectedValue, DDL_Day0.SelectedValue, 0, 0);
                //SP.AddWithValue("@dt1", dt1.ToShortDateString());
                //SP.AddWithValue("@dt2", dt2.ToShortDateString());
                strSqlWhere += " AND (RDate between @dt1 AND @dt2) ";
            }
            string strSqlComm = "SELECT OfficeReference.OfficeReferenceID,OfficeReference.Paraph,OfficeReference.OfficeLetterID As LetterNO,GuestInfo.Name As Fname, GuestInfo.Family As Family, OfficeReference.Date As RDate, OfficeReferenceType.Name AS OfficeReferenceTypeMane, OfficeLetterSubject.Name AS OfficeLetterSubjectName, OfficeLetterType.Name AS OfficeLetterTypeName, OfficePriority.Name AS OfficePriorityName FROM OfficeReference INNER JOIN OfficePriority ON OfficeReference.OfficePriorityID = OfficePriority.OfficePriorityID INNER JOIN OfficeLetter ON OfficeReference.OfficeLetterID = OfficeLetter.OfficeLetterID INNER JOIN OfficeLetterSubject ON OfficeLetter.OfficeLetterSubjectID = OfficeLetterSubject.OfficeLetterSubjectID INNER JOIN OfficeLetterType ON OfficeLetter.OfficeLetterTypeID = OfficeLetterType.OfficeLetterTypeID INNER JOIN GuestInfo ON OfficeReference.SenderPersonalID = GuestInfo.GuestID INNER JOIN OfficeReferenceType ON OfficeReference.OfficeReferenceTypeID = OfficeReferenceType.OfficeReferenceTypeID WHERE (OfficeReference.ToPersonalID = " + DAL.CheckData.CheckTokenGuestUserID() + ") AND (OfficeReference.Type = @Type) ORDER BY OfficeReference.OfficeReferenceID DESC ";
            if (strSqlWhere == "")
                strSqlComm = strSqlComm + " where  OfficeLetter.unitid=" + Tools.Tools.GetViewUnitID + " order by RDate Desc  ";
            else
                strSqlComm = strSqlComm + " where " + strSqlWhere.Substring(4) + " and OfficeLetter.unitid=" + Tools.Tools.GetViewUnitID + " order by RDate Desc ";

            PagedDataSource pgitems = new PagedDataSource();
            DataView dv = new DataView(DAL.ViewData.MyDT(strSqlComm, SP));
            pgitems.DataSource = dv;
            pgitems.AllowPaging = true;
            pgitems.PageSize = 25;
            if (CurrentPage < 0)
                CurrentPage = 0;
            pgitems.CurrentPageIndex = CurrentPage;
            lnkPreviousPage.Enabled = !pgitems.IsFirstPage;
            lnkNextPage.Enabled = !pgitems.IsLastPage;
            Tools.Tools.GetPagging(pgitems, rptPages, CurrentPage);
            rptLetters.DataSource = pgitems;
            rptLetters.DataBind();
            rptLetters.Dispose();

        }
        protected void SearchBTN_Click(object sender, EventArgs e)
        {
            TitleDB();
        }
        protected void lnkNextPage_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            //	if (Request.QueryString["ID"] != null && Request.QueryString["ID"].IndexOf("type") != -1)
            TitleDB();
            //else
            //TitleDB(0);
        }

        protected void lnkPreviousPage_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;

            TitleDB();
        }
        public int CurrentPage
        {
            get
            {
                //Look for current page in ViewState
                object o = this.Session["_LettersPage"];
                if (o == null)
                    return 0; // default page index of 0
                else
                    return (int)o;
            }
            set
            {
                this.Session["_LettersPage"] = value;
            }
        }
        protected void rptPages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                CurrentPage = (Tools.Tools.ConvertToInt32(e.CommandArgument) - 1);
            }
            catch { }
            TitleDB();
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
    }
}