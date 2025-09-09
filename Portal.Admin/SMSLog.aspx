<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SMSLog.aspx.cs" Inherits="Portal.Admin.SMS_Log" %>

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
			<asp:Button ID="DellArchive" runat="server" OnClick="DellArchive_Click" Text="حذف آرشیو" /></td>
		</tr>
		<tr>
			<td>کد زیر 27
			</td>
			<td>
			<a href="/SMSLog.aspx?ID=2"><%=ADAL.A_ExecuteData.CNTData("select  count(*)  FROM SMS_SMSLog WHERE (Result <1000) AND (Result > 0)  AND (Result !=27) ") %></a>
			</td>
			<td>
				<asp:Button ID="Button4" runat="server"
					Text="حذف" OnClick="Button4_Click"  />
			<asp:Button ID="SendToQueueBTN" runat="server"
					Text="انتقال به صف" OnClick="SendToQueueBTN_Click" />
			</td>
		</tr>
		<tr>
			<td>در صف تاکنون
			</td>
			<td>
			<a href="/SMSLog.aspx?ID=1">	<%=ADAL.A_ExecuteData.CNTData("select  count(*) from SMS_SMSLog WHERE  (Result = - 1) AND (Date < GETDATE()) ") %></a>
		<asp:Button ID="Button6" runat="server"
					Text="ریست سرویس" OnClick="Button6_Click" />	</td>
			<td>
			آینده:<%=ADAL.A_ExecuteData.CNTData("select  count(*) from SMS_SMSLog WHERE  (Result = - 1) AND (Date> GETDATE()) ") %>
			</td>
		</tr>
	</table>
	<asp:Button ID="Button5" runat="server" Text="ارسال صف" OnClick="Button5_Click" />
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
						<%# Eval("ResultGUID").ToString().Trim()%><br />
						<%# Eval("result").ToString().Trim()%>
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
						<asp:Button Width="50px" Text="-20" CommandArgument='<%# Eval("SMS_SMSLogID").ToString().Trim()%>'
							CommandName="SET-20" ID="Button7" runat="server" />
						<asp:Button Width="100px" Text="برگشت اعتبار" CommandArgument='<%# Eval("SMS_SMSLogID").ToString().Trim()%>'
							CommandName="SETCount" ID="Button3" runat="server" /><%# Eval("status").ToString().Trim()%></td>
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
