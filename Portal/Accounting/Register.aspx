<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/Accounting.master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Portal.Accounting.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TelegramBody" runat="server">
<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrBeforTitle,0)%>
	<%=HttpContext.GetGlobalResourceObject("resource", "TelegramRegister").ToString()%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterTitle,0)%>
<div class="TelegramComment">	با تشکر از انتخاب شما<br />
			لازم به ذکر است پس از ثبت نام کد فعال سازی به موبایل شما پیامک خواهد شد. پس از وارد کردن کد ارسالی شما قادر خواهید بود در سیستم فعالیت داشته باشید
	</div>	
	<table dir="rtl" align="center" class="TableColor" id="Table1" cellspacing="1" cellpadding="1" width="80%">
	
		<tr>
			<td>
				شماره موبایل
			</td>
			<td>
				<asp:TextBox ID="MobileNoTB" dir="ltr" runat="server" MaxLength="11"></asp:TextBox>* (09395510284)
			</td>
		</tr>
		<tr>
			<td>
				<%=HttpContext.GetGlobalResourceObject("resource", "UserName").ToString()%>
			</td>
			<td>
				<asp:TextBox ID="usernameTB" dir="ltr" runat="server" MaxLength="32"></asp:TextBox>*
			</td>	
		</tr>
		<tr>
			<td>
				<%=HttpContext.GetGlobalResourceObject("resource", "Password").ToString()%>
			</td>
			<td>
				<asp:TextBox ID="passTB" dir="ltr" TextMode="Password" runat="server" MaxLength="32"></asp:TextBox>*
				(حداقل 5 کاراکتر)
			</td>
		</tr>
		<tr>
			<td>
				<%=HttpContext.GetGlobalResourceObject("resource", "PasswordCheck").ToString()%>
			</td>
			<td>
				<asp:TextBox ID="pass1TB" dir="ltr" TextMode="Password" runat="server" MaxLength="32"></asp:TextBox>*
			</td>
		</tr>
		<tr>
			<td>
				<%=HttpContext.GetGlobalResourceObject("resource", "Email").ToString()%>
			</td>
			<td>
				<asp:TextBox ID="emailTB" dir="ltr" runat="server" MaxLength="32"></asp:TextBox>*<asp:RegularExpressionValidator
					ID="EmailValidator" runat="server" ControlToValidate="emailTB" ErrorMessage="ایمیل صحیح وارد نمایید"
					ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="FasTRegister"></asp:RegularExpressionValidator>
			</td>
		</tr>
		<tr>
			<td>
				<%=HttpContext.GetGlobalResourceObject("resource", "NameFamily").ToString()%>
			</td>
			<td>
				<asp:TextBox ID="nameTB" runat="server" MaxLength="64"></asp:TextBox>*
			</td>
		</tr>
		<tr>
			<td colspan=2>
				<asp:CheckBox ID="PolicyCB" Checked="true" runat="server" /><a href="#" onclick="window.open('/Telegram/policy.htm','','width=500,height=700')">قوانین و مقرات سایت را مطالعه و قبول دارم</a>
			</td>
		</tr>
		<tr>
		<td>
				کد امنیتی
			</td>
        <td valign=middle>
    <asp:TextBox ID="SecurityImageTB" MaxLength="5" Width="50px" runat="server"></asp:TextBox>&nbsp;<img id="SecureImage" src="/SecureImage.aspx" alt="تصویر امنیتی" title="تصویر امنیتی" />&nbsp;<img onclick='document.getElementById("SecureImage").src = "/SecureImage.aspx?ID="+(new Date).getTime();' style="cursor:hand" src="/Images/Loading-1.gif" alt="تغییر عکس" title="تغییر عکس" />
    </td></tr>
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SentBotton %>"
					Width="70px" OnClick="SaveBTN_Click" ValidationGroup="FasTRegister"></asp:Button>
				<input type="reset" style="width: 70px" value="از نو" size="20">
			</td>
		</tr>
	</table>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterBody,0)%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterFooter,0)%>
</asp:Content>
