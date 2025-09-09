<%@ Page Title="" Language="C#" MasterPageFile="~/Automation/Automation.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Portal.Automation.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AutomationBody" runat="server">
	<table align="center">
		<tr>
			<td valign="top">
				<asp:PlaceHolder ID="StatePH" EnableViewState="False" runat="server"></asp:PlaceHolder>
			</td>
			<td valign="top">
				<asp:PlaceHolder ID="EldersPH" EnableViewState="False" runat="server"></asp:PlaceHolder>
			</td>
		</tr>
	</table>


</asp:Content>
