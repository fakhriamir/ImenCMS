<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="PostPrice.aspx.cs" Inherits="Portal.Admin.PostPrice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="1" cellpadding="1" width="80%">
		<tr>
			<td colspan="2" class="RowHead">
				<%=GetGlobalResourceObject("resource", "CostTable")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "FromG")%>
			</td>
			<td>
				<asp:TextBox ID="gfromTB" runat="server" MaxLength="25"></asp:TextBox>*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "ToG")%>
			</td>
			<td>
				<asp:TextBox ID="gtoTB" runat="server" MaxLength="25"></asp:TextBox>*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "PriceRial")%>
			</td>
			<td>
				<asp:TextBox ID="MoneyTB" runat="server"></asp:TextBox>
			</td>
		</tr>
	
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNText %>" Width="70px" OnClick="SaveBTN_Click">
				</asp:Button><input type="reset" style="width: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20">
			</td>
		</tr>
	</table>
	<br>
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="1" cellpadding="2" width="100%"
		border="1">
	
		<tr class="RowHead">
			<td>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "FromG")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "ToG")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "PriceRial")%>
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
						&nbsp;<%# Eval("PostPriceID").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("GFrom").ToString().Trim()%>
					</td>
					<td align="center">
						&nbsp;<%# Eval("GTo").ToString().Trim()%>
					</td>
					<td align="center">
						&nbsp;<%# Eval("Payment").ToString().Trim()%>
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("PostPriceID").ToString().Trim() %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("PostPriceID").ToString().Trim() %>'
							CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
