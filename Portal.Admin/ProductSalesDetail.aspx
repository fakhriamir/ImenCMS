<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="ProductSalesDetail.aspx.cs" Inherits="Portal.Admin.ProductSalesDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script type="text/javascript">

	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" id="My" align="center" cellspacing="1" cellpadding="2" border="1">
		<asp:Repeater runat="server" ID="ViewDR">
			<ItemTemplate>
				<tr>
					<td colspan="2" align="center">
						<%=GetGlobalResourceObject("resource", "Status")%>:&nbsp;<%#Tools.Tools.GetSalesState(Eval("State").ToString())%></td>
				</tr>
				<tr align="center">
					<td>
						<%=GetGlobalResourceObject("resource", "FactorCode")%>: &nbsp;<%# Eval("Factorid").ToString().Trim()%></td>
					<td  >
						<div onclick="$('#UserAddress').show()"><%=GetGlobalResourceObject("resource", "FullName")%> : &nbsp;<%# Eval("Name").ToString().Trim() %>&nbsp;<%# Eval("family").ToString().Trim()%>-<%# Eval("UserID").ToString().Trim()%>-</div></td>
				</tr>
				<tr align="center">
					<td>
						<%=GetGlobalResourceObject("resource", "PriceRial")%> : &nbsp;<%# Eval("money").ToString()%></td>
					<td>
						<%=GetGlobalResourceObject("resource", "_Date")%> : &nbsp;<%#Tools.Calender.MyPDate(Eval("date").ToString().Trim())%></td>
				</tr>
				<tr align="center">
					<td>
						<%=GetGlobalResourceObject("resource", "Bank")%> : &nbsp;<%# MyBankType[Tools.Tools.ConvertToInt32(Eval("banktype"))]%></td>
					<td>
						<%=GetGlobalResourceObject("resource", "PayID")%> : &nbsp;<%# Eval("bankid").ToString().Trim()%></td>
				</tr>
				<tr align="center">
					<td>
						<%=GetGlobalResourceObject("resource", "MobileNo")%> : &nbsp;<%# Eval("mobno").ToString().Trim()%></td>
					<td>
						<%=GetGlobalResourceObject("resource", "Tel")%> : &nbsp;<%# Eval("telno").ToString().Trim()%></td>
				</tr>
				<tr align="center">
					<td>
						<%=GetGlobalResourceObject("resource", "PostalCode")%> : &nbsp;<%# Eval("Postalcode").ToString().Trim()%></td>
					<td>
						<%=GetGlobalResourceObject("resource", "Ostan")%> : &nbsp;<%# Eval("Oname").ToString().Trim()%></td>
				</tr>
				<tr align="center">
					<td>
						<%=GetGlobalResourceObject("resource", "City")%> : &nbsp;<%# Eval("CName").ToString().Trim()%></td>
					<td>
						<%=GetGlobalResourceObject("resource", "Address")%> : &nbsp;<%# Eval("Address").ToString().Trim()%></td>
				</tr>
				<tr align="center" id="UserAddress" style="display:none">
					<td colspan="2">
					<%#GetUserAddress(Eval("UserID").ToString().Trim()) %>
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
		
	</table>
	
	<br />
	<table border="1" width="80%" align=center>
		<tr  class="RowHead">
			<td>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Title")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Weight")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Total")%>
			</td>
			<td>
				مبلغ
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Del")%>
			</td>
		</tr>
		<asp:Repeater runat="server" ID="ProductDR" OnItemCommand="ProductDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td>
						<%# Eval("productID")%>
					</td>
					<td>
						<%# Eval("Name")%>
					</td>
					<td>
						<%#Eval("Weight").ToString()%>
						گرم
					</td>
					<td>
						<%#Eval("total").ToString()%>
						<%=GetGlobalResourceObject("resource", "_Number")%>
					</td>
					<td>
						<%#Eval("Money").ToString()%>
					</td>
					<td>
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Imgs/zarb.gif"
							Width="16px" Height="16px" CommandArgument='<%# Eval("productID").ToString().Trim() %>'
							CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>" AlternateText="<%$ resources: resource, Del %>" runat="server" />
			
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<br />
	<table border="1" width="80%">
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Status")%>
			</td>
			<td>
				<asp:DropDownList ID="StateDL" runat="server">
					<asp:ListItem Value="2" Text="<%$ resources: resource , Final %>"></asp:ListItem>
					<asp:ListItem Value="4" Text="<%$ resources: resource , NoFinal %>"></asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "PostCode")%></td>
			<td>
				<asp:TextBox ID="PostIDTB" runat="server" MaxLength="16" Width="202px"/>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "UserMsg")%></td>
			<td>
				<asp:TextBox ID="MessageTB" runat="server" Height="86px" MaxLength="256" 
					TextMode="MultiLine" Width="198px"/>
			</td>
		</tr>
		<tr>
			<td>
				&nbsp;</td>
			<td>
				<asp:Button ID="SaveBTN" runat="server" onclick="SaveBTN_Click" Text="<%$ resources: resource, SaveBTNText %>" />
			</td>
		</tr>
	</table>
</asp:Content>
