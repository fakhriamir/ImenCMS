<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="UserAccess.aspx.cs" Inherits="NewsService.UserAccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="0" cellpadding="1"
		width="80%">
		<tr>
			<td colspan="2" class="RowHead">
				<%=GetGlobalResourceObject("resource", "SpecialAccessMng")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Permission")%>
			</td>
			<td>
				<asp:CheckBoxList ID="AccessCBL" TextAlign="Right" DataTextField="Name" DataValueField="UserID"
					runat="server">
				</asp:CheckBoxList>
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNSave%>" OnClick="SaveBTN_Click" /><input
					type="reset" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20">
			</td>
		</tr>
	</table>
</asp:Content>
