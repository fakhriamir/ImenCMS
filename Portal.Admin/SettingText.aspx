<%@ Page Language="C#" ValidateRequest="false" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SettingText.aspx.cs" Inherits="Portal.Admin.SettingText" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ContentPlaceHolderID="head" runat="server" ID="ContentA1">
</asp:Content>
<asp:Content ContentPlaceHolderID="Body" runat="server" ID="aa">
	<table class="TableColor" align="center">
		<tr>
			<td class="RowHead" colspan="2">
				<%=GetGlobalResourceObject("resource", "TextSetting")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "PartName")%>
			</td>
			<td>
				<asp:DropDownList ID="TextTypeDL" runat="server" Width="350px" AutoPostBack="True" DataTextField="Name"
					DataValueField="SettingTextNameID" OnSelectedIndexChanged="TextTypeDL_SelectedIndexChanged" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Lang")%>
			</td>
			<td>
				<asp:DropDownList ID="LanguageDL"   CssClass="NotSearch" runat="server" DataTextField="Name" AutoPostBack="True"
					DataValueField="LangID" OnSelectedIndexChanged="TextTypeDL_SelectedIndexChanged" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Setting")%>
			</td>
			<td>
				<CKEditor:CKEditorControl FormatSource="false" ID="MyFCK" runat="server">
				</CKEditor:CKEditorControl>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Guidance")%>
			</td>
			<td>
				<asp:Label ID="MyHelpLB" runat="server" Text="" />
			</td>
		</tr>
		<tr>
			<td colspan="2" align=center>
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNSave %>" OnClick="SaveBTN_Click" />
			</td>
		</tr>
	</table>
</asp:Content>
