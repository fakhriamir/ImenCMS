<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="Portal.Admin.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table class="TableColor" width="70%" border="1">
		<tr>
			<td colspan="2">
				<%=GetGlobalResourceObject("resource", "TotalAllNews")%>
			</td>
			<td colspan="2" align="center">
				<%=ADAL.A_ExecuteData.CNTData("select count(*) from news where unitid=" + ADAL.A_CheckData.GetUnitID())%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "TotalStatement")%>
			</td>
			<td align="center">
				<%=ADAL.A_ExecuteData.CNTData("select count(*) from news where type=3 and unitid=" + ADAL.A_CheckData.GetUnitID())%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "TotalSpecialNews")%>
			</td>
			<td align="center">
				<%=ADAL.A_ExecuteData.CNTData("select count(*) from news where type=1 and unitid=" + ADAL.A_CheckData.GetUnitID())%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "TotalORGNews")%>
			</td>
			<td align="center">
				<%=ADAL.A_ExecuteData.CNTData("select count(*) from news where type=2 and unitid=" + ADAL.A_CheckData.GetUnitID())%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "TotalNews")%>
			</td>
			<td align="center">
				<%=ADAL.A_ExecuteData.CNTData("select count(*) from news where type=0 and unitid=" + ADAL.A_CheckData.GetUnitID())%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "TotalVisualNews")%>
			</td>
			<td align="center">
				<%=ADAL.A_ExecuteData.CNTData("select count(*) from news where type=4 and unitid=" + ADAL.A_CheckData.GetUnitID())%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "NewsComment")%>
			</td>
			<td align="center">
				<%=ADAL.A_ExecuteData.CNTData("SELECT COUNT(*) AS Expr1  FROM Rating INNER JOIN News ON Rating.ID = News.NewsID  WHERE     (Rating.Type = 6) AND (News.UnitID = " + ADAL.A_CheckData.GetUnitID() + ")")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "NewsPost")%>
			</td>
			<td align="center">
				<%=ADAL.A_ExecuteData.CNTData("SELECT     COUNT(*) AS Expr1  FROM         Comment INNER JOIN                        News ON Comment.ID = News.NewsID  WHERE     (Comment.Type = 6) and News.unitid=" + ADAL.A_CheckData.GetUnitID())%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "NewsView")%>
			</td>
			<td align="center">
				<%=ADAL.A_ExecuteData.CNTData("select sum(hit) from news where unitid=" + ADAL.A_CheckData.GetUnitID())%>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<%=GetGlobalResourceObject("resource", "TotalArticle")%>
			</td>
			<td colspan="2" align="center">
				<%=ADAL.A_ExecuteData.CNTData("select count(*) from article where unitid=" + ADAL.A_CheckData.GetUnitID())%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "ArticleComment")%>
			</td>
			<td align="center">
				<%=ADAL.A_ExecuteData.CNTData("SELECT COUNT(*) AS Expr1 FROM Rating INNER JOIN Article ON Rating.ID = Article.ArticleID WHERE (Rating.Type = 1) and Article.unitid=" + ADAL.A_CheckData.GetUnitID())%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "ArticlePost")%>
			</td>
			<td align="center">
				<%=ADAL.A_ExecuteData.CNTData("SELECT COUNT(*) AS Expr1  FROM Comment INNER JOIN article ON Comment.ID = article.articleID  WHERE (Comment.Type = 6) and article.unitid=" + ADAL.A_CheckData.GetUnitID())%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "ArticleView")%>
			</td>
			<td align="center">
				<%=ADAL.A_ExecuteData.CNTData("select sum(hit) from article where unitid=" + ADAL.A_CheckData.GetUnitID())%>
			</td>
			<td align="center">
			</td>
			<td align="center">
			</td>
		</tr>
	</table>
</asp:Content>
