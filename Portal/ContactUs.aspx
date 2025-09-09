<%@ Page Title="" EnableViewState="false" Language="C#" MasterPageFile="~/Pages.master"	AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="Portal.ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrBeforTitle,0)%>
    <%=HttpContext.GetGlobalResourceObject("resource", "Def_Contact_Title").ToString()%>
    <%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterTitle,0)%>
    <table style="width: 100%" border="0">
        <asp:MultiView runat="server" ID="ItemMV">
            <asp:View runat="server" ID="TextItem">
                <tr>
                    <td>
                        <asp:Repeater ID="ContactDG" runat="server">
                            <ItemTemplate>
                                
                  
                  <asp:Literal ID="Literal3" runat="server" Text="<%$ resources: resource, Title %>" />: <%#Tools.Tools.SetItemTitle(this.Page, Eval("Title").ToString().Trim())%><br />
                  <asp:Literal ID="Literal1" runat="server" Text="<%$ resources: resource, Manager %>" />: <%# Eval("Manager").ToString().Trim()%><br />
                  <%#Tools.Tools.CheckNotEmpty(HttpContext.GetGlobalResourceObject("resource", "Email").ToString(), Tools.Tools.UtfToAscii(Eval("Email").ToString().Trim()), "", true)%>
                  <%#Tools.Tools.CheckNotEmpty(HttpContext.GetGlobalResourceObject("resource", "Tel").ToString(), Eval("TelNo").ToString().Trim(), "", true)%>
                  <%#Tools.Tools.CheckNotEmpty(HttpContext.GetGlobalResourceObject("resource", "Fax").ToString(), Eval("FaxNo").ToString().Trim(), "", true)%>
                  <hr>
           
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </asp:View>
            <asp:View runat="server" ID="TitleTop">
                <tr>
                    <td colspan="3" align="right">
                        <asp:Repeater runat="server" ID="ArticleTitleDR">
                            <ItemTemplate>
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ resources: resource, Title %>" />:
                                <%# Eval("Title").ToString().Trim()%><br />
                                <asp:Literal ID="Literal1" runat="server" Text="<%$ resources: resource, Manager %>" />:
                                <%# Eval("Manager").ToString().Trim()%><br />
                                <%#Tools.Tools.CheckNotEmpty(HttpContext.GetGlobalResourceObject("resource", "Email").ToString(), Tools.Tools.UtfToAscii(Eval("Email").ToString().Trim()), "", true)%>
                                <%#Tools.Tools.CheckNotEmpty(HttpContext.GetGlobalResourceObject("resource", "Tel").ToString(), Eval("TelNo").ToString().Trim(), "", true)%>
                                <%#Tools.Tools.CheckNotEmpty(HttpContext.GetGlobalResourceObject("resource", "Fax").ToString(), Eval("FaxNo").ToString().Trim(), "", true)%>
                                <hr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <asp:LinkButton ID="lnkPreviousPage" runat="server" OnClick="lnkPreviousPage_Click"
                            Text="<%$ resources: resource, Back %>" />
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
                        <asp:LinkButton ID="lnkNextPage" runat="server" OnClick="lnkNextPage_Click" Text="<%$ resources: resource, Next %>" />
                    </td>
                </tr>
            </asp:View>
        </asp:MultiView>
    </table>
    <%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterBody,0)%>
    <%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterFooter,0)%>
</asp:Content>
