<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ProductShop.aspx.cs" Inherits="Portal.Admin.ProductShop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <table dir="rtl" align="center" class="TableColor" width="80%">
        <tr class="RowHead">
            <td colspan="2" align="center">مدیریت فروشگاه ها</td>
        </tr>
        <tr>
            <td>عنوان فروشگاه</td>
            <td>
                <asp:TextBox ID="nameTB" runat="server" MaxLength="64" />*</td>
        </tr>
        <tr>
            <td>آدرس</td>
            <td>
                <asp:TextBox ID="addressTB" runat="server" MaxLength="512" /></td>
        </tr>
        <tr>
            <td>تلفن</td>
            <td>
                <asp:TextBox ID="telTB" runat="server" MaxLength="32" /></td>
        </tr>
        <tr>
            <td>موبایل</td>
            <td>
                <asp:TextBox ID="mobTB" runat="server" MaxLength="12" /></td>
        </tr>
        <tr>
            <td>فاکس</td>
            <td>
                <asp:TextBox ID="faxTB" runat="server" MaxLength="32" /></td>
        </tr>
        <tr>
            <td>نام مدیر</td>
            <td>
                <asp:TextBox ID="mannameTB" runat="server" MaxLength="64" /></td>
        </tr>
       
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="SaveBTN" runat="server" Text="اضافه شود" Width="70px" OnClick="SaveBTN_Click" /><input type="reset" style="width: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20"></td>
        </tr>
    </table>
    <br>
    <%if (ViewDR.Items.Count != 0)
        {%>
    <table dir="rtl" id="My" align="center" cellspacing="1" cellpadding="2" width="100%" border="1">
        <tr class="RowHead">
            <td><%=GetGlobalResourceObject("resource", "ID")%></td>
            <td><%=GetGlobalResourceObject("resource", "Title")%></td>
            <td><%=GetGlobalResourceObject("resource", "Edit")%></td>
            <td><%=GetGlobalResourceObject("resource", "Del")%></td>
        </tr>
        <asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
            <ItemTemplate>
                <tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
                    <td align="center">&nbsp;<%# DataBinder.Eval(Container.DataItem, "ProductShopID").ToString().Trim() %></td>
                    <td align="center">&nbsp;<%# Eval("Name").ToString().Trim() %></td>
                    <td align="center">
                        <asp:ImageButton ImageUrl="/Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("ProductShopID").ToString().Trim() %>' CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" /></td>
                    <td align="center">
                        <asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="/Imgs/zarb.gif" Width="16px" Height="16px" CommandArgument='<%# Eval("ProductShopID").ToString().Trim() %>' CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" /></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <%}%>
</asp:Content>
