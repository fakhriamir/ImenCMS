<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AddMenu.aspx.cs" Inherits="NewsService.AddMenu" %>

<asp:Content ContentPlaceHolderID="head" runat="server" ID="ContentA1">
</asp:Content>
<asp:Content ContentPlaceHolderID="Body" runat="server" ID="aa">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="0" cellpadding="1" width="80%">
		<tr class="RowHead">
			<td colspan="2" align="center">
				<%=GetGlobalResourceObject("resource", "AddMenuPT")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td>
				<asp:TextBox ID="menustrTB" runat="server" MaxLength="32" />*
			</td>
		</tr>
		
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNText %>" OnClick="SaveBTN_Click"/><input
					type="reset" style="width: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20">
			</td>
		</tr>
	</table>
	<br/>
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0" cellpadding="2" width="80%"
		border="0">
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
		<asp:Repeater runat="server" ID="ViewDR"    OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("MenuID").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("MenuStr").ToString().Trim() %>
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("MenuID") %>' CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif" Width="16px" Height="16px" CommandArgument='<%# Eval("MenuID") %>' CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
