<%@ Page Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="UserAdmin.aspx.cs" Inherits="NewsService.UserAdmin" %>
<asp:Content ContentPlaceHolderID="head" runat="server" ID="ContentA1">
<%--<script type="text/javascript">
       
        $(document).ready(function () {
            //$("#aspnetForm").validate();
            $('.MessSendBTN').val('<%=GetGlobalResourceObject("resource", "ResetBTN")%>');
        
        });
       
	</script>--%>
</asp:Content>
<asp:Content ContentPlaceHolderID="Body" runat="server" ID="aa">
	<table class="TableColor" dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" cellspacing="0" cellpadding="1" width="70%" align="center">
		<tr>
			<th class="RowHead" colspan="2">
				<%=GetGlobalResourceObject("resource", "UserInfo")%>
			</th>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "FullName")%>
			</td>
			<td>
				<asp:TextBox ID="nameTB" CssClass="required email" runat="server" MaxLength="256" Width="256px" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "UserName")%>
			</td>
			<td>
				<asp:TextBox ID="usernameTB" CssClass="required length:8" runat="server" MaxLength="32" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Password")%>
			</td>
			<td>
				<asp:TextBox ID="passTB" runat="server" MaxLength="32" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "PermissionType")%>
			</td>
			<td>
				<asp:DropDownList ID="AccessDL" runat="server" DataTextField="Name" DataValueField="AccessTypeID">
				</asp:DropDownList><a href="AccessTypes.aspx">
					<img width="20" height="20" alt=<%=GetGlobalResourceObject("resource", "Edit")%> title=<%=GetGlobalResourceObject("resource", "Edit")%> src="/Imgs/myedit.gif" /></a>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "OfficeName")%>
			</td>
			<td>
				<asp:DropDownList ID="UnitDL" runat="server" DataTextField="name" DataValueField="unitid">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "MobileNo")%>
			</td>
			<td>
				<asp:TextBox ID="mobnoTB" runat="server" MaxLength="11" Width="35%" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Status")%>
			</td>
			<td>
				<asp:DropDownList ID="disableDL" runat="server">
					<asp:ListItem Value="0" Text="<%$ resources: resource, Active %>"></asp:ListItem>
					<asp:ListItem Value="1" Text="<%$ resources: resource, InActive %>"></asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
	
		
		<tr>
			<td align="center" colspan="2">
		<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNText %>" OnClick="SaveBTN_Click1"/>
                <input type="reset" size="20" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>">
			</td>
		</tr>
	</table>
	<script runat="server">
		string ATID = "";

		string SetAcc(string MyI)
		{
			ATID = MyI;
			return ATID;
		}
	</script>
	<br>
	
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
				<%=GetGlobalResourceObject("resource", "Message")%>
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
						&nbsp;<%# Eval("UserID").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("Name").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("UnitName").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("TypeName").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("mobno").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%#GetUserState(Convert.ToBoolean(Eval("Disable")))%>
					</td>
					<td align="center">
						<input style="Width:90px" type="button" class="MessSendBTN" onclick="SelectPrepMsg('/Messagesend.aspx?UserID=<%# Eval("UserID").ToString().Trim() %>',550,350);"  value="<%=GetGlobalResourceObject("resource", "SendMsg")%>"/>
		
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("UserID").ToString().Trim() %>' CommandName="EDIT" ID="EditBTN" ToolTip="" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						 <asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif" Width="16px" Height="16px" CommandArgument='<%# Eval("UserID").ToString().Trim() %>'	commandname="DEL" id="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
</asp:Content>
