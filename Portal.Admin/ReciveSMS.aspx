<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ReciveSMS.aspx.cs" Inherits="Portal.Admin.ReciveSMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="1" cellpadding="2" width="100%"
		border="1">
		<asp:Repeater runat="server" ID="ViewDR">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("FromNo").ToString().Trim()%>
					</td>
					<td align="center">
						&nbsp;<%# Eval("Text").ToString().Trim()%>
					</td>
					<td align="center">
						&nbsp;<%# Eval("PDate").ToString().Trim()%>
					</td>
					</td><td align="center">
						&nbsp;<%# Eval("Date").ToString().Trim()%>
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument=''
							CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
</asp:Content>
