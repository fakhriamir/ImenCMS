<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="RevSlideShow.aspx.cs" Inherits="Portal.Admin.RevSlideShow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="0" cellpadding="1" width="80%">
		<tr>
			<td colspan="2" class="RowHead">
				مدیریت اسلاید نمایشی ماژولار
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Lang")%>
			</td>
			<td>
				<asp:DropDownList ID="MyLanguageDL" runat="server" DataTextField="Name" DataValueField="LangID">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				عنوان گالری*
			</td>
			<td>
				<asp:TextBox ID="nameTB" runat="server" MaxLength="64" Width="166px" />
			</td>
		</tr>
		
        <tr>
			<td>
				لینک
			</td>
			<td>
				<asp:TextBox ID="LinkTB" runat="server" MaxLength="64" Width="166px" />
			</td>
		</tr>
        <tr>
            <td>اولویت</td>
            <td><asp:TextBox ID="SortTB" runat="server" MaxLength="64" Width="166px" /></td>
        </tr>
        <tr>
            <td>وضعیت</td>
            <td>
                <asp:DropDownList ID="EnableDL" runat="server">
                   <asp:ListItem Value="1" Enabled="true">فعال</asp:ListItem>
                   <asp:ListItem Value="0">غیر فعال</asp:ListItem>
                </asp:DropDownList>
            </td>
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
		border="1">
		<tr class="RowHead">
			<td>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
            <td>
                لینک
            </td>
            <td>اجزا</td>
            <td>اولویت</td>
            <td>وضعیت</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Edit")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Del")%>
			</td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" EnableViewState=false OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("RevGalleryTitleID").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("Title").ToString().Trim() %>
					</td>
                    <td align="center">
						&nbsp;<%# Eval("Link").ToString().Trim() %>
					</td>
                    
                   	<td align="center">
						<input style="width: 70px" type="button" onclick="SelectPrepMsg('/RevSlideShowObject.aspx?ID=<%# Eval("RevGalleryTitleID") %> ',640,540);"
							value="اجزا" />						
					</td>
                    <td align="center" >
                        &nbsp;<%# Eval("Sort").ToString().Trim() %>
                    </td>
                     <td align="center" >
                        &nbsp;<%# Eval("enable").ToString().Trim() =="1" ? "فعال" : "غیر فعال"%>
                    </td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("RevGalleryTitleID").ToString().Trim() %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
						
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("RevGalleryTitleID").ToString().Trim() %>'
							CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>

    <input style="width: 70px" type="button" onclick="SelectPrepMsg('/RevSlideShowPreview.aspx',930,400);"
							value="پیش نمایش" />	<br />	
    <a href="/SettingUnits.aspx?ID=21">تنظیمات اسلاید</a>

</asp:Content>
