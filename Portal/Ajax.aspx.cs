using System;
using DAL;
using System.Data.SqlClient;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections;
using System.Net;
using Tools;

namespace Portal
{
	public partial class Ajax : System.Web.UI.Page
	{
		public static string DNSValidation(string EmailAddress)
		{
			return "true";
		}
		
        protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["MyType"] == null)
				return;
			if (Request.QueryString["MyType"].Length > 3)
			{
				if (Request.QueryString["MyType"] != null && Tools.Tools.GetSubstring(Request.QueryString["MyType"], 0, 4) == "Tele")
				{
					GetTelegramAnswer(Request.QueryString["MyType"].Substring(4));
					return;
				}
				else if (Request.QueryString["MyType"] != null && Tools.Tools.GetSubstring(Request.QueryString["MyType"], 0, 4) == "Teme")
				{
					SendTelegramMessage(Request.QueryString["MyType"].Substring(4));
					return;
				}
				else if (Request.QueryString["MyType"] != null && Tools.Tools.GetSubstring(Request.QueryString["MyType"], 0, 4) == "TeFo")
				{
					SendTelegramForward(Request.QueryString["MyType"].Substring(4));
					return;
				}
				else if (Request.QueryString["MyType"] != null && Request.QueryString["MyType"].Substring(0, 4) == "Comm")
				{
					AddCommand(Request.QueryString["MyType"].Substring(4));
					return;
				}
				else if (Request.QueryString["MyType"].ToLower() == "rate")
				{
					AddRate();
					return;
				}
				else if (Request.QueryString["MyType"].ToLower().Substring(0, 4) == "prrf")
				{
					GetProjectRefrence(Request.QueryString["MyType"].Substring(4));
					return;
				}
				else if (Request.QueryString["MyType"].ToLower().Substring(0, 4) == "cora")
				{
					AddCommandRate(Request.QueryString["MyType"].Substring(4));
					return;
				}
				else if (Request.QueryString["MyType"].ToLower().Substring(0, 4) == "cara")
				{
					AddCampignRate(Request.QueryString["MyType"].Substring(4));
					return;
				}
				else if (Request.QueryString["MyType"].ToLower().Substring(0, 4) == "spli")
				{
					SponsorLike(Request.QueryString["MyType"].Substring(4));
					return;
				}
				else if (Request.QueryString["MyType"].ToLower().Substring(0, 4) == "spem")
				{
					SponsorUserEmail(Request.QueryString["MyType"].Substring(4));
					return;
				}
				else if (Request.QueryString["MyType"].ToLower().Substring(0, 4) == "shop")
				{
					Shoping(Request.QueryString["MyType"].Substring(4));
					return;
				}
                else if (Request.QueryString["MyType"].ToLower().Substring(0, 4) == "adsh")
				{
					ShowFactorDetail(Request.QueryString["MyType"].Substring(4));
					return;
				}
				else if (Request.QueryString["MyType"].ToLower().Substring(0, 4) == "shfa")
				{
					ShowFactorDetailHTML(Request.QueryString["MyType"].Substring(4));
					return;
				}
				else if (Request.QueryString["MyType"].ToLower().Substring(0, 4) == "mape")
                {
                    GetMapItem(Request.QueryString["MyType"].Substring(4));
                    return;
                }
                else if (Request.QueryString["MyType"].ToLower().Substring(0, 4) == "mapd")
                {
                    GetMapDetait(Request.QueryString["MyType"].Substring(4));
                    return;
                }
                else if (Request.QueryString["MyType"].ToLower().Substring(0, 4) == "maps")
                {
                    GetMapSearch(Request.QueryString["MyType"].Substring(4));
                    return;
                }
                else if (Request.QueryString["MyType"].ToLower().Substring(0, 4) == "mapc")
                {
                    GetShahr(Request.QueryString["MyType"].Substring(4));
                    return;
                }
                else if (Request.QueryString["MyType"].ToLower().Substring(0, 4) == "mapi")
                {
                    GetMapSub(Request.QueryString["MyType"].Substring(4));
                    return;
                }
                else if (Request.QueryString["MyType"].ToLower().Substring(0, 4) == "mapa")
                {
                    AddMapItem(Request.QueryString["MyType"].Substring(4));
                    return;
                }
                else if (Request.QueryString["MyType"].ToLower().Substring(0, 4) == "mapr")
                {
                    AddMapReport(Request.QueryString["MyType"].Substring(4));
                    return;
                }
				else if (Request.QueryString["MyType"].ToLower().Substring(0, 4) == "inpo")
                {
                    InstagramGetPosts();
                    return;
                }
				else if (Request.QueryString["MyType"].ToLower().Substring(0, 4) == "infs")
                {
                    InstagramFollowers();
                    return;
                }
            }

			if (Request.QueryString["MyType"] != null && Request.QueryString["MyType"].Substring(0, 1) == "R")
			{
				SetLetterReferenceType(Request.QueryString["MyType"].Substring(1));
				return;
			}
			else if (Request.QueryString["MyType"] != null && Request.QueryString["MyType"].Substring(0, 1) == "N")
			{
				SetProjectReferenceType(Request.QueryString["MyType"].Substring(1));
				return;
			}
			else if (Request.QueryString["MyType"] != null && Request.QueryString["MyType"].Substring(0, 1) == "C")
			{
				GetSMSPhoneCNT(Request.QueryString["MyType"].Substring(1));
				return;
			}
			else if (Request.QueryString["MyType"] != null && Request.QueryString["MyType"].Substring(0, 1) == "A")
			{
				GetSMSPostalPhoneCNT(Request.QueryString["MyType"].Substring(1));
				return;
			}
			else if (Request.QueryString["MyType"] != null && Request.QueryString["MyType"].Substring(0, 1) == "P")
			{
				DelShopCart(Request.QueryString["MyType"].Substring(1));
				return;
			}
			else if (Request.QueryString["MyType"] != null && Request.QueryString["MyType"].Substring(0, 1) == "L")
			{
				CheckAvalibilityLineNo(Request.QueryString["MyType"].Substring(1));
				return;
			}
			else if (Request.QueryString["MyType"] != null && Request.QueryString["MyType"] == "1")
			{
				GetCity(Request.QueryString["MyVal"]);
				return;
			}
			else if (Request.QueryString["MyType"] != null && Request.QueryString["MyType"].Substring(0, 1) == "T")
			{
				GetSMSTreeNode(Request.QueryString["MyType"]); return;
			}
			else if (Request.QueryString["MyType"] != null && Request.QueryString["MyType"].Substring(0, 1) == "M")
			{
				GetOfficeTemplate(Request.QueryString["MyType"].Substring(1));
				return;
			}
			else if (Request.QueryString["MyType"] != null && Request.QueryString["MyType"].Substring(0, 1) == "S")
			{
				SetReciveArchive(Request.QueryString["MyType"]);
				return;
			}
			else if (Request.QueryString["MyType"] != null && Request.QueryString["MyType"].Substring(0, 2) == "FL")//FroumLike 
			{
				ForumLike(Request.QueryString["MyType"].Substring(2));
				return;
			}
		}

        private void ShowFactorDetail(string ProductID)
        {
            int PID = Tools.Tools.ConvertToInt32(ProductID);
            if (PID <= 0)
            {
                Response.Write("Error");
                Response.Flush();
            }
            SqlParameterCollection SP = new SqlCommand().Parameters;
            SP.AddWithValue("@PID", PID);
            int CNT = ExecuteData.CNTData("SELECT count(*) FROM Product WHERE(ProductID = @PID)", SP);
            if(CNT==0)
            {
                Response.Write("Error");
                Response.Flush();
            }
            int Filesale = ExecuteData.CNTData("SELECT ProductSubject.Filesales FROM Product INNER JOIN ProductSubject ON Product.ProductSubjectID = ProductSubject.ProductSubjectID  WHERE(Product.ProductID = @PID)", SP);
            if (Filesale == 0)
                Tools.Shop.AddProToShopCard(null, PID.ToString());
            else if (Filesale == 1)
                Tools.Shop.AddProToDVD(null, PID.ToString());
            Response.Write(Tools.Shop.GetShopingCNT());
            Response.Flush();
        }
		private void ShowFactorDetailHTML(string factorID)
        {
			if (DAL.CheckData.CheckTokenGuestUserID() <= 0)
			{
				Response.Write("Error");
				Response.Flush();
				return;
			}

			int FID = Tools.Tools.ConvertToInt32(factorID);
            if (FID <= 0)
            {
                Response.Write("Error");
                Response.Flush();
				return;
            }
			string ot = "<table class=\"FactorItemView\"> ";//<tr><td></td><td></td></tr>
			SqlParameterCollection SP = new SqlCommand().Parameters;
            SP.AddWithValue("@FID", FID);
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT FactorItem.FactorItemID, FactorItem.ProductID, FactorItem.Total, Product.Name, Product.PicName FROM FactorItem INNER JOIN Product ON FactorItem.ProductID = Product.ProductID WHERE(FactorItem.FactorID = @FID) ORDER BY FactorItem.FactorID",SP);
			while(MyRead.Read())
			{
				ot += "<tr><td><a href=\"/Shop/Product/" + MyCL.MGInt(MyRead, 1) + "/" +Tools.Tools.UrlWordReplace(MyCL.MGStr(MyRead, 3) ) + "\" target=\"_blanck\"><img alt=\"" + MyCL.MGStr(MyRead,3)+ "\" title=\"" + MyCL.MGStr(MyRead, 3) + "\" onerror=\"this.style.display='none';\" src=\""+DAL.CheckData.GetFilesRoot()+"/Images/proPic/th_"+ MyCL.MGStr(MyRead, 4) + "\" /></a></td>"+
					  "<td><h3><a href=\"/Shop/Product/" + MyCL.MGInt(MyRead, 1) + "/" + Tools.Tools.UrlWordReplace(MyCL.MGStr(MyRead, 3)) + "\" target=\"_blanck\">" + MyCL.MGStr(MyRead, 3) + "</a></h3><h4>شناسه فاکتور: " + MyCL.MGInt(MyRead, 0) + "</h4><h4>تعداد: " + MyCL.MGInt(MyRead, 2) + "</h4></td></tr>";
				
			}
			MyRead.Close();
            Response.Write(factorID+"-"+ot+"</table>");
            Response.Flush();
        }

        private void InstagramFollowers()
		{
			
		}
		private void InstagramGetPosts()
		{
			if (Instagram.Instagram.InstagramUserID ==-1)
			{
				Response.Write("10");
				Response.Flush();
				return;
			}				
			WebClient client = new WebClient();
			var response = client.DownloadString("https://api.instagram.com/v1/users/self/media/recent/?access_token="+Tools.Instagram.GetAccessToken(Instagram.Instagram.InstagramUserID) + "");
			Response.Write(response);
			Response.Flush();
		}

		void GetCity(string OstanID)
        {
            if (OstanID == "")
                return;
            string ShahrItem = "";
            SqlParameterCollection SP = new SqlCommand().Parameters;
            SP.AddWithValue("@ID", OstanID);
            SqlDataReader MyRead = ViewData.MyDR1("SELECT ShahrID, Name  FROM Shahr  WHERE (OstanId = @ID)", SP);
            while (MyRead.Read())
                ShahrItem += Tools.MyCL.MGInt(MyRead, 0) + "#" + Tools.MyCL.MGStr(MyRead, 1) + "&";
            //A_ViewData.MyConnection.Close();
            MyRead.Close(); MyRead.Dispose();
            Response.Write(ShahrItem);
        }
        void GetShahr(string OstanID)
        {
            if (OstanID == "")
                return;
            string ShahrItem = "{\"0\":\"انتخاب شهر\",";
            SqlParameterCollection SP = new SqlCommand().Parameters;
            SP.AddWithValue("@ID", OstanID);
            SqlDataReader MyRead = ViewData.MyDR1("SELECT ShahrID, Name  FROM Shahr  WHERE (OstanId = @ID) order by name", SP);
            while (MyRead.Read())
                ShahrItem += "\"" + Tools.MyCL.MGInt(MyRead, 0) + "\":\"" + Tools.MyCL.MGStr(MyRead, 1) + "\",";
            //A_ViewData.MyConnection.Close();
            MyRead.Close(); MyRead.Dispose();
            ShahrItem = "var cd = " + ShahrItem.TrimEnd(',') + "};";
            Response.Write(ShahrItem);
            Response.Flush();
        }
        void GetMapSub(string CatidID)
        {
            if (CatidID == "")
                return;
            string ShahrItem = "{\"0\":\"انتخاب زیرموضوع\",";
            SqlParameterCollection SP = new SqlCommand().Parameters;
            SP.AddWithValue("@ID", CatidID);
            SqlDataReader MyRead = ViewData.MyDR1("SELECT MapCategoryItemID,  Title FROM MapCategoryItem WHERE(MapCategoryID = @ID) ORDER BY Title", SP);
            while (MyRead.Read())
                ShahrItem += "\"" + Tools.MyCL.MGInt(MyRead, 0) + "\":\"" + Tools.MyCL.MGStr(MyRead, 1) + "\",";
            //A_ViewData.MyConnection.Close();
            MyRead.Close(); MyRead.Dispose();
            ShahrItem = "var cc = " + ShahrItem.TrimEnd(',') + "};";
            Response.Write(ShahrItem);
            Response.Flush();
        }
        private void AddMapItem(string Search)
        {
            if (Request.Cookies["MyLanguage"] == null)
            {
                Response.Write("10");
                Response.Flush();
                return;
            }
            string[] MyPos = Regex.Split(Search, "{A}");
            if(MyPos.Length<13 || MyPos.Length > 13)
            {
                Response.Write("10");
                Response.Flush();
                return;
            }
            string Token = MyPos[11];
            Token = Tools.Tools.MyDecry(Tools.Tools.ConvertFromBase64(Token));
            DateTime MyDT = Convert.ToDateTime(Token);
            int cp = DateTime.Compare(DateTime.Now, MyDT);
            if (cp > 15)
            {
                Response.Write("10");
                Response.Flush();
                return;
            }
            if (MyPos[0] == null || MyPos[0] == "")
            {
                Response.Write("10");
                Response.Flush();
                return;
            }
     /*       AddName   0
AddSubCatSel    1
AddModir    2
AddEmail    3   
AddWeb  4
AddTel  5
AddFax  6
SubShahrS el   7
AddAddress  8
AddLat  9
AddLon  10*/
            SqlParameterCollection SP = new SqlCommand().Parameters;
            SP.AddWithValue("@ShahrID", MyPos[7]);
            SP.AddWithValue("@OstanID", DAL.ExecuteData.CNTData("SELECT OstanId  FROM Shahr WHERE (ShahrID = @ShahrID)",SP));
            SP.AddWithValue("@Name", MyPos[0]);
            SP.AddWithValue("@MapCategoryItemID", MyPos[1]);
            SP.AddWithValue("@Modir", MyPos[2]);
            SP.AddWithValue("@Email", MyPos[3]);
            SP.AddWithValue("@WebSite", MyPos[4]);
            SP.AddWithValue("@Tel", MyPos[5]);
            SP.AddWithValue("@Fax", MyPos[6]);
            SP.AddWithValue("@lat",MyPos[9]);
            SP.AddWithValue("@lon",MyPos[10]);
            SP.AddWithValue("@Address", MyPos[8]);
            SP.AddWithValue("@Type", 11);            
            SP.AddWithValue("@GuestID", DAL.CheckData.CheckTokenGuestUserID());
            if (MyPos[12]!="")
            {
                SP.AddWithValue("@MapItemID", MyPos[12]);
                DAL.ExecuteData.ExecData("INSERT INTO MapItemEdit (Name, OstanID, ShahrID, MapCategoryItemID, Modir, Email, WebSite, Tel, Fax, lat, lon, Geo, Address, Type, GuestID,MapItemID) VALUES(@Name, @OstanID, @ShahrID, @MapCategoryItemID, @Modir, @Email, @WebSite, @Tel, @Fax, @lat, @lon, geography::STGeomFromText('POINT(" + float.Parse(MyPos[9]) + " " + float.Parse(MyPos[10]) + ")', 4326), @Address, @Type, @GuestID,@MapItemID)", SP);
            }
            else
                DAL.ExecuteData.ExecData("INSERT INTO MapItem (Name, OstanID, ShahrID, MapCategoryItemID, Modir, Email, WebSite, Tel, Fax, lat, lon, Geo, Address, Type, GuestID) VALUES(@Name, @OstanID, @ShahrID, @MapCategoryItemID, @Modir, @Email, @WebSite, @Tel, @Fax, @lat, @lon, geography::STGeomFromText('POINT("+float.Parse( MyPos[9]) + " "+ float.Parse(MyPos[10]) + ")', 4326), @Address, @Type, @GuestID)", SP);
            Response.Write("1");
            Response.Flush();


        }
        private void GetMapSearch(string Search)
        {
            if (Request.Cookies["MyLanguage"] == null)
            {
                Response.Write("10");
                Response.Flush();
                return;
            }
            string[] MyPos = Regex.Split(Search, "{A}");
            string Token = MyPos[1];
            Token = Tools.Tools.MyDecry(Tools.Tools.ConvertFromBase64(Token));
            DateTime MyDT = Convert.ToDateTime(Token);
            int cp = DateTime.Compare(DateTime.Now, MyDT);
            if (cp > 15)
            {
                Response.Write("10");
                Response.Flush();
                return;
            }
            if (MyPos[0] == null || MyPos[0]=="")
            {
                Response.Write("");
                Response.Flush();
                return;
            }
            SqlParameterCollection SP = new SqlCommand().Parameters;
            SP.AddWithValue("@Str", Tools.Tools.SetSearchWord(MyPos[0].Replace("{B}", "+")));    

            string ot = "";
            SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT TOP (15) MapItem.MapItemID, MapItem.Name, MapItem.Modir, MapItem.Email, MapItem.WebSite, MapItem.Tel, MapItem.Fax, MapItem.lat, MapItem.lon, MapItem.Address, MapItem.Type, MapItem.Zoom, Shahr.Name AS ShahrName, Ostan.Name AS OstanName, MapCategory.Title, MapCategoryItem.Title AS Mtitle, MapCategoryItem.MapCategoryItemID, MapCategory.MapCategoryID  FROM MapCategory INNER JOIN MapCategoryItem ON MapCategory.MapCategoryID = MapCategoryItem.MapCategoryID INNER JOIN MapItem INNER JOIN Shahr ON MapItem.ShahrID = Shahr.ShahrID INNER JOIN Ostan ON Shahr.OstanId = Ostan.OstanID ON MapCategoryItem.MapCategoryItemID = MapItem.MapCategoryItemID  WHERE CONTAINS(MapItem.Name, @Str)", SP); 
            while (MyRead.Read())
            {
                ot += ",{";
                ot += "\"id\":" + Tools.MyCL.MGInt(MyRead, 0) + ",";//id
                ot += "\"N\":\"" + Tools.MyCL.MGStr(MyRead, 1) + "\",";//name
                ot += "\"E\":\"" + Tools.MyCL.MGStr(MyRead, 3) + "\",";
                ot += "\"W\":\"" + Tools.MyCL.MGStr(MyRead, 4) + "\",";
                ot += "\"T\":\"" + Tools.MyCL.MGStr(MyRead, 5) + "\",";
                ot += "\"F\":\"" + Tools.MyCL.MGStr(MyRead, 6) + "\",";
                ot += "\"X\":" + Tools.MyCL.MGStr(MyRead,7) + ",";//lat
                ot += "\"Y\":" + Tools.MyCL.MGStr(MyRead, 8) + ",";//lon
                ot += "\"A\":\"" + Tools.MyCL.MGStr(MyRead, 9) + "\",";
                ot += "\"P\":" + Tools.MyCL.MGInt(MyRead, 10) + ",";//type
                ot += "\"Z\":" + Tools.MyCL.MGInt(MyRead, 11) + ",";//Zoom
                ot += "\"S\":\"" + Tools.MyCL.MGStr(MyRead, 12) + "\",";//shahr
                ot += "\"O\":\"" + Tools.MyCL.MGStr(MyRead, 13) + "\",";//ostan
                ot += "\"M\":\"" + Tools.MyCL.MGStr(MyRead, 14) + "\",";//MT
                ot += "\"C\":\"" + Tools.MyCL.MGStr(MyRead, 15) + "\",";//MCT
                ot += "\"Q\":" + Tools.MyCL.MGInt(MyRead, 16) + ",";//CatitemID
                ot += "\"U\":" + Tools.MyCL.MGInt(MyRead, 17) + ",";//Catid
                ot += "}";
                /*ot += ",[";
                ot += "\"" + Tools.MyCL.MGStr(MyRead, 8)+"\",";
                ot += "\"" + Tools.MyCL.MGStr(MyRead, 9) + "\",";
                ot += "\"" + Tools.MyCL.MGStr(MyRead, 1) + "\",";
                ot += "]";*/
            }
            MyRead.Close(); MyRead.Dispose();
            if (ot == "")
            {
                Response.Write("5");
                Response.Flush();
                return;
            }
            ot = " bb =[" + ot.Substring(1) + "]";

            Response.Write(ot);
            Response.Flush();
        }
        private void AddMapReport(string Item)
        {
            if (Request.Cookies["MyLanguage"] == null)
            {
                Response.Write("10");
                Response.Flush();
                return;
            }
            string[] MyPos = Regex.Split(Item, "{A}");

            SqlParameterCollection SP = new SqlCommand().Parameters;
            SP.AddWithValue("@MapItemID", MyPos[2]);
            SP.AddWithValue("@Type", MyPos[0]);
            SP.AddWithValue("@Matn", MyPos[1]);
            SP.AddWithValue("@GuestID", DAL.CheckData.CheckTokenGuestUserID());
            DAL.ExecuteData.AddData("INSERT INTO MapReport(MapItemID, Type, Matn, GuestID) VALUES (@MapItemID, @Type, @Matn, @GuestID)", SP);

            Response.Write("1");
            Response.Flush();
        }
        private void GetMapDetait(string ID)
        {
            if (Request.Cookies["MyLanguage"] == null)
            {
                Response.Write("10");
                Response.Flush();
                return;
            }
            string[] MyPos = Regex.Split(ID, ",");
            string Token = MyPos[1];
            Token = Tools.Tools.MyDecry(Tools.Tools.ConvertFromBase64(Token));
            DateTime MyDT = Convert.ToDateTime(Token);
            int cp = DateTime.Compare(DateTime.Now, MyDT);
            if (cp > 15)
            {
                Response.Write("10");
                Response.Flush();
                return;
            }
            SqlParameterCollection SP = new SqlCommand().Parameters;
            SP.AddWithValue("@ID", MyPos[0]);
            string ot = "";
            SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT MapItem.MapItemID, MapItem.UID, MapItem.Name,MapItem.Modir, MapItem.Email, MapItem.WebSite, MapItem.Tel, MapItem.Fax, MapItem.Address, Ostan.Name AS Oname, Shahr.Name AS SName, MapCategoryItem.Title FROM MapItem INNER JOIN Ostan ON MapItem.OstanID = Ostan.OstanID INNER JOIN Shahr ON MapItem.ShahrID = Shahr.ShahrID INNER JOIN MapCategoryItem ON MapItem.MapCategoryItemID = MapCategoryItem.MapCategoryItemID WHERE (MapItem.MapItemID = @ID) ", SP);
            if (MyRead.Read())
            {
                ot += "{";
                ot += "\"id\":" + Tools.MyCL.MGInt(MyRead, 0) + ",";
                ot += "\"UID\":\"" + Tools.MyCL.MGUID(MyRead, 1) + "\",";
                ot += "\"Name\":\"" + Tools.MyCL.MGStr(MyRead, 2) + "\",";
                ot += "\"Mod\":\"" + Tools.MyCL.MGStr(MyRead, 3) + "\",";
                ot += "\"Ema\":\"" + Tools.MyCL.MGStr(MyRead, 4) + "\",";
                ot += "\"Web\":\"" + Tools.MyCL.MGStr(MyRead, 5) + "\",";
                ot += "\"Tel\":\"" + Tools.MyCL.MGStr(MyRead, 6) + "\",";
                ot += "\"Fax\":\"" + Tools.MyCL.MGStr(MyRead, 7) + "\",";
                ot += "\"Add\":\"" + Tools.MyCL.MGStr(MyRead, 8) + "\",";
                ot += "\"ONa\":\"" + Tools.MyCL.MGStr(MyRead, 9) + "\",";
                ot += "\"SName\":\"" + Tools.MyCL.MGStr(MyRead, 10) + "\",";
                ot += "\"Cat\":\"" + Tools.MyCL.MGStr(MyRead, 11) + "\"";
                ot += "}";
            }
            MyRead.Close(); MyRead.Dispose();
            if (ot == "")
            {
                Response.Write("5");
                Response.Flush();
                return;
            }
            ot = "var dd =[" + ot + "]";
            Response.Write(ot);
            Response.Flush();
        }
        private void GetMapItem(string Position)
        {
            if(Request.Cookies["MyLanguage"]==null )
            {
                Response.Write("10");
                Response.Flush();
                return;
            }
            string[] MyPos = Regex.Split(Position, ",");
            string Token = MyPos[5];
            Token = Tools.Tools.MyDecry(Tools.Tools.ConvertFromBase64(Token));
            DateTime MyDT = Convert.ToDateTime(Token);
            int cp = DateTime.Compare(DateTime.Now, MyDT);
            if(cp>15)
            {
                Response.Write("10");
                Response.Flush();
                return;
            }
            SqlParameterCollection SP = new SqlCommand().Parameters;
            SP.AddWithValue("@zoom", MyPos[0]);
            SP.AddWithValue("@MinLon", MyPos[1]);
            SP.AddWithValue("@MinLat", MyPos[2]);
            SP.AddWithValue("@MaxLon", MyPos[3]);
            SP.AddWithValue("@MaxLat", MyPos[4]);

            string ot = "";
            SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT  TOP (150) MapItem.MapItemID, MapItem.Name, MapItem.OstanID, MapItem.ShahrID, MapCategory.MapCategoryID, MapItem.Modir, MapItem.Email, MapItem.WebSite, MapItem.lat, MapItem.lon, MapItem.Geo, MapItem.Address, MapItem.Type, MapItem.Zoom, MapCategoryItem.Title, MapCategory.Title AS Ctitle,MapCategoryItem.MapCategoryItemID FROM MapItem INNER JOIN MapCategoryItem ON MapItem.MapCategoryItemID = MapCategoryItem.MapCategoryItemID INNER JOIN MapCategory ON MapCategoryItem.MapCategoryID = MapCategory.MapCategoryID WHERE(Geo.Lat BETWEEN @MinLat AND @MaxLat) AND(Geo.Long BETWEEN @MinLon AND @MaxLon) and zoom <= @zoom order by newid()", SP);
            while (MyRead.Read())
            {
                ot+=",{";
                ot += "\"id\":" + Tools.MyCL.MGInt(MyRead, 0) + ",";//lat
                ot += "\"T\":" + Tools.MyCL.MGStr(MyRead, 8) + ",";//lat
                ot += "\"N\":" + Tools.MyCL.MGStr(MyRead, 9) + ",";//lon
                ot += "\"C\":" + Tools.MyCL.MGInt(MyRead, 4) + ",";//catid
                ot += "\"F\":" + Tools.MyCL.MGInt(MyRead, 16) + ",";//catItemID
                ot += "\"I\":\"" + Tools.MyCL.MGStr(MyRead, 1) + "<br><span>"+ Tools.MyCL.MGStr(MyRead,15) + "> "+ Tools.MyCL.MGStr(MyRead, 14) + "</span><br><span>" + Tools.MyCL.MGStr(MyRead, 11) + "</span>\"";
                ot += "}";
                /*ot += ",[";
                ot += "\"" + Tools.MyCL.MGStr(MyRead, 8)+"\",";
                ot += "\"" + Tools.MyCL.MGStr(MyRead, 9) + "\",";
                ot += "\"" + Tools.MyCL.MGStr(MyRead, 1) + "\",";
                ot += "]";*/
            }
            MyRead.Close(); MyRead.Dispose();
            if (ot=="")
            {
                Response.Write("5");
                Response.Flush();
                return;
            }
            ot = "var a =[" + ot.Substring(1)+"]";

            Response.Write(ot);
            Response.Flush();
        }

        private void SponsorUserEmail(string ID)
		{
			try
			{

				string UserID = Tools.Tools.MyDecry(Tools.Tools.ConvertFromBase64(ID));
				if (!Tools.Tools.CompareDateTime(Convert.ToDateTime(UserID.Substring(UserID.IndexOf("L") + 1)), 10))
				{
					//کلید خراب
					Response.Write("6");
					Response.Flush();
					return;
				}


				SqlParameterCollection SP = new SqlCommand().Parameters;

				SP.AddWithValue("@UserID", UserID.Substring(0, UserID.IndexOf("L")));
				string Email = DAL.ExecuteData.CNTDataStr("SELECT Email   FROM GuestInfo WHERE (GuestID = @UserID)", SP);

				Response.Write( Email);
				Response.Flush();
				return;


			}
			catch
			{
				Response.Write("6");
				Response.Flush();
				return;
			}
		}

		private void SponsorLike(string ID)
		{
			try
			{
				string IsLike = ID.Substring(0, 1);
				ID = ID.Substring(1);
				string SponsorID = ID.Substring(0, (ID.IndexOf("L")));
				string UserID = ID.Substring((ID.IndexOf("L") + 1));
				UserID = Tools.Tools.MyDecry(Tools.Tools.ConvertFromBase64(UserID));
				if (!Tools.Tools.CompareDateTime(Convert.ToDateTime(UserID.Substring(UserID.IndexOf("L") + 1)), 20))
				{
					//کلید خراب
					Response.Write("6");
					Response.Flush();
					return;
				}
				if (IsLike == "0")
				{
					SqlParameterCollection SP = new SqlCommand().Parameters;
					SP.AddWithValue("@SponsorID", SponsorID);
					SP.AddWithValue("@UserID", UserID.Substring(0, UserID.IndexOf("L")));
					DAL.ExecuteData.DeleteData("delete from SponsorLike WHERE (SponsorID = @SponsorID) AND (GuestID = @UserID)", SP);
					Response.Write("4");
					Response.Flush();
					return;
				}
				else
				{
					SqlParameterCollection SP = new SqlCommand().Parameters;
					SP.AddWithValue("@SponsorID", SponsorID);
					SP.AddWithValue("@UserID", UserID.Substring(0, UserID.IndexOf("L")));
					SP.AddWithValue("@UnitID", Tools.Tools.GetViewUnitID);
					int CNT = DAL.ExecuteData.CNTData("SELECT COUNT(*) AS Expr1  FROM SponsorLike WHERE (SponsorID = @SponsorID) AND (GuestID = @UserID)", SP);
					if (CNT != 0)//تکراری
					{
						Response.Write("5");
						Response.Flush();
						return;
					}

					DAL.ExecuteData.AddData("INSERT INTO SponsorLike (SponsorID, GuestID, UnitID)  VALUES (@SponsorID,@UserID,@UnitID)", SP);
					Response.Write("7");
					Response.Flush();
				}
				return;
			}
			catch
			{
				Response.Write("6");
				Response.Flush();
				return;
			}
		}
		private void AddCampignRate(string ID)
		{
			try
			{
				string CommID = ID.Substring(0, (ID.IndexOf("L") ));
				string UserID = ID.Substring((ID.IndexOf("L") + 1));
				UserID = Tools.Tools.MyDecry(Tools.Tools.ConvertFromBase64(UserID));
				if (!Tools.Tools.CompareDateTime(Convert.ToDateTime(UserID.Substring(UserID.IndexOf("L") + 1)), 20))
				{
					//کلید خراب
					Response.Write("6" + CommID);
					Response.Flush();
					return;
				}
				SqlParameterCollection SP = new SqlCommand().Parameters;
				SP.AddWithValue("@CommID", CommID);
				SP.AddWithValue("@UserID", UserID.Substring(0, UserID.IndexOf("L")));
				int CNT = DAL.ExecuteData.CNTData("SELECT COUNT(*) AS Expr1  FROM CampaignGuest WHERE (CampaignID = @CommID) AND (GuestID = @UserID)", SP);
				if (CNT != 0)//تکراری
				{
					Response.Write("5" + CommID);
					Response.Flush();
					return;
				}
				
				DAL.ExecuteData.AddData("INSERT INTO CampaignGuest (CampaignID, GuestID)  VALUES (@CommID,@UserID)", SP);
				Response.Write("7" + CommID);
				Response.Flush();
				return;
			}
			catch
			{
				Response.Write("6");
				Response.Flush();
				return;
			}
		}
		private void Shoping(string p)
		{

			SqlDataReader MyRead = null;
			if (p == "DV")
			{
				if (!Tools.Shop.CheckNull("ShopingDVD"))
				{
					//System.Data.SqlClient.SqlParameterCollection SP = new System.Data.SqlClient.SqlCommand().Parameters;
					ArrayList MyArray = new ArrayList();
					MyArray = (ArrayList)Session["ShopingDVD"];
					string ProID = "";
					for (int i = 0; i < MyArray.Count; i++)
						ProID += ((Object[])MyArray[i])[0] + ",";
					//SP.AddWithValue("@ProID", ProID.TrimEnd(','));
					MyRead = DAL.ViewData.MyDR1("SELECT Product.ProductID, Product.Name, Product.Discount, ProductPrice.Money, ProductPriceType.Name AS PTName FROM Product INNER JOIN ProductPrice ON Product.ProductID = ProductPrice.ProductID INNER JOIN ProductPriceType ON ProductPrice.ProductPriceTypeID = ProductPriceType.ProductPriceTypeID  WHERE (Product.ProductID IN (" + ProID.TrimEnd(',') + "))", null);
					//ViewData.MyConnection.Close();
				}
			}
			else
			{
				if (!Tools.Shop.CheckNull("ShopingCard"))
				{
					ArrayList MyArray = new ArrayList();
					MyArray = (ArrayList)Session["ShopingCard"];
					string ProID = "";
					for (int i = 0; i < MyArray.Count; i++)
						ProID += ((Object[])MyArray[i])[0] + ",";
					MyRead = DAL.ViewData.MyDR1("SELECT Product.ProductID, Product.Name, Product.Discount, ProductPrice.Money, ProductPriceType.Name AS PTName FROM Product INNER JOIN ProductPrice ON Product.ProductID = ProductPrice.ProductID INNER JOIN ProductPriceType ON ProductPrice.ProductPriceTypeID = ProductPriceType.ProductPriceTypeID  WHERE (Product.ProductID IN (" + ProID.TrimEnd(',') + "))", null);
				}
			}
			string OT = "";
			while (MyRead.Read())
			{
				OT += "<tr id='SDPro" + Tools.MyCL.MGInt(MyRead, 0) + "'><td> " + Tools.MyCL.MGStr(MyRead, 1) + " </td>  <td> " + Tools.Shop.GetPriceDiscount(Tools.MyCL.MGInt(MyRead, 3), Tools.MyCL.MGInt(MyRead, 2)) + " " + Tools.MyCL.MGStr(MyRead, 4) + " </td> <td> <img src=\"/Images/zarb.gif\" onclick=\"DELDItem('" + Tools.MyCL.MGInt(MyRead, 0) + "')\" width=\"16px\" height=\"16px\" title=\"حذف\" alt=\"حذف\" /> </td> </tr>";
			}
			MyRead.Close();
			MyRead.Dispose();
			Response.Write(OT);
			Response.Flush();
		}

		private void SendTelegramForward(string InText)
		{
			string[] MyIDs = Regex.Split(InText, "@");
			int ToUserID = Tools.Tools.ConvertToInt32(MyIDs[0]);
			int fromchatid = Tools.Tools.ConvertToInt32(MyIDs[2]);
			int MessID = Tools.Tools.ConvertToInt32(MyIDs[1]);
			if (ToUserID < 0 || fromchatid < 0 || MessID < 0)
			{
				Response.Write("error");
				Response.Flush();
			}
			Tools.TelegramMe.MyForwardMessage(ToUserID, MessID, MessID);
			Response.Write("ok");
			Response.Flush();
		}
		private void SendTelegramMessage(string inText)
		{
			string MessID = inText.Substring(0, inText.IndexOf("{$}"));
			string txt = inText.Substring(inText.IndexOf("{$}") + 3);
			int Messid = Tools.Tools.ConvertToInt32(MessID, -1);
			if (Messid == -1)
			{
				Response.Write("error");
				Response.Flush();
				return;
			}
			Tools.TelegramMe.SendToServerTextMessage(txt, "", MessID, Telegram.Telegram.TelegramUserID);
			var tb = "<table border=\"0\" dir=rtl width='100%' class='myviewsimtable'><tr><td><div class=\"SimConnBodyDiv\">" + txt + "</div>";
			tb += "<div class=\"SimConnDateDiv\">" + Tools.Calender.MyPDate(DateTime.Now) + " " + Tools.Calender.GetTime(DateTime.Now) + "</div></td>";
			tb += "<td style=\"width:70px;\" valign=\"top\" ><img src=\"/Images/Telegram/UserAva.png\" class=\"SimConnAvaImg\" /></td></tr></table>";

			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@TelegramUserID", Telegram.Telegram.TelegramUserID);
			SP.AddWithValue("@MessageID", MessID);
			SP.AddWithValue("@Message", txt);
			DAL.ExecuteData.AddData("INSERT INTO TelegramSend (TelegramUserID, MessageID, Message) VALUES (@TelegramUserID, @MessageID, @Message)", SP);
			Response.Write(tb);
			Response.Flush();
		}
		private void GetTelegramAnswer(string FromID)
		{
			int Fid = Tools.Tools.ConvertToInt32(FromID);
			if (Fid < 0)
			{
				Response.Write("Error");
				Response.Flush();
			}

			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@Fid", Fid);

			string OutText = "";
			//SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT AutoAnswer, Body, Date  FROM TelegramRecive  WHERE (FromID = @Fid)  ORDER BY TelegramReciveID",SP);
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT AutoAnswer, FromID, Message, Date,type,MessageID FROM (SELECT 6 AS AutoAnswer, TelegramSendID AS FromID, Message, Date,type,0 as MessageID FROM TelegramSend WHERE (MessageID = @Fid) UNION SELECT AutoAnswer, FromID, Body, Date,type,MessageID FROM TelegramRecive WHERE (chatID = @Fid)) AS derivedtbl_1 ORDER BY Date", SP);
			while (MyRead.Read())
			{
				if (Tools.MyCL.MGInt(MyRead, 0) == 6)
				{
					OutText += "<table border=\"0\" dir=rtl width='100%' class='myviewsimtable'><tr><td><div class=\"SimConnBodyDiv\">" + Tools.MyCL.MGStr(MyRead, 2) + "</div>";
					OutText += "<div class=\"SimConnDateDiv\">" + Tools.Calender.MyPDate(Tools.MyCL.MGDTDT(MyRead, 3)) + " " + Tools.Calender.GetTime(Tools.MyCL.MGDTDT(MyRead, 3)) + "</div></td>";
					OutText += "<td style=\"width:70px;\" valign=\"top\" ><img src=\"/Images/Telegram/UserAva.png\" class=\"SimConnAvaImg\" /></td></tr></table>";
				}
				else
				{
					int MessType = Tools.MyCL.MGInt(MyRead, 4);
					switch (MessType)
					{
						case (int)Tools.MyVar.TelegramMessType.text:
							OutText += "<table border=\"0\" dir=rtl width='100%' class='myviewsimtable'><tr><td style=\"width:70px;\" valign=\"top\" ><img src=\"/Images/Telegram/MessAva.png\" class=\"SimConnAvaImg\" /></td> <td> ";
							OutText += "<div class=\"SimConnBodyDiv\">" + Tools.MyCL.MGStr(MyRead, 2) + "</div>";
							OutText += "<div class=\"SimConnDateDiv\">" + Tools.Calender.MyPDate(Tools.MyCL.MGDTDT(MyRead, 3)) + " " + Tools.Calender.GetTime(Tools.MyCL.MGDTDT(MyRead, 3)) + "</div></td></tr></table>";
							break;
						case (int)Tools.MyVar.TelegramMessType.contact:
							OutText += "<table border=\"0\" dir=rtl width='100%' class='myviewsimtable'><tr><td style=\"width:70px;\" valign=\"top\" ><img src=\"/Images/Telegram/MessAva.png\" class=\"SimConnAvaImg\" /></td> <td> ";
							OutText += "<div class=\"SimConnBodyDiv\">" + Tools.TelegramMe.GetContactMessage(Tools.MyCL.MGStr(MyRead, 2)) + "</div>";
							OutText += "<div class=\"SimConnDateDiv\">" + Tools.Calender.MyPDate(Tools.MyCL.MGDTDT(MyRead, 3)) + " " + Tools.Calender.GetTime(Tools.MyCL.MGDTDT(MyRead, 3)) + "</div></td></tr></table>";
							break;
						case (int)Tools.MyVar.TelegramMessType.location://onclick="SelectPrepMsg('TextImport.aspx?ID=<%=ReqID%>')" 
							OutText += "<table border=\"0\" dir=rtl width='100%' class='myviewsimtable'><tr><td style=\"width:70px;\" valign=\"top\" ><img src=\"/Images/Telegram/MessAva.png\" class=\"SimConnAvaImg\" /></td> <td> ";
							OutText += "<div class=\"SimConnBodyDiv\">" + Tools.TelegramMe.GetLocationMessage(Tools.MyCL.MGStr(MyRead, 2), Tools.MyCL.MGInt(MyRead, 1)) + "</div>";
							OutText += "<div class=\"SimConnDateDiv\">" + Tools.Calender.MyPDate(Tools.MyCL.MGDTDT(MyRead, 3)) + " " + Tools.Calender.GetTime(Tools.MyCL.MGDTDT(MyRead, 3)) + "</div></td></tr></table>";
							break;
						case (int)Tools.MyVar.TelegramMessType.photo:
							OutText += "<table border=\"0\" dir=rtl width='100%' class='myviewsimtable'><tr><td style=\"width:70px;\" valign=\"top\" ><img src=\"/Images/Telegram/MessAva.png\" class=\"SimConnAvaImg\" /></td> <td> ";
							OutText += "<div class=\"SimConnBodyDiv\">" + Tools.TelegramMe.GetPhotoMessage(Tools.MyCL.MGStr(MyRead, 2), Tools.MyCL.MGInt(MyRead, 1)) + "</div>";//<br><input onclick=\"ForwardMess('" + Tools.MyCL.MGInt(MyRead, 5) + "'," + Tools.MyCL.MGInt(MyRead, 1) + ")\" id=\"Button1\" type=\"button\" value=\"ارسال به دیگری\" />
							OutText += "<div class=\"SimConnDateDiv\">" + Tools.Calender.MyPDate(Tools.MyCL.MGDTDT(MyRead, 3)) + " " + Tools.Calender.GetTime(Tools.MyCL.MGDTDT(MyRead, 3)) + "</div></td></tr></table>";
							break;
						case (int)Tools.MyVar.TelegramMessType.video:
							OutText += "<table border=\"0\" dir=rtl width='100%' class='myviewsimtable'><tr><td style=\"width:70px;\" valign=\"top\" ><img src=\"/Images/Telegram/MessAva.png\" class=\"SimConnAvaImg\" /></td> <td> ";
							OutText += "<div class=\"SimConnBodyDiv\">" + Tools.TelegramMe.GetVideoMessage(Tools.MyCL.MGStr(MyRead, 2), Tools.MyCL.MGInt(MyRead, 1)) + "</div>";
							OutText += "<div class=\"SimConnDateDiv\">" + Tools.Calender.MyPDate(Tools.MyCL.MGDTDT(MyRead, 3)) + " " + Tools.Calender.GetTime(Tools.MyCL.MGDTDT(MyRead, 3)) + "</div></td></tr></table>";
							break;
						case (int)Tools.MyVar.TelegramMessType.audio:
							OutText += "<table border=\"0\" dir=rtl width='100%' class='myviewsimtable'><tr><td style=\"width:70px;\" valign=\"top\" ><img src=\"/Images/Telegram/MessAva.png\" class=\"SimConnAvaImg\" /></td> <td> ";
							OutText += "<div class=\"SimConnBodyDiv\">" + Tools.TelegramMe.GetVoiceMessage(Tools.MyCL.MGStr(MyRead, 2), Tools.MyCL.MGInt(MyRead, 1)) + "</div>";
							OutText += "<div class=\"SimConnDateDiv\">" + Tools.Calender.MyPDate(Tools.MyCL.MGDTDT(MyRead, 3)) + " " + Tools.Calender.GetTime(Tools.MyCL.MGDTDT(MyRead, 3)) + "</div></td></tr></table>";
							break;
						case (int)Tools.MyVar.TelegramMessType.document:
							OutText += "<table border=\"0\" dir=rtl width='100%' class='myviewsimtable'><tr><td style=\"width:70px;\" valign=\"top\" ><img src=\"/Images/Telegram/MessAva.png\" class=\"SimConnAvaImg\" /></td> <td> ";
							OutText += "<div class=\"SimConnBodyDiv\">" + Tools.TelegramMe.GetDocumentMessage(Tools.MyCL.MGStr(MyRead, 2), Tools.MyCL.MGInt(MyRead, 1)) + "</div>";
							OutText += "<div class=\"SimConnDateDiv\">" + Tools.Calender.MyPDate(Tools.MyCL.MGDTDT(MyRead, 3)) + " " + Tools.Calender.GetTime(Tools.MyCL.MGDTDT(MyRead, 3)) + "</div></td></tr></table>";
							break;
						case (int)Tools.MyVar.TelegramMessType.sticker:
							OutText += "<table border=\"0\" dir=rtl width='100%' class='myviewsimtable'><tr><td style=\"width:70px;\" valign=\"top\" ><img src=\"/Images/Telegram/MessAva.png\" class=\"SimConnAvaImg\" /></td> <td> ";
							OutText += "<div class=\"SimConnBodyDiv\">" + Tools.TelegramMe.GetStickerMessage(Tools.MyCL.MGStr(MyRead, 2), Tools.MyCL.MGInt(MyRead, 1)) + "</div>";
							OutText += "<div class=\"SimConnDateDiv\">" + Tools.Calender.MyPDate(Tools.MyCL.MGDTDT(MyRead, 3)) + " " + Tools.Calender.GetTime(Tools.MyCL.MGDTDT(MyRead, 3)) + "</div></td></tr></table>";
							break;
						default:
							OutText += "<table border=\"0\" dir=rtl width='100%' class='myviewsimtable'><tr><td style=\"width:70px;\" valign=\"top\" ><img src=\"/Images/Telegram/MessAva.png\" class=\"SimConnAvaImg\" /></td> <td> ";
							OutText += "<div class=\"SimConnBodyDiv\">" + Tools.MyCL.MGStr(MyRead, 2) + "</div>";
							OutText += "<div class=\"SimConnDateDiv\">" + Tools.Calender.MyPDate(Tools.MyCL.MGDTDT(MyRead, 3)) + " " + Tools.Calender.GetTime(Tools.MyCL.MGDTDT(MyRead, 3)) + "</div></td></tr></table>";
							break;

					}
				}
			}
			MyRead.Close(); MyRead.Dispose();
			Response.Write(OutText + "</table>");
			Response.Flush();
		}
		private void AddCommandRate(string ID)
		{
			try
			{
				string CommID = ID.Substring(1, (ID.IndexOf("L") - 1));
				string Rate = ID.Substring(0, 1);
				string UserID = ID.Substring((ID.IndexOf("L") + 1));
				UserID = Tools.Tools.MyDecry(Tools.Tools.ConvertFromBase64(UserID));
				if (!Tools.Tools.CompareDateTime(Convert.ToDateTime(UserID.Substring(UserID.IndexOf("L") + 1)), 20))
				{
					//کلید خراب
					Response.Write("6" + CommID);
					Response.Flush();
					return;
				}
				SqlParameterCollection SP = new SqlCommand().Parameters;
				SP.AddWithValue("@CommID", CommID);
				SP.AddWithValue("@UserID", UserID.Substring(0, UserID.IndexOf("L")));
				int CNT = DAL.ExecuteData.CNTData("SELECT COUNT(*) AS Expr1  FROM CommentRate WHERE (CommentID = @CommID) AND (GuestID = @UserID)", SP);
				if (CNT != 0)//تکراری
				{
					Response.Write("5" + CommID);
					Response.Flush();
					return;
				}
				string RateSTR = "raten = raten+1";
				if (Rate == "1")
					RateSTR = "ratep = ratep+1";

				DAL.ExecuteData.AddData("INSERT INTO CommentRate (CommentID, GuestID)  VALUES (@CommID,@UserID)", SP);
				DAL.ExecuteData.AddData("UPDATE Comment  SET  " + RateSTR + "  WHERE (CommentID =@CommID)", SP);
				Response.Write(RateSTR.Substring(4, 1) + CommID);
				Response.Flush();
				return;
			}
			catch
			{
				Response.Write("6");
				Response.Flush();
				return;
			}
		}
		private void GetProjectRefrence(string inp)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@ProjectTicketingID", inp);
			inp += "%<fieldset id=\"TicketCycleFS\"><legend>ارجاعات تیکت</legend><table id=\"TicketCycle\"><tr><th>وضعیت</th><th>ارجاع دهنده</th><th>ارجاع گیرنده</th><th>تاریخ مشاهده</th><th>تاریخ خاتمه</th><th>تاریخ ارجاع</th></tr>";
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT ProjectActivityReference.ProjectActivityReferenceID, ProjectActivityReference.ProjectActivityID, ProjectActivityReference.SenderPersonalID, ProjectActivityReference.ToPersonalID, ProjectActivityReference.ProjectPriorityID, ProjectActivityReference.Paraph, ProjectActivityReference.PerParaph, ProjectActivityReference.Date, ProjectActivityReference.ViewDate, ProjectActivityReference.EndDate, ProjectActivityReference.UnitID, ProjectActivityReference.Type, ProjectActivityReference.ProjectActivityReferenceTypeID FROM ProjectActivityReference INNER JOIN ProjectTicketing ON ProjectActivityReference.ProjectActivityID = ProjectTicketing.ProjectActivityID WHERE (ProjectTicketing.ProjectTicketingID = @ProjectTicketingID) ORDER BY ProjectActivityReference.ProjectActivityID DESC ", SP);
			while (MyRead.Read())
			{
				inp += "<tr>";
				inp += "<td>" + Tools.MyCL.MGInt(MyRead, "type").ToString() + "</td>";
				inp += "<td>" + Tools.Automation.GetGuestName(Tools.MyCL.MGInt(MyRead, 2).ToString()) + "</td>";
				inp += "<td>" + Tools.Automation.GetGuestName(Tools.MyCL.MGInt(MyRead, 3).ToString()) + "</td>";
				inp += "<td>" + Tools.Calender.MyPDateTime(Tools.MyCL.MGDT(MyRead, 8)) + "</td>";
				inp += "<td>" + Tools.Calender.MyPDateTime(Tools.MyCL.MGDT(MyRead, 9)) + "</td>";
				inp += "<td>" + Tools.Calender.MyPDateTime(Tools.MyCL.MGDT(MyRead, 7)) + "</td>";
				inp += "</tr>";
			}
			inp += "</table></fieldset>";
			inp += "<fieldset><legend>اقدامات تیکت</legend><table id=\"TicketActs\"><tr><th>اقدام کننده</th><th>شرح اقدام</th><th>تاریخ</th></tr>";

			MyRead.Close(); MyRead.Dispose();
			MyRead = DAL.ViewData.MyDR1("SELECT ProjectActivityAction.ProjectActivityActionID, ProjectActivityAction.ProjectActivityID, ProjectActivityAction.GuestID, ProjectActivityAction.Text, ProjectActivityAction.UnitID, ProjectActivityAction.Date  FROM            ProjectActivityAction INNER JOIN                          ProjectTicketing ON ProjectActivityAction.ProjectActivityID = ProjectTicketing.ProjectActivityID	WHERE (ProjectTicketing.ProjectTicketingID = @ProjectTicketingID) ORDER BY ProjectActivityAction.ProjectActivityActionID DESC ", SP);
			while (MyRead.Read())
			{
				inp += "<tr>";
				//<b>شماره ارجاع: </b>&nbsp;<%# DataBinder.Eval(Container.DataItem, "ProjectActivityReferenceid").ToString().Trim() %><br />
				inp += "<td>" + Tools.Automation.GetGuestName(Tools.MyCL.MGInt(MyRead, 2).ToString()) + "</td>";
				inp += "<td>" + Tools.MyCL.MGStr(MyRead, 3) + "</td>";
				inp += "<td>" + Tools.Calender.MyPDateTime(Tools.MyCL.MGDT(MyRead, 5)) + "</td>";
				inp += "</tr>";
			}
			inp += "</table></fieldset>";
			MyRead.Close(); MyRead.Dispose();
			Response.Write(inp);
			Response.Flush();
			return;
		}
		private void AddRate()
		{
			HttpCookie httpCookie1;
			string ReqType = Tools.Tools.ConvertToInt32(Request.QueryString["Type"], 0).ToString();
			string ReqID = Tools.Tools.ConvertToInt32(Request.QueryString["ID"]).ToString();
			int RateReq = Tools.Tools.ConvertToInt32(Request.QueryString["Rate"]);
			if (RateReq <= 0)
				RateReq = 5;
			string string2 = "Rate-" + ReqType + "-" + ReqID;
			if (base.Request.Cookies[string2] == null)
			{
				httpCookie1 = new HttpCookie(string2, "1");
				httpCookie1.Expires = new DateTime(3000, 1, 1);
				base.Response.Cookies.Add(httpCookie1);
				SqlParameterCollection SP = new SqlCommand().Parameters;
				SP.AddWithValue("@Type", ReqType);
				SP.AddWithValue("@ID", ReqID);
				SP.AddWithValue("@Rate", RateReq);
				SP.AddWithValue("@IP", Tools.Tools.GetUserIPAddress());
				//if (ExecuteData.CNTData("select count(*) from rating where Type=@Type and ID=@ID and IP=@IP", SP) == 0)
				ExecuteData.AddData("INSERT INTO Rating (Type, ID, Rate,IP)  VALUES (@Type, @ID, @Rate,@IP)", SP);
				Response.Write("OK");
				return;
			}
			else
			{
				Response.Write("Reapet");
				return;
			}
		}
		private void AddCommand(string p)
		{
			try
			{
				string[] myItems = Regex.Split(p, "{E}");
				if (myItems.Length == 0)
				{
					Response.Write("CO");
					Response.Flush();
					return;
				}
				if (myItems[0] != Tools.Tools.SecurityImageCode)
				{
					Response.Write("CO");
					Response.Flush();
					return;
				}
				SqlParameterCollection SP = new SqlCommand().Parameters;
				SP.AddWithValue("@name", Regex.Unescape(myItems[3]));
				SP.AddWithValue("@Email", myItems[4]);
				SP.AddWithValue("@Comment", Tools.Tools.ReplacetEmotion(Regex.Unescape(myItems[5])));
				SP.AddWithValue("@Type", myItems[2]);
				SP.AddWithValue("@ID", myItems[1]);
				SP.AddWithValue("@UID", Tools.Tools.GetViewUnitID);
				ExecuteData.AddData(" INSERT INTO Comment( Name, Email, Comment, Type,ID,unitid,Disable) VALUES (@name,@Email,@Comment,@Type,@ID,@UID," + Tools.Tools.GetSetting(387, "1") + ")", SP);
				Response.Write("OK");
				Response.Flush();
				return;
			}
			catch
			{
				//Logging.ErrorLog("Comment", "Error1", ex.Message + "{$$$}" + ex.Source + "{$$$}" + ex.Data + "{$$$}" + ex.InnerException + "{$$$}" + ex.StackTrace + "{$$$}" + ex.TargetSite + "{$$$}");

				Response.Write("ER");
				Response.Flush();
				return;
			}
		}
		private void ForumLike(string p)
		{
			p = Tools.Tools.ConvertFromBase64(Regex.Unescape(p));
			if (p == "")
			{
				Response.Write("er0");
				return;
			}
			string MessageID = "", GuestID = "";
			try
			{
				MessageID = p.Substring(0, p.IndexOf("-"));
				GuestID = p.Substring(p.IndexOf("-") + 1);
			}
			catch
			{
				Response.Write("No" + MessageID);
				return;
			}
			if (MessageID == "" || GuestID == "")
				return;
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@MID", MessageID);
			SP.AddWithValue("@GID", GuestID);
			int cnt = DAL.ExecuteData.CNTData("SELECT COUNT(*) AS Expr1  FROM ForumMessagesLike  WHERE (ForumMessagesID =@MID) AND (GuestID = @GID)", SP);
			if (cnt <= 0)
				DAL.ExecuteData.InsertData("INSERT INTO ForumMessagesLike(ForumMessagesID, GuestID) VALUES (@MID,@GID)", SP);
			else
			{
				Response.Write("No" + MessageID);
				return;
			}
			Response.Write("OK" + MessageID);
		}

		private void GetOfficeTemplate(string TemplateID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@RID", TemplateID);
			string Matn = DAL.ExecuteData.CNTDataStr("SELECT Text  FROM OfficeLetterTemplate  WHERE (OfficeLetterTemplateID = @RID) ", SP);
			//Matn = Matn.Replace("\"","{A}");
			//Matn = Matn.Replace("'", "{B}");
			//Matn = Matn.Replace("\r", "");
			//Matn = Matn.Replace("\n", "");

			Response.Write("OK" + Matn);
		}
		private void SetLetterReferenceType(string p)
		{
			if (p.IndexOf("_") == -1)
			{
				Response.Write("error");
				return;
			}
			string SetType = p.Substring(p.IndexOf("_") + 1);
			string EndDateSetComm = "";
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@RID", p.Substring(0, p.IndexOf("_")));
			SP.AddWithValue("@Type", p.Substring(p.IndexOf("_") + 1));
			if (SetType == "2" || SetType == "3")
				EndDateSetComm = ",EndDate=getdate()";
			DAL.ExecuteData.ExecData("UPDATE OfficeReference  SET Type =@Type " + EndDateSetComm + " WHERE (OfficeReferenceID = @RID)", SP);
			Response.Write("OK" + p.Substring(0, p.IndexOf("_")));
		}
		private void SetProjectReferenceType(string p)
		{
			if (p.IndexOf("_") == -1)
			{
				Response.Write("error");
				return;
			}
			string SetType = p.Substring(p.IndexOf("_") + 1);
			string EndDateSetComm = "";
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@RID", p.Substring(0, p.IndexOf("_")));
			SP.AddWithValue("@Type", p.Substring(p.IndexOf("_") + 1));
			if (SetType == "2" || SetType == "3")
				EndDateSetComm = ",EndDate=getdate()";
			DAL.ExecuteData.ExecData("UPDATE ProjectActivity  SET Type =@Type " + EndDateSetComm + " WHERE (ProjectActivityID = @RID)", SP);
			Response.Write("OK" + p.Substring(0, p.IndexOf("_")));
		}
		private void SetReciveArchive(string SMSID)
		{
			SMSID = SMSID.Substring(1);
			if (SMS.SMS.SMSUserID == -1)
				return;
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@ID", SMSID.Substring(1));
			if (SMSID.Substring(0, 1) == "-")
				DAL.ExecuteData.DeleteData("DELETE FROM SMS_Recive  WHERE (SMS_UserID = " + SMS.SMS.SMSUserID + ") AND (SMS_ReciveID = @ID) ", SP);
			else
				DAL.ExecuteData.ExecData("UPDATE SMS_Recive SET Type = " + (int)Tools.MyVar.MessageReciveType.Archive + " WHERE     (SMS_UserID = " + SMS.SMS.SMSUserID + ") AND (SMS_ReciveID = @ID) ", SP);
			Response.Write("OK" + SMSID.Substring(1));
		}
		private void CheckAvalibilityLineNo(string LineNo)
		{
			int available = 1;
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@MM", LineNo.TrimStart('+'));
			int CNT = DAL.ExecuteData.CNTData("select count(*) from TempMobileBye where TempMobileBye=@MM ", SP);
			if (CNT != 0)
				Response.Write("OK" + 2);
			else
			{
				SP.Clear();
				SP.AddWithValue("@MM", "98" + LineNo);
				CNT = DAL.ExecuteData.CNTData("SELECT COUNT(*) AS Expr1 FROM SMS_LineNo  WHERE ([LineNo] = @MM)", SP);
				if (CNT != 0)
					Response.Write("OK" + 2);
				Response.Write("OK" + available);
			}
		}
		private void GetSMSPhoneCNT(string PhoneNo)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@PhoneNo", PhoneNo + "%");
			int CNT = ExecuteData.CNTData("SELECT COUNT(*)  FROM SMS_BulkPhone where (MobNo LIKE @PhoneNo)", SP);
			Response.Write("OK" + CNT);
		}
		private void GetSMSPostalPhoneCNT(string PhoneNo)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@PhoneNo", PhoneNo + "%");
			int CNT = ExecuteData.CNTData("SELECT count(*)  FROM SMS_PostalCode  WHERE (Postcode LIKE @PhoneNo)", SP);
			Response.Write("OK" + CNT);
		}
		private void GetSMSTreeNode(string Level)
		{
			Level = Level.Substring(2);
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@Level", Level + "%");

			int CNT = ExecuteData.CNTData("SELECT COUNT(*) FROM SMS_BulkPhoneTree WHERE ([Level] LIKE @Level) AND (LEN([Level]) = " + (Level.Length + 3) + ")", SP);
			if (CNT == 0)
				return;
			int LevLen = Level.Length + 3;
			string MenuStr = "[ ";
			SqlDataReader MyRead = ViewData.MyDR1("SELECT Name, [Level],ChildCNT FROM SMS_BulkPhoneTree WHERE (LEN([Level]) =" + LevLen + ") AND ([Level] LIKE @Level) order by [level]", SP, true);
			while (MyRead.Read())
			{
				MenuStr += "  {\"title\": \"" + Tools.MyCL.MGStr(MyRead, 0) + " (" + Tools.MyCL.MGInt(MyRead, 2) + ")\", " + GetItemCNT(Tools.MyCL.MGStr(MyRead, 1)) + " \"key\": \"C" + Tools.MyCL.MGStr(MyRead, 1) + "\" ";
				//BuildMenuLevel2(Tools.MyCL.MGStr(MyRead, 1));
				MenuStr += "},";
			}
			MyRead.Close(); MyRead.Dispose();
			MenuStr = MenuStr.TrimEnd(',');
			MenuStr += "]";
			Response.Write(MenuStr);
		}
		private string GetItemCNT(string Level)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@Level", Level + "%");

			if (DAL.ExecuteData.CNTData("SELECT COUNT(*) AS Expr1 FROM SMS_BulkPhoneTree WHERE ([Level] LIKE @Level) AND (LEN([Level]) = " + (Level.Length + 3) + ")", SP) >= 1)
				return "\"isFolder\": true, \"isLazy\": true,";
			else
				return "";
		}
		void DelShopCart(string PID)
		{
			if (PID.Substring(0, 1) == "D")
				Tools.Shop.DelProToShopCard(PID.Substring(1), "ShopingDVD");
			else
				Tools.Shop.DelProToShopCard(PID);
			Response.Write("OK");
		}
	}
}
