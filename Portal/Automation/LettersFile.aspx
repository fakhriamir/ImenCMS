<%@ Page Title="" Language="C#" MasterPageFile="~/Dialog.Master" AutoEventWireup="true" CodeBehind="LettersFile.aspx.cs" Inherits="Portal.Automation.LettersFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCP" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BoduCB" runat="server">
	<fieldset>
		<legend>فایلهای پیوست نامه</legend>
		<table dir="rtl" align="center" class="TableColor" width="80%">
			<tr>
				<td>عنوان فایل</td>
				<td>
					<asp:TextBox ID="nameTB" runat="server" MaxLength="32" /></td>
			</tr>
			<tr>
				<td>نوع فایل</td>
				<td>
					<asp:DropDownList DataValueField="OfficeFileTypeID" DataTextField="name" ID="officefiletypeidDL" runat="server" /></td>
			</tr>
			<tr>
				<td>پیوست فایل</td>
				<td>
					<asp:FileUpload ID="FileFU" runat="server" /></td>
			</tr>
			<tr>
				<td colspan="2" align="center">
					<asp:Button ID="SaveBTN" runat="server" Text="اضافه شود" Width="70px" OnClick="SaveBTN_Click" /><input type="reset" style="WIDTH: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20"></td>
			</tr>
		</table>
	</fieldset>
	<br />
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="rtl" id="My" align="center" cellspacing="1" cellpadding="2" width="100%" border="1">
		<tr class="RowHead">
			<th><%=GetGlobalResourceObject("resource", "ID")%></th>
			<th><%=GetGlobalResourceObject("resource", "Title")%></th>
			<th>نام فایل</th>
			<th>دریافت</th>
			<th><%=GetGlobalResourceObject("resource", "Del")%></th>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">&nbsp;<%# Eval("OfficeLetterAtachID").ToString().Trim() %></td>
					<td align="center">&nbsp;<%# Eval("Name").ToString().Trim() %></td>
					<td align="left">&nbsp;<%# Eval("FileName").ToString().Trim() %></td>
					<td align="center"><a href="/Automation/OfficeImage-F<%# Eval("OfficeLetterAtachID").ToString().Trim() %>.aspx" target="_blank">دریافت</a></td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Images/delete.png" Width="16px" Height="16px" CommandArgument='<%# Eval("OfficeLetterAtachID").ToString().Trim() %>' CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" /></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
