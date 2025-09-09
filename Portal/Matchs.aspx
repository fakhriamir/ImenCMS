<%@ Page Title="" EnableViewState="false" Language="C#" MasterPageFile="~/Pages.master"
	AutoEventWireup="true" CodeBehind="Matchs.aspx.cs" Inherits="Portal.Matchs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
	
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrBeforTitle,0)%>
	<%=HttpContext.GetGlobalResourceObject("resource", "Match_Title").ToString()%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterTitle,0)%>
	<table style="width: 100%" border="0">
		<asp:Repeater ID="TextDG" runat="server">
			<ItemTemplate>
				<tr>
					<td style="width: 20%" align="center">
						<%=HttpContext.GetGlobalResourceObject("resource", "Question").ToString()%>:
					</td>
					<td align="right">
						<%#Tools.Tools.SetItemTitle(this.Page, Eval("Question").ToString().Trim())%>
					</td>
				</tr>
				<tr>
					<td align="center">
						<%=HttpContext.GetGlobalResourceObject("resource", "ParticipantNO").ToString()%>:
					</td>
					<td align="right">
						<%# Portal.Matchs.GetMatchCNT(Eval("MatchID").ToString().Trim())%>
						<%=HttpContext.GetGlobalResourceObject("resource", "Person").ToString()%>:
					</td>
				</tr>
				<tr>
					<td align="center">
						<%=HttpContext.GetGlobalResourceObject("resource", "Winners").ToString()%>:
					</td>
					<td align="right">
						<%# Winnersname(Eval("MatchID").ToString().Trim())%>
					</td>
				</tr>
				<tr>
					<td align="center">
						<%=HttpContext.GetGlobalResourceObject("resource", "Gift").ToString()%>:
					</td>
					<td align="right">
						<%# Eval("jayeze").ToString().Trim()%>
					</td>
				</tr>
				<tr>
					<td>
					</td>
					<td>
						&nbsp;
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterBody,0)%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterFooter,0)%>
</asp:Content>
