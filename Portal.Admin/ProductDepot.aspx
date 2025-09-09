<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ProductDepot.aspx.cs" Inherits="Portal.Admin.ProductDepot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0" cellpadding="2" width="100%"
		border="1">
		<tr class="RowHead">
			<td>
				<%=GetGlobalResourceObject("resource", "ProuctID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Name")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "cnt")%>
			</td>
			
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" >
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("ProductID").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("Name").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("ProSum").ToString().Trim()%>
					</td>
					
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
</asp:Content>
