using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Automation
{
	public partial class Automation : System.Web.UI.MasterPage
	{
		public string RefCNT, RefFollowCNT;

		protected void Page_Load(object sender, EventArgs e)
		{
			UserMenu.Visible = true;
			RefCNT = DAL.ExecuteData.CNTDataStr("SELECT count(*) FROM OfficeReference INNER JOIN OfficePriority ON OfficeReference.OfficePriorityID = OfficePriority.OfficePriorityID INNER JOIN OfficeLetter ON OfficeReference.OfficeLetterID = OfficeLetter.OfficeLetterID INNER JOIN OfficeLetterSubject ON OfficeLetter.OfficeLetterSubjectID = OfficeLetterSubject.OfficeLetterSubjectID INNER JOIN OfficeLetterType ON OfficeLetter.OfficeLetterTypeID = OfficeLetterType.OfficeLetterTypeID INNER JOIN GuestInfo ON OfficeReference.SenderPersonalID = GuestInfo.GuestID INNER JOIN OfficeReferenceType ON OfficeReference.OfficeReferenceTypeID = OfficeReferenceType.OfficeReferenceTypeID WHERE (OfficeReference.ToPersonalID = " + DAL.CheckData.CheckTokenGuestUserID() + ") AND (OfficeReference.Type =0)");
			RefFollowCNT = DAL.ExecuteData.CNTDataStr("SELECT count(*) FROM OfficeReference INNER JOIN OfficePriority ON OfficeReference.OfficePriorityID = OfficePriority.OfficePriorityID INNER JOIN OfficeLetter ON OfficeReference.OfficeLetterID = OfficeLetter.OfficeLetterID INNER JOIN OfficeLetterSubject ON OfficeLetter.OfficeLetterSubjectID = OfficeLetterSubject.OfficeLetterSubjectID INNER JOIN OfficeLetterType ON OfficeLetter.OfficeLetterTypeID = OfficeLetterType.OfficeLetterTypeID INNER JOIN GuestInfo ON OfficeReference.SenderPersonalID = GuestInfo.GuestID INNER JOIN OfficeReferenceType ON OfficeReference.OfficeReferenceTypeID = OfficeReferenceType.OfficeReferenceTypeID WHERE (OfficeReference.ToPersonalID = " + DAL.CheckData.CheckTokenGuestUserID() + ") AND (OfficeReference.Type =1)");


		}
	}
}