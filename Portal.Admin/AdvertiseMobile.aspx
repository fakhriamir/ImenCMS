<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdvertiseMobile.aspx.cs" Inherits="Portal.Admin.AdvertiseMobile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="rtl" align="center" class="TableColor" width="80%">
		<tr class="RowHead">
			<th colspan="2" align="center">نام صفحهMobileAdver</th>
		</tr>
		<tr>
			<td>عنوان</td>
			<td>
				<asp:TextBox ID="nameTB" runat="server" MaxLength="32" />*</td>
		</tr>
		<tr>
			<td>آدرس عکس</td>
			<td>
				<asp:TextBox ID="imageaddressTB" runat="server" MaxLength="128" />*</td>
		</tr>
		<tr>
			<td>عنوان دکمه</td>
			<td>
				<asp:TextBox ID="buttomtextTB" runat="server" MaxLength="32" />*</td>
		</tr>
		<tr>
			<td>لینک دکمه</td>
			<td>
				<asp:TextBox ID="buttomhrefTB" runat="server" MaxLength="128" />*</td>
		</tr>
		<tr>
			<td>ترتیب</td>
			<td>
				<asp:TextBox ID="sortTB" runat="server" MaxLength="25" />*</td>
		</tr>
		
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="اضافه شود" Width="70px" OnClick="SaveBTN_Click" /><input type="reset" style="WIDTH: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20"></td>
		</tr>
	</table>
	<br>
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="rtl" id="My" align="center" cellspacing="1" cellpadding="2" width="100%" border="1">
		<tr class="RowHead">
			<td><%=GetGlobalResourceObject("resource", "ID")%></td>
			<td><%=GetGlobalResourceObject("resource", "Title")%></td>
			<td>بازدید</td>
			<td><%=GetGlobalResourceObject("resource", "Edit")%></td>
			<td><%=GetGlobalResourceObject("resource", "Del")%></td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">&nbsp;<%# DataBinder.Eval(Container.DataItem, "MobileAdverID").ToString().Trim() %></td>
					<td align="center">&nbsp;<%# Eval("Name").ToString().Trim() %></td>
						<td align="center">&nbsp;<%# Eval("hit").ToString().Trim() %></td>
					<td align="center">
						<asp:ImageButton ImageUrl="/Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("MobileAdverID").ToString().Trim() %>' CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" /></td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="/Imgs/zarb.gif" Width="16px" Height="16px" CommandArgument='<%# Eval("MobileAdverID").ToString().Trim() %>' CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" /></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
