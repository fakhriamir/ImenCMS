<%@ Page Title="" Language="C#" MasterPageFile="~/Pages.master" AutoEventWireup="true" CodeBehind="Map.aspx.cs" Inherits="Portal.Map" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrBeforTitle,0)%>
	<%=HttpContext.GetGlobalResourceObject("resource", "BranchDispersion").ToString()%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterTitle,0)%>
	<div id="branchMap">
<img src="/images/MapIran.jpg" width="446" height="446" border="0" usemap="#Map" />
<map name="Map" id="Map">
  <area shape="poly" coords="65,100,44,94,30,70,24,44,47,43,72,36,81,73,90,92" href="/map-o1.aspx" alt="آذربایجان شرقی" />
  <area shape="poly" coords="101,93,86,85,76,48,72,31,86,23" href="/map-o3.aspx" alt="اردبیل" />
  <area shape="poly" coords="25,121,10,84,2,64,2,26,12,17,28,37,34,87,64,100,66,115,42,110" href="/map-o1.aspx" alt="آذربایجان غربی" />
  <area shape="poly" coords="45,153,28,125,47,113,72,116,83,131,81,152" href="/map-o19.aspx" alt="کردستان" />
  <area shape="poly" coords="70,114,70,102,90,95,109,101,106,113,107,127,94,134" href="/map-o13.aspx" alt="زنجان" />
  <area shape="poly" coords="100,73,115,87,138,99,130,108,110,112" href="/map-o24.aspx" alt="گیلان" />
  <area shape="poly" coords="104,135,125,135,139,121,134,109,112,115" href="/map-o17.aspx" alt="قزوین" />
  <area shape="poly" coords="141,103,165,111,208,105,214,120,176,128,139,115" href="/map-o26.aspx" alt="مازندران" />
  <area shape="poly" coords="144,139,161,126,174,131,192,129,190,137,176,142,168,145,169,153,144,153" href="/map-o7.aspx" alt="تهران" />
  <area shape="poly" coords="142,119,159,126,140,138,131,134" href="/map-o18.aspx"  alt="البرز" />
  <area shape="poly" coords="238,78,268,66,252,95,219,111,218,93" href="/map-o23.aspx" alt="گلستان" />
  <area shape="poly" coords="276,107,261,90,277,65,310,76,311,93,302,105" href="/map-o11.aspx" alt="خراسان شمالی" />
  <area shape="poly" coords="320,75,358,103,380,124,366,175,300,177,262,196,278,155,283,109,310,106" href="/map-o10.aspx" alt="خراسان رضوی" />
  <area shape="poly" coords="260,207,301,181,372,181,382,243,351,271,286,233" href="/map-o9.aspx" alt="خراسان جنوبی" />
  <area shape="poly" coords="170,155,260,98,276,120,270,160,262,178,174,167" href="/map-o14.aspx" alt="سمنان" />
  <area shape="poly" coords="35,147,50,161,68,155,77,173,52,183,38,175,25,184,22,161" href="/map-o21.aspx" alt="کرمانشاه" />
  <area shape="poly" coords="82,173,86,135,108,143,107,157,104,181" href="/map-o29.aspx" alt="همدان" />
  <area shape="poly" coords="27,187,54,190,75,213,74,233,44,210" href="/map-o5.aspx" alt="ایلام" />
  <area shape="poly" coords="79,212,60,190,79,179,112,191,122,206,112,217,93,205" href="/map-o25.aspx" alt="لرستان" />
  <area shape="poly" coords="124,195,108,182,112,142,146,142,129,160,134,177,146,185" href="/map-o27.aspx" alt="مرکزی" />
  <area shape="poly" coords="83,286,72,240,87,213,114,222,129,249,121,264,132,281" href="/map-o12.aspx" alt="خوزستان" />
  <area shape="poly" coords="122,217,146,226,157,261,134,253,122,231" href="/map-o8.aspx" alt="چهارمحال و بختیاری" />
  <area shape="poly" coords="123,205,168,169,257,183,255,201,194,225,187,249,160,256,155,227" href="/map-o4.aspx" alt="اصفهان" />
  <area shape="poly" coords="140,294,126,265,134,255,163,267,166,280,149,281" href="/map-o22.aspx" alt="کهکیلویه و بویر احمد" />
  <area shape="poly" coords="130,289,160,312,182,354,159,349" href="/map-o6.aspx" alt="بوشهر" />
  <area shape="poly" coords="171,258,195,266,220,298,252,330,254,352,207,373,186,352,148,287,169,281" href="/map-o16.aspx" alt="فارس" />
  <area shape="poly" coords="211,273,190,252,195,228,257,207,281,231,270,251,243,255,229,275,233,295,224,301" href="/map-o30.aspx" alt="یزد" />
  <area shape="poly" coords="235,303,235,277,274,250,285,237,349,276,345,332,344,381,313,372,298,339" href="/map-o20.aspx" alt="کرمان" />
  <area shape="poly" coords="350,404,348,331,358,268,402,244,402,269,388,287,406,319,433,353,440,371,416,383,412,414" href="/map-o15.aspx" alt="سیستان و بلوچستان" />
  <area shape="poly" coords="254,322,293,340,308,370,342,385,351,411,309,407,296,372,270,372,238,388,211,378,262,353" href="/map-o28.aspx" alt="هرمزگان" />
  <area shape="poly" coords="136,158,166,157,166,168,146,174,131,170" href="/map-o18.aspx" alt="قم" />

</map>
	</div><div id="CityBox">
	<asp:Repeater ID="CityDR" runat="server">
		<ItemTemplate>
			<span id="CityName"><a href="/Map/<%# Eval("city").ToString().Trim() %>/<%# Eval("name").ToString().Trim() %>.aspx">
				<%# Eval("name").ToString().Trim() %></a></span>
		</ItemTemplate>

	</asp:Repeater>
	</div>
	<div id="googleMap" ><asp:PlaceHolder ID="MapPH" runat="server"></asp:PlaceHolder></div>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterBody,0)%>
	<%=Tools.Template.GetCadr(Tools.Template.CadrItem.CadrAfterFooter,0)%>
</asp:Content>
