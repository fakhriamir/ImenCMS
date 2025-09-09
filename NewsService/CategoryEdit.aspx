<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CategoryEdit.aspx.cs" Inherits="NewsService.CategoryEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script language="javascript" type="text/javascript">
		$(document).ready(function () {
			//alert($('.CatDL'));
			var CatOpts = {<%=CatOptions%> };
			$.each(CatOpts, function (val, text) {
				$("select").append("<option value=" + val + ">" + text + "</Option>");
			});
			$("select").each(function () {
				var CurCatID = $(this).attr("catid");
				$(this).val(CurCatID);
			});
			$("select").change(function () {
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
							$("#N" + html.substring(2)).css('background-color', 'green');
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
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<fieldset>
		<legend>جستجو</legend>
		<asp:TextBox ID="SearchTB" runat="server"></asp:TextBox><asp:Button ID="SearchBTN" runat="server" Text="جستجو" OnClick="SearchBTN_Click" />
	</fieldset>
	<fieldset>
		<legend>شاخه ها و اخبار</legend>
		<asp:Repeater runat="server" ID="NewsCatDR">
			<ItemTemplate>
					<%# Eval("Name").ToString().Trim()%>(<%# Eval("CNT").ToString().Trim()%>) 
					
			
			</ItemTemplate>
			<SeparatorTemplate>&nbsp;&nbsp;</SeparatorTemplate>
		</asp:Repeater>
	</fieldset>

	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="1" cellpadding="2" width="100%"
		border="1">
		
		<tr class="RowHead">
			<td>
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
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">&nbsp;<%# Eval("CategoryID").ToString().Trim()%></td>
					<td align="center">&nbsp;<%# Eval("Title").ToString().Trim()%></td>
					<td align="right" >&nbsp;<%# Eval("Lead").ToString().Trim()%></td>

					<td align="center">&nbsp;<a href="<%# Eval("Link").ToString().Trim()%>" target="_blank">لینک خبر</a>
						<input id="D<%# Eval("NewsID").ToString().Trim()%>"  type="button" value="تصاویر" onclick="OpenDialog('<%# GetImageTag(Eval("Matn").ToString().Trim())%>')"/>
					<input id="M<%# Eval("NewsID").ToString().Trim()%>" type="button" value="متن" onclick="OpenDialog($('#MT<%# Eval("NewsID").ToString().Trim()%>	').html())"/>
						<div id="MT<%# Eval("NewsID").ToString().Trim()%>" " style="display:none"><%#Eval("Matn").ToString().Trim()%></div>
				
							</td>
					<td align="center">&nbsp;</td>
					<td align="center">&nbsp;<%# Eval("Ref").ToString().Trim()%></td>
					<td align="center">&nbsp;
						<select id="N<%# Eval("NewsID").ToString().Trim()%>" class="NotSearch" catid="<%# Eval("CategoryID").ToString().Trim()%>"></select>
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
