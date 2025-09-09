using System;
using DAL;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace Portal
{
	public partial class MatchView : System.Web.UI.Page
	{
		int MatchGroupID = -1;
		public string FinishTime="";
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["ID"] == null && Request.QueryString["ID"].Trim() == "")
				Response.Redirect("/Default.aspx");

			if (!int.TryParse(Request.QueryString["ID"], out MatchGroupID))
				Response.Redirect("/Default.aspx");

			if (DAL.ExecuteData.CNTDataStr("SELECT StartTime  FROM MatchGroup  WHERE (MatchGroupID =" + MatchGroupID + ")") != "" && DAL.ExecuteData.CNTDataStr("SELECT FinishTime  FROM MatchGroup  WHERE (MatchGroupID =" + MatchGroupID + ")") != "")
			{
				if (MatchGroupID != DAL.ExecuteData.CNTData("SELECT MatchGroupID  FROM MatchGroup  WHERE (GETDATE() BETWEEN StartTime AND FinishTime) AND (MatchGroupID =" + MatchGroupID + ")"))
				{
					Tools.Tools.Alert(Page, "زمان مسابقه هنوز شروع نشده است یا به اتمام رسیده است");
					return;
				}
			}
			if (DAL.ExecuteData.CNTData("select state from  MatchGroup WHERE (MatchGroupID =" + MatchGroupID + ")") == 2)
			{
				Tools.Tools.Alert(Page, " مسابقه به اتمام رسیده است");//<br>برندگان:<br><font color=red>" + Winnersname(MatchGroupID.ToString())+"</font>");
				return;
			
			}
			if (!DAL.CheckData.CheckAccess(Tools.MyVar.SiteGuest.Match, MatchGroupID))
				Response.Redirect("/Members/MemberLogin.aspx");
			if (DAL.CheckData.GuestUserLoginID() != 0)
			{
				MatchNameTB.Text = DAL.ExecuteData.CNTDataStr("SELECT pcode FROM GuestInfo  WHERE (GuestID = " + DAL.CheckData.GuestUserLoginID() + ")")+"- ";
				MatchNameTB.Text += DAL.ExecuteData.CNTDataStr("SELECT RTRIM(Name) + ' ' + Family AS Expr1  FROM GuestInfo  WHERE (GuestID = " + DAL.CheckData.GuestUserLoginID() + ")");
				MatchNameTB.Enabled = false;
				//MatchMobTB.Text = "0";
				//MatchMobTB.Enabled = false;
			}
			ViewState["MatchID"] = "";
			if (!IsPostBack)
			{
				SqlParameterCollection SP = new SqlCommand().Parameters;
				SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);
				SP.AddWithValue("@MatchID", MatchGroupID);
				if (DAL.ExecuteData.CNTDataStr("SELECT StartTime  FROM MatchGroup  WHERE (MatchGroupID =" + MatchGroupID + ")") != "" && DAL.ExecuteData.CNTDataStr("SELECT FinishTime  FROM MatchGroup  WHERE (MatchGroupID =" + MatchGroupID + ")") != "")
					FinishTime = DAL.ExecuteData.CNTDataStr("SELECT FinishTime  FROM MatchGroup  WHERE (MatchGroupID =@MatchID) and (ShowTimer=1)", SP);
				string OrderByStr = " ORDER BY Matchid  ";
				if (DAL.ExecuteData.CNTData("SELECT IsRandom  FROM MatchGroup  WHERE (MatchGroupID =@MatchID)", SP) == 1)
					OrderByStr = " order by newid() ";
				QuestionDR.DataSource = ViewData.MyDT("SELECT Question,MatchID FROM Match where MatchGroupID=@MatchID   and unitid=@UnitID "+OrderByStr, SP);
				QuestionDR.DataBind();
			}				
		}
		string Winnersname(string MatchGroupID)
		{
			string OutText = "";
			SqlParameterCollection SP = new SqlCommand().Parameters;
			//System.wTools.Tools.GetViewUnitID = "";
			SP.AddWithValue("@MatchID", MatchGroupID);
			SqlDataReader MyRead = ViewData.MyDR1("SELECT Name  FROM MatchAnswer  WHERE (MatchgroupID = @MatchID) AND (Winner = 1) ", SP);
			while (MyRead.Read())
				OutText += "&nbsp;-&nbsp;" + Tools.MyCL.MGStr(MyRead, 0);
			MyRead.Close(); MyRead.Dispose();
			//ViewData.MyConnection.Close();
			if (OutText == "")
				return "";
			return OutText.Substring(7);
		}
		protected void Match_click(object sender, EventArgs e)
		{
			if (DAL.ExecuteData.CNTDataStr("SELECT StartTime  FROM MatchGroup  WHERE (MatchGroupID =" + MatchGroupID + ")") != "" && DAL.ExecuteData.CNTDataStr("SELECT FinishTime  FROM MatchGroup  WHERE (MatchGroupID =" + MatchGroupID + ")") != "")
			{
				if (MatchGroupID != DAL.ExecuteData.CNTData("SELECT MatchGroupID  FROM MatchGroup  WHERE (GETDATE() BETWEEN StartTime AND FinishTime) AND (MatchGroupID =" + MatchGroupID + ")"))
				{
					Tools.Tools.Alert(Page, "زمان مسابقه هنوز شروع نشده است یا به اتمام رسیده است");
					return;
				}
			}

			if (MatchNameTB.Text.Trim() == "" || MatchMobTB.Text.Trim()=="")
			{
				Tools.Tools.Alert(Page, "نام و موبایل خود را وارد نماييد");
				return;
			}
			ArrayList MyAnswer=new ArrayList();			
			for(int i = 0; i < Request.Params.Count; i++)
			{
				if (Request.Params.AllKeys[i].ToLower().IndexOf("MatchIDTB".ToLower()) != -1)
				{
					string MatchID = Request.Params[i];
					string MyMatchItemID="";
					if(Request.Params.AllKeys[i+1].IndexOf("MatchItemRBL")!=-1)
						MyMatchItemID = Request.Params[i+1];
					else
					{
						Tools.Tools.Alert(Page, "شما به همه سوالات پاسخ نداده اید");
						return;
					}
					MyAnswer.Add(new object[] {MatchID,MyMatchItemID});
				}
			}
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@Email", MatchMailTB.Text);
			SP.AddWithValue("@Name", MatchNameTB.Text);
			
			SP.AddWithValue("@MobNo", MatchMobTB.Text);
			if (ExecuteData.CNTData("select count(*) from matchanswer where mobno=@MobNo and matchGroupid=" + MatchGroupID, SP) != 0)
			{
				Tools.Tools.Alert(Page, "شما قبلا شرکت کرده ايد<br>");
				return;
			}
			SP.AddWithValue("@MatchGroupID", MatchGroupID);
			SP.AddWithValue("@MatchIP", Tools.Tools.GetUserIPAddress());
			SP.AddWithValue("@Gender", GenderDL.SelectedValue);
			SP.AddWithValue("@City", CityTB.Text);
			SP.AddWithValue("@Education", EducationDL.SelectedValue);
			ExecuteData.AddData("INSERT INTO MatchAnswer (MatchGroupID,IP,Name,Email,MobNo,Gender,Education,City) VALUES (@MatchGroupID,@MatchIP,@Name,@Email,@MobNo,@Gender,@Education,@City)", SP);
			int MatchAnswerID = ExecuteData.CNTData("SELECT IDENT_CURRENT('MatchAnswer') AS Expr1");
			for (int i = 0; i < MyAnswer.Count; i++)
			{
				ExecuteData.AddData("INSERT INTO MatchAnswerItem(MatchAnswerID,MatchID, MatchItemID) VALUES (" + MatchAnswerID + "," + ((Object[])MyAnswer[i])[0].ToString() + "," + ((Object[])MyAnswer[i])[1].ToString() + ")");		
			}
			Tools.Tools.Alert(Page, "اطلاعات شما برای مسابقه ارسال شد. با تشکر ار شرکت شما در مسابقه");
		}
		public string GetMatchTitle()
		{
			string Title = DAL.ExecuteData.CNTDataStr("SELECT name  FROM MatchGroup  WHERE (MatchGroupID =" + MatchGroupID + ")");
			//Tools.Tools.SetItemTitle(this.Page, Title);
			return Title;
		}
		public string GetMatchMatn()
		{
			string Title = DAL.ExecuteData.CNTDataStr("SELECT matn  FROM MatchGroup  WHERE (MatchGroupID =" + MatchGroupID + ")");
			return Title;
		}
		protected void QuestionDR_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				RadioButtonList radioButtonMatchItem = (RadioButtonList)e.Item.FindControl("MatchItemRBL");
				HtmlInputHidden MyMatchID = (HtmlInputHidden)e.Item.FindControl("MatchIDTB");
				radioButtonMatchItem.DataSource = ViewData.MyDT("SELECT MatchItemID, Name FROM MatchItem WHERE (MatchID = " + MyMatchID.Value + ") order by MatchItemID ", null);
				radioButtonMatchItem.DataBind();
			}
		}
	}
}