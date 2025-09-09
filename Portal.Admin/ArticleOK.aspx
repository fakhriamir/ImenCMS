<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ArticleOK.aspx.cs" Inherits="Portal.Admin.ArticleOK" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

	<%-- <asp:DropDownList ID="TypeDL" runat="server" DataValueField="NewsSubjectID" DataTextField="Subject">
			 </asp:DropDownList>--%>
	<asp:Repeater ID="ArticleDR" runat="server" OnItemCommand="ViewDR_ItemCommand">	
		<ItemTemplate>
			<table class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>" dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="Table1" cellspacing="0" cellpadding="1" width="80%" border="1">
				<tr>
					<td style="height: 22px">
						<%=GetGlobalResourceObject("resource", "Title")%>:
						<%# Eval("Title").ToString().Trim()%>
					</td>
					<td>
						<%# Eval("wdate").ToString()%> - <%=GetGlobalResourceObject("resource", "User")%>:	<%# Eval("username").ToString().Trim()%>
					</td>
				</tr>				
				<tr>
					<td colspan="2">
						<%=GetGlobalResourceObject("resource", "Abstract")%>:<%# Eval("Chekide").ToString().Trim()%></td>
				</tr>
				<tr>
					<td colspan="2">
						<%=GetGlobalResourceObject("resource", "Text")%>:<%# Eval("text").ToString().Trim()%></td>
				</tr>
				<tr>
					<td colspan="2">
						<%=GetGlobalResourceObject("resource", "Author")%>:<%# Eval("author").ToString().Trim()%></td>
				</tr>
				<tr>
					<td colspan="2">
						<img class="Def_article_img" onerror="this.style.display='none'"
												src='/files/<%=ADAL.A_CheckData.GetUnitID()%>/ArticlePic/<%# Eval("Image").ToString().Trim() %>' />
					</td>
				</tr>
				<tr>					
					<td >
						<a href="/Articles.aspx?ID=<%# Eval("articleid") %>">
							<img src="/Imgs/edit.gif" width="18" height="17" border="0" align="absmiddle" alt=<%=GetGlobalResourceObject("resource", "Edit")%>></a>
										
						<asp:Button Text="<%$ resources: resource,ArticleOK %>" CommandArgument='<%# Eval("articleid") %>' CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource,ArticleOK %>" AlternateText="<%$ resources: resource,ArticleOK %>" runat="server" />
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif" Width="16px" Height="16px" CommandArgument='<%# Eval("articleid") %>' CommandName="DEL"	ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
						<input style="width: 80px" type="button" onclick="SelectPrepMsg('/Messagesend.aspx?UserID=<%#Eval("UserID")%>&Content=<%# HttpUtility.UrlEncode("مقاله:"+ Eval("Title").ToString().Trim())%>',560,340);"
							value="<%=GetGlobalResourceObject("resource", "Reference")%>" />
							<asp:Button Text="<%$ resources: resource,BackArticle %>" CommandArgument='<%# Eval("articleid") %>' CommandName="ArticleBack"
							ID="Button1" ToolTip="<%$ resources: resource,BackArticle %>" AlternateText="<%$ resources: resource,BackArticle %>" runat="server" />
					
					</td>
					<td>
					<input style="width: 70px" type="button" onclick="SelectPrepMsg('/GuestAccess.aspx?ID=<%# Eval("ArticleID") %>&Type=<%=((int)Tools.MyVar.SiteGuest.Article)%>',560,490);"
							value="<%=GetGlobalResourceObject("resource", "Permission")%>" />
						<%#ADAL.A_CheckData.ReturnAccessName(Tools.MyVar.SiteGuest.Article, Eval("ArticleID").ToString())%>
				</td>
				</tr>
			</table>
		</ItemTemplate>
	</asp:Repeater>
	<br/>
	<asp:TextBox ID="NewsIDTB" Width="0" Style="display: none" Height="0" runat="server"
		Text=""/>
	

</asp:Content>
