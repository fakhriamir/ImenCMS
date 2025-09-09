<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="NewsService.MyCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="1" cellpadding="1" width="80%">
		<tr>
			<td colspan="2" class="RowHead">
				<%=GetGlobalResourceObject("resource", "NewsCatigory")%>
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
				<%=GetGlobalResourceObject("resource", "SubSet")%>
			</td>
			<td>
				 <asp:DropDownList ID="LeveLDL" runat="server" DataTextField="name" DataValueField="level">
                </asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Subject")%>
			</td>
			<td>
				<asp:TextBox ID="nameTB" runat="server" MaxLength="64"/>*
			</td>
		</tr>
		
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNText %>" Width="70px" 
					onclick="SaveBTN_Click"/><input
					type="reset" style="width: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20">
			</td>
		</tr>
	</table>
	<br />
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="1" cellpadding="2" width="100%"
		border="1">
		
		<tr class="RowHead">
			<td>
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
						<asp:ImageButton ImageUrl="/Imgs/Up.png" Width="18px" Height="18px" CommandArgument='<%# Eval("Sort") +"#"+ Eval("CategoryID") %>'
							CommandName="UP" ID="UpIB" ToolTip="<%$ resources: resource, MoveUp %>" AlternateText="<%$ resources: resource, MoveUp %>"
							runat="server" />
						<asp:ImageButton ImageUrl="/Imgs/down.png" Width="18px" Height="18px" CommandArgument='<%# Eval("Sort") +"#"+ Eval("CategoryID") %>'
							CommandName="DOWN" ID="DownBTN" ToolTip="<%$ resources: resource, MoveDown %>" AlternateText="<%$ resources: resource, MoveDown %>"
							runat="server" />
					</td>
					<td align="center">
						&nbsp;<%# Eval("CategoryID").ToString().Trim()%>
					</td>
					<td align="right">
						<%#Tools.Tools.GetLevelTab(Eval("[level]").ToString().Trim())%>&nbsp;<%# Eval("Name").ToString().Trim() %>
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="/Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("CategoryID") %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="/Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("CategoryID") %>' CommandName="DEL"
							ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
