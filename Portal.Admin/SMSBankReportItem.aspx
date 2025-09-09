<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="SMSBankReportItem.aspx.cs" Inherits="Portal.Admin.SMS_BankReportItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<br>
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="1" cellpadding="2" width="100%"
		border="1">
		<tr class="RowHead">
			<td>
				<%=GetGlobalResourceObject("resource", "FactorCode")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "PriceRial")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "_Date")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "UserID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Bank")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">&nbsp;<%# Eval("Factorid").ToString().Trim()%></td>
					<td align="center">&nbsp;<%# Eval("money")%></td>
					<td align="center">&nbsp;<%#Tools.Calender.MyPDate(Eval("date").ToString().Trim())%></td>
					<td align="center">&nbsp;<%#Eval("userid").ToString()%></td>
					<td align="center">&nbsp;<%#Eval("BankType")%></td>
					<td align="center">&nbsp;<%#Eval("BankID").ToString()%></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>

	<%}%>
</asp:Content>
