<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="PhotoLink.aspx.cs" Inherits="Portal.Admin.PhotoLink" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="1" cellpadding="1" width="80%">
		<tr>
			<td colspan="2" class="RowHead">
				<%=GetGlobalResourceObject("resource", "RotatePicLink")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Lang")%>
			</td>
			<td>
				<asp:DropDownList ID="LanguageDL"   CssClass="NotSearch" runat="server" DataTextField="Name" DataValueField="LangID">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td>
				<asp:TextBox ID="titleTB" runat="server" MaxLength="124"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Link")%>
			</td>
			<td>
				<asp:TextBox ID="linkTB" runat="server" MaxLength="64"></asp:TextBox>*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Pic")%>
			</td>
			<td>
				<input id="ImageUP" runat="server" type="file" />*
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
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
            <td>
				<%=GetGlobalResourceObject("resource", "Pic")%>
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
						&nbsp;<%# Eval("PhotoLinkID").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("title").ToString().Trim() %>
					</td>
					<td align="center" style="width: 300px">
						&nbsp;<img style="width: 300px" src='<%=ADAL.A_CheckData.GetFilesRoot(true) %>/images/Photolink/<%# Eval("picname").ToString().Trim()%>' />
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("PhotoLinkID") %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("PhotoLinkID") %>' CommandName="DEL"
							ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
