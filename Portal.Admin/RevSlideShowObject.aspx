<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="RevSlideShowObject.aspx.cs" Inherits="Portal.Admin.RevSlideShowObject" %>
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
				<%--<%=GetGlobalResourceObject("resource", "Lang")%>--%>
			</td>
			<td>
				<asp:DropDownList ID="MyLanguageDL" runat="server" DataTextField="Name" DataValueField="LangID" Visible="false">
				</asp:DropDownList>
			</td>
		</tr>
		
		
        <tr>
			<td>
				XPos
			</td>
			<td>
				<asp:TextBox ID="XPOSTB" runat="server" MaxLength="64" Width="166px" />
			</td>
		</tr>
        <tr>
			<td>
				YPos
			</td>
			<td>
				<asp:TextBox ID="YPOSTB" runat="server" MaxLength="64" Width="166px" />
			</td>
		</tr>
        <tr>
			<td>
				XPos-Start
			</td>
			<td>
				<asp:TextBox ID="XStartTB" runat="server" MaxLength="64" Width="166px" />
			</td>
		</tr>
        <tr>
			<td>
				YPos-Start
			</td>
			<td>
				<asp:TextBox ID="YStartTB" runat="server" MaxLength="64" Width="166px" />
			</td>
		</tr>
        <tr>
			<td>
				اولویت
			</td>
			<td>
				<asp:TextBox ID="SortTB" runat="server" MaxLength="64" Width="166px" />
			</td>
		</tr>
        <tr>
			<td>
				Z-Index
			</td>
			<td>
				<asp:TextBox ID="ZindexTB" runat="server" MaxLength="64" Width="166px" />
			</td>
		</tr>
       <tr>
			<td>
				مدت زمان نمایش
			</td>
			<td>
				<asp:TextBox ID="timeTB" runat="server" MaxLength="64" Width="166px" />
			</td>
		</tr>
       <tr>
			<td>
				عکس
			</td>
			<td>
				 <asp:FileUpload ID="MyFileBrows" runat="server"   />
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNText %>" OnClick="SaveBTN_Click"/>
                <input type="reset" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20">
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
				XPOS
			</td>
            <td>
                YPOS
            </td>
            <td>XPOS-Start</td>
            <td>YPOS-Start</td>
            <td>اولویت</td>
            <td>مدت زمان نمایش</td>
            <td>عکس</td>
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
						&nbsp;<%# Eval("RevGalleryObjectID").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("xpos").ToString().Trim() %></td>
                    <td align="center">
						&nbsp;<%# Eval("ypos").ToString().Trim() %></td>
                    <td align="center">
						&nbsp;<%# Eval("xpos-start").ToString().Trim() %></td>
                    <td align="center">
						&nbsp;<%# Eval("ypos-start").ToString().Trim() %></td>
                   	<td align="center">
                        &nbsp;<%# Eval("sort").ToString().Trim() %>
					</td>
                   	<td align="center">
                        &nbsp;<%# Eval("time").ToString().Trim() %>
					</td>
                    <td>
                        <img style="width: 100px; height: auto" src='<%=ADAL.A_CheckData.GetFilesRoot(true) %>/Images/Gallery/<%# Eval("img").ToString().Trim()%>' />
                    </td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("RevGalleryObjectID").ToString().Trim() %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
						
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("RevGalleryObjectID").ToString().Trim() %>'
							CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
