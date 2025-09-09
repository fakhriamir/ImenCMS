<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SMSReport.aspx.cs" Inherits="Portal.Admin.SMS_Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table>
		<tr>
			<td style="width: 50%; vertical-align: top">
				<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0" cellpadding="2" width="100%" border="0">
					<tr class="RowHead">
						<td colspan="2">
							<%=GetGlobalResourceObject("resource", "MemberShipReport")%>
						</td>
					</tr>
					<tr class="RowHead">
						<td>
							<%=GetGlobalResourceObject("resource", "_Date")%>
						</td>
						<td>
							<%=GetGlobalResourceObject("resource", "TotalMember")%>
						</td>
					</tr>
					<asp:Repeater runat="server" ID="ViewDR">
						<ItemTemplate>
							<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
								<td align="center">
									<%# Eval("y").ToString().Trim()%>-<%# Eval("m").ToString().Trim()%>-<%# Eval("d").ToString().Trim()%>
								</td>
								<td align="center" style="width: 300px">
									<%# Eval("CNT").ToString().Trim()%>
								</td>
							</tr>
						</ItemTemplate>
					</asp:Repeater>
				</table>
			</td>
			<td style="width: 50%; vertical-align: top">
				<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="Table1" align="center" cellspacing="1" cellpadding="2" width="100%" border="1">
					<tr class="RowHead">
						<td colspan="3">
							<%=GetGlobalResourceObject("resource", "SMSReport")%>
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
					<asp:Repeater runat="server" ID="SMSLog">
						<ItemTemplate>
							<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
								<td align="center">
									<%# Eval("y").ToString().Trim()%>-<%# Eval("m").ToString().Trim()%>-<%# Eval("d").ToString().Trim()%>
								</td>
								<td align="center" style="width: 300px">
									<%# Eval("CNT").ToString().Trim()%>
								</td>
								<td align="center" style="width: 300px">
									<%# Eval("allCNT").ToString().Trim()%>
								</td>
							</tr>
						</ItemTemplate>
					</asp:Repeater>
				</table>
			</td>
		</tr>
	</table>
	<%=GetGlobalResourceObject("resource", "SoldSum")%>:<%=ADAL.A_ExecuteData.CNTData("SELECT     SUM(Count) AS Expr1 FROM         SMS_SMSActive") %><br />
	<%=GetGlobalResourceObject("resource", "SentSum")%>:<%=ADAL.A_ExecuteData.CNTData("SELECT     SUM(SendCount) AS Expr1 FROM         SMS_SMSLog") %><br />
</asp:Content>
