<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminDefault2.aspx.cs" Inherits="Portal.Admin.AdminDefault2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table  class="TableColor" align="center" style="width: 90%" border="0">
		<tr>
			<td colspan="3" align="center" valign="middle" style="height: 150px; background-color: #D4E4F1;">
				<asp:DropDownList ID="DL11" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td colspan="3" align="center" valign="middle" style="background-color: #D4E4F1;">
				<asp:DropDownList ID="DL12" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
			</td>
		</tr>
		<tr style="background-color: #D4F1F1">
			<td align="center">
				<asp:DropDownList ID="DL21" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
			</td>
			<td align="center" colspan="2">
				<asp:DropDownList ID="DL22" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td align="center" style="height: 150px; background-color: #ECD4F1; width: 20%">
				<asp:DropDownList ID="DL311" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL312" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL313" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL314" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL315" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL316" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
			</td>
			<td style="width: 50%" align="center">
				<asp:DropDownList ID="DL321" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL322" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL323" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL324" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL325" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL326" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
			</td>
			<td align="center" style="height: 150px; background-color: #ECD4F1; width: 30%">
				<asp:DropDownList ID="DL331" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL332" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL333" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL334" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL335" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL336" DataTextField="Name" DataValueField="DefaultItemID"
					runat="server">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td colspan="3" align="center">
				<asp:DropDownList ID="DL41" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
				<br />
				<asp:DropDownList ID="DL42" DataTextField="Name" DataValueField="DefaultItemID" runat="server">
				</asp:DropDownList>
			</td>
		</tr>
	</table>
	<center>
		<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNSave %>" OnClick="SaveBTN_Click" />
	</center>
</asp:Content>
