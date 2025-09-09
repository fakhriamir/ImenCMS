<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/Accounting.master" AutoEventWireup="true" CodeBehind="Coding.aspx.cs" Inherits="Portal.Accounting.Coding" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AccountingBody" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="1" cellpadding="1" width="80%">
		
		
		<tr>
			<td>
				زیرمجموعه
			</td>
			<td>
				 <asp:DropDownList ID="LeveLDL" runat="server" DataTextField="name" DataValueField="level">
                </asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				عنوان حساب
			</td>
			<td>
				<asp:TextBox ID="nameTB" runat="server" MaxLength="64"/>*
			</td>
		</tr>
		<tr>
			<td>
				کد حساب
			</td>
			<td>
				<asp:TextBox ID="CodeTB" runat="server" MaxLength="64"/><span class="MessHint"> (در صورت خالی بودن اتوماتیک پر می شود)</span>
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="اضافه شود" Width="70px" 
					onclick="SaveBTN_Click"/><input
					type="reset" style="width: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20">
			</td>
		</tr>
	</table>
	<br />
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table class="tablesorter"  align="center" cellspacing="1" cellpadding="2" width="100%"		border="1">
		<thead>
		<tr class="RowHead">
			
			<td>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td>
				
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Edit")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Del")%>
			</td>
		</tr></thead>
		<tbody>
		<asp:Repeater runat="server" ID="ViewDR"   OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<%--<td align="center">
						<asp:ImageButton ImageUrl="/Imgs/Up.png" Width="18px" Height="18px" CommandArgument='<%# Eval("Sort") +"#"+ Eval("NewsCategoryID") %>'
							CommandName="UP" ID="UpIB" ToolTip="<%$ resources: resource, MoveUp %>" AlternateText="<%$ resources: resource, MoveUp %>"
							runat="server" />
						<asp:ImageButton ImageUrl="/Imgs/down.png" Width="18px" Height="18px" CommandArgument='<%# Eval("Sort") +"#"+ Eval("NewsCategoryID") %>'
							CommandName="DOWN" ID="DownBTN" ToolTip="<%$ resources: resource, MoveDown %>" AlternateText="<%$ resources: resource, MoveDown %>"
							runat="server" />
					</td>--%>
					<td align="center">
						<%# Eval("Code").ToString().Trim()%>
					</td>
					<td align="right">
						<%#Tools.Tools.GetLevelTab(Eval("[level]").ToString().Trim())%>&nbsp;<%# Eval("Name").ToString().Trim() %>
					</td>
						<td align="center">
						<%# GetParentCode(Eval("[level]").ToString().Trim())%>
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="/Images/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("AccountingCodingID") %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="/Images/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("AccountingCodingID") %>' CommandName="DEL"
							ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table></tbody>
	<%}%>
</asp:Content>
