<%@ Page Title="" EnableViewState="false" Language="C#" MasterPageFile="~/View1.Master" AutoEventWireup="true" CodeBehind="Default1.aspx.cs" Inherits="Portal.Default_1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MyHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MyBody" runat="server">
    <%if (-6 != 3)
      {%>
    <table style="width: 100%" border="0" cellpadding="3" cellspacing="0">
        <tr>
            <td valign="top" colspan="3" align="center">
                <asp:PlaceHolder ID="DL12" EnableViewState="False" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <%if (-6 == 0)
          { %>
        <tr>
            <td>
                <asp:PlaceHolder ID="DL21" EnableViewState="False" runat="server"></asp:PlaceHolder>
            </td>
            <td>
                <asp:PlaceHolder ID="DL22" EnableViewState="False" runat="server"></asp:PlaceHolder>
            </td>
            <td>
                <asp:PlaceHolder ID="DL23" EnableViewState="False" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <%}
          else if (-6 == 1)
          {%>
        <tr>
            <td colspan="2" align="center">
                <asp:PlaceHolder ID="DL21A1" EnableViewState="False" runat="server"></asp:PlaceHolder>
            </td>
            <td align="center">
                <asp:PlaceHolder ID="DL23A1" EnableViewState="False" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <%}
          else if (-6 == 2)
          {%>
        <tr>
            <td valign="top" align="center">
                <asp:PlaceHolder ID="DL21A2" EnableViewState="False" runat="server"></asp:PlaceHolder>
            </td>
            <td colspan="2" align="center">
                <asp:PlaceHolder ID="DL22A2" EnableViewState="False" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <%}%>
        <tr>
            <td valign="top" align="center" style="width: <%=DAL.CheckData.GetUnitSetting("DefRight",Tools.Tools.GetViewUnitID)%>">
                <asp:PlaceHolder ID="DL311" EnableViewState="False" runat="server"></asp:PlaceHolder>
            </td>
            <td valign="top" align="center" style="width: <%=DAL.CheckData.GetUnitSetting("DefCenter",Tools.Tools.GetViewUnitID)%>">
                <asp:PlaceHolder ID="DL321" EnableViewState="False" runat="server"></asp:PlaceHolder>
            </td>
            <td valign="top" align="center" style="width: <%=DAL.CheckData.GetUnitSetting("DefLeft",Tools.Tools.GetViewUnitID)%>">
                <asp:PlaceHolder ID="DL331" EnableViewState="False" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
    </table>
    <%}
      else if (-6 == 3)
      {%>
    <table border="0" cellspacing="1" style="width: 100%" align="center">
        <tr>
            <td colspan="4" align="center" valign="middle">
                <asp:PlaceHolder ID="D3_12" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="1" valign="top">
                <asp:PlaceHolder ID="D3_22" runat="server"></asp:PlaceHolder>
            </td>
            <td align="center" colspan="2" valign="top">
                <asp:PlaceHolder ID="D3_315" runat="server"></asp:PlaceHolder>
            </td>
            <td align="center" rowspan="3" valign="top" style="background: url(/images/bgleft.gif) repeat-x">
                <asp:PlaceHolder ID="D3_336" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td align="center" style="width: <%=DAL.CheckData.GetUnitSetting("DefRight",Tools.Tools.GetViewUnitID)%>"
                valign="top">
                <asp:PlaceHolder ID="D3_311" runat="server"></asp:PlaceHolder>
            </td>
            <td style="width: <%=DAL.CheckData.GetUnitSetting("DefCenter",Tools.Tools.GetViewUnitID)%>"
                align="center" valign="top">
                <asp:PlaceHolder ID="D3_321" runat="server"></asp:PlaceHolder>
            </td>
            <td align="center" style="width: <%=DAL.CheckData.GetUnitSetting("DefLeft",Tools.Tools.GetViewUnitID)%>"
                valign="top">
                <asp:PlaceHolder ID="D3_331" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <asp:PlaceHolder ID="D3_41" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
    </table>
	<%} if (-6 <0)
	{%>
	<asp:PlaceHolder ID="CustomPH" runat="server"></asp:PlaceHolder>
	<%} %>
</asp:Content>