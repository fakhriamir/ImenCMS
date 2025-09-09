using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tools
{
	using System.Xml;
	public class RssFeader
	{
		public static void AddRSSItem(XmlTextWriter writer, string ItemTitle, string ItemLink)
		{
			writer.WriteStartElement("item");
			writer.WriteElementString("title", ItemTitle);
			writer.WriteElementString("link", ItemLink);
			writer.WriteEndElement();
		}
		public static void AddRSSItem(XmlTextWriter writer, string ItemTitle, string ItemLink, string ItemDescription, string ItemAuthor)
		{
			writer.WriteStartElement("item");
			writer.WriteElementString("title", ItemTitle);
			writer.WriteElementString("link", ItemLink);
			writer.WriteElementString("description", ItemDescription);
			writer.WriteElementString("author", ItemAuthor);
			writer.WriteEndElement();
		}
		public static void AddRSSItem(XmlTextWriter writer, string ItemTitle, string ItemLink, string ItemDescription)
		{
			writer.WriteStartElement("item");
			writer.WriteElementString("title", ItemTitle);
			writer.WriteElementString("link", ItemLink);
			writer.WriteElementString("description", ItemDescription);
			writer.WriteEndElement();
		}
		public static void AddRSSItem(XmlTextWriter writer, string ItemTitle, string ItemLink, string ItemDescription, string ItemAuthor, string ItemPublishDate)
		{
			writer.WriteStartElement("item");
			writer.WriteElementString("title", ItemTitle);
			writer.WriteElementString("link", ItemLink);
			writer.WriteElementString("description", ItemDescription);
			writer.WriteElementString("author", ItemAuthor);
			writer.WriteElementString("publishdate", ItemPublishDate);
			writer.WriteEndElement();
		}
		public static void WriteRSSPrologue(XmlTextWriter writer, string Commnet, string Title, string Link, string Description, string CopyRight, string Generator)
		{
			writer.WriteStartDocument();
			writer.WriteComment(Commnet);
			writer.WriteStartElement("rss");
			writer.WriteAttributeString("version", "2.0");
			writer.WriteStartElement("channel");
			writer.WriteElementString("title", Title);
			writer.WriteElementString("link", Link);
			writer.WriteElementString("description", Description);
			writer.WriteElementString("copyright", CopyRight);
			writer.WriteElementString("generator", Generator);
		}
		public static void WriteRSSPrologue(XmlTextWriter writer, string Commnet, string Title, string Link)
		{
			writer.WriteStartDocument();
			writer.WriteComment(Commnet);
			writer.WriteStartElement("rss");
			writer.WriteAttributeString("version", "2.0");
			writer.WriteStartElement("channel");
			writer.WriteElementString("title", Title);
			writer.WriteElementString("link", Link);
		}

		public static void WriteRSSClosing(XmlTextWriter writer)
		{
			writer.WriteEndElement();
			writer.WriteEndElement();
			writer.WriteEndDocument();
			writer.Flush();
			writer.Close();
		}
	}
}
