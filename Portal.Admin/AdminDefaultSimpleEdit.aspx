<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminDefaultSimpleEdit.aspx.cs" Inherits="Portal.Admin.AdminDefaultSimpleEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<style type="text/css">
		#PreViewDiv div
		{
			height: 100px;
			border: 2 solide red;
		}
		.TempC
		{
			border: 2px solid red;
			text-align: center;
			vertical-align: middle;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<asp:DropDownList ID="TemplateDL" AutoPostBack="true" DataTextField="Name" DataValueField="DefaultUnitCustomID"
		runat="server" OnSelectedIndexChanged="TemplateDL_SelectedIndexChanged" />
<asp:TextBox dir="ltr" name="code"  Style="margin-bottom: 14px; width: 97%;
			height: 400px" ID="MyFCK" runat="server" TextMode="MultiLine" Wrap="False" />
	<asp:Button ID="OKBTN" runat="server" Text="OKBTN" OnClick="OKBTN_Click"/>
	<br />

	
	<table border=1>
	<tr>
				<td align="center">
					<%=GetGlobalResourceObject("resource", "ModuleName")%>
				</td>
				<td align="center">
				    <%=GetGlobalResourceObject("resource","UsedTag") %>
				</td>
			</tr>
	<asp:Repeater runat="server" ID="ViewDR" >
		<ItemTemplate>
			<tr>
				<td align="center">
					&nbsp;<%# Eval("Name").ToString().Trim() %>
				</td>
				<td align="center">
					#<%# Eval("PageName").ToString().Trim() %>#
				</td>
			</tr>
		</ItemTemplate>
	</asp:Repeater>
	</table>
</asp:Content>
