<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true"
	CodeBehind="AdminDefaultSimple.aspx.cs" Inherits="Portal.Admin.AdminDefaultSimple" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<style type="text/css">
		#PreViewDiv div
		{
			height: 100px;
			border: 2 solide red;
		}
		.TempC
		{
			border: 2px solid red;
			text-align: center;
			vertical-align: middle;
		}
	</style>
	<script language="javascript" type="text/javascript">
		function ResVal(ID) {
			if (document.getElementById(ID).value == '<%=GetGlobalResourceObject("resource", "WidthPX")%>')
				document.getElementById(ID).value = "";
		}
		function AddDivRow(ID, CNT) {
			if (document.all) {
				document.getElementById("DivRow" + ID).innerText = "";
				document.getElementById("DivWidth" + ID).innerText = "";
			}
			else {
				document.getElementById("DivRow" + ID).textContent = "";
				document.getElementById("DivWidth" + ID).textContent = "";
			}
			if (CNT == "")
				return;

			for (i = 0; i < CNT; i++) {
				if (document.all) {

					var optn = document.createElement("<select id=\"Row" + ID + "_" + i + "\">");
					var opWidth = document.createElement("<input onfocus='ResVal(\"RowWidth" + ID + "_" + i + "\")' id=\"RowWidth" + ID + "_" + i + "\" value='" + GetGlobalResourceObject("resource", "WidthPX").ToString() + "'>");

					document.getElementById("DivWidth" + ID).appendChild(opWidth);
					//alert(ID);	
					document.getElementById("DivRow" + ID).appendChild(optn);
					//alert("Row" + ID + "_" + i);

					FillDefItem("Row" + ID + "_" + i);
				}
				else {
					var optn = document.createElement("select");
					optn.setAttribute("id", "Row" + ID + "_" + i);
					document.getElementById("DivRow" + ID).appendChild(optn);

					var opWidth = document.createElement("input");
					opWidth.setAttribute("onfocus", "ResVal(\"RowWidth" + ID + "_" + i + "\")");
					opWidth.setAttribute("id", "RowWidth" + ID + "_" + i);
					opWidth.setAttribute("value", '<%=GetGlobalResourceObject("resource", "WidthPX")%>');
					document.getElementById("DivWidth" + ID).appendChild(opWidth);


					//alert("Row" + ID + "_" + i);

					FillDefItem("Row" + ID + "_" + i);
				}
			}
		}
		function FillDefItem(ID) {
			var DL = document.getElementById(ID);
			var MyDefItem = "<%=DefItems%>";
			// alert(MyDefItem);
			//  return;

			while (MyDefItem.length != 0) {
				var endL = MyDefItem.indexOf("&");
				var tmpit = MyDefItem.substr(0, endL);
				MyDefItem = MyDefItem.substr(endL + 1);

				var optn = document.createElement("OPTION");
				optn.text = tmpit.substr(tmpit.indexOf('#') + 1);
				optn.value = tmpit.substr(0, tmpit.indexOf('#'));
				DL.options.add(optn);
			}
		}
		function GetItemVal(val, name) {
		    if (val == "" || val == "NaN" || val == '<%=GetGlobalResourceObject("resource", "WidthPX")%>')
				return "";
			else
				return name + ":" + val + "px;";
		}
		function BuildPreview() {
			if (document.getElementById("TempName").value == "") {
				alert(GetGlobalResourceObject("resource", "InsertTemplateName").ToString());
				return;
			}
			var OT = "<div ID='DefDiv' style='" + GetItemVal(parseInt(document.getElementById("DefWidth").value) + 18, "width") + "'>";
			OT += GetCadrItem(1);
			OT += GetCadrItem(2);
			OT += GetCadrItem(3);
			OT += GetCadrItem(4);
			OT += GetCadrItem(5);
			OT += GetCadrItem(6);
			var EndOT = "</div>";
			document.getElementById("TemplateHTML").value = OT + EndOT;
			//alert(OT);
			document.getElementById("SaveBTN").style.display = "";
			document.getElementById("PreViewDiv").innerHTML = OT + EndOT;
		}
		function GetCadrItem(ID) {
			var rettext = "";
			for (i = 0; i < 4; i++) {

				if (null != document.getElementById("Row" + ID + "_" + i)) {
					rettext += "<div dir=ltr class='TempC' style='float:right;" + GetItemVal(document.getElementById("RowWidth" + ID + "_" + i).value, "width") + "' >#" + document.getElementById("Row" + ID + "_" + i).value + "#</div>";
				}
			}
			rettext += "<br/>";
			return rettext;
		}
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<%=GetGlobalResourceObject("resource", "PageWidth")%>:<input id="DefWidth" type="text"
		value="<%=GetGlobalResourceObject("resource", "WidthPX")%>" onfocus="ResVal('DefWidth')" />
	<%=GetGlobalResourceObject("resource", "TemplateName")%>:<asp:TextBox ID="TempName"
		runat="server" ClientIDMode="Static" />
	<table class="TableColor" border="0" width="100%" style="height: 350px">
		<tr>
			<td style="height: 100px;" valign="top">
				<table>
					<tr>
						<td colspan="2">
							<%=GetGlobalResourceObject("resource", "ModuleCount")%><select id="Row1" onchange="AddDivRow(1,this.value)">
								<option value="">
									<%=GetGlobalResourceObject("resource", "Select")%></option>
								<option value="1">
									<%=GetGlobalResourceObject("resource", "One")%></option>
								<option value="2">
									<%=GetGlobalResourceObject("resource", "Two")%></option>
								<option value="3">
									<%=GetGlobalResourceObject("resource", "Three")%></option>
								<option value="4">
									<%=GetGlobalResourceObject("resource", "Four")%></option>
							</select>
						</td>
					</tr>
					<tr>
						<td style="width: 120px;">
							<div id="DivRow1">
							</div>
						</td>
						<td style="width: 120px;">
							<div id="DivWidth1">
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td style="height: 100px;">
				<table>
					<tr>
						<td colspan="2">
							<%=GetGlobalResourceObject("resource", "ModuleCount")%><select id="Row2" onchange="AddDivRow(2,this.value)">
								<option value="">
									<%=GetGlobalResourceObject("resource", "Select")%></option>
								<option value="1">
									<%=GetGlobalResourceObject("resource", "One")%></option>
								<option value="2">
									<%=GetGlobalResourceObject("resource", "Two")%></option>
								<option value="3">
									<%=GetGlobalResourceObject("resource", "Three")%></option>
								<option value="4">
									<%=GetGlobalResourceObject("resource", "Four")%></option>
							</select>
						</td>
					</tr>
					<tr>
						<td style="width: 120px;">
							<div id="DivRow2">
							</div>
						</td>
						<td style="width: 120px;">
							<div id="DivWidth2">
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td style="height: 100px;">
				<table>
					<tr>
						<td colspan="2">
							<%=GetGlobalResourceObject("resource", "ModuleCount")%><select id="Row3" onchange="AddDivRow(3,this.value)">
								<option value="">
									<%=GetGlobalResourceObject("resource", "Select")%></option>
								<option value="1">
									<%=GetGlobalResourceObject("resource", "One")%></option>
								<option value="2">
									<%=GetGlobalResourceObject("resource", "Two")%></option>
								<option value="3">
									<%=GetGlobalResourceObject("resource", "Three")%></option>
								<option value="4">
									<%=GetGlobalResourceObject("resource", "Four")%></option>
							</select>
						</td>
					</tr>
					<tr>
						<td style="width: 120px;">
							<div id="DivRow3">
							</div>
						</td>
						<td style="width: 120px;">
							<div id="DivWidth3">
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td style="height: 100px;">
				<table>
					<tr>
						<td colspan="2">
							<%=GetGlobalResourceObject("resource", "ModuleCount")%><select id="Row4" onchange="AddDivRow(4,this.value)">
								<option value="">
									<%=GetGlobalResourceObject("resource", "Select")%></option>
								<option value="1">
									<%=GetGlobalResourceObject("resource", "One")%></option>
								<option value="2">
									<%=GetGlobalResourceObject("resource", "Two")%></option>
								<option value="3">
									<%=GetGlobalResourceObject("resource", "Three")%></option>
								<option value="4">
									<%=GetGlobalResourceObject("resource", "Four")%></option>
							</select>
						</td>
					</tr>
					<tr>
						<td style="width: 120px;">
							<div id="DivRow4">
							</div>
						</td>
						<td style="width: 120px;">
							<div id="DivWidth4">
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td style="height: 100px;">
				<table>
					<tr>
						<td colspan="2">
							<%=GetGlobalResourceObject("resource", "ModuleCount")%><select id="Row5" onchange="AddDivRow(5,this.value)">
								<option value="">
									<%=GetGlobalResourceObject("resource", "Select")%></option>
								<option value="1">
									<%=GetGlobalResourceObject("resource", "One")%></option>
								<option value="2">
									<%=GetGlobalResourceObject("resource", "Two")%></option>
								<option value="3">
									<%=GetGlobalResourceObject("resource", "Three")%></option>
								<option value="4">
									<%=GetGlobalResourceObject("resource", "Four")%></option>
							</select>
						</td>
					</tr>
					<tr>
						<td style="width: 120px;">
							<div id="DivRow5">
							</div>
						</td>
						<td style="width: 120px;">
							<div id="DivWidth5">
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td style="height: 100px;">
				<table>
					<tr>
						<td colspan="2">
							<%=GetGlobalResourceObject("resource", "ModuleCount")%><select id="Row6" onchange="AddDivRow(6,this.value)">
								<option value="">
									<%=GetGlobalResourceObject("resource", "Select")%></option>
								<option value="1">
									<%=GetGlobalResourceObject("resource", "One")%></option>
								<option value="2">
									<%=GetGlobalResourceObject("resource", "Two")%></option>
								<option value="3">
									<%=GetGlobalResourceObject("resource", "Three")%></option>
								<option value="4">
									<%=GetGlobalResourceObject("resource", "Four")%></option>
							</select>
						</td>
					</tr>
					<tr>
						<td style="width: 120px;">
							<div id="DivRow6">
							</div>
						</td>
						<td style="width: 120px;">
							<div id="DivWidth6">
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<input type="button" value="<%=GetGlobalResourceObject("resource", "EditorPreview")%>" onclick="BuildPreview()" />
	<div id="PreViewDiv" style="border: 1 red solid;">
	</div>
	<div id="SaveBTN" style="display: none;">
		<asp:TextBox dir="ltr" TextMode="MultiLine" ID="TemplateHTML" ClientIDMode="Static"
			Width="500px" Height="300px" runat="server"></asp:TextBox><br />
		<asp:Button ID="MySaveBTN" runat="server" Text="<%$ resources: resource, SaveBTNSave%>" OnClick="SaveBTN_Click" /></div>
</asp:Content>
