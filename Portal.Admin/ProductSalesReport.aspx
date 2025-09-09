<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ProductSalesReport.aspx.cs" Inherits="Portal.Admin.ProductSalesReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table2" cellspacing="1" cellpadding="1">
		
	
	<%if (ViewDR.Items.Count != 0)
   {%>
   <%=sood.ToString() %>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0" cellpadding="2" width="100%"
		border="0">
		<tr class="RowHead">
			<td>
				
			</td>
			<td>
			
			</td>
			<td>
				
			</td>
			
		</tr>
		<asp:Repeater runat="server" ID="ViewDR">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("ProductID").ToString().Trim()%></td>
					<td align="center">
						&nbsp;<%# Eval("Name")%></td>
					
					<td align="center">
						&nbsp;<%#Eval("CNT").ToString()%></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	
	<%}%>
</asp:Content>
