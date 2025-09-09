using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
	public partial class Frame : System.Web.UI.Page
	{
		public string MyAddress = "";
		protected void Page_Load(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(Request.QueryString["ID"]))
				Response.Redirect("Default.aspx");
			else
				MyAddress ="http://"+Request.QueryString["ID"];
		}
	}
}