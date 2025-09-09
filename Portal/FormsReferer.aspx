<%@ Page Title="" Language="C#" MasterPageFile="~/Pages.master" AutoEventWireup="true" CodeBehind="FormsReferer.aspx.cs" Inherits="Portal.FormsReferer" %>
<asp:Content ID="MyContent" ContentPlaceHolderID="PageBody" runat="server">
	  <fieldset>
        <legend>جستجوی تیکت</legend>
                شماره پیگیری
                    <asp:TextBox ID="TNoTB" TextMode="Number" runat="server" />&nbsp;&nbsp;&nbsp;
                رمز پیگیری
                    <asp:TextBox ID="TPassTB" TextMode="Number" runat="server" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="SearchTicketBTN" runat="server" Text="جستجوی تیکت" OnClick="SearchTicketBTN_Click" />

    </fieldset>
	
    <h4>لیست تیکت های ارسالی</h4>
   <div id="ticketResult">
        <asp:Repeater ID="ViewDR" runat="server">
            <ItemTemplate>
               تاریخ :   <%# Tools.Calender.MyPDateTime(Eval("Date").ToString()) %> <br />
                تاریخ پاسخگویی:   <%#  Tools.Calender.MyPDateTime(Eval("AndwerDate").ToString()) %><br />
                وضعیت:   <%# Eval("State") =="0" ? "پاسخ داده شده" : "در حال بررسی" %> <br />
                توضیحات:  <%# Eval("[Desc]") %>
                      <%--  FormRefererID, UserID, , AndwerDate, UserAnswerID, UnitID, [Desc], FormNameID, State 
                            <%# Eval("") %>
                --%>
                
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
