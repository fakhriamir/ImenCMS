<%@ Page Title="" EnableViewState="true" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ProductImport.aspx.cs" Inherits="Portal.Admin.ProductImport" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table class="TableColor" dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" align="center" id="Table1" cellspacing="0" cellpadding="1"
		width="80%">
		
		<tr>
			<td colspan="2" class="RowHead">
				<%=GetGlobalResourceObject("resource", "ImportExcel2003")%>:
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "PrdType")%>
			</td>
			<td>
				<asp:DropDownList ID="productsubjectDL" runat="server" DataTextField="Name" DataValueField="productsubjectID"
				 />
				
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "File")%>:
			</td>
			<td>
				<input id="File1" runat="server" type="file" />
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="SaveBTN" runat="server" Text="<%$ resources: resource,TablePreview%>" OnClick="SaveBTN_Click">
				</asp:Button>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "SelectTable")%>:
			</td>
			<td>
				<asp:DropDownList ID="ShetNameDL" runat="server">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				<asp:Button ID="FieldSelectBTN" runat="server" Text="<%$ resources: resource,SelectField%>" OnClick="FieldSelectBTN_Click"
					Enabled="False" />
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center">
				<table align=center>
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
							<%=GetGlobalResourceObject("resource", "PrdTitle")%>
						</td>
						<td>
							<asp:DropDownList ID="NameDL" runat="server" />
						
						</td>
					</tr>
					<tr>
						<td>
							<%=GetGlobalResourceObject("resource", "Info")%>
						</td>
						<td>
							<asp:DropDownList ID="InfoDL" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							<%=GetGlobalResourceObject("resource", "Rebate")%>
						</td>
						<td>
							<asp:DropDownList ID="discountDL" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							<%=GetGlobalResourceObject("resource", "Weight")%>
						</td>
						<td>
							<asp:DropDownList ID="WeightDL" runat="server" />
							
							
						</td>
					</tr>
					<tr>
						<td>
							<%=GetGlobalResourceObject("resource", "Subject")%>
						</td>
						<td>
							<asp:DropDownList ID="ProductCategortDL" runat="server" />
							
						</td>
					</tr>

					<tr>
						<td>
							<%=GetGlobalResourceObject("resource", "PrdPic")%>
						</td>
						<td>
							<asp:DropDownList ID="ProdPic" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							<%=GetGlobalResourceObject("resource", "DescPic")%>
						</td>
						<td>
							
							<asp:DropDownList ID="PicSelectDL" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							<%=GetGlobalResourceObject("resource", "Stock")%>
						</td>
						<td>
							
							<asp:DropDownList ID="holdingDL" runat="server" />
						</td>
					</tr>
					 <tr>
			<td>
				<%=GetGlobalResourceObject("resource", "KeyWord")%>
			</td>
			<td>
				<asp:DropDownList ID="seokeyDL" runat="server" />
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Description")%> SEO
			</td>
			<td>
				<asp:DropDownList ID="seodescDL" runat="server" />
			</td>
		</tr>
				
				<asp:Panel ID="FormPN" runat="server">
				</asp:Panel></table>
				<asp:Button ID="AddToBankDL" runat="server" Text="<%$ resources: resource, TransferBankBTN %>" Enabled="False"
					OnClick="AddToBankDL_Click" />
			</td>
		</tr>
	</table>
	<asp:Label Text="" ID="ErrorMess" runat="server"></asp:Label>
	<script language="javascript" type="text/javascript">
		for (var i = 0; i < $('select').get().reverse().length; i++) {
			var obj = $('select').get(i).id;
			if (obj.toString().indexOf("MyForm") != -1) {
				//alert(obj.replace("ctl00_Body_MyForm", ""));
				//alert(document.getElementById("ctl00_Body_MyLB" + obj.replace("ctl00_Body_MyForm", "")).innerHTML);
				var myval = document.getElementById("ctl00_Body_MyLB" + obj.replace("ctl00_Body_MyForm", "")).innerHTML;
				//alert(myval);
				$('select').get(i).value=myval;
				//alert(myval);
			}
		}
	</script>
</asp:Content>
