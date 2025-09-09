<%@ Page Language="C#" MasterPageFile="~/Pages.master"  AutoEventWireup="true" CodeBehind="Forms.aspx.cs" Inherits="Portal.MyForms" %>
<asp:Content ID="MyContent" ContentPlaceHolderID="PageBody" runat="server">
	<script type="text/javascript" src="/Scripts/ui.core.dp.js"></script>
    <script type="text/javascript" src="/scripts/jquery.ui.datepicker-cc.js"></script>
    <script type="text/javascript" src="/scripts/calendar.js"></script>
	<script type="text/javascript" src="/scripts/jquery.ui.datepicker-cc-ar.js"></script>
	<script type="text/javascript" src="/scripts/jquery.ui.datepicker-cc-fa.js"></script>
	
    <link href="/scripts/ui.core.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/ui.datepicker.css" rel="stylesheet" type="text/css" />
    <link href="/scripts/ui.theme.css" rel="stylesheet" type="text/css" />
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrBeforTitle,0)%>
	<asp:Label ID="Label1" runat="server" Text="" /><div style="float: left"><span runat="server" id="RefererDiv"><a href="/FormsReferer-<%=string.IsNullOrEmpty(Request.QueryString["ID"])?"":Request.QueryString["ID"] %>.aspx">پیگیری فرم</a></span>
	<img style="width: 20px; height: 20px" alt="<%=HttpContext.GetGlobalResourceObject("resource", "Print").ToString()%>" title="<%=HttpContext.GetGlobalResourceObject("resource", "Print").ToString()%>" src="/Images/print_icon.gif" onclick="window.print()">
	</div> 
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterTitle,0)%>
	<table id="Forms" border="0" width="100%" style="font-size: 13px;">
        <tr>
            <td>
                <asp:Label ID="TitleLB" Visible="false" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="TextLB" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="FormPN" runat="server">
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="MessLB" runat="server" ForeColor="Red" />
                <asp:Button Visible="false" ID="SendBTN" ClientIDMode="Static" runat="server" Text="<%$ resources: resource, SentBotton %>"
                    OnClick="Send_Click" />
            </td>
        </tr>
    </table>
	<table align="center" id="Table1" border="0" width="100%" style="font-size: 13px;">
		<tr>
			<td align="center">
				<div runat="server" style="margin: 0 auto" id="PrintSallery">
				</div>
			</td>
		</tr>
	</table>
    <div runat="server" id="SearchDiv" visible="false">
       <%=HttpContext.GetGlobalResourceObject("resource", "SearchWord").ToString()%>
        <asp:TextBox MaxLength="20" ID="SearchText" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="SearchType" runat="server">
        </asp:DropDownList>
        <asp:Button ID="SearchBTN" ClientIDMode="Static" runat="server" Text="<%$ resources: resource, Def_Search_Title %>"
            OnClick="SearchBTN_Click" />
    </div>
    <div runat="server" id="BodyDiv">
    </div>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterBody,0)%>
    <%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterFooter,0)%>
    <script type="text/javascript">
    	
		 $(function () {
            $('.CalLoad').datepicker({
                changeMonth: true,
                changeYear: true
				
            });
            $('.CalLoad').attr('readonly', true);
        });
	</script>
    
</asp:Content>
