<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="MenuItems.aspx.cs" Inherits="NewsService.MenuItems" %>

<asp:Content ID="ContentA1" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="aa" runat="server" ContentPlaceHolderID="Body">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="0" cellpadding="1"
		width="80%">
		<tr>
			<td colspan="2" class="RowHead">
                 <%=GetGlobalResourceObject("resource", "AddSubMenu")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "MainMenuName")%>
			</td>
			<td>
				<asp:DropDownList ID="MenuIDDL" runat="server" DataValueField="MenuID" DataTextField="MenuStr">
					<asp:ListItem Value="-1" Text="<%$ resources:resource, NoMenu %>"></asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "SubMenuName")%>
			</td>
			<td>
				<asp:TextBox ID="childstrTB" runat="server" MaxLength="32" />*
			</td>
		</tr>
		<tr>
			<td>
                <%=GetGlobalResourceObject("resource", "SubMenuURL")%>
				
			</td>
			<td>
				<asp:TextBox ID="childhrefTB" dir="ltr" runat="server" MaxLength="52" Width="250px"/>*
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN"
					runat="server" Text="<%$ resources: resource, SaveBTNText %>" OnClick="SaveBTN_Click"/><input type="reset"
						style="width: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20">
			</td>
		</tr>
	</table>
	<br>
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
				<%=GetGlobalResourceObject("resource", "Link")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "SubSet")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Edit")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Del")%>
			</td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("MenuChildID").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("childStr").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("childhref").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("MenuStr").ToString().Trim() %>
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("MenuChildID").ToString().Trim() %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("MenuChildID").ToString().Trim() %>'
							CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
