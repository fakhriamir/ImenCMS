<%@ Page Title="" Language="C#" MasterPageFile="~/Dialog.Master" AutoEventWireup="true" CodeBehind="ViewLetter.aspx.cs" Inherits="Portal.Automation.ViewLetter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCP" runat="server">
	<style>
		.RowHead {
			background-color: #e8e8e8;
			border-bottom: 2px solid #808080;
			text-align: center;
			font-weight: bold;
			height: 22px;
		}

		.RowItem, .RowAlter {
			text-align: center;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BoduCB" runat="server">
	<table width="90%" align="center">
		<asp:Repeater runat="server" ID="ViewDR">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						<b>شناسه نامه: </b><%#Tools.Automation.GetLetterNo( Eval("OfficeLetterID").ToString().Trim())%></td>
					<td align="center">
						<b>توضیح:</b><%# Eval("comm").ToString().Trim()%></td>
					<td><b>تاریخ صدور: </b><%#Tools.Calender.MyPDate(Eval("Date").ToString() )%></td>
					<td>
						<img style="width: 20px; height: 20px; cursor: pointer;" src="Images/Attach.png" onclick="SelectPrepMsg1('/Automation/LettersFile-<%# Eval("OfficeLetterID") %>.aspx',520,490);"
							alt="پیوست نامه" />
					</td>
					<td align="center">
						<img style="width: 20px; height: 20px; cursor: pointer;" src="Images/Reference.png" onclick="SelectPrepMsg1('/Automation/Reference-<%# Eval("OfficeLetterID") %>.aspx',520,490);"
							alt="ارجاع" /></td>
					<td>
						<img style="width: 20px; height: 20px; cursor: pointer;" src="Images/References.png" onclick="SelectPrepMsg1('/Automation/referenceView-<%# Eval("OfficeLetterID") %>.aspx',520,490);"
							alt="ارجاعات" />
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="Images/sign.png" Width="18px" Height="18px" CommandArgument='<%# Eval("OfficeLetterID").ToString().Trim() %>'
							CommandName="Sign" ID="ImageButton1" ToolTip="امضای نامه" AlternateText="امضای نامه"
							runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="Images/Edit.png" Width="18px" Height="18px" CommandArgument='<%# Eval("OfficeLetterID").ToString().Trim() %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="ویرایش  نامه" AlternateText="ویرایش"
							runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Images/delete.png" Width="20px" Height="20px"
							CommandArgument='<%# Eval("OfficeLetterID").ToString().Trim() %>' CommandName="DEL" ID="DelBTN" ToolTip="حذف"
							AlternateText="حذف" runat="server" />
					</td>
				</tr>
				
				<tr style="display: <%# GetBodyDisplay(Eval("body")) %>">
					<td colspan="9">
						<div style="width: 100%; padding: 10px; margin: 10px;">
							<%# Eval("body").ToString().Trim().Replace("##LetterID##",Tools.Automation.GetLetterNo( Eval("OfficeLetterID").ToString().Trim())) %>
						</div>
					</td>
				</tr>
				<tr  style="display: <%# GetBodyImageDisplay(Eval("bodyIMG")) %>">
					<td colspan="9">
						<img src="/Automation/OfficeImage-<%# Eval("OfficeLetterID").ToString().Trim() %>.aspx" /></td>
				</tr>

			</ItemTemplate>
		</asp:Repeater>
	</table>

	<fieldset>
		<legend>ارجاعات</legend>
		<%if (ViewDR.Items.Count != 0)
	{%>
		<table dir="rtl" id="My" align="center" cellspacing="1" cellpadding="2" width="100%" border="0">

			<asp:Repeater runat="server" ID="ReferenceDR">
				<ItemTemplate>
					<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
						<td align="center">شماره ارجاع:&nbsp;<%# DataBinder.Eval(Container.DataItem, "officereferenceid").ToString().Trim() %> </td>
						<td align="center">تاریخ:&nbsp;<%# Tools.Calender.MyPDateTime(Eval("date").ToString().Trim()) %></td>
						<td align="center">اولویت:&nbsp;<%# Eval("OfficePriorityName").ToString().Trim() %></td>
						<td align="center">نوع ارجاع:&nbsp;<%# Eval("OfficeReferenceTypeName").ToString().Trim() %></td>
						<td align="center">ارجاع دهنده:&nbsp;<%# Eval("SenderName").ToString().Trim() %></td>
						<td align="center">گیرنده:&nbsp;<%# Eval("ToName").ToString().Trim() %></td>
					</tr>
					<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
						<td align="right" colspan="6">پاراف:&nbsp;<%# Eval("Paraph").ToString().Trim() %>
							<%# GetPerParaph(Eval("PerParaph").ToString().Trim(),Eval("ToPersonalID").ToString().Trim()) %>
						</td>
					</tr>
				</ItemTemplate>
			</asp:Repeater>
		</table>
		<%}%>
	</fieldset>
</asp:Content>
