<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
	<%
	 
		////string filepath = Server.MapPath(@"d:\\database\\samenletters.bak");  
		//// Create New instance of FileInfo class to get the properties of the file being downloaded 
		//System.IO.FileInfo myfile = new System.IO.FileInfo("d:\\database\\samen.zip"); 
		//// Checking if file exists   
		////if (myfile.Exists)    {    
		//    // Clear the content of the response  
		//    Response.ClearContent();
		//    // Add the file name and attachment, which will force the open/cancel/save dialog box to show, to the header 
		//    Response.AddHeader("Content-Disposition", "attachment; filename=d:\\database\\samen.zip");  
		//    // Add the file size into the response header 
		//    Response.AddHeader("Content-Length", myfile.Length.ToString()); 
		//    // Set the ContentType 
		//    Response.ContentType = "application/zip"; 
		//    // Write the file into the response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead) 
		//    Response.TransmitFile(myfile.FullName);  
		//    // End the response
		//    Response.End();  
		////} 
		
 %>
<%--    id<%=Portal.GuestAuth.UserState.GuestUserID().ToString() %><br />
	checkid<%=Portal.GuestAuth.UserState.CheckGuestUserLogin().ToString() %>--%>
   </div>
    </form>
</body>
</html>
