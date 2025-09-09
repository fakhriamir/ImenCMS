<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Personal.aspx.cs" Inherits="Portal.Admin.Personal" %>
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

			$('#ctl00_Body_birthdateTB').datepicker();

			$('p').hover(function () { $(this).css('background', '#eee'); }, function () { $(this).css('background', '#fff'); });
		});
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="0" cellpadding="1" width="80%">
		<tr>
			<td colspan="2" class="RowHead">
				<%=GetGlobalResourceObject("resource", "PersonalMng")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Chart")%>
			</td>
			<td>
				<asp:DropDownList ID="unitchartDL" runat="server" DataTextField="name" DataValueField="unitchartid">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "PostTitle")%>
			</td>
			<td>
				<asp:TextBox ID="postnameTB" runat="server" MaxLength="64" />*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "_Name")%>
			</td>
			<td>
				<asp:TextBox ID="nameTB" runat="server" MaxLength="32" />*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "LastName")%>
			</td>
			<td>
				<asp:TextBox ID="familyTB" runat="server" MaxLength="32" />*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Edu")%>
			</td>
			<td>
				<asp:TextBox ID="tahsilTB" runat="server" MaxLength="32" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "BirthDate")%>
			</td>
			<td>
				<asp:TextBox ID="birthdateTB" runat="server" MaxLength="12" />yyyy/mm/dd
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "BirthPlace")%>
			</td>
			<td>
				<asp:TextBox ID="birthplaceTB" runat="server" MaxLength="32" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Pic")%>
			</td>
			<td>
				<asp:DropDownList ID="AddressDL" runat="server" DataTextField="Name" DataValueField="LangID">
				</asp:DropDownList>
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
			<td colspan="2">
				<CKEditor:CKEditorControl FormatSource="false"  ID="MyFCK" Toolbar="Basic"  runat="server" Width="500px" Height="250px" >
	</CKEditor:CKEditorControl>
			</td>
		</tr>
		
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNText %>" OnClick="SaveBTN_Click"/><input
					type="reset" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20">
			</td>
		</tr>
	</table>
	<br>
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0" cellpadding="2" width="100%"
		border="1">
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
		<asp:Repeater runat="server" ID="ViewDR"   OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("PersonalID").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("Name").ToString().Trim() %>
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("PersonalID").ToString().Trim() %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("PersonalID").ToString().Trim() %>'
							CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
		<tr>
			<td colspan=4 align=center>
				<asp:LinkButton ID="lnkPreviousPage" runat="server" OnClick="lnkPreviousPage_Click" Text="<%$ resources: resource, Previews%>"></asp:LinkButton>
				&nbsp;
				<asp:Repeater ID="rptPages" runat="server" OnItemCommand="rptPages_ItemCommand" OnItemCreated="rptPages_ItemCreated">
					<ItemTemplate>
						<asp:LinkButton ID="btnPage" CommandName="Page" CommandArgument='<%#Container.DataItem %>'
							runat="server"><%# Container.DataItem %>
						</asp:LinkButton>
					</ItemTemplate>
					<SeparatorTemplate>
						&nbsp;-&nbsp;
					</SeparatorTemplate>
				</asp:Repeater>
				&nbsp;
				<asp:LinkButton ID="lnkNextPage" runat="server" OnClick="lnkNextPage_Click" Text="<%$ resources: resource, Next%>"></asp:LinkButton>
			</td>
		</tr>
	</table>
	<%}%>
</asp:Content>
