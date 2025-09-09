<%@ Page Title="" EnableViewState="true" ValidateRequest="false" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ProductView.aspx.cs" Inherits="Portal.Admin.ProductView" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table2" cellspacing="1" cellpadding="1" >
		<tr>
			<td>
				
					<asp:TextBox ID="IDFilterTB" runat="server"></asp:TextBox></td>
				<td>
					<asp:Button ID="FilterBTN" runat="server" Text="<%$ resources: resource, Filter %>" OnClick="FilterBTN_Click" 
						 /></td>
		</tr>
		</table>
	<br />
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="1" cellpadding="2" width="100%"
		border="1">
		
		<tr class="RowHead">
			<td>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Visit")%>
			</td>
				<td>
			لینک
			</td>
				<td>
			مبلغ
			</td>
			<td>
			عکس
			</td>
			
		</tr>
		<asp:Repeater runat="server" ID="ViewDR"    >
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("ProductID").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("Name").ToString().Trim() %>
						<br /> <%=Tools.Tools.GetSiteURL() %>/Shop/Product/<%#Eval("ProductID")%>/<%#Tools.Tools.UrlWordReplace( Eval("Name").ToString())%>
  
					</td>
					<td align="center">
						&nbsp;<%# Eval("hit").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("money").ToString().Trim() %></td>
					<td align="center" ><span style="direction:ltr">
                                          </span>
                                					</td>
					<td align="center">
			   <img  src="<%=ADAL.A_CheckData.GetFilesRoot(true) %>/Images/proPic/th_<%#Eval("PicName")%>" style="width:80px" />
                                        		</td>
				
				</tr>
			</ItemTemplate>
		</asp:Repeater>
		<tr>
			<td colspan=5 align="center">
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
