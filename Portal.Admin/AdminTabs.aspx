<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminTabs.aspx.cs" Inherits="Portal.Admin.AdminTabs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" align="center" id="Table1" cellspacing="0" cellpadding="1" width="80%">
		<tr>
			<td colspan="2" align="center" class="RowHead">
				<%=GetGlobalResourceObject("resource","MultipleTabMng") %>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource","Lang") %>
			</td>
			<td>
				<asp:DropDownList ID="LanguageDL"   CssClass="NotSearch" runat="server" DataTextField="Name" DataValueField="LangID">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource","Position") %>
			</td>
			<td>
				<asp:DropDownList ID="TypeDL" runat="server">
					<asp:ListItem Value="0" Text="<%$ resources: resource, TabJQ %>" />
					<asp:ListItem Value="1" Text="<%$ resources: resource, TabJQ2 %>"  />
					<asp:ListItem Value="2" Text="<%$ resources: resource, TabJQ3 %>"  />
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource","ModuleName") %>
			</td>
			<td>
				<asp:DropDownList ID="defaultitemidDL" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource","ModuleTitle") %>
			</td>
			<td>
				<asp:TextBox ID="NameTB" runat="server"></asp:TextBox>
			</td>
		</tr>
	
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNText %>" OnClick="SaveBTN_Click"/><input
					type="reset" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20">
			</td>
		</tr>
	</table>
	<br>
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0" cellpadding="2" width="100%"
		border="0">
		<tr class="RowHead" >
			<td>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "TabName")%>
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
						&nbsp;<%# Eval("TabsAdminID").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("DName").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("Name").ToString().Trim() %>
					</td>
						<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("TabsAdminID").ToString().Trim() %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("TabsAdminID").ToString().Trim() %>'
							CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
