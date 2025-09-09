<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/Accounting.master" AutoEventWireup="true" CodeBehind="Logins.aspx.cs" Inherits="Portal.Accounting.Logins" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AccountingBody" runat="server">
<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrBeforTitle,0)%>
  <%=Tools.Tools.GetSetting(181, HttpContext.GetGlobalResourceObject("resource", "TelegramLogin").ToString())%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterTitle,0)%>
	<script type="text/javascript">
		function OnKeyPress(e) {
			if ((e == null) && (!window.event)) { return; } // ensures Mozilla doesn't handle on keydown which only IE needs
			var RealEvent = (e == null ? window.event : e); // Who's the Real Slim Event

			var RealKeyCode = RealEvent.keyCode ? RealEvent.keyCode : RealEvent.which; // Will the Real Key Code Please stand up
			if (RealKeyCode == 13) {
				RealEvent.returnValue = false; // no return value
				RealEvent.cancelBubble = true; // plz don't bubble kthxbai
				document.getElementById("<%=MyLoginBTN.ClientID%>").click();
			}
		}
	</script>
<div id="Div1" runat="server">
    <table id="Table1" class="TableColor" cellspacing="1" cellpadding="1" align="center">
        <tr>
            <td class="Def_Login_UserName_TD">
                <%=HttpContext.GetGlobalResourceObject("resource", "UserName").ToString()%>:
            </td>
            <td>
                <asp:TextBox CssClass="Def_Login_UserTB" ID="LoginUserTB" dir="ltr" runat="server" autocomplete="off" />
            </td>
        </tr>
        <tr>
            <td class="Def_Login_Password_TD">
                <%=HttpContext.GetGlobalResourceObject("resource", "Password").ToString()%>:
            </td>
            <td>
                <asp:TextBox CssClass="Def_Login_PassTB" ID="LoginPassTB" dir="ltr" runat="server"
                    autocomplete="off" TextMode="Password" />
            </td>
        </tr>
		<tr>
		<td class="Def_Login_Password_TD">کد امنیتی</td>
			<td >
				<asp:TextBox onkeypress="OnKeyPress(event);" ID="SecurityImageTB" MaxLength="5" Width="50px" runat="server"></asp:TextBox><img
					id="SecureImage" src="/SecureImage.aspx" alt="تصویر امنیتی" title="تصویر امنیتی" /><img
						onclick='document.getElementById("SecureImage").src = "/SecureImage.aspx?ID="+(new Date).getTime();'
						style="cursor: hand" src="/Images/Loading-1.gif" alt="تغییر عکس" title="تغییر عکس" />
			</td>
		</tr>
        <tr>
            <td align="center" colspan="2" class="Def_Login_BTN_TD">
                &nbsp;
                <asp:Button CssClass="Def_Login_BTN" ID="MyLoginBTN" runat="server" Text="<%$ resources: resource, SentBotton %>"
                    OnClick="LoginBTN_Click"></asp:Button>
                <br />
               
            </td>
        </tr>
    </table>
</div>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterBody,0)%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterFooter,0)%>

</asp:Content>
