<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminMemberPages.aspx.cs" Inherits="Portal.Admin.AdminMemberPages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table class="TableColor" align="center" style="width: 90%" border="0">
		<tr>
			<td colspan="3" align="center" valign="middle" style="background-color: #D4E4F1;">
				<asp:DropDownList ID="DL0" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL1" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL2" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
				<br />
			</td>
		</tr>
		<tr>
			<td align="center" style="height: 150px; background-color: #ECD4F1; width: 20%">
				<asp:DropDownList ID="DL1_1" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL1_2" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL1_3" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL1_4" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL1_5" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
			</td>
			<td align="center" style="height: 150px; background-color: #ECD4F1; width: 50%">
				<asp:DropDownList ID="DL2_1" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL2_2" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL2_3" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL2_4" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL2_5" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
			</td>
			<td align="center" style="height: 150px; background-color: #ECD4F1; width: 20%">
				<asp:DropDownList ID="DL3_1" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL3_2" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL3_3" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL3_4" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL3_5" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td colspan="3" align="center" valign="middle" style="background-color: #D4E4F1;">
				<asp:DropDownList ID="DL3" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL4" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL5" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
				<br />
			</td>
		</tr>
	</table>
	<center>
		<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNSave %>" OnClick="SaveBTN_Click" />
	</center>
</asp:Content>
