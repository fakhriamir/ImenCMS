<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.Master" AutoEventWireup="true" CodeBehind="NotFound.aspx.cs" Inherits="Portal.NotFound" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
<br /><br />
<br /><br />
<div style="color:Red;width:100%;font-size:16px;text-align:center;">
<img src="/Images/PageNotFound.png" /><br />
<%if(Request.QueryString["ID"] == "1")
  { %>
 
<%}
  else if(Request.QueryString["ID"] == "2")
  { %>
 

<%}
   else if(Request.QueryString["ID"] == "3")
  { %>

Your IP Address is Block for 10 Min.
<%}
  else
  { %>

 
<% } %>
</div>
</asp:Content>
