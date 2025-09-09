<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ProductSales.aspx.cs" Inherits="Portal.Admin.ProductSales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<br>
	<asp:Button runat="server" Text="نمایش همه فاکتورها" ID="ViewAllBTN" OnClick="ViewAllBTN_Click" />
	<br />
	<%if (ViewDR.Items.Count != 0)
   {%>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="1" cellpadding="2" width="100%" border="1">
		
		<tr  class="RowHead">
			<td>
				<%=GetGlobalResourceObject("resource", "FactorCode")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "FullName")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "PriceRial")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "_Date")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Status")%>
			</td>
            <td>
				<%=GetGlobalResourceObject("resource", "Status")%>
			</td>
             <td>
				<%=GetGlobalResourceObject("resource", "Status")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Info")%>
			</td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("Factorid").ToString().Trim()%></td>
					<td align="center">
						&nbsp;<%# Eval("Name").ToString().Trim() %>&nbsp;<%# Eval("family").ToString().Trim()%></td>
					<td align="center">
						&nbsp;<%# Eval("money").ToString()%></td>
					<td align="center">
						&nbsp;<%#Tools.Calender.MyPDate(Eval("date").ToString().Trim())%></td>
					<td align="center">
						&nbsp;<%#Tools.Tools.GetSalesState(Eval("State").ToString())%></td>
                    <td align="center">
						&nbsp;<%#Enum.GetName(typeof( Tools.MyVar.FactorType), Tools.Tools.ConvertToInt32(Eval("type").ToString()))%></td>
					<td align="center">
						&nbsp;<%# Eval("bankid").ToString()%></td>
                    <td align="center">
						<input style="Width:100px" type="button" onclick="SelectPrepMsg('/ProductSalesDetail.aspx?ID=<%# Eval("Factorid").ToString().Trim()%>',520,490);"  value="<%=GetGlobalResourceObject("resource", "Info")%>"/>
						
							<asp:Button 
							Width="100px"  CommandArgument='<%# Eval("Factorid").ToString().Trim() %>'
							CommandName="Final" ID="FinalBTN" Text="<%$ resources: resource , Final %>"  runat="server" />
				
						</td>

					
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%}%>
</asp:Content>
