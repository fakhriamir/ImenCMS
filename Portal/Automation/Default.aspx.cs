using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Automation
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Tools.Automation.CheckAccess("automation_Default.aspx");
		

			StatePH.Controls.Add(LoadControl("/Def/AutomationState.ascx"));
			StatePH.Controls.Add(LoadControl("/Def/Elders.ascx"));
			EldersPH.Controls.Add(LoadControl("/Def/Calender.ascx"));
		}
	}
}