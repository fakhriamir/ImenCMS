using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsService
{
	public partial class test : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Searcharoo.Indexer.Spider mm = new Searcharoo.Indexer.Spider();
			mm.BuildCatalog(new Uri("http://www.rasekhoon.net"));
		}
	}
}