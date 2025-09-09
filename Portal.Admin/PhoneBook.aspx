<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="PhoneBook.aspx.cs" Inherits="Portal.Admin.PhoneBook" %>

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

			$('#ctl00_Body_birthdateTB').datepicker();

			$('p').hover(function () { $(this).css('background', '#eee'); }, function () { $(this).css('background', '#fff'); });
		});
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="0" cellpadding="1"
		width="80%">
		<tr>
			<td colspan="2" class="RowHead">
				<%=GetGlobalResourceObject("resource", "PhoneBook")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "_Name")%>
			</td>
			<td>
				<asp:TextBox ID="nameTB" runat="server" MaxLength="50" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "LastNAme")%>
			</td>
			<td>
				<asp:TextBox ID="familyTB" runat="server" MaxLength="50" />*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "MobileNo")%>
			</td>
			<td>
				<asp:TextBox ID="mobnoTB" runat="server" MaxLength="11" />*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "BirthDate")%>
			</td>
			<td>
				<asp:TextBox ID="birthdateTB" runat="server" />
				yyyy/mm/dd
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Catigory")%><a href="PhoneBookType.aspx">
					<img width="20" height="20" alt=<%=GetGlobalResourceObject("resource", "Edit")%> title=<%=GetGlobalResourceObject("resource", "Edit")%> src="/Imgs/myedit.gif" /></a>
			</td>
			<td>
				<asp:CheckBoxList ID="PhoneTypeCBL" runat="server" DataTextField="Name" DataValueField="PhoneBookTypeID"
					RepeatColumns="5">
				</asp:CheckBoxList>
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
	
	<br />
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="0" cellpadding="1">
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "FilterTel")%>
			</td>
			<td>
				<asp:TextBox ID="PhoneNoTB" runat="server" />
			</td>
			<td>
				<asp:Button ID="PhoneNoBTN" runat="server" Text="<%$ resources: resource, Filter %>" OnClick="PhoneNoBTN_Click" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "FilterCatigory")%>
			</td>
			<td>
				<asp:DropDownList ID="PhoneBookTypeDL" runat="server" DataTextField="Name" DataValueField="PhoneBookTypeID" />
			</td>
			<td>
				<asp:Button ID="FilterBTN" runat="server" Text="<%$ resources: resource, Filter %>" OnClick="FilterBTN_Click" />
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
				<asp:Button ID="NameBTN" runat="server" Text="<%$ resources: resource, Filter %>" OnClick="NameBTN_Click" />
			</td>
		</tr>
	</table>
	<br />
	<%if (ViewDR.Items.Count != 0)
   {%><table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0" cellpadding="2" width="100%"
		border="1">
		<tr class="RowHead">
			<td>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "_Type")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "FullName")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Tel")%>
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
						&nbsp;<%# Eval("PhoneBookID").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("Name").ToString().Trim() %>&nbsp;<%# Eval("Family").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("MobNo").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<a href="PhoneBookAddType.aspx?ID=<%# Eval("PhoneBookID").ToString().Trim() %>"><%=GetGlobalResourceObject("resource", "PersonalCatigory")%></a>
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("PhoneBookID").ToString().Trim() %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("PhoneBookID").ToString().Trim() %>'
							CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
