using System.ComponentModel;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using Tools;

namespace NewsService
{
	[WebService(Namespace = "NewsService")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	public class S_P_W_News : System.Web.Services.WebService
	{
		[WebMethod]
		public DataTable GetNewsDetail(string TerminalID,int NewsID)
		{
			if (!GetUserAuthenticate(TerminalID))
				return null;
			DataTable MyDT = new DataTable("Reference");
			MyDT.Columns.Add("NewsID", typeof(System.Int32));
			MyDT.Columns.Add("ID", typeof(System.Int32));
			MyDT.Columns.Add("Title", typeof(System.String));
			MyDT.Columns.Add("Lead", typeof(System.String));
			MyDT.Columns.Add("Matn", typeof(System.String));
			MyDT.Columns.Add("Link", typeof(System.String));
			MyDT.Columns.Add("NewsRefID", typeof(System.Int32));
			MyDT.Columns.Add("Ref", typeof(System.String));
			MyDT.Columns.Add("DT", typeof(System.String));
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@NewsID", NewsID);

			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT NewsID, ID, Title, Lead, Matn, Link, NewsRefID, Ref, DT, Type  FROM         News WHERE     (NewsID = @NewsID)", SP, "MyNewsConnectionStr");
			while (MyRead.Read())
			{
				DataRow dr = MyDT.NewRow();
				dr["NewsID"] = Tools.MyCL.MGInt(MyRead, 0);
				dr["ID"] = Tools.MyCL.MGInt(MyRead, 1);
				dr["Title"] = Tools.MyCL.MGStr(MyRead,2);
				dr["Lead"] = Tools.MyCL.MGStr(MyRead, 3);
				dr["Matn"] = Tools.MyCL.MGStr(MyRead, 4);
				dr["Link"] = Tools.MyCL.MGStr(MyRead, 5);
				dr["NewsRefID"] = Tools.MyCL.MGInt(MyRead, 6);
				dr["Ref"] = Tools.MyCL.MGStr(MyRead, 7);
				dr["DT"] = Tools.MyCL.MGStr(MyRead, 8);
				MyDT.Rows.Add(dr);
			}
			MyRead.Close(); MyRead.Dispose();
			return MyDT;
		}
		[WebMethod]
		public DataTable GetNewsPrint(string TerminalID, string NewsID)
		{
			if (!GetUserAuthenticate(TerminalID))
				return null;
			DataTable MyDT = new DataTable("NewsPrint");
			MyDT.Columns.Add("NewsID", typeof(System.Int32));
			MyDT.Columns.Add("ID", typeof(System.Int32));
			MyDT.Columns.Add("Title", typeof(System.String));
			MyDT.Columns.Add("Lead", typeof(System.String));
			MyDT.Columns.Add("Matn", typeof(System.String));
			MyDT.Columns.Add("Link", typeof(System.String));
			MyDT.Columns.Add("NewsRefID", typeof(System.Int32));
			MyDT.Columns.Add("Ref", typeof(System.String));
			MyDT.Columns.Add("DT", typeof(System.String));
			if(NewsID.IndexOf("'") !=-1)
				return null;
			//SqlParameterCollection SP = new SqlCommand().Parameters;
			//SP.AddWithValue("@NewsID", NewsID);

			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT NewsID, ID, Title, Lead, Matn, Link, NewsRefID, Ref, DT, Type  FROM News WHERE NewsID in (" + NewsID + ") order by id desc", null, "MyNewsConnectionStr");
			while (MyRead.Read())
			{
				DataRow dr = MyDT.NewRow();
				dr["NewsID"] = Tools.MyCL.MGInt(MyRead, 0);
				dr["ID"] = Tools.MyCL.MGInt(MyRead, 1);
				dr["Title"] = Tools.MyCL.MGStr(MyRead, 2);
				dr["Lead"] = Tools.MyCL.MGStr(MyRead, 3);
				dr["Matn"] = Tools.MyCL.MGStr(MyRead, 4);
				dr["Link"] = Tools.MyCL.MGStr(MyRead, 5);
				dr["NewsRefID"] = Tools.MyCL.MGInt(MyRead, 6);
				dr["Ref"] = Tools.MyCL.MGStr(MyRead, 7);
				dr["DT"] = Tools.MyCL.MGStr(MyRead, 8);
				MyDT.Rows.Add(dr);
			}
			MyRead.Close(); MyRead.Dispose();
			return MyDT;
		}
		[WebMethod]
		public DataTable GetNewsCategory(string TerminalID)
		{
			if (!GetUserAuthenticate(TerminalID))
				return null;
			DataTable MyDT = new DataTable("Category");
			MyDT.Columns.Add("CategoryID", typeof(System.Int32));
			MyDT.Columns.Add("Name", typeof(System.String));
			MyDT.Columns.Add("Lev", typeof(System.String));

			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT CategoryID, Name, [Level], UnitID, Sort, LangID  FROM Category ORDER BY [Level]", null, "MyNewsConnectionStr");
			while (MyRead.Read())
			{
				DataRow dr = MyDT.NewRow();
				dr["CategoryID"] = Tools.MyCL.MGInt(MyRead, 0);
				dr["Name"] = Tools.MyCL.MGStr(MyRead, 1);
				dr["Lev"] = Tools.MyCL.MGStr(MyRead, 2);
				MyDT.Rows.Add(dr);
			}
			MyRead.Close(); MyRead.Dispose();
			return MyDT;
		}
		[WebMethod]
		public DataTable GetReference(string TerminalID)
		{
			if (!GetUserAuthenticate(TerminalID))
				return null;
			DataTable MyDT = new DataTable("Reference");
			MyDT.Columns.Add("NewsRefID", typeof(System.Int32));
			MyDT.Columns.Add("Ref", typeof(System.String));
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT NewsRefID, Ref FROM NewsRef ORDER BY Ref", null, "MyNewsConnectionStr");
			while (MyRead.Read())
			{
				DataRow dr = MyDT.NewRow();
				dr["NewsRefID"] = Tools.MyCL.MGInt(MyRead,0);
				dr["Ref"] = Tools.MyCL.MGStr(MyRead,1);
				MyDT.Rows.Add(dr);
			}
			MyRead.Close(); MyRead.Dispose();
			return MyDT;
		}
		[WebMethod]
		public DataTable SearchNews(string TerminalID, string NotReferenceID, string FilterWords)
		{
			/*date = YYYY/MM/DD*/
			if (!GetUserAuthenticate(TerminalID))
				return null;
			
			DataTable MyDT = new DataTable("NewsTable");
			MyDT.Columns.Add("Newsid", typeof(System.Int32));
			MyDT.Columns.Add("title", typeof(System.String));
			MyDT.Columns.Add("Ref", typeof(System.String));
			FilterWords = FilterWords.Replace("'", "");
			string MyFilter="";
			if (NotReferenceID.Trim() == "")
				MyFilter = " and (CONTAINS(Title, '%" + FilterWords + "%')) OR (CONTAINS(Matn,'%" + FilterWords + "%')) "; 
			else
				MyFilter = " and (NewsRefID NOT IN (" + FilterWords + ")) AND (CONTAINS(Title ,'%" + FilterWords + "%')) OR (NewsRefID NOT IN (" + NotReferenceID + ")) AND (CONTAINS(matn, N'%" + FilterWords + "%')) "; 
			SqlDataReader MyRead = DAL.ViewData.MyDR1("select Newsid,title,ref from News where type=0 "+FilterWords+"  order by ID desc", null, "MyNewsConnectionStr");
			while (MyRead.Read())
			{				
					DataRow dr = MyDT.NewRow();
					dr["Newsid"] = Tools.MyCL.MGInt(MyRead, 0);
					dr["title"] = Tools.MyCL.MGStr(MyRead, 1);
					dr["Ref"] = Tools.MyCL.MGStr(MyRead, 2);
					MyDT.Rows.Add(dr);
			}
			return MyDT;
		}
		[WebMethod]
		public DataTable GetLastNewsTitle(string TerminalID,string NewsDate,string ReferenceID,int PostCount,object[] FilterWords,int CategoryID)
		{
			/*date = YYYY/MM/DD*/
			if (!GetUserAuthenticate(TerminalID))
				return null;
			if(NewsDate.IndexOf("'") !=-1 || ReferenceID.IndexOf("'") !=-1)
				return null;
			string Filter = FilterNews(NewsDate,ReferenceID, PostCount);
			DataTable MyDT = new DataTable("NewsTable");
			MyDT.Columns.Add("Newsid", typeof(System.Int32));
			MyDT.Columns.Add("title", typeof(System.String));
			MyDT.Columns.Add("Ref", typeof(System.String));
			MyDT.Columns.Add("AutoCategoryID", typeof(System.Int32));
			if(CategoryID!=0)
			{
				SqlParameterCollection SP = new SqlCommand().Parameters;
				SP.AddWithValue("@CategoryID",CategoryID);
				string IDs = ","+CategoryID;
				string Lel = DAL.ExecuteData.CNTDataStr("SELECT [Level] FROM Category  WHERE (CategoryID =@CategoryID)", SP);
				SqlDataReader MyCat = DAL.ViewData.MyDR1("SELECT CategoryID FROM Category WHERE ([Level] LIKE '"+Lel+"%') ");
				while (MyCat.Read())
				{
					IDs += "," + Tools.MyCL.MGInt(MyCat, 0);
				}
				MyCat.Close(); MyCat.Dispose();
				Filter += " and AutoCategoryID in (" + IDs.Substring(1) + ")";  
			}
			string[] MyItems = GetItems(FilterWords);
			SqlDataReader MyRead = DAL.ViewData.MyDR1("select top " + PostCount + " Newsid,title,ref,AutoCategoryID from News where type=0 " + Filter + " order by ID desc", null, "MyNewsConnectionStr");
			while (MyRead.Read())
			{
				if (MyItems != null)
				{
					if (MyCheck(MyItems, Tools.MyCL.MGStr(MyRead, 1)))
					{
						DataRow dr = MyDT.NewRow();
						dr["Newsid"] = Tools.MyCL.MGInt(MyRead, 0);
						dr["title"] = Tools.MyCL.MGStr(MyRead, 1);
						dr["Ref"] = Tools.MyCL.MGStr(MyRead, 2);
						dr["AutoCategoryID"] = Tools.MyCL.MGInt(MyRead, 3);
						MyDT.Rows.Add(dr);
					}
				}
				else
				{
					DataRow dr = MyDT.NewRow();
					dr["Newsid"] = Tools.MyCL.MGInt(MyRead, 0);
					dr["title"] = Tools.MyCL.MGStr(MyRead, 1);
					dr["Ref"] = Tools.MyCL.MGStr(MyRead, 2);
					dr["AutoCategoryID"] = Tools.MyCL.MGInt(MyRead, 3);
					MyDT.Rows.Add(dr);
				}
			}
			return MyDT;
		}
		string[] GetItems(object[] FilterWords)
		{
			string[] MyItems = null;
			if (FilterWords.Length <= 0 )
				return MyItems;
			MyItems = new string[FilterWords.Length];
			for (int i = 0; i < FilterWords.Length; i++)
			{
				if (FilterWords[i].ToString().IndexOf("'") == -1 && FilterWords[i].ToString().Trim()!="")
					MyItems[i] = FilterWords[i].ToString().Replace("^", " ");
			}
			return MyItems;
		}
		private bool GetUserAuthenticate(string TerminallID)
		{
			if (TerminallID == "awqaf-skjdhnudnunhuihmiscdhsdpimsdcomi")
				return true;
			return false;
		}
		private string FilterNews(string NewsDate, string ReferenceID, int PostCount)
		{
			string MyFilter = "";
			if (ReferenceID.Trim() != "")
				MyFilter = " AND (NewsRefID IN (" + ReferenceID + ")) ";
			if (NewsDate!=null && NewsDate.Trim()!="")
			{
				//if (MyFilter == "")
				//    MyFilter = " where tar = N'"+NewsDate+"' ";
				//else
				MyFilter += " and tar = N'" + NewsDate + "' ";
			}
			//if (FilterWords != null)//word filter
			//{
			//    string WF = "";
			//    for (int i = 0; i < FilterWords.Length; i++)
			//    {
			//        WF += " or title like N'" + FilterWords[i] + "' ";
			//    }
			//    if (WF != "")
			//        WF = WF.Substring(3);
			//    //if (MyFilter == "")
			//    //    MyFilter = " where "+WF;
			//    //else
			//    if (WF != "")
			//        MyFilter += " and (" + WF + ") ";
			//}
			return MyFilter;
		}
		bool MyCheck(string[] MyItem, string Tit)
		{
			if (MyItem == null)
				return false;
			Tit = MyClass.RepWord(Tit);
			for (int i = 0; i < MyItem.Length; i++)
			{
				if (Tit.IndexOf(MyItem[i]) != -1)
					return true;
			}
			return false;
		}
	}
}