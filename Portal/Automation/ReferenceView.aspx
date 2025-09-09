<%@ Page Title="" Language="C#" MasterPageFile="~/Dialog.Master" AutoEventWireup="true" CodeBehind="ReferenceView.aspx.cs" Inherits="Portal.Automation.ReferenceView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCP" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BoduCB" runat="server">
	<%if (ViewDR.Items.Count != 0)
   {%>
    <fieldset>
        <legend>ارجاعات</legend>
		<asp:Repeater runat="server" ID="ViewDR">
			<ItemTemplate>
				<div class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<b>شماره ارجاع: </b>&nbsp;<%# DataBinder.Eval(Container.DataItem, "officereferenceid").ToString().Trim() %><br />
					<b>ارجاع دهنده: </b>&nbsp;<%# Tools.Automation.GetGuestName( Eval("senderpersonalid").ToString().Trim()) %><br />
                    <p>
                        <b>ارجاع گیرنده: </b>&nbsp;<%# Tools.Automation.GetGuestName(Eval("topersonalid").ToString().Trim()) %> &nbsp;&nbsp;&nbsp;
                        <b>تاریخ مشاهده : </b>&nbsp;<%#Tools.Calender.MyPDateTime(Eval("viewdate").ToString().Trim())%> &nbsp;&nbsp;&nbsp;
                        <b>تاریخ خاتمه : </b>&nbsp;<%#Tools.Calender.MyPDateTime(Eval("enddate").ToString().Trim())%>
                    </p>
					<b>تاریخ ارجاع : </b>&nbsp;<%#Tools.Calender.MyPDateTime(Eval("date").ToString().Trim())%><br />
                    <b>پی نوشت : </b>&nbsp;<%# Eval("paraph").ToString().Trim() %>
					<%# GetPerParaph(Eval("PerParaph").ToString().Trim(),Eval("topersonalid").ToString().Trim(),Eval("senderpersonalid").ToString().Trim()) %>
                    
                    <hr style="border:1px dotted #f0f0f0; height:1px;" />
				</div>
			</ItemTemplate>
		</asp:Repeater>
    </fieldset>
	<%}%>
</asp:Content>
