using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data.SqlClient;
using System.Data;
namespace Portal.Automation
{
	public partial class NewLetter : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Tools.Automation.CheckAccess("automation_NewLetter.aspx");
			if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim().ToLower() == "in")
			{

			}
			if (!IsPostBack)
			{
				UpdateGrid();
			}
		}
		void UpdateGrid()
		{
			PeriorityDL.DataSource = DAL.ViewData.MyDT("Select OfficePriorityID, Name from OfficePriority where unitid=" + Tools.Tools.GetViewUnitID + " order by sort");
			PeriorityDL.DataBind();
			//PeriorityDL.Items.Insert(0, new ListItem(""));

			SubjectDL.DataSource = DAL.ViewData.MyDT("Select OfficeLetterSubjectID, Name from OfficeLetterSubject where unitid=" + Tools.Tools.GetViewUnitID + " order by sort");
			SubjectDL.DataBind();
			//SubjectDL.Items.Insert(0, new ListItem(""));

			ClassDL.DataSource = DAL.ViewData.MyDT("Select OfficeClassificationID, Name from OfficeClassification where unitid=" + Tools.Tools.GetViewUnitID + " order by sort");
			ClassDL.DataBind();
			//ClassDL.Items.Insert(0, new ListItem(""));
			LetterTemplateDL.DataSource = DAL.ViewData.MyDT("SELECT -1 as OfficeLetterTemplateID,'انتخاب' as name, 0 as sort union SELECT OfficeLetterTemplateID, Name,sort  FROM OfficeLetterTemplate  WHERE (UnitID =" + Tools.Tools.GetViewUnitID + ")  ORDER BY Sort");
			LetterTemplateDL.DataBind();
			

			LetterTypeDL.DataSource = DAL.ViewData.MyDT("Select OfficeLetterTypeID, Name from OfficeLetterType where unitid=" + Tools.Tools.GetViewUnitID + " order by sort");
			LetterTypeDL.DataBind();
			//LetterTypeDL.Items.Insert(0, new ListItem(""));
			string WhereComm = "";
			if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim().ToLower() == "in")
			{
				Tools.Automation.FillSenderAccess(SenderDL, DAL.CheckData.CheckTokenGuestUserID().ToString(), true);
				SignerDL.Visible = false;
				WhereComm = " and (OfficeLetter.SenderPersonalID<0) ";
			}
			else
			{
				Tools.Automation.FillSenderAccess(SenderDL, DAL.CheckData.CheckTokenGuestUserID().ToString());
				Tools.Automation.FillSenderAccess(SignerDL, DAL.CheckData.CheckTokenGuestUserID().ToString());
				SignerDL.Visible = true;
			}
			Tools.Automation.FillSenderAccess(RecieverDL, DAL.CheckData.CheckTokenGuestUserID().ToString());
			SupplierTB.Text = DAL.CheckData.GetUserLogonName();
			SupplierTB.Enabled = false;

			//letterID.Text = Tools.Automation.GetLetterNo(); //Tools.Calender.MyPDate().Substring(2, Tools.Calender.MyPDate().IndexOf("/")) + "-" + DAL.ExecuteData.ExecuteScalar("SELECT MAX(OfficeLetterID) FROM OfficeLetter") + 100;
			//letterID.Enabled = false;


			PagedDataSource pgitems = new PagedDataSource();
			DataView dv = new DataView(DAL.ViewData.MyDT("SELECT OfficeLetter.OfficeLetterID, OfficeLetter.Comm, OfficeLetter.Date, OfficeLetter.SignerPersonalID, OfficeLetter.SupplierPersonalID, OfficeLetter.SenderPersonalID, GuestInfo.Name + ' ' + GuestInfo.Family AS ReciverName, GuestInfo_1.Name + ' ' + GuestInfo_1.Family AS SenderName, OfficeLetterSubject.Name, OfficeLetter.Type FROM OfficeLetter INNER JOIN GuestInfo ON OfficeLetter.RecieverPersonalID = GuestInfo.GuestID INNER JOIN GuestInfo AS GuestInfo_1 ON OfficeLetter.SenderPersonalID = GuestInfo_1.GuestID  INNER JOIN OfficeLetterSubject ON OfficeLetter.OfficeLetterSubjectID = OfficeLetterSubject.OfficeLetterSubjectID WHERE (OfficeLetter.SupplierPersonalID = " + DAL.CheckData.CheckTokenGuestUserID() + ") AND (OfficeLetter.UnitID =" + Tools.Tools.GetViewUnitID + ") " + WhereComm + " ORDER BY OfficeLetter.OfficeLetterID DESC"));
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
		protected void SaveBTN_Click(object sender, EventArgs e)
		{
			FileUpload img = (FileUpload)FileFU;
			//&bool FileUp = false;
			SqlParameterCollection SP = new SqlCommand().Parameters;
			if (FileFU.PostedFile.FileName != "")
			{
				if (!Tools.Picture.CheckPic(FileFU.PostedFile))
				{
					Tools.Tools.Alert(Page, "نوع تصویر وارد شده معتبر نمی باشد");
					return;
				}
			}
			if (BodyTB.Text.Trim() == "" && img.PostedFile == null)
			{
				Tools.Tools.Alert(Page, "متن یا تصویر نامه وارد شود");
				return;
			}
			
			Byte[] imgByte = null;
			string UpdateBI = "", InsertBI = "", InsertBI1 = "";

			if (img.HasFile && img.PostedFile != null)
			{
				HttpPostedFile File = FileFU.PostedFile;
				imgByte = new Byte[File.ContentLength];
				File.InputStream.Read(imgByte, 0, File.ContentLength);
				SP.AddWithValue("-@BodyIMG", imgByte);
				UpdateBI = " BodyIMG=@BodyIMG, ";
				InsertBI = " ,[BodyIMG] ";
				InsertBI1 = " ,@BodyIMG ";
			}
			
			
			SP.AddWithValue("@Subject", SubjectDL.SelectedValue);
			SP.AddWithValue("@Periority", PeriorityDL.SelectedValue);
			SP.AddWithValue("@Classification", ClassDL.SelectedValue);
			SP.AddWithValue("@Sender", SenderDL.SelectedValue);
			SP.AddWithValue("@RecieverPersonalID", RecieverDL.SelectedValue);
			SP.AddWithValue("@Supplier", DAL.CheckData.CheckTokenGuestUserID().ToString());
			SP.AddWithValue("@Signer", SignerDL.SelectedValue);
			SP.AddWithValue("@Desc", DescTB.Text.Trim().ToString());
			SP.AddWithValue("@Body", BodyTB.Text.ToString());
			SP.AddWithValue("@OfficeLetterTypeID", LetterTypeDL.SelectedValue);
            SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);
			//SP.AddWithValue("@Date", DateTime.Now);
			if (ViewState["LettersS"] != null && ViewState["LettersS"].ToString().Trim() != "")
				DAL.ExecuteData.AddData("UPDATE dbo.OfficeLetter SET "+UpdateBI+" RecieverPersonalID=@RecieverPersonalID, SupplierPersonalID = @SupplierPersonalID ,SignerPersonalID = @SignerPersonalID ,OfficeLetterSubjectID = @OfficeLetterSubjectID ,Comm = @Comm ,Body = @Body,OfficeClassificationID = @OfficeClassificationID ,UnitID = @UnitID ,OfficePriorityID = @OfficePriorityID ,[OfficeLetterTypeID] = @OfficeLetterTypeID where OfficeLetterID=" + ViewState["LettersS"].ToString().Trim(), SP);
			else
				DAL.ExecuteData.AddData("INSERT INTO [dbo].[OfficeLetter]([SupplierPersonalID] ,[SenderPersonalID],[SignerPersonalID],[OfficeLetterSubjectID] ,[Comm] ,[Body] ,[OfficeClassificationID],[UnitID],[OfficePriorityID],[OfficeLetterTypeID],[RecieverPersonalID] "+InsertBI+") VALUES(@Supplier,@Sender,@Signer,@Subject,@Desc,@Body,@Classification,@UnitID,@Periority,@OfficeLetterTypeID,@RecieverPersonalID "+InsertBI1+")", SP);
		    UpdateGrid();
			BodyTB.Text = "";
			Tools.Tools.Alert(Page, "نامه با موفقیت ثبت شد");
		}
        //protected void RefBTN_Click(object sender, EventArgs e)
        //{
        //    SqlParameterCollection SP = new SqlCommand().Parameters;
        //    SP.AddWithValue("@Subject", SubjectDL.SelectedValue);
        //    SP.AddWithValue("@Periority", PeriorityDL.SelectedValue);
        //    SP.AddWithValue("@Classification", ClassDL.SelectedValue);
        //    SP.AddWithValue("@Sender", SenderTB.Text.ToString().Substring(0, SenderTB.Text.Trim().IndexOf("-")));
        //    SP.AddWithValue("@Supplier", SupplierTB.Text.ToString().Substring(0, SenderTB.Text.Trim().IndexOf("-")));
        //    SP.AddWithValue("@Signer", SignerDL.SelectedValue);
        //    SP.AddWithValue("@Desc", DescTB.Text.Trim().ToString());
        //    SP.AddWithValue("@Body", BodyTB.Text.ToString());
        //    SP.AddWithValue("@LetterType", LetterTypeDL.SelectedValue);
        //    //SP.AddWithValue("@UnitID", DAL.CheckData.GetUnitID());
        //    SP.AddWithValue("@Date", Tools.Calender.MyPDate());

        //    DAL.ExecuteData.AddData("INSERT INTO dbo.OfficeReference(OfficeReferenceID,SenderPersonalID,ToPersonalID,OfficePriorityID,Paraph,PerParaph,UnitID,Type) VALUES (OfficeReferenceID,SenderPersonalID,ToPersonalID ,OfficePriorityID,Paraph ,PerParaph,UnitID,Type)", SP);
        //}
      
        protected void ViewDR_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string ComArg = e.CommandArgument.ToString();
            if (e.CommandName == "DEL")
            {
               /* if (false)//DAL.CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Del))
                {
                    Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "DelAccessText").ToString(), "", true);
                    return;
                }
                else*/
                    DAL.ExecuteData.DeleteData("delete from OfficeLetter where OfficeLetterID=" + ComArg);
            }
			else if (e.CommandName == "EDIT")
			{

			}
			else if (e.CommandName == "Sign")
			{
				int SignID =Tools.Automation.SetSign(ComArg);
				if(SignID==-1)
					Tools.Tools.Alert(Page, "یک بار دیگر نامه امضا شده است");
				else
					Tools.Tools.Alert(Page, "نامه با موفقیت امضا شد");
			}
			UpdateGrid();
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
	}
}