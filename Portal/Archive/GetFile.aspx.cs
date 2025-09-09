using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Archive
{
	public partial class GetFile : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Tools.Meeting.CheckAccess("meeting");

			//int RID;
			if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim() == "")
			{
				//Response.Write("E1");
				return;
			}
			else if (Request.QueryString["ID"].Substring(0, 1).ToLower() == "f")
			{//View File
				if (!Tools.Archive.CheckFileAccess(Request.QueryString["ID"].Substring(1)))
				{
					Response.Write("you have not access");
					return;
				}
				GetItemFile(Tools.Tools.ConvertToInt32(Request.QueryString["ID"].Substring(1)));
			}
			else
			{
				//Response.Write("E2");
				return;
			}
			//{
			//	if (!int.TryParse(Request.QueryString["ID"], out RID))
			//		return;
			//	UDR(RID);
			//}
		}
		private void GetItemFile(int RID)
		{
			Context.Response.ContentType = DAL.ExecuteData.CNTDataStr("SELECT FileType FROM ArchiveFile  WHERE (MArchiveFileID = " + RID + ") AND (UnitID = " + Tools.Tools.GetViewUnitID + ")");
			Context.Response.AddHeader("Content-Disposition", "attachment; filename=" + DAL.ExecuteData.CNTDataStr("SELECT FileName FROM ArchiveFile  WHERE (ArchiveFileID = " + RID + ") AND (UnitID = " + Tools.Tools.GetViewUnitID + ")").Replace(" ", "-"));
			Stream strm = GetFileStream(RID);
			if (strm == null)
				return;
			byte[] buffer = new byte[4096];
			int byteSeq = strm.Read(buffer, 0, 4096);

			while (byteSeq > 0)
			{
				Context.Response.OutputStream.Write(buffer, 0, byteSeq);
				byteSeq = strm.Read(buffer, 0, 4096);
			}
		}
		private void UDR(int RID)
		{
			Context.Response.ContentType = "image/jpeg";
			Stream strm = GetImage(RID);
			if (strm == null)
				return;
			byte[] buffer = new byte[4096];
			int byteSeq = strm.Read(buffer, 0, 4096);

			while (byteSeq > 0)
			{
				Context.Response.OutputStream.Write(buffer, 0, byteSeq);
				byteSeq = strm.Read(buffer, 0, 4096);
			}
		}
		public Stream GetImage(int ID)
		{
			object img = DAL.ExecuteData.ExecuteScalar("SELECT BodyIMG FROM OfficeLetter  WHERE (OfficeLetterID = " + ID + ") AND (UnitID = " + Tools.Tools.GetViewUnitID + ")");
			try
			{
				return new MemoryStream((byte[])img);
			}
			catch
			{
				return null;
			}
		}
		public Stream GetFileStream(int ID)
		{
			object img = DAL.ExecuteData.ExecuteScalar("SELECT [File] FROM ArchiveFile  WHERE (ArchiveFileID = " + ID + ") AND (UnitID = " + Tools.Tools.GetViewUnitID + ")");
			try
			{
				return new MemoryStream((byte[])img);
			}
			catch
			{
				return null;
			}
		}
	}
}