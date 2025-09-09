using System;
using System.Data.SqlClient;
namespace NewsService
{
	public partial class UserAccess : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (ADAL.A_CheckData.PageAccess(this.Page.ToString()))
				Response.Redirect("/Logins");
			if (!IsPostBack)
				UpdateGrid();
			if (Request.QueryString["ID"] == null || Request.QueryString["ID"] == "")
				Response.Redirect("/Logins");
		}
		void UpdateGrid()
		{
			AccessCBL.DataSource = ADAL.A_ViewData.MyDR("SELECT UserID, Name  FROM AdminUsers where unitid=" + ADAL.A_CheckData.GetUnitID());
			AccessCBL.DataBind();
			SetAccess();
		}
		private void SetAccess()
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@ID", Request.QueryString["ID"]);
			SqlDataReader MyRead = ADAL.A_ViewData.MyDR("SELECT UserID FROM FormViewAccess where FormNameID=@ID", SP);
			while (MyRead.Read())
			{
                try
                {
                    AccessCBL.Items[AccessCBL.Items.IndexOf(AccessCBL.Items.FindByValue(Tools.MyCL.MGInt(MyRead, 0).ToString()))].Selected = true;
                }
                catch { }
			}
			MyRead.Close();
			MyRead.Dispose();
		}
		protected void SaveBTN_Click(object sender, EventArgs e)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@ID", Request.QueryString["ID"]);
			ADAL.A_ExecuteData.DeleteData("DELETE FROM FormViewAccess WHERE (FormNameID = @ID) AND (UnitID = " + ADAL.A_CheckData.GetUnitID() + ")", SP);
			for (int i = 0; i < AccessCBL.Items.Count; i++)
			{
				if (AccessCBL.Items[i].Selected == true)
					ADAL.A_ExecuteData.InsertData("INSERT INTO FormViewAccess ( FormNameID, UserID, UnitID) VALUES (@ID," + AccessCBL.Items[i].Value + "," + ADAL.A_CheckData.GetUnitID() + ")", SP);
			}
            Tools.Tools.Alert(Page, GetGlobalResourceObject("resource", "SuccessChange").ToString());
		}
	}
}