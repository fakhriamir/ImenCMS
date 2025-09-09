<%@ Page Title="" Language="C#" MasterPageFile="~/Automation/Automation.master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Portal.Automation.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AutomationBody" runat="server">
    	<fieldset style="width: 700px; margin:0 auto ;">
		<legend style="font-weight: bold;">صدور نامه</legend>
    <table id="SearchLetterBox" style="width:800px;" cellpadding="0" cellspacing="0">
        <tr>
            <td>شماره نامه</td>
            <td>
                <asp:TextBox ID="LetterNoTB" runat="server"></asp:TextBox></td>
            <td>موضوع نامه</td>
            <td><asp:TextBox ID="LetterSubjectTB" runat="server"></asp:TextBox></td>
        </tr>
                <tr>
            <td>نوع نامه</td>
            <td>
                <asp:DropDownList runat="server" ID="LetterTypeDL" DataTextField="Name" DataValueField="OfficeLetterTypeID"> </asp:DropDownList></td>
            <td>فرستنده</td>
            <td><asp:TextBox ID="SenderTB" runat="server"></asp:TextBox></td>
        </tr>
                <tr>
            <td>گیرنده</td>
            <td>
                <asp:TextBox ID="ReciverTB" runat="server"></asp:TextBox></td>
            <td>توضیح نامه</td>
            <td><asp:TextBox ID="DescTB" runat="server"></asp:TextBox></td>
        </tr>
                <tr>
            <td>از تاریخ</td>
            <td>
                <asp:TextBox ID="FromDateTB" runat="server"></asp:TextBox></td>
            <td>تا تاریخ</td>
            <td><asp:TextBox ID="ToDateTB" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align:center;">
                <asp:Button ID="SearchBTN" runat="server" Text="جستجو" OnClick="SearchBTN_Click" /></td>
        </tr>
    </table>
   </fieldset>
    <br />
     <%if (rptLetters.Items.Count != 0)
   {%>
	<asp:Repeater ID="rptLetters" runat="server">
		<HeaderTemplate>
			<table width="95%" cellspacing="0" cellpadding="0" style="font-size:9pt;">
				<tr>
					<th style="background-color:#e0e0e0; border-bottom:1px solid gray; text-align:center;">
						شماره نامه
					</th>
					<th style="background-color:#e0e0e0; border-bottom:1px solid gray; text-align:center;">
						موضوع نامه
					</th>
					<th style="background-color:#e0e0e0; border-bottom:1px solid gray; text-align:center;">
						فرستنده
					</th>
					<th style="background-color:#e0e0e0; border-bottom:1px solid gray; text-align:center;">
						تاریخ صدور
					</th>
					<th style="background-color:#e0e0e0; border-bottom:1px solid gray; text-align:center;">
						ارجحیت
					</th>
				</tr>
		</HeaderTemplate>

		<ItemTemplate>
			<tr  class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
				<td>
					<%#Eval("LetterNO")%>
				</td>
				<td>
                    <%#Eval("OfficeLetterSubjectName")%>
				</td>
				<td>
					<%#Eval("Fname") %>&nbsp; <%#Eval("Family") %>
				</td>
				<td>
					<%#Eval("RDate") %>
				</td>
                <td >
                    <%#Eval("OfficePriorityName") %> 
                </td>
			</tr>
		</ItemTemplate>
        <FooterTemplate>
        <asp:Label ID="lblEmptyData" Text="<%$ resources: resource, NoResultMsg %>" runat="server" Visible="false"></asp:Label>
        </FooterTemplate>
	</asp:Repeater>
       <%} %>
	<tr>
		<td colspan="4" align="center">
			<asp:LinkButton ID="lnkPreviousPage" runat="server" OnClick="lnkPreviousPage_Click"> <%=HttpContext.GetGlobalResourceObject("resource", "Back").ToString()%></asp:LinkButton>
			&nbsp;
			<asp:Repeater ID="rptPages" runat="server" OnItemCommand="rptPages_ItemCommand" OnItemCreated="rptPages_ItemCreated">
				<ItemTemplate>
					<asp:LinkButton ID="btnPage" CommandName="Page" CommandArgument='<%#Container.DataItem %>'
						runat="server"><%# Container.DataItem %></asp:LinkButton>
				</ItemTemplate>
				<SeparatorTemplate>
					&nbsp;-&nbsp;
				</SeparatorTemplate>
			</asp:Repeater>
			&nbsp;
			<asp:LinkButton ID="lnkNextPage" runat="server" OnClick="lnkNextPage_Click"> <%=HttpContext.GetGlobalResourceObject("resource", "Next").ToString()%></asp:LinkButton>
		</td>
	</tr>
	</table>
</asp:Content>
