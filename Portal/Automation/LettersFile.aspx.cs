using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Automation
{
	public partial class LettersFile : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Tools.Automation.CheckAccess("");
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
			if (!IsPostBack)
				UpdateGrid();
		}
		void UpdateGrid()
		{
			ViewDR.DataSource = DAL.ViewData.MyDR1("SELECT officeletteratachid,name,officefiletypeid,officeletterid,FileName FROM OfficeLetterAtach where unitid=" + Tools.Tools.GetViewUnitID);
			ViewDR.DataBind();
			officefiletypeidDL.DataSource = DAL.ViewData.MyDR1("SELECT OfficeFileTypeID, Name  FROM OfficeFileType  where unitid=" + Tools.Tools.GetViewUnitID + " order by sort");
			officefiletypeidDL.DataBind();
		}
		protected void SaveBTN_Click(object sender, System.EventArgs e)
		{
						
			FileUpload img = (FileUpload)FileFU;
			if (!Tools.Tools.CheckFileExtention(Path.GetExtension(img.FileName)))
			{
				Tools.Tools.Alert(Page, "پسوند فایل مورد نظر مورد تایید نمی باشد");
				return;
			}		
			Byte[] imgByte = null;
			if (img.HasFile && img.PostedFile != null)
			{
				HttpPostedFile File = FileFU.PostedFile;
				imgByte = new Byte[File.ContentLength];
				File.InputStream.Read(imgByte, 0, File.ContentLength);
			}
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@name", nameTB.Text.Trim());
			SP.AddWithValue("-@file", imgByte);
			SP.AddWithValue("@Type", img.PostedFile.ContentType);
			SP.AddWithValue("@FileName", img.FileName);
			SP.AddWithValue("@officefiletypeid", officefiletypeidDL.SelectedValue);
			SP.AddWithValue("@officeletterid", Tools.Tools.ConvertToInt32(Request.QueryString["ID"]));
			SP.AddWithValue("@unitid", Tools.Tools.GetViewUnitID);
			DAL.ExecuteData.AddData("INSERT INTO OfficeLetterAtach (name ,officefiletypeid ,officeletterid ,[file] ,unitid,Type,FileName ) VALUES ( @name ,@officefiletypeid ,@officeletterid ,@file ,@unitid,@Type,@FileName )", SP);
			
			UpdateGrid();
			nameTB.Text = "";
			Tools.Tools.Alert(Page, "فایل مورد نظر اضافه شد");
		}
		protected void ViewDR_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			string ComArg = e.CommandArgument.ToString();
			
			if (e.CommandName == "DEL")
			{
				
					DAL.ExecuteData.DeleteData("delete from OfficeLetterAtach where OfficeLetterAtachID=" + ComArg);
			}
			//else if (e.CommandName == "EDIT")
			//{
			//	if (ADAL.A_CheckData.GetUserAccess(this.Page.ToString(), Tools.MyVar.UserAccess.Edit))
			//	{
			//		Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "EditAccessText").ToString(), "", true);
			//		return;
			//	}
			//	SqlDataReader MyRead = ADAL.A_ViewData.MyDR("select  officeletteratachid,name,officefiletypeid,officeletterid,file,unitid from OfficeLetterAtach where OfficeLetterAtachID=" + ComArg);

			//	if (!MyRead.Read())
			//		return;
			//	ViewState["OfficeLetterAtachS"] = Tools.MyCL.MGInt(MyRead, 0).ToString().Trim();
			//	nameTB.Text = Tools.MyCL.MGStr(MyRead, 1);
			//	officefiletypeidTB.Text = Tools.MyCL.MGInt(MyRead, 2).ToString();
			//	officeletteridTB.Text = Tools.MyCL.MGInt(MyRead, 3).ToString();
			//	unitidTB.Text = Tools.MyCL.MGInt(MyRead, 5).ToString();

			//	SaveBTN.Text = "ويرايش شود";
			//}
		}
	}
}