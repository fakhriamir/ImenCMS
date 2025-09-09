<%@ Page Title="" EnableViewState="true" Language="C#" MasterPageFile="~/Pages.master" AutoEventWireup="true" CodeBehind="AdvanceSearch.aspx.cs" Inherits="Portal.AdvanceSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
	<link type="text/css" href="/scripts/ui.core.css" rel="stylesheet" />
	<link type="text/css" href="/scripts/ui.theme.css" rel="stylesheet" />
	<link type="text/css" href="/scripts/ui.datepicker.css" rel="stylesheet" />
	<script type="text/javascript" src="/scripts/jquery.ui.datepicker-cc.js"></script>
	<script type="text/javascript" src="/scripts/calendar.js"></script>
	<script type="text/javascript" src="/scripts/jquery.ui.datepicker-cc-ar.js"></script>
	<script type="text/javascript" src="/scripts/jquery.ui.datepicker-cc-fa.js"></script>
	<script type="text/javascript">
		$(function () {
			$('#ctl00_Body_FromTB').datepicker({
				changeMonth: true,
				changeYear: true
			});
			$('#ctl00_Body_ToTB').datepicker({
				changeMonth: true,
				changeYear: true
			});

		});
		$(document).ready(function () {
			//$('#tabs div').hide();
			//$('#tabs div:first').show();
			//$('#tabs ul li:first').addClass('active');
			$('#tabs ul li a').click(function () {
				$('#tabs ul li').removeClass('active');
				$(this).parent().addClass('active');
				var currentTab = $(this).attr('href');
				$('#tabs div').hide();
				$(currentTab).show();

				return false;
			});
			$('#tabs div').hide();

			$('#tabs #tab-<%=TabID%>').show();
            $('#tabs #tabh<%=TabID%>').addClass('active');

        });

        $(document).ready(function () {
        	$("#tabs").tabs();
        	$('#tabs').bind('tabsselect', function (event, ui) {
        		var selectedTab = ui.index;
        		$("#<%= hidLastTab.ClientID %>").val(selectedTab);
            });
        });
	</script>

    <style type="text/css">
        #tabs {
            font-size: 90%;
            margin: 20px 0;
        }

            #tabs ul {
                float: <%=Tools.Tools.PageAlign%>;
                background: #fff;
                padding-top: 4px;
                margin: 0;
            }

            #tabs li {
                margin-left: 8px;
                list-style: none;
            }

            * html #tabs li {
                display: inline;
            }

                #tabs li, #tabs li a {
                    float: <%=Tools.Tools.PageAlign%>;
                }

            #tabs ul li.active {
                border-top: 2px #dedede solid;
                background: #efefef;
            }

                #tabs ul li.active a {
                    color: #333333;
                }

            #tabs div {
                background: #f9f9f9;
                clear: both;
                padding: 15px;
                min-height: 100px;
                border-bottom: 2px solid #dedede;
                border-right: 1px solid #dedede;
                border-left: 1px solid #dedede;
            }

                #tabs div h3 {
                    margin-bottom: 12px;
                }

                #tabs div p {
                    line-height: 150%;
                }

            #tabs ul li a {
                text-decoration: none;
                padding: 8px;
                color: #000;
                font-weight: bold;
            }

        .thumbs {
            float: left;
            border: #000 solid 1px;
            margin-bottom: 20px;
            margin-right: ProductSearchBTN20px;
        }
        -->
    </style>
    <%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrBeforTitle,0)%>
    <%=HttpContext.GetGlobalResourceObject("resource", "Def_AdvanceSearch_Title").ToString()%>
    <%--	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterTitle,0)%>--%>
    <asp:HiddenField ID="hidLastTab" runat="server" Value="0" />
	<div id="tabs">
		<ul>
			<li id="tabh7"><a href="#tab-7"><%=HttpContext.GetGlobalResourceObject("resource", "AllParts").ToString()%></a></li>
			<li id="tabh1"><a href="#tab-1"><%=HttpContext.GetGlobalResourceObject("resource", "Def_NewsArchive_Title").ToString()%></a></li>
			<li id="tabh2"><a href="#tab-2"><%=HttpContext.GetGlobalResourceObject("resource", "Pages").ToString()%></a></li>
			<li id="tabh8"><a href="#tab-8"><%=HttpContext.GetGlobalResourceObject("resource", "Def_Product_Title").ToString()%></a></li>
			<li id="tabh3"><a href="#tab-3"><%=HttpContext.GetGlobalResourceObject("resource", "Software").ToString()%></a></li>
			<li id="tabh4"><a href="#tab-4"><%=Tools.Tools.GetSetting(377, HttpContext.GetGlobalResourceObject("resource", "Def_Article_Title").ToString())%></a></li>
			<li id="tabh5"><a href="#tab-5"><%=HttpContext.GetGlobalResourceObject("resource", "Movie").ToString()%></a></li>
			<li id="tabh6"><a href="#tab-6"><%=HttpContext.GetGlobalResourceObject("resource", "Sound").ToString()%></a></li>
		</ul>
		<div id="tab-7">
			<table class="SearchBoxAD">
				<tr>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "SearchWord").ToString()%></td>
					<td>
						<asp:TextBox ID="SearchTB" dir="<%=Tools.Tools.PageDir%>" runat="server" Width="300px" CssClass="textBoxstyle" />&nbsp;
					</td>
				</tr>
				<tr>
					<td align="left" colspan="2">
						<asp:Button ID="SearchBTN" runat="server" Text="<%$ resources: resource, Def_Search_Title %>" OnClick="SearchBTN_Click" />
					</td>
				</tr>
			</table>
		</div>
		<div id="tab-8">
			<table class="SearchBoxAD">
				<tr>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "ProductName").ToString()%>:</td>
					<td>
						<asp:TextBox ID="ProductTitleTB" runat="server"></asp:TextBox></td>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "ProcudtCategory").ToString()%> :</td>
					<td>
						<asp:DropDownList ID="ProcudtCategoryDL" runat="server" DataTextField="Name" DataValueField="ProductCategoryID"></asp:DropDownList></td>
				</tr>
				<tr>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "ProductType").ToString()%> : </td>
					<td>
						<asp:DropDownList ID="ProductTypeDL" runat="server" DataTextField="Name" DataValueField="ProductSubjectID"></asp:DropDownList>
					</td>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "ProductDetail").ToString()%> :</td>
					<td>
						<asp:TextBox ID="ProductDetailTB" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td>
						<asp:Button ID="ProductSearchBTN" runat="server" Text="<%$ resources: resource, ProductSearchBTN %>" OnClick="ProductSearchBTN_Click" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
			</table>
		</div>
		<div id="tab-1">
			<table class="SearchBoxAD">
				<tr>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "NewsTitle").ToString()%>:</td>
					<td>
						<asp:TextBox ID="NewsTitleTB" runat="server"></asp:TextBox></td>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "NewsAbstract").ToString()%> :</td>
					<td>
						<asp:TextBox ID="chekidehTB" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "NewsBody").ToString()%> : </td>
					<td>
						<asp:TextBox ID="NewsBodyTB" runat="server"></asp:TextBox></td>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "NewsResource").ToString()%>  :</td>
					<td>
						<asp:TextBox ID="NewsRefTB" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "NewsPeriority").ToString()%> :</td>
					<td>
						<asp:DropDownList ID="NewsPeriorityDL" runat="server" DataTextField="Name" DataValueField="NewsPeriorityID"></asp:DropDownList></td>
					<td>&nbsp;</td>
					<td>
						<asp:CheckBox ID="NewsHitCHK" Text="<%$ resources: resource, TopHit %>" runat="server" /></td>
				</tr>
				<tr>
					<td>
						<asp:Button ID="NewsSearchBTN" runat="server" Text="<%$ resources: resource, NewsSearchBTN %>" OnClick="NewsSearchBTN_Click" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
			</table>


		</div>
		<div id="tab-2">
			<table class="SearchBoxAD">
				<tr>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "PageTitle").ToString()%> : </td>
					<td>
						<asp:TextBox ID="PageTitleTB" runat="server"></asp:TextBox></td>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "PageBody").ToString()%> : </td>
					<td>
						<asp:TextBox ID="PageBodyTB" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td>

						<asp:Button ID="PageSearchBTN" runat="server" Text="<%$ resources: resource, PageSearchBTN %>" OnClick="PageSearchBTN_Click" />
					</td>
					<td></td>
					<td></td>
					<td></td>
				</tr>
			</table>
			<br />

		</div>
		<div id="tab-3">
			<table class="SearchBoxAD">
				<tr>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "FileName").ToString()%> : </td>
					<td>
						<asp:TextBox ID="FileTitleTB" runat="server"></asp:TextBox>
					</td>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "FileBody").ToString()%> :</td>
					<td>
						<asp:TextBox ID="FileBodyTB" runat="server"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "Subject").ToString()%> :</td>
					<td>
						<asp:DropDownList ID="SubjectFileDL" runat="server" DataTextField="Name" DataValueField="SoftTypeID"></asp:DropDownList>
					</td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td>
						<asp:Button ID="SoftSearchBTN" runat="server" Text="<%$ resources: resource, FileSearchBTN %>" OnClick="SoftSearchBTN_Click" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
			</table>
			&nbsp;&nbsp;
		</div>
		<div id="tab-4">
			<table class="SearchBoxAD">
				<tr>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "ArticleTitle").ToString()%> :</td>
					<td>
						<asp:TextBox ID="ArticleTitleTB" runat="server"></asp:TextBox></td>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "ArticleAbstract").ToString()%> :</td>
					<td>
						<asp:TextBox ID="ArticleChekideTB" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "ArticleBody").ToString()%>  :</td>
					<td>
						<asp:TextBox ID="ArticleBodyTB" runat="server"></asp:TextBox></td>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "Author").ToString()%> :</td>
					<td>
						<asp:TextBox ID="ArticleAuthorTB" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "ArticleCatigory").ToString()%> :</td>
					<td>
						<asp:DropDownList ID="ArticleCatDL" runat="server" DataTextField="Name" DataValueField="ArticleTypeID"></asp:DropDownList></td>
					<td>
						<asp:CheckBox ID="CheckBox1" Text="<%$ resources: resource, TopHit %>" runat="server" /></td>
					<td>
						<asp:Button ID="ArticleSearchBTN" runat="server" Text="<%$ resources: resource, ArticleSearchBTN %>" OnClick="ArticleSearchBTN_Click" /></td>
				</tr>
			</table>

		</div>
		<div id="tab-5">
			<table class="SearchBoxAD">
				<tr>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "MovieTitle").ToString()%> :</td>
					<td>
						<asp:TextBox ID="MovieTitleTB" runat="server"></asp:TextBox></td>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "MovieDesc").ToString()%>  :</td>
					<td>
						<asp:TextBox ID="MovieTextTB" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "MovieSubject").ToString()%>  :</td>
					<td>
						<asp:DropDownList ID="MovieSubjectDL" runat="server" DataTextField="Name" DataValueField="MovieTypeID"></asp:DropDownList></td>
					<td>
						<asp:Button ID="MovieSearchBTN" runat="server" Text="<%$ resources: resource, MovieSearchBTN %>" OnClick="MovieSearchBTN_Click" /></td>
					<td></td>
				</tr>

			</table>

		</div>
		<div id="tab-6">
			<table class="SearchBoxAD">
				<tr>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "SoundTitle").ToString()%> :</td>
					<td>
						<asp:TextBox ID="SoundTitleTB" runat="server"></asp:TextBox></td>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "SoundDesc").ToString()%> :</td>
					<td>
						<asp:TextBox ID="SoundTextTB" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><%=HttpContext.GetGlobalResourceObject("resource", "SoundSubject").ToString()%> :</td>
					<td>
						<asp:DropDownList ID="SoundSubjectDL" runat="server" DataTextField="Name" DataValueField="SoundTypeID"></asp:DropDownList></td>
					<td>
						<asp:Button ID="SoundSearchBTN" runat="server" Text="<%$ resources: resource, SoundSearchBTN %>" OnClick="SoundSearchBTN_Click" /></td>
					<td></td>
				</tr>
			</table>

		</div>

	</div>

	<table align="center">
		<tr>
			<td>
				<script runat="server">
					string[] SearchLink = { "", "News/News", "Article", "Movies", "Sounds", "Software", "Page", "Shop/Product" };
					string[] SearchType = { "", HttpContext.GetGlobalResourceObject("resource", "Def_NewsArchive_Title").ToString(), HttpContext.GetGlobalResourceObject("resource", "Articles").ToString(), HttpContext.GetGlobalResourceObject("resource", "Movie").ToString(), HttpContext.GetGlobalResourceObject("resource", "Sound").ToString(), HttpContext.GetGlobalResourceObject("resource", "Software").ToString(), HttpContext.GetGlobalResourceObject("resource", "Pages").ToString(), HttpContext.GetGlobalResourceObject("resource", "Def_Product_Title").ToString() };
				</script>
				<%if (ViewDR.Items.Count != 0)
	  {%><br />
				<table dir="<%=Tools.Tools.PageDir%>" align="<%=Tools.Tools.PageAlign%>" cellspacing="0" cellpadding="4" style="width: 600px; border: 1px solid #e5e6e7;">
					<tr>
						<td align="center" style="background-color: White">
							<%=HttpContext.GetGlobalResourceObject("resource", "SearchResultNo").ToString()%> : 
                            <asp:Label runat="server" ID="Resultlb"></asp:Label>
						</td>
					</tr>
					<asp:Repeater runat="server" ID="ViewDR">
						<ItemTemplate>
							<tr style="background-color: #f3f3f3;">
								<td align="<%=Tools.Tools.PageAlign%>" style="border-bottom: 1px solid #e5e6e7;">
									<h3><a href="/<%#SearchLink[Tools.Tools.ConvertToInt32(Eval("SType").ToString().Trim())] %>/<%# Eval("ID").ToString().Trim() %>/<%#Tools.Tools.UrlWordReplace(Eval("TITLE").ToString().Trim()) %>"><%#Eval("TITLE").ToString().Trim() %></a></h3>
									<div id="AdvanceSearchBody"><%# GetHighLight(Eval("Body").ToString().Trim()) %></div>
								</td>
							</tr>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tr style="background-color: #fcfbe3;">
								<td align="<%=Tools.Tools.PageAlign%>" style="border-bottom: 1px solid #e5e6e7;">
									<h3><a href="/<%#SearchLink[Tools.Tools.ConvertToInt32(Eval("SType").ToString().Trim())] %>/<%# Eval("ID").ToString().Trim() %>/<%#Tools.Tools.UrlWordReplace(Eval("TITLE").ToString().Trim()) %>"><%#Eval("TITLE").ToString().Trim() %></a></h3>
									<div id="AdvanceSearchBody"><%# GetHighLight(Eval("Body").ToString().Trim()) %></div>
								</td>
							</tr>
						</AlternatingItemTemplate>
						<FooterTemplate>
						</FooterTemplate>
					</asp:Repeater>
					<tr>
						<td align="center">
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
				</table>
				<%}%>
			</td>
		</tr>
	</table>
	<div style="clear:both"></div>

	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterBody,0)%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterFooter,0)%>
</asp:Content>
