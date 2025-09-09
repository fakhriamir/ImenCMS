<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Advertise.aspx.cs" Inherits="Portal.Admin.Advertise" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center"  id="Table1" cellspacing="1" cellpadding="1" width="80%">
		<tr>
			<td colspan="2" align="center" class="RowHead">
				<%=GetGlobalResourceObject("resource","AddADV") %>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource","ADVType") %>
			</td>
			<td>
				<asp:DropDownList ID="TypeDL" runat="server">
					<asp:ListItem Selected="True" Text="<%$ resources: resource,NoRestriction %>" Value="0" />
					<asp:ListItem Text="<%$ resources: resource,ClickRestriction %>" Value="1" />
					<asp:ListItem Text="<%$ resources: resource, ViewRestriction %>" Value="2" />
					<%--<asp:ListItem Text="محدوديت زماني" Value="3" />--%>
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource","ADVName") %>
			</td>
			<td>
				<asp:TextBox ID="nameTB" runat="server" MaxLength="60"></asp:TextBox>*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource","address") %>
			</td>
			<td>
				<asp:TextBox ID="urlTB" runat="server" MaxLength="256"></asp:TextBox>*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource","File") %>
			</td>
			<td>
				<input id="ImageUP" runat="server" type="file" />*
			</td>
		</tr>
		<%--<tr>
			<td>
				تعداد کليک
			</td>
			<td>
				<asp:TextBox ID="clickcountTB" ReadOnly="true" runat="server" MaxLength="25"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				تعداد نمايش
			</td>
			<td>
				<asp:TextBox ID="viewcountTB" ReadOnly="true" runat="server" MaxLength="25"></asp:TextBox>
			</td>
		</tr>--%>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "DisplayPermitNo")%>
			</td>
			<td>
				<asp:TextBox ID="maxviewcountTB" runat="server" MaxLength="25"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "ClickPermitNo")%>
			</td>
			<td>
				<asp:TextBox ID="maxclickcountTB" runat="server" MaxLength="25"></asp:TextBox>
			</td>
		</tr>
		<%--<tr>
			<td>
				مجوز تاريخ شروع
			</td>
			<td>
				<asp:TextBox ID="startdateTB" runat="server" MaxLength="4"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				مجوز تاريخ پايان
			</td>
			<td>
				<asp:TextBox ID="enddateTB" runat="server" MaxLength="4"></asp:TextBox>
			</td>
		</tr>--%>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Active")%>
			</td>
			<td>
				<asp:DropDownList ID="enableDL" runat="server">
					<asp:ListItem Selected="true" Text="<%$ resources: resource,Active %>" Value="1" />
					<asp:ListItem Text="<%$ resources: resource,Inactive %>" Value="0" />
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "WidthPX")%>
			</td>
			<td>
				<asp:TextBox ID="widthTB" runat="server" MaxLength="5"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "HeightPX")%>
			</td>
			<td>
				<asp:TextBox ID="heightTB" runat="server" MaxLength="5"></asp:TextBox>
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
		border="0">
		
		<tr class="RowHead">
			<td>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "ClickNo")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "ViewNo")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Edit")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Del")%>
			</td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR"    OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("AdvertiseID").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("Name").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("cLICKcOUNT").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("ViewCount").ToString().Trim() %>
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("AdvertiseID").ToString().Trim() %>' CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif" Width="16px" Height="16px" CommandArgument='<%# Eval("AdvertiseID").ToString().Trim() %>' CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
							
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
