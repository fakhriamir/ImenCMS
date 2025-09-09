<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SettingUnits.aspx.cs" Inherits="NewsService.SettingUnits" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div id="SettingDiv">
        <ul>
            <asp:Repeater runat="server" ID="ItemDR">
                <ItemTemplate>
                    <li><a href="/SettingUnits.aspx?ID=<%# Eval("SettingUnitNameTypeID")%>"><%# Eval("Name").ToString().Trim() %></a></li>
                </ItemTemplate>
            </asp:Repeater>
                   <li><a href="/SettingText.aspx"><%=GetGlobalResourceObject("resource", "TextSetting")%></a></li>
            
        </ul>
       <%-- <div style="background-color: #fdc8ff; border-radius: 2px; width: 120px; float: left; color: #fff; height: 30px;">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/SettingManage.aspx">مدیریت تنظیمات</asp:HyperLink>
        </div>--%>
    </div>
    <div style="clear:both;margin-bottom:30px;"></div>
	<table align="center" cellspacing="0" class="TableColor">
		<tr>
			<th colspan="2">
				<%=GetGlobalResourceObject("resource", "SysSetting")%>
			</th>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Lang")%>
			</td>
			<td>
				<asp:DropDownList ID="LanguageDL"   CssClass="NotSearch" AutoPostBack="true" runat="server" 
					DataTextField="Name" DataValueField="LangID" 
					onselectedindexchanged="LanguageDL_SelectedIndexChanged">
				</asp:DropDownList>
			</td>
		</tr>
		<asp:Panel ID="FormPN" runat="server">
		</asp:Panel>
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNSave%>" OnClick="SaveBTN_Click" />
			</td>
		</tr>
	</table>
</asp:Content>
