<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ResourceEditor.aspx.cs" Inherits="Portal.Admin.ResourceEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<asp:DropDownList ID="LangDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LangDL_SelectedIndexChanged">
		<asp:ListItem Value="" Text="<%$ resources: resource, Farsi %>"></asp:ListItem>
		<asp:ListItem Value=".en-us" Text="<%$ resources: resource, English %>"></asp:ListItem>
	</asp:DropDownList>
	<table dir="ltr" class="TableColor" align="center" id="Table1" cellspacing="1" cellpadding="1"
		width="60%">
		<tr>
			<td colspan="2" class="RowHead">
				<%=GetGlobalResourceObject("resource", "DisplayLangMng")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td align="left">
				<asp:TextBox ID="nameTB" runat="server" MaxLength="128" />*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Translation")%>
			</td>
			<td align="left">
				<asp:TextBox ID="TranslateTB" runat="server" MaxLength="1000" Width="465px" />*
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, Edit %>" Width="70px" 
					onclick="SaveBTN_Click">
				</asp:Button><input type="reset" style="width: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20">
			</td>
		</tr>
	</table><br />
	<table dir="ltr" id="My" align="center" cellspacing="0" cellpadding="2" width="60%"	border="0">
		<tr class="RowHead">
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Translation")%>
			</td>
				<td>
				<%=GetGlobalResourceObject("resource", "Edit")%>
			</td>
			<%--	<td>
				<%=GetGlobalResourceObject("resource", "Del")%>
			</td>--%>
		</tr>
		<asp:Repeater ID="ViewDR" runat="server"   OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="left">
						<%#Eval("NameVal")%>
					</td>
					<td align="left">
						<%#Eval("TextVal")%>
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("NameVal").ToString().Trim()+"$$$"+Eval("TextVal") %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<%--<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("NameVal").ToString().Trim() %>'
							CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>--%>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
</asp:Content>
