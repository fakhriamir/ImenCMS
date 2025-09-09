<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SMSUsers.aspx.cs" Inherits="Portal.Admin.SMS_Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<fieldset style="width: 400px; margin: auto;">
		<legend>فیلتر</legend>
		<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor"
			align="center" id="Table2" cellspacing="1" cellpadding="1">
			<tr>
				<td>
					<%=GetGlobalResourceObject("resource", "FilterUserWith1400")%>
				</td>
				<td>
					<asp:TextBox ID="IDFilterTB" runat="server"></asp:TextBox>
				</td>
				<td>
					<asp:Button ID="FilterBTN" runat="server" Text="<%$ resources: resource, Filter%>"
						OnClick="FilterBTN_Click" />
				</td>
			</tr>
			<tr>
				<td>
					<%=GetGlobalResourceObject("resource", "FilterUserWithout1400")%>
				</td>
				<td>
					<asp:TextBox ID="Not1400TB" runat="server"></asp:TextBox>
				</td>
				<td>
					<asp:Button ID="Not1400BTN" runat="server" Text="<%$ resources: resource, Filter%>"
						OnClick="Not1400BTN_Click" />
				</td>
			</tr>
			<tr>
				<td>
					<%=GetGlobalResourceObject("resource", "MobileFilter")%>
				</td>
				<td>
					<asp:TextBox ID="MobileTB" runat="server"></asp:TextBox>
				</td>
				<td>
					<asp:Button ID="MobileBTN" runat="server" Text="<%$ resources: resource, Filter%>"
						OnClick="MobileBTN_Click" />
				</td>
			</tr>
			<tr>
				<td>
					<%=GetGlobalResourceObject("resource", "FilterEmail")%>
				</td>
				<td>
					<asp:TextBox ID="EmailTB" runat="server"></asp:TextBox>
				</td>
				<td>
					<asp:Button ID="EmailBTN" runat="server" Text="<%$ resources: resource, Filter%>"
						OnClick="EmailBTN_Click" />
				</td>
			</tr>
			<tr>
				<td>
					<%=ADAL.A_ExecuteData.CNTData("SELECT COUNT(*) AS Expr1  FROM SMS_User  WHERE (UnitID = "+ADAL.A_CheckData.GetUnitID()+")")%>
				</td>
				<td colspan="2">
					<asp:Button ID="Etebar27BTN" runat="server" Text="<%$ resources: resource, ReturnCredit27%>"
						OnClick="Etebar27BTN_Click" />
					<asp:Button ID="OverSMSByeBTN" runat="server" Text="بالای 100 هزار" OnClick="OverSMSByeBTN_Click"
						/>
				</td>
			</tr>
		</table>
	</fieldset>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0" cellpadding="0" width="90%"
		border="1">
		<tr>
			<th align="center">1400

			</th>
			<th align="center">
				<%=GetGlobalResourceObject("resource", "ID")%>
			</th>
			<th align="center">
				<%=GetGlobalResourceObject("resource", "_Name")%>
			</th>
			<th align="center">
				<%=GetGlobalResourceObject("resource", "Email")%>
			</th>
			<th align="center">
				<%=GetGlobalResourceObject("resource", "Buy")%>
			</th>
				<th align="center">
				
			</th>
			<th align="center">
				<%=GetGlobalResourceObject("resource", "Send")%>
			</th>
			<th align="center">
				<%=GetGlobalResourceObject("resource", "MobileNo")%>
			</th>
			<th align="center">
				<%=GetGlobalResourceObject("resource", "Status")%>
			</th>
			<th align="center">
				<%=GetGlobalResourceObject("resource", "_Date")%>
			</th>
			<th></th>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>" style="background: <%#Tools.Tools.ConvertToInt32(Eval("Count"))<Tools.Tools.ConvertToInt32(Eval("SendCount"))?"red":"" %>">
					<td align="center">&nbsp;<%# (Tools.Tools.ConvertToInt32(Eval("SMS_UserID").ToString().Trim())+1400) %></td>
					<td align="center">&nbsp;<%# Eval("SMS_UserID").ToString().Trim() %></td>
					<td align="center">&nbsp;<%# Eval("Name").ToString().Trim() %></td>
					<td align="center">&nbsp;<%# Eval("email").ToString().Trim() %></td>
					<td align="center">&nbsp;<%# Eval("Count").ToString().Trim()%></td>
					<td align="center">&nbsp;<%# Eval("byeCount").ToString().Trim()%></td>
					<td align="center">&nbsp;<%# Eval("SendCount").ToString().Trim()%></td>
					<td align="center">&nbsp;<%# Eval("MobileNo").ToString().Trim()%></td>
					<td align="center">&nbsp;<%# Eval("state").ToString().Trim() %></td>
					<td align="center">&nbsp;<%# Eval("ActiveDate").ToString().Trim()%></td>
					<td align="center">
						<input style="Width: 70px" type="button" onclick="SelectPrepMsg('/SMSUserInfo.aspx?ID=<%# Eval("SMS_UserID") %>	',620,590);" value="<%=GetGlobalResourceObject("resource", "Description")%>" />
						<asp:Button Width="50px" Text="<%$ resources: resource, ActiveCode%>" CommandArgument='<%# Eval("SMS_UserID").ToString().Trim()+"-"+Eval("MobileNo").ToString().Trim() %>'
							CommandName="Active" ID="ActiveBTN" runat="server" />
						<asp:Button Width="50px" Text="16" CommandArgument='<%# Eval("SMS_UserID").ToString().Trim()%>'
							CommandName="ret16" ID="ret16btn" runat="server" />
						<asp:Button Width="50px" Text="<%$ resources: resource, Block%>" CommandArgument='<%# Eval("SMS_UserID").ToString().Trim()%>'
							CommandName="Block" ID="BlockBTN" runat="server" />
						<asp:Button Width="50px" Text="<%$ resources: resource, SMSStatus%>" CommandArgument='<%# Eval("SMS_UserID").ToString().Trim()%>'
							CommandName="State" ID="SMSStateBTN" runat="server" />
						<asp:Button Width="50px" Text="<%$ resources: resource, ShowHidden%>" CommandArgument='<%# Eval("SMS_UserID").ToString().Trim()%>'
							CommandName="HiddenSMS" ID="Button1" runat="server" />
						<asp:Button Width="50px" Text="وایبر فعال" CommandArgument='<%# Eval("SMS_UserID").ToString().Trim()+"-"+Eval("MobileNo").ToString().Trim()%>'
							CommandName="MessagingAC" ID="Button2" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
</asp:Content>
