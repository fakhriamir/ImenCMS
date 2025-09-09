using System; using DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
	public partial class Monitor : System.Web.UI.Page
	{
		public string CurDate = "";
		protected void Page_Load(object sender, EventArgs e)
		{
			CurDate = Tools.Calender.GetCurPersianDate() + " - " + Tools.Calender.GetCurArabicDate() + " - " + Tools.Calender.GetCurEngDate();
		}
	}
}