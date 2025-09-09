<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="BankManage.aspx.cs" Inherits="Portal.Admin.BankManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="1" cellpadding="1"
		width="80%">
		<tr>
			<td colspan="2" class="RowHead">
				<%=GetGlobalResourceObject("resource", "BankAccMng")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td>
				<asp:TextBox ID="nameTB" runat="server" MaxLength="64"></asp:TextBox>*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "bank")%>
			</td>
			<td>
				<asp:DropDownList ID="engnameDL" runat="server">
					<asp:ListItem Value="Melat" Text="<%$ resources: resource, Mellat %>"></asp:ListItem>
					<asp:ListItem Value="Saman" Text="<%$ resources: resource, Saman %>"></asp:ListItem>
					<asp:ListItem Value="Parsian" Text="<%$ resources: resource, Parsian %>"></asp:ListItem>
					<asp:ListItem Value="Nevin" Text="<%$ resources: resource, EghtesadNovin %>"></asp:ListItem>
					<asp:ListItem Value="Pasargad" Text="بانک پاسارگاد"></asp:ListItem>
					<asp:ListItem Value="Sadad" Text="بانک ملی"></asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				action
			</td>
			<td>
				<asp:TextBox ID="actionTB" dir="ltr" runat="server" MaxLength="512" Width="500px"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				mid
			</td>
			<td>
				<asp:TextBox ID="midTB" dir="ltr" runat="server" MaxLength="128" Width="500px"></asp:TextBox>*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "RedirectURL")%>
			</td>
			<td>
				<asp:TextBox ID="redirecturlTB" dir="ltr" runat="server" MaxLength="512" Width="500px"></asp:TextBox>*
			</td>
		</tr><tr>
			<td>
				<%=GetGlobalResourceObject("resource", "RedirectURLShop")%>
			</td>
			<td>
				<asp:TextBox ID="redirecturlshopTB" dir="ltr" runat="server" MaxLength="512" Width="500px"></asp:TextBox>*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "UserName")%>
			</td>
			<td>
				<asp:TextBox ID="usernameTB" dir="ltr" runat="server" MaxLength="128" Width="500px"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Password")%>
			</td>
			<td>
				<asp:TextBox ID="passwordTB" dir="ltr" runat="server" MaxLength="2042" Width="500px"></asp:TextBox>
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
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0" cellpadding="2" width="100%"
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
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("BankID").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("Name").ToString().Trim() %></td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("BankID").ToString().Trim() %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("BankID").ToString().Trim() %>'
							CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
