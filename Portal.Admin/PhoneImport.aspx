<%@ Page Title="" EnableViewState="true" Language="C#" MasterPageFile="~/Admin.Master"
	AutoEventWireup="true" CodeBehind="PhoneImport.aspx.cs" Inherits="Portal.Admin.PhoneImport" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table class="TableColor" dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" align="center" id="Table1" cellspacing="0" cellpadding="1"
		width="80%">
		<tr>
			<td colspan="2" class="RowHead">
				<%=GetGlobalResourceObject("resource", "ImportExcel2003")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "File")%>
			</td>
			<td>
				<input id="File1" runat="server" type="file" />
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, TablePreview %>" OnClick="SaveBTN_Click">
				</asp:Button>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "SelectTable")%>
			</td>
			<td>
				<asp:DropDownList ID="ShetNameDL" runat="server">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="FieldSelectBTN" runat="server" Text="<%$ resources: resource, SelectField %>" OnClick="FieldSelectBTN_Click"
					Enabled="False" />
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				<asp:Panel ID="FormPN" runat="server">
				</asp:Panel>
				<asp:Button ID="AddToBankDL" runat="server" Text="<%$ resources: resource, TransferBankBTN %>" Enabled="False"
					OnClick="AddToBankDL_Click" />
			</td>
		</tr>
	</table>
</asp:Content>
