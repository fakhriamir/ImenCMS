<%@ Page Title="" Language="C#" MasterPageFile="~/Pages.master" AutoEventWireup="true" CodeBehind="Campaign.aspx.cs" Inherits="Portal.Campaign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
	<script runat="server">
		string MyTitle = Tools.Tools.GetSetting(479, HttpContext.GetGlobalResourceObject("resource", "Def_Campaign_Title").ToString());
		string SetTitle(string InText)
		{
			MyTitle = InText;
			return InText;
		}
	</script>
	<script language="javascript" type="text/javascript">

		function SendCompaignRate(ID) {
			var UserID = "<%=GetUserID()%>";
			if (UserID == "") {
				jAlert("شما باید وارد سیستم شده باشید<br>");
				return;
			}
			//document.getElementById("RateComm" + ID).style.display = "";
			GetAjaxVal("/Ajax.aspx?MyType=CaRa" + ID + "L" + UserID, 15);
		}
		function CampaignRateBack(ID) {
			//alert(ID+"ddd");
			//document.getElementById("RateComm" + ID.substr(1)).style.display = "none";
			if (ID.substr(0, 1) == "5")
				jAlert("شما عضو این کمپین هستید");
			else if (ID.substr(0, 1) == "6")
				jAlert("صفحه را یک بار دیگر بارگذاری نمایید");
			else if (ID.substr(0, 1) == "7") {
				//document.getElementById("MyRatep" + ID.substr(1)).innerHTML = parseInt(document.getElementById("MyRatep" + ID.substr(1)).innerHTML) + 1;
				jAlert("عضویت شما با موفقیت ثبت شد");
			}
		}
	</script>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrBeforTitle,0)%>
	<%=MyTitle%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterTitle,0)%>
	<table style="width: 100%" border="0">
		<asp:MultiView runat="server" ID="ItemMV">
			<asp:View runat="server" ID="TextItem">
				<tr>
					<td>
						<asp:PlaceHolder ID="RatePH" runat="server" />
					</td>
				</tr>
				<tr>
					<td>
						<asp:Repeater ID="ArticleDG" runat="server">
							<ItemTemplate>
								<div id="CampaignTable">
									<div id="CampaignTopTBL">
										<div id="Campaignright" class="cover">
											<img class="DefCampaignimg"	src='/Files/<%=Tools.Tools.GetViewUnitID%>/Images/Campaign/<%# Eval("PicName").ToString().Trim() %>' />
										</div>
										
										<div id="CampaignLeft">
											<div id="CampaignTitle"><%# SetTitle(Tools.Tools.SetItemTitle(this.Page, Eval("Title").ToString().Trim()))%></div>
											<div id="CampaignHit">
												<%=HttpContext.GetGlobalResourceObject("resource", "Visitor").ToString()%>
												<%# Eval("Hit").ToString().Trim() %>
											</div>
											<div id="Campaigndate"><%#Tools.Calender.ReverseDate( Eval("Date").ToString()) %></div>
										</div>

									</div>
									<div id="CampaignText" >
										<%# Eval("Matn").ToString().Trim()%>
									</div>
									<div id="CampaignUsers">
											تعداد اعضای این کمپین <%#Tools.Tools.RepNumberToPersian( GetCampaignCNT( Eval("CampaignID").ToString().Trim())) %> نفر می باشد.
										</div>
										<div id="CampaignBTN">
											<input type="button" onclick="SendCompaignRate(<%# Eval("CampaignID").ToString().Trim() %>)" value="من هستم" class="CompaignInputRate button green"/>
										</div>
								</div>
							</ItemTemplate>
						</asp:Repeater>
					</td>
				</tr>
				<tr>
					<td><br />
						<section class="panel detail" id="detail-about">
												<header>
													<div class="title">نظرات شما</div>
												</header>
												<article style="height:300px;overflow-y:scroll">
													<asp:PlaceHolder ID="CommentPL" runat="server" />
												</article>
											</section>
					</td>
				</tr>
			</asp:View>
			<asp:View runat="server" ID="TitleTop">
				<tr>
					<td colspan="3" align="center">
						<div style="text-align: center; width: 1200px;margin:0px auto">
							<asp:Repeater runat="server" ID="ArticleTitleDR">
								<ItemTemplate>
									<div class="CampaignTitleRep">
										<div id="Campaignimg" class="cover">
											<a href='/Campaign/<%# Eval("CampaignID").ToString().Trim()%>/<%# Tools.Tools.UrlWordReplace(Eval("Title").ToString().Trim())%>.aspx'>
												<img class="DefCampaignimgRep" src='/Files/<%=Tools.Tools.GetViewUnitID%>/Images/Campaign/<%# Eval("PicName").ToString().Trim() %>' /></a>
										</div>
										<div id="CampaignListBox">
											<div id="CampaignTitle">
												<a href='/Campaign/<%# Eval("CampaignID").ToString().Trim()%>/<%# Tools.Tools.UrlWordReplace(Eval("Title").ToString().Trim())%>.aspx'>
													<%# Eval("Title").ToString().Trim()%></a>
											</div>
											<div id="CampaignHitRep">
												<%=HttpContext.GetGlobalResourceObject("resource", "HitNo").ToString()%>:
                                    <%# Eval("Hit").ToString().Trim()%>
											</div>
											<div id="CampaignUsersRep">
												<%#Tools.Tools.RepNumberToPersian( GetCampaignCNT( Eval("CampaignID").ToString().Trim())) %>
											</div>
										</div>
									</div>
								</ItemTemplate>
							</asp:Repeater>
						</div>
					</td>
				</tr>
				<tr>
					<td colspan="3" align="center">

						<a runat="server" id="BackHref"><%=HttpContext.GetGlobalResourceObject("resource", "Back").ToString()%></a>
						&nbsp;
						<asp:Repeater ID="rptPages" runat="server">
							<ItemTemplate>
								<a <%# Container.DataItem.ToString().Trim() == (CurrentPage + 1).ToString()? "class=\"CurentPage\"" : "class=\"PagingLNK\"" %> href="/Campaign/<%=GetTypeLink()%>p<%# Container.DataItem %>.aspx"><%# Container.DataItem %>
								</a>
							</ItemTemplate>
							<SeparatorTemplate>
								&nbsp;-&nbsp;
							</SeparatorTemplate>
						</asp:Repeater>
						&nbsp;
						<a runat="server" id="NextHref"><%=HttpContext.GetGlobalResourceObject("resource", "Next").ToString()%></a>
					</td>
				</tr>
			</asp:View>
		</asp:MultiView>
	</table>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterBody,0)%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterFooter,0)%>
</asp:Content>
