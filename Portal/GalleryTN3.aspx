<%@ Page Title="" Language="C#" MasterPageFile="~/Pages.master" AutoEventWireup="true" CodeBehind="GalleryTN3.aspx.cs" Inherits="Portal.GalleryTN3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
    <link rel="stylesheet" href="/Style/tn3.css" type="text/css" media="screen" />
 
    <script type="text/javascript" src="/Scripts/jquery.tn3lite.min.js"></script>
    <script runat="server">
        string SiteTitle = HttpContext.GetGlobalResourceObject("resource", "Def_Gallery_Title").ToString();
        string SetName(string aa)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim() != "")
            {
                if (Request.QueryString["ID"].ToLower().IndexOf("type") != -1)
                    SiteTitle = aa;
            }
            return "";
        }
    </script>
    <!--  initialize the TN3 when the DOM is ready -->
    <script type="text/javascript">
        $(document).ready(function () {
            //Thumbnailer.config.shaderOpacity = 1;
            var tn1 = $('.mygallery').tn3({
                skinDir: "skins",
                imageClick: "fullscreen",
                image: {
                    maxZoom: 1.5,
                    crop: true,
                    clickEvent: "dblclick",
                    transitions: [{
                        type: "blinds"
                    }, {
                        type: "grid"
                    }, {
                        type: "grid",
                        duration: 460,
                        easing: "easeInQuad",
                        gridX: 1,
                        gridY: 8,
                        // flat, diagonal, circle, random
                        sort: "random",
                        sortReverse: false,
                        diagonalStart: "bl",
                        // fade, scale
                        method: "scale",
                        partDuration: 360,
                        partEasing: "easeOutSine",
                        partDirection: "left"
                    }]
                }
            });
        });
    </script>
    <%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrBeforTitle,0)%>
    <%=SiteTitle%>
    <%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterTitle,0)%>

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
            <div id="contentF001">
                <div class="mygallery">
                    <div class="tn3 album">
                        <ol>
                            <asp:Repeater ID="TitleDR" runat="server">
                                <HeaderTemplate></HeaderTemplate>
                                <ItemTemplate>
                                    <li><%# SetName(Eval("Gallerywaretype").ToString().Trim())%>
                                        <h4><%# Eval("[Desc]")%></h4>
                                        <a href="<%=DAL.CheckData.GetFilesRoot() %>/Images/Gallery/<%# Eval("Name").ToString().Trim()%>">
                                            <img src='<%=DAL.CheckData.GetFilesRoot() %>/Images/Gallery/TH_<%# Eval("Name").ToString().Trim()%>' />
                                        </a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ol>
                    </div>
                </div>
            </div>
            <div style="text-align: center; width: 100%">
            </div>
        </asp:View>
    </asp:MultiView>
    <%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterBody,0)%>
    <%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterFooter,0)%>
</asp:Content>
