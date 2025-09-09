<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SendMail.aspx.cs" Inherits="Portal.Admin.SendMail" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table class="TableColor" style="width:100%" >
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "SubscriberType")%>
			</td>
			<td>
				<asp:DropDownList ID="SubscribeTypeDL" DataValueField="SubscribeTypeID" DataTextField="Name"
					runat="server">
				</asp:DropDownList>
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=GetGlobalResourceObject("resource", "EmailReceiver")%>:<asp:TextBox ID="TestMailTB"
					MaxLength="50" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Subject")%>
			</td>
			<td>
				<asp:TextBox ID="titleTB" runat="server" MaxLength="64" Width="500px" />
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<CKEditor:CKEditorControl FormatSource="false" ID="MyFCK" runat="server">
				</CKEditor:CKEditorControl>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				
				<asp:Button ID="SendBTN" runat="server" Text="<%$ resources: resource, Send %>" OnClick="SendBTN_Click" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Description")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "ReplaceName")%><br />
				#Email# جهت جایگزین شدن ایمیل آدرس<br />
				#ID# جهت جایگزین شدن کد اشتراک

			</td>
		</tr>
		<tr>
			<td>
				لینک لغو عضویت
			</td>
			<td dir="ltr">
				http://www.آدرس سایت شما.com/unSubscribe-#ID#.aspx?Email=#Email#

			</td>
		</tr>
	</table>
</asp:Content>
