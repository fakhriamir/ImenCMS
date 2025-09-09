<%@ Page Title="" EnableViewState="false" Language="C#" MasterPageFile="~/Pages.master" AutoEventWireup="true" CodeBehind="Article.aspx.cs" Inherits="Portal.Article" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
	<script runat="server" >
		string MyTitle = Tools.Tools.GetSetting(377, HttpContext.GetGlobalResourceObject("resource", "Def_Article_Title").ToString());
		string SetTitle(string InText)
		{
			MyTitle = InText;
			return InText;
		}
	</script>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrBeforTitle,0)%>
    <%=MyTitle%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterTitle,0)%>
	<table style="width: 100%" border="0">
		<asp:MultiView runat="server" ID="ItemMV">
			<asp:View runat="server" ID="TextItem">
				<tr>
					<td>
						<asp:PlaceHolder ID="RatePH" runat="server" />
					</td>
				</tr>
				<tr>
					<td >
						<asp:Repeater ID="ArticleDG" runat="server">
							<ItemTemplate>
                                <div id="ArtTable" itemscope itemtype="http://schema.org/Article">
                                    <div id="ArtTopTBL">
                                        <div id="Artright">
                                            <img class="Def_article_img" itemprop="thumbnailUrl" onerror="this.src='/images/bull.gif';this.style.height='8px';this.style.width='8px';"
                                                src='/files/<%=Tools.Tools.GetViewUnitID%>/ArticlePic/<%# Eval("Image").ToString().Trim() %>' />
                                        </div>
                                        <div id="ArtLeft">
                                            <div id="ArtTitle" itemprop="name"><%# SetTitle(Tools.Tools.SetItemTitle(this.Page, Eval("Title").ToString().Trim()))%></div>
                                            <div id="ArtHit">
                                                <%=HttpContext.GetGlobalResourceObject("resource", "Visitor").ToString()%>
                                                <%# Eval("Hit").ToString().Trim() %>
                                            </div>
                                            <div id="Artdate" itemprop="datePublished" ><%# Eval("WDate").ToString()%></div>
                                            <div id="ArtType" >
                                                <%=HttpContext.GetGlobalResourceObject("resource", "Category").ToString()%>:
											<span itemprop="about"><%# Eval("TypeName").ToString().Trim()%></span>
                                            </div>

                                        </div>

                                    </div>
                                    <div id="ArtText" itemprop="articleBody">
                                            <%# Eval("Text").ToString().Trim()%>
                                    </div>
                                </div>
							</ItemTemplate>
						</asp:Repeater>
					</td>
				</tr>
				<tr>
					<td>
						<asp:PlaceHolder ID="CommentPL" runat="server" />
					</td>
				</tr>
			</asp:View>
			<asp:View runat="server" ID="TitleTop">
				<tr>
					<td colspan="3" align="right">
						<asp:Repeater runat="server" ID="ArticleTitleDR">
							<ItemTemplate><div class="ArticleTitleRep" itemscope itemtype="http://schema.org/Article">
                                <div id="Articleimg">
                                    <img  itemprop="thumbnailUrl" class="Def_article_img" onerror="this.src='/images/bull.gif';this.style.height='8px';this.style.width='8px';"
                                        src='/files/<%=Tools.Tools.GetViewUnitID%>/ArticlePic/<%# Eval("Image").ToString().Trim() %>' />
                                </div>
                                <div id="ArticleListBox">
                                    <div id="ArticleTitle" itemprop="name">
                                        <a href='/Article/<%# Eval("ArticleID").ToString().Trim()%>/<%# Tools.Tools.UrlWordReplace(Eval("Title").ToString().Trim())%>.aspx'>
                                            <%# Eval("Title").ToString().Trim()%></a>
                                    </div>
                                    <div id="ArticleHit">
                                        <%=HttpContext.GetGlobalResourceObject("resource", "HitNo").ToString()%>:
                                    <%# Eval("Hit").ToString().Trim()%>
                                    </div>

                                    <div id="ArticleChekide" itemprop="description"><%# Eval("Chekide").ToString().Trim()%></div>
                                </div>
							</div></ItemTemplate>
							<SeparatorTemplate>
							
							</SeparatorTemplate>
						</asp:Repeater>
					</td>
				</tr>
				<tr>
					<td colspan="3" align="center">
					
						  <a runat="server" ID="BackHref" > <%=HttpContext.GetGlobalResourceObject("resource", "Back").ToString()%></a>
						&nbsp;
						<asp:Repeater ID="rptPages" runat="server" >
							<ItemTemplate>
								<a <%# Container.DataItem.ToString().Trim() == (CurrentPage + 1).ToString()? "class=\"CurentPage\"" : "class=\"PagingLNK\"" %> href="/Article/<%=GetTypeLink()%>p<%# Container.DataItem %>"><%# Container.DataItem %>
								</a>
							</ItemTemplate>
							<SeparatorTemplate>
								&nbsp;-&nbsp;
							</SeparatorTemplate>
						</asp:Repeater>
						&nbsp;
						<a runat="server" ID="NextHref" > <%=HttpContext.GetGlobalResourceObject("resource", "Next").ToString()%></a>
					</td>
				</tr>
			</asp:View>
		</asp:MultiView>
	</table>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterBody,0)%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterFooter,0)%>
</asp:Content>
