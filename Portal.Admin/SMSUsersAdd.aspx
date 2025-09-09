<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SMSUsersAdd.aspx.cs" Inherits="Portal.Admin.SMS_UsersAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" align="center" class="TableColor" width="80%">
		<tr class="RowHead">
			<td colspan="2" align="center">
				<%=GetGlobalResourceObject("resource", "DefineUser")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "FullName")%>
			</td>
			<td>
				<asp:TextBox ID="nameTB" runat="server" MaxLength="64" />*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "UserName")%>
			</td>
			<td>
				<asp:TextBox ID="usernameTB" runat="server" MaxLength="32" />*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Password")%>
			</td>
			<td>
				<asp:TextBox ID="passTB" runat="server" MaxLength="32" />*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Email")%>
			</td>
			<td>
				<asp:TextBox ID="emailTB" runat="server" MaxLength="32" />*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "MobileNo")%>
			</td>
			<td>
				<asp:TextBox ID="mobilenoTB" runat="server" MaxLength="15" />*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Status")%>
			</td>
			<td>
				<asp:TextBox ID="stateTB" runat="server" MaxLength="25" />*
			</td>
		</tr>
		
		
		<tr>
			<td colspan="2" align="center">
				
				<asp:Button ID="SaveBTN"
					runat="server" Text="<%$ resources: resource, SaveBTNText %>" Width="70px" onclick="SaveBTN_Click" /><input type="reset" style="width: 70px"
						value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20">
			</td>
		</tr>
	</table>
<br><table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="0" cellpadding="1">
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "FilterUserID")%>
			</td>
			<td>
				<asp:TextBox ID="PCodeFilterTB" runat="server" />
			</td>
			<td>
				<asp:Button ID="PhoneNoBTN" runat="server" Text="<%$ resources: resource, Filter%>" 
					onclick="PhoneNoBTN_Click" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "FilterName")%>
			</td>
			<td>
				<asp:TextBox ID="NameFilterTB" runat="server" />
			</td>
			<td>
				<asp:Button ID="NameBTN" runat="server" Text="<%$ resources: resource, Filter%>" onclick="NameBTN_Click" />
			</td>
		</tr>
	</table>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0" cellpadding="2" width="90%"
		border="1">
<asp:Repeater runat="server" ID="ViewDR" EnableViewState="false"   OnItemCommand="ViewDR_ItemCommand" >
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("SMS_UserID").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("Name").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("email").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("MobileNo").ToString().Trim()%></td>
					<td align="center">
						&nbsp;<%# Eval("state").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("ActiveDate").ToString().Trim()%></td>
					<td align="center">
						<asp:Button Text="<%$ resources: resource, SendActiveCode %>" CommandArgument='<%# Eval("SMS_UserID").ToString().Trim()+"-"+Eval("MobileNo").ToString().Trim() %>' CommandName="Active" ID="ActiveBTN" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
		</table>
</asp:Content>
