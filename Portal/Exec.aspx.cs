using System; using DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Portal
{
	public partial class Exec : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Tools.Tools.SetPageHit(this.Page.ToString(),this.Page.ClientQueryString);

		}
		public static string GetControlString(string ASCXPath)
		{

			Page page = new Page();
			UserControl ctl = (UserControl)page.LoadControl(ASCXPath);
			page.Controls.Add(ctl);
			StringWriter writer = new StringWriter();
			HttpContext.Current.Server.Execute(page, writer, true);
			return writer.ToString();
		}
	}
}