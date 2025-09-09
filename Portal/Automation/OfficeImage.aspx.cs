using System;
using System.IO;

namespace Portal.Automation
{
	public partial class OfficeImage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			int RID;
			if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim() == "")
				return;
			if (Request.QueryString["ID"].Substring(0, 1) == "F")
			{//View File
				GetItemFile(Tools.Tools.ConvertToInt32( Request.QueryString["ID"].Substring(1)));
			}
			else
			{
				if (!int.TryParse(Request.QueryString["ID"], out RID))
					return;
				UDR(RID);
			}
		}
		private void GetItemFile(int RID)
		{
			Context.Response.ContentType = DAL.ExecuteData.CNTDataStr("SELECT Type FROM OfficeLetterAtach  WHERE (OfficeLetterAtachID = " + RID + ") AND (UnitID = " + Tools.Tools.GetViewUnitID + ")");
			Context.Response.AddHeader("Content-Disposition", "attachment; filename=" + DAL.ExecuteData.CNTDataStr("SELECT FileName FROM OfficeLetterAtach  WHERE (OfficeLetterAtachID = " + RID + ") AND (UnitID = " + Tools.Tools.GetViewUnitID + ")").Replace(" ","-"));
			Stream strm = GetFile(RID);
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
		public Stream GetFile(int ID)
		{
			object img = DAL.ExecuteData.ExecuteScalar("SELECT [File] FROM OfficeLetterAtach  WHERE (OfficeLetterAtachID = " + ID + ") AND (UnitID = " + Tools.Tools.GetViewUnitID + ")");
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