using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Searcharoo;
using System.Collections;
namespace NewsService.BigData
{
	public partial class Register : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//	Searcharoo.Indexer.Spider ss = new Searcharoo.Indexer.Spider();


		}
		//ArrayList visited, pmd;
		public void parseUrl(string url, UTF8Encoding enc, WebClient browser)
		{
			//if (visited.Contains(url)) { 
			//	// Url already spidered, skip and go to next link 
			//	Response.Write ("<br><font size=-2> "+ url +" already spidered</font>"); 
			//} else { 
			//	// Add this URL to the 'visited' list, so we'll
			//	// skip it if we come across it again 
			//	visited.Add(url); 
			//	string fileContents =
			//	   enc.GetString (browser.DownloadData(url)); // from Listing 1  
			//	// ### Pseudo-code ### 
			//	// 1. Find links in the downloaded page
			//	//      (add to linkLocal ArrayList - code in Listing 2) 
			//	// 2. Extract <TITLE> and <META> Description,
			//	//      Keywords (as Version 1 Listing 4) 
			//	// 3. Remove all HTML and whitespace (as Version 1) 
			//	// 4. Convert words to string array,
			//	//      and add to catalog  (as Version 1 Listing 7) 
			//	// 5. If any links were found, recursively call this page 
			//	if (null != pmd.LocalLinks) 
			//	foreach (object link in pmd.LocalLinks) { 
			//		parseUrl (Convert.ToString(link), enc, browser); 
			//	} 
		}
	}
}