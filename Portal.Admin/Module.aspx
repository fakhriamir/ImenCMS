<%@ Page Language="C#" EnableEventValidation="false" ValidateRequest="false" EnableViewState="true" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Module.aspx.cs" Inherits="Portal.Admin.MyModule" %>
<asp:Content ContentPlaceHolderID="head" runat="server" ID="ContentA1">
	<script language="javascript" type="text/javascript">
		$(document).ready(function () {
			<%=ModuleType%>
			$("#ModuleTypeDL").change(function () {
				var inval = $("#ModuleTypeDL").val();
				//alert(inval);
				$("#TypeStr").text("$" + myType[inval].replace(/\$/g, "$ - $") + "$");
				$("#TypeValDL").empty();
				$("#TypeValDL").append("<option value=0>همه موضوعات</Option>");
				$.ajax({
					type: "POST",
					url: "/ajax.aspx?MyType=3&myVal=" + inval,
					//data: data,
					
					success: function (html) {
						//alert(html);
						eval(html);
						$.each(MyCont, function (val, text) {
							//alert($("#TypeValDL"));
							$("#TypeValDL").append("<option value=" + val + ">" + text + "</Option>");
						});
						$("#TypeValDL").val($("#ItemIDTB").val());
					},
					error: function () {
						jAlert('برقراری ارتباط با مشکل روبرو شده است', 'پیام سیستم');
						return;
					}
				});
				
			});
			$("#TypeValDL").change(function () {
				//alert($(".TypeValDL").val());
				$("#ItemIDTB").val($("#TypeValDL").val());
			});

			$("#ModuleTypeDL").change();
		});
	</script>
	<style type="text/css">
		.auto-style1 {
			width: 100%;
		}
	</style>
</asp:Content>
<asp:Content ContentPlaceHolderID="Body" runat="server" ID="aa">
	<div>
		<asp:Repeater runat="server" ID="KadrDR">
			<ItemTemplate>
				(<%# Eval("ID").ToString().Trim() %>-<%# Eval("Name").ToString().Trim() %>)
			</ItemTemplate>
		</asp:Repeater>
	</div>

	<%=GetGlobalResourceObject("resource", "ModuleBox")%>
	<asp:DropDownList ID="TextTypeDL"  CssClass="NotSearch"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="TextTypeDL_SelectedIndexChanged" />

	<br />
	<%=GetGlobalResourceObject("resource", "Lang")%>:
	<asp:DropDownList AutoPostBack="True" OnSelectedIndexChanged="TextTypeDL_SelectedIndexChanged"
		ID="LanguageDL" runat="server"  CssClass="NotSearch"  DataTextField="Name" DataValueField="LangID" />
	<br />
	<%=GetGlobalResourceObject("resource", "Title")%>:<asp:TextBox runat="server" ID="CadrTitleTB" />
	<fieldset><legend>دیتای ماژول</legend>
		
		<table class="auto-style1">
			<tr>
				<td>نام جدول دیتا</td>
				<td><asp:DropDownList ID="ModuleTypeDL" ClientIDMode="Static" runat="server"  CssClass="NotSearch"  DataTextField="Name" DataValueField="ModuleTypeID" /></td>
				<td>تعداد رکورد:<asp:TextBox runat="server" ID="CountTB" /></td>
                <td>شروع نماش از رکورد</td>
				<td><asp:TextBox runat="server" ID="StartFromTB" /></td>
			</tr>
			<tr>
				<td>نمایش اطلاعات</td>
				<td>
					<asp:DropDownList ID="SortDL" runat="server" CssClass="NotSearch">
						<asp:ListItem Value="0" Text="آخرین رکورد به اولین"></asp:ListItem>
						<asp:ListItem Value="1" Text="اولین رکورد به آخرین"></asp:ListItem>
						<asp:ListItem Value="2" Text="تصادفی"></asp:ListItem>
                        <asp:ListItem Value="3" Text="پربازدیدترین"></asp:ListItem>
					</asp:DropDownList></td>
				<td>موضوع نمایشی</td>
				<td><asp:DropDownList ID="TypeValDL" ClientIDMode="Static" runat="server" CssClass="NotSearch" />
						<div style="display:none"><asp:TextBox ID="ItemIDTB" ClientIDMode="Static"  runat="server"></asp:TextBox></div>
				</td>
			</tr>
			<tr>
				<td>علامت های جایگزین دیتا</td>
				<td colspan="3">
					<div dir="ltr" id="TypeStr"></div>
				</td>
				
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
		</table>
		
		</fieldset>
	<fieldset><legend>تگ ابتدای ماژول</legend>
	<asp:TextBox  ID="FirstTagTB" runat="server" TextMode="MultiLine" Height="200px" Width="856px" dir=ltr/>
		</fieldset>
	<fieldset><legend>تگ تکراری همراه با رکورد</legend>
	<asp:TextBox  ID="ReapetTagTB" runat="server" TextMode="MultiLine" Height="200px" Width="856px" dir=ltr/>
		</fieldset>
	<fieldset><legend>تگ انتهای ماژول</legend>
	<asp:TextBox  ID="EndTagTB" runat="server" TextMode="MultiLine" Height="200px" Width="856px" dir=ltr/>
		</fieldset>	
	<br />
	<%=GetGlobalResourceObject("resource", "ContinueTag")%>: <asp:TextBox ID="MoreLinkTB" dir=ltr Width="600px" MaxLength="1024" runat="server" />
	<br />
	<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNSave %>" OnClick="SaveBTN_Click" />
</asp:Content>
