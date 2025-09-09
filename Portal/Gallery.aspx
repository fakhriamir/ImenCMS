<%@ Page Title="" EnableViewState="true" Language="C#" MasterPageFile="~/Pages.master" AutoEventWireup="true" CodeBehind="Gallery.aspx.cs" Inherits="Portal.Gallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrBeforTitle,0)%>
    <%=GetGalleryTitle(HttpContext.GetGlobalResourceObject("resource", "Def_Gallery_Title").ToString())%>
    <%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterTitle,0)%>
    <link rel="stylesheet" href="/Style/lightbox.css" type="text/css" media="screen" />
    <script src="/Scripts/jquery.lightbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".lightbox").lightbox();
        });
    </script>
    <asp:MultiView runat="server" ID="ItemMV">
        <asp:View runat="server" ID="TextItem">
            <div>
                <asp:PlaceHolder ID="RatePH" runat="server" />
            </div>
            <div style="text-align: center">
                <asp:Repeater ID="TextDG" runat="server">
                    <ItemTemplate>
                        <img style="width: 100%" border="0" title="<%#Tools.Tools.SetItemTitle(this.Page, Eval("[Desc]").ToString().Trim())%>"
                            alt="<%# Eval("[Desc]").ToString().Trim()%>" src='<%=DAL.CheckData.GetFilesRoot() %>/Images/Gallery/<%# Eval("Name").ToString().Trim()%>' />
                        <br />
                        <%# Eval("[Desc]").ToString().Trim()%>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div>
                <asp:PlaceHolder ID="CommentPL" runat="server" />
            </div>
        </asp:View>
        <asp:View runat="server" ID="TitleTop">
            <div class="photogallery">
                <asp:Repeater runat="server" ID="TitleDR">
                    <ItemTemplate>
					<span class="GalleryImageCon">
                       <a href="<%=DAL.CheckData.GetFilesRoot() %>/Images/Gallery/<%# Eval("Name").ToString().Trim()%>"
                            class="lightbox" rel="flowers">
                            <img style="width: 200px" border="0" title="<%# Eval("[Desc]").ToString().Trim()%>"
                                alt="<%# Eval("[Desc]").ToString().Trim()%>" src='<%=DAL.CheckData.GetFilesRoot() %>/Images/Gallery/TH_<%# Eval("Name").ToString().Trim()%>' /></a>
                            </span>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div style="text-align: center; width: 100%">
                <br />
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
            </div>
        </asp:View>
    </asp:MultiView>
    <%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterBody,0)%>
    <%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterFooter,0)%>
</asp:Content>
