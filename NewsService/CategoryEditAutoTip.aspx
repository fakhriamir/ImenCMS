<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CategoryEditAutoTip.aspx.cs" Inherits="NewsService.CategoryEditAutoTip" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script language="javascript" type="text/javascript">
		$(document).ready(function () {
			//alert($('.CatDL'));
			var CatOpts = {<%=CatOptions%> };
			$.each(CatOpts, function (val, text) {
				$("select").not(".NotSel").append("<option value=" + val + ">" + text + "</Option>");
			});
			$("select").not(".NotSel").each(function () {
				var CurCatID = $(this).attr("catid");
				$(this).val(CurCatID);
			});
			$("select").not(".NotSel").change(function () {
				var CurID = $(this).attr("id");
				
				$.ajax({
					type: "POST",
					mimeType: "text/html",
					url: "/ajax.aspx?MyType=Sub" + CurID + "-" + $(this).val(),
					//data: data,

					success: function (html) {
						//alert(html);
						if(html=="error")
						{
							jAlert('برقراری ارتباط با مشکل روبرو شده است', 'پیام ');
							return;
						}
						else if(html.indexOf("OK")!=-1)
						{
							//alert(html.substring(2));
							$("#C" + html.substring(2)).css('display', 'none');
						}
					},
					error: function () {
						jAlert('برقراری ارتباط با مشکل روبرو شده است', 'پیام سیستم');
						return;
					}
				});
				return false; // cancel original event to prevent form submitting
			});
		});
		function MyEvent(id,ev)
		{
			//alert("1");
			$.ajax({
				type: "POST",
				mimeType: "text/html",
				url: "/ajax.aspx?MyType=ECO" + id + "-" +ev,
				//data: data,

				success: function (html) {
					//alert(html);
					if (html == "error") {
						jAlert('برقراری ارتباط با مشکل روبرو شده است', 'پیام ');
						return;
					}
					else if (html.indexOf("OK") != -1) {
						//alert(html.substring(2));
						$("#C" + html.substring(2)).css('display', 'none');
					}
				},
				error: function () {
					jAlert('برقراری ارتباط با مشکل روبرو شده است', 'پیام سیستم');
					return;
				}
			});
			return false;

		}
		function Show()
		{
			document.getElementById("IconDiv").style.display="";
		}
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"><div id="IconDiv" style="display:none">
	<asp:Button ID="SignAnother" runat="server" Text="ساخت تعدادی هوشمند" OnClick="SignAnother_Click" />
	<asp:Button ID="UpdateAuto" runat="server" Text="ساختن جدول کلمات" OnClick="UpdateAuto_Click" />
	<asp:Button ID="SignAllBTN" runat="server" Text="ساخت 100000 هوشمند" OnClick="SignAllBTN_Click" />
	<asp:Button ID="ResetAllSignBTN" runat="server" Text="حذف همه موضوع هوشمندها" OnClick="ResetAllSignBTN_Click"  /></div>
	<fieldset>
		<legend>شاخه ها و اخبار</legend>
		<asp:Repeater runat="server" ID="NewsCatDR">
			<ItemTemplate>
				<%# Eval("Name").ToString().Trim()%>(<%# Eval("CNT").ToString().Trim()%>) 
			</ItemTemplate>
			<SeparatorTemplate>&nbsp;&nbsp;</SeparatorTemplate>
		</asp:Repeater>
	</fieldset>
	
	<fieldset>
		<legend>فیلتر موضوعی</legend>
	<asp:Button ID="CatFilterBTN" runat="server" Text="فیلتر" OnClick="CatDL_Click" />
		<asp:DropDownList CssClass="NotSel" DataValueField="CategoryID" DataTextField="Name" ID="CatDL" runat="server"></asp:DropDownList>	
	</fieldset>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="1" cellpadding="2" width="100%"
		border="1">
		
		<tr class="RowHead">
			<td onclick="javascript:Show()">
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Edit")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Del")%>
			</td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>" id="C<%# Eval("NewsID").ToString().Trim()%>">
					<td align="center">&nbsp;<%# Eval("CategoryID").ToString().Trim()%></td>
					<td align="center">&nbsp;<%# Eval("Title").ToString().Trim()%></td>
					<td align="right">&nbsp;<%# Eval("Lead").ToString().Trim()%></td>
					<td align="center">&nbsp;<a href="<%# Eval("Link").ToString().Trim()%>" target="_blank">لینک خبر</a>
							<input id="D<%# Eval("NewsID").ToString().Trim()%>"  type="button" value="تصاویر" onclick="OpenDialog('<%# GetImageTag(Eval("Matn").ToString().Trim())%>	')"/>
					<input id="M<%# Eval("NewsID").ToString().Trim()%>" type="button" value="متن" onclick="OpenDialog($('#MT<%# Eval("NewsID").ToString().Trim()%>	').html())"/>
						<div id="MT<%# Eval("NewsID").ToString().Trim()%>" " style="display:none"><%#Eval("Matn").ToString().Trim()%></div>
				
					</td>
					<td align="center">&nbsp;<div style="display:none"><%#Tools.Tools.SetExplain( Eval("Matn").ToString().Trim(),150)%></div></td>
					<td align="center">&nbsp;<%# Eval("Ref").ToString().Trim()%></td>
					<td align="center">&nbsp;
						<select id="N<%# Eval("NewsID").ToString().Trim()%>" class="NotSearch" catid="<%# Eval("AutoCategoryID").ToString().Trim()%>"></select>
						<input type="button" value="تایید" onclick="MyEvent('<%# Eval("NewsID").ToString().Trim()%>','OK')"/>
						<input type="button" value="حذف" onclick="MyEvent('<%# Eval("NewsID").ToString().Trim()%>','DEL')"/>

					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>

	</table>
	<table border="0" align="center">
		<tr>
			<td>
				<asp:LinkButton ID="lnkPreviousPage" runat="server" OnClick="lnkPreviousPage_Click"
					Text="<%$ resources: resource, Previews%>"></asp:LinkButton>
				&nbsp;
				<asp:Repeater ID="rptPages" runat="server" OnItemCommand="rptPages_ItemCommand" OnItemCreated="rptPages_ItemCreated">
					<ItemTemplate>
						<asp:LinkButton ID="btnPage" CommandName="Page" CommandArgument='<%#Container.DataItem %>'
							runat="server"><%# Container.DataItem %>
						</asp:LinkButton>
					</ItemTemplate>
					<SeparatorTemplate>
						&nbsp;-&nbsp;
					</SeparatorTemplate>
				</asp:Repeater>
				&nbsp;
				<asp:LinkButton ID="lnkNextPage" runat="server" OnClick="lnkNextPage_Click" Text="<%$ resources: resource, Next%>"></asp:LinkButton>
			</td>
		</tr>
	</table>
</asp:Content>
