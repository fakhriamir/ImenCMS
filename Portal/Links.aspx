<%@ Page Title="" EnableViewState="false" Language="C#" MasterPageFile="~/Pages.master"
	AutoEventWireup="true" CodeBehind="Links.aspx.cs" Inherits="Portal.Links" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrBeforTitle,0)%>
	<%=HttpContext.GetGlobalResourceObject("resource", "Def_Links_Title").ToString()%>
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
					<td align="right">
						<asp:Repeater ID="TextDG" runat="server">
							<ItemTemplate>
								<img style="width: 100px;" onerror="this.style.display='none'" border='0' src='/Files/<%=Tools.Tools.GetViewUnitID%>/LinkPic/<%# Eval("ImgAddress").ToString().Trim() %>' />
								<a href="/Links/<%# Eval("LinkID").ToString().Trim()%>/<%#Tools.Tools.UrlWordReplace(Eval("Name").ToString().Trim())%>.aspx">
									<%#Tools.Tools.SetItemTitle(this.Page, Eval("Name").ToString().Trim())%></a> (<%# Eval("TypeName").ToString().Trim()%>)
								<br />
								<%=HttpContext.GetGlobalResourceObject("resource", "HitNo").ToString()%>:
								<%# Eval("hit").ToString().Trim()%>
								<br>
								<%# Eval("Text").ToString().Trim()%>
								<a href="<%# Eval("Address").ToString().Trim()%>">
									<%# Eval("Address").ToString().Trim()%></a>
								<br />
							</ItemTemplate>
						</asp:Repeater>
					</td>
				</tr>
				<tr>
					<td>
						<asp:PlaceHolder ID="CommentPL" runat="server" />
					</td>
				</tr>
			</asp:View>
			<asp:View runat="server" ID="TitleTop">
				<tr>
					<td colspan="3" align="right">
						<asp:Repeater runat="server" ID="TitleDR">
							<ItemTemplate>
								<img style="width: 100px;float:right;margin:3px;" onerror="this.style.display='none'" border='0' src='/Files/<%=Tools.Tools.GetViewUnitID%>/LinkPic/<%# Eval("ImgAddress").ToString().Trim() %>' />
								<a href="/Links/<%# Eval("LinkID").ToString().Trim()%>/<%# Eval("Name").ToString().Trim()%>.aspx">
									<%# Eval("Name").ToString().Trim()%></a> (<%# Eval("TypeName").ToString().Trim()%>)
								<br />
						<%=HttpContext.GetGlobalResourceObject("resource", "VisitorCNT").ToString()%>
								<%# Eval("hit").ToString().Trim()%>
								<br>
								
								<a href="/Links/<%# Eval("LinkID").ToString().Trim()%>/<%# Eval("Name").ToString().Trim()%>.aspx">
									<%# Eval("Address").ToString().Trim()%></a>
								<br />
								<br />
								<br />
							</ItemTemplate>
						</asp:Repeater>
					</td>
				</tr>
				<tr>
					<td colspan="3" align=center>
						<asp:LinkButton ID="lnkPreviousPage" runat="server" OnClick="lnkPreviousPage_Click" Text="<%$ resources: resource, Back %>" />
						&nbsp;
						<asp:Repeater ID="rptPages" runat="server" OnItemCommand="rptPages_ItemCommand" OnItemCreated="rptPages_ItemCreated">
							<ItemTemplate>
								<asp:LinkButton ID="btnPage" CommandName="Page" CommandArgument='<%#Container.DataItem %>'
									runat="server"><%# Container.DataItem %>
								</asp:LinkButton>
							</ItemTemplate>
							<SeparatorTemplate>
								&nbsp;-&nbsp;
							</SeparatorTemplate>
						</asp:Repeater>
						&nbsp;
						<asp:LinkButton ID="lnkNextPage" runat="server" OnClick="lnkNextPage_Click" Text="<%$ resources: resource, Next %>" />
					</td>
				</tr>
			</asp:View>
		</asp:MultiView>
	</table>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterBody,0)%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterFooter,0)%>
</asp:Content>
