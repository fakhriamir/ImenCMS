using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
	public partial class FormsReferer : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		protected void SearchTicketBTN_Click(object sender, EventArgs e)
		{
			int AID = (Tools.Tools.ConvertToInt32(TNoTB.Text));
			if (AID < 0)
			{
				Tools.Tools.Alert(Page, "شماره تیکت یا رمز تیکت قابل قبول نمی باشد");
				return;
			}
			System.Data.SqlClient.SqlParameterCollection SP = new System.Data.SqlClient.SqlCommand().Parameters;
			SP.AddWithValue("@TID", TPassTB.Text);
			SP.AddWithValue("@AID", AID);
			ViewDR.DataSource = DAL.ViewData.MyDT("SELECT FormRefererID, UserID, Date, AndwerDate, UserAnswerID, UnitID, [Desc], FormNameID, State  FROM FormReferer  WHERE (SUBSTRING(CAST(UserID AS varchar(64)), 0, 4) = @TID) AND (FormRefererID = @AID)", SP);
			ViewDR.DataBind();
		}
	}
}