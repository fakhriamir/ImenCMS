<%@ Page Title="" Language="C#" MasterPageFile="~/Automation/Automation.master" AutoEventWireup="true" CodeBehind="NewLetter.aspx.cs" Inherits="Portal.Automation.NewLetter" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AutomationBody" runat="server">
	<script language="javascript" type="text/javascript">	
		$(document).ready(function () {
			$('#<%=LetterTemplateDL.ClientID%>').change(function () {
				var idx = $("#<%=LetterTemplateDL.ClientID%>").val();
				GetAjaxVal("/Ajax.aspx?MyType=M" + idx, 9);
				document.getElementById("LoadIMG").style.display = "";
			});
		});

		function LetterTemplateCallBack(MyTemplate) {
			document.getElementById("LoadIMG").style.display = "none";
			//alert(MyTemplate);
			MyTemplate = MyTemplate.replace("","");
			MyTemplate = MyTemplate.replace("##Date##","<%=Tools.Calender.MyPDate()%>");
			var Reciv =$("#<%=RecieverDL.ClientID%>").find('option:selected').text();
			if(Reciv.indexOf("-")==-1 || Reciv=="")
			{
				MyTemplate = MyTemplate.replace("##Name##","");
				MyTemplate = MyTemplate.replace("##Occupation##","");
			}
			MyTemplate = MyTemplate.replace("##Name##",Reciv.substring(Reciv.indexOf("-")+1));
			MyTemplate = MyTemplate.replace("##Occupation##",Reciv.substring(0,Reciv.indexOf("-")));
		
			var Signer =$("#<%=SignerDL.ClientID%>").find('option:selected').text();
			if(Signer.indexOf("-")==-1 || Signer=="")
			{
				MyTemplate = MyTemplate.replace("##SignerName##","");
				MyTemplate = MyTemplate.replace("##SignerOccupation##","");
			}
			MyTemplate = MyTemplate.replace("##SignerName##",Signer.substring(Signer.indexOf("-")+1));
			MyTemplate = MyTemplate.replace("##SignerOccupation##",Signer.substring(0,Signer.indexOf("-")));
			MyTemplate = MyTemplate.replace("##Sign##","");
		
			CKEDITOR.instances.<%=BodyTB.ClientID%>.setData(MyTemplate);
			CKEDITOR.instances.<%=BodyTB.ClientID%>.updateElement();
			//document.getElementById("<%=BodyTB.ClientID%>").value = MyTemplate;
		}
	</script>
    
	<fieldset style="width: 700px; margin:0 auto ;">
		<legend style="font-weight: bold;">صدور نامه</legend>

		<table dir="rtl" align="center" class="TableColor" width="700px" style="overflow-x:scroll;">
			<tr>
				<td>ارجحیت</td>
				<td>
					<asp:DropDownList ID="PeriorityDL" runat="server" DataTextField="name" DataValueField="OfficePriorityID">
					</asp:DropDownList>
					*
				</td>
			</tr>

			<tr>
				<td>طبقه بندی</td>
				<td>
					<asp:DropDownList ID="ClassDL" runat="server" DataTextField="name" DataValueField="OfficeClassificationID">
					</asp:DropDownList>
				</td>
			</tr>

			<tr>
				<td>نوع سند</td>
				<td>
					<asp:DropDownList ID="LetterTypeDL" runat="server" DataTextField="name" DataValueField="OfficeLetterTypeID">
					</asp:DropDownList>
				</td>
			</tr>

			<tr>
				<td>تهیه کننده</td>
				<td>
					<asp:TextBox ID="SupplierTB" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>فرستنده</td>
				<td>
					<asp:DropDownList ID="SenderDL" runat="server" />
				</td>
			</tr>
			<tr>
				<td>امضا کننده</td>
				<td>
					<asp:DropDownList ID="SignerDL" runat="server">
					</asp:DropDownList>
				</td>
			</tr>

			<tr>
				<td>گیرندگان</td>
				<td>
					<asp:DropDownList ID="RecieverDL" runat="server">
					</asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td>موضوع</td>
				<td>
					<asp:DropDownList ID="SubjectDL" runat="server" DataTextField="name" DataValueField="OfficeLetterSubjectID">
					</asp:DropDownList>
					*
				</td>
			</tr>
			<tr>
				<td>توضیحات</td>
				<td>
					<asp:TextBox ID="DescTB" runat="server" MaxLength="32" Height="76px" TextMode="MultiLine" Width="466px" /></td>
			</tr>
			<tr>
				<td>انتخاب الگو:</td>
				<td>
					<asp:DropDownList ID="LetterTemplateDL" runat="server" DataValueField="OfficeLetterTemplateID" DataTextField="name"></asp:DropDownList>
					<img alt="در حال انجام دستور" id='LoadIMG' src="/Images/Loadading_S.gif" style="display: none" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;تصویر نامه
					<asp:FileUpload ID="FileFU" runat="server" />
				</td>
			</tr>
			<tr>
				<td colspan="2">متن نامه<CKEditor:CKEditorControl FormatSource="false" ID="BodyTB" runat="server" Width="800px">
					</CKEditor:CKEditorControl>
				</td>
			</tr>
			<tr>
				<td colspan="2" align="center">
					<asp:Button ID="SaveBTN" runat="server" Text="ثبت نامه" OnClick="SaveBTN_Click" />
				</td>
			</tr>
		</table>
	</fieldset>
 
	<%if (ViewDR.Items.Count != 0)
   {%><br />
	<fieldset style="width: 100%;">
		<legend style="font-weight: bold;">نامه های صادره</legend>

	<table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="My" align="center" cellspacing="0"
		cellpadding="2" width="80%" border="0">
		<tr class="RowHead" style="border-bottom: 3px solid #333;">
			<th >شماره نامه
			</th>
			<th >گیرنده
			</th>
			<th >موضوع نامه
			</th>
			<th >تاریخ ارسال
			</th>
			<th >پیوست نامه
			</th>
			<th >ارجاع
			</th>
			<th >مشاهده ارجاعات</th>
			<th >امضاء 
			</th>
			<th >ویرایش
			</th>
			<th >حذف
			</th>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr class="<%# Container.ItemIndex % 2 == 0 ? "RowItem" : "RowAlter" %>">
					<td align="center">
						<%#Tools.Automation.GetLetterNo( Eval("OfficeLetterID").ToString().Trim())%></td>
					<td align="center">
						<%# Eval("ReciverName").ToString().Trim()%></td>
					<td align="center">
						<%# Eval("name").ToString().Trim()%></td>
					<td><%#Tools.Calender.MyPDate(Eval("Date").ToString() )%></td>
					<td align="center">
						<img style="width: 20px; height: 20px; cursor: pointer;" src="Images/Attach.png" onclick="SelectPrepMsg('/Automation/LettersFile-<%# Eval("OfficeLetterID") %>.aspx',520,490);"
							alt="پیوست نامه" />
					</td>
					<td align="center">
						<img style="width: 20px; height: 20px; cursor: pointer;" src="Images/Reference.png" onclick="SelectPrepMsg('/Automation/Reference-<%# Eval("OfficeLetterID") %>.aspx',520,490);"
							alt="ارجاع" />
					</td>
					<td align="center">
						<img style="width: 20px; height: 20px; cursor: pointer;" src="Images/References.png" onclick="SelectPrepMsg('/Automation/referenceView-<%# Eval("OfficeLetterID") %>.aspx',650,500);"
							alt="ارجاعات" />
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="Images/sign.png" Width="18px" Height="18px" CommandArgument='<%# Eval("OfficeLetterID").ToString().Trim() %>'
							CommandName="Sign" ID="ImageButton1" ToolTip="امضای نامه" AlternateText="امضای نامه"
							runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton ImageUrl="Images/Edit.png" Width="18px" Height="18px" CommandArgument='<%# Eval("OfficeLetterID").ToString().Trim() %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="ویرایش  نامه" AlternateText="ویرایش"
							runat="server" />
					</td>
					<td align="center">
						<asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Images/delete.png" Width="20px" Height="20px"
							CommandArgument='<%# Eval("OfficeLetterID").ToString().Trim() %>' CommandName="DEL" ID="DelBTN" ToolTip="حذف"
							AlternateText="حذف" runat="server" />
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
	<table border="0" align="center">
		<tr>
			<td>
				<asp:LinkButton ID="lnkPreviousPage" runat="server" OnClick="lnkPreviousPage_Click" Text="قبلی"></asp:LinkButton>
				&nbsp;
				<asp:Repeater ID="rptPages" runat="server" OnItemCommand="rptPages_ItemCommand" OnItemCreated="rptPages_ItemCreated">
					<ItemTemplate>
						<asp:LinkButton ID="btnPage" CommandName="Page" CommandArgument='<%#Container.DataItem %>' runat="server"><%# Container.DataItem %></asp:LinkButton>
					</ItemTemplate>
					<SeparatorTemplate>
						&nbsp;-&nbsp;
					</SeparatorTemplate>
				</asp:Repeater>
				&nbsp;
				<asp:LinkButton ID="lnkNextPage" runat="server" OnClick="lnkNextPage_Click" Text="بعدی"></asp:LinkButton>
			</td>
		</tr>
	</table>
	<!--End Data-->
		</fieldset>
	<br />
	<%}%>
</asp:Content>
