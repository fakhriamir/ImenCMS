using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Archive
{
	public partial class AddFiles : System.Web.UI.Page
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Tools.Archive.CheckAccess("Archive_AddFiles.aspx");
			if (!IsPostBack)
			{
				if (Request.QueryString["ID"] == null || Request.QueryString["ID"] == "")
					UpdateGrid();
				else
					UpdateGrid(Request.QueryString["ID"]);
			}
		}
		void UpdateGrid(string FileID="")
		{
			Tools.Archive.FillArchive(archiveidDL);
			Tools.Archive.FillArchiveCategory(archivecategoryDL);
			Tools.Archive.FillArchiveType(archivetypeidDL);
			Tools.Project.FillProjectAccess(projectidDL, DAL.CheckData.CheckTokenGuestUserID().ToString());
			
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@unitid", Tools.Tools.GetViewUnitID);
			if (FileID.Trim() != "")
			{
				SP.AddWithValue("@ID", FileID);
				SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT archivefileid,ArchiveID,archivecategoryid,projectid,ArchiveTypeID,title,description,filename,filetype,filesize,parentarchivefileid,filever,unitid,guestid,type FROM ArchiveFile where unitid=@unitid and ArchiveFileID=@ID", SP);
				if (MyRead.Read())
				{
					Tools.Tools.SetDropDownListValue(archiveidDL, Tools.MyCL.MGInt(MyRead, 1).ToString());
					Tools.Tools.SetDropDownListValue(archivecategoryDL, Tools.MyCL.MGInt(MyRead, 2).ToString());
					Tools.Tools.SetDropDownListValue(archivetypeidDL, Tools.MyCL.MGInt(MyRead, 4).ToString());
					Tools.Tools.SetDropDownListValue(projectidDL, Tools.MyCL.MGInt(MyRead, 3).ToString());
					archiveidDL.Enabled = false;
					archivecategoryDL.Enabled = false;
					archivetypeidDL.Enabled = false;
					projectidDL.Enabled = false;
				}
				MyRead.Close(); MyRead.Dispose();				
				ViewDR.DataSource = DAL.ViewData.MyDT("SELECT archivefileid,archivecategoryid,projectid,title,description,filename,filetype,filesize,parentarchivefileid,filever,unitid,guestid,type FROM ArchiveFile where unitid=@unitid and (ArchiveFileID=@ID or ParentArchiveFileID= @ID ) ", SP);
			}
			else
			{
				ViewDR.DataSource = DAL.ViewData.MyDT("SELECT archivefileid,archivecategoryid,projectid,title,description,filename,filetype,filesize,parentarchivefileid,filever,unitid,guestid,type FROM ArchiveFile where unitid=@unitid and GuestID=" + DAL.CheckData.CheckTokenGuestUserID() + " ", SP);
			}
			ViewDR.DataBind();
			Tools.Archive.FillArchive(archiveidDL);
			Tools.Archive.FillArchiveCategory(archivecategoryDL);
			Tools.Archive.FillArchiveType(archivetypeidDL);
			Tools.Project.FillProjectAccess(projectidDL, DAL.CheckData.CheckTokenGuestUserID().ToString());
		
		}
		protected void SaveBTN_Click(object sender, System.EventArgs e)
		{
			if (titleTB.Text.Trim() == "" )
			{
				Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "FillStarItem").ToString());
				return;
			}
            string FileType = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf("."));
            String FileName = Guid.NewGuid().ToString().Trim() + FileType;
            String FileAddress = "";
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@archivecategoryid", archivecategoryDL.SelectedValue);
			SP.AddWithValue("@archiveid", archiveidDL.SelectedValue);
			SP.AddWithValue("@archivetypeid", archivetypeidDL.SelectedValue);
			SP.AddWithValue("@projectid", projectidDL.SelectedValue);
			SP.AddWithValue("@title", titleTB.Text.Trim());
			SP.AddWithValue("@description", descriptionTB.Text.Trim());
			SP.AddWithValue("@filename", FileUpload1.PostedFile.FileName);
			SP.AddWithValue("@filetype", FileUpload1.PostedFile.ContentType);
			SP.AddWithValue("@filesize", FileUpload1.PostedFile.ContentLength);
			if(Request.QueryString["ID"]==null || Request.QueryString["ID"]=="")
				SP.AddWithValue("@parentarchivefileid", DBNull.Value);
			else
				SP.AddWithValue("@parentarchivefileid", Tools.Tools.ConvertToInt32(Request.QueryString["ID"]));
			SP.AddWithValue("@filever", fileverTB.Text.Trim());
			SP.AddWithValue("@unitid", Tools.Tools.GetViewUnitID);
			SP.AddWithValue("@guestid", DAL.CheckData.CheckTokenGuestUserID());
			SP.AddWithValue("@type", 0);



			if (ViewState["ArchiveFileS"] != null && ViewState["ArchiveFileS"].ToString().Trim() != "")
				DAL.ExecuteData.AddData("update ArchiveFile set archivecategoryid = @archivecategoryid,archiveid = @archiveid,archivetypeid = @archivetypeid,projectid = @projectid,title = @title,description = @description,filename = @filename,filetype = @filetype,filesize = @filesize,parentarchivefileid = @parentarchivefileid,filever = @filever,unitid = @unitid,guestid = @guestid,type = @type where ArchiveFileID=" + ViewState["ArchiveFileS"].ToString().Trim(), SP);
			else
			{
				if (string.IsNullOrEmpty(FileUpload1.PostedFile.FileName.Trim()))
				{
					Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "FillStarItem").ToString());
					return;
				}
				FileUpload img = (FileUpload)FileUpload1;
				if (!Tools.Tools.CheckFileExtention(Path.GetExtension(img.FileName)))
				{
					Tools.Tools.Alert(Page, "پسوند فایل مورد نظر مورد تایید نمی باشد");
					return;
				}
				
                if (Tools.Tools.GetSetting(416, "0") == "0")
                {
                    FileAddress = Tools.Tools.GetSetting(417) + FileName.Substring(0, 2) + "/";
                    if (!Directory.Exists(FileAddress))
                    {
                        FileAddress += FileName.Substring(2, 2) + "/" + FileName.Substring(4, 2) + "/";
                        Directory.CreateDirectory(FileAddress);
                    }
                    else if (!Directory.Exists(Path.Combine(FileAddress, FileAddress.Substring(2, 2))))
                    {
                        FileAddress += Path.Combine(FileAddress, FileAddress.Substring(2, 2)) + FileName.Substring(4, 2);
                        Directory.CreateDirectory(FileAddress);
                    }

                    FileAddress = FileAddress.Replace("/", @"\");
                    FileUpload1.PostedFile.SaveAs(FileAddress + FileName.Substring(0,FileName.IndexOf(".")));

                    SP.AddWithValue("@FileAddress", FileName.Substring(0,FileName.IndexOf(".")));
                    
                    DAL.ExecuteData.AddData("INSERT INTO ArchiveFile (archivecategoryid ,archiveid ,archivetypeid ,projectid ,title ,description ,filename  ,FileAddress, filetype ,filesize ,parentarchivefileid ,filever ,unitid ,guestid ,type ) VALUES ( @archivecategoryid ,@archiveid ,@archivetypeid ,@projectid ,@title ,@description ,@filename ,@FileAddress , @filetype ,@filesize ,@parentarchivefileid ,@filever ,@unitid ,@guestid ,@type )", SP);
                }               
                else{
                    Byte[] imgByte = null;
                    if (img.HasFile && img.PostedFile != null)
                    {
                        HttpPostedFile File = FileUpload1.PostedFile;
                        imgByte = new Byte[File.ContentLength];
                        File.InputStream.Read(imgByte, 0, File.ContentLength);
                    }

                    SP.AddWithValue("-@file", imgByte);
                    DAL.ExecuteData.AddData("INSERT INTO ArchiveFile (archivecategoryid ,archiveid ,archivetypeid ,projectid ,title ,description ,filename ,[file] , filetype ,filesize ,parentarchivefileid ,filever ,unitid ,guestid ,type ) VALUES ( @archivecategoryid ,@archiveid ,@archivetypeid ,@projectid ,@title ,@description ,@filename ,@file  , @filetype ,@filesize ,@parentarchivefileid ,@filever ,@unitid ,@guestid ,@type )", SP);
                }
				
			}
			ViewState["ArchiveFileS"] = null;
			UpdateGrid();
			SaveBTN.Text = "اضافه شود";
			
			titleTB.Text = "";
			descriptionTB.Text = "";
		
			fileverTB.Text = "";
			
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
				DAL.ExecuteData.DeleteData("delete from ArchiveFile where ArchiveFileID=" + ComArg);
			}
			else if (e.CommandName == "EDIT")
			{
			
				SqlDataReader MyRead = DAL.ViewData.MyDR1("select  archivefileid,archivecategoryid,archiveid,archivetypeid,projectid,title,description,filename,[file],filetype,filesize,parentarchivefileid,filever,unitid,guestid,type from ArchiveFile where ArchiveFileID=" + ComArg);

				if (!MyRead.Read())
					return;
				ViewState["ArchiveFileS"] = Tools.MyCL.MGInt(MyRead, 0).ToString().Trim();
				archivecategoryDL.SelectedValue = Tools.MyCL.MGInt(MyRead, 1).ToString();
				Tools.Tools.SetDropDownListValue(	archiveidDL, Tools.MyCL.MGInt(MyRead, 2).ToString());
				Tools.Tools.SetDropDownListValue(archivetypeidDL, Tools.MyCL.MGInt(MyRead, 3).ToString());
				Tools.Tools.SetDropDownListValue(projectidDL, Tools.MyCL.MGInt(MyRead, 4).ToString());
				titleTB.Text = Tools.MyCL.MGStr(MyRead, 5);
				descriptionTB.Text = Tools.MyCL.MGStr(MyRead, 6);
				
				fileverTB.Text = Tools.MyCL.MGInt(MyRead, 12).ToString();
				
				SaveBTN.Text = "ويرايش شود";
			}
		}
	}
}