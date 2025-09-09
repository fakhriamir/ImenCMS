<%@ Page Title="" Language="C#" MasterPageFile="~/Pages.master" AutoEventWireup="true" CodeBehind="CompanyInfo.aspx.cs" Inherits="Portal.CompanyInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrBeforTitle,0)%>
    <%=HttpContext.GetGlobalResourceObject("resource", "Def_CompanyInfo_Title")%>
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
						<asp:Repeater ID="CoInfoDG" runat="server">
							<ItemTemplate>
								<div id="CompanyInfoTable">
									<div id="CompanyInfoChildTitle"><%# Tools.Tools.SetItemTitle(this.Page, Eval("Title").ToString().Trim())%></div>
									<div id="CompanyInfoDesc">
										<div>توضیحات : <%# Eval("Des").ToString().Trim() %></div>
										<div>آدرس : <%# Eval("Adr").ToString().Trim() %></div>
										<div>تلفن : <%# Eval("Tel").ToString().Trim() %></div>
										<div>فکس : <%# Eval("Fax").ToString().Trim() %></div>
										<div>پست الکترونیکی : <%# Eval("Email").ToString().Trim() %></div>
										<div>سایت : <a href='http://<%# Eval("Site").ToString().Trim() %>' target="_blank"><%# Eval("Site").ToString().Trim() %></a></div>
									</div>
								
							

                                 
                              <div id="CompanyInfoHit">
										<%=HttpContext.GetGlobalResourceObject("resource", "Visitor").ToString()%>
										<%# Eval("Hit").ToString().Trim() %>
									</div>
									</div>
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
					<td>
						<fieldset id="CompanyInfoSearch">
							<legend>جستجو</legend>
<asp:TextBox ID="SearchTB" runat="server"></asp:TextBox>
						<asp:Button ID="SearchBTN" runat="server" Text="<%$ resources: resource, Def_Search_Title %>" OnClick="SearchBTN_Click" />
				
						</fieldset>
							</td>
				</tr>
				<tr>
					<td colspan="3" align="right">
						<asp:Repeater runat="server" ID="ArticleTitleDR">
							<ItemTemplate>
								
									<div style="" id="CompanyInfoListBox">
										<div id="CompanyInfoTitle">
											<a href='/CompanyInfo/<%# Eval("CompanyInfoID").ToString().Trim()%>/<%# Tools.Tools.UrlWordReplace(Eval("Title").ToString().Trim())%>.aspx'>
												<%# Eval("Title").ToString().Trim()%></a>
										</div>
	
									</div>
								
							</ItemTemplate>
							<SeparatorTemplate>
							</SeparatorTemplate>
						</asp:Repeater>
					</td>
				</tr>
				<tr>
					<td colspan="3" align=center>
						<asp:LinkButton ID="lnkPreviousPage" runat="server" OnClick="lnkPreviousPage_Click"> <%=HttpContext.GetGlobalResourceObject("resource", "Back").ToString()%></asp:LinkButton>
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
						<asp:LinkButton ID="lnkNextPage"  runat="server" OnClick="lnkNextPage_Click"> <%=HttpContext.GetGlobalResourceObject("resource", "Next").ToString()%></asp:LinkButton>
					</td>
				</tr>
			</asp:View>
		</asp:MultiView>
	</table>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterBody,0)%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterFooter,0)%>
</asp:Content>
