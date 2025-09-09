<%@ Page Title="" Language="C#" MasterPageFile="~/Pages.master" AutoEventWireup="true" CodeBehind="Content.aspx.cs" Inherits="Portal.Content" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
	<script runat="server">
		string SiteTitle = "";
		string SetName(string aa)
		{
			SiteTitle = aa;
			return "";
		}
	</script>
	<table style="width: 100%" border="0">
		<tr>
			<td>
				<asp:PlaceHolder ID="PH0" runat="server" />
			</td>
		</tr>
	</table>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrBeforTitle,0)%>
	<%=SiteTitle%><div style="float: left; vertical-align: top; margin: 3px">
		<asp:PlaceHolder ID="RatePH" runat="server" />
		<img onclick="window.print()" id="printico" style="cursor: hand; width: 20px; height: 20px" alt="<%=HttpContext.GetGlobalResourceObject("resource", "Print").ToString()%>"
			title="<%=HttpContext.GetGlobalResourceObject("resource", "Print").ToString()%>"
			src="/Images/print_icon.gif" /></div>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterTitle,0)%>
	<table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td>
				<asp:Repeater ID="TextDG" runat="server">
					<ItemTemplate>
						<%# SetName(Tools.Tools.SetItemTitle(this.Page, Eval("Name").ToString().Trim()))%>
						<%# Eval("Texts").ToString().Trim()%>
					</ItemTemplate>
				</asp:Repeater>
			</td>
		</tr>
	</table>
	<table style="width: 100%" border="0">
		<tr>
			<td>
				<asp:PlaceHolder ID="PH1" runat="server" />
			</td>
			<td>
				<asp:PlaceHolder ID="PH3" runat="server" />
			</td>
			<td>
				<asp:PlaceHolder ID="PH5" runat="server" />
				<asp:PlaceHolder ID="PH6" runat="server" />
			</td>
		</tr>
	</table>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterBody,0)%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterFooter,0)%>
</asp:Content>
