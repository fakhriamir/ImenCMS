<%@ Page Title="" Language="C#" MasterPageFile="~/Archive/Archive.master" AutoEventWireup="true" CodeBehind="AddFiles.aspx.cs" Inherits="Portal.Archive.AddFiles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ArchiveHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ArchiveBody" runat="server">
	<table dir="rtl" align="center" class="TableColor" width="80%">
		<tr class="RowHead">
			<td colspan="2" align="center">آضافه کردن فایل</td>
		</tr>
		<tr>
			<td>شاخه آرشیو</td>
			<td>
				<asp:DropDownList DataValueField="ArchiveCategoryIDDL" ID="archivecategoryDL"  runat="server" DataTextField="Name" />*</td>
		</tr>
		<tr>
			<td>موضوع آرشیو</td>
			<td>
				<asp:DropDownList DataValueField="ArchiveID" DataTextField="name" ID="archiveidDL" runat="server" />*</td>
		</tr>
		<tr>
			<td>نوع فایل آرشیو</td>
			<td>
				<asp:DropDownList ID="archivetypeidDL" runat="server" DataValueField="ArchiveTypeID" DataTextField="name"/>*</td>
		</tr>
		<tr>
			<td>عنوان پروژه</td>
			<td>
				<asp:DropDownList ID="projectidDL" runat="server" MaxLength="25" /></td>
		</tr>
		<tr>
			<td>عنوان فایل</td>
			<td>
				<asp:TextBox ID="titleTB" runat="server" MaxLength="64" />*</td>
		</tr>
		<tr>
			<td>توضیح فایل</td>
			<td>
				<asp:TextBox ID="descriptionTB" TextMode="MultiLine" runat="server" MaxLength="1024" /></td>
		</tr>
		
		<tr>
			<td>انتخاب فایل</td>
			<td>
				<asp:FileUpload ID="FileUpload1" runat="server" /></td>
		</tr>
		
		
		<tr>
			<td>نسخه فایل</td>
			<td>
				<asp:TextBox ID="fileverTB" runat="server" MaxLength="25" /></td>
		</tr>
	
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="اضافه شود" Width="70px" OnClick="SaveBTN_Click" /><input type="reset" style="WIDTH: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20"></td>
		</tr>
	</table>
	<br>
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="rtl" id="My" align="center" cellspacing="1" cellpadding="2" width="100%" border="1">
		<tr class="RowHead">
			<td><%=GetGlobalResourceObject("resource", "ID")%></td>
			<td><%=GetGlobalResourceObject("resource", "Title")%></td>
			<td>دسترسی</td>
			<td>نسخه جدید</td>
			<td><%=GetGlobalResourceObject("resource", "Edit")%></td>
			<td><%=GetGlobalResourceObject("resource", "Del")%></td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">&nbsp;<%# DataBinder.Eval(Container.DataItem, "ArchiveFileID").ToString().Trim() %></td>
					<td align="center">&nbsp;<%# Eval("Title").ToString().Trim() %></td>
					<td align="center">
                        <img style="width:20px; height:20px;cursor:pointer;" src="Images/References.png" onclick="SelectPrepMsg('/Archive/Access-<%# Eval("ArchiveFileID") %>.aspx',650,500);"
                            alt="دسترسی" />
                    </td>
					<td align="center">
                        <img style="width:20px; height:20px;cursor:pointer;" src="Images/References.png" onclick="SelectPrepMsg('/Archive/AddFileDialog-<%# Eval("ArchiveFileID") %>.aspx',650,500);"
                            alt="نسخه جدید" />
                    </td>
					<td align="center">
                        <img style="width:20px; height:20px;cursor:pointer;" src="Images/References.png" onclick="SelectPrepMsg('/Archive/View-<%# Eval("ArchiveFileID") %>.aspx',650,500);"
                            alt="مشخصات فایل" />
                    </td>
					<td align="center">
						<asp:ImageButton ImageUrl="/Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("ArchiveFileID").ToString().Trim() %>' CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" /></td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="/Imgs/zarb.gif" Width="16px" Height="16px" CommandArgument='<%# Eval("ArchiveFileID").ToString().Trim() %>' CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" /></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
