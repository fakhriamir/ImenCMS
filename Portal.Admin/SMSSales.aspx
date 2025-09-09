<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SMSSales.aspx.cs" Inherits="Portal.Admin.SMS_Sales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script runat=server>
		int sood = 0;
		int sales = 0;
		string SumSood(string aa)
		{
			sales += Tools.Tools.ConvertToInt32(aa);
			if (aa.Trim() == "8000")
			{
				sood += 350;
				return "350";
			}
			if (aa.Trim() == "16000")
			{
				sood += 700; return "700";
			}
			if (aa.Trim() == "32000")
			{
				sood += 1400; return "1400";
			}
			if (aa.Trim() == "46500")
			{
				sood += 1950; return "1950";
			}
			if (aa.Trim() == "62000")
			{
				sood += 2600; return "2600";
			}
			if (aa.Trim() == "75000")
			{
				sood += 3000; return "3000";
			}
			if (aa.Trim() == "112000")
			{
				sood += 4000; return "4000";
			}
			if (aa.Trim() == "125000")
			{
				sood += 3500; return "3500";
			}
			if (aa.Trim() == "360000")
			{
				sood += 9000; return "9000";
			}
			if (aa.Trim() == "575000")
			{
				sood += 12500; return "12500";
			} 
			if (aa.Trim() == "1100000")
			{
				sood += 20000; return "20000";
			} 
			if (aa.Trim() == "3000000")
			{
				sood += 30000; return "30000";
			}
			return "0";
		}
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" class="TableColor" align="center" id="Table2" cellspacing="1" cellpadding="1">
		<tr>
			<td>
				فیلتر شماره فاکتور
			</td>
			<td>
				<asp:TextBox ID="FactorIDFilterTB" runat="server"></asp:TextBox>
			</td>
			<td>
				<asp:Button ID="FactorIDFilterBTN" runat="server" 
					Text="<%$ resources: resource, Filter%>" onclick="FactorIDFilterBTN_Click" />
			</td>
		</tr>
		<tr>
			<td>
				فیلتر ماه و سال میلادی
			</td>
			<td>
				<asp:DropDownList ID="YearDL" runat="server" Width="64px">
					<asp:ListItem Value='2012'>2010</asp:ListItem>
					<asp:ListItem Value='2011'>2011</asp:ListItem>
					<asp:ListItem Value='2012' >2012</asp:ListItem>
					<asp:ListItem Value='2013' Selected="True">2013</asp:ListItem>
					<asp:ListItem Value='2014'>2014</asp:ListItem>
				</asp:DropDownList>
				<asp:DropDownList ID="MonthDL" runat="server" Width="87px">
					<asp:ListItem Value="1" Selected=True >1</asp:ListItem>
					<asp:ListItem Value="2">2</asp:ListItem>
					<asp:ListItem Value="3">3</asp:ListItem>
					<asp:ListItem Value="4">4</asp:ListItem>
					<asp:ListItem Value="5">5</asp:ListItem>
					<asp:ListItem Value="6">6</asp:ListItem>
					<asp:ListItem Value="7">7</asp:ListItem>
					<asp:ListItem Value="8">8</asp:ListItem>
					<asp:ListItem Value="9">9</asp:ListItem>
					<asp:ListItem Value="10">10</asp:ListItem>
					<asp:ListItem Value="11">11</asp:ListItem>
					<asp:ListItem Value="12">12</asp:ListItem>
				</asp:DropDownList>
			</td>
			<td>
				<asp:Button ID="Button1" runat="server" 
					Text="<%$ resources: resource, Filter%>" onclick="Button1_Click"  />
			</td>
		</tr></table>
	<br>
	<%if (ViewDR.Items.Count != 0)
   {%>
   <%=sood.ToString() %>-<%=sales.ToString()%>
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0" cellpadding="2" width="100%"
		border="0">
		<tr class="RowHead">
			<td>
				<%=GetGlobalResourceObject("resource", "FactorCode")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "PriceRial")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "_Date")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "UserID")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "Bank")%>
			</td>
			<td>
				<%=GetGlobalResourceObject("resource", "ID")%>
			</td><td>
				سود
			</td>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						&nbsp;<%# Eval("Factorid").ToString().Trim()%></td>
					<td align="center">
						&nbsp;<%# Eval("money")%></td>
					<td align="center">
						&nbsp;<%#Tools.Calender.MyPDate(Eval("date").ToString().Trim())%></td>
					<td align="center">
						&nbsp;<%#Eval("userid").ToString()%></td>
					<td align="center">
						&nbsp;<%#Tools.Bank.GetBankName(Eval("BankType").ToString())%>
					<td align="center">
						&nbsp;<%#Eval("BankID").ToString()%></td>
					<td align="center">
						&nbsp;<%# SumSood(Eval("money").ToString().Trim())%></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<%=sood.ToString() %>-<%=sales.ToString()%>
	<%}%>
</asp:Content>
