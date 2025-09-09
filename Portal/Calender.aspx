<%@ Page Title="" EnableViewState="false" Language="C#" MasterPageFile="~/Pages.master"	AutoEventWireup="true" CodeBehind="Calender.aspx.cs" Inherits="Portal.MyCalender" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrBeforTitle,0)%>
	<%=HttpContext.GetGlobalResourceObject("resource", "Def_Calender_Title").ToString()%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterTitle,0)%>
	<table style="width: 100%" border="0">
		<asp:MultiView runat="server" ID="ItemMV">
			<asp:View runat="server" ID="TextItem">
				<tr>
					<td>
						<asp:Repeater ID="ContactDG" runat="server">
							<ItemTemplate>
								<p align="center">
									<b>
										<%#Tools.Tools.SetItemTitle(Page, Eval("Title").ToString().Trim())%></b></p>
								<br />
								<p align="justify">
									<%# Eval("Comment").ToString().Trim()%>
								</p>
							</ItemTemplate>
						</asp:Repeater>
					</td>
				</tr>
			</asp:View>
			<asp:View runat="server" ID="TitleTop">
				<tr>
					<td>
						<p align="center">
							<asp:Label ID="ThisDayLB" runat="server" Text="" /></p>
					</td>
				</tr>
				<tr>
					<td align="right">
						<asp:Repeater runat="server" ID="ArticleTitleDR">
							<ItemTemplate>
								<a href="/Calender/<%# Eval("ID")%>/<%#Tools.Tools.UrlWordReplace(Eval("Title").ToString().Trim())%>">
									<%# Eval("Title").ToString().Trim()%></a><br />
								<hr>
							</ItemTemplate>
						</asp:Repeater>
					</td>
				</tr>
			</asp:View>
		</asp:MultiView>
	</table>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterBody,0)%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterFooter,0)%>
</asp:Content>
