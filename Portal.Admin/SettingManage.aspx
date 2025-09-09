<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SettingManage.aspx.cs" Inherits="Portal.Admin.SettingManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="0" cellpadding="1" >
		<tr>
			<td colspan="2" align="center" class="RowHead">
				مدیریت تنظیمات
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
				کد تنظیمات
			</td>
			<td>
			    <asp:TextBox ID="SettingIDTB" runat="server" MaxLength="64" />
			</td>
		</tr>

		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td>
				<asp:TextBox ID="titleTB" runat="server"  Width="300"/>
			</td>
		</tr>
		<tr>
			<td>
				نوع تنظیمات
			</td>
			<td>
				<asp:TextBox ID="TypeTB" runat="server"  />
			</td>
		</tr>
		<tr>
			<td >
				نام جدول
			</td>
            <td>
                <asp:TextBox ID="TableNameTB" runat="server" />
            </td>
		</tr>
		<tr>
			<td>
				مقدار فیلد
			</td>
			<td>
				<asp:TextBox ID="ValueFieldTB" runat="server" />
			</td>
		</tr>
		<tr>
			<td>
				عنوان فیلد
			</td>
			<td>
				<asp:TextBox ID="TextFieldTB" runat="server" />
			</td>
		</tr>
		<tr>
			<td>
				شرط
			</td>
			<td>
				<asp:TextBox ID="ConditionTB" runat="server"  />
			</td>
		</tr>
		<tr>
            <td>توضیحات</td>
            <td><asp:TextBox ID="DescTB" runat="server" MaxLength="64" /></td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNText %>" OnClick="SaveBTN_Click"/><input
					type="reset" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20">
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
				کد تنظیمات
			</td>
			<td>
				عنوان تنظیمات
			</td>
			<td>
				نوع
			</td>
			<td>
				نام جدول
			</td>
			<td>
				مقدار فیلد
			</td>
            <td>
                عنوان فیلد
            </td>
            <td>
                شرط
            </td>
            <td>
                توضیحات
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
						&nbsp;<%# Eval("SettingUnitNameID").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("Name").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("Type").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("TableName").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("ValueFeild").ToString().Trim() %></td>
                    <td align="center">
						&nbsp;<%# Eval("TextField").ToString().Trim() %></td>
                    <td align="center">
						&nbsp;<%# Eval("Condition").ToString().Trim() %></td>
                    <td align="center">
						&nbsp;<%# Eval("Description").ToString().Trim() %></td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("SettingUnitNameID").ToString().Trim() %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("SettingUnitNameID").ToString().Trim() %>'
							CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
		<tr>
			<td colspan=8 align=center>
				<asp:LinkButton ID="lnkPreviousPage" runat="server" OnClick="lnkPreviousPage_Click" Text="<%$ resources: resource, Previews%>"></asp:LinkButton>
				&nbsp;
				<asp:Repeater ID="rptPages" runat="server" OnItemCommand="rptPages_ItemCommand" OnItemCreated="rptPages_ItemCreated">
					<ItemTemplate>
						<asp:LinkButton ID="btnPage" CommandName="Page" CommandArgument='<%#Container.DataItem %>'
							runat="server"><%# Container.DataItem %>
						</asp:LinkButton>
					</ItemTemplate>
					<SeparatorTemplate>
						&nbsp;-&nbsp;
					</SeparatorTemplate>
				</asp:Repeater>
				&nbsp;
				<asp:LinkButton ID="lnkNextPage" runat="server" OnClick="lnkNextPage_Click" Text="<%$ resources: resource, Next%>"></asp:LinkButton>
			</td>
		</tr>
	</table>
	<%}%>
</asp:Content>
