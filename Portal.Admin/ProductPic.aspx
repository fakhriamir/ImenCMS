<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="ProductPic.aspx.cs" Inherits="Portal.Admin.ProductPic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<%if (ViewDR.Items.Count != 0)
		{%>
	<table dir="rtl" id="My" align="center" cellspacing="1" cellpadding="2" width="100%" border="1">

		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">&nbsp;<%# Eval("pic").ToString().Trim() %><br />
						<img  src="<%=ADAL.A_CheckData.GetFilesRoot(true) %>/Images/proPic/<%#Eval("pic")%>" style="width: 250px" />
					</td>
					<td>
					<asp:FileUpload  Visible="<%#!Getvisibility(Container.ItemIndex) %>" id="PicMF" runat="server"  />
						<asp:Button Visible="<%#Getbuttonvisibility(Container.ItemIndex,0) %>" CommandArgument='<%# Eval("pic").ToString().Trim() %>' CommandName="Edit" ID="pic0BTN" runat="server" Text="تغییر" />
						<asp:Button Visible="<%#Getbuttonvisibility(Container.ItemIndex,1) %>" CommandArgument='<%# Eval("pic").ToString().Trim() %>' CommandName="THEdit" ID="pic1BTN" runat="server" Text="تغییر" />
					
						<asp:ImageButton Visible="<%#Getvisibility(Container.ItemIndex) %>" OnClientClick="return confirm_delete();" ImageUrl="/Imgs/zarb.gif" Width="16px" Height="16px" CommandArgument='<%# Eval("pic").ToString().Trim() %>' CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
		<tr><td colspan="2">
			<asp:FileUpload id="PicsMF" Multiple="Multiple" class="multi" runat="server"/>
				<asp:Button  ID="pic2BTN" runat="server" Text="بارگذاری" OnClick="pic2BTN_Click" />
				
		    </td></tr>
	</table>
	<%}%>
</asp:Content>
