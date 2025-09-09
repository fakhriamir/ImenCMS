<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ChangePass.aspx.cs" Inherits="Portal.Admin.ChangePass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table class="TableColor"  dir="<%=GetGlobalResourceObject("resource", "_Dir")%>"  align="center" id="Table1" cellspacing="0" cellpadding="1" width="60%" >
		<tr>
			<th colspan="2" align="center">
				<%=GetGlobalResourceObject("resource", "ChangePass")%>
			</th>
		</tr>
		<tr>
			<td >
				<%=GetGlobalResourceObject("resource", "ExPass")%>:
			</td>
			<td >
				<asp:TextBox CssClass="required" Style="padding-top: 1px; font-family: Arial" ID="OldPassTB" Width="100"
					dir="ltr" runat="server" TextMode="Password" autocomplete="off" />
			</td>
		</tr>
		<tr>
			<td >
				<%=GetGlobalResourceObject("resource", "NewPass")%>:
			</td>
			<td >
				<asp:TextBox Style="padding-top: 1px; font-family: Arial" ID="NewPassTB" Width="100"
					dir="ltr" runat="server" TextMode="Password" autocomplete="off" />
			</td>
		</tr>
		<tr>
			<td >
				<%=GetGlobalResourceObject("resource", "NewPassRep")%>:
			</td>
			<td >
				<asp:TextBox Style="padding-top: 1px; font-family: Arial" ID="NewPass1TB" Width="100"
					dir="ltr" runat="server" TextMode="Password" autocomplete="off" />
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				
				<asp:Button ID="ChangePassBTN" runat="server" Text="<%$ resources: resource, ChangeBTN %>" Width="90" OnClick="ChangePass_Click">
				</asp:Button>
				<input type="button" value="<%=GetGlobalResourceObject("resource", "CancelBTN")%>" style="width: 60px; cursor: hand" />
			</td>
		</tr>
	</table>
</asp:Content>
