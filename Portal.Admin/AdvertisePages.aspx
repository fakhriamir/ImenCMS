<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdvertisePages.aspx.cs" Inherits="Portal.Admin.AdvertisePages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="1" cellpadding="1" width="80%">
		<tr>
			<td colspan="2" class="RowHead">
				<%=GetGlobalResourceObject("resource","AdvInPages") %>
			</td>
		</tr>
		<tr>
			<td>
			صفحه
			</td>
			<td>
				<asp:DropDownList ID="PageIDDL" runat="server" AutoPostBack="True" 
					onselectedindexchanged="AdvertiseIDDL_SelectedIndexChanged">
					<asp:ListItem Text="<%$ resources: resource,DefaultPage %>" Value="1" />
					<asp:ListItem Text="<%$ resources: resource,ArticlePT %>" Value="2" />
					<asp:ListItem Text="<%$ resources: resource,OrgChart %>" Value="3" />
					<asp:ListItem Text="<%$ resources: resource,ContactPT %>" Value="4" />
					<asp:ListItem Text="<%$ resources: resource,GalleryPT %>" Value="5" />
					<asp:ListItem Text="<%$ resources: resource,LinksPT %>" Value="6" />
					<asp:ListItem Text="<%$ resources: resource,MatchPT %>" Value="7" />
					<asp:ListItem Text="<%$ resources: resource,MoviePT %>" Value="8" />
					<asp:ListItem Text="<%$ resources: resource,CreatedPagesPT %>" Value="9" />
					<asp:ListItem Text="<%$ resources: resource,ManagerPT %>" Value="10" />
					<asp:ListItem Text="<%$ resources: resource,ProductsPT %>" Value="11" />
					<asp:ListItem Text="<%$ resources: resource,ProductSubjectPT %>" Value="12" />
					<asp:ListItem Text="<%$ resources: resource,FAQPT %>" Value="13" />
					<asp:ListItem Text="<%$ resources: resource,SearchPT %>" Value="14" />
					<asp:ListItem Text="<%$ resources: resource,StorePT %>" Value="15" />
					<asp:ListItem Text="<%$ resources: resource,SoftwarePT %>" Value="16" />
					<asp:ListItem Text="<%$ resources: resource,VoicePT %>" Value="17" />
					<asp:ListItem Text="<%$ resources: resource,SendArticlePT %>" Value="18" />
					<asp:ListItem Text="<%$ resources: resource,BranchesPT %>" Value="19" />
                    <asp:ListItem Text="<%$ resources: resource,SendMsgPT %>" Value="19" />
                    <asp:ListItem Text="<%$ resources: resource,NewsPT %>" Value="20" />
                    <asp:ListItem Text="<%$ resources: resource,FormsPT %>" Value="21" />
					<asp:ListItem Text="سیستم ارسال پیامک" Value="22" />
					<asp:ListItem Text="سیستم تلگرام" Value="23" />
					<asp:ListItem Text="کمپین ها" Value="24" />
					<asp:ListItem Text="صفحه اصلی پروژه های حمایتی" Value="25" />
					<asp:ListItem Text="صفحه نمایش پروژه های حمایتی" Value="26" />
				</asp:DropDownList>	
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource","SelectAdv") %>
			</td>
			<td>
				<asp:DropDownList ID="AdvertiseIDDL" DataTextField="name" 
					DataValueField="AdvertiseID" runat="server"  />
				
			</td>
		</tr>
		
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNText %>" Width="70px" 
					onclick="SaveBTN_Click"/><input type="reset" style="width: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20">
			</td>
		</tr>
	</table>
	<br>
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="1" cellpadding="2" width="100%"
		border="0">
		
		<tr class="RowHead">
		<td>
				<%=GetGlobalResourceObject("resource", "Order")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Pic")%>
			</td>
			
			<td>
				<%=GetGlobalResourceObject("resource", "Del")%>
			</td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR"   OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
				<td align="center">
						<asp:ImageButton ImageUrl="Imgs/Up.png" Width="18px" Height="18px" CommandArgument='<%# Eval("Sort") +"#"+ Eval("AdvertisePageID") %>'
							CommandName="UP" ID="UpIB" ToolTip="<%$ resources: resource,MoveUp %>" AlternateText="<%$ resources: resource,MoveUp %>"
							runat="server" />
						<asp:ImageButton ImageUrl="Imgs/down.png" Width="18px" Height="18px" CommandArgument='<%# Eval("Sort") +"#"+ Eval("AdvertisePageID") %>'
							CommandName="DOWN" ID="DownBTN" ToolTip="<%$ resources: resource,MoveDown %>" AlternateText="<%$ resources: resource,MoveDown %>"
							runat="server" />
					</td>
					<td align="center">
						&nbsp;<%# Eval("AdvertisePageID").ToString().Trim() %></td>
					<td align="center" style="width:200px">
						&nbsp;<%# Eval("Name").ToString().Trim() %></td>
						<td align="center" style="width:180px;" >
						<%# GetAdver(Eval("Files").ToString().Trim())%>
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("AdvertisePageID") %>' CommandName="DEL"
							ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
