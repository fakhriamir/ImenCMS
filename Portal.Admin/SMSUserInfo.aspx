<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="SMSUserInfo.aspx.cs" Inherits="Portal.Admin.SMS_UserInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<fieldset >
		<legend>مشخصات کاربری</legend>
		<asp:Repeater runat="server" ID="ViewDR" EnableViewState="false">
			<ItemTemplate>
				<table border="1">
					<tr>
						<td align="center">
							&nbsp;<%# Eval("SMS_UserID").ToString().Trim() %>
						</td>
						<td align="center">
							Name:&nbsp;<%# Eval("Name").ToString().Trim() %>
						</td>
						<td align="center">
							UN:&nbsp;<%# Eval("UserName").ToString().Trim()%>
						</td>
						<td align="center">
							&nbsp;<%# Tools.Tools.MyDecry(Eval("pass").ToString().Trim())%>
						</td>
					</tr>
					<tr>
						<td align="center">
							&nbsp;<%# Eval("email").ToString().Trim() %>
						</td>
						<td align="center">
							&nbsp;<%# Eval("Count").ToString().Trim()%>
						</td>
						<td align="center">
							&nbsp;<%# Eval("SendCount").ToString().Trim()%>
						</td>
					</tr>
					<tr>
						<td align="center">
							<%# Eval("MobileNo").ToString().Trim()%>
						</td>
						<td align="center">
							<%# Eval("state").ToString().Trim() %>
						</td>
						<td align="center">
							&nbsp;<%# Eval("ActiveDate").ToString().Trim()%>
						</td>
					</tr>
					<tr>
						<td align="center">
							<asp:Button Width="50px" Text="<%$ resources: resource, ActiveCode%>" CommandArgument='<%# Eval("SMS_UserID").ToString().Trim()+"-"+Eval("MobileNo").ToString().Trim() %>'
								CommandName="Active" ID="ActiveBTN" runat="server" />
						</td>
						<td align="center">
							<asp:Button Width="50px" Text="<%$ resources: resource, Block%>" CommandArgument='<%# Eval("SMS_UserID").ToString().Trim()+"-"+Eval("MobileNo").ToString().Trim() %>'
								CommandName="Block" ID="BlockBTN" runat="server" />
						</td>
						<td>
						</td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:Repeater>
	</fieldset>
	<fieldset>
		<legend>تغییر شماره موبایل</legend>
		<%=GetGlobalResourceObject("resource", "MobileNo")%>:<asp:TextBox ID="MobileNoTB"
			runat="server" Text=""></asp:TextBox>
		state:<asp:TextBox ID="StateTB" runat="server" Text=""></asp:TextBox>
		<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNSave%>"
			OnClick="SaveBTN_Click" />
	</fieldset>
	<fieldset>
		<legend>اضافه کردن اعتبار</legend>SMS:<asp:TextBox ID="SMSAddTB" runat="server" Text=""></asp:TextBox>
		Comm:<asp:TextBox ID="CommAddTB" TextMode="MultiLine" runat="server" Text=""></asp:TextBox>
		<asp:Button ID="AddSMSCountBTN" runat="server" Text="<%$ resources: resource, SaveBTNSave%>"
			OnClick="AddSMSCountBTN_Click" />
	</fieldset>
	<fieldset>
		<legend>فاکتورهای صادره</legend>
		<table border="1">
			<asp:Repeater runat="server" ID="FactorDR" EnableViewState="false">
				<ItemTemplate>
					<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
						<td align="center">
							&nbsp;<%# Eval("FactorID").ToString().Trim()%>
						</td>
						<td align="center">
							&nbsp;<%# Eval("Money").ToString().Trim()%>
						</td>
						<td align="center">
							&nbsp;<%# Eval("State").ToString().Trim()%>
						</td>
						<td align="center">
							&nbsp;<%# Eval("Date").ToString().Trim()%>
						</td>
						<td align="center">
							&nbsp;<%# Eval("BankID").ToString().Trim()%>
						</td>
						<td align="center">
							&nbsp;<%#Tools.Bank.GetBankName(Eval("BankType").ToString())%>
						</td>
						<td align="center">
							&nbsp;<%# Eval("Type").ToString().Trim()%>
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</table>
	</fieldset>
	<fieldset>
		<legend>تایید فاکتور بانک ملت</legend>
	<%=GetGlobalResourceObject("resource", "TransactionID")%><asp:TextBox ID="SaleReferenceIdTB"
		runat="server"></asp:TextBox>
	<%=GetGlobalResourceObject("resource", "FactorCode")%><asp:TextBox ID="SaleOrderIdTB"
		runat="server"></asp:TextBox>
	<asp:Button ID="SeltBTN" runat="server" Text="Selt" OnClick="SeltBTN_Click" /></fieldset>
</asp:Content>
