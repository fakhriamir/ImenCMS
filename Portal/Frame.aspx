<%@ Page Title="" Language="C#" MasterPageFile="~/View.Master" AutoEventWireup="true" CodeBehind="Frame.aspx.cs" Inherits="Portal.Frame" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MyHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MyBody" runat="server">
<div style="direction:ltr"><iframe frameborder=0 src="" scrolling="auto" id="MyFrame" name="MyFrame" width="100%" height="1300px" ></iframe></div>
<script language="javascript" type="text/javascript" >
//	$("#MyFrame").load(function () {
//		b = $("#MyFrame").contents().find("body");

//		alert(b);
//		
//	})
//	function Test(a) {
//		alert(a);
//	}
//	//var ID = getQuerystring('id');
	var Addr = "<%=MyAddress%>";
//	//alert(document.getElementById("MyFrame"));
	document.getElementById("MyFrame").src = Addr;
//	//alert(ID);
//	function window_onload() {
//		alert(document.getElementById("MyFrame").My);
//	}

</script>
</asp:Content>
