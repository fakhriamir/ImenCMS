using System; using DAL;
using System.Data.SqlClient;
using System.Web;
using System.Text.RegularExpressions;
namespace Portal
{
    public partial class Google : System.Web.UI.Page
    {
		string SiteURL = "";

		protected void Page_Load(object sender, EventArgs e)
        {
			SiteURL = HttpContext.Current.Request.Url.Scheme + Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Host;//.ToLower();

			Tools.Tools.SetPageHit(this.Page.ToString(),this.Page.ClientQueryString);
			if (Request.QueryString["ID"] == null || Request.QueryString["ID"] == "")
			{
				//Response.Write(SiteURL);
				Response.Write(" <?xml version=\"1.0\" encoding=\"UTF-8\"?><sitemapindex xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
				Response.Write("<sitemap><loc>" + SiteURL +LangSTR+ "/google-News</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod></sitemap>");
				Response.Write("<sitemap><loc>" + SiteURL + LangSTR + "/google-Contacts</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod></sitemap>");
				Response.Write("<sitemap><loc>" + SiteURL + LangSTR + "/google-Article</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod></sitemap>");
				//Response.Write("<sitemap><loc>" + SiteURL + LangSTR + "/google-Gallery</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod></sitemap>");
				//Response.Write("<sitemap><loc>" + SiteURL + LangSTR + "/google-Match</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod></sitemap>");
				Response.Write("<sitemap><loc>" + SiteURL + LangSTR + "/google-Movie</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod></sitemap>");
				Response.Write("<sitemap><loc>" + SiteURL + LangSTR + "/google-Personal</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod></sitemap>");
				Response.Write("<sitemap><loc>" + SiteURL + LangSTR + "/google-Product</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod></sitemap>");
				Response.Write("<sitemap><loc>" + SiteURL + LangSTR + "/google-ProductCategory</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod></sitemap>");
				Response.Write("<sitemap><loc>" + SiteURL + LangSTR + "/google-Soft</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod></sitemap>");
				Response.Write("<sitemap><loc>" + SiteURL + LangSTR + "/google-Sound</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod></sitemap>");
				Response.Write("<sitemap><loc>" + SiteURL + LangSTR + "/google-Texts</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod></sitemap>");
				if (LangSTR == "")
				{
					Response.Write("<sitemap><loc>" + SiteURL + "/google-Calender</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod></sitemap>");
					Response.Write("<sitemap><loc>" + SiteURL + "/google-Tip</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod></sitemap>");
				}
				Response.Write("</sitemapindex>");
			}
			else
			{
				string SiteUrl = Request.QueryString["ID"].ToLower();
				string TitleSelect = "name";
				if (SiteUrl == "googlenews")
				{
					GoogleNews();
					return;
				}
				if (SiteUrl == "articlepage")
				{
					ArticlePage();
					return;
				}
				if (SiteUrl == "productpage")
				{
					ProductPage();
					return;
				}
				if (SiteUrl == "newspage")
				{
					NewsPage();
					return;
				}
				if (SiteUrl == "softwarepage")
				{
					SoftwarePage();
					return;
				}
				if (SiteUrl == "shoppage")
				{
					shopPage();
					return;
				}
				if (SiteUrl == "googlemovie")
				{
					GoogleMovie();
					return;
				}
				if (SiteUrl == "image")
				{
					GoogleGallery();
					return;
				}
				if (SiteUrl == "news" || SiteUrl == "article" || SiteUrl == "calender"||SiteUrl == "companyinfo") 
				{
					TitleSelect = "title";				
				}
				else if (SiteUrl == "match") 
				{
					TitleSelect = "Question";				
				}
				else if (SiteUrl == "contacts")
				{
					TitleSelect = "title";
					SiteUrl = "ContactUs";
				}
				else if (SiteUrl == "personal")
					SiteUrl = "Personals";
				//else if (SiteUrl == "gallery")
				//	SiteUrl = "gallery";
				else if (SiteUrl == "soft")
					SiteUrl = "software";
				else if (SiteUrl == "sound")
					SiteUrl = "Sounds";
				else if (SiteUrl == "texts")
					SiteUrl = "Page";
				else if (SiteUrl == "movie")
					SiteUrl = "movies";
				else if (SiteUrl == "tip")
				{
					SiteUrl = "SMS/TipsView";
					TitleSelect = "text";
				}
				else if (SiteUrl == "product" || SiteUrl == "productcategory")
					SiteUrl = "Shop/" + Request.QueryString["ID"];
				else if (SiteUrl == "sponsor" )
					SiteUrl = "Sponsor/project/";

				int SelectType = 0;
				if (SiteUrl.ToLower() == "calender" || SiteUrl== "SMS/TipsView")
                    SelectType = 1;
				Response.Write(" <?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
				SqlParameterCollection SP = new SqlCommand().Parameters;
				string RID = Request.QueryString["ID"];
				RID = RID.Replace("'","");
				SP.AddWithValue("@TN", Request.QueryString["ID"]);
				SqlDataReader MyRead;
                if(SelectType==0)
                    MyRead = ViewData.MyDR1("select * from " + RID + " where langid="+Tools.Tools.LangID+" and unitid=" +Tools.Tools.GetViewUnitID, SP);
                else
                    MyRead = ViewData.MyDR1("select * from " + RID , SP);
				while (MyRead.Read())
				{
					string Name = Tools.MyCL.MGStr(MyRead, TitleSelect).Trim();
					Response.Write(string.Format("<url><loc>" + SiteURL + LangSTR + "/{2}/{0}/{3}</loc><lastmod>{1}</lastmod><changefreq>never</changefreq><priority>0.8</priority></url> ", Tools.MyCL.MGInt(MyRead, 0), DateTime.Now.ToString("yyyy-MM-dd"), SiteUrl, Tools.Tools.UrlWordReplace(Name)));
				}
				MyRead.Close(); MyRead.Dispose();
				Response.Write("</urlset>");
			}          
        }

		private void SoftwarePage()
		{
			int cnt = DAL.ExecuteData.CNTData("SELECT count(*)  FROM Soft WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=" + Tools.Tools.GetViewUnitID + ")");
			cnt = cnt / 25;
			Response.Write(" <?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"  xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"  xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9  http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\" >");

			for (int i = 1; i < cnt; i++)
			{
				Response.Write("<url><loc>" + SiteURL + LangSTR + "/Software/p" + i + "</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod><changefreq>never</changefreq><priority>0.8</priority></url> ");
			}
			//Response.Write("</urlset>");

			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT SoftTypeID  FROM SoftType WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=" + Tools.Tools.GetViewUnitID + ")", null, true);
			while (MyRead.Read())
			{
				cnt = DAL.ExecuteData.CNTData("SELECT count(*)  FROM Soft WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=" + Tools.Tools.GetViewUnitID + ") and (SoftTypeID =" + Tools.MyCL.MGInt(MyRead, 0) + ")");
				cnt = cnt /25;
				for (int i = 1; i < cnt; i++)
				{
					Response.Write("<url><loc>" + SiteURL + LangSTR + "/Software/type" + Tools.MyCL.MGInt(MyRead, 0) + "/p" + i + "</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod><changefreq>never</changefreq><priority>0.8</priority></url> ");
				}
			}
			MyRead.Close(); MyRead.Dispose();
			Response.Write("</urlset>");
		}
		private void shopPage()
		{
			int cnt = DAL.ExecuteData.CNTData("SELECT count(*)  FROM Product WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=" + Tools.Tools.GetViewUnitID + ")");
			cnt = cnt / 25;
			Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			Response.Write("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"  xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"  xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9  http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\" >");

			for (int i = 1; i < cnt; i++)
			{
				Response.Write("<url><loc>" + SiteURL + LangSTR + "/Shop/Product/p" + i + "</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod><changefreq>never</changefreq><priority>0.8</priority></url> ");
			}
			//Response.Write("</urlset>");

			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT ProductCategoryID  FROM ProductCategory WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=" + Tools.Tools.GetViewUnitID + ")", null, true);
			while (MyRead.Read())
			{
				cnt = DAL.ExecuteData.CNTData("SELECT count(*)  FROM Product WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=" + Tools.Tools.GetViewUnitID + ") and (ProductCategoryID like '%," + Tools.MyCL.MGInt(MyRead, 0) + ",%')");
				cnt = cnt / 25;
				for (int i = 1; i < cnt; i++)
				{
					Response.Write("<url><loc>" + SiteURL + LangSTR + "/Shop/ProductCategory/type" + Tools.MyCL.MGInt(MyRead, 0) + "/p" + i + "</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod><changefreq>never</changefreq><priority>0.8</priority></url> ");
				}
			}
			MyRead.Close(); MyRead.Dispose();
			Response.Write("</urlset>");
		}

		private void NewsPage()
		{
			Response.Write(" <?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

			int cnt = DAL.ExecuteData.CNTData("SELECT count(*)  FROM news WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=" + Tools.Tools.GetViewUnitID + ")");
			cnt = cnt / 10;
			for (int i = 1; i < cnt; i++)
			{
				Response.Write("<url><loc>" + SiteURL + LangSTR + "/News/p" + i + "</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod><changefreq>never</changefreq><priority>0.8</priority></url> ");
			}

			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT NewsCategoryID  FROM NewsCategory WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=" + Tools.Tools.GetViewUnitID + ")", null, true);
			while (MyRead.Read())
			{
				cnt = DAL.ExecuteData.CNTData("SELECT count(*)  FROM NewsCategoryItem WHERE (unitid=" + Tools.Tools.GetViewUnitID + ") and (NewsCategoryID =" + Tools.MyCL.MGInt(MyRead, 0) + ")");
				cnt = cnt / 10;
				for (int i = 1; i < cnt; i++)
				{
					Response.Write("<url><loc>" + SiteURL + LangSTR + "/News/Cat" + Tools.MyCL.MGInt(MyRead, 0) + "/p" + i + "</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod><changefreq>weekly</changefreq><priority>0.8</priority></url> ");
				}
			}
			MyRead.Close(); MyRead.Dispose();

			MyRead = DAL.ViewData.MyDR1("SELECT NewsSubjectID  FROM NewsSubject WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=" + Tools.Tools.GetViewUnitID + ")", null, true);
			while (MyRead.Read())
			{
				cnt = DAL.ExecuteData.CNTData("SELECT count(*)  FROM news WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=" + Tools.Tools.GetViewUnitID + ") and (type =" + Tools.MyCL.MGInt(MyRead, 0) + ")");
				cnt = cnt / Tools.Tools.ConvertToInt32(Tools.Tools.GetSetting(439, "25"));
				for (int i = 1; i < cnt; i++)
				{
					Response.Write("<url><loc>" + SiteURL + LangSTR + "/News/type" + Tools.MyCL.MGInt(MyRead, 0) + "/p" + i + "</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod><changefreq>weekly</changefreq><priority>0.8</priority></url> ");
				}
			}
			MyRead.Close(); MyRead.Dispose();

			MyRead = DAL.ViewData.MyDR1("SELECT NewsTagID, Word  FROM NewsTag WHERE (UnitID = " + Tools.Tools.GetViewUnitID + ")");
			while (MyRead.Read())
			{
				Response.Write("<url><loc>" + SiteURL + LangSTR + "/News/tags" + Tools.MyCL.MGInt(MyRead, 0) + "/" + Tools.Tools.UrlWordReplace(Tools.MyCL.MGStr(MyRead, 1)) + "</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod><changefreq>weekly</changefreq><priority>0.8</priority></url> ");
				
			}
			MyRead.Close(); MyRead.Dispose();
			Response.Write("</urlset>");
		}

		private void ProductPage()
		{
			
			Response.Write(" <?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
			
			int cnt = DAL.ExecuteData.CNTData("SELECT count(*)  FROM Product WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=" + Tools.Tools.GetViewUnitID + ")");
			cnt = cnt / Tools.Tools.ConvertToInt32(Tools.Tools.GetSetting(439, "25")); 
			for (int i = 1; i < cnt; i++)
			{
				Response.Write("<url><loc>" + SiteURL + LangSTR + "/Shop/Product/p" + i + "</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod><changefreq>never</changefreq><priority>0.8</priority></url> ");
			}

			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT ProductCategoryID  FROM ProductCategory WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=" + Tools.Tools.GetViewUnitID + ")",null,true);
			while (MyRead.Read())
			{
				cnt = DAL.ExecuteData.CNTData("SELECT count(*)  FROM Product WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=" + Tools.Tools.GetViewUnitID + ") and (ProductCategoryID like ',"+Tools.MyCL.MGInt(MyRead,0)+",')");
				cnt = cnt / 25;
				for (int i = 1; i < cnt; i++)
				{
					Response.Write("<url><loc>" + SiteURL + LangSTR + "/Shop/ProductCategory/type" + Tools.MyCL.MGInt(MyRead, 0) + "/p" + i + "</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod><changefreq>never</changefreq><priority>0.8</priority></url> ");
				}
			}
			MyRead.Close(); MyRead.Dispose();

			MyRead = DAL.ViewData.MyDR1("SELECT ProductSubjectID  FROM ProductSubject WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=" + Tools.Tools.GetViewUnitID + ")", null, true);
			while (MyRead.Read())
			{
				cnt = DAL.ExecuteData.CNTData("SELECT count(*)  FROM Product WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=" + Tools.Tools.GetViewUnitID + ") and (ProductSubjectID =" + Tools.MyCL.MGInt(MyRead, 0) + ")");
				cnt = cnt / Tools.Tools.ConvertToInt32(Tools.Tools.GetSetting(439, "25")); 
				for (int i = 1; i < cnt; i++)
				{
					Response.Write("<url><loc>" + SiteURL + LangSTR + "/Shop/Product/type"+Tools.MyCL.MGInt(MyRead,0)+"/p" + i + "</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod><changefreq>never</changefreq><priority>0.8</priority></url> ");
				}
			}
			MyRead.Close(); MyRead.Dispose();
			Response.Write("</urlset>");
		}

		private void ArticlePage()
		{
			int cnt = DAL.ExecuteData.CNTData("SELECT count(*)  FROM Article WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid="+Tools.Tools.GetViewUnitID+")");
			cnt = cnt / Tools.Tools.ConvertToInt32(Tools.Tools.GetSetting(438, "25"));
			Response.Write(" <?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
			
			for (int i = 1; i < cnt; i++)
			{
				Response.Write("<url><loc>" + SiteURL + LangSTR + "/Article/p" + i + "</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod><changefreq>never</changefreq><priority>0.8</priority></url> ");
			}
			//Response.Write("</urlset>");

			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT ArticleTypeid  FROM ArticleType WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=" + Tools.Tools.GetViewUnitID + ")", null, true);
			while (MyRead.Read())
			{
				cnt = DAL.ExecuteData.CNTData("SELECT count(*)  FROM Article WHERE (LangID = " + Tools.Tools.LangID + ") and (unitid=" + Tools.Tools.GetViewUnitID + ") and (ArticleTypeid =" + Tools.MyCL.MGInt(MyRead, 0) + ")");
				cnt = cnt / Tools.Tools.ConvertToInt32(Tools.Tools.GetSetting(439, "25"));
				for (int i = 1; i < cnt; i++)
				{
					Response.Write("<url><loc>" + SiteURL + LangSTR + "/Article/type" + Tools.MyCL.MGInt(MyRead, 0) + "/p" + i + "</loc><lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod><changefreq>never</changefreq><priority>0.8</priority></url> ");
				}
			}
			MyRead.Close(); MyRead.Dispose();
			Response.Write("</urlset>");
		}

		private void GoogleGallery()
		{
			/*Response.Write(" <?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xmlns:image=\"http://www.google.com/schemas/sitemap-image/1.1\">");
			SqlDataReader MyRead;
			MyRead = ViewData.MyDR1("SELECT Gallery.GalleryTypeID, Gallery.Name, Gallery.[Desc], GalleryType.Name AS typeName FROM Gallery INNER JOIN  GalleryType ON Gallery.GalleryTypeID = GalleryType.GalleryTypeID where Gallery.unitid=" + Tools.Tools.GetViewUnitID + " ORDER BY Gallery.GalleryTypeID DESC");
			int OldTypeID = 0;
			bool First=true;
			while (MyRead.Read())
			{
				if(First)
				{
					Response.Write(" <url>");
					Response.Write("   <loc>" + SiteURL + "/gallery/type" + Tools.MyCL.MGInt(MyRead, 0) + "/" + Tools.Tools.UrlWordReplace(Tools.MyCL.MGStr(MyRead, 3)) + "</loc>");
					First = false;
				}
				else if (OldTypeID != Tools.MyCL.MGInt(MyRead, 0))
				{
					Response.Write(" </url>");
					Response.Write(" <url>");
					Response.Write("   <loc>" + SiteURL + "/gallery/type" + Tools.MyCL.MGInt(MyRead, 0) + "/" +Tools.Tools.UrlWordReplace(Tools.MyCL.MGStr(MyRead, 3)) + "</loc>");

				}
				Response.Write("   <image:image>");
				Response.Write("   		<image:loc>http://example.com/image.jpg</image:loc>");
				Response.Write("   		<image:caption>" + Tools.MyCL.MGStr(MyRead, 3) + "</image:caption>");
				Response.Write("   		<image:title>" + Tools.MyCL.MGStr(MyRead, 2) + "</image:title>");
				Response.Write("   </image:image>");
				OldTypeID = Tools.MyCL.MGInt(MyRead, 0);
			}
			Response.Write(" </url>");
			Response.Write("</urlset>");
			MyRead.Close(); MyRead.Dispose();*/
		}
		private void GoogleMovie()
		{
			Response.Write(" <?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"  xmlns:video=\"http://www.google.com/schemas/sitemap-video/1.1\">");
			SqlDataReader MyRead;
			MyRead = ViewData.MyDR1(" SELECT MovieID, Name, Text, MovAddress, Hit, PicName   FROM Movie where unitid=" + Tools.Tools.GetViewUnitID + " and disable=0 ORDER BY MovieID DESC");
			
			while (MyRead.Read())
			{
				Response.Write(" <url>");
				Response.Write("   <loc>" + SiteURL + "/Movies/" + Tools.MyCL.MGInt(MyRead, 0) + "/" + Tools.Tools.UrlWordReplace(Tools.MyCL.MGStr(MyRead, 1)) + "</loc>");
				Response.Write("   <video:video>");
				Response.Write("		<video:thumbnail_loc>" + SiteURL + "/Files/" + Tools.Tools.GetViewUnitID + "/Images/Movies/"+Tools.MyCL.MGStr(MyRead,5)+"</video:thumbnail_loc> ");
				Response.Write("		<video:title>" + Tools.MyCL.MGStr(MyRead, 1) + "</video:title>");
				Response.Write("		<video:description>" + Regex.Replace(Tools.MyCL.MGStr(MyRead, 2), @"<[^>]*>", String.Empty).Replace("&nbsp;", " ") + "</video:description>");
				if (Tools.MyCL.MGStr(MyRead, 3).IndexOf("http:/")==-1)
					Response.Write("	<video:content_loc>" + SiteURL + "/Files/" + Tools.Tools.GetViewUnitID + "/Movies/" + Tools.MyCL.MGStr(MyRead, 3)+ "</video:content_loc>");
				else
					Response.Write("	<video:content_loc>" + Tools.MyCL.MGStr(MyRead, 3) + "</video:content_loc>");
				Response.Write("  </video:video>");
				Response.Write(" </url>");
			}
			Response.Write("</urlset>");
			MyRead.Close(); MyRead.Dispose();
		}
		string LangSTR
		{
			get
			{
				object LangVal = HttpContext.Current.Request.Cookies["MyLanguage"];
				if (LangVal == null)
					return "";
				string LangStr = HttpContext.Current.Request.Cookies["MyLanguage"].Value.ToLower();
				if (LangStr == "fa-ir")
					return "";
				else if (LangStr == "en-us")
					return "/en-us";
				else if (LangStr == "de-gr")
					return "/de-gr";
				else if (LangStr == "ar")
					return "ar";
				return "";
			}
		}
	
		private void GoogleNews()
		{
			Response.Write(" <?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xmlns:news=\"http://www.google.com/schemas/sitemap-news/0.9\">");
			SqlDataReader MyRead;
			MyRead = ViewData.MyDR1("select NewsID, Title, Chekide, News, Ref, Tar from news where unitid=" + Tools.Tools.GetViewUnitID + " and   (DATEDIFF(mm, Tar, GETDATE()) = 1) order by newsid desc");
			while (MyRead.Read())
			{
				Response.Write(" <url>");
				Response.Write("   <loc>" + SiteURL + "/News/" + Tools.MyCL.MGInt(MyRead, 0) + "/" + Tools.Tools.UrlWordReplace(Tools.MyCL.MGStr(MyRead, 1)) + "</loc>");
				Response.Write("   <news:news>");
				Response.Write("     <news:publication>");
				Response.Write("       <news:name>Technology</news:name>");
				Response.Write("       <news:language>fa-ir</news:language>");
				Response.Write("     </news:publication>");
				//Response.Write("     <news:access>Subscription</news:access>");
				Response.Write("     <news:genres>PressRelease ,Blog</news:genres>");
				Response.Write("     <news:publication_date>" + Tools.MyCL.MGDTDT(MyRead,5).ToString("yyyy-MM-dd") + "</news:publication_date>");
				Response.Write("     <news:title>" + Tools.MyCL.MGStr(MyRead, 1) + "</news:title>");
				Response.Write("     <news:keywords>" +Tools.Tools.GetKeyWord( Tools.MyCL.MGStr(MyRead, 1)+" "+Tools.MyCL.MGStr(MyRead, 2))+ "</news:keywords>");
				Response.Write(" 	<news:geo_locations>Tehran, Iran</news:geo_locations>");
				Response.Write("   </news:news>");
				Response.Write(" </url>");
			}
			Response.Write("</urlset>");
			MyRead.Close();MyRead.Dispose();
		}
    }
}
