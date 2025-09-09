<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SendSMS.aspx.cs" Inherits="Portal.Admin.SendSMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">

function textCounter(field,cntfield,maxlimit) {
if (field.value.length > maxlimit) // if too long...trim it!
field.value = field.value.substring(0, maxlimit);
// otherwise, update 'characters left' counter
else
cntfield.innerHTML = field.value.length;
}
//  End -->


</script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table class="TableColor" >
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "SMSCredit")%>
			</td>
			<td>
				<asp:Label ID="SMSLB" runat="server" ForeColor="Red"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "SelectCatigory")%>
			</td>
			<td>
				<asp:CheckBoxList ID="PhoneTypeCBL" runat="server" DataTextField="Name" DataValueField="PhoneBookTypeID"
					RepeatColumns="5">
				</asp:CheckBoxList>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Text")%>
			</td>
			<td>
				<asp:TextBox ID="MatnTB" runat="server" Height="121px" TextMode="MultiLine" MaxLength="512" Width="177px"></asp:TextBox><br />
				<div><%=GetGlobalResourceObject("resource", "CharacterNO")%>:<span id="description_counter"></span></div>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				
				<asp:Button ID="CheckSMSBTN" runat="server" OnClick="CheckSMSBTN_Click" Text="<%$ resources: resource, Calculate %>" />
				<asp:Button ID="SendSMSBTN" runat="server" Enabled="False" OnClick="SendSMSBTN_Click"
					Text="<%$ resources: resource, SendSMS %>" Width="110px" />
			</td>
		</tr>
	</table>
</asp:Content>
