<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/Accounting.master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="Portal.Accounting.Customer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AccountingHead" runat="server">
		<link href="/Style/ufd-base.css" rel="stylesheet" type="text/css" />
	<script src="/Scripts/jquery.ui.ufd.js" type="text/javascript"></script>
		<link href="/Style/udf/plain/plain.css" rel="stylesheet" type="text/css" />
	<script src="/Scripts/select2.full.js" type="text/javascript"></script>
	<link href="/Style/select2.min.css" rel="stylesheet" />
  <script  type="text/javascript">
  	$(function () {
  		//$(".AutoCompTafzil").ufd({ log: false });
  		$(".AutoCompTafzil").select2({
  			placeholder: "انتخاب"
  		});
  	});
  </script>
	<style type="text/css">
		.ufd input,
		.ufd button
		{
			position: relative;
			float: right;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AccountingBody" runat="server">
	<table dir="rtl" align="center" class="TableColor" width="80%">
		<tr class="RowHead">
			<th colspan="2" align="center">اضافه کردن طرف حساب</th>
		</tr>

		<tr>
			<td>نوع </td>
			<td>
				<asp:DropDownList ID="TypeDL" runat="server">
					<asp:ListItem Text="حقیقی" Value="0" />
					<asp:ListItem Text="حقوقی" Value="1" />
					<asp:ListItem Text="کارمند" Value="2" />
					<asp:ListItem Text="کالا" Value="3" />
				</asp:DropDownList>*</td>
		</tr>
		<tr>
			<td>ماهیت</td>
			<td>
				<asp:DropDownList ID="MatterDL" runat="server">
					<asp:ListItem Text="ترازنامه ای" Value="0" />
					<asp:ListItem Text="سود و زیانی" Value="1" />
					<asp:ListItem Text="انتظامی" Value="2" />
				</asp:DropDownList>*</td>
		</tr>
		<tr>
			<td>نام</td>
			<td>
				<asp:TextBox ID="nameTB" runat="server" MaxLength="64" />*</td>
		</tr>
		<tr>
			<td>فامیل</td>
			<td>
				<asp:TextBox ID="familyTB" runat="server" MaxLength="64" />*</td>
		</tr>
		<tr>
			<td>تفضیلی</td>
			<td>
				<asp:DropDownList ID="accountingcodingDL" Width="300px" DataValueField="AccountingCodingID" DataTextField="name" CssClass="AutoCompTafzil" runat="server"></asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>کد ملی</td>
			<td>
				<asp:TextBox ID="melicodeTB" runat="server" MaxLength="16" /></td>
		</tr>
		<tr>
			<td>تلفن</td>
			<td>
				<asp:TextBox ID="telTB" runat="server" MaxLength="64" /></td>
		</tr>
		<tr>
			<td>نمابر</td>
			<td>
				<asp:TextBox ID="faxTB" runat="server" MaxLength="64" /></td>
		</tr>
		<tr>
			<td>همراه</td>
			<td>
				<asp:TextBox ID="mobileTB" runat="server" MaxLength="64" /></td>
		</tr>
		<tr>
			<td>وبسایت</td>
			<td>
				<asp:TextBox ID="websiteTB" runat="server" MaxLength="32" /></td>
		</tr>
		<tr>
			<td>پست الکترونیک</td>
			<td>
				<asp:TextBox ID="emailTB" runat="server" MaxLength="32" /></td>
		</tr>
		<tr>
			<td>معرف</td>
			<td>
				<asp:TextBox ID="reagentTB" runat="server" MaxLength="64" /></td>
		</tr>
		<tr>
			<td>کد اقتصادی</td>
			<td>
				<asp:TextBox ID="ecocodeTB" runat="server" MaxLength="32" /></td>
		</tr>
		<tr>
			<td>کد ثبت</td>
			<td>
				<asp:TextBox ID="sabtcodeTB" runat="server" MaxLength="32" /></td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="اضافه شود" Width="70px" OnClick="SaveBTN_Click" /><input type="reset" style="width: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20"></td>
		</tr>
	</table>
	<br>
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="rtl" id="My" align="center" cellspacing="1" cellpadding="2" width="100%" border="1">
		<tr class="RowHead">
			<td><%=GetGlobalResourceObject("resource", "ID")%></td>
			<td><%=GetGlobalResourceObject("resource", "name")%></td>
			<td><%=GetGlobalResourceObject("resource", "family")%></td>
			<td><%=GetGlobalResourceObject("resource", "Edit")%></td>
			<td><%=GetGlobalResourceObject("resource", "Del")%></td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">&nbsp;<%# DataBinder.Eval(Container.DataItem, "AccountingCustomerID").ToString().Trim() %></td>
					<td align="center">&nbsp;<%# Eval("Name").ToString().Trim() %></td>
					<td align="center">&nbsp;<%# Eval("family").ToString().Trim() %></td>
					<td align="center">
						<asp:ImageButton ImageUrl="/Images/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("AccountingCustomerID").ToString().Trim() %>' CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" /></td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="/Images/zarb.gif" Width="16px" Height="16px" CommandArgument='<%# Eval("AccountingCustomerID").ToString().Trim() %>' CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" /></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
