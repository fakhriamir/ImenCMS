<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ScheduleBanner.aspx.cs" Inherits="Portal.Admin.ScheduleBanner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    	<script type="text/javascript" src="/scripts/jquery.ui.datepicker-cc.js"></script>
	<script type="text/javascript" src="/scripts/calendar.js"></script>
	<script type="text/javascript" src="/scripts/jquery.ui.datepicker-cc-ar.js"></script>
	<script type="text/javascript" src="/scripts/jquery.ui.datepicker-cc-fa.js"></script>
	<script type="text/javascript">
	    $(function () {
	        $('#ctl00_Body_StartTimeTB').datepicker({
	            changeMonth: true,
	            changeYear: true
	        });
	        $('#ctl00_Body_EndTimeTB').datepicker({
	            changeMonth: true,
	            changeYear: true
	        });
	    });
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="0" cellpadding="1"
		width="50%">
		<tr>
			<td colspan="2" class="RowHead">
				<%=GetGlobalResourceObject("resource", "Gallery")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Lang")%>
			</td>
			<td>
				<asp:DropDownList ID="LanguageDL"   CssClass="NotSearch" runat="server" DataTextField="Name" DataValueField="LangID">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "SelectPic")%>:
			</td>
			<td align="right">
				<asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true"/>
			</td>
		</tr>
        <tr>
            <td>توضیحات(Alt)</td>
            <td align="right">
                <asp:TextBox ID="AltTB" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>لینک</td>
            <td align="right">
                <asp:TextBox ID="LinkTB" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>زمان شروع نمایش</td>
            <td align="right">
                <asp:TextBox ID="StartTimeTB" runat="server"></asp:TextBox></td>
        </tr>
 
        <tr>
            <td>زمان پایان نمایش</td>
            <td align="right">
                <asp:TextBox ID="EndTimeTB" runat="server"></asp:TextBox></td>
        </tr>
		<tr>
			<td align="center" colspan="2">
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNText %>" OnClick="SaveBTN_Click" />
				<input type="reset" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20">
			</td>
		</tr>
	</table>
    	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0" cellpadding="2" width="100%"
		border="1">
		<tr class="RowHead">
			<td>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Subject")%>
			</td>
            <td>
                بازه زمانی نمایش تصویر
            </td>
			<td><%=GetGlobalResourceObject("resource", "Pic")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Del")%>
			</td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("SchedulePicID").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("Title").ToString().Trim() %>
					</td>
					<td align="center">
						&nbsp;<%# Eval("link").ToString().Trim() %>
					</td>
                    <td align="center">
						شروع: &nbsp;<%# Tools.Calender.MyPDateTime(Eval("StartDate").ToString().Trim()) %><br />
                        پایان: &nbsp;<%# Tools.Calender.MyPDateTime(Eval("EndDate").ToString().Trim()) %>
					</td>
					<td align="center">
						<img style="width: 100px; height: auto" src='<%=ADAL.A_CheckData.GetFilesRoot(true) %>/images/Gallery/th_<%# Eval("Name").ToString().Trim()%>' />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("SchedulePicID").ToString().Trim() %>'
							CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
		<tr>
			<td colspan=5 align=center>
				<asp:LinkButton ID="lnkPreviousPage" runat="server" OnClick="lnkPreviousPage_Click" Text="<%$ resources: resource, Previews%>"></asp:LinkButton>
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
	<%}%>
</asp:Content>
