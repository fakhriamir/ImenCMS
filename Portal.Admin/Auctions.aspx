<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true"
	CodeBehind="Auctions.aspx.cs" Inherits="Portal.Admin.Auctions" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link type="text/css" href="Scripts/ui.core.css" rel="stylesheet" />
	<link type="text/css" href="Scripts/ui.theme.css" rel="stylesheet" />
	<link type="text/css" href="Scripts/ui.datepicker.css" rel="stylesheet" />
	<script type="text/javascript" src="/scripts/jquery-1.3.2.min.js"></script>
	<script type="text/javascript" src="/scripts/ui.datepicker-cc.min.js"></script>
	<script type="text/javascript" src="/scripts/calendar.min.js"></script>
	<script type="text/javascript" src="/scripts/ui.datepicker-cc-fa.js"></script>
	<script type="text/javascript">
		$(function () {

			$('#ctl00_Body_stdateTB').datepicker();
			$('#ctl00_Body_endateTB').datepicker();

			$('p').hover(function () { $(this).css('background', '#eee'); }, function () { $(this).css('background', '#fff'); });
		});
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="0" cellpadding="1"
		width="80%">
		<tr>
			<td colspan="2" class="RowHead">
				<%=GetGlobalResourceObject("resource", "Auction")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Lang")%>
			</td>
			<td>
				<asp:DropDownList ID="LanguageDL"   CssClass="NotSearch" runat="server" DataTextField="Name" DataValueField="LangID">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				
			</td>
			<td>
				<asp:TextBox ID="nameTB" runat="server" MaxLength="64" Width="285px" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "_Type")%>
			</td>
			<td>
				<asp:DropDownList ID="AuctionsTypeDL" DataTextField="Name" DataValueField="AuctionTypeID"
					runat="server">
				</asp:DropDownList>
				<a href="AuctionTypes">
					<img width="20" height="20" alt="<%=GetGlobalResourceObject("resource", "Edit")%>" title="<%=GetGlobalResourceObject("resource", "Edit")%>" src="/Imgs/myedit.gif" /></a>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Description")%>
			</td>
			<td>
				<CKEditor:CKEditorControl FormatSource="false" ID="textTB" Toolbar="Basic" runat="server"
					Width="500px" Height="250px">
				</CKEditor:CKEditorControl>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "StartDate")%>
			</td>
			<td>
				<asp:TextBox ID="stdateTB" runat="server" MaxLength="10" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "EndDate")%>
			</td>
			<td>
				<asp:TextBox ID="endateTB" runat="server" MaxLength="10" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "AuctionPrice")%>
			</td>
			<td>
				<asp:TextBox ID="moneyTB" runat="server" MaxLength="25" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "AddressAuction")%>
			</td>
			<td>
				<asp:TextBox ID="addressTB" runat="server" MaxLength="128" Width="285px" />
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNText %>" OnClick="SaveBTN_Click" /><input
					type="reset" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20">
			</td>
		</tr>
	</table>
	<br>
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0" cellpadding="2" width="100%"
		border="0">
		<tr class="RowHead">
			<td>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Edit")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Del")%>
			</td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("AuctionID").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("Name").ToString().Trim() %>
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("AuctionID").ToString().Trim() %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("AuctionID").ToString().Trim() %>'
							CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
