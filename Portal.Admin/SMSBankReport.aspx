<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SMSBankReport.aspx.cs" Inherits="Portal.Admin.SMS_BankReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	
				<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="Table1" align="center" cellspacing="1" cellpadding="2" width="50%" border="1">
					<tr class="RowHead">
						<td colspan="3">
							گزارش بانک ملت
						</td>
					</tr>
					<tr class="RowHead">
						<td>
							<%=GetGlobalResourceObject("resource", "_Date")%>
						</td>
						<td>
							<%=GetGlobalResourceObject("resource", "Total")%>
						</td>
						<td>
							<%=GetGlobalResourceObject("resource", "Sum")%>
						</td>
					</tr>
					<asp:Repeater runat="server" ID="MellatDR">
						<ItemTemplate>
							<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
								<td align="center">
									<%#Tools.Calender.MyPDate(Eval("y").ToString().Trim()+"/"+Eval("m").ToString().Trim()+"/"+ Eval("d").ToString().Trim())%>
								</td>
								<td align="center" style="width: 300px">
									<%# Eval("SumMoney").ToString().Trim()%>
								</td>
								<td align="center" style="width: 300px">
									 <input style="width: 110px" type="button" onclick="SelectPrepMsg('/SMSBankReportItem.aspx?ID=<%# Eval("BankType") %>	&y=<%# Eval("y") %>	&m=<%# Eval("m") %>	&d=<%# Eval("d") %>	',520,490);"
							value="فاکتورها" />
								</td>
							</tr>
						</ItemTemplate>
					</asp:Repeater>
				</table><br />
		<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="Table2" align="center" cellspacing="1" cellpadding="2" width="50%" border="1">
					<tr class="RowHead">
						<td colspan="3">
							گزارش بانک اقتصاد نوین
						</td>
					</tr>
					<tr class="RowHead">
						<td>
							<%=GetGlobalResourceObject("resource", "_Date")%>
						</td>
						<td>
							<%=GetGlobalResourceObject("resource", "Total")%>
						</td>
						<td>
							<%=GetGlobalResourceObject("resource", "Sum")%>
						</td>
					</tr>
					<asp:Repeater runat="server" ID="NevinDR">
						<ItemTemplate>
							<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
								<td align="center">
									<%#Tools.Calender.MyPDate(Eval("y").ToString().Trim()+"/"+Eval("m").ToString().Trim()+"/"+ Eval("d").ToString().Trim())%>
								</td>
								<td align="center" style="width: 300px">
									<%# Eval("SumMoney").ToString().Trim()%>
								</td>
								<td align="center" style="width: 300px">
									 <input style="width: 110px" type="button" onclick="SelectPrepMsg('/SMSBankReportItem.aspx?ID=<%# Eval("BankType") %>	&y=<%# Eval("y") %>	&m=<%# Eval("m") %>	&d=<%# Eval("d") %>	',520,490);"
							value="فاکتورها" />
								</td>
							</tr>
						</ItemTemplate>
					</asp:Repeater>
				</table><br />
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="Table3" align="center" cellspacing="1" cellpadding="2" width="50%" border="1">
					<tr class="RowHead">
						<td colspan="3">
							گزارش بانک پارسیان
						</td>
					</tr>
					<tr class="RowHead">
						<td>
							<%=GetGlobalResourceObject("resource", "_Date")%>
						</td>
						<td>
							<%=GetGlobalResourceObject("resource", "Total")%>
						</td>
						<td>
							<%=GetGlobalResourceObject("resource", "Sum")%>
						</td>
					</tr>
					<asp:Repeater runat="server" ID="ParsianDR">
						<ItemTemplate>
							<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
								<td align="center">
									<%#Tools.Calender.MyPDate(Eval("y").ToString().Trim()+"/"+Eval("m").ToString().Trim()+"/"+ Eval("d").ToString().Trim())%>
								</td>
								<td align="center" style="width: 300px">
									<%# Eval("SumMoney").ToString().Trim()%>
								</td>
								<td align="center" style="width: 300px">
									 <input style="width: 110px" type="button" onclick="SelectPrepMsg('/SMSBankReportItem.aspx?ID=<%# Eval("BankType") %>&y=<%# Eval("y") %>&m=<%# Eval("m") %>&d=<%# Eval("d") %>',520,490);"
							value="فاکتورها" />
								</td>
							</tr>
						</ItemTemplate>
					</asp:Repeater>
				</table>
<br />
	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="Table4" align="center" cellspacing="0" cellpadding="2" width="50%" border="0">
					<tr class="RowHead">
						<td colspan="3">
							گزارش بانک پاسارگاد
						</td>
					</tr>
					<tr class="RowHead">
						<td>
							<%=GetGlobalResourceObject("resource", "_Date")%>
						</td>
						<td>
							<%=GetGlobalResourceObject("resource", "Total")%>
						</td>
						<td>
							<%=GetGlobalResourceObject("resource", "Sum")%>
						</td>
					</tr>
					<asp:Repeater runat="server" ID="ParsagadDR">
						<ItemTemplate>
							<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
								<td align="center">
									<%#Tools.Calender.MyPDate(Eval("y").ToString().Trim()+"/"+Eval("m").ToString().Trim()+"/"+ Eval("d").ToString().Trim())%>
								</td>
								<td align="center" style="width: 300px">
									<%# Eval("SumMoney").ToString().Trim()%>
								</td>
								<td align="center" style="width: 300px">
									 <input style="width: 110px" type="button" onclick="SelectPrepMsg('/SMSBankReportItem.aspx?ID=<%# Eval("BankType") %>	&y=<%# Eval("y") %>	&m=<%# Eval("m") %>	&d=<%# Eval("d") %>	',520,490);"
							value="فاکتورها" />
								</td>
							</tr>
						</ItemTemplate>
					</asp:Repeater>
				</table>
</asp:Content>
