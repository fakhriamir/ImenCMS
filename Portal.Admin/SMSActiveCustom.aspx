<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SMSActiveCustom.aspx.cs" Inherits="Portal.Admin.SMS_ActiveCustom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="rtl" id="My" align="center" cellspacing="1" cellpadding="2" width="100%"
		border="1">
		<tr class="RowHead">
			<td>
				كد
			</td>
			<td>
				توضیح
			</td>
			<td>
				کاربر سیستم
			</td>
			<td>
				تعداد
			</td>
			<td>
				کاربر
			</td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("SMS_SMSActiveCustomeID").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("comm").ToString().Trim()%></td>
					<td align="center">
						&nbsp;<%# Eval("sms_userid").ToString().Trim()%></td>
					<td align="center">
						&nbsp;<%# Eval("count").ToString().Trim()%></td>
					<td align="center">
						&nbsp;<%# Eval("userid").ToString().Trim()%></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
