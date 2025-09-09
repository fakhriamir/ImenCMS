<%@ Page Title="" EnableViewState="false" Language="C#" MasterPageFile="~/Pages.master" AutoEventWireup="true" CodeBehind="Movies.aspx.cs" Inherits="Portal.Movies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
	<script runat="server" type="text/C#">
	string GetPicture(string PicName)
		{
			if (PicName.Trim() == "")
				return "";
			return "<img class=\"MoviePageImage\" src=\"/Files/" + Tools.Tools.GetViewUnitID + "/Images/Movies/"+PicName+"\" />";
		}
	</script>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrBeforTitle,0)%>
	<%=HttpContext.GetGlobalResourceObject("resource", "Def_Movie_Title").ToString()%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterTitle,0)%>
	<script language="javascript" type="text/javascript" src="/Scripts/swfobject.js"></script>
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
						<asp:Repeater ID="MovieDG" runat="server">
							<ItemTemplate>
								<%# GetPicture( Eval("PicName").ToString().Trim())%>
							
								<%# Eval("MovieType").ToString().Trim()%>
								- <a href='/Movies/<%# Eval("MovieID").ToString().Trim()%>/<%#Tools.Tools.UrlWordReplace(Eval("Name").ToString().Trim())%>.aspx'>
									<%#Tools.Tools.SetItemTitle(this.Page, Eval("Name").ToString().Trim())%></a>
								<a href="<%#GetMovieAddress( Eval("MovAddress").ToString().Trim())%>">
									<img src="/Images/Media-Down.gif" alt="<%=HttpContext.GetGlobalResourceObject("resource", "Download").ToString()%>"
										title="<%=HttpContext.GetGlobalResourceObject("resource", "Download").ToString()%>" /></a>
								<%--<a href="/PlayMedia-M<%# Eval("MovieID").ToString().Trim()%>.aspx">
									<img src="/Images/Media_Play.gif" alt="<%=HttpContext.GetGlobalResourceObject("resource", "Play").ToString()%>"
										title="<%=HttpContext.GetGlobalResourceObject("resource", "Play").ToString()%>" /></a>--%>
								<br />
								<p align="center">
									<div id="player1" align="center">
									</div>
									<script type="text/javascript">
										var so = new SWFObject('/Scripts/Myplayer.swf', 'ply1', '520', '440', '9');
										so.addParam('allowfullscreen', 'true');
										so.addParam('allowscriptaccess', 'always');
										so.addParam('flashvars', 'file=<%#GetMovieAddress( Eval("MovAddress").ToString().Trim())%>&lightcolor=green');
                    so.write('player1');
									</script>
								</p>
								<%# Eval("text").ToString().Trim()%>
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
						<asp:Repeater runat="server" ID="TitleDG">
							<ItemTemplate>
								<div class="MovieAllPageDiv">
								<%# GetPicture( Eval("PicName").ToString().Trim())%>
								<a href='/Movies/<%# Eval("MovieID").ToString().Trim()%>/<%#Tools.Tools.UrlWordReplace(Eval("Name").ToString().Trim())%>.aspx'><%# Eval("MovieType").ToString().Trim()%>
								- 
									<%# Eval("Name").ToString().Trim()%></a> 
								<a href="<%# GetMovieAddress(Eval("MovAddress").ToString().Trim())%>">
										<img src="/Images/Media-Down.gif" alt="<%=HttpContext.GetGlobalResourceObject("resource", "Download").ToString()%>"
											title="<%=HttpContext.GetGlobalResourceObject("resource", "Download").ToString()%>" /></a>
								<%--<a href="/PlayMedia-M<%# Eval("MovieID").ToString().Trim()%>.aspx">
									<img src="/Images/Media_Play.gif" alt="<%=HttpContext.GetGlobalResourceObject("resource", "Play").ToString()%>"
										title="<%=HttpContext.GetGlobalResourceObject("resource", "Play").ToString()%>" /></a>--%>
								<br />
									</div>
							
							</ItemTemplate>
						</asp:Repeater>
							
					</td>
				</tr>
				<tr>
					<td colspan="3" align="center">
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
