<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Subscribe.aspx.cs" Inherits="Portal.Admin.Subscribe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="0" cellpadding="1" width="80%">
		<tr>
			<td colspan="2" class="RowHead">
				<%=GetGlobalResourceObject("resource", "Subscribers")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "FullName")%>
			</td>
			<td>
				<asp:TextBox ID="nameTB" runat="server" MaxLength="64" />*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Email")%>
			</td>
			<td>
				<asp:TextBox ID="emailTB" runat="server" MaxLength="64" />*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "MobileNo")%>
			</td>
			<td>
				<asp:TextBox ID="mobnoTB" runat="server" MaxLength="12" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "SubscriberType")%>
			</td>
			<td>
				<asp:DropDownList ID="SubscribeTypeDL" DataValueField="SubscribeTypeID" DataTextField="Name"
					runat="server">
				</asp:DropDownList>
				<a href="SubscribeType.aspx">
					<img width="20" height="20" alt="<%=GetGlobalResourceObject("resource", "Edit")%>" title="<%=GetGlobalResourceObject("resource", "Edit")%>" src="/Imgs/myedit.gif" /></a>
			</td>
		</tr>
	
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNText %>" OnClick="SaveBTN_Click"/><input
					type="reset" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20">
			</td>
		</tr>
	</table>
	<br />
	<asp:Button ID="ExportToExcel" runat="server" Text="خروجی اکسل" OnClick="ExportToExcel_Click" />
	<div>
	<asp:Repeater runat="server" ID="CountDR">
			<ItemTemplate>
				<div>
						&nbsp;<%# Eval("Name").ToString().Trim() %>:
						&nbsp;<%# Eval("CNT").ToString().Trim() %></div>
			</ItemTemplate>
		</asp:Repeater>
		</div>
	<br>
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0" cellpadding="2" width="100%"
		border="1">
		<tr class="RowHead">
			<td>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "_Name")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Email")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "MobileNo")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "_Type")%>
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
						&nbsp;<%# Eval("SubscribeID").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("Name").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("Email").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("mobno").ToString().Trim()%></td>
					<td align="center">
						&nbsp;<%# Eval("sname").ToString().Trim()%></td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("SubscribeID").ToString().Trim() %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("SubscribeID").ToString().Trim() %>'
							CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
		<tr>
			<td colspan=7 align=center>
				<asp:LinkButton ID="lnkPreviousPage" runat="server" OnClick="lnkPreviousPage_Click" Text="<%$ resources: resource, Previews%>"></asp:LinkButton>
				&nbsp;
				<asp:Repeater ID="rptPages" runat="server" OnItemCommand="rptPages_ItemCommand" OnItemCreated="rptPages_ItemCreated">
					<ItemTemplate>
						<asp:LinkButton ID="btnPage" CommandName="Page" CommandArgument='<%#Container.DataItem %>'
							runat="server"><%# Container.DataItem %>
						</asp:LinkButton>
					</ItemTemplate>
					<SeparatorTemplate>
						&nbsp;-&nbsp;
					</SeparatorTemplate>
				</asp:Repeater>
				&nbsp;
				<asp:LinkButton ID="lnkNextPage" runat="server" OnClick="lnkNextPage_Click" Text="<%$ resources: resource, Next%>"></asp:LinkButton>
			</td>
		</tr>
	</table>
	<%}%>
</asp:Content>
