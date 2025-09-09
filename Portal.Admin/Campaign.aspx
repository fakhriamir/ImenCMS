<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Campaign.aspx.cs" Inherits="Portal.Admin.Campaign" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="rtl" align="center" class="TableColor" width="80%">
		<tr class="RowHead">
			<th colspan="2" align="center">مدیریت کمپین</th>
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
			<td>عنوان</td>
			<td>
				<asp:textbox id="titleTB" runat="server" maxlength="256" />
				*</td>
		</tr>
		<tr>
			<td colspan="2">
				<CKEditor:CKEditorControl FormatSource="false"  ID="MyFCK" runat="server" ></CKEditor:CKEditorControl>
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
			<td>
				<%=GetGlobalResourceObject("resource", "Status")%>
			</td>
			<td>
				
				<asp:DropDownList ID="VisibeDL" runat="server">
				<asp:ListItem  Value="1" Text="<%$ resources: resource, Active %>"></asp:ListItem>
				<asp:ListItem Value="0" Text="<%$ resources: resource, InActive %>"></asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				<asp:button id="SaveBTN" runat="server" text="اضافه شود" width="70px" OnClick="SaveBTN_Click" />
				<input type="reset" style="width: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20"></td>
		</tr>
	</table>
	<br>
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="rtl" id="My" align="center" cellspacing="1" cellpadding="2" width="100%" border="1">
		<tr class="RowHead">
			<td></td>
			<td><%=GetGlobalResourceObject("resource", "Title")%></td>
			<td></td>
			<td><%=GetGlobalResourceObject("resource", "Edit")%></td>
			<td><%=GetGlobalResourceObject("resource", "Del")%></td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="right">
						<asp:ImageButton ImageUrl="/Imgs/Up.png" Width="18px" Height="18px" CommandArgument='<%# Eval("Sort") +"#"+ Eval("CampaignID") %>'
							CommandName="UP" ID="UpIB" ToolTip="<%$ resources: resource, MoveUp %>" AlternateText="<%$ resources: resource, MoveUp %>"
							runat="server" />
						<asp:ImageButton ImageUrl="/Imgs/down.png" Width="18px" Height="18px" CommandArgument='<%# Eval("Sort") +"#"+ Eval("CampaignID") %>'
							CommandName="DOWN" ID="DownBTN" ToolTip="<%$ resources: resource, MoveDown %>"
							AlternateText="<%$ resources: resource, MoveDown %>" runat="server" />
					</td>
					<td align="center"><%# Eval("Title").ToString().Trim() %></td>
					<td style="width:110px" align="center"><img style="width:100px" src="<%=ADAL.A_CheckData.GetFilesRoot(true) + "/Images/Campaign/"%><%# Eval("picname").ToString().Trim() %>" /></td>
					<td align="center">
						<asp:ImageButton ImageUrl="/Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("CampaignID").ToString().Trim() %>' CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" /></td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="/Imgs/zarb.gif" Width="16px" Height="16px" CommandArgument='<%# Eval("CampaignID").ToString().Trim() %>' CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" /></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
