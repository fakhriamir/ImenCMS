<%@ Page Title="" Language="C#" MasterPageFile="~/Dialog.Master" AutoEventWireup="true" CodeBehind="LetterAction.aspx.cs" Inherits="Portal.Automation.LetterAction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCP" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BoduCB" runat="server">
    <fieldset>
        <legend>اقدامات نامه</legend>
        <table dir="rtl" align="center" class="TableColor" width="80%">
            <tr>
                <td>توضیح اقدام</td>
                <td>
                    <asp:TextBox ID="textTB" runat="server" TextMode="MultiLine" MaxLength="5000" />*</td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="SaveBTN" runat="server" Text="اضافه شود" Width="70px" OnClick="SaveBTN_Click" /><input type="reset" style="WIDTH: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20"></td>
            </tr>
        </table>
    </fieldset>
	<br/>
	<%if (ViewDR.Items.Count != 0)
   {%>
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<div class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<b>شماره اقدام:</b>&nbsp; <%# DataBinder.Eval(Container.DataItem, "OfficeLetterActionID").ToString().Trim() %><br/>
					<b>توضیح:</b>&nbsp;<%# Eval("text").ToString().Trim() %><br />
                    <b>تاریخ اقدام:</b>&nbsp; <%# Tools.Calender.MyPDateTime(Eval("date").ToString()) %><br />
                    <b>اقدام کننده:</b>&nbsp; <%# Tools.Automation.GetGuestName(Eval("Guestid").ToString().Trim()) %>
                    <hr style="border:1px dotted #f0f0f0; height:1px;" />
				</div>
			</ItemTemplate>
		</asp:Repeater>

	<%}%>
</asp:Content>
