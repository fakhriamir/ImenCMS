<%@ Page Language="C#" EnableViewState="false"  AutoEventWireup="true" CodeBehind="Logins.aspx.cs" Inherits="NewsService.Logins" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>..:: Login Page ::..</title>
	<%--<link href="page.css" type="text/css" rel="stylesheet" />--%>
	<script src="/scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
	<script src="/Scripts/jquery.alerts.js" type="text/javascript"></script>
	<link href="/Style/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=PassTB.ClientID %>").val("Password");
            $("#<%=PassTB.ClientID %>").blur(function () {
                if (this.value == "")
                    $("#<%=PassTB.ClientID %>").val("Password");
            });
            $("#<%=PassTB.ClientID %>").focusin(function () {
                if (this.value == "Password")
                    $("#<%=PassTB.ClientID %>").val("");
            });
            $("#<%=PassTB.ClientID %>").focusout(function () {
                if (this.value == "")
                    $("#<%=PassTB.ClientID %>").val("Password");
            });

            $("#<%=UserTB.ClientID %>").val("User Name");
            $("#<%=UserTB.ClientID %>").blur(function () {
                if (this.value == "")
                    $("#<%=UserTB.ClientID %>").val("User Name");
            });
            $("#<%=UserTB.ClientID %>").focusin(function () {
                if (this.value == "User Name")
                    $("#<%=UserTB.ClientID %>").val("");
            });
            $("#<%=UserTB.ClientID %>").focusout(function () {
                if (this.value == "")
                    $("#<%=UserTB.ClientID %>").val("User Name");
            });
       });
    </script>
	<style type="text/css">
		#login-box
		{
			width: 411px;
			height: 391px;
			/*padding: 58px 76px 0 76px;*/
			color: #ebebeb;
			font: 12px Arial, Helvetica, sans-serif;
			background: url(/imgs/loginBox.png)  ;          
			margin:0px auto ;
            margin-top:80px;
		}
		#login-box img
		{
			border: none;
		}		
		#login-box h2
		{
			padding: 0;
			margin: 0 auto 0 auto;
			color: #ebebeb;
			font: bold 14pt tahoma;
		}		
		#login-box-field
		{
			float: right;
			display: inline;
			width: 370px;
			margin: 0;
			margin: 3px 0 7px 0;
		}	
		.form-login
		{
			width: 305px;
			padding: 10px 4px 6px 10px;
			border: 1px solid #ddd;
			background-color: #ebebeb;
			font-size: 16px;
			color: #a0a0a0;
            border-radius:3px;
		}		
		.login-box-options
		{
			clear: both;
			padding-left: 87px;
			font-size: 11px;
		}		
		.login-box-options a
		{
			color: #ebebeb;
			font-size: 11px;
		}
	    #UserTB {
	     background:url('/Imgs/user.png') no-repeat #ebebeb; padding:3px; height:30px; }
	    #PassTB {
	    background:url('/Imgs/pass.png') no-repeat #ebebeb; padding:3px;  height:30px;}
	</style>
</head>
<body style="background-color:#a0a0a0; background:url('/Imgs/loginBG.png'); margin:0;">
	<form id="form1" runat="server">
        
		<div id="login-box">
			<div id="login-box-field" style="margin-top:90px;" >
				<asp:TextBox CssClass="form-login" ID="UserTB" dir="rtl" MaxLength="64" runat="server" Text="<%$ resources: resource, UserName %>"
					autocomplete="on"  /></div>

			<div id="login-box-field">
				<asp:TextBox CssClass="form-login" ID="PassTB" dir="rtl" runat="server"   MaxLength="64" TextMode="Password" /></div>

			<div id="login-box-field">
				<asp:DropDownList CssClass="form-login" ID="LanguageDL"  dir="ltr" runat="server">
					<asp:ListItem Text="فارسی" Value="" />
					<asp:ListItem Text="English" Value="en-us" />
				</asp:DropDownList>
			</div>
			<br />
			<%--<span class="login-box-options"><input type="checkbox" name="1" value="1"> Remember Me <a href="#" style="margin-left:30px;">Forgot password?</a></span>--%>
			<div style="clear: both; padding-top: 40px; margin-left: 150px;">
				<asp:ImageButton ID="LoginBTN" src="/Imgs/login-btn.png" runat="server" Text="تقاضاي ورود"
					OnClick="LoginBTN_Click" />
			</div>
		</div>
        <div style="clear:both;"></div>
        <div style="margin-left:100px; margin-top:20px;"><img src="/Imgs/arm.png" /></div>
        <div style="background:url('/Imgs/line.png') repeat-x; height:28px; width:100%;"></div>

	</form>
</body>
</html>
