<%@ Page Title="" Language="C#" MasterPageFile="~/Dialog.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="Portal.Archive.View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCP" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BoduCB" runat="server">
	<%if (ViewDR.Items.Count != 0)
   {%>
	
		<asp:Repeater runat="server" ID="ViewDR">
			<ItemTemplate>
				<table>
                   
				    <tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">&nbsp;<%# DataBinder.Eval(Container.DataItem, "ArchiveFileID").ToString().Trim() %></td>
					<td align="center">&nbsp;<%# Eval("Title").ToString().Trim() %></td>					
					</tr>
                    <tr>
                        <td>
                    
                            <a href=' <%# Tools.Archive.GetFileAddress(Eval("FileAddress").ToString().Trim())+Eval("FileAddress").ToString() %>'>مشاهده فایل</a>
                            <%# Tools.Archive.GetFileAddress(Eval("FileAddress").ToString().Trim())+Eval("FileAddress").ToString() %>
                            
                 
                        </td>
                        <td></td>
                    </tr>
				</table>
			</ItemTemplate>
		</asp:Repeater>
	
	<%}%></asp:Content>
