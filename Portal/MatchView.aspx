<%@ Page Title="" Language="C#" MasterPageFile="~/Pages.master" AutoEventWireup="true" CodeBehind="MatchView.aspx.cs" Inherits="Portal.MatchView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrBeforTitle,0)%>
	<%=Tools.Tools.SetItemTitle(this.Page,GetMatchTitle())%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterTitle,0)%>
	<div class="MatchViewHeaderDiv">
		<%=Tools.Tools.GetSetting(469,"")%>
	</div>
	<table class="Def_Match_Table">
		<tr>
			<td>
				<div dir="<%=Tools.Tools.PageDir%>"><%=GetMatchMatn() %></div>
				<div class="Def_Match_Div" dir="<%=Tools.Tools.PageDir%>">
					<%=HttpContext.GetGlobalResourceObject("resource", "NameFamily").ToString()%>:<br />
					<asp:TextBox CssClass="Def_Match_NameTB" ID="MatchNameTB" runat="server" /><br>
					<%=HttpContext.GetGlobalResourceObject("resource", "Email").ToString()%>:<br />
					<asp:TextBox CssClass="Def_Match_MailTB" ID="MatchMailTB" runat="server" />
					<br>
					<%=HttpContext.GetGlobalResourceObject("resource", "Mobile").ToString()%>:<br />
					<asp:TextBox CssClass="Def_Match_MobTB" ID="MatchMobTB" runat="server" />
					<br>
					<div style="display: <%=Tools.Tools.GetSetting(468,"none")%>">
						<%=HttpContext.GetGlobalResourceObject("resource", "Gender").ToString()%>:<br />
						<asp:DropDownList ID="GenderDL" runat="server">
							<asp:ListItem Value="1" Text="مرد" />
							<asp:ListItem Value="2" Text="زن" />
						</asp:DropDownList>
						<br>
						<%=HttpContext.GetGlobalResourceObject("resource", "City").ToString()%>:<br />
						<asp:TextBox CssClass="Def_Match_MobTB" ID="CityTB" runat="server" MaxLength="32" />
						<br>
						<%=HttpContext.GetGlobalResourceObject("resource", "Education").ToString()%>:<br />
						<asp:DropDownList ID="EducationDL" runat="server">
							<asp:ListItem Value="1" Text="زیر دیپلم" />
							<asp:ListItem Value="2" Text="دیپلم" />
							<asp:ListItem Value="3" Text="فوق دیپلم" />
							<asp:ListItem Value="4" Text="لیسانس" />
							<asp:ListItem Value="5" Text="فوق لیسانس" />
							<asp:ListItem Value="6" Text="دکترا" />
						</asp:DropDownList>
						<br>
					</div>
					<div id="MatchTimer"></div>
				</div>
			</td>
		</tr>
		<tr>
			<td align="right">
				<asp:Repeater ID="QuestionDR" runat="server"
					OnItemDataBound="QuestionDR_ItemDataBound">
					<ItemTemplate>
						<input id="MatchIDTB" runat="server" type="hidden" value='<%# Eval("MatchID") %>' />
						<b>
							<%# (Tools.Tools.ConvertToInt32(DataBinder.Eval(Container, "ItemIndex"))+1) %>-
							<%# Eval("Question") %></b>
						<asp:RadioButtonList ID="MatchItemRBL" DataTextField="Name" DataValueField="MatchItemID" TextAlign="Right" runat="server">
						</asp:RadioButtonList>
						<%--<asp:Repeater ID="MatchItemDR" runat="server">
							<ItemTemplate>
								<%# Eval("Name") %>
							</ItemTemplate>
						</asp:Repeater>--%>
					</ItemTemplate>
				</asp:Repeater>
				<asp:Label ID="MessLB" runat="server" Text="" ForeColor="Red"></asp:Label>
				<asp:Button ID="btnMatch" runat="server" OnClick="Match_click" Text="<%$ resources: resource, SentBotton %>" />
			</td>
		</tr>
	</table>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterBody,0)%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterFooter,0)%>
	<script language="javascript" type="text/javascript">
		var FinT = "<%=FinishTime.Trim()%>";
		var interval
		function myFunction() {
			if (FinT == "")
				return;
			var e = new Date();

			var enddate = new Date(FinT);
			var x = document.getElementById("MatchTimer");
			if (enddate.getTime() - e.getTime() > 0)
				x.innerHTML = "زمان باقیمانده: " + parseInt((enddate.getTime() - e.getTime()) / 1000 / 60 / 60) + ":" + parseInt((enddate.getTime() - e.getTime()) / 1000 / 60 % 60) + ":" + parseInt((enddate.getTime() - e.getTime()) / 1000 % 60);
			else {
				jAlert("زمان مسابقه به پایان رسیده است");
				clearInterval(interval);
			}
		}
		//myFunction();
		if (FinT != "")
			interval = setInterval(myFunction, 1000);
	</script>
</asp:Content>
