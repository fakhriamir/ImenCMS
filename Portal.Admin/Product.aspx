<%@ Page Title="" EnableViewState="true" EnableEventValidation="false" ValidateRequest="false" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="Portal.Admin.Product" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table1" cellspacing="1" cellpadding="1" width="95%">
		<tr>
			<td colspan="2" class="RowHead">
				<%=GetGlobalResourceObject("resource", "PrdMng")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Lang")%>
			</td>
			<td>
				<asp:DropDownList ID="LanguageDL"   CssClass="NotSearch" runat="server" DataTextField="Name" DataValueField="LangID">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "PrdType")%>
			</td>
			<td>
				<asp:DropDownList ID="productsubjectDL" runat="server" DataTextField="Name" DataValueField="productsubjectID"
					AutoPostBack="True" OnSelectedIndexChanged="productsubjectDL_SelectedIndexChanged" />
					<a href="/ProductSubject">
						<img width="20" height="20" alt="ویرایش" title="ویرایش" src="/Imgs/myedit.gif" /></a>
			
			</td>
		</tr>
        <tr>
			<td>
				فروشگاه
			</td>
			<td>
				<asp:DropDownList ID="productShopDL" runat="server" DataTextField="Name" DataValueField="productShopID"
					/>
				
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "PrdTitle")%>
			</td>
			<td>
				<asp:TextBox ID="nameTB" runat="server" MaxLength="128"/>*
			</td>
		</tr>
		<tr>
			<td>
				توضیح مختصر
			</td>
			<td>
				<asp:TextBox ID="SummaryTB" TextMode="MultiLine" runat="server" Width="350px" MaxLength="512"/>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Info")%>
			</td>
			<td>
				<CKEditor:CKEditorControl FormatSource="false"  ID="MyFCK"  runat="server" Width="850px" Height="250px">
	</CKEditor:CKEditorControl>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Rebate")%>
			</td>
			<td>
				<asp:TextBox ID="discountTB" runat="server" Text="0" MaxLength="25"/>*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Weight")%>
			</td>
			<td>
				<asp:TextBox ID="WeightTB" runat="server" MaxLength="25"/>
				<%=GetGlobalResourceObject("resource", "ToG")%>*
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Subject")%>
			</td>
			<td>
				<asp:CheckBoxList RepeatColumns="5" DataValueField="ProductCategoryID" DataTextField="Name"
					ID="ProductCategortCB" runat="server">
				</asp:CheckBoxList>
			</td>
		</tr>
		
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "PrdPic")%>
			</td>
			<td>
				<input id="THPicSelectTB" runat="server" type="file" />
			</td>
		</tr>
        <tr>
			<td>
				<%=GetGlobalResourceObject("resource", "DescPic")%>
			</td>
			<td>
				<input id="PicSelectTB" runat="server" type="file" />
			</td>
		</tr>
         <tr>
			<td>
				دیگر تصاویر
			</td>
			<td>
				 <asp:FileUpload id="PicsMF" Multiple="Multiple" class="multi" runat="server"/>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Stock")%>
			</td>
			<td>
				<asp:TextBox ID="holdingTB" runat="server" MaxLength="25"/>*
			</td>
		</tr>
		
		<asp:Panel ID="ProductTypePN" runat="server">
		</asp:Panel>
		<asp:Panel ID="PriceTypePN" runat="server">
		</asp:Panel>
        <tr>
			<td>
				<%=GetGlobalResourceObject("resource", "KeyWord")%>
			</td>
			<td>
				<asp:TextBox ID="seokeyTB" runat="server" MaxLength="64"/>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Description")%> SEO
			</td>
			<td>
				<asp:TextBox ID="seodescTB" runat="server" MaxLength="64"/>
			</td>
		</tr>
		
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNText %>" Width="70px" OnClick="SaveBTN_Click">
				</asp:Button><input type="reset" style="width: 70px" value="<%=GetGlobalResourceObject("resource", "ResetBTN")%>" size="20">
			</td>
		</tr>
	</table>
	<br>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table2" cellspacing="1" cellpadding="1" >
		<tr>
			<td>
				
					<asp:TextBox ID="IDFilterTB" runat="server"></asp:TextBox></td>
				<td>
					<asp:Button ID="FilterBTN" runat="server" Text="<%$ resources: resource, Filter %>" OnClick="FilterBTN_Click" 
						 /></td>
		</tr>
		</table>
	<br />
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="1" cellpadding="2" width="100%"
		border="1">
		
		<tr class="RowHead">
			<td>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Visit")%>
			</td>
				<td>
			محصولات مرتبط
			</td>
			<td>
			عکس
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Edit")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Del")%>
			</td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR"    OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("ProductID").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("Name").ToString().Trim() %></td>
					<td align="center">
						&nbsp;<%# Eval("hit").ToString().Trim() %></td>
					<td align="center">
						<input style="Width:100px" type="button" onclick="SelectPrepMsg('/ProductRelation?ID=<%# Eval("ProductID") %>',520,490);"  value="محصولات مرتبط"/>
					</td>
					<td align="center">
						<input style="Width:50px" type="button" onclick="SelectPrepMsg('/ProductPic?ID=<%# Eval("ProductID") %>',620,490,'تصاویر محصول');"  value="عکس"/>
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="Imgs/edit.gif" Width="18px" Height="18px" CommandArgument='<%# Eval("ProductID").ToString().Trim() %>' CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>" runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif" Width="16px" Height="16px" CommandArgument='<%# Eval("ProductID").ToString().Trim() %>' CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
		<tr>
			<td colspan=5 align="center">
				<asp:LinkButton ID="lnkPreviousPage" runat="server" OnClick="lnkPreviousPage_Click" Text="<%$ resources: resource, Previews%>"></asp:LinkButton>
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
				<asp:LinkButton ID="lnkNextPage" runat="server" OnClick="lnkNextPage_Click" Text="<%$ resources: resource, Next%>"></asp:LinkButton>
			</td>
		</tr>
	</table>
	<%}%>
</asp:Content>
