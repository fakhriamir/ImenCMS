<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Suggest.aspx.cs" Inherits="Portal.Admin.Suggest" %>

<asp:Content ContentPlaceHolderID="head" runat="server" ID="ContentA1">
</asp:Content>
<asp:Content ContentPlaceHolderID="Body" runat="server" ID="aa">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" align="center" id="Table1" cellspacing="0" cellpadding="1" width="80%">
		<tr>
			<td colspan="2" class="RowHead">
				<%=GetGlobalResourceObject("resource", "SugestMng")%>
			</td>
		</tr>
	</table>
	<br>
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0" cellpadding="2" width="100%"
		border="1">
		<tr class="RowHead">
			<td>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Del")%>
			</td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR"   OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("SuggestID").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("Name").ToString().Trim() %><br />
						&nbsp;<%# Eval("Email").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("subject").ToString().Trim() %><br />
						&nbsp;<%# Eval("text").ToString().Trim() %>
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("SuggestID").ToString().Trim() %>'
							CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
