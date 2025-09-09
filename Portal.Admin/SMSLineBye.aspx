<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SMSLineBye.aspx.cs" Inherits="Portal.Admin.SMS_LineBye" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0" cellpadding="2" width="100%" border="0">
		
		<tr  class="RowHead">
			<td>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "SMSNo")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "_Date")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "UserID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "_Name")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Email")%>
			</td><td>
				<%=GetGlobalResourceObject("resource", "MobileNo")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Date")%>
			</td>
		<%--	<td>
				اعتبار
			</td>--%>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# (Container.ItemIndex+1).ToString().Trim()%>
					</td>
					<td align="center">
						&nbsp;<%# Eval("[LineNo]").ToString().Trim()%>
					</td>
					<td align="center">
						&nbsp;<%#Tools.Calender.MyPDate(Eval("StDate").ToString().Trim())%>
					</td>
					<td align="center">
						&nbsp;<%#Eval("SMS_UserID").ToString()%>
					</td>
					<td align="center">
						&nbsp;<%#Eval("Name")%>
					</td>
					<td align="center">
						&nbsp;<%#Eval("Email").ToString()%>
					</td>
					<td align="center">
						&nbsp;<%#Eval("MobileNo").ToString()%>
					</td>
					<td align="center">
						&nbsp;<%#GetEndSMSDate(Eval("SMS_UserID").ToString())%>
					</td>
					<%--<td align="center">
						&nbsp;<%#Tools.SMS.GetSahandSamanehCredit(Eval("[LineNo]").ToString().Trim())%>
					</td>--%>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
</asp:Content>
