<%@ Page Title="" Language="C#" MasterPageFile="~/Dialog.Master" AutoEventWireup="true" CodeBehind="Reference.aspx.cs" Inherits="Portal.Automation.Reference" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCP" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BoduCB" runat="server">
	
	<fieldset>
        <legend>ارجاع نامه</legend>
        <img id="MyBackBTN" src="/automation/Images/back.png" alt="بازگشت" onclick="BackClick()" style="float:left; cursor:pointer; width:25px;height:25px;" />
        	<table dir="rtl" align="center" class="TableColor" width="80%">
		<tr>
			<td>اولویت ارجاع</td>
			<td><asp:DropDownList ID="PeriorityDL" runat="server" DataValueField="OfficePriorityID"  DataTextField="name">
                </asp:DropDownList>
				</td>
		</tr>
		<tr>
			<td>نوع ارجاع</td>
			<td><asp:DropDownList ID="OfficeReferenceTypeDL" DataValueField="OfficeReferenceTypeid" DataTextField="name"  runat="server">
                </asp:DropDownList>
				</td>
		</tr>
		<tr>
			<td>شماره نامه</td>
			<td>
				<asp:TextBox ID="officeletteridTB" Enabled="false" runat="server" MaxLength="25" /></td>
		</tr>
		<tr>
			<td>گیرنده</td>
			<td>
			<asp:DropDownList ID="RecieverDL" runat="server">
                </asp:DropDownList></td>
		</tr>
		
		<tr>
			<td>پاراف</td>
			<td>
				<asp:TextBox ID="paraphTB" TextMode="MultiLine" runat="server"  Width="400px" Height="100px"  /></td>
		</tr>
		<tr>
			<td>پاراف شخصی</td>
			<td>
				<asp:TextBox ID="perparaphTB" TextMode="MultiLine" runat="server" Width="400px" Height="70px"  /></td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="ارجاع شود" OnClick="SaveBTN_Click" /></td>
		</tr>
	</table>
	
    </fieldset>

</asp:Content>
