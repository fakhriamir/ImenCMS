$(document).ready(function () {
    if (typeof String.prototype.trim !== 'function') {
        String.prototype.trim = function () {
            return this.replace(/^\s+|\s+$/g, '');
        }
    }
    $(".Myaccordion h3:first").addClass("active");
    $(".Myaccordion ul:not(:first)").hide();

    $(".Myaccordion h3").click(function () {
        $(this).next("ul").slideToggle("100")
		.siblings("ul:visible").slideUp("100");
        $(this).toggleClass("active");
        $(this).siblings("h3").removeClass("active");
    });

});
function OpenDialog(PageName) {
	//$('#dialog-modal-teb').title = "تتتتتتتتتتتتت";
	$('#dialog-modal-teb').dialog({ autoOpen: false, modal: true, width: 650, height: 550, bgiframe: true, resizable: true });
	$('#dialog-modal-teb').dialog('open');
	$('#dialog-modal-teb').html(unescape(PageName));
	return false;
}
function SelectPrepMsg(PageName) {
	//$('#dialog-modal-teb').title = "تتتتتتتتتتتتت";
	$("#modalIFrameTeb").attr('src', "about:blank");
	$('#dialog-modal-teb').dialog({ autoOpen: false, modal: true, width: 650, height: 550, bgiframe: true, resizable: true });
	$('#dialog-modal-teb').dialog('open');
	//var src = PageName + '.aspx';
	$("#modalIFrameTeb").attr('src', PageName);
	return false;
}
function SelectPrepMsg(PageName, wid) {
	//$('#dialog-modal-teb').title = "تتتتتتتتتتتتت";
	$('#dialog-modal-teb').dialog({ autoOpen: false, modal: true, width: wid, height: 550, bgiframe: true, resizable: true });
	$('#dialog-modal-teb').dialog('open');
	//var src = PageName + '.aspx';
	$("#modalIFrameTeb").attr('src', PageName);
	return false;
}
function SelectPrepMsg(PageName, wid, hei) {
	//$('#dialog-modal-teb').title = "تتتتتتتتتتتتت";
	$('#dialog-modal-teb').dialog({ autoOpen: false, modal: true, width: wid, height: hei, bgiframe: true, resizable: true });
	$('#dialog-modal-teb').dialog('open');
	//var src = PageName + '.aspx';
	$("#modalIFrameTeb").attr('src', PageName);
	return false;
}

 var Link = "";
var Seed = null;
function GTL(Link, Condition, Name, Target) {
	var TheForm = document.createElement("FORM");
	TheForm.id = "MyPostForm";
	TheForm.name = "MyPostForm";
	TheForm.action = Link;
	TheForm.method = "POST";
	TheForm.style.position = "absolute";
	//    if (event != null) {
	//        if (event.shiftKey)
	//            TheForm.target = Name;
	//    }
	//    if (Target != null)
	//        TheForm.target = Target;
	document.body.appendChild(TheForm);

	//    var Value;
	//    if (Name != null)
	//        Value = Name;
	//    else {
	//        Value = event.srcElement.id;
	//        Value = Value.replace("Grid", "");
	//        Value = Value.replace("Table", "");
	//       }

	var e = document.createElement("INPUT");
	if (Name != null) {
		e.name = "Name";
		e.value = Name;
		e.type = "hidden";
		TheForm.appendChild(e)
	}
	var f = document.createElement("INPUT");
	if (Condition != null) {
		Condition = Condition.replace("$", "'");
		Condition = Condition.replace("$", "'");
		f.name = "Condition";
		f.value = Condition;
		f.type = "hidden";
		TheForm.appendChild(f)
	}
	TheForm.submit();
}
function confirm_delete() {
	if (confirm("Are You sure to delete this row?") == true)
		return true;
	else
		return false;
}
function CityChange(shahrid) {
	document.getElementById("ctl00_Body_CityTB").value = document.getElementById("ctl00_Body_ShahrDL").value;
}


function OstanChange(OstanID) {
	// alert('<%=Portal.Admin.Ajax.GetCity("1")%>');

	var DL = document.getElementById("ctl00_Body_ShahrDL");
	clearOptions(DL);

	var optn = document.createElement("OPTION");
	optn.text = "Pleas Wait ..";
	optn.value = "-1";
	DL.options.add(optn);
	AjaxGetVal(OstanID, '', 1);
}
function OstanFill(CityStr) {
	if (CityStr == "")
		return;
	var DL = document.getElementById("ctl00_Body_ShahrDL");
	var ostanTXT = CityStr;
	// alert(ostanTXT);
	//  return;
	clearOptions(DL);
	while (ostanTXT.length != 0) {
		var endL = ostanTXT.indexOf("&");
		var tmpit = ostanTXT.substr(0, endL);
		ostanTXT = ostanTXT.substr(endL + 1);

		var optn = document.createElement("OPTION");
		optn.text = tmpit.substr(tmpit.indexOf('#') + 1);
		optn.value = tmpit.substr(0, tmpit.indexOf('#'));
		DL.options.add(optn);
	}
	CityChange(document.getElementById("ctl00_Body_ShahrDL").value)
}
//alert("aaaa");
var objXMLHTTP = null;
var objXMLHTTPpop = null;
function AjaxGetVal(Myval, stat, MyType) {
	//alert("aa");

	if (window.XMLHttpRequest) { // Mozilla, Safari, ...
		objXMLHTTP = new XMLHttpRequest();
		objXMLHTTPpop = new XMLHttpRequest();
	}
	if (window.ActiveXObject) { // IE
		try {
			objXMLHTTP = new ActiveXObject("Msxml2.XMLHTTP");
			objXMLHTTPpop = new ActiveXObject("Msxml2.XMLHTTP");
		}
		catch (e) {
			try {
				objXMLHTTP = new ActiveXObject("Microsoft.XMLHTTP");
				objXMLHTTPpop = new ActiveXObject("Microsoft.XMLHTTP");
			}
			catch (e1) {
				objXMLHTTP = null;
				objXMLHTTPpop = null;
			}
		}
	}
	// document.getElementById('loadBox').style.display = '';
	var strOutput;

	strOutput = 'Error';
	objXMLHTTP.open("GET", 'ajax.aspx?MyVal=' + Myval + '&MyDiv=' + stat + '&MyType=' + MyType);
	objXMLHTTP.onreadystatechange = AjacBack;
	objXMLHTTP.send(null);
}
function AjacBack() {
	//alert("bb");
	var strout = "";
	if (objXMLHTTP.readyState == 4 && objXMLHTTP.status == 200) {
		strOutput = objXMLHTTP.responseText;

		OstanFill(strOutput);
		// document.getElementById('loadBox').style.display = 'none';
		// SC();
	}
}
function clearOptions(list) {
	var i;
	for (i = list.options.length - 1; i >= 0; i--) {
		list.remove(i);
	}

}
function MyTrim(Str) {
	var i = 0;
	while ((Str.charAt(i) == " " || Str.charAt(i) == "‌") && i < Str.length) i++;
	if (i == Str.length) return "";
	Str = Str.substr(i);
	i = Str.length - 1;
	while ((Str.charAt(i) == " " || Str.charAt(i) == "‌") && i >= 0) i--;
	if (i == -1) return Str;
	Str = Str.substr(0, i + 1);
	return Str;
}
function GetLParams() {
	if (document.getElementById("MyLoading") != undefined) {
		document.getElementById("MyLoading").style.display = '';
	}

	var PreStr = "$#PB";
	//var PreStr="";
	var PStr = "";

	var Pass = MyTrim(document.getElementById("bepass").value.toLowerCase());
	while (Pass != Pass.replace("&#1740;", "ي"))
		Pass = Pass.replace("&#1740;", "ي");

	var UserN = document.getElementById("beuser").value;
	while (UserN != UserN.replace("&#1740;", "ي"))
		UserN = UserN.replace("&#1740;", "ي");
	PStr += PreStr + "User=" + UserN;
	PStr += PreStr + "Pass=" + hex_md5(hex_md5(Pass) + Seed);
	PStr += PreStr;
	return PStr;
}
function confirmdelete(formname, nothingmessage, confirmmessage) {
	//Count selected checkboxes
	var formControls = document.formdelete.elements;
	var c = 0;
	for (var i = 0; i < formControls.length; i++)
		if (formControls[i].type.toLowerCase() == 'checkbox' && formControls[i].checked)
			c++;
	//If no checkbox is selected 
	if (c == 0) {
		alert(nothingmessage);
		return false;
	}
	else {
		// entry is marked so show confirm option 
		var agree = confirm(confirmmessage);
		if (agree) {
			//Submit the form
			//return true;
			document.forms[formname].submit();
		}
		else
			return false;
	}
}
function MyNewsEscape(InText) {
	window.location = "News.aspx?MID=" + escape(InText.replace("+","%305%"));
}
