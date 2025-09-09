<%@ Page Title="" Language="C#" MasterPageFile="~/Pages.master" AutoEventWireup="true" CodeBehind="Library.aspx.cs" Inherits="Portal.Library" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
<script runat="server" >
		string MyTitle = Tools.Tools.GetSetting(483, HttpContext.GetGlobalResourceObject("resource", "Def_Library_Title").ToString());
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
						<asp:Repeater ID="LibraryDG" runat="server">
							<ItemTemplate>
                                <div id="LibTable">
                                    <div id="LibTopTBL">
                                        <div id="Libright">
                                            <img class="Def_Library_img" onerror="this.style.display='none'"
                                                src='/files/<%=Tools.Tools.GetViewUnitID%>/LibraryPic/<%# Eval("ImgAddress").ToString().Trim() %>' />
                                        </div>
                                        <div id="LibLeft">
                                            <div id="LibTitle" ><%# SetTitle(Tools.Tools.SetItemTitle(this.Page, Eval("Title").ToString().Trim()))%></div>
											<div id="LibChekide" ><%#  Eval("Chekide").ToString().Trim()%></div>
                                            <div id="LibHit">
                                                <%=HttpContext.GetGlobalResourceObject("resource", "Visitor").ToString()%>
                                                <%# Eval("Hit").ToString().Trim() %>
                                            </div>
                                            <div id="Libdate"><%#Tools.Tools.CheckNotEmpty("تاریخ انتشار",Tools.Calender.ReverseDate( Eval("PublishDate").ToString())) %></div>
                                            <div id="LibType" >
                                                <%# Tools.Tools.CheckNotEmpty("نویسنده",Eval("Writer").ToString().Trim(),"",true) %>
												<%# Tools.Tools.CheckNotEmpty("نویسنده",Eval("Writer1").ToString().Trim(),"",true) %>
												<%# Tools.Tools.CheckNotEmpty("نویسنده",Eval("Writer2").ToString().Trim(),"",true) %>
												<%# Tools.Tools.CheckNotEmpty("نویسنده",Eval("Writer3").ToString().Trim(),"",true) %>
												<%# Tools.Tools.CheckNotEmpty("نویسنده",Eval("Writer4").ToString().Trim(),"",true) %>
												<%# Tools.Tools.CheckNotEmpty("انتشارات",Eval("Publisher").ToString().Trim(),"",true) %>
                                            </div>
											<div>شاخه بندی:<br />
												<%#GetCategory( Eval("LibraryID").ToString().Trim())%>
											</div>

                                        </div>

                                    </div>
									<div id="LibText">										
										<%#GetLibraryText( Eval("FileAddress").ToString().Trim(), Eval("Matn").ToString().Trim())%>
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
						<asp:Repeater runat="server" ID="LibraryTitleDR">
							<ItemTemplate><div class="LibraryTitleRep" >
                                <div id="Libraryimg">
                                    <img   class="Library_img" onerror="this.style.display='none';"
                                        src='/files/<%=Tools.Tools.GetViewUnitID%>/LibraryPic/<%# Eval("ImgAddress").ToString().Trim() %>' />
                                </div>
                                <div id="LibraryListBox">
                                    <div id="LibraryTitle" itemprop="name">
                                        <a href='/Library/<%# Eval("LibraryID").ToString().Trim()%>/<%# Tools.Tools.UrlWordReplace(Eval("Title").ToString().Trim())%>.aspx'>
                                            <%# Eval("Title").ToString().Trim()%></a>
                                    </div>
                                    <div id="LibChekide"><%#  Eval("Chekide").ToString().Trim()%></div>
                                    <div id="LibHit">
                                        <%=HttpContext.GetGlobalResourceObject("resource", "Visitor").ToString()%>
                                        <%# Eval("Hit").ToString().Trim() %>
                                    </div>
                                    <div id="Libdate"><%#Tools.Tools.CheckNotEmpty("تاریخ انتشار",Tools.Calender.ReverseDate( Eval("PublishDate").ToString())) %></div>
                                    <div id="LibType">
                                        <%# Tools.Tools.CheckNotEmpty("نویسنده",Eval("Writer").ToString().Trim(),"",true) %>
                                        <%# Tools.Tools.CheckNotEmpty("نویسنده",Eval("Writer1").ToString().Trim(),"",true) %>
                                        <%# Tools.Tools.CheckNotEmpty("نویسنده",Eval("Writer2").ToString().Trim(),"",true) %>
                                        <%# Tools.Tools.CheckNotEmpty("مترجم",Eval("Writer3").ToString().Trim(),"",true) %>
                                        <%# Tools.Tools.CheckNotEmpty("مصحح",Eval("Writer4").ToString().Trim(),"",true) %>
                                        <%# Tools.Tools.CheckNotEmpty("انتشارات",Eval("Publisher").ToString().Trim(),"",true) %>
                                    </div>
                                    <div>
                                        شاخه بندی:<br />
                                        <%#GetCategory( Eval("LibraryID").ToString().Trim())%>
                                    </div>
                                    <div id="LibraryChekide"><%# Eval("Chekide").ToString().Trim()%></div>
                                    <div id="LibraryDownload"><a href ="/Files/<%=Tools.Tools.GetViewUnitID %>/Library/<%# Eval("FileAddress").ToString().Trim()%>">دانلود</a></div>
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
								<a <%# Container.DataItem.ToString().Trim() == (CurrentPage + 1).ToString()? "class=\"CurentPage\"" : "class=\"PagingLNK\"" %> href="/Library/<%=GetTypeLink()%>p<%# Container.DataItem %>.aspx"><%# Container.DataItem %>
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
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterFooter,0)%></asp:Content>
