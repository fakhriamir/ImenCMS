<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="PageAddress.aspx.cs" Inherits="Portal.Admin.PageAddress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0" cellpadding="2" width="70%" border="0">
        <tr class="RowHead">
            <td>
                <%=GetGlobalResourceObject("resource", "ID")%>
            </td>
            <td>
                <%=GetGlobalResourceObject("resource", "Title")%>
            </td>
            <td>
                <%=GetGlobalResourceObject("resource", "Link")%>
            </td>
        </tr>
        <asp:Repeater runat="server" ID="ViewDR">
            <ItemTemplate>
                <tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
                    <td align="center">&nbsp;<%# Eval("PageAddressID").ToString().Trim() %></td>
                    <td>&nbsp;<%# Eval("Name").ToString().Trim() %></td>
                    <td align="center">&nbsp;<%# Eval("Link").ToString().Trim() %></td>

                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>
