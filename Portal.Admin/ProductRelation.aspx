<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="ProductRelation.aspx.cs" Inherits="Portal.Admin.ProductRelation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="rtl" align="center" class="TableColor" width="80%">
		<tr class="RowHead">
			<th colspan="2" align="center">محصولات مرتبط</th>
		</tr>
		<tr>
			<td>محصولات</td>
			<td>
				<asp:dropdownlist id="productDL" runat="server" datatextfield="Name" datavaluefield="ProductID" />
				*</td>
		</tr>

		<tr>
			<td colspan="2" align="center">
				<asp:button id="SaveBTN" runat="server" text="اضافه شود" width="70px" OnClick="SaveBTN_Click" />
				<input type="reset" style="width: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20"></td>
		</tr>
	</table>
	<br>
	<%if (ViewDR.Items.Count != 0)
		{%>
	<table dir="rtl" id="My" align="center" cellspacing="1" cellpadding="2" width="100%" border="1">
		<tr class="RowHead">
			<td><%=GetGlobalResourceObject("resource", "ID")%></td>
			<td><%=GetGlobalResourceObject("resource", "Title")%></td>
			<td><%=GetGlobalResourceObject("resource", "Del")%></td>
		</tr>
		<asp:repeater runat="server" id="ViewDR" onitemcommand="ViewDR_ItemCommand"> 
			<ItemTemplate> 
				<TR class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>"> 
					<TD align="center">
						&nbsp;<%# DataBinder.Eval(Container.DataItem, "ProductRelationID").ToString().Trim() %></TD>
					<TD align="center"> &nbsp;<%# Eval("Name").ToString().Trim() %></TD> 
			
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="/Imgs/zarb.gif" Width="16px" Height="16px" CommandArgument='<%# Eval("ProductRelationID").ToString().Trim() %>' CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" /></td> 
				</TR>
			</ItemTemplate>
		</asp:repeater>
	</table>
	<%}%>
</asp:Content>
