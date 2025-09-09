using System;
using System.Data.SqlClient;

namespace Portal.Archive
{
	public partial class View : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Tools.Archive.CheckAccess("Archive");

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
			Tools.Archive.CheckAccessFileAccess(Tools.Tools.ConvertToInt32(Request.QueryString["ID"]), "0,1,2,3,4,5");
			if (!IsPostBack)
				UpdateGrid();
		}
		private void UpdateGrid()
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@ArchiveFileID", Tools.Tools.ConvertToInt32(Request.QueryString["ID"]));
            ViewDR.DataSource = DAL.ViewData.MyDR1("SELECT ArchiveFile.ArchiveFileID, ArchiveFile.ArchiveCategoryID, ArchiveFile.ArchiveID, ArchiveFile.ArchiveTypeID, ArchiveFile.ProjectID, ArchiveFile.Title, ArchiveFile.Description, ArchiveFile.FileName, ArchiveFile.[File], ArchiveFile.FileAddress, ArchiveFile.FileType, ArchiveFile.FileSize, ArchiveFile.ParentArchiveFileID, ArchiveFile.FileVer, ArchiveFile.UnitID, ArchiveFile.GuestID, ArchiveFile.Type, Project.Name AS ProjectName, ArchiveCategory.Name AS ArchiveCategoryName, ArchiveType.Name AS ArchiveTypeName, Archive.Name AS ArchiveName FROM ArchiveFile LEFT OUTER  JOIN ArchiveCategory ON ArchiveFile.ArchiveCategoryID = ArchiveCategory.ArchiveCategoryID LEFT OUTER  JOIN ArchiveType ON ArchiveFile.ArchiveTypeID = ArchiveType.ArchiveTypeID LEFT OUTER  JOIN Project ON ArchiveFile.ProjectID = Project.ProjectID LEFT OUTER  JOIN Archive ON ArchiveFile.ArchiveID = Archive.ArchiveID WHERE (ArchiveFile.ArchiveFileID = @ArchiveFileID) OR (ArchiveFile.ParentArchiveFileID = @ArchiveFileID) ", SP);
			ViewDR.DataBind();
		}

	}
}