<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Monitor.aspx.cs" Inherits="Portal.Monitor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title><%=HttpContext.GetGlobalResourceObject("resource", "SamenCooperative").ToString()%></title>
	<script src="/Scripts/jquery-1.4.2.js" type="text/javascript"></script>
	<script src="/Scripts/jquery-fonteffect-1.0.0.min.js" type="text/javascript"></script>
	<script src="/Scripts/jquery.marquee.js" type="text/javascript"></script>
	<style type="text/css">
		body
		{
			background-image: url("/Images/samen.jpg");
			background-position: inherit;
			background-repeat: no-repeat;
			margin: 0px 0px 0px 0px;
		}
		#Container
		{
			width: 800px;
			height: 600px;
		}
		#CurTime
		{
			font-family: B Titr;
			font-size: 30px;
			position: relative;
			top: 40px;
			color: #871218;
		}
		#DateDiv
		{
			position: absolute;
			top: 95px;
			width: 800px;
			font-family: b Titr;
			font-size: 30px;
		}
		#ArzDiv
		{
			position: absolute;
			top: 550px;
			width: 800px;
			font-family: B Traffic;
			font-size: 30px;
			color: Green;
		}
		#TextDiv
		{
			position: absolute;
			top: 170px;
			width: 800px;
			font-family: B farnaz;
			font-size: 60px;
			color: #E8A7C8;
			text-align: center;
			left: 0px;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div id="Container">
		<div id="CurTime">
		</div>
		<div id="DateDiv" class="demo">
			<marquee behavior="scroll" scrollamount="2" direction="right" width="800"><%=CurDate %></marquee>
		</div>
		<div id="TextDiv">
			میلاد مسعود منجی عالم بشریت حضرت صاحب الزمان عج بر تمامی مسلمانان جهان مبارک باد<br />
			تعاونی اعتبار ثامن الائمه
		</div>
		<div id="ArzDiv" class="demo">
			<marquee behavior="scroll" scrollamount="2" direction="right" width="800">دلار آمریکا 11250 ریال - ریال عربستان 2500 ریال - یورو 17950 ریال - روبل 5660 ریال  - ین 15890 ریال - سکه تمام بهار آزادی 3564000 ریال - نیم سکه بهار آزادی 1580000 ریال - ریع سکه بهار آزادی 900000 ریال - هر گرم طلا در بازار ایران 350000 ریال</marquee>
		</div>
	</div>
	</form>
	<script language="javascript" type="text/javascript">
		Clock();
		self.setInterval("Clock()", 60000);
		function Clock() {
		
			var now = new Date();
			var hour = now.getHours();
			var minute = now.getMinutes();
			var second = now.getSeconds();
			document.getElementById("CurTime").innerHTML = hour + ":" + minute;// + ":" + second;
		}
		$("#DateDiv").FontEffect({

			outline: false, // Apply the outline effect
			outlineColor1: "",    // [find contrasting] The upper left  outline color
			outlineColor2: "",    // [outlineColor1] the lower right outline color
			outlineWeight: 1,     // 1=light,2=normal,3=bold
			mirror: false, // Apply the mirror effect
			mirrorColor: "",     // [object color] The color of the reflex
			mirrorOffset: -10,   // The distance from text
			mirrorHeight: 80,    // The height of the reflex (perc.)*
			mirrorDetail: 1,     // The reflex detail 1=high,2=medium,3=low
			mirrorTLength: 50,    // The length of the sfumature (perc.)*
			mirrorTStart: 0.2,   // The starting opacity of the reflex (0-1)
			shadow: true, // Apply the shadow effect
			shadowColor: "#000", // The color of the shadow
			shadowOffsetTop: 5,     // The top offset position (px)
			shadowOffsetLeft: 5,     // The left offset position (px)
			shadowBlur: 1,     // The shadow blur 1=none,2=low,3=high
			shadowOpacity: 0.1,   // The opacity of the shadow (0=none,1=all)
			gradient: false, // Apply the gradient effect
			gradientColor: "",    // The color of the gradient
			gradientPosition: 20,    // The start position of the gradient (perc.)*
			gradientLength: 50,    // The length of the gradient (perc.)*
			gradientSteps: 20,    // the steps of the gradient
			hideText: false // Hide the source text
		})
		$("#ArzDiv").FontEffect({
			outline: true,
			outlineColor1: "#00c",
			outlineColor2: "#00c",
			outlineWeight: 1,
			shadow: true,
			shadowColor: "#ccf",
			shadowOffsetTop: 4,
			shadowOffsetLeft: 4,
			shadowBlur: 2,
			shadowOpacity: 0.05,
			gradient: true,
			gradientColor: "#ccf"
		})

		$("#TextDiv").FontEffect({
		
			gradient: true,
			gradientColor:"#521031",
			shadow: true,
			shadowColor: "#ccf",
			shadowOffsetTop: 4,
			shadowOffsetLeft: 4,
			shadowBlur: 2,
			shadowOpacity: 0.05,
		
		})
		$('div.demo marquee').marquee();
	</script>
</body>
</html>
