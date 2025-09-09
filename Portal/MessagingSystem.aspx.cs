using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
	public partial class MessagingSystem : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["SignIn"] != null && Request.QueryString["SignIn"] != "")
				SetSignIn();
		
		}

		private void SetSignIn()
		{

			Response.Write("OK");
		}
	}
}