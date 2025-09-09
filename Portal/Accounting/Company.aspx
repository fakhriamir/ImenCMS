<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/Accounting.master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="Portal.Accounting.CompanyInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AccountingBody" runat="server">
	<table dir="rtl" align="center" class="TableColor" width="80%">
		<thead>
			<tr class="RowHead">
				<th colspan="2" align="center">مشخصات شرکت ها</th>
			</tr>
		</thead>
		<tbody>
			<tr>
				<td>نام شرکت</td>
				<td>
					<asp:TextBox ID="nameTB" runat="server" MaxLength="50" />*</td>
			</tr>
			<tr>
				<td>آدرس پستی</td>
				<td>
					<asp:TextBox ID="addressTB" runat="server" MaxLength="1024" /></td>
			</tr>
			<tr>
				<td>کد پستی</td>
				<td>
					<asp:TextBox ID="postalcodeTB" runat="server" MaxLength="11" /></td>
			</tr>
			<tr>
				<td>شماره ملی ثبت شرکت</td>
				<td>
					<asp:TextBox ID="melicodeTB" runat="server" MaxLength="20" /></td>
			</tr>
			<tr>
				<td>شماره اقتصادی</td>
				<td>
					<asp:TextBox ID="ecocodeTB" runat="server" MaxLength="32" /></td>
			</tr>
			<tr>
				<td>شماره ثبت</td>
				<td>
					<asp:TextBox ID="sabtcodeTB" runat="server" MaxLength="32" /></td>
			</tr>
			<tr>
				<td>شماره تلفن</td>
				<td>
					<asp:TextBox ID="telnoTB" runat="server" MaxLength="32" /></td>
			</tr>
			<tr>
				<td>نام و نام خانوادگی مدیرعامل</td>
				<td>
					<asp:TextBox ID="ceonameTB" runat="server" MaxLength="32" /></td>
			</tr>
			<tr>
				<td>موبایل مدیرعامل</td>
				<td>
					<asp:TextBox ID="ceomobileTB" runat="server" MaxLength="12" /></td>
			</tr>
			<tr>
				<td>ایمیل مدیرعامل</td>
				<td>
					<asp:TextBox ID="ceoemailTB" runat="server" MaxLength="32" /></td>
			</tr>
			<tr>
				<td>آدرس وبسایت</td>
				<td>
					<asp:TextBox ID="websiteTB" runat="server" MaxLength="32" /></td>
			</tr>
			<tr>
				<td>ایمیل</td>
				<td>
					<asp:TextBox ID="emailTB" runat="server" MaxLength="32" /></td>
			</tr>
			<tr>
				<td colspan="2" align="center">
					<asp:Button ID="SaveBTN" runat="server" Text="اضافه شود" Width="70px" OnClick="SaveBTN_Click" /><input type="reset" style="width: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20"></td>
			</tr>
		</tbody>
	</table>
	<br>
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="rtl" id="My" align="center" class="SortTable tablesorter" cellspacing="1" cellpadding="2" width="100%" border="1">
		<thead><tr class="RowHead">
			<th><%=GetGlobalResourceObject("resource", "ID")%></th>
			<th><%=GetGlobalResourceObject("resource", "Title")%></th>
			<td>سال مالی</td>
			<td><%=GetGlobalResourceObject("resource", "Edit")%></td>
			<td><%=GetGlobalResourceObject("resource", "Del")%></td>
		</tr></thead>
		<tbody>
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">&nbsp;<%# DataBinder.Eval(Container.DataItem, "AccountingCompanyID").ToString().Trim() %></td>
					<td align="center">&nbsp;<%# Eval("Name").ToString().Trim() %></td>
					<td align="center">
						<input style="Width:70px" type="button" onclick="SelectPrepMsg('/Year.aspx?ID=<%# Eval("AccountingCompanyID") %>',520,490);"  value="سال مالی"/>
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="/Images/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("AccountingCompanyID").ToString().Trim() %>' CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" /></td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="/Images/zarb.gif" Width="16px" Height="16px" CommandArgument='<%# Eval("AccountingCompanyID").ToString().Trim() %>' CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" /></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater></tbody>
	</table>
	<%}%>
</asp:Content>
