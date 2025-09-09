<%@ Page Title="" Language="C#" MasterPageFile="~/Dialog.Master" AutoEventWireup="true" CodeBehind="Year.aspx.cs" Inherits="Portal.Accounting.Year" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCP" runat="server">
	<script type="text/javascript" src="/Scripts/ui.core.min.js"></script>
<script type="text/javascript" src="/Scripts/ui.datepicker-cc.min.js"></script>
<script type="text/javascript" src="/Scripts/calendar.min.js"></script>
<script type="text/javascript" src="/Scripts/ui.datepicker-cc-fa.js"></script>
<link type="text/css" href="/Scripts/ui.core.css" rel="stylesheet" />
<link type="text/css" href="/Scripts/ui.theme.css" rel="stylesheet" />
<link type="text/css" href="/Scripts/ui.datepicker.css" rel="stylesheet" />
	<script type="text/javascript">
	    $(function () {
	    	$('#startdateTB').datepicker({
	    		changeMonth: true,
	    		dateFormat: 'yy/mm/dd',
	            changeYear: true
	        });
	    	$('#enddateTB').datepicker({
	    		changeMonth: true,
	    		dateFormat: 'yy/mm/dd',
	            changeYear: true
	        });
	      
	    });
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BoduCB" runat="server">
	<table dir="rtl" align="center" class="TableColor" width="80%">
		<tr class="RowHead">
			<th colspan="2" align="center">سال مالی شرکت</th>
		</tr>
		
		<tr>
			<td>سال مالی</td>
			<td>
				<asp:TextBox ID="nameTB" runat="server" MaxLength="4" /></td>
		</tr>
		<tr>
			<td>تاریخ شروع</td>
			<td>
				<asp:TextBox ID="startdateTB" ClientIDMode="Static" dir="ltr" runat="server" MaxLength="10" /></td>
		</tr>
		<tr>
			<td>تاریخ پایان</td>
			<td>
				<asp:TextBox ID="enddateTB" ClientIDMode="Static"   dir="ltr" runat="server" MaxLength="10" /></td>
		</tr>
		<tr>
			
			<td colspan="2" >
				<asp:CheckBox ID="DefCB" runat="server" Text="سال به عنوان پیش فرض باشد" />
				
			</td>
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
			<td><%=GetGlobalResourceObject("resource", "Title")%></td>
			<td>پیش فرض</td>
			<td><%=GetGlobalResourceObject("resource", "Edit")%></td>
			<td><%=GetGlobalResourceObject("resource", "Del")%></td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">&nbsp;<%# DataBinder.Eval(Container.DataItem, "accountingyearid").ToString().Trim() %></td>
					<td align="center">&nbsp;<%# Eval("Name").ToString().Trim() %></td>
					<td align="center">&nbsp;<%# Eval("def").ToString().Trim() %></td>
					<td align="center">
						<asp:ImageButton ImageUrl="/Images/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("accountingyearid").ToString().Trim() %>' CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" /></td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="/Images/zarb.gif" Width="16px" Height="16px" CommandArgument='<%# Eval("accountingyearid").ToString().Trim() %>' CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" /></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
