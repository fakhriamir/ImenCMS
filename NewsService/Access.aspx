<%@ Page  Language="C#"  MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Access.aspx.cs" Inherits="NewsService.MyAccess" %>
<asp:Content ID="aa" runat="server" ContentPlaceHolderID="Body">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" border="0" cellspacing="0"
		cellpadding="0" width="50%">
		<tr>
			<td colspan="2" align="center" class="RowHead">
				<%=GetGlobalResourceObject("resource", "AccessPT")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "AccessType")%>
			</td>
			<td>
				<asp:DropDownList ID="AccessTypeIDDL" runat="server" DataValueField="AccessTypeID"
					DataTextField="Name"/>
				
				<a href="AccessTypes.aspx">
					<img width="20" height="20" alt=<%=GetGlobalResourceObject("resource", "Edit")%> title=<%=GetGlobalResourceObject("resource", "Edit")%> src="/Imgs/myedit.gif" /></a>
				*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "PageName")%>
			</td>
			<td>
				<asp:DropDownList ID="PageIDDL" runat="server" DataValueField="MenuChildID" DataTextField="ChildStr"
					Width="350px" />*
			</td>
		</tr>
		<tr>
			<td align="center" colspan="2">
				<asp:CheckBox ID="InsertCB" runat="server" Checked="True" Text="<%$ resources: resource, AddAccess %>" />
				<asp:CheckBox ID="EditCB" runat="server" Checked="True" Text="<%$ resources: resource, EditAccess %>"/>
				<asp:CheckBox ID="DelCB" runat="server" Checked="True" Text="<%$ resources: resource, DelAccess %>" />
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNText %>"  OnClick="SaveBTN_Click" />
				<input type="reset" style="width: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20" />
			</td>
		</tr>
	</table>
	<br />
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" border="0" id="Table1" cellspacing="0"
		cellpadding="0" width="30%">
		<tr>
			<td colspan="2" align="center" class="RowHead">
			<%=GetGlobalResourceObject("resource", "AccessTypeView")%>
			</td>
		</tr>
		<tr>
			<td>
				<asp:DropDownList ID="AccessTypeID1DL" runat="server" DataValueField="AccessTypeID"
					DataTextField="Name">
				</asp:DropDownList>
				*
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="ViewBTN" runat="server" Text="<%$ resources: resource, View %>" OnClick="ViewBTN_Click" />
			</td>
		</tr>
	</table>
	<br />
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0" cellpadding="2" width="70%"
		border="0">
		<tr class="RowHead">
			<td>
			<%=GetGlobalResourceObject("resource", "ID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Page")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "AccessType")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "AddAccess")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "DelAccess")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "EditAccess")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Del")%>
			</td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("AccessID").ToString().Trim() %></td>
					<td>
						&nbsp;<%# Eval("childstr").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("Type").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("Ins").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("Del").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("Edit").ToString().Trim() %></td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("AccessID") %>' CommandName="DEL"
							ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
</asp:Content>
