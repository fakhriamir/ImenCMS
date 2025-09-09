<%@ Page Title="" Language="C#" MasterPageFile="~/Dialog.Master" AutoEventWireup="true" CodeBehind="Access.aspx.cs" Inherits="Portal.Archive.Access" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCP" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BoduCB" runat="server">
	<fieldset>
		<legend>دسترسی جدید</legend>
		<div>
			<asp:CheckBoxList ID="AccessCBL" runat="server" RepeatDirection="Horizontal">
				<asp:ListItem Value="0">دسترسی کامل</asp:ListItem>
				<asp:ListItem Value="1">خواندن</asp:ListItem>
				<asp:ListItem Value="2">نوشتن</asp:ListItem>
				<asp:ListItem Value="3">ویرایش</asp:ListItem>
				<asp:ListItem Value="4">حذف</asp:ListItem>
			</asp:CheckBoxList>
			<asp:TextBox ID="PNameTB" runat="server"></asp:TextBox>
			<asp:Button ID="SearchBTN" runat="server" Text="جستجو" OnClick="SearchBTN_Click" /><br />
			<%if (GuestDR.Items.Count != 0)
	 {%>

			<table dir="rtl" id="Table1" align="center" cellspacing="0" cellpadding="2" width="100%" border="1">
				<tr class="RowHead">
					<th><%=GetGlobalResourceObject("resource", "Name")%></th>
					<th>سمت</th>
					<th>اضافه</th>
				</tr>
				<asp:Repeater runat="server" ID="GuestDR" OnItemCommand="GuestDR_ItemCommand">
					<ItemTemplate>
						<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
							<td align="center"><%# Eval("Fname").ToString().Trim() %> &nbsp; <%# Eval("Family").ToString().Trim() %></td>
							<td align="center"><%# Eval("Semat").ToString() %></td>
							<td align="center">
								<asp:ImageButton ImageUrl="ییImages/Edit.png" Width="18px" Height="18px" CommandArgument='<%# Eval("GuestID").ToString().Trim() %>'
									CommandName="Invite" ID="InviteBTN" ToolTip="اضافه" AlternateText="اضافه"
									runat="server" />
							</td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>
			</table>
			<%} %>
		</div>
		<hr />
		<br />
		<table dir="rtl" align="center" class="TableColor" width="80%">
			<tr>
				<td>نام </td>
				<td>
					<asp:DropDownList ID="ParticipantDL" runat="server">
					</asp:DropDownList>
				</td>
			
				<td align="center">
					<asp:Button ID="SaveBTN" runat="server" Text="اضافه" OnClick="SaveBTN_Click" /></td>
			</tr>
		</table>

	</fieldset>
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="rtl" id="My" align="center" cellspacing="0" cellpadding="2" width="100%" border="1">
		<tr class="RowHead">
			
			<th><%=GetGlobalResourceObject("resource", "Name")%></th>
			<th>دسترسی</th>
		
			<th><%=GetGlobalResourceObject("resource", "Del")%></th>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr >
				
					<td align="center"><%# Tools.Project.GetGuestName( Eval("GuestID").ToString().Trim()) %></td>
					<td align="center"><%# GetAccessStr( Eval("Access").ToString()) %></td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Images/delete.png" Width="16px" Height="16px" CommandArgument='<%# Eval("ArchiveFileAccessID").ToString().Trim() %>' CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" /></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
