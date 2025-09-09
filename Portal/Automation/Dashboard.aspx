<%@ Page Title="" Language="C#" MasterPageFile="~/Automation/Automation.master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Portal.Automation.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AutomationBody" runat="server">
	<div runat="server" visible="false" id="WorkDiv">
        <fieldset>
            <legend>انتخاب پوشه</legend>
            <asp:DropDownList ID="WorkDL" runat="server" DataValueField="OfficeWorkFolderID" DataTextField="name" AutoPostBack="true" OnSelectedIndexChanged="WorkDL_SelectedIndexChanged" />
            <img style="width: 20px; height: 20px; cursor:pointer;" src="Images/edit.png" onclick="SelectPrepMsg('/Automation/workFolder.aspx',520,490);"
                alt="ویرایش لیست" />
        </fieldset>        
	</div>
	<%if (ViewDR.Items.Count != 0)
   {%>
	<div id="tblLetters">
        <table dir="<%=GetGlobalResourceObject("resource", "_Dir")%>" id="MyDashboard" align="center" cellspacing="0"
		cellpadding="5"  border="0"  width="1100px">
		<tr class="RowHead">
            <th>شماره ارجاع
            </th>
            <th>شماره نامه
            </th>
            <th>موضوع نامه
            </th>
            <th>نوع نامه
            </th>
            <th>فرستنده
            </th>
            <th>تاریخ ارسال
            </th>
            <th>پی نوشت
            </th>
            <th>ارجحیت
            </th>
            <th>اقدام نامه</th>
            <th>ارجاع
            </th>
            <th>ارجاعات</th>
            <th>حذف</th>
            <th>انتقال به </th>
		</tr>
		<asp:Repeater runat="server" ID="ViewDR" OnItemCommand="ViewDR_ItemCommand">
			<ItemTemplate>
				<tr id="TR<%# Eval("OfficeReferenceID").ToString().Trim()%>">
					<td align="center" name="<%#Eval("OfficeLetterID").ToString().Trim() %>" refid="<%# Eval("OfficeReferenceID").ToString().Trim()%>">&nbsp;<%# Eval("OfficeReferenceID").ToString().Trim()%></td>
					<td align="center" >&nbsp;<%#Tools.Automation.GetLetterNo(Eval("OfficeLetterID").ToString().Trim()) %></td>
					<td align="center">&nbsp;<%# Eval("OfficeLetterSubjectName").ToString().Trim()%></td>
					<td align="center">&nbsp;<%# Eval("OfficeLetterTypeName").ToString().Trim()%></td>
					<td align="center">&nbsp;<%# Eval("Name").ToString().Trim()%>&nbsp;<%# Eval("Family").ToString().Trim()%></td>
					<td align="center">&nbsp;<%# Tools.Calender.MyPDateTime( Eval("Date").ToString().Trim())%></td>
					<td align="center">&nbsp;<%# Eval("Paraph").ToString().Trim()%></td>
					<td align="center">&nbsp;<%# Eval("OfficePriorityName").ToString().Trim()%></td>

					<td align="center" style="display:none;">
						<asp:ImageButton ImageUrl="Images/edit.png" Width="18px" Height="18px" CommandArgument='<%# Eval("OfficeLetterID").ToString().Trim() %>'
							CommandName="EDIT" ID="EditBTN" ToolTip="<%$ resources: resource, Edit %>" AlternateText="<%$ resources: resource, Edit %>"
							runat="server" />
						
					</td>
					<td align="center"> <img style="width: 20px; height: 20px; cursor:pointer;" src="Images/Action.png" onclick="SelectPrepMsg('/Automation/LetterAction-<%# Eval("OfficeLetterID").ToString().Trim() %>.aspx',520,490);"
                        alt="اقدامات نامه" /></td>

                    <td align="center">
                        <img style="width: 20px; height:20px;cursor:pointer;" src="Images/Reference.png" onclick="SelectPrepMsg('/Automation/Reference-<%# Eval("OfficeLetterID") %>.aspx',520,490);"
                            alt="ارجاع" />
                    </td>
					<td align="center">
                        <img style="width:20px; height:20px;cursor:pointer;" src="Images/References.png" onclick="SelectPrepMsg('/Automation/referenceView-<%# Eval("OfficeLetterID") %>.aspx',650,500);"
                            alt="ارجاعات" />
                    </td>
                    <td><asp:ImageButton OnClientClick="return confirm_delete();" ImageUrl="Images/delete.png" Width="20px" Height="20px"
							CommandArgument='<%# Eval("OfficeLetterID").ToString().Trim() %>' CommandName="DEL" ID="DelBTN" ToolTip="<%$ resources: resource, Del %>"
							AlternateText="<%$ resources: resource, Del %>" runat="server" />
					</td>
					<td><div id="MoveLetterDiv">
						<select id="MoveLetter<%#Eval("OfficeReferenceID").ToString().Trim() %>" >
							<option value="#">انتخاب</option>
							<option value="0">کارتابل اصلی</option>
							<option value="1">کارتابل پیگیری</option>
							<option value="2">اقدام و خاتمه کار</option>
							<option value="3">خاتمه کار</option>
							<%=Tools.Automation.GetMoveLetterOption() %>
						</select><input class="MoveLetterBTN" onclick="SetLetterType(<%#Eval("OfficeReferenceID").ToString().Trim() %>)" type="button" value="اجرا" /></div>
					<img alt="در حال انجام دستور" id='LoadIMG<%# Eval("OfficeReferenceID").ToString().Trim()%>' src="/Images/Loadading_S.gif" style="display: none" /></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
        </div>
	<table border="0" align="center">
		<tr>
			<td>
				<asp:LinkButton ID="lnkPreviousPage" runat="server" OnClick="lnkPreviousPage_Click" Text="<%$ resources: resource, Previous%>"></asp:LinkButton>
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
				<asp:LinkButton ID="lnkNextPage" runat="server" OnClick="lnkNextPage_Click" Text="<%$ resources: resource, Next%>"></asp:LinkButton>
			</td>
		</tr>
	</table>
	<!--End Data-->
	<br />
	<%}%>
</asp:Content>
