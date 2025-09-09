<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SMSReferrer.aspx.cs" Inherits="Portal.Admin.SMS_Referrer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table align="center" class="TableColor" dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" cellspacing="3" cellpadding="3" width="100%" border="0">
		<tr>
			<th>
				
			</th>
			<th>
				
			</th>
			
		</tr>
		<asp:Repeater ID="LogsDR" runat="server">
			<ItemTemplate>
				<tr>
					<td>
						<%# Eval("RefererUserID").ToString().Trim()%>
					</td>
					<td>
						<%# Eval("SendCount").ToString().Trim()%>
					</td>
									
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
</asp:Content>
