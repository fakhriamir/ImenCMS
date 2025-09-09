<%@ Page Title="" EnableViewState="false" Language="C#" MasterPageFile="~/AdminPage.Master"
	AutoEventWireup="true" CodeBehind="PlayMedia.aspx.cs" Inherits="Portal.Admin.PlayMedia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script language="javascript" type="text/javascript" src="/Scripts/swfobject.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<br>
	<br>
	<table style="border: none; width: 450px; height: 100%" cellpadding="0" cellspacing="0"
		border="0" align="center">
		<tr>
			<td>
				<object style="display: <%=MediaPlayer%>" id="VIDEO" width="320" height="240" classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6"
					type="application/x-oleobject">
					<param name="URL" value="<%=FileAddress%>">
					<param name="SendPlayStateChangeEvents" value="True">
					<param name="AutoStart" value="false">
					<param name="PlayCount" value="9999">
				</object>
				<div style="display: <%=JWPlayer%>" id="player1" align="center">
				</div>
				<script type="text/javascript">
					var so = new SWFObject('/Scripts/Myplayer.swf', 'ply1', '320', '<%=MyHeight%>', '9');
					so.addParam('allowfullscreen', 'true');
					so.addParam('allowscriptaccess', 'always');
					so.addParam('flashvars', 'file=<%=FileAddress%>&lightcolor=green');
					so.write('player1');  
				</script>
			</td>
		</tr>
		<tr>
			<td style="height: 8px">
			</td>
		</tr>
	</table>
</asp:Content>
