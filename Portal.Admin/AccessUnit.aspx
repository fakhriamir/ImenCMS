<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true"
	CodeBehind="AccessUnit.aspx.cs" Inherits="Portal.Admin.AccessUnit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script src="/scripts/jquery.tree.js" type="text/javascript"></script>
	<link rel="Stylesheet" href="scripts/tree.css" type="text/css" />
	<script type="text/javascript">
		function SetIdToTextBox() {
			document.getElementById("CheckItemTB").value = $("#tree").getCheckedNodes();
			return true;
		}
		eval("<%=GenScript%>");
		function createNode() {
			var root = {
				"id": "M0",
				"text": "<%=GetGlobalResourceObject("resource", "TreeRoot")%>",
				"value": "M0",
				"showcheck": true,
				complete: true,
				"isexpand": true,
				"checkstate": 0,
				"hasChildren": true
			};
			var arr = [];
			for (var i = 0; i < RootItem.length; i++) {
				var subarr = [];
				for (var j = 0; j < 60; j++) {
					var CurChild = ChildItem[i].toString().substr(0, ChildItem[i].indexOf('$'));
					ChildItem[i] = ChildItem[i].toString().substr(ChildItem[i].indexOf('$') + 1);
					if (CurChild != "") {
						var curCBState = CurChild.substr(0, 1);
						CurChild = CurChild.substr(1);
						subarr.push({
							"id": CurChild.substr(0, CurChild.indexOf('#')),
							"text": CurChild.substr(CurChild.indexOf('#') + 1),
							"value": CurChild.substr(0, CurChild.indexOf('#')),
							"showcheck": true,
							complete: true,
							"isexpand": false,
							"checkstate": curCBState,
							"hasChildren": false
						});
					}
				}
				var CBState = RootItem[i].toString().substr(0, 1);
				RootItem[i] = RootItem[i].toString().substr(1);
				arr.push({
					"id": "M" + RootItem[i].toString().substr(0, RootItem[i].indexOf('#')),
					"text": RootItem[i].toString().substr(RootItem[i].indexOf('#') + 1),
					"value": "M" + RootItem[i].toString().substr(0, RootItem[i].indexOf('#')),
					"showcheck": true,
					complete: true,
					"isexpand": false,
					"checkstate": CBState,
					"hasChildren": true,
					"ChildNodes": subarr
				});
			}
			root["ChildNodes"] = arr;
			return root;
		}
		treedata = [createNode()];
		var userAgent = window.navigator.userAgent.toLowerCase();
		//.browser.msie8 = $.browser.msie && /msie 8\.0/i.test(userAgent);
		//$.browser.msie7 = $.browser.msie && /msie 7\.0/i.test(userAgent);
	//	$.browser.msie6 = !$.browser.msie8 && !$.browser.msie7 && $.browser.msie && /msie 6\.0/i.test(userAgent);
		function load() {
			var o = { showcheck: true
				//onnodeclick:function(item){alert(item.text);},        
			};
			o.data = treedata;
			$("#tree").treeview(o);
			$("#showchecked").click(function (e) {
				var s = $("#tree").getCheckedNodes();
				if (s != null)
					alert(s.join(","));
				else
					alert("NULL");
			});
			$("#showcurrent").click(function (e) {
				var s = $("#tree").getCurrentNode();
				if (s != null)
					alert(s.text);
				else
					alert("NULL");
			});
		}
		//if ($.browser.msie6) {
		//	load();
		//}
	//	else {
			$(document).ready(load);
		//}
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<table class="TableColor" dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" cellspacing="0" cellpadding="1" width="70%" align="center">
		<tr>
			<td class="RowHead" colspan="2">
				<%=GetGlobalResourceObject("resource", "AccessUnitPT")%>
			</td>
		</tr>
		<tr>
			<td>
				<%=GetGlobalResourceObject("resource", "Portal")%>
			</td>
			<td>
				<asp:DropDownList ID="UnitDL" runat="server" DataTextField="name" DataValueField="unitid"
					AutoPostBack="true" OnSelectedIndexChanged="UnitDL_SelectedIndexChanged">
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<div style="text-align: right; border-bottom: #c3daf9 1px solid; border-left: #c3daf9 1px solid;
					width: 350px; border-top: #c3daf9 1px solid; border-right: #c3daf9 1px solid;">
					<div id="tree">
					</div>
				</div>
			</td>
		</tr>
		<tr>
			<td align="center" colspan="2">
				<span style="display: none">
					<asp:TextBox ID="CheckItemTB" ClientIDMode="Static" runat="server"></asp:TextBox></span>
				<asp:Button ID="SaveBTN" OnClientClick="return SetIdToTextBox();" runat="server"
					Text="<%$ resources: resource, SaveBTNSave%>" OnClick="SaveBTN_Click" />
			</td>
		</tr>
	</table>
</asp:Content>
