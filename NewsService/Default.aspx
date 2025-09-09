<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin.Master" CodeBehind="Default.aspx.cs" Inherits="NewsService.Default" %>

<asp:Content ContentPlaceHolderID="head" runat="server" ID="ContentA1">
	<style type="text/css">
		#main .place {
			float: <%=GetGlobalResourceObject("resource", "_Align")%>;
			height: auto;
			list-style: none;
			margin: 3px;
			min-height: 100px;
			padding: 0px;
			text-align: center;
			vertical-align: top;
		}
		/*#main .placeHolder {border:#d9d9d9 solid 1px; margin:3px 0px; background:url(/_img/palceholder_bg.png) left top; -moz-border-radius:5px;}*/
		#main .p0x { /* customize width by pixels */
		}

		#main .p1x {
			width: 294px;
		}

		#main .p2x {
			width: 594px;
		}

		#main .p3x {
			width: 894px;
		}
	</style>
</asp:Content>
<asp:Content ContentPlaceHolderID="Body" runat="server" ID="aa">

<%--    <input style="display: none" id="Button1" onclick="SelectPrepMsg('changepass.aspx', 200, 100)" type="button" value="button" />--%>
    <%--<img src="/Imgs/besm.gif" align="center" style="width: 600px; height: auto" />--%>
    <div style="width: 100%; margin: 0 auto; float: <%=GetGlobalResourceObject("resource", "_Align")%>; margin-top: 20px;">
        <div id="DateNow">
            <asp:PlaceHolder ID="DateViewPH" runat="server"></asp:PlaceHolder>
        </div>
        <div id="IconToolbar" style="float: <%=GetGlobalResourceObject("resource", "_Align")%>; margin-<%=GetGlobalResourceObject("resource", "_Align")%>: 0px;">
            <table>
                <tr>
                    <td>
                        <a href="/CustomCadr.aspx">
                            <img src="/Imgs/ICON/CustomKadr.png" /><br />
                            <%=GetGlobalResourceObject("resource", "CustomizeBox")%></a>
                    </td>
                    <td>
                        <a href="/Texts.aspx">
                            <img src="/Imgs/ICON/PageEdit.png" /><br />
                            <%=GetGlobalResourceObject("resource", "CreatePageMng")%></a></td>
                    <td>
                        <a href="/TemplateEditStyle.aspx">
                            <img src="/Imgs/ICON/css.png" /><br />
                            <%=GetGlobalResourceObject("resource", "CSSMan")%></a></td>
                    <td>
                        <a href="/Gallery.aspx">
                            <img src="/Imgs/ICON/gallery.png" /><br />
                            <%=GetGlobalResourceObject("resource", "GalleryMan")%></a></td>
                    <td>
                        <a href="/Articles.aspx">
                            <img src="/Imgs/ICON/article.png" /><br />
                            <%=GetGlobalResourceObject("resource", "Articles")%></a></td>
                    <td>
                        <a href="/FileMan.aspx">
                            <img src="/Imgs/ICON/filedirectory.png" /><br />
                            <%=GetGlobalResourceObject("resource", "FilesMng")%></a></td>
                    <td>
                        <a href="/News.aspx">
                            <img src="/Imgs/ICON/news.png" /><br />
                            <%=GetGlobalResourceObject("resource", "News")%></a></td>
                    <%--<td>
							<a href="/unitChart.aspx">
								<img src="/Imgs/ICON/chart.png" /><br />
								چارت سازمانی</a></td>--%>
                    <td>
                        <a href="/SettingUnits.aspx?ID=1">
                            <img src="/Imgs/ICON/setting.png" /><br />
                            <%=GetGlobalResourceObject("resource", "SettingSystem")%></a></td>

                </tr>
            </table>
        </div>
        <div style="display: none;">
            <asp:PlaceHolder ID="ElderPH" runat="server"></asp:PlaceHolder>
        </div>
        <div style="width: 1050px;">
            <div style="width: 700px; height: 330px; float: <%=GetGlobalResourceObject("resource", "_Align")%>; border: 1px solid #d0d0d0; box-shadow: 2px 2px 2px #d9d9d9; border-radius: 3px; margin-top: 30px; background: url(/Imgs/gr.png) repeat-x #fff; margin-<%=GetGlobalResourceObject("resource", "_Align")%>: 20px;">
                <asp:PlaceHolder ID="HitsChartPH" runat="server"></asp:PlaceHolder>
            </div>
            <div style="width: 270px; float: <%=GetGlobalResourceObject("resource", "_Align")%>; margin-<%=GetGlobalResourceObject("resource", "_Align")%>: 10px;">
                <div id="Elder">
                    <%if (ADAL.A_CheckData.GetAccessTypeID() == "1")
                      { %>
                    <div style="margin-top: 5px">
                        <asp:DropDownList ID="UnitDL" CssClass="NotSearch" AutoPostBack="true" runat="server" DataTextField="name"
                            DataValueField="unitid" OnSelectedIndexChanged="UnitDL_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <%} %>
                     <div style="margin-top: 5px">
                        <asp:DropDownList ID="LanguageDL" CssClass="NotSearch" AutoPostBack="true" runat="server" OnSelectedIndexChanged="LanguageDL_SelectedIndexChanged" >
                            <asp:ListItem Text="فارسی" Value=""></asp:ListItem>
                            <asp:ListItem Text="English" Value="en-us"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div style="clear: both;"></div>
               
                <div id="PageVisit">
                    <asp:PlaceHolder ID="PopularPagePH" runat="server"></asp:PlaceHolder>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
