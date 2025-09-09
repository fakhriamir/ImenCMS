<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SubmitArticles.aspx.cs" Inherits="Portal.Admin.SubmitArticles" %>
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

			$('#ctl00_Body_wdateTB').datepicker();

			$('p').hover(function () { $(this).css('background', '#eee'); }, function () { $(this).css('background', '#fff'); });
		});
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="0" cellpadding="1" width="100%">
		<tr>
			<td colspan="2" class="RowHead">
				مديريت مقالات
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Subject")%>
			</td>
			<td>
				<asp:DropDownList ID="ArticleTypeDL" DataTextField="Name" DataValueField="ArticleTypeID"
					runat="server">
				</asp:DropDownList>
				<a href="ArticleTypes.aspx">
					<img width="20" height="20" alt=<%=GetGlobalResourceObject("resource", "Edit")%> title=<%=GetGlobalResourceObject("resource", "Edit")%> src="/Imgs/myedit.gif" /></a>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td>
				<asp:TextBox ID="titleTB" runat="server" MaxLength="64" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Abstract")%>
			</td>
			<td>
				<asp:TextBox ID="ChekideTB" runat="server" Width="550" MaxLength="254" />
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<CKEditor:CKEditorControl FormatSource="false"  ID="MyFCK" runat="server">
	</CKEditor:CKEditorControl>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Author")%>
			</td>
			<td>
				<asp:TextBox ID="authorTB" runat="server" MaxLength="64" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "PublishDate")%>
			</td>
			<td>
				<asp:TextBox ID="wdateTB" runat="server" />
				yyyy/mm/dd
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Pic")%>
			</td>
			<td>
				<asp:TextBox ID="ImageTB" runat="server" MaxLength="250" />
			</td>
		</tr>
		
		<tr>
			<td colspan="2" align="center">
				<asp:CheckBox ID="ViewCB" Text="<%$ resources: resource, ViewMsg %>" runat="server" />
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
				<%=GetGlobalResourceObject("resource", "Author")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Visit")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "_Date")%>
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
						&nbsp;<%# Eval("ArticleID").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("tITLE").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("author").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("hit").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("wdate").ToString().Trim() %>
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("ArticleID").ToString().Trim() %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("ArticleID").ToString().Trim() %>'
							CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
