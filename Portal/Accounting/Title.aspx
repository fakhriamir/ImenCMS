<%@ Page Title="" Language="C#" MasterPageFile="~/Accounting/Accounting.master" AutoEventWireup="true" CodeBehind="Title.aspx.cs" Inherits="Portal.Accounting.Title" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AccountingHead" runat="server">
	<script src="/Scripts/select2.full.min.js" type="text/javascript"></script>
	<link href="/Style/select2.min.css" rel="stylesheet" />
	<script type="text/javascript" src="/Scripts/ui.core.min.js"></script>
<script type="text/javascript" src="/Scripts/ui.datepicker-cc.min.js"></script>
<script type="text/javascript" src="/Scripts/calendar.min.js"></script>
<script type="text/javascript" src="/Scripts/ui.datepicker-cc-fa.js"></script>
<link type="text/css" href="/Scripts/ui.core.css" rel="stylesheet" />
<link type="text/css" href="/Scripts/ui.theme.css" rel="stylesheet" />
<link type="text/css" href="/Scripts/ui.datepicker.css" rel="stylesheet" />
	<script src="/Scripts/jquery.price_format.2.0.min.js" type="text/javascript"></script>
	<script type="text/javascript">
		var optItems = '<%=OptionItems%>';
		var tafzilItem = '<%=TafzilItems%>';
		$("*").keypress(function (event) {
			//alert(event.which);
			if (event.which == 42) {//*****
				SaveTitle();				
				return false;
			}
			if (event.which == 47) {////////
				SaveTitleTemp();
				return false;
			}
			if (event.which == 43) {//+++++
				AddRecord();
				return false;
			}
			if (event.which == 45){ //-------
				DelRecord();
				return false;
			}			
		});
		function DelRecord()
		{
			$("#MyTr" + myI).remove();
			myI--;
		}
		var myI=0;
		function AddRecord()
		{
			myI++;
			if ($('#MyItemBody tr:last').html() != null)
				$('#MyItemBody tr:last').after('<tr id="MyTr' + myI + '"><td><select class="MoenCode" /></td><td><select class="TafzilCode"/></td><td><input type="text" class="Sharh"/></td><td><input type="text" class="Bedehkar" value="0"/></td><td><input type="text" class="Bestankar" value="0"/></td><td><input type="text" class="RefNo"/></td><td><input type="text" class="RefDate CalenderTB"/></td></tr>');
			else
				$('#MyItemBody').after('<tr id="MyTr' + myI + '"><td><select class="MoenCode" /></td><td><select class="TafzilCode"/></td><td><input type="text" class="Sharh"/></td><td><input type="text" class="Bedehkar" value="0"/></td><td><input type="text" class="Bestankar" value="0"/></td><td><input type="text" class="RefNo"/></td><td><input type="text" class="RefDate CalenderTB"/></td></tr>');
			LoadItems();
		}
		$(document).ready(function () {
			$(".AddRow").click(function () {
				AddRecord();
			});
			$(".DelRow").click(function () {
				DelRecord();
			});	
			$("#SaveBTN").click(function () {
				SaveTitle();
			});
			$("#SaveTempBTN").click(function () {
				SaveTitleTemp();
			});
			LoadItems();
		
		});
		function LoadItems()
		{
			$(".TafzilCode").html(tafzilItem);
			$(".TafzilCode").select2({
				placeholder: "انتخاب",
				dropdownAutoWidth: true
			});
			$('.Bedehkar').priceFormat({
				prefix: '',
				centsSeparator: ',',
				thousandsSeparator: ',',
				centsLimit: 0
			});
			$('.Bestankar').priceFormat({
				prefix: '',
				centsSeparator: ',',
				thousandsSeparator: ',',
				centsLimit: 0
			});
			$('.CalenderTB').datepicker({
				changeMonth: true,
				dateFormat: 'yy/mm/dd',
				changeYear: true
			});
			$(".MoenCode").select2({
				placeholder: "انتخاب",
				dropdownAutoWidth: true
			});
			$(".MoenCode").html(optItems);
			$('.Bestankar').focusout(SumInput);
			$('.Bedehkar').focusout(SumInput);
			$("#MyItemTables input").focus(function () {
				tr = $(this).parent().parent().attr('id');
				$("#MoeinTitleView").text($("#" + tr + " .MoenCode").find('option:selected').text());
			});
		}
		var Bedehkar = 0;
		var Bestankar = 0;
		function SaveTitle() {
			if (Bedehkar != Bestankar) {
				jAlert('مبالغ وارد شده تراز نمی باشد', 'خطا');
				return;
			}
			GetItemsStr();
			$("#<%=ServerSaveBTN.ClientID%>").click();
		}
		function SaveTitleTemp() {
			GetItemsStr();
			$("#<%=ServerSaveTempBTN.ClientID%>").click();
		}
		function SumInput() {
			Bedehkar = 0;
			Bestankar = 0;
			$('#MyItemTables').each(function () {
				$(this).find('.Bedehkar').each(function () {
					Bedehkar += parseInt($(this).val().replace(",", ""));
				});
				$(this).find('.Bestankar').each(function () {
					Bestankar += parseInt($(this).val().replace(",", ""));
				});
			});
			$("#Bedview").html(Bedehkar);
			$("#Besview").html(Bestankar);
			$("#Tarview").html("اختلاف سند:" + (Bestankar - Bedehkar));
		}
		function GetItemsStr() {
			var outStr = "";
			outStr += $("#MyTr .MoenCode").val() + "{#}";
			outStr += $("#MyTr .TafzilCode").val() + "{#}";
			outStr += $("#MyTr .Sharh").val() + "{#}";
			outStr += $("#MyTr .Bedehkar").val() + "{#}";
			outStr += $("#MyTr .Bestankar").val() + "{#}";
			outStr += $("#MyTr .RefNo").val() + "{#}";
			outStr += $("#MyTr .RefDate").val() + "{#}";
			for (var i = 1; i <= myI; i++) {
				outStr += $("#MyTr" + i + " .MoenCode").val() + "{#}";
				outStr += $("#MyTr" + i + " .TafzilCode").val() + "{#}";
				outStr += $("#MyTr" + i + " .Sharh").val() + "{#}";
				outStr += $("#MyTr" + i + " .Bedehkar").val() + "{#}";
				outStr += $("#MyTr" + i + " .Bestankar").val() + "{#}";
				outStr += $("#MyTr" + i + " .RefNo").val() + "{#}";
				outStr += $("#MyTr" + i + " .RefDate").val() + "{#}";
			}
			$(".TempTB").val(outStr);
		}
	</script>
	<style type="text/css">
		.MoenCode{
				width:80px;
		}
		.TafzilCode{
			width:80px;
		}
		.Sharh{
			width:150px;
		}
		.Bedehkar{
				width:100px;
				direction:ltr
		}
		.Bestankar{
			width:100px;
				direction:ltr
		}
		.RefNo{
			width:80px;
		}
		.RefDate{
			width:80px;
		}
	

	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AccountingBody" runat="server">
	<fieldset style="width: 70%; margin: 0 auto;">
		<legend>اطلاعات سند</legend>
		<table dir="rtl" align="center" width="80%">
			<tr>
				<td>شماره سند</td>
				<td>
					<asp:TextBox ID="titlenoTB" runat="server" MaxLength="25" dir="ltr" /></td>
				<td>تاریخ</td>
				<td>
					<asp:TextBox ID="datefaTB" CssClass="CalenderTB" runat="server" MaxLength="10" /></td>
			</tr>

			<tr>
				<td>شماره فرعی</td>
				<td>
					<asp:TextBox ID="titleno1TB" runat="server" MaxLength="25" dir="ltr"/></td>

				<td>شماره عطف</td>
				<td>
					<asp:TextBox ID="atfnoTB" runat="server" MaxLength="25" dir="ltr"/></td>
			</tr>
			<tr>
				<td>سال مالی</td>
				<td>
					<asp:DropDownList ID="YearDL" DataValueField="AccountingYearID" DataTextField="name" runat="server"></asp:DropDownList>
				</td>
			
			
				<td>نوع سند</td>
				<td>
					<asp:TextBox ID="typeTB" runat="server" MaxLength="25" />*</td>
			</tr>
			<tr>
				
				<td>توضیح سند</td>
				<td colspan="3">
					<asp:TextBox ID="descriptionTB" TextMode="MultiLine" runat="server" MaxLength="1024" Height="49px" Width="245px" /></td>
			</tr>
		</table>
	</fieldset>
	<fieldset style="width:70%;margin:0 auto;">
		<legend>ریز سند</legend>
	<table border="1" cellpadding="0" cellspacing="1" id="MyItemTables">
		<thead>
		<tr>
			<%--<th>ردیف</th>--%>
			<th>کد معین</th>
			<th>کد تفضیلی</th>
			<th>شرح</th>
			<th>بدهکار</th>
			<th>بستانکار</th>
			<th>شماره پیگیری</th>
			<th>تاریخ پیگیری</th>
		</tr>
		</thead>
		<tbody id="MyItemBody">
			<tr id="MyTr">
				<td>
					<select class="MoenCode" /></td>
				<td>
					<select class="TafzilCode"/></td>
				<td>
					<input type="text" class="Sharh"/></td>
				<td>
					<input type="text" class="Bedehkar" value="0"/></td>
				<td>
					<input type="text" class="Bestankar" value="0"/></td>
				<td>
					<input type="text" class="RefNo"/></td>
				<td>
					<input type="text" class="RefDate CalenderTB"/></td>
			</tr>
		</tbody>
		<tfoot>
			<tr>
				<td colspan="3">
					<div style="float:right;padding:2px;"><img src="/Images/Accounting/add.png" style="width:22px; cursor:pointer" class="AddRow" alt="اضافه (+)" title="اضافه (+)"/>
					<img src="/Images/Accounting/del.png" style="width:23px;cursor:pointer" class="DelRow" alt="حذف (-)" title="حذف (-)"/></div>
					<div id="MoeinTitleView" style="float:right;padding:2px;"></div>
				</td>
				<td align="left">
					<div id="Bedview"></div></td>
				<td align="left">
					<div id="Besview"></div></td>
				<td colspan="2" align="left">
					<div id="Tarview"></div></td>
			</tr>
		</tfoot>
		
	</table>
		<input id="SaveTempBTN" type="button" value="دخیره موقت (/)" />
		<input id="SaveBTN" type="button" value="ذخیره سند (*)" />
		<span style="display:none">
				<asp:TextBox ID="TempTB" CssClass="TempTB" runat="server" /></td>
			<asp:Button ID="ServerSaveBTN" runat="server" Text="اضافه شود" Width="70px" OnClick="SaveBTN_Click" />
			<asp:Button ID="ServerSaveTempBTN" runat="server" Text="اضافه کمکی" Width="70px" OnClick="ServerSaveTempBTN_Click" />
		</span>
		</fieldset>
</asp:Content>

