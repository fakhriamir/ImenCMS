using System; using DAL;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
namespace Portal
{
	public partial class MyForms : System.Web.UI.Page
	{
		int MyViewItems = 0;
		int GeustItem = 0;
		protected void Page_Load(object sender, EventArgs e)
		{
			Tools.Tools.SetPageHit(this.Page.ToString(),this.Page.ClientQueryString);

			if (string.IsNullOrEmpty(Request.QueryString["ID"]))
				Response.Redirect("Default.aspx");
			int RID;
			SearchDiv.Visible = false;
			SendBTN.Visible = false;
			if (!int.TryParse(Request.QueryString["ID"], out RID))
				Response.Redirect("Default.aspx");
			if (!CheckActive(RID))
				Response.Redirect("Default.aspx");

			if (!DAL.CheckData.CheckAccess(Tools.MyVar.SiteGuest.Forms, RID))
				Response.Redirect("/Members/MemberLogin.aspx");


			FillTitle(RID);
			Default.Adv.PageID = 21;
			if (GeustItem == 1)
			{
				if (DAL.CheckData.GuestUserLoginID() == 0)
					Response.Redirect("/Members/MemberLogin.aspx");
				FillData(RID, false, true);
				return;
			}
			if (CheckAddData(RID))
				ViewItems(RID);
			if (CheckSearch(RID))
				SearchData(RID);
			if (CheckViewData(RID))
				FillData(RID);
			
		}
		void FillTitle(int ID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@FormNameID", ID);
			SqlDataReader MyRead = ViewData.MyDR1("SELECT FormNameID, Name, Text, ViewItem,Guestitem FROM FormName WHERE  (UnitID = " + Tools.Tools.GetViewUnitID + ") and (FormNameID = @FormNameID)", SP);
			if (MyRead.Read())
			{
				Label1.Text = TitleLB.Text = Tools.Tools.SetItemTitle(this.Page, Tools.MyCL.MGStr(MyRead, 1));
				if (TitleLB.Text.IndexOf("فيش") != -1)
					isFish = true;
				TextLB.Text = Tools.MyCL.MGStr(MyRead, 2);
				MyViewItems = Tools.MyCL.MGInt(MyRead, 3);
				GeustItem = Tools.MyCL.MGInt(MyRead, 4);
			}
			MyRead.Close(); MyRead.Dispose();
		}
		bool isFish = false;
		void FillData(int ID, bool Filter = false,bool GuestFilter=false)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@ID", ID);
			string OutText = "<table dir=\"rtl\" id=\"My\" align=\"center\" cellspacing=\"0\" cellpadding=\"2\" border=\"1\">";
			int ItemLen = Tools.Tools.ConvertToInt32(DAL.ExecuteData.CNTData("SELECT FormItemID, FormNameID, ItemName, Type, ISNull, Len, Sort FROM FormItem WHERE (FormNameID =@ID)", SP));
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT FormItemID, FormNameID, ItemName, Type, ISNull, Len, Sort FROM FormItem WHERE (FormNameID =@ID) AND Type!=2 order by sort", SP);
			ArrayList MyItemHead = new ArrayList();
			string FileID = "";
			while (MyRead.Read())
			{
				MyItemHead.Add(Tools.MyCL.MGStr(MyRead, 2));
			}
			MyRead.Close(); MyRead.Dispose();			
			string SearchFilter = "";
			if (Filter)
			{
				SearchFilter = " and userid in (select userid from FormChar where ( FormItemID = @ItemID) AND (Text LIKE '%" + SearchText.Text.Replace("'", "") + "%')) ";
				SP.AddWithValue("@ItemID", SearchType.SelectedValue);
			}
			if (GuestFilter)
			{
				int ITID = DAL.ExecuteData.CNTData("SELECT FormItemID  FROM FormItem  WHERE (FormNameID = @ID) AND (ItemName LIKE '%#1#%')",SP);
				SearchFilter = " and userid in (select userid from FormChar where ( FormItemID = @ItemID) AND (Text LIKE '" + DAL.ExecuteData.CNTDataStr("select pcode from guestinfo where GuestID=" + DAL.CheckData.GuestUserLoginID()) + "')) ";
				SP.AddWithValue("@ItemID", ITID);				
			}
			if(MyItemHead.Count!=0)
                MyRead = DAL.ViewData.MyDR1("SELECT top (" + MyItemHead.Count + ")  FormChar.FormCharID, FormChar.FormItemID, FormChar.UserID, FormChar.Text FROM FormChar inner join FormItem on FormChar.FormItemID=FormItem.FormItemID WHERE (FormChar.FormNameID = @ID) " + SearchFilter + "  ORDER BY UserID, FormItem.sort", SP);
			else
                MyRead = DAL.ViewData.MyDR1("SELECT  FormChar.FormCharID, FormChar.FormItemID, FormChar.UserID, FormChar.Text FROM FormChar  inner join FormItem on FormChar.FormItemID=FormItem.FormItemID WHERE (FormChar.FormNameID = @ID) " + SearchFilter + "  ORDER BY UserID, FormItem.sort", SP);
			int i = 0;
			string FildIDtemp = "";
			//if(MyItemHead.len
			if (!GuestFilter)
			{
				while (MyRead.Read())
				{
					if (i > MyItemHead.Count)
						continue;
					OutText += "<tr><td>" + MyItemHead[i] + "</td><td>" + Tools.MyCL.MGStr(MyRead, 3) + "</td></tr>";
					i++;
				}
			}
			else
			{
				int cc = 1;
				OutText += "<tr>";
				if(isFish)
					PrintSallery.InnerHtml = "<input style=\"Width:150px\" type=\"button\" onclick=\"Javascript:window.open('/Members/PrintSallery-" + ID + ".aspx','','width=700px,height=450px,scrollbars=yes,resizable=yes');\"  value=\"چاپ فیش حقوقی\"/>";
				while (MyRead.Read())
				{
					if (i > MyItemHead.Count)
						continue;
					if (Tools.MyCL.MGStr(MyRead, 3) != "" && Tools.MyCL.MGStr(MyRead, 3) != "0")
					{
						OutText += "<td style=\"background-color:#ced0f8\">" + MyItemHead[i] + "</td><td style=\"background-color:#fbe4fe\">" + Tools.MyCL.MGStr(MyRead, 3) + "</td>";
						if (cc % 2 == 0)
							OutText += "</tr><tr>";
						cc++;
					}
					i++;
				}
			}
			FildIDtemp = FileID;
			MyRead.Close(); MyRead.Dispose();
			OutText += "</table>";
			BodyDiv.InnerHtml = OutText;
		}
		bool CheckViewData(int ID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@ID", ID);
			int MyType = ExecuteData.CNTData("SELECT ViewItem FROM FormName WHERE (FormNameID = @ID)", SP);
			if (MyType == 0)
				return false;
			return true;
		}
		void SearchData(int ID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@FormNameID", ID);
			SearchType.Items.Clear();
			SqlDataReader MyRead = ViewData.MyDR1("SELECT FormItemID, ItemName  FROM FormItem  WHERE (FormNameID = @FormNameID) AND (Type = 0 OR Type = 1) ORDER BY Sort", SP);
			while (MyRead.Read())
			{
				SearchType.Items.Add(new ListItem(Tools.MyCL.MGStr(MyRead, 1), Tools.MyCL.MGInt(MyRead, 0).ToString()));
			}
			MyRead.Close(); MyRead.Dispose();
			SearchDiv.Visible = true;
		}
		bool CheckSearch(int ID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@ID", ID);
			int MyType = ExecuteData.CNTData("SELECT SearchData FROM FormName WHERE (FormNameID = @ID)", SP);
			if (MyType == 0)
				return false;
			return true;
		}
		bool CheckAddData(int ID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@ID", ID);
			int MyType = ExecuteData.CNTData("SELECT AddData FROM FormName WHERE (FormNameID = @ID)", SP);
			if (MyType == 0)
				return false;
			return true;
		}
		void ViewItems(int ID)
		{
			bool Referer = false;
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@FormNameID", ID);
			Referer = Convert.ToBoolean(DAL.ExecuteData.CNTData("SELECT Referer FROM FormName WHERE (FormNameID = @FormNameID)", SP));
			RefererDiv.Visible = Referer;
			//RefererBTN.Attributes.Add("onclick","SelectPrepMsg('/FormsReferer.aspx?ID="+ID+"')");

			SendBTN.Visible = true;
			//ViewData.MyConnection.Close();
			FormPN.Controls.Add(new LiteralControl("<table align=center>"));
			SqlDataReader MyRead = ViewData.MyDR1("SELECT FormItemID, FormNameID, ItemName, Type, ISNull, Len, Sort  FROM FormItem  WHERE (FormNameID = @FormNameID) order by sort", SP);
			while (MyRead.Read())
			{
				Label MyLB = new Label();
				int ItemType = Tools.MyCL.MGInt(MyRead, 3);
				if (ItemType == 0 || ItemType == 1 || ItemType == 4)//text , int,Date
				{
					MyLB.Text = Tools.MyCL.MGStr(MyRead, 2).Replace("#2#", "") + GetStar(Tools.MyCL.MGInt(MyRead, 4));
					TextBox MyTB = new TextBox();
					MyTB.ID = "MyForm" + Tools.MyCL.MGInt(MyRead, 0);
					if (ItemType == 4)//tarikh filed
						MyTB.CssClass = "CalLoad";
					try
					{
						MyTB.MaxLength = Tools.Tools.ConvertToInt32(Tools.MyCL.MGStr(MyRead, 5));
						if (Tools.Tools.ConvertToInt32(Tools.MyCL.MGStr(MyRead, 5)) > 100)
							MyTB.TextMode = TextBoxMode.MultiLine;
					}
					catch { MyTB.MaxLength = 30; }
					if (Tools.MyCL.MGStr(MyRead, 2).IndexOf("#2#") != -1)
					{
						if (DAL.CheckData.GuestUserLoginID() >= 1)
						{
							MyTB.Text += DAL.ExecuteData.CNTDataStr("SELECT RTRIM(CAST(PCode AS char(10))) + '- ' + RTRIM(Name) + ' ' + rtrim(Family) AS Expr1 FROM GuestInfo  WHERE (GuestID = " + DAL.CheckData.GuestUserLoginID() + ")");
							MyTB.Enabled = false;
						}
					}
					FormPN.Controls.Add(new LiteralControl("<tr><td align=right>"));
					FormPN.Controls.Add(MyLB);
					FormPN.Controls.Add(new LiteralControl("</td><td align=right>"));
					FormPN.Controls.Add(MyTB);
					FormPN.Controls.Add(new LiteralControl("</td></tr>"));
				}
				else if (ItemType == 2)//Picture
				{
					MyLB.Text = Tools.MyCL.MGStr(MyRead, 2) + GetStar(Tools.MyCL.MGInt(MyRead, 4));
					System.Web.UI.HtmlControls.HtmlInputFile MyCo = new System.Web.UI.HtmlControls.HtmlInputFile();
					MyCo.ID = "MyForm" + Tools.MyCL.MGInt(MyRead, 0);
					MyCo.Attributes["runat"] = "server";

					FormPN.Controls.Add(new LiteralControl("<tr><td align=right>"));
					FormPN.Controls.Add(MyLB);
					FormPN.Controls.Add(new LiteralControl("</td><td align=right>"));
					FormPN.Controls.Add(MyCo);
					FormPN.Controls.Add(new LiteralControl("</td></tr>"));
				}
				else if (ItemType == 3)//Drowpdownlist
				{
					MyLB.Text = Tools.MyCL.MGStr(MyRead, 2) + GetStar(Tools.MyCL.MGInt(MyRead, 4));
					DropDownList MyDL = new DropDownList();
					MyDL.ID = "MyForm" + Tools.MyCL.MGInt(MyRead, 0);
					Regex MySplit = new Regex("@");
					string[] MyItem = MySplit.Split(Tools.MyCL.MGStr(MyRead, 5));
					for (int i = 0; i < MyItem.Length; i++)
					{
						if (MyItem[i].Trim() != "")
							MyDL.Items.Add(new ListItem(MyItem[i], MyItem[i]));
					}
					FormPN.Controls.Add(new LiteralControl("<tr><td align=right>"));
					FormPN.Controls.Add(MyLB);
					FormPN.Controls.Add(new LiteralControl("</td><td align=right>"));
					FormPN.Controls.Add(MyDL);
					FormPN.Controls.Add(new LiteralControl("</td></tr>"));
				}
			}
			MyRead.Close(); MyRead.Dispose();
			//ViewData.MyConnection.Close();
			FormPN.Controls.Add(new LiteralControl("</table>"));
		}
		bool CheckActive(int ID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@ID", ID);
			int MyType = ExecuteData.CNTData("SELECT Active FROM FormName WHERE (FormNameID = @ID)", SP);
			if (MyType == 0)
				return false;
			return true;
		}
		string GetStar(int MyVal)
		{
			if (MyVal == 0)
				return "<font color=red>*</font>";
			return "";
		}
		protected void Send_Click(object sender, EventArgs e)
		{
			string UserID = Guid.NewGuid().ToString();
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@FormNameID", Tools.Tools.ConvertToInt32(Request.QueryString["ID"]));
		

			/*check data*/
			SqlDataReader MyRead = ViewData.MyDR1("SELECT FormItemID, FormNameID, ItemName, Type, ISNull, Len, Sort  FROM FormItem  WHERE (FormNameID = @FormNameID)", SP);
			while (MyRead.Read())
			{
				if (Tools.MyCL.MGInt(MyRead, 3) != 2)
				{
					string Par = Request.Params["ctl00$ctl00$MyBody$PageBody$MyForm" + Tools.MyCL.MGInt(MyRead, 0)];
					if (Tools.MyCL.MGInt(MyRead, 4) == 0 && Par == "")
					{
						MyRead.Close(); MyRead.Dispose();
						//ViewData.MyConnection.Close();
						MessLB.Text = "فیلد های ستاره دار را پر نمایید<br/>";
						return;
					}
				}
				else
				{
					HttpPostedFile Myf = Request.Files["ctl00$ctl00$MyBody$PageBody$MyForm" + Tools.MyCL.MGInt(MyRead, 0)];
					if (Myf == null || Myf.FileName == null || Myf.FileName == "")
						continue;
					Tools.MyCL.MGStr(MyRead, 5);
					string MyFilePath = Myf.FileName.Substring(Myf.FileName.IndexOf(".") + 1).ToLower();

					if (Tools.MyCL.MGStr(MyRead, 5).ToLower().IndexOf("@" + MyFilePath + "@") == -1)
					{
						MyRead.Close(); MyRead.Dispose();
						//ViewData.MyConnection.Close();
						MessLB.Text = GetGlobalResourceObject("resource", "InvalidPerfixMsg").ToString()+"<br/>";
						return;
					}
				}
			}
			MyRead.Close(); MyRead.Dispose();
			//ViewData.MyConnection.Close();
			/**/

			//Referer Check
			bool Referer = false;
			Referer = Convert.ToBoolean(DAL.ExecuteData.CNTData("SELECT Referer  FROM FormName WHERE (FormNameID = @FormNameID)", SP));
			//Email Check
			string EmailAddress = DAL.ExecuteData.CNTDataStr("SELECT Email  FROM FormName WHERE (FormNameID = @FormNameID)", SP);

			string EmailSTR = "";
			MyRead = ViewData.MyDR1("SELECT FormItemID, FormNameID, ItemName, Type, ISNull, Len, Sort  FROM FormItem  WHERE (FormNameID = @FormNameID)", SP);
			while (MyRead.Read())
			{
				if (Tools.MyCL.MGInt(MyRead, 3) != 2)
				{
					string Par = Request.Params["ctl00$ctl00$MyBody$PageBody$MyForm" + Tools.MyCL.MGInt(MyRead, 0)];
					//  if (Par != "")
					//  {
					SP.Clear();
					if (Tools.MyCL.MGStr(MyRead, 2).IndexOf("#2#") != -1)
					{
						if (DAL.CheckData.GuestUserLoginID() >= 1)
							SP.AddWithValue("@TXT", DAL.ExecuteData.CNTDataStr("SELECT RTRIM(CAST(PCode AS char(10))) + '- ' + RTRIM(Name) + ' ' + rtrim(Family) AS Expr1 FROM GuestInfo  WHERE (GuestID = " + DAL.CheckData.GuestUserLoginID() + ")"));
					}
					else
						SP.AddWithValue("@TXT", Par);
					EmailSTR += "<br>" + Tools.MyCL.MGStr(MyRead, 2) + ": " + Par + "  ";
					ExecuteData.AddData("INSERT INTO FormChar( FormItemID, Text,UserID,FormNameID) VALUES (" + Tools.MyCL.MGInt(MyRead, 0) + ",@TXT,'" + UserID + "'," + Tools.MyCL.MGInt(MyRead, 1) + ")", SP);
				}
				else
				{
					HttpPostedFile Myf = Request.Files["ctl00$ctl00$MyBody$PageBody$MyForm" + Tools.MyCL.MGInt(MyRead, 0)];
					byte[] content = new byte[Myf.ContentLength];
					Stream imagestream = Myf.InputStream;
					imagestream.Read(content, 0, content.Length);
					imagestream.Close();
					SP.Clear();
					SP.Add("@TXT", System.Data.SqlDbType.Image);
					SP["@TXT"].Value = content;
					
					SP.AddWithValue("@FileType", Myf.FileName.Substring(Myf.FileName.IndexOf(".") + 1));
					// SP.AddWithValue("@TXT", content);
					ExecuteData.AddImages("INSERT INTO FormFile( FormItemID, Files,UserID,FileType,FormNameID) VALUES (" + Tools.MyCL.MGInt(MyRead, 0) + ",@TXT,'" + UserID + "',@FileType," + Tools.MyCL.MGInt(MyRead, 1) + ")", SP);
				}
			}
			MyRead.Close(); MyRead.Dispose();
			if (EmailAddress.Trim() != "")
			{
				Tools.MySendMail.SendMail(EmailAddress, "فرم الکترونیک", EmailSTR);
			}
			if (Referer)
			{
				SP.Clear();
				SP.AddWithValue("@FormNameID", Tools.Tools.ConvertToInt32(Request.QueryString["ID"]));
				SP.AddWithValue("@UserID",UserID);
				SP.AddWithValue("@UnitID" , Tools.Tools.GetViewUnitID);
				DAL.ExecuteData.AddData("INSERT INTO FormReferer (UserID, UnitID,FormNameID) VALUES (@UserID,@UnitID,@FormNameID)", SP);
				int CurID = DAL.ExecuteData.CNTData("SELECT IDENT_CURRENT('FormReferer') ");
				Tools.Tools.Alert(Page, "کاربر گرامی درخواست شما با موفقیت ارسال شد.<br> شماره پیگیری شما <font color=red>" + CurID + "</font> و رمز پیگیری شما <font color=red>" + UserID.Substring(0, 3) + "</font> می باشد.");
			}
			else
				Tools.Tools.Alert(Page,GetGlobalResourceObject("resource", "SuccessForm").ToString());
			//ViewData.MyConnection.Close();
		}
		protected void SearchBTN_Click(object sender, EventArgs e)
		{
			int RID;
			if (!int.TryParse(Request.QueryString["ID"], out RID))
				Response.Redirect("Default.aspx");

			FillData(RID, true);
		}
	}
}