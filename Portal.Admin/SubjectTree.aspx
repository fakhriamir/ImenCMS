<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SubjectTree.aspx.cs" Inherits="Portal.Admin.SubjectTree" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="0" cellpadding="1" width="80%">
		<tr>
			<td colspan="2" class="RowHead">
				<%=GetGlobalResourceObject("resource", "SubjectTrees")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "،TreeTitle")%>
			</td>
			<td>
				<asp:TextBox ID="subjecttypeidTB" runat="server" MaxLength="25" />*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "SubTree")%>
			</td>
			<td>
				<asp:TextBox ID="parentidTB" runat="server" MaxLength="25" />*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td>
				<asp:TextBox ID="nameTB" runat="server" MaxLength="128" />*
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
		border="1">
		<tr class="RowHead">
			<td>
				<%=GetGlobalResourceObject("resource", "Order")%>
			</td>
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
		<asp:Repeater runat="server" ID="ViewDR"   OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/Up.png" Width="18px" Height="18px" CommandArgument='<%# Eval("Sort") +"#"+ Eval("ContactID") %>'
							CommandName="UP" ID="UpIB" ToolTip="<%$ resources: resource, MoveUp %>" AlternateText="<%$ resources: resource, MoveUp %>"
							runat="server" />
						<asp:ImageButton ImageUrl="Imgs/down.png" Width="18px" Height="18px" CommandArgument='<%# Eval("Sort") +"#"+ Eval("ContactID") %>'
							CommandName="DOWN" ID="DownBTN" ToolTip="<%$ resources: resource, MoveDown %>" AlternateText="<%$ resources: resource, MoveDown %>"
							runat="server" />
					</td>
					<td align="center">
						&nbsp;<%# Eval("SubjectID").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("Name").ToString().Trim() %>
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("ContactID") %>' CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif" Width="16px" Height="16px" CommandArgument='<%# Eval("ContactID") %>' CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
