<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="WorldList.aspx.cs" Inherits="NewsService.WorldList" %>
<asp:Content ContentPlaceHolderID="head" runat="server" ID="ContentA1">

</asp:Content>
<asp:Content ContentPlaceHolderID="Body" runat="server" ID="aa">
	
	<table id="My" dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" cellspacing="0" cellpadding="2" width="90%" align="center"
		border="0">
		<tr >
			<th>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</th>
			<th>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</th>
			<th>
				<%=GetGlobalResourceObject("resource", "OfficeName")%>
			</th>
			<th>
				<%=GetGlobalResourceObject("resource", "Permission")%>
			</th>
			<th>
				<%=GetGlobalResourceObject("resource", "MobileNo")%>
			</th>
			<th>
				<%=GetGlobalResourceObject("resource", "Status")%>
			</th>
			
			<th>
				<%=GetGlobalResourceObject("resource", "Edit")%>
			</th>
			<th>
				<%=GetGlobalResourceObject("resource", "Del")%>
			</th>
		</tr>
		<asp:Repeater ID="ViewDR" runat="server"    OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center"> 
						&nbsp;<%# Eval("CategoryWordID").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("Word").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("CNT").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("Weight").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("Name").ToString().Trim() %>
					</td>
					
					
					<td align="center">
						<asp:ImageButton Visible="false" ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("CategoryWordID").ToString().Trim() %>' CommandName="EDIT" ID="EditBTN" ToolTip="" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						 <asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif" Width="16px" Height="16px" CommandArgument='<%# Eval("CategoryWordID").ToString().Trim() %>'	commandname="DEL" id="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
</asp:Content>
