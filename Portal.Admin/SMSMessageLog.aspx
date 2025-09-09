<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SMSMessageLog.aspx.cs" Inherits="Portal.Admin.SMSMessageLog" %>

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
		<tr>
			<td>گیرنده
			</td>
			<td>
				<asp:TextBox ID="ReciveMobTB" runat="server"></asp:TextBox>
			</td>
			<td>
				<asp:Button ID="ReciveMobBTN" runat="server"
					Text="<%$ resources: resource, Filter %>" OnClick="ReciveMobBTN_Click" />
			</td>
		</tr>
		<tr>
			<td>فرستنده
			</td>
			<td>
				<asp:TextBox ID="SenderMobBTN" runat="server"></asp:TextBox>
			</td>
			<td>
				<asp:Button ID="Button4" runat="server"
					Text="<%$ resources: resource, Filter %>" OnClick="ReciveMobBTN_Click" />
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
				<%=GetGlobalResourceObject("resource", "_Date")%>
			</th>

			<th>
				<%=GetGlobalResourceObject("resource", "SendID")%>
			</th>
			<th>
				<%=GetGlobalResourceObject("resource", "Total")%>
			</th>
			<th>
				<%=GetGlobalResourceObject("resource", "Status")%>
			</th>
			<th>
				
			</th>
		</tr>
		<asp:Repeater ID="LogsDR" runat="server" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr>
					<td>
						<%# Eval("sms_userid").ToString().Trim()%>
					</td>
					<td>
						<%# Eval("Text").ToString().Trim()%>
					</td>
					<td>
						<%# Eval("MobNo").ToString().Trim()%>
					</td>
					<td>
						<%#Tools.Calender.MyPDate(Eval("Date").ToString().Trim())%><br />
						<%#Tools.Calender.GetTime(Eval("Date").ToString().Trim())%>
					</td>
					<td align="center">
						<%# Eval("Result").ToString().Trim()%>
					</td>
					<td align="center">
						<%# Eval("SendCount").ToString().Trim()%>
					</td>
					<td>
						<%# Eval("FromLineNo").ToString().Trim()%>
					</td>
					<td>
						<asp:Button Width="50px" Text="حذف" CommandArgument='<%# Eval("SMS_SMSLogID").ToString().Trim()%>'
							CommandName="DEL" ID="Button1" runat="server" />
						<asp:Button Width="50px" Text="-1" CommandArgument='<%# Eval("SMS_SMSLogID").ToString().Trim()%>'
							CommandName="SET-1" ID="Button2" runat="server" />
						<asp:Button Width="100px" Text="برگشت اعتبار" CommandArgument='<%# Eval("SMS_SMSLogID").ToString().Trim()%>'
							CommandName="SETCount" ID="Button3" runat="server" /></td>
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
				<asp:LinkButton ID="lnkNextPage" runat="server" OnClick="lnkNextPage_Click" Text="<%$ resources: resource, Next %>"> </asp:LinkButton>
			</td>
		</tr>
	</table>
</asp:Content>
