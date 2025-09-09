<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SMSLogRecive.aspx.cs" Inherits="Portal.Admin.SMS_LogRecive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table2" cellspacing="1" cellpadding="1">
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "FilterUserID")%>
			</td>
			<td>
				<asp:TextBox ID="IDFilterTB" runat="server"></asp:TextBox>
			</td>
			<td>
				<asp:Button ID="FilterBTN" runat="server" Text="<%$ resources: resource, Filter %>" OnClick="FilterBTN_Click" />
			</td>
		</tr>
	</table>
	<table align="center" class="TableColor" dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" cellspacing="3" cellpadding="3" width="100%" border="0">
		<tr>
			<th>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</th>
			<th>
				<%=GetGlobalResourceObject("resource", "Text")%>
			</th>
			<th>
				<%=GetGlobalResourceObject("resource", "MobileNo")%>
			</th>
				<th>
				<%=GetGlobalResourceObject("resource", "SMSNo")%>
			</th>
			<th>
				<%=GetGlobalResourceObject("resource", "_Date")%>
			</th>			
			<th>
				<%=GetGlobalResourceObject("resource", "ReceiveID")%>
			</th>
			
		</tr>
		<asp:Repeater ID="LogsDR" runat="server">
			<ItemTemplate>
				<tr>
					<td>
						<%# Eval("sms_userid").ToString().Trim()%>
					</td>
					<td>
						<%# Eval("Text").ToString().Trim()%>
					</td>
					<td>
						<%# Eval("FromNo").ToString().Trim()%>
					</td>
					<td>
						<%# Eval("ToNo").ToString().Trim()%>
					</td>
					<td>
						<%#Tools.Calender.MyPDate(Eval("Date").ToString().Trim())%>
					</td>
					<td align="center">
						<%# Eval("MessageID").ToString().Trim()%>
					</td>					
				</tr>
			</ItemTemplate>
		</asp:Repeater>
		<tr>
			<td colspan="5" align="center">
				<asp:LinkButton ID="lnkPreviousPage" runat="server" OnClick="lnkPreviousPage_Click" Text="<%$ resources: resource, Previews %>"> </asp:LinkButton>
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
				<asp:LinkButton ID="lnkNextPage" runat="server" OnClick="lnkNextPage_Click" Text="<%$ resources: resource, Next %>"></asp:LinkButton>
			</td>
		</tr>
	</table>
</asp:Content>
