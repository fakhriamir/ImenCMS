<%@ Page Title="" EnableViewState="false" Language="C#" MasterPageFile="~/Pages.master"	AutoEventWireup="true" CodeBehind="Gallery1.aspx.cs" Inherits="Portal.Gallery1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
	<script src="/Scripts/jquery-1.4.2.js"></script>
	<script src="/Scripts/galleria.js"></script>
	<style>
		html, body
		{
		}
		#galleria
		{
			width: 630px;
			height: 500px;
			margin: 20px auto;
		}
		a
		{
			color: #aaa;
		}
	</style>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrBeforTitle,0)%>
	<%=HttpContext.GetGlobalResourceObject("resource", "Def_Gallery_Title").ToString()%>
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
					<td align="right">
						<asp:Repeater ID="TextDG" runat="server">
							<ItemTemplate>
								<img style="width: 100%" border="0" title="<%#Tools.Tools.SetItemTitle(this.Page, Eval("[Desc]").ToString().Trim())%>"
									alt="<%# Eval("[Desc]").ToString().Trim()%>" src='<%=DAL.CheckData.GetFilesRoot() %>/Images/Gallery/<%# Eval("Name").ToString().Trim()%>' />
							</ItemTemplate>
						</asp:Repeater>
					</td>
				</tr>
				<tr>
					<td>
						<asp:LinkButton ID="BackBTN" runat="server" OnClick="BackBTN_Click" Text="<%$ resources: resource, Back %>" />
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:LinkButton ID="NextBTN" runat="server" OnClick="NextBTN_Click" Text="<%$ resources: resource, Next %>" />
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
						<div id="MyGV" style="width: 550px" runat="server">
						</div>
						<script type="text/javascript">

							// Load theme
							Galleria.loadTheme('Scripts/galleria.classic.js');

							// run galleria and add some options
							$('#galleria').galleria({
								image_crop: true,
								data_config: function (img) {
									// will extract and return image captions from the source:
									return {
										title: $(img).parent().next('strong').html(),
										description: $(img).parent().next('strong').next().html()
									};
								}
							});
						</script>
						<%--<asp:Repeater runat="server" ID="TitleDR">
                           <ItemTemplate>
                        
                            <a href="/Gallery-<%# Eval("GalleryID").ToString().Trim()%>.aspx">
                                <img style="width:120px" border="0" title="<%# Eval("[Desc]").ToString().Trim()%>"
                                    alt="<%# Eval("[Desc]").ToString().Trim()%>" src='<%=DAL.CheckData.GetFilesRoot() %>/Images/Gallery/th_<%# Eval("Name").ToString().Trim()%>' /></a>
                        
                        </ItemTemplate>
                        </asp:Repeater>--%>
					</td>
				</tr>
				<%--<tr>
                    <td>
                        <asp:LinkButton ID="lnkPreviousPage" runat="server" OnClick="lnkPreviousPage_Click">قبلي</asp:LinkButton>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="NowPNoLB"/>
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkNextPage" runat="server" OnClick="lnkNextPage_Click">بعدي</asp:LinkButton>
                    </td>
                </tr>--%>
			</asp:View>
		</asp:MultiView>
	</table>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterBody,0)%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterFooter,0)%>
</asp:Content>
