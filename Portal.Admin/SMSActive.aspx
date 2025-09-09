<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SMSActive.aspx.cs" Inherits="Portal.Admin.SMSActive" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table align="center" width="80%" >
		<tr class="RowHead">
			<td align="center">
			</td>
			<td align="center">
				<%=GetGlobalResourceObject("resource", "PackageTitle")%>
			</td>
			<td align="center">
				<%=GetGlobalResourceObject("resource", "Description")%>
			</td>
			<td align="center">
				<%=GetGlobalResourceObject("resource", "TotalSMS")%>
			</td>
			<td align="center">
				<%=GetGlobalResourceObject("resource", "PriceRial")%>
			</td>
			<td align="center">
				<%=GetGlobalResourceObject("resource", "Buy")%>
			</td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" EnableViewState="false" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("SMSBuyID").ToString().Trim()%>
					</td>
					<td align="right">
						&nbsp;<%# Eval("Name").ToString().Trim() %>
					</td>
					<td align="right">
						&nbsp;<%# Eval("Text").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("CNT").ToString().Trim() %>
					</td>
					<td align="left">
						&nbsp;<%# Eval("Payment").ToString().Trim() %>
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/Shop.png" Width="30px" Height="30px" CommandArgument='<%# Eval("SMSBuyID").ToString().Trim() %>'
							CommandName="Buy" ID="BuyBTN" ToolTip="<%$resources:resource, ConnectBank %>" AlternateText="<%$resources:resource, ConnectBank %>"
							runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
</asp:Content>
