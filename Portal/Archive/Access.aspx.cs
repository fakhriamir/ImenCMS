using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace Portal.Archive
{
	public partial class Access : System.Web.UI.Page
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
			Tools.Archive.CheckAccessFileAccess(Tools.Tools.ConvertToInt32(Request.QueryString["ID"]),"0");
			if (!IsPostBack)
				UpdateGrid();
		}
		private void UpdateGrid()
		{
			Tools.Archive.FillAllPeople(ParticipantDL, DAL.CheckData.CheckTokenGuestUserID().ToString());
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@ArchiveFileID",Tools.Tools.ConvertToInt32( Request.QueryString["ID"]));
			ViewDR.DataSource = DAL.ViewData.MyDR1("SELECT ArchiveFileAccessID, GuestID, ArchiveFileID, Access, Date, OwnerGustID  FROM ArchiveFileAccess  WHERE (Disable = 0) AND (ArchiveFileID = @ArchiveFileID) ", SP);
			ViewDR.DataBind();
		}
		public string GetAccessStr(string IDs)
		{
			if (IDs.TrimEnd() == "")
				return "";
			string[] MyAccess = {"دسترسی کامل", "خواندن", "نوشتن", "ویرایش", "حذف"};
			string[] Items = Regex.Split(IDs, ",");
			string OT = "";
			for (int i = 0; i < Items.Length; i++)
			{
				int It = Tools.Tools.ConvertToInt32(Items[i]);
				if (It != -1)
					OT += ", " + MyAccess[It];
			}
			return OT.TrimStart(',').Trim();
		}

		protected void SaveBTN_Click(object sender, EventArgs e)
		{
			InsertAccess(ParticipantDL.SelectedValue);
		}

		private void InsertAccess(string GuestID)
		{
			if(GetAccessItemCheck()=="")
			{
				Tools.Tools.Alert(Page, "یک سطح دسترسی انتخاب نمایید");
				return;
			}
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@ArchiveFileID", Tools.Tools.ConvertToInt32(Request.QueryString["ID"]));
			SP.AddWithValue("@GuestID", GuestID);
			SP.AddWithValue("@Access", GetAccessItemCheck());
			SP.AddWithValue("@OwnerGustID", DAL.CheckData.CheckTokenGuestUserID());
			int cnt = DAL.ExecuteData.CNTData("SELECT COUNT(*) FROM ArchiveFileAccess  WHERE (ArchiveFileID = @ArchiveFileID) AND (GuestID = @GuestID) and (disable=0)", SP);
			if (cnt != 0)
			{
				Tools.Tools.Alert(Page, "این دسترسی یک بار دیگر داده شده است");
				return;
			}
			DAL.ExecuteData.AddData("INSERT INTO ArchiveFileAccess(GuestID, ArchiveFileID, Access, OwnerGustID)  VALUES (@GuestID,@ArchiveFileID,@Access,@OwnerGustID)", SP);
			UpdateGrid();
			Tools.Tools.Alert(Page, "با موفقیت ثبت شد");
		}

		private string GetAccessItemCheck()
		{
			string OT="";
			for (int i = 0; i < AccessCBL.Items.Count; i++)
			{
				if (AccessCBL.Items[i].Selected)
					OT += AccessCBL.Items[i].Value + ",";
			}
			return OT.TrimEnd(',');
		}
		protected void SearchBTN_Click(object sender, EventArgs e)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@PName", "%" + PNameTB.Text.Trim() + "%");
			SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);
			GuestDR.DataSource = DAL.ViewData.MyDR1("SELECT GuestInfo.GuestID, GuestInfo.Name AS Fname, GuestInfo.Family, UnitChart.Name AS Semat FROM GuestInfo INNER JOIN    UnitChart ON GuestInfo.UnitChartID = UnitChart.UnitChartID WHERE   (GuestInfo.UnitID = @UnitID) and ((GuestInfo.Family LIKE @PName) or (GuestInfo.Name LIKE @PName) or (UnitChart.Name LIKE @PName))", SP);
			GuestDR.DataBind();
			if (GuestDR.Items.Count == 0)
				Tools.Tools.Alert(Page, "موردی یافت نشد");

		}
		protected void GuestDR_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			string ComArgg = e.CommandArgument.ToString();
			if (e.CommandName == "Invite")
			{
				InsertAccess(ComArgg);
			}
		}
		protected void ViewDR_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			string ComArg = e.CommandArgument.ToString();
			if (e.CommandName == "DEL")
			{
				DAL.ExecuteData.DeleteData("UPDATE ArchiveFileAccess  SET Disable = 1 WHERE (ArchiveFileAccessID = " +Tools.Tools.ConvertToInt32(ComArg)+ ")");
				Tools.Tools.Alert(Page, "با موفقیت حذف شد");
			}
			UpdateGrid();
		}
	}
}