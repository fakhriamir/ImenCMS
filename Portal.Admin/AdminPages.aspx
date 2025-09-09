<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminPages.aspx.cs" Inherits="Portal.Admin.AdminPages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table class="TableColor" align="center" style="width: 90%" border="0">
		<tr>
			<td colspan="2" align="center" valign="middle" style="height: 150px; background-color: #D4E4F1;">
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center" valign="middle" style="background-color: #D4E4F1;">
				<asp:DropDownList ID="DL0" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList><br />
				<asp:DropDownList ID="DL01" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
				<br />
			</td>
		</tr>
		<tr>
			<td align="center" style="height: 150px; background-color: #ECD4F1; width: 20%">
				<asp:DropDownList ID="DL1" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DropDownList1" Enabled="false" runat="server">
					<asp:ListItem Selected="True" Text="<%$ resources: resource, RelatedBoxesTypes %>"></asp:ListItem>
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL2" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL3" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL4" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL5" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL6" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
			</td>
			<td style="width: 50%; height: 300px" valign="bottom" align="center">
				<table border="0" style="width: 100%">
					<tr>
						<td>
							<asp:DropDownList ID="DL7" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
							</asp:DropDownList>
						</td>
						<td>
							<asp:DropDownList ID="DL8" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
							</asp:DropDownList>
						</td>
						<td>
							<asp:DropDownList ID="DL9" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
							</asp:DropDownList>
						</td>
					</tr>
					<tr>
						<td>
							<asp:DropDownList ID="DL10" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
							</asp:DropDownList>
						</td>
						<td>
							<asp:DropDownList ID="DL11" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
							</asp:DropDownList>
						</td>
						<td>
							<asp:DropDownList ID="DL12" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
							</asp:DropDownList>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<center>
		<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNSave %>" OnClick="SaveBTN_Click" />
	</center>
</asp:Content>
