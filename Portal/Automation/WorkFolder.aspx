<%@ Page Title="" Language="C#" MasterPageFile="~/Dialog.Master" AutoEventWireup="true" CodeBehind="WorkFolder.aspx.cs" Inherits="Portal.Automation.WorkFolder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCP" runat="server">
    <style>
        .RowHead
        {
            background-color:#e8e8e8;
            border-bottom :2px solid #808080;
            text-align:center;
            font-weight:bold;
            height:22px;
        }
        .RowItem, .RowAlter
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BoduCB" runat="server">
	<fieldset>
        <legend>مدیریت پوشه های کاری</legend>

    <table dir="rtl" align="center" class="TableColor" width="80%" border="0" cellspacing="0" cellpadding="2">
		<tr>
			<td>عنوان پوشه</td>
			<td>
				<asp:TextBox ID="nameTB" runat="server" MaxLength="32" />*</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="اضافه شود" Width="70px" OnClick="SaveBTN_Click" /><input type="reset" style="WIDTH: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20"></td>
		</tr>
	</table>
    </fieldset>
	<br>
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="rtl" id="My" align="center" cellspacing="0" cellpadding="2" width="100%" border="0">
		<tr class="RowHead">
			<td><%=GetGlobalResourceObject("resource", "ID")%></td>
			<td><%=GetGlobalResourceObject("resource", "Title")%></td>
			<td><%=GetGlobalResourceObject("resource", "Edit")%></td>
			<td><%=GetGlobalResourceObject("resource", "Del")%></td>
            <td><%=GetGlobalResourceObject("resource", "Order")%></td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
                <tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
                    <td align="center">&nbsp;<%# DataBinder.Eval(Container.DataItem, "OfficeWorkFolderID").ToString().Trim() %></td>
                    <td align="center">&nbsp;<%# Eval("Name").ToString().Trim() %></td>
                    <td align="center">
                        <asp:ImageButton ImageUrl="Images/edit.png" Width="18px" Height="18px" CommandArgument='<%# Eval("OfficeWorkFolderID").ToString().Trim() %>' CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" /></td>
                    <td align="center">
                        <asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Images/delete.png" Width="16px" Height="16px" CommandArgument='<%# Eval("OfficeWorkFolderID").ToString().Trim() %>' CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" /></td>
                    <td align="center">
                        <asp:ImageButton ImageUrl="Images/Up.png" Width="18px" Height="18px" CommandArgument='<%# Eval("Sort") +"#"+ Eval("OfficeWorkFolderID") %>'
                            CommandName="UP" ID="UpIB" ToolTip="<%$ resources: resource, MoveUp%>" AlternateText="<%$ resources: resource, MoveUp%>"
                            runat="server" />
                        <asp:ImageButton ImageUrl="Images/down.png" Width="18px" Height="18px" CommandArgument='<%# Eval("Sort") +"#"+ Eval("OfficeWorkFolderID") %>'
                            CommandName="DOWN" ID="DownBTN" ToolTip="<%$ resources: resource, MoveDown%>"
                            AlternateText="<%$ resources: resource, MoveDown%>" runat="server" />
                    </td>
                </tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
