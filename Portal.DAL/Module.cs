using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
using System.Text.RegularExpressions;

namespace Tools
{
	public class Module
	{
		public static string GetModuleData(int ModuleTypeID, int ItemTypeID, string RepeatTag, int CNT, int Sort,string WhereComm,int StartFrom)
		{
			string ot = "";
			switch (ModuleTypeID)
			{
				case 1://Article
					{
						ot = GetArticleStr(ItemTypeID, RepeatTag, CNT, Sort, WhereComm);
					}
					break;
				case 2://news
					{
						ot = GetNewsStr(ItemTypeID, RepeatTag, CNT, Sort, WhereComm,StartFrom);
					}
					break;
				case 3://Movie
					{
						ot = GetMovieStr(ItemTypeID, RepeatTag, CNT, Sort, WhereComm);
					}
					break;
				case 4://Sound
					{
						ot = GetSoundStr(ItemTypeID, RepeatTag, CNT, Sort, WhereComm);
					}
					break;
				case 5://gallery
					{
						ot = GetGalleryStr(ItemTypeID, RepeatTag, CNT, Sort, WhereComm);
					}
					break;
				case 6://Software
					{
						ot = GetSoftwareStr(ItemTypeID, RepeatTag, CNT, Sort, WhereComm);
					}break;
				case 7://Product
					{
						ot = GetProductStr(ItemTypeID, RepeatTag, CNT, Sort, WhereComm);
					}
					break;
				case 8://Campaign
					{
						ot = GetCampaignStr(ItemTypeID, RepeatTag, CNT, Sort, WhereComm);
					}
					break;
				default:
					break;
			}
			return ot;
		}
		private static string GetCampaignPicture(string PicName)
		{
			if (PicName.Trim() == "")
				return "";
			return "<img class=\"CampaignPageImage\" src=\"/Files/" + Tools.GetViewUnitID + "/Images/Campaign/" + PicName + "\" />";
		}
		private static string GetCampaignStr(int ItemTypeID, string RepeatTag, int CNT, int Sort, string WhereComm)
		{
			string ot = "";
			//SoundID$Name$SoundAddress$SoundTypeID$SoundTypeName$Hit$LangID$PageLink
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT TOP (" + Tools.ConvertToInt32(CNT.ToString()) + ") CampaignID, Title, PicName, Date, LangID, Hit FROM Campaign WHERE (UnitID = " + Tools.GetViewUnitID + ") AND (Visible = 1) AND (LangID = " + Tools.LangID + ")   ORDER BY CampaignID " + (Sort == 0 ? "desc" : ""));
			int i = 0;
			while (MyRead.Read())
			{
				string tmp = RepeatTag;
				tmp = tmp.Replace("$IndexID$", i.ToString()); i++;
				tmp = tmp.Replace("$CampaignID$", MyCL.MGInt(MyRead, 0).ToString());
                tmp = CheckLength(tmp, "Title", MyCL.MGStr(MyRead, 1));
				tmp = tmp.Replace("$ImageTag$", GetCampaignPicture(MyCL.MGStr(MyRead, 2)));
				tmp = tmp.Replace("$ImageSRC$", "/Files/" + Tools.GetViewUnitID + "/Images/Campaign/" + MyCL.MGStr(MyRead, 2));
				tmp = tmp.Replace("$LangID$", MyCL.MGInt(MyRead, 4).ToString());
				tmp = tmp.Replace("$Hit$", MyCL.MGInt(MyRead, 5).ToString());
				tmp = tmp.Replace("$PageLink$", "/Campaign/" + MyCL.MGInt(MyRead, 0) + "/" + Tools.UrlWordReplace(MyCL.MGStr(MyRead, 1)) + "");
				ot += tmp;
			}
			MyRead.Close(); MyRead.Dispose();
			return ot;
		}
		private static string GetProductStr(int ItemTypeID, string RepeatTag, int CNT, int Sort, string WhereComm)
		{
			string ot = "";
			string SortStr = " ORDER BY Product.ProductID " + (Sort == 0 ? "desc" : "");
			if (Sort == 2)
				SortStr = " order by newid() ";
            if (Sort == 3)
                SortStr = " order by Product.hit desc ";
            //ProductID$ProductSubjectID$LangID$Product.Name$Hit$Weight$Discount$ImageSRC$Image2SRC$ProductSubjectName$PageLink
            SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT TOP (" + Tools.ConvertToInt32(CNT.ToString()) + ") Product.ProductID, Product.ProductSubjectID, Product.LangID, Product.Name, Product.Hit, Product.Weight, Product.Discount,Product.PicName, ProductSubject.Name AS TypeName, Product.Summary FROM Product INNER JOIN ProductSubject ON Product.ProductSubjectID = ProductSubject.ProductSubjectID WHERE  (Product.Holding <> 0) and (Product.UnitID = " + Tools.GetViewUnitID + ")  AND (Product.LangID = " + Tools.LangID + ") " + (ItemTypeID == 0 ? "" : " AND (Product.ProductSubjectID =" + ItemTypeID + ")") + "  "+SortStr);
			int i = 0;
			while (MyRead.Read())
			{
				string tmp = RepeatTag;
				tmp = tmp.Replace("$IndexID$", i.ToString()); i++;
				tmp = tmp.Replace("$ProductID$", MyCL.MGInt(MyRead, 0).ToString());
                tmp = CheckLength(tmp, "$Product.Name$", MyCL.MGStr(MyRead, 3));
                tmp = tmp.Replace("$Product.Name$", MyCL.MGStr(MyRead, 3));
                tmp = tmp.Replace("$Summary$", MyCL.MGStr(MyRead, 9));
				tmp = tmp.Replace("$Weight$", MyCL.MGInt(MyRead, 5).ToString());
				tmp = tmp.Replace("$Discount$", MyCL.MGInt(MyRead, 6).ToString());
				tmp = tmp.Replace("$ProductSubjectID$", MyCL.MGInt(MyRead, 1).ToString());
                //tmp = tmp.Replace("$FileAddress$", GetFileLink(MyCL.MGStr(MyRead, 2), MyCL.MGStr(MyRead, 10)));
                tmp = tmp.Replace("$ProductSubjectNam$", MyCL.MGStr(MyRead, 8).ToString());
				tmp = tmp.Replace("$LangID$", MyCL.MGInt(MyRead, 2).ToString());
				tmp = tmp.Replace("$Hit$", MyCL.MGInt(MyRead, 4).ToString());
				tmp = tmp.Replace("$ImageSRC$", "/Files/" + Tools.GetViewUnitID + "/Images/ProPic/th_" + MyCL.MGStr(MyRead, 7));
				tmp = tmp.Replace("$Image2SRC$", "/Files/" + Tools.GetViewUnitID + "/Images/ProPic/" + MyCL.MGStr(MyRead, 7));
			
				tmp = tmp.Replace("$PageLink$", "/Shop/Product/" + MyCL.MGInt(MyRead, 0) + "/" + Tools.UrlWordReplace(MyCL.MGStr(MyRead, 3)) + "");
				ot += tmp;
			}
			MyRead.Close(); MyRead.Dispose();
			return ot;
		}
		private static string GetSoftwareStr(int ItemTypeID, string RepeatTag, int CNT, int Sort, string WhereComm)
		{
			string ot = "";
			//SoftID$Name$Address$SoftTypeID$Hit$LangID$FileAddress$FileSize$SoftTypeName$PageLink
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT TOP (" + Tools.ConvertToInt32(CNT.ToString()) + ") Soft.SoftID, Soft.Name, Soft.Address, Soft.SoftTypeID, Soft.UnitID, Soft.Hit, Soft.LangID, Soft.softid as bb, Soft.FileSize, SoftType.Name AS typename,soft.LinkAddress,soft.FileSize FROM Soft INNER JOIN SoftType ON Soft.SoftTypeID = SoftType.SoftTypeID WHERE (Soft.UnitID = " + Tools.GetViewUnitID + ") AND (Soft.LangID = " + Tools.LangID + ") " + (ItemTypeID == 0 ? "" : " AND (Soft.SoftTypeID=" + ItemTypeID + ")") + "  ORDER BY Soft.SoftID " + (Sort == 0 ? "desc" : ""));
			int i = 0;
			while (MyRead.Read())
			{
				string tmp = RepeatTag;
				tmp = tmp.Replace("$IndexID$", i.ToString()); i++;
				tmp = tmp.Replace("$SoftID$", MyCL.MGInt(MyRead, 0).ToString());
                tmp = CheckLength(tmp, "Name", MyCL.MGStr(MyRead, 1));
                tmp = CheckLength(tmp, "SoftTypeName", MyCL.MGStr(MyRead, 9));
				tmp = tmp.Replace("$FileSize$", MyCL.MGStr(MyRead, 11));
				tmp = tmp.Replace("$FileAddress$", GetFileLink(MyCL.MGStr(MyRead, 2), MyCL.MGStr(MyRead, 10)));
				tmp = tmp.Replace("$SoftTypeID$", MyCL.MGInt(MyRead, 3).ToString());
				tmp = tmp.Replace("$LangID$", MyCL.MGInt(MyRead, 6).ToString());
				tmp = tmp.Replace("$Hit$", MyCL.MGInt(MyRead, 5).ToString());
				tmp = tmp.Replace("$PageLink$", "/SoftWare/" + MyCL.MGInt(MyRead, 0) + "/" + Tools.UrlWordReplace(MyCL.MGStr(MyRead, 1)) + "");
				ot += tmp;
			}
			MyRead.Close(); MyRead.Dispose();
			return ot;
		}
		private static string GetFileLink(object Address, object LinkAddress)
		{
			if (Address.ToString().Trim() != "")
				return "/Files/" + Tools.GetViewUnitID + "/SoftWare/" + Address.ToString().Trim();
			else if (LinkAddress.ToString().Trim() != "")
				return LinkAddress.ToString().Trim();
			else
				return "";
		}
		private static string GetGalleryStr(int ItemTypeID, string RepeatTag, int CNT, int Sort, string WhereComm)
		{
			string ot = "";
			//GalleryID$GalleryName$Hit$LangID$GalleryTypeName$GalleryTypeID$ImageSRC$ImageTAG$PageLink
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT TOP (" + Tools.ConvertToInt32(CNT.ToString()) + ")  Gallery.GalleryID, Gallery.Name, Gallery.[Desc], Gallery.GalleryTypeID, Gallery.UnitID, Gallery.Hit, Gallery.LangID, Gallery.Disable, GalleryType.Name AS typename FROM (SELECT *, ROW_NUMBER() OVER(PARTITION BY Gallerytypeid ORDER BY Galleryid DESC) rn FROM Gallery) AS Gallery inner join Gallerytype on Gallery.Gallerytypeid = Gallerytype.Gallerytypeid WHERE rn = 1 and (Gallery.UnitID = " + Tools.GetViewUnitID + ") AND (Gallery.Disable = 0) AND (Gallery.LangID = " + Tools.LangID + ") " + (ItemTypeID == 0 ? "" : " AND (Gallery.GalleryTypeID=" + ItemTypeID + ")") + "  ORDER BY Gallery.GalleryID " + (Sort == 0 ? "desc" : ""));
			int i = 0; 
			while (MyRead.Read())
			{
				string tmp = RepeatTag;
				tmp = tmp.Replace("$IndexID$", i.ToString()); i++;
				tmp = tmp.Replace("$GalleryID$", MyCL.MGInt(MyRead, 0).ToString());
                tmp = CheckLength(tmp, "GalleryName", MyCL.MGStr(MyRead, 2));
				tmp = tmp.Replace("$GalleryTypeID$", MyCL.MGInt(MyRead, 3).ToString());
				tmp = tmp.Replace("$LangID$", MyCL.MGInt(MyRead, 6).ToString());
				tmp = tmp.Replace("$Hit$", MyCL.MGInt(MyRead, 5).ToString());
				
				tmp = tmp.Replace("$ImageTag$", GetImagePicture(MyCL.MGStr(MyRead, 8)));
				tmp = tmp.Replace("$ImageSRC$", "/Files/" + Tools.GetViewUnitID + "/Images/Gallery/" + MyCL.MGStr(MyRead, 1));
			
				tmp = tmp.Replace("$PageLink$", "/Gallery/" + MyCL.MGInt(MyRead, 0) + "/" + Tools.UrlWordReplace(MyCL.MGStr(MyRead,1)) + "");
				ot += tmp;
			}
			MyRead.Close(); MyRead.Dispose();
			return ot;
		}
		private static string GetImagePicture(string PicName)
		{
			if (PicName.Trim() == "")
				return "";
			return "<img class=\"GalleryPageImage\" src=\"/Files/" + Tools.GetViewUnitID + "/Images/Gallery/" + PicName + "\" />";
		}
		private static string GetSoundStr(int ItemTypeID, string RepeatTag, int CNT, int Sort, string WhereComm)
		{
			string ot = "";
			//SoundID$Name$SoundAddress$SoundTypeID$SoundTypeName$Hit$LangID$PageLink
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT TOP (" + Tools.ConvertToInt32(CNT.ToString()) + ") Sound.SoundID, Sound.Name, Sound.MovAddress, Sound.SoundTypeID, Sound.UnitID, Sound.Hit, Sound.LangID, Sound.Disable, SoundType.Name AS typename FROM Sound INNER JOIN SoundType ON Sound.SoundTypeID = SoundType.SoundTypeID WHERE (Sound.UnitID = " + Tools.GetViewUnitID + ") AND (Sound.Disable = 0) AND (Sound.LangID = " + Tools.LangID + ") " + (ItemTypeID == 0 ? "" : " AND (Sound.SoundTypeID=" + ItemTypeID + ")") + "  ORDER BY Sound.SoundID " + (Sort == 0 ? "desc" : ""));
			int i = 0; 
			while (MyRead.Read())
			{
				string tmp = RepeatTag;
				tmp = tmp.Replace("$IndexID$", i.ToString()); i++;
				tmp = tmp.Replace("$SoundID$", MyCL.MGInt(MyRead, 0).ToString());
                tmp = CheckLength(tmp,"Name", MyCL.MGStr(MyRead, 1));
                tmp = CheckLength(tmp,"SoundTypeName", MyCL.MGStr(MyRead, 8));
				tmp = tmp.Replace("$SoundAddress$", GetSoundAddress(MyCL.MGStr(MyRead, 2)));
				tmp = tmp.Replace("$SoundTypeID$", MyCL.MGInt(MyRead, 3).ToString());
				tmp = tmp.Replace("$LangID$", MyCL.MGInt(MyRead, 6).ToString());
				tmp = tmp.Replace("$Hit$", MyCL.MGInt(MyRead, 5).ToString());
				tmp = tmp.Replace("$PageLink$", "/Sounds/" + MyCL.MGInt(MyRead, 0) + "/" + Tools.UrlWordReplace(MyCL.MGStr(MyRead, 1)) + "");
				ot += tmp;
			}
			MyRead.Close(); MyRead.Dispose();
			return ot;
		}
		private static string GetSoundAddress(string Address)
		{
			if (Address.ToLower().IndexOf("http://") == -1)
				return "/Files/" + Tools.GetViewUnitID + "/Sounds/" + Address;
			return Address;
		}
		private static string GetMovieStr(int ItemTypeID, string RepeatTag, int CNT, int Sort, string WhereComm)
		{
			string ot = "";
			//MovieID$Name$MovieTypeName$MovAddress$ImageSRC$ImageTag$Hit$LangID$MovieTypeID$PageLink
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT TOP (" + Tools.ConvertToInt32(CNT.ToString()) + ") Movie.MovieID, Movie.Name, Movie.MovAddress, Movie.MovieTypeID, Movie.UnitID, Movie.Hit, Movie.LangID, Movie.Disable, Movie.PicName, MovieType.Name AS typename FROM Movie INNER JOIN MovieType ON Movie.MovieTypeID = MovieType.MovieTypeID WHERE (Movie.UnitID = " + Tools.GetViewUnitID + ") AND (Movie.Disable = 0) AND (Movie.LangID = " + Tools.LangID + ") " + (ItemTypeID == 0 ? "" : " AND (Movie.MovieTypeID=" + ItemTypeID + ")") + "  ORDER BY Movie.MovieID " + (Sort == 0 ? "desc" : ""));
			int i = 0; 
			while (MyRead.Read())
			{
				string tmp = RepeatTag;
				tmp = tmp.Replace("$IndexID$", i.ToString()); i++;
				tmp = tmp.Replace("$MovieID$", MyCL.MGInt(MyRead, 0).ToString());
                tmp = CheckLength(tmp,"Name", MyCL.MGStr(MyRead, 1));
                tmp = CheckLength(tmp,"MovieTypeName", MyCL.MGStr(MyRead, 9));
				tmp = tmp.Replace("$MovAddress$", GetMovieAddress(MyCL.MGStr(MyRead, 2)));
				tmp = tmp.Replace("$MovieTypeID$", MyCL.MGInt(MyRead, 3).ToString());
				tmp = tmp.Replace("$LangID$", MyCL.MGInt(MyRead,6).ToString());
				tmp = tmp.Replace("$Hit$", MyCL.MGInt(MyRead, 5).ToString());
				tmp = tmp.Replace("$ImageTag$", GetMoviePicture(MyCL.MGStr(MyRead, 8)));
				tmp = tmp.Replace("$ImageSRC$", "/Files/" + Tools.GetViewUnitID + "/Images/Movies/" + MyCL.MGStr(MyRead, 8));
				tmp = tmp.Replace("$PageLink$", "/Movies/" + MyCL.MGInt(MyRead, 0) + "/" + Tools.UrlWordReplace(MyCL.MGStr(MyRead, 1)) + "");
				ot += tmp;
			}
			MyRead.Close(); MyRead.Dispose();
			return ot;
		}
		private static string GetMoviePicture(string PicName)
		{
			if (PicName.Trim() == "")
				return "";
			return "<img class=\"MoviePageImage\" src=\"/Files/" + Tools.GetViewUnitID + "/Images/Movies/" + PicName + "\" />";
		}
		private static string GetMovieAddress(string Address)
		{
			if (Address.ToLower().IndexOf("http:/") == -1)
				return "/Files/" + Tools.GetViewUnitID + "/Movies/" + Address;
			return Address;
		}
		private static string GetNewsStr(int ItemTypeID, string RepeatTag, int CNT, int Sort, string WhereComm,int StartFrom)
		{
			//NewsID$Title$ONTitle$Chekide$NewsTypeName$ImageSRC$Date$NewsTypeID$Ref$PageLink
			string ot = "";
			string SortStr = " ORDER BY  News.NewsID " + (Sort == 0 ? "desc" : "");
			if (Sort == 2 && StartFrom==0)
				SortStr = " order by newid() ";
            if (Sort == 3)
                SortStr = " order by News.hit desc ";
            SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT  News.NewsID, News.Title, News.Chekide, News.News, News.Ref, News.Tar, News.Hit, News.Type, News.ONTitle, (SELECT TOP (1) '" + DAL.CheckData.GetFilesRoot() + "/Images/News/' + RTRIM(LTRIM(NewsID)) + '/' + FileName AS Expr1 FROM Media WHERE (NewsID = News.NewsID) AND (DefView = 1)) AS FileName,NewsSubject.Subject,news.langid FROM News INNER JOIN NewsSubject ON News.Type = NewsSubject.NewsSubjectID WHERE (News.UnitID = " + Tools.GetViewUnitID + ") AND (News.Disable = 0) AND (News.[view] =1) AND (News.LangID = " + Tools.LangID + ") " + (ItemTypeID == 0 ? "" : " AND (NewsSubject.NewsSubjectID=" + ItemTypeID + ")") + "  "+SortStr+ " OFFSET "+StartFrom+" ROWS FETCH NEXT "+ Tools.ConvertToInt32(CNT.ToString()) + " ROWS ONLY ", null,true);
			int i = 0; 
			while (MyRead.Read())
			{
				string tmp = RepeatTag;
				tmp = tmp.Replace("$IndexID$", i.ToString()); i++;
				tmp = tmp.Replace("$SubjectName$", DAL.ExecuteData.CNTDataStr("SELECT TOP (1) NewsCategory.Name FROM NewsCategoryItem INNER JOIN NewsCategory ON NewsCategoryItem.NewsCategoryID = NewsCategory.NewsCategoryID  WHERE (NewsCategoryItem.NewsID = "+MyCL.MGInt(MyRead, 0)+")"));
				tmp = tmp.Replace("$NewsID$", MyCL.MGInt(MyRead, 0).ToString());
                tmp = CheckLength(tmp,"Title", MyCL.MGStr(MyRead,1));
                tmp = CheckLength(tmp,"Chekide", MyCL.MGStr(MyRead, 2));
                tmp = CheckLength(tmp,"ONTitle", MyCL.MGStr(MyRead, 8));
				tmp = tmp.Replace("$Ref$", MyCL.MGStr(MyRead, 4));
				tmp = tmp.Replace("$NewsTypeName$", MyCL.MGStr(MyRead, 10));
				tmp = tmp.Replace("$NewsTypeID$", MyCL.MGInt(MyRead,7).ToString());
				tmp = tmp.Replace("$Date$",Calender.MyPDate(MyCL.MGDTDT(MyRead, 5)));
				tmp = tmp.Replace("$DateDay$", Calender.GetPersianDate(MyCL.MGDTDT(MyRead, 5))[2].ToString());
				tmp = tmp.Replace("$DateMonth$", Calender.GetPersianDate(MyCL.MGDTDT(MyRead, 5))[1].ToString());
				tmp = tmp.Replace("$DateMonthName$",Calender.FarMonth[Calender.GetPersianDate(MyCL.MGDTDT(MyRead, 5))[1]].ToString());
				tmp = tmp.Replace("$DateYear$", Calender.GetPersianDate(MyCL.MGDTDT(MyRead, 5))[0].ToString());
				tmp = tmp.Replace("$LangID$", MyCL.MGInt(MyRead,11).ToString());
				tmp = tmp.Replace("$Hit$", MyCL.MGInt(MyRead, 6).ToString());
				tmp = tmp.Replace("$ImageSRC$",GetPic( MyCL.MGStr(MyRead,9)));
				tmp = tmp.Replace("$PageLink$", "/News/" + MyCL.MGInt(MyRead, 0) + "/" + Tools.UrlWordReplace(MyCL.MGStr(MyRead, 1)) + "");
				ot += tmp;  
			}
			MyRead.Close(); MyRead.Dispose();
			return ot;
		}
		private static string GetPic(string PicAddress)
		{
			if (PicAddress.Trim() != "")
				return PicAddress;
			else
				return Tools.GetSetting(461, "/Files/83/nopic.png");
		}
		private static string GetArticleStr(int ItemTypeID, string RepeatTag, int CNT, int Sort, string WhereComm)
		{
			//ArticleID$ArticleTypeID$Title$Chekide$LangID$Hit$ArticleTypeName$ImageSRC$PageLink
			string ot = "";
			string SortStr = " ORDER BY  Article.ArticleID " + (Sort == 0 ? "desc" : "");
			if (Sort == 2)
				SortStr = " order by newid() ";
            if (Sort == 3)
                SortStr = " order by Article.hit desc ";
            SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT TOP (" + Tools.ConvertToInt32(CNT.ToString()) + ") Article.ArticleID, Article.ArticleTypeID, Article.Title, Article.Chekide, Article.Image, Article.LangID, Article.UnitID, Article.Hit, Article.Type, Article.Disable, Article.Sort, ArticleType.Name FROM Article INNER JOIN ArticleType ON Article.ArticleTypeID = ArticleType.ArticleTypeID  WHERE (Article.UnitID = " + Tools.GetViewUnitID + ") AND (Article.Disable = 0) AND (Article.LangID = " + Tools.LangID + ") " + (ItemTypeID == 0 ? "" : " AND (Article.ArticleTypeID=" + ItemTypeID + ")") + " "+SortStr);
			int i = 0; 
			while (MyRead.Read())
			{
				string tmp = RepeatTag;
				tmp = tmp.Replace("$IndexID$", i.ToString()); i++;
				tmp = tmp.Replace("$ArticleID$", MyCL.MGInt(MyRead, 0).ToString());
				tmp = tmp.Replace("$ArticleTypeID$", MyCL.MGInt(MyRead, 1).ToString());
                tmp = CheckLength(tmp,"Title", MyCL.MGStr(MyRead, 2));
                tmp = CheckLength(tmp, "Chekide", MyCL.MGStr(MyRead, 3));
				tmp = tmp.Replace("$LangID$", MyCL.MGInt(MyRead, 5).ToString());
				tmp = tmp.Replace("$Hit$", MyCL.MGInt(MyRead, 7).ToString());
				tmp = tmp.Replace("$ArticleTypeName$", MyCL.MGStr(MyRead, 11));
				tmp = tmp.Replace("$ImageSRC$", "/Files/" + Tools.GetViewUnitID + "/ArticlePic/" + MyCL.MGStr(MyRead, 4));
				tmp = tmp.Replace("$PageLink$", "/Article/" + MyCL.MGInt(MyRead, 0) + "/" + Tools.UrlWordReplace(MyCL.MGStr(MyRead, 2)) + "");
				ot += tmp;
			}
			MyRead.Close(); MyRead.Dispose();
			return ot;
		}

        public static string CheckLength(string tmp, string ItemIndex, string ItemVal)
        {
            if (Regex.IsMatch(tmp, @"\$" + ItemIndex + @":\d+\$"))
            {
                Match match = Regex.Match(tmp, @"\$" + ItemIndex + @":\d+\$");
                if (match.Success)
                {
                    string va = match.Groups[0].Value;
                    int mylen =Tools.ConvertToInt32(va.Substring(va.IndexOf(":")+1).Replace("$",""));
                    tmp = Regex.Replace(tmp, @"\$" + ItemIndex + @":\d+\$",Tools.SetExplainFix(ItemVal,mylen,"")+" ...");
                }               
            }
            tmp = tmp.Replace("$" + ItemIndex + "$", ItemVal);
            return tmp;
        }
    }
}
