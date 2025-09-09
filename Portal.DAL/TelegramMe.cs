using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Telegram.Bot.Types;
using Newtonsoft.Json;
using System.IO;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Data;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;
using Telegram.Bot.Types.Enums;
namespace Tools
{
	public class TelegramMe
	{
		public static MyCL.TelegramUserInfo CheckToken(string Token, int UserID)
		{
			Token = Tools.RepNumber(Token);
			string address = "https://api.telegram.org/bot" + Token + "/getMe";
			string UrlSet = "https://api.telegram.org/bot" + Token + "/setWebhook?url=https://bot.imencms.com/default-" + UserID + ".aspx";
			try
			{
				string OutText = "";
				using (WebClient client = new WebClient())
				{
					OutText = client.DownloadString(address);
				}
				System.Data.SqlClient.SqlParameterCollection SP1 = new System.Data.SqlClient.SqlCommand().Parameters;
				SP1.AddWithValue("@txt", "A" + OutText);
				DAL.ExecuteData.AddData(" INSERT INTO TelegramTemp(txt) VALUES (@txt) ", SP1);
				JObject d = JObject.Parse(OutText);

				if (!(bool)d["ok"])
					return new MyCL.TelegramUserInfo();
				else if ((bool)d["ok"])
				{
					using (WebClient client = new WebClient())
					{
						client.DownloadString(UrlSet);
					}
					MyCL.TelegramUserInfo MyUserInfo = new MyCL.TelegramUserInfo();
					MyUserInfo.FirstName = d["result"]["first_name"].ToString();
					MyUserInfo.UserName = d["result"]["username"].ToString();
					MyUserInfo.RobotID = Tools.ConvertToInt32(d["result"]["id"].ToString(), 0);
					return MyUserInfo;
				}
				else
					return new MyCL.TelegramUserInfo();
			}
			catch
			{
				return new MyCL.TelegramUserInfo();
			}
		}
		public static void SetToken(string Token, int UserID)
		{
			Token = Tools.RepNumber(Token);
			string UrlSet = "https://api.telegram.org/bot" + Token + "/setWebhook?url=https://bot.imencms.com/default-" + UserID + ".aspx";
			try
			{
				using (WebClient client = new WebClient())
				{
					client.DownloadString(UrlSet);
				}
			}
			catch
			{

			}
		}
		public static void FillAnswerType(DropDownList TypeValDL)
		{
			TypeValDL.DataSource = DAL.ViewData.MyDR1("SELECT TelegramAnswerTypeID, Name  FROM TelegramAnswerType order by sort");
			TypeValDL.DataBind();
		}
		public static string Token = "";
		public static int SendTextMessage(string InText, int TelegramUserID, string MessageID, long FromID)
		{
			Token = UserToken(TelegramUserID);
			string SMessage = "", Key = "";
			int TelegramVoteItemID = CheckVoteItem(InText, TelegramUserID);
			if (TelegramVoteItemID > 0)
			{
				SqlParameterCollection SP = new System.Data.SqlClient.SqlCommand().Parameters;
				SP.AddWithValue("@FromID", FromID);
				Key = GetKeyword(TelegramUserID);
				int VoteID = DAL.ExecuteData.CNTData("SELECT TelegramVoteID  FROM TelegramVoteItem WHERE (TelegramVoteItemID =" + TelegramVoteItemID + ")");
				int cnt = DAL.ExecuteData.CNTData("SELECT COUNT(*) AS Expr1  FROM TelegramVoteAnswer  WHERE (FromID = @FromID) AND (TelegramVoteID = " + VoteID + ")", SP);
				if (cnt > 0)
				{
					SMessage = "شما یک بار دیگر در این مسابقه / نظرسنجی شرکت کرده اید";
					SendToServerTextMessage(SMessage, Key, MessageID, TelegramUserID);
					return 1;
				}
				DAL.ExecuteData.InsertData("INSERT INTO TelegramVoteAnswer (TelegramUserID, FromID, TelegramVoteItemID,TelegramVoteID)  VALUES (" + TelegramUserID + ",@FromID," + TelegramVoteItemID + "," + VoteID + ")", SP);

				SMessage = "گزینه شما با موفقیت ثبت گردید.\n با تشکر";
				SendToServerTextMessage(SMessage, Key, MessageID, TelegramUserID);
				return 1;
			}
			else if (InText.ToLower().IndexOf("start") != -1 || Tools.ReplaceWord(InText.ToLower()).IndexOf(Tools.ReplaceWords("منوي اصلي")) != -1)
			{
				SMessage = DAL.ExecuteData.CNTDataStr("SELECT  TOP (1) WelcomeMess  FROM TelegramSetting WHERE (TelegramUserID = " + TelegramUserID + ") order by TelegramSettingID desc");
				string Serverpath = HttpContext.Current.Server.MapPath("Upload") + "\\" + TelegramUserID + "\\";
				/*
				*/
				Key = GetKeyword(TelegramUserID);
				//SendMessage(MessageType.TextMessage,Tools.ConvertToInt32(MessageID),SMessage,0)
				SendToServerTextMessage(SMessage, Key, MessageID, TelegramUserID);
				if (System.IO.File.Exists(Serverpath + "\\welcome.jpg"))
				{
					//FileToSend myfile = new FileToSend("welcome.jpg", GetFileStream(Serverpath + "\\welcome.jpg"));
					//SendMessageAsync(MessageType.PhotoMessage, MessageID, myfile, false, 0, null, null).ConfigureAwait(false);
				}
				return 1;
			}
			else if (InText.IndexOf("Estekhareh") != -1)
			{
				SMessage = GetAutoMessage(6);
				Key = GetKeyword(TelegramUserID);
				SendToServerTextMessage(SMessage, Key, MessageID, TelegramUserID);
				return 1;
			}
			else if (Tools.MySubString(InText, 0, 4).IndexOf("شهر ") != -1 || Tools.MySubString(InText, 0, 8).IndexOf("Country") != -1 || Tools.MySubString(InText, 0, 6).IndexOf("State") != -1 || Tools.MySubString(InText, 0, 5).IndexOf("City") != -1 || Tools.MySubString(InText, 0, 5).IndexOf("استان") != -1)
			{
				SMessage = GetPrayerTime(InText);
				Key = GetKeyword(TelegramUserID);
				SendToServerTextMessage(SMessage, Key, MessageID, TelegramUserID);
				return 1;
			}
			else if (Tools.MySubString(InText, 0, 4).IndexOf("/Doa") != -1)
			{
				SendDoa(InText, MessageID, TelegramUserID);
				return 1;
			}
			else if (Tools.MySubString(InText, 0, 9).IndexOf("/Calender") != -1)
			{
				SendCalender(InText, MessageID, TelegramUserID);
				return 1;
			}
			else if (Tools.MySubString(InText, 0, 10).Trim().ToLower().IndexOf("حدیث موضوع") != -1)
			{
				//if (InText.IndexOf("منوي حدیث شاخه اصلي") != -1)
				//{
				//	SMessage = "حدیث شاخه اصلی";
				//	Key = GetHadithKeyword("");
				//}
				//else
				//{
				SqlParameterCollection SP1 = new System.Data.SqlClient.SqlCommand().Parameters;
				SP1.AddWithValue("@txt", InText.Replace("حدیث", "").Replace("موضوع", "").Trim());
				string Lev = DAL.ExecuteData.CNTDataStr("SELECT [Level] FROM HadithCategory  WHERE (Name = @txt)", SP1);
				if (Lev == "")
				{
					SMessage = "چنین موضوعی وجود ندارد";
					Key = GetHadithKeyword("");
				}
				else
				{
					SMessage = GetHadith(Lev);
					Key = GetHadithKeyword(Lev);
				}
				//}
				SendToServerTextMessage(SMessage, Key, MessageID, TelegramUserID);
				return 1;
			}
			else
			{
				string GalleryPath = HttpContext.Current.Server.MapPath("Upload") + "\\" + TelegramUserID + "\\";
				System.Data.SqlClient.SqlParameterCollection SP = new System.Data.SqlClient.SqlCommand().Parameters;
				SP.AddWithValue("@txt", InText);

				SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT [Level], Type, TypeVal, TypeBody FROM TelegramAnswer WHERE (TelegramUserID = " + TelegramUserID + ") AND (Name = @txt)", SP);
				if (MyRead.Read())
				{
					int MyType = MyCL.MGInt(MyRead, 1);
					switch (MyType)
					{
						case 1:
							SMessage = MyCL.MGStr(MyRead, 3);
							string Level = MyCL.MGStr(MyRead, 0);
							MyRead.Close(); MyRead.Dispose();
							Key = GetKeyword(TelegramUserID, Level);
							SendToServerTextMessage(SMessage, Key, MessageID, TelegramUserID);
							return 1;
							//break;
						case 2:
						case 3:
						case 4://عکس و صوت و فیلم
							int TypeVal = MyCL.MGInt(MyRead, 2);
							Level = MyCL.MGStr(MyRead, 0);
							MyRead.Close(); MyRead.Dispose();
							int SendType = DAL.ExecuteData.CNTData("SELECT SendType  FROM TelegramGalleryType  WHERE (TelegramGalleryTypeID = " + TypeVal + ")");
							string CommandText = "SELECT TOP (1) Name, FileName, Disable   FROM TelegramGallery  WHERE (TelegramGalleryTypeID = " + TypeVal + ") AND (TelegramUserID = " + TelegramUserID + ") AND (Disable = 0) ORDER BY NEWID()";
							if(SendType==1)
								CommandText = "SELECT TOP (10) Name, FileName, Disable FROM TelegramGallery  WHERE (TelegramGalleryTypeID = " + TypeVal + ") AND (TelegramUserID = " + TelegramUserID + ") AND (Disable = 0) ORDER BY NEWID()";
							SqlDataReader MyRead1 = DAL.ViewData.MyDR1(CommandText, SP);
							while (MyRead1.Read())
							{
								var additionalParameters = new Dictionary<string, object>
								{
									{"caption", MyCL.MGStr(MyRead1, 0)}
								};
								if (!System.IO.File.Exists(GalleryPath + "\\" + TypeVal + "\\" + MyCL.MGStr(MyRead1, 1)))
								{
									SendToServerTextMessage("آدرس فایل مورد نظر اشتباه می باشد", Key, MessageID, TelegramUserID);
									return 1;
								}
								//FileToSend myfile = new FileToSend(MyCL.MGStr(MyRead1, 1), GetFileStream(GalleryPath + "\\" + TypeVal + "\\" + MyCL.MGStr(MyRead1, 1)));
								//SendMessageAsync(GetMessageType(MyType),MessageID, myfile, false, 0, GetIReplyMarkup(TelegramUserID,Level), additionalParameters).ConfigureAwait(false);
							}
							MyRead1.Close(); MyRead1.Dispose();
							//Key = GetKeyword(TelegramUserID, Level);
							return 1;
							//break;
						case 5://اعمال و ادعیه
							MyRead.Close(); MyRead.Dispose();
							SMessage = GetAmalToday();
							SendToServerTextMessage(SMessage, Key, MessageID, TelegramUserID);
							return 1;
							//break;
						case 6://استخاره
							SMessage = "استخاره با قرآن کریم\nابتدا سه صلوات بر محمد و آل محمد بفرستید بعد متن زیر را بخوانید\nأسْتَخْيرُ الله برحمتهِ خيرةٌ في عافيةٍ ، یا مَن یَعلم اِهدِ مَن لا یَعلَم\n نیت کرده /Estekhareh را لمس نمایید";
							MyRead.Close(); MyRead.Dispose();
							SendToServerTextMessage(SMessage, Key, MessageID, TelegramUserID);
							return 1;
							//break;
						case 7://حافظ
							SMessage = "بیتی از حافظ: \n" + GetAutoMessage(7);
							MyRead.Close(); MyRead.Dispose();
							SendToServerTextMessage(SMessage, Key, MessageID, TelegramUserID);
							return 1;
							//break;
						case 8://شعرا
							SMessage = GetAutoMessage(8);
							MyRead.Close(); MyRead.Dispose();
							SendToServerTextMessage(SMessage, Key, MessageID, TelegramUserID);
							return 1;
							//break;
						case 9://حدیث
							SMessage = GetAutoMessage(9);
							MyRead.Close(); MyRead.Dispose();
							SendToServerTextMessage(SMessage, Key, MessageID, TelegramUserID);
							return 1;
							//break;
						case 10://مناسبت
							MyRead.Close(); MyRead.Dispose();

							int[] arabd = Calender.GetArabicDate(DateTime.Now);
							int[] engd = Calender.GetEngDate(DateTime.Now);
							int[] perd = Calender.GetPersianDate(DateTime.Now);
							SMessage = "امروز " + perd[2] + " " + Calender.FarMonth[perd[1]] + " " + perd[0] + "\n" + arabd[2] + " " + Calender.AraMonth[arabd[1]] + " " + arabd[0] + " هجري قمري \n " + engd[2] + " " + Calender.EngMonth[engd[1]] + " " + engd[0] + " ميلادي \n";
							SqlDataReader MyCall = DAL.ViewData.MyDR1("SELECT id, title,Type, DayType FROM Calender WHERE (Type = 1) AND (Month = " + perd[1] + ") AND (Day = " + perd[2] + ") OR (Type = 2) AND (Month = " + arabd[1] + ") AND (Day = " + arabd[2] + ") OR (Type = 3) AND (Month = " + engd[1] + ") AND (Day = " + engd[2] + ") ORDER BY Type, myOrder");
							while (MyCall.Read())
							{
								SMessage += "" + MyCL.MGStr(MyCall, 1) + " /Calender" + MyCL.MGInt(MyCall, 0) + "\n";
							}
							MyRead.Close(); MyRead.Dispose();
							SendToServerTextMessage(SMessage, Key, MessageID, TelegramUserID);

							return 1;
							//break;
						case 11://آیه قرآن
							SMessage = GetAutoMessage(11);
							MyRead.Close(); MyRead.Dispose();
							SendToServerTextMessage(SMessage, Key, MessageID, TelegramUserID);
							return 1;
							//break;
						case 12://سخنان بزرگان
							SMessage = GetAutoMessage(12);
							MyRead.Close(); MyRead.Dispose();
							SendToServerTextMessage(SMessage, Key, MessageID, TelegramUserID);
							return 1;
							//break;
						case 13://متن
							TypeVal = MyCL.MGInt(MyRead, 2);
							MyRead.Close(); MyRead.Dispose();
							string AnswerBody = GetTextGallery(TypeVal);
							//SendMessageAsync(MessageType.TextMessage, MessageID, AnswerBody, false, 0).ConfigureAwait(false);
							return 1;
							//break;
						case 14://موقعیت
							TypeVal = MyCL.MGInt(MyRead, 2);
							MyRead.Close(); MyRead.Dispose();
							MyRead1 = DAL.ViewData.MyDR1("SELECT Lat, Lam  FROM TelegramPosition WHERE (TelegramPositionID = " + TypeVal + ") AND (TelegramUserID = " + TelegramUserID + ")", SP);
							if (MyRead1.Read())
							{

								//Location MyLoc = new Location();
								//MyLoc.Latitude = float.Parse(MyCL.MGStr(MyRead1, 0));
								//MyLoc.Longitude = float.Parse(MyCL.MGStr(MyRead1, 1));
								//SendMessage(MessageType.LocationMessage, Tools.ConvertToInt32(MessageID), MyLoc, 0, null, null).ConfigureAwait(false);
								SendToServerMessage(TelegramUserID, "sendLocation?longitude=" + MyCL.MGStr(MyRead1, 1) + "&latitude=" + MyCL.MGStr(MyRead1, 0) + "&chat_id=" + MessageID);
							}
							MyRead1.Close(); MyRead1.Dispose();
							return 1;
							//break;
						case 15://مشخصات تماس
							TypeVal = MyCL.MGInt(MyRead, 2);
							MyRead.Close(); MyRead.Dispose();
							MyRead1 = DAL.ViewData.MyDR1("SELECT Name, Family, UserID, PhoneNo  FROM TelegramContact  WHERE (TelegramContactID =" + TypeVal + ") AND (TelegramUserID =" + TelegramUserID + ")", SP);
							if (MyRead1.Read())
							{
								//Contact MyContact = new Contact();
								//MyContact.FirstName = MyCL.MGStr(MyRead1, 0);
								//MyContact.LastName = MyCL.MGStr(MyRead1, 1);
								//MyContact.PhoneNumber = MyCL.MGStr(MyRead1, 3);
								//MyContact.UserId = MyCL.MGInt(MyRead1, 2);

								//SendMessage(MessageType.ContactMessage, Tools.ConvertToInt32(MessageID), MyContact, 0, null, null).ConfigureAwait(false);
							}
							MyRead1.Close(); MyRead1.Dispose();
							return 1;
							//break;
						case 16://فایل
							TypeVal = MyCL.MGInt(MyRead, 2);
							MyRead.Close(); MyRead.Dispose();
							MyRead1 = DAL.ViewData.MyDR1("SELECT TOP (1) Name, FileName, Disable   FROM TelegramGallery  WHERE (TelegramGalleryTypeID = " + TypeVal + ") AND (TelegramUserID = " + TelegramUserID + ") AND (Disable = 0) ORDER BY NEWID()", SP);
							if (MyRead1.Read())
							{
								var additionalParameters = new Dictionary<string, object>
								{
									{"caption", MyCL.MGStr(MyRead1, 0)}
								};
								//FileToSend myfile = new FileToSend(MyCL.MGStr(MyRead1, 1), GetFileStream(GalleryPath + "\\" + TypeVal + "\\" + MyCL.MGStr(MyRead1, 1)));
								//SendMessageAsync(MessageType.DocumentMessage, MessageID, myfile, false, 0, null, additionalParameters).ConfigureAwait(false);
							}
							MyRead1.Close(); MyRead1.Dispose();
							return 1;
							//break;
						case 17://اوقات شرعی
							SMessage = "بخش اوقات شرعی\n \n شما می توانید به طور مثال با ارسال دستور \n شهر تهران \n اوقات شرعی آن شهر را ببینید. یا با ارسال \n استان تهران\n لیست شهرهای استان تهران را ببینید\n برای دریافت لیست کشورها دستور /Country را وارد نمایید\n برای گرفتن اوقات شرعی فردا می توانید به عنوان مثال دستور زیر را ارسال نمایید:\n شهر تهران فردا";
							MyRead.Close(); MyRead.Dispose();
							SendToServerTextMessage(SMessage, Key, MessageID, TelegramUserID);

							return 1;
							//break;
						case 18://ارسال حدیث موضوعی
							SMessage = "یک موضوع انتخاب نمایید";

							MyRead.Close(); MyRead.Dispose();
							Key = GetHadithKeyword("");
							SendToServerTextMessage(SMessage, Key, MessageID, TelegramUserID);
							return 1;
							//break;
						case 19://فال روزانه
							MyRead.Close(); MyRead.Dispose();
							GetFallMessage(Key, MessageID, TelegramUserID);

							return 1;
							//break;

						case 20://نظرسنجی و مسابقه
							int VoteVal = MyCL.MGInt(MyRead, 2);

							MyRead.Close(); MyRead.Dispose();
							GetVoteMessage(VoteVal, MessageID, TelegramUserID);

							return 1;
							//break;
						case 21://RSS
							int RssTypeVal = MyCL.MGInt(MyRead, 2);
							MyRead.Close(); MyRead.Dispose();
							Key = GetKeyword(TelegramUserID);
							GetRSSText(RssTypeVal, TelegramUserID, Key, MessageID);

							return 1;
							//break;

						default:
							MyRead.Close(); MyRead.Dispose();
							return 0;
							//break;
					}
				}
				else
				{
					MyRead.Close(); MyRead.Dispose();
					if (Tools.ConvertToInt64(MessageID) < 0)
					{
						return 0;
					}
					SMessage = "پاسخ مناسبی وارد نشده است \n ImenCMS.com سیستم مدیریت ربات تلگرام";
					Key = GetKeyword(TelegramUserID);
					SendToServerTextMessage(SMessage, Key, MessageID, TelegramUserID);
					MyRead.Close(); MyRead.Dispose();
					return 0;
				}
			}
		}

		/*private static IReplyMarkup GetIReplyMarkup(int TelegramUserID,string lev)
		{

			/*
			string Keyw = "keyboard: [";
			string CommandTXT = "";
			string RootMenu = "";
			if (lev == "")
				CommandTXT = "SELECT Name  FROM TelegramAnswer  WHERE (TelegramUserID = " + TelegramUserID + ") AND (LEN([Level]) = 2) ORDER BY TelegramAnswerID";
			else
			{
				CommandTXT = "SELECT Name  FROM TelegramAnswer  WHERE (TelegramUserID = " + TelegramUserID + ") AND [level] like '" + lev + "%' AND (LEN([Level]) = " + ((lev.Length) + 2) + ") ORDER BY TelegramAnswerID";
				RootMenu = ",[\"منوي اصلي\"]";
			}

			int MenuCNT = DAL.ExecuteData.CNTData("SELECT MenuCNT  FROM TelegramUser  WHERE (TelegramUserID = " + TelegramUserID + ")");
			if (MenuCNT <= 0)
				MenuCNT = 1;
			
			
			
			int i = 0; string Item = "";
			SqlDataReader MyRead = DAL.ViewData.MyDR1(CommandTXT);
			while (MyRead.Read())
			{
				Item += "\"" + MyCL.MGStr(MyRead, 0) + "\",";
				i++;
				if (i == MenuCNT)
				{
					i = 0;
					Keyw += "[" + Item.TrimEnd(',') + "],";
					Item = "";
				}
			}
			MyRead.Close(); MyRead.Dispose();
			if (Item != "")
			{
				if (RootMenu == "")
					Keyw += "[" + Item.TrimEnd(',') + "],";
				else
				{
					Keyw += "[" + Item.TrimEnd(',') + ",\"منوي اصلي\"],";
					RootMenu = "";
				}
			}
			if (Keyw == "")
				return null;



		
			string Menu = (Keyw.TrimEnd(',') + RootMenu + "]");

			InlineKeyboardMarkup markup = new InlineKeyboardMarkup();
			markup..bu.row("A", "B", "C")
	                                           .row("D")
	                                           .row("E", "F")
	                                           .build();

			rkm.Keyboard =
				new KeyboardButton[][]
				{
			

			return null;
		}*/
		private static void GetRSSText(int RssTypeVal, int TelegramUserID, string Key, string MessageID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@TelegramRssTypeID", RssTypeVal);
			string URL = DAL.ExecuteData.CNTDataStr("SELECT Address from TelegramRssType where telegramuserid=" + TelegramUserID + " and TelegramRssTypeID=@TelegramRssTypeID", SP);
			string CurItem = DAL.ExecuteData.CNTDataStr("SELECT Items from TelegramRssType where telegramuserid=" + TelegramUserID + " and TelegramRssTypeID=@TelegramRssTypeID", SP);
			int Rand = DAL.ExecuteData.CNTData("SELECT Random from TelegramRssType where telegramuserid=" + TelegramUserID + " and TelegramRssTypeID=@TelegramRssTypeID", SP);
			if (URL != "")
			{
				try
				{
					RssReader rssReader = new RssReader();
					RssFeed feed = rssReader.Retrieve(URL);
					for (int i = 0; i < Rand + 1; i++)
					{
						string Items = CurItem;
						if (feed.ErrorMessage == null || feed.ErrorMessage == "")
						{
							if (feed.Items[i].Author != null)
							{
								Items = Items.Replace("Author", feed.Items[i].Author);
							}
							if (feed.Items[i].Comments != null)
							{
								Items = Items.Replace("Comments", feed.Items[i].Comments);
							}
							if (feed.Items[i].Description != null)
							{
								Items = Items.Replace("Description", feed.Items[i].Description);
							}
							if (feed.Items[i].Guid != null)
							{
								Items = Items.Replace("Guid", feed.Items[i].Guid);
							}
							if (feed.Items[i].Link != null)
							{
								Items = Items.Replace("Link", feed.Items[i].Link);
							}
							if (feed.Items[i].Pubdate != null)
							{
								Items = Items.Replace("Pubdate", feed.Items[i].Pubdate);
							}
							if (feed.Items[i].Title != null)
							{
								Items = Items.Replace("Title", feed.Items[i].Title);
							}
							Items = Items.Replace("{A}{A}", "{A}").Replace("{A}{A}", "{A}").Replace("{A}{A}", "{A}").Replace("{A}{A}", "{A}").Replace("{A}{A}", "{A}").Replace("{A}{A}", "{A}").Replace("{A}{A}", "{A}").Replace("{A}{A}", "{A}").Replace("{A}", "\n");
							SendToServerTextMessage(Items, Key, MessageID.ToString(), TelegramUserID);
						}
					}
				}
				catch { }
			}
		}

		private static int CheckVoteItem(string InText, int TelegramUserID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@InText", InText);
			SP.AddWithValue("@TelegramUserID", TelegramUserID);
			return DAL.ExecuteData.CNTData("SELECT TelegramVoteItemID  FROM TelegramVoteItem WHERE (TelegramUserID = @TelegramUserID) AND (Name = @InText)", SP);
		}

		private static void GetVoteMessage(int VoteID, string MessageID, int TelegramUserID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@Key", VoteID);
			string VoteText = DAL.ExecuteData.CNTDataStr("SELECT name  FROM TelegramVote WHERE (TelegramVoteID = @Key)", SP);
			SendToServerTextMessage(VoteText, GetVoteKeyworrd(VoteID), MessageID, TelegramUserID);
		}

		private static string GetVoteKeyworrd(int VoteID)
		{
			string Keyw = "&reply_markup={\"keyboard\":[";
			string RootMenu = "";
			RootMenu = ",[\"منوي اصلي\"]";

			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT Name   FROM TelegramVoteItem WHERE (TelegramVoteID = " + VoteID + ")");
			while (MyRead.Read())
			{
				Keyw += "[\"" + MyCL.MGStr(MyRead, 0) + "\"],";
			}
			MyRead.Close(); MyRead.Dispose();
			if (Keyw == "&reply_markup={\"keyboard\":[")
				return "";
			return Keyw.TrimEnd(',') + RootMenu + "]}";
		}

		private static void GetFallMessage(string Key, string MessageID, int TelegramUserID)
		{
			SqlDataReader MyDR = DAL.ViewData.MyDR1("SELECT ForMon, txt  FROM TelegramFalDaily  WHERE (Day = " + Calender.GetPersianDate(DateTime.Now)[2] + ") AND (Mon = " + Calender.GetPersianDate(DateTime.Now)[1] + ") order by formon");
			while (MyDR.Read())
			{
				string RetMess = "فال روزانه مورخه : " + Calender.GetPersianDateNumber(DateTime.Now) + "\n";
				RetMess += "متولدین ماه " + Calender.FarMonth[MyCL.MGInt(MyDR, 0)] + " \n";
				RetMess += MyCL.MGStr(MyDR, 1) + " \n\n\n";
				SendToServerTextMessage(RetMess, Key, MessageID, TelegramUserID);
			}
			MyDR.Dispose(); MyDR.Dispose();
		}

		private static void SendCalender(string InText, string MessageID, int TelegramUserID)
		{
			string OutText = "";
			int CallID = Tools.ConvertToInt32(InText.Replace("/Calender", ""), -1);
			if (CallID == -1)
				return;
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@ID", CallID);
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT title, comment, myOrder, Day, Month, Type  FROM Calender WHERE (id = @ID)", SP);
			if (MyRead.Read())
			{
				switch (MyCL.MGInt(MyRead, 5))
				{
					case 1:
						OutText = MyCL.MGInt(MyRead, 3) + " " + Calender.FarMonth[MyCL.MGInt(MyRead, 4)] + "\n";
						break;
					case 2:
						OutText = MyCL.MGInt(MyRead, 3) + " " + Calender.AraMonth[MyCL.MGInt(MyRead, 4)] + "\n";
						break;
					case 3:
						OutText = MyCL.MGInt(MyRead, 3) + " " + Calender.EngMonth[MyCL.MGInt(MyRead, 4)] + "\n";
						break;
					default:
						break;
				}

				OutText += MyCL.MGStr(MyRead, 0) + "\n";
				OutText += MyCL.MGStr(MyRead, 1) + "\n";

			}
			MyRead.Close(); MyRead.Dispose();
			SendToServerTextMessage(OutText, "", MessageID, TelegramUserID);

		}

		private static void SendDoa(string InText, string MessageID, int TelegramUserID)
		{
			//string OutText = "";
			int DoaID = Tools.ConvertToInt32(InText.Replace("/Doa", ""), -1);
			if (DoaID == -1)
				return;
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@DoaID", DoaID);
			string DoaName = DAL.ExecuteData.CNTDataStr("SELECT Title FROM Doa  WHERE (DoaID =@DoaID)", SP) + "\n";
			string Serverpath = HttpContext.Current.Server.MapPath("Doa");
			if (!System.IO.File.Exists(Serverpath + "\\" + DoaID + ".html"))
			{
				StreamWriter MyOut = new StreamWriter(Serverpath + "\\" + DoaID + ".html", false, Encoding.UTF8);
				MyOut.WriteLine("<html><head><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"/> <title>" + DoaName + "</title></head><body>");
				MyOut.WriteLine("<div style=\"text-align:center;padding:2px;margin:0 auto;width:100%;direction:rtl;\">" + DoaName + "</div>");
				SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT Text, Trans, Type FROM DoaItem  WHERE (DoaID = @DoaID)  ORDER BY Sort", SP);
				while (MyRead.Read())
				{
					MyOut.WriteLine("<div style=\"text-align:center;padding:2px;margin:0 auto;color:green;font-weight:bold;width:100%;direction:rtl\">" + MyCL.MGStr(MyRead, 0).Replace("{%}", " ") + "</div>");
					MyOut.WriteLine("<div style=\"text-align:center;padding:2px;margin:0 auto;width:100%;direction:rtl\">" + MyCL.MGStr(MyRead, 1).Replace("{%}", " ") + "</div>");
				}
				MyOut.WriteLine("</body></html>");
				MyOut.Close();
				MyRead.Close(); MyRead.Dispose();
			}
			var additionalParameters = new Dictionary<string, object>
								{
									{"caption", DoaName}
								};
			//FileToSend myfile = new FileToSend(DoaID + ".html", GetFileStream(Serverpath + "\\" + DoaID + ".html"));
			//SendMessageAsync(MessageType.DocumentMessage, MessageID, myfile, false, 0, null, additionalParameters).ConfigureAwait(false);

		}
		private static string GetAmalToday()
		{
			string OutText = "";
			int[] Hijri = Calender.GetArabicDate(DateTime.Now);
			OutText += "امروز مصادف است با " + Hijri[2] + " ماه " + Calender.AraMonth[Hijri[1]] + "\n اعمال امروز:\n";
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT DoaTime.DoaTimeID, DoaTime.DoaID, Doa.Title  FROM DoaTime INNER JOIN Doa ON DoaTime.DoaID = Doa.DoaID  WHERE (DoaTime.TimeType = 1) AND (DoaTime.Daily = " + (int)DateTime.Now.DayOfWeek + ")  UNION  SELECT DoaTime_1.DoaTimeID, DoaTime_1.DoaID, Doa_1.Title  FROM DoaTime AS DoaTime_1 INNER JOIN Doa AS Doa_1 ON DoaTime_1.DoaID = Doa_1.DoaID  WHERE (DoaTime_1.TimeType = 2) AND (DoaTime_1.Month = " + Hijri[1] + ") AND (DoaTime_1.MonDay = " + Hijri[2] + " OR DoaTime_1.MonDay = - 1)");
			while (MyRead.Read())
			{
				OutText += "" + MyCL.MGStr(MyRead, 2) + " /Doa" + MyCL.MGInt(MyRead, 1) + "\n";
			}
			MyRead.Close(); MyRead.Dispose();
			return OutText;
		}
		private static string GetHadith(string Lev)
		{
			string OutText = "";
			int CatID = DAL.ExecuteData.CNTData("SELECT HadithCategoryID  FROM HadithCategory WHERE ([Level] = '" + Lev + "')");
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT TOP (1) Title, HadithCategoryIDs, Subject, Matn, Trans, Ref  FROM Hadith  WHERE (HadithCategoryIDs = " + CatID + ")  ORDER BY NEWID()");
			while (MyRead.Read())
			{
				OutText += "" + MyCL.MGStr(MyRead, 2) + "\n\n";
				OutText += MyCL.MGStr(MyRead, 3) + "\n";
				OutText += MyCL.MGStr(MyRead, 4) + "\n\n";
				OutText += MyCL.MGStr(MyRead, 5) + "\n";
			}
			MyRead.Close(); MyRead.Dispose();
			if (OutText.Trim() == "")
				return "یک شاخه انتخاب نمایید";
			return OutText;
		}
		private static string GetPrayerTime(string InText)
		{
			InText = InText.Trim('/');
			string RetMess = "";
			DateTime MyDate = DateTime.Now;
			if (InText.IndexOf("شهر") != -1)
			{
				if (InText.IndexOf("فردا") != -1)
					MyDate = MyDate.AddDays(1);
				InText = InText.Replace("شهر", "").Replace("فردا", "").Trim();
				SqlParameterCollection SP = new SqlCommand().Parameters;
				SP.AddWithValue("@Name", InText);
				DataTable MyRead = DAL.ViewData.MyDT("SELECT PrayerCityID, PrayerStateID, PrayerCountryID, Name, EngName, TimeZone, Lon, Lat  FROM PrayerCity WHERE (Name = @Name)", SP);
				if (MyRead.Rows.Count > 1)
				{
					RetMess += "یکی از شهرهای زیر را انتخاب نمایید\n";
					for (int i = 0; i < MyRead.Rows.Count; i++)
					{
						RetMess += "شهر " + MyRead.Rows[i]["Name"] + " " + GetStateCountry(MyRead.Rows[i]["PrayerStateID"].ToString(), MyRead.Rows[i]["PrayerCountryID"].ToString()) + " /City" + MyRead.Rows[i]["PrayerCityID"].ToString() + "\n";
					}
					MyRead.Clear(); MyRead.Dispose();
				}
				else if (MyRead.Rows.Count > 0)
				{
					RetMess += "شهر " + MyRead.Rows[0]["Name"] + "\n";
					RetMess += " مورخه: " + Calender.GetPersianDateNumber(MyDate) + "\n";
					RetMess += GetPrayerTimeTable(MyRead.Rows[0]["PrayerCityID"], MyDate);
					RetMess += "طول جغرافیایی: " + MyRead.Rows[0]["Lon"] + "\n";
					RetMess += "عرض جغرافیایی: " + MyRead.Rows[0]["Lat"] + "\n";
					RetMess += "زمان محلی: " + MyRead.Rows[0]["TimeZone"] + "\n";
				}
				else
				{
					RetMess = "شهر مورد نظر وجود ندارد";

				}

			}
			else if (InText.IndexOf("Country") != -1) //*******************************************************************
			{
				InText = InText.Replace("Country", "");

				if (InText.Trim() == "")
				{
					DataTable MyRead = DAL.ViewData.MyDT("SELECT top (50) PrayerCountryID, Name  FROM PrayerCountry ORDER BY PrayerCountryID");
					if (MyRead.Rows.Count > 0)
					{
						RetMess += "یکی از کشورهای زیر را انتخاب نمایید\n";
						for (int i = 0; i < MyRead.Rows.Count; i++)
						{
							RetMess += "کشور " + MyRead.Rows[i]["Name"] + " /Country" + MyRead.Rows[i]["PrayerCountryID"].ToString() + "\n";
						}
						MyRead.Clear(); MyRead.Dispose();
					}
				}
				else
				{
					int PrayerCountryID = Tools.ConvertToInt32(InText, 0);
					if (PrayerCountryID == 0)
						return "کد وارد شده معتبر نمی باشد";
					SqlParameterCollection SP = new SqlCommand().Parameters;
					SP.AddWithValue("@PrayerCountryID", PrayerCountryID);

					DataTable MyRead = DAL.ViewData.MyDT("SELECT PrayerStateID, Name  FROM PrayerState WHERE (PrayerCountryID = @PrayerCountryID)", SP);
					if (MyRead.Rows.Count > 0)
					{
						RetMess += "یکی از استان های زیر را انتخاب نمایید\n";
						for (int i = 0; i < MyRead.Rows.Count; i++)
						{
							RetMess += "استان " + MyRead.Rows[i]["Name"] + " /State" + MyRead.Rows[i]["PrayerStateID"].ToString() + "\n";
						}
						MyRead.Clear(); MyRead.Dispose();
					}
				}
			}
			else if (InText.IndexOf("State") != -1)//*******************************************************************
			{
				InText = InText.Replace("State", "");

				if (InText.Trim() == "")
					return "کد یک استان را وارد نمایید";
				int PrayerStateID = Tools.ConvertToInt32(InText, 0);
				if (PrayerStateID == 0)
					return "کد وارد شده معتبر نمی باشد";
				SqlParameterCollection SP = new SqlCommand().Parameters;
				SP.AddWithValue("@PrayerStateID", PrayerStateID);

				DataTable MyRead = DAL.ViewData.MyDT("SELECT PrayerCityID, PrayerStateID, PrayerCountryID, Name, EngName, TimeZone, Lon, Lat  FROM PrayerCity WHERE (PrayerStateID =@PrayerStateID)", SP);
				if (MyRead.Rows.Count > 0)
				{
					RetMess += "لیست شهرهای استان " + InText;
					RetMess += "یکی از شهرهای زیر را انتخاب نمایید\n";
					for (int i = 0; i < MyRead.Rows.Count; i++)
					{
						RetMess += "شهر " + MyRead.Rows[i]["Name"] + " /City" + MyRead.Rows[i]["PrayerCityID"].ToString() + "\n";
					}
					MyRead.Clear(); MyRead.Dispose();
				}
			}
			else if (InText.IndexOf("City") != -1)//*******************************************************************
			{
				if (InText.IndexOf("Next") != -1)
					MyDate = MyDate.AddDays(1);
				InText = InText.Replace("City", "").Replace("Next", "").Trim();

				if (InText.Trim() == "")
					return "کد یک شهر را وارد نمایید";
				int CityID = Tools.ConvertToInt32(InText, 0);
				if (CityID == 0)
					return "کد وارد شده معتبر نمی باشد";
				SqlParameterCollection SP = new SqlCommand().Parameters;
				SP.AddWithValue("@CityID", CityID);
				DataTable MyRead = DAL.ViewData.MyDT("SELECT PrayerCityID, PrayerStateID, PrayerCountryID, Name, EngName, TimeZone, Lon, Lat  FROM PrayerCity WHERE (PrayerCityID = @CityID)", SP);
				if (MyRead.Rows.Count > 0)
				{
					RetMess += "اوقات شرعی شهر " + MyRead.Rows[0]["Name"] + "\n";
					RetMess += " مورخه: " + Calender.GetPersianDateNumber(MyDate) + "\n";
					RetMess += GetPrayerTimeTable(MyRead.Rows[0]["PrayerCityID"], MyDate);
					RetMess += "طول جغرافیایی: " + MyRead.Rows[0]["Lon"] + "\n";
					RetMess += "عرض جغرافیایی: " + MyRead.Rows[0]["Lat"] + "\n";
					RetMess += "زمان محلی: " + MyRead.Rows[0]["TimeZone"] + "\n";
				}
				MyRead.Clear(); MyRead.Dispose();
			}
			else if (InText.IndexOf("استان") != -1)//*******************************************************************
			{
				InText = InText.Replace("استان", "").Trim();
				SqlParameterCollection SP = new SqlCommand().Parameters;
				SP.AddWithValue("@Name", InText);
				int StateID = DAL.ExecuteData.CNTData("SELECT PrayerStateID  FROM PrayerState WHERE (Name = @Name)", SP);
				if (StateID == 0)
					return "استان مورد نظر شما وجود ندارد";

				DataTable MyRead = DAL.ViewData.MyDT("SELECT PrayerCityID, PrayerStateID, PrayerCountryID, Name, EngName, TimeZone, Lon, Lat  FROM PrayerCity WHERE (PrayerStateID =" + StateID + ")");
				if (MyRead.Rows.Count > 0)
				{
					RetMess += "لیست شهرهای استان " + InText;
					RetMess += "یکی از شهرهای زیر را انتخاب نمایید\n";
					for (int i = 0; i < MyRead.Rows.Count; i++)
					{
						RetMess += "شهر " + MyRead.Rows[i]["Name"] + " /City" + MyRead.Rows[i]["PrayerCityID"].ToString() + "\n";
					}
					MyRead.Clear(); MyRead.Dispose();
				}
			}
			return RetMess;
		}

		private static string GetPrayerTimeTable(object CityID, DateTime MyDate)
		{
			string RetVal = "";
			DataTable MyDT = DAL.ViewData.MyDT("SELECT Sobh, Toloe, Zohr, Ghorob, Maghreb, NimeShab, Ghibleh  FROM PrayerTime  WHERE (PrayerCityID = " + CityID + ") AND (ShamsiDay = " + Calender.GetPersianDate(MyDate)[2] + ") AND (ShamsiMonth = " + Calender.GetPersianDate(MyDate)[1] + ")");
			if (MyDT.Rows.Count <= 0)
				return "";
			RetVal += "اذان صبح: " + MyDT.Rows[0]["Sobh"] + "\n";
			RetVal += "طلوع آفتاب: " + MyDT.Rows[0]["Toloe"] + "\n";
			RetVal += "اذان ظهر: " + MyDT.Rows[0]["Zohr"] + "\n";
			RetVal += "غروب آفتاب: " + MyDT.Rows[0]["Ghorob"] + "\n";
			RetVal += "اذان مغرب: " + MyDT.Rows[0]["Maghreb"] + "\n";
			RetVal += "نیمه شب: " + MyDT.Rows[0]["NimeShab"] + "\n";
			RetVal += "خورشید در قبله: " + MyDT.Rows[0]["Ghibleh"] + "\n";
			MyDT.Clear(); MyDT.Dispose();
			return RetVal;
		}

		private static object GetStateCountry(string StateID, string CountryID)
		{
			string Retval = "";
			string StateName = DAL.ExecuteData.CNTDataStr("SELECT Name  FROM PrayerState WHERE (PrayerStateID = " + StateID + ")").Trim();
			if (StateName != "انتخاب نشد")
				Retval = "استان" + StateName;
			Retval += " کشور " + DAL.ExecuteData.CNTDataStr("SELECT Name  FROM PrayerCountry WHERE (PrayerCountryID = " + StateID + ")").Trim();
			return Retval;
		}
		private static string GetTextGallery(int TypeVal)
		{
			string SortBy = " ORDER BY NEWID() ";
			int gallType = DAL.ExecuteData.CNTData("SELECT Random  FROM TelegramTextType  WHERE (TelegramTextTypeID = " + TypeVal + ")");
			if (gallType == 0)
				SortBy = " order by TelegramTextID desc";
			else if (gallType == 2)
				SortBy = " and (GETDATE() BETWEEN StDate AND EnDate) ";
			string OT = DAL.ExecuteData.CNTDataStr("SELECT TOP (1) RTRIM(LTRIM(Text)) + '{$}' + RTRIM(LTRIM(Ref)) AS Expr1 FROM TelegramText WHERE (TelegramTextTypeID = " + TypeVal + ") and disable=0  " + SortBy).Replace("{$}", "\n").Trim();
			if (OT == "")
				return "پاسخ مناسبی وارد نشده است";
			return OT;
		}
		public static void SendToServerTextMessage(string SMessage, string Key, string MessageID, int TelegramUserID)
		{
			//string MessageText="";
			//if(SMessage!="")
			//	MessageText = "text=" + SMessage + "&";
			string Address = "https://api.telegram.org/bot" + UserToken(TelegramUserID) + "/sendMessage?text=" + SMessage + "&chat_id=" + MessageID + "" + Key;
			using (WebClient client = new WebClient())
			{
				/*IWebProxy myprox = new I
				client.Proxy=*/
				string comm = "";
				try
				{
					comm = client.DownloadString(Address);

				}
				catch 
				{
					//DAL.Logging.ErrorLog("telconn", Address, ee.Message);
					//System.Data.SqlClient.SqlParameterCollection SP1 = new System.Data.SqlClient.SqlCommand().Parameters;
					//SP1.AddWithValue("@txt", "AA" + Address);
					//DAL.ExecuteData.AddData(" INSERT INTO TelegramTemp(txt) VALUES (@txt) ", SP1);
				}
			}
		}
		public static void SendToServerMessage(int TelegramUserID, string StrParameter)
		{
			string Address = "https://api.telegram.org/bot" + UserToken(TelegramUserID) + "/" + StrParameter;
			using (WebClient client = new WebClient())
			{
				string comm = "";
				try
				{
					comm = client.DownloadString(Address);
				}
				catch
				{
					//DAL.Logging.ErrorLog("telconn", Address, comm);
					//System.Data.SqlClient.SqlParameterCollection SP1 = new System.Data.SqlClient.SqlCommand().Parameters;
					//SP1.AddWithValue("@txt", "AA" + Address);
					//DAL.ExecuteData.AddData(" INSERT INTO TelegramTemp(txt) VALUES (@txt) ", SP1);

				}
			}
		}
		private static MessageType GetMessageType(int MyType)
		{

			if (MyType == 3)
				return MessageType.VideoMessage;
			else if (MyType == 4)
				return MessageType.VoiceMessage;
			return MessageType.PhotoMessage;
		}
		private static Stream GetFileStream(string FileAddress)
		{
			Stream fs = System.IO.File.OpenRead(FileAddress);
			return fs;
		}

		private static string GetAutoMessage(int Type)
		{
			return DAL.ExecuteData.CNTDataStr("SELECT TOP (1) txt  FROM TelegramAutoAnswer WHERE (Type = " + Type + ")  ORDER BY NEWID()").Replace("{$}", "\n");
		}
		public static string GetKeyword(int TelegramUserID, string lev = "")
		{

			//&reply_markup={%22keyboard%22:[[%22option1%22],[%22option2%22]]}
			string Keyw = "&reply_markup={\"keyboard\":[";
			string CommandTXT = "";
			string RootMenu = "";
			if (lev == "")
				CommandTXT = "SELECT Name  FROM TelegramAnswer  WHERE (TelegramUserID = " + TelegramUserID + ") AND (LEN([Level]) = 2) ORDER BY TelegramAnswerID";
			else
			{
				CommandTXT = "SELECT Name  FROM TelegramAnswer  WHERE (TelegramUserID = " + TelegramUserID + ") AND [level] like '" + lev + "%' AND (LEN([Level]) = " + ((lev.Length) + 2) + ") ORDER BY TelegramAnswerID";
				RootMenu = ",[\"منوي اصلي\"]";
			}

			int MenuCNT = DAL.ExecuteData.CNTData("SELECT MenuCNT  FROM TelegramUser  WHERE (TelegramUserID = " + TelegramUserID + ")");
			if (MenuCNT <= 0)
				MenuCNT = 1;
			int i = 0; string Item = "";
			SqlDataReader MyRead = DAL.ViewData.MyDR1(CommandTXT);
			while (MyRead.Read())
			{
				Item += "\"" + MyCL.MGStr(MyRead, 0) + "\",";
				i++;
				if (i == MenuCNT)
				{
					i = 0;
					Keyw += "[" + Item.TrimEnd(',') + "],";
					Item = "";
				}

			}
			MyRead.Close(); MyRead.Dispose();
			if (Item != "")
			{
				if (RootMenu == "")
					Keyw += "[" + Item.TrimEnd(',') + "],";
				else
				{
					Keyw += "[" + Item.TrimEnd(',') + ",\"منوي اصلي\"],";
					RootMenu = "";
				}
			}
			if (Keyw == "&reply_markup={\"keyboard\":[")
				return "";
			return Keyw.TrimEnd(',') + RootMenu + "]}";
		}
		private static string GetHadithKeyword(string lev = "")
		{
			//&reply_markup={%22keyboard%22:[[%22option1%22],[%22option2%22]]}
			string Keyw = "&reply_markup={\"keyboard\":[";
			string CommandTXT = "";
			string RootMenu = "";
			if (lev == "")
				CommandTXT = "SELECT Name  FROM HadithCategory  WHERE (LEN([Level]) = 2) ORDER BY Sort";
			else
				CommandTXT = "SELECT Name  FROM HadithCategory  WHERE [level] like '" + lev + "%' AND (LEN([Level]) = " + ((lev.Length) + 2) + ") ORDER BY Sort";
			RootMenu = ",[\"منوي اصلي\"]";

			SqlDataReader MyRead = DAL.ViewData.MyDR1(CommandTXT);
			while (MyRead.Read())
			{
				Keyw += "[\"حدیث موضوع " + MyCL.MGStr(MyRead, 0) + "\"],";
			}
			MyRead.Close(); MyRead.Dispose();
			if (Keyw == "&reply_markup={\"keyboard\":[")
				return "";
			return Keyw.TrimEnd(',') + RootMenu + "]}";
		}

		private static string UserToken(int TelegramUserID)
		{
			return DAL.ExecuteData.CNTDataStr("SELECT Token FROM TelegramSetting WHERE (TelegramUserID = " + TelegramUserID + ")");
		}

		public static int GetUserSize(int TelegramUserID)
		{
			return DAL.ExecuteData.CNTData(" SELECT Size FROM TelegramUser WHERE (TelegramUserID = " + TelegramUserID + ")");
		}

		public static int GetGalleryType(string GalleryTypeID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@TelegramGalleryTypeID", GalleryTypeID);

			return DAL.ExecuteData.CNTData("SELECT type  FROM TelegramGalleryType  WHERE (TelegramGalleryTypeID = @TelegramGalleryTypeID)", SP);
		}

		public static bool UserAccessGalleryType(string GalleryTypeID, int TelegramUserID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@TelegramGalleryTypeID", GalleryTypeID);

			int CNT = DAL.ExecuteData.CNTData("SELECT COUNT(*) AS Expr1 FROM TelegramGalleryType  WHERE (TelegramGalleryTypeID = @TelegramGalleryTypeID) AND (TelegramUserID =" + TelegramUserID + ")", SP);
			if (CNT == 1)
				return true;
			return false;

		}
		public static void MyForwardMessage(int chatId, int fromChatId, int messageId)
		{
			//ForwardMessageAsync(chatId.ToString(), fromChatId.ToString(), messageId, false).ConfigureAwait(false);
		}
	/*	public static Task<Message> ForwardMessageAsync(string chatId, string fromChatId, int messageId,
		  bool disableNotification = false)
		{
			var parameters = new Dictionary<string, object>
            {
                {"chat_id", chatId},
                {"from_chat_id", fromChatId},
                {"message_id", messageId},
            };

			if (disableNotification)
				parameters.Add("disable_notification", true);

			return SendWebRequestAsync<Message>("forwardMessage", parameters);
		}*/
		/*private static async Task<Message> ForwardMessage(int chatId, int fromChatId, int messageId)
		{
			var parameters = new Dictionary<string, object>
            {
                {"chat_id", chatId},
                {"from_chat_id", fromChatId},
                {"message_id", messageId},
            };

			return await SendWebRequestAsync<Message>("forwardMessage", parameters).ConfigureAwait(false);
		}*/
		private const string BaseUrl = "https://api.telegram.org/bot";
		private const string BaseFileUrl = "https://api.telegram.org/file/bot";
		/*public static Task<Message> SendMessageAsync(MessageType type, string chatId, object content,
		   bool disableNotification = false,
		   int replyToMessageId = 0,
		   //IReplyMarkup replyMarkup = null,
		   Dictionary<string, object> additionalParameters = null
		  )
		{
			if (additionalParameters == null)
				additionalParameters = new Dictionary<string, object>();

			var typeInfo = type.ToKeyValue();

			additionalParameters.Add("chat_id", chatId);

			if (disableNotification)
				additionalParameters.Add("disable_notification", true);

			//if (replyMarkup != null)
				//additionalParameters.Add("reply_markup", replyMarkup);

			if (replyToMessageId != 0)
				additionalParameters.Add("reply_to_message_id", replyToMessageId);

			if (!string.IsNullOrEmpty(typeInfo.Value))
				additionalParameters.Add(typeInfo.Value, content);

			return SendWebRequestAsync<Message>(typeInfo.Key, additionalParameters);
		}*/
		/*
		public static async Task<T> SendWebRequestAsync<T>(string method, Dictionary<string, object> parameters = null
			)
        {

            var uri = new Uri(BaseUrl + Token + "/" + method);

            using (var client = new HttpClient())
            {
                ApiResponse<T> responseObject = null;
				try
				{
					HttpResponseMessage response;

					if (parameters == null || parameters.Count == 0)
					{
						response = await client.GetAsync(uri)
											   .ConfigureAwait(false);
					}
					else if(""=="")// if (parameters.Any(p => p.Value is FileToSend))
					{
						using (var form = new MultipartFormDataContent())
						{
							foreach (var parameter in parameters.Where(parameter => parameter.Value != null))
							{
								var content = ConvertParameterValue(parameter.Value);

								if (parameter.Key == "timeout" && (int)parameter.Value != 0)
								{
									client.Timeout = TimeSpan.FromSeconds((int)parameter.Value + 1);
								}

								if (parameter.Value is FileToSend)
								{
									client.Timeout = TimeSpan.FromMinutes(4);
									form.Add(content, parameter.Key, ((FileToSend)parameter.Value).Filename);
								}
								else
									form.Add(content, parameter.Key);
							}

							response = await client.PostAsync(uri, form)
												   .ConfigureAwait(false);
						}
					}
					else
					{
						var payload = JsonConvert.SerializeObject(parameters);

						var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");

						response = await client.PostAsync(uri, httpContent)
											   .ConfigureAwait(false);
					}

				}
				catch { }

				// if (responseObject == null)
				//    responseObject = new ApiResponse<T> {Ok = false, Message = "No response received"};

				//if (!responseObject.Ok)
				//    throw new ApiRequestException(responseObject.Message, responseObject.Code);

				//return null;// responseObject.ResultObject;
				
            }
        }*/

		private static async Task SendWebRequest(string method, Dictionary<string, object> parameters = null)
		{
			await SendRequest(method, parameters).ConfigureAwait(false);
		}

		private static async Task<HttpResponseMessage> SendRequest(string method, Dictionary<string, object> parameters = null)
		{
			var uri = new Uri("https://api.telegram.org/bot" + Token + "/" + method);

			HttpResponseMessage response;

			if (parameters != null)
			{
				using (var form = new MultipartFormDataContent())
				{

					foreach (var parameter in parameters.Where(parameter => parameter.Value != null))
					{
						var content = ConvertParameterValue(parameter.Value);

						/*if (parameter.Value is FileToSend)
							form.Add(content, parameter.Key, ((FileToSend)parameter.Value).Filename);
						else
							form.Add(content, parameter.Key);*/
					}

					using (var client = new HttpClient())
					{
						response = await client.PostAsync(uri, form).ConfigureAwait(false);
					}
				}
			}
			else
			{
				using (var client = new HttpClient())
				{
					response = await client.GetAsync(uri).ConfigureAwait(false);
				}
			}
			return response;
		}
		private static HttpContent ConvertParameterValue(object value)
		{
			var type = value.GetType().Name;
			switch (type)
			{
				case "String":
				case "Int32":
					return new StringContent(value.ToString());
				case "Boolean":
					return new StringContent((bool)value ? "true" : "false");
				//case "FileToSend":
					//return new StreamContent(((FileToSend)value).Content);
				default:
					return new StringContent(JsonConvert.SerializeObject(value));
			}
		}

		public static bool UserAccessTextType(string TextTypeID, int TelegramUserID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@TelegramGalleryTypeID", TextTypeID);
			int CNT = DAL.ExecuteData.CNTData("SELECT COUNT(*) AS Expr1 FROM TelegramTextType  WHERE (TelegramTextTypeID = @TelegramGalleryTypeID) AND (TelegramUserID =" + TelegramUserID + ")", SP);
			if (CNT == 1)
				return true;
			return false;
		}
		public static bool UserAccessRssType(string TextTypeID, int TelegramUserID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@TelegramRssTypeID", TextTypeID);
			int CNT = DAL.ExecuteData.CNTData("SELECT COUNT(*) AS Expr1 FROM TelegramRssType  WHERE (TelegramRssTypeID = @TelegramRssTypeID) AND (TelegramUserID =" + TelegramUserID + ")", SP);
			if (CNT == 1)
				return true;
			return false;
		}
		public static bool UserAccessVoteType(string VoteID, int TelegramUserID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@TelegramVoteID", VoteID);
			int CNT = DAL.ExecuteData.CNTData("SELECT COUNT(*) AS Expr1 FROM TelegramVote  WHERE (TelegramVoteID = @TelegramVoteID) AND (TelegramUserID =" + TelegramUserID + ")", SP);
			if (CNT == 1)
				return true;
			return false;
		}
		public static string[] UserTypeName = { "غیر فعال", "معمولی", "برنزی", "برنزی ویژه", "نقره ای", "نقره ای ویژه", "طلایی", "طلایی ویژه" };
		private static int[] UserTypeAutoAnswer = { 0, 1, 5, 10, 20, 50, 100000, 100000 };
		private static int[] UserTypeTexts = { 0, 10, 400, 500, 1000, 5000, 8000, 100000 };
		private static int[] UserTypeViewRecive = { 0, 1, 5, 10, 20, 50, 100000, 100000 };
		private static int[] UserTypePos = { 0, 0, 0, 0, 1, 2, 10, 100 };
		private static int[] UserTypeContact = { 0, 0, 0, 0, 0, 1, 1, 1 };
		private static int[] UserTypeRssFeed = { 0, 0, 0, 0, 0, 0, 2, 20 };
		private static int[] UserTypeAllMess = { 0, 0, 0, 0, 0, 0, 1, 1 };
		private static int[] UserTypeVote = { 0, 0, 0, 0, 0, 0, 10, 20 };
		public static int[] UserTypeSize = { 0, 50, 500, 1000, 10000, 20000, 50000, 100000 };
		public static string GetUserState(int TelegramUserID)
		{
			return UserTypeName[DAL.ExecuteData.CNTData("SELECT UserType  FROM TelegramUser  WHERE (TelegramUserID = " + TelegramUserID + ")")];
		}
		public static int GetUserTypeID(int TelegramUserID)
		{
			return DAL.ExecuteData.CNTData("SELECT UserType  FROM TelegramUser  WHERE (TelegramUserID = " + TelegramUserID + ")");
		}
		public static string GetGalleryTypeStr(int type, int TelegramUserID)
		{
			string inText = "";
			SqlDataReader Myread = DAL.ViewData.MyDR1("SELECT TelegramGalleryTypeID, Name  FROM TelegramGalleryType WHERE (TelegramUserID = " + TelegramUserID + ") AND (type = " + type + ")");
			while (Myread.Read())
			{//"Value 1": "Text 1"
				inText += "\"" + MyCL.MGInt(Myread, 0) + "\":\"" + MyCL.MGStr(Myread, 1) + "\",";
			}
			Myread.Close(); Myread.Dispose();
			return inText.TrimEnd(',');
		}
		public static string GetContactStr(int TelegramUserID)
		{
			string inText = "";
			SqlDataReader Myread = DAL.ViewData.MyDR1("SELECT TelegramContactID, Name + ' ' + Family AS Name  FROM TelegramContact WHERE (TelegramUserID = " + TelegramUserID + ") ");
			while (Myread.Read())
			{//"Value 1": "Text 1"
				inText += "\"" + MyCL.MGInt(Myread, 0) + "\":\"" + MyCL.MGStr(Myread, 1) + "\",";
			}
			Myread.Close(); Myread.Dispose();
			return inText.TrimEnd(',');
		}
		public static string GetTextStr(int TelegramUserID)
		{
			string inText = "";
			SqlDataReader Myread = DAL.ViewData.MyDR1("SELECT TelegramTextTypeID, Name  FROM TelegramTextType WHERE (TelegramUserID = " + TelegramUserID + ")");
			while (Myread.Read())
			{//"Value 1": "Text 1"
				inText += "\"" + MyCL.MGInt(Myread, 0) + "\":\"" + MyCL.MGStr(Myread, 1) + "\",";
			}
			Myread.Close(); Myread.Dispose();
			return inText.TrimEnd(',');
		}
		public static string GetRSSStr(int TelegramUserID)
		{
			string inText = "";
			SqlDataReader Myread = DAL.ViewData.MyDR1("SELECT TelegramRssTypeID, Name  FROM TelegramRssType WHERE (TelegramUserID = " + TelegramUserID + ")");
			while (Myread.Read())
			{//"Value 1": "Text 1"
				inText += "\"" + MyCL.MGInt(Myread, 0) + "\":\"" + MyCL.MGStr(Myread, 1) + "\",";
			}
			Myread.Close(); Myread.Dispose();
			return inText.TrimEnd(',');
		}
		public static string GetVoteStr(int TelegramUserID)
		{
			string inText = "";
			SqlDataReader Myread = DAL.ViewData.MyDR1("SELECT TelegramVoteID, Name  FROM TelegramVote WHERE (TelegramUserID = " + TelegramUserID + ")");
			while (Myread.Read())
			{//"Value 1": "Text 1"
				inText += "\"" + MyCL.MGInt(Myread, 0) + "\":\"" + MyCL.MGStr(Myread, 1) + "\",";
			}
			Myread.Close(); Myread.Dispose();
			return inText.TrimEnd(',');
		}
		public static string GetPosStr(int TelegramUserID)
		{
			string inText = "";
			SqlDataReader Myread = DAL.ViewData.MyDR1("SELECT TelegramPositionID, Name  FROM TelegramPosition WHERE (TelegramUserID = " + TelegramUserID + ")");
			while (Myread.Read())
			{//"Value 1": "Text 1"
				inText += "\"" + MyCL.MGInt(Myread, 0) + "\":\"" + MyCL.MGStr(Myread, 1) + "\",";
			}
			Myread.Close(); Myread.Dispose();
			return inText.TrimEnd(',');
		}
		public static int GetHaveAccess(MyVar.TelegramPageType MyPage, int UserID)
		{
			switch (MyPage)
			{
				case MyVar.TelegramPageType.AutoAnswer:
					return UserTypeAutoAnswer[GetUserTypeID(UserID)];
				case MyVar.TelegramPageType.Pos:
					return UserTypePos[GetUserTypeID(UserID)];
				case MyVar.TelegramPageType.Texts:
					return UserTypeTexts[GetUserTypeID(UserID)];
				case MyVar.TelegramPageType.Contact:
					return UserTypeContact[GetUserTypeID(UserID)];
				case MyVar.TelegramPageType.AllMess:
					return UserTypeAllMess[GetUserTypeID(UserID)];
				case MyVar.TelegramPageType.ViewRecive:
					return UserTypeViewRecive[GetUserTypeID(UserID)];
				case MyVar.TelegramPageType.Vote:
					return UserTypeVote[GetUserTypeID(UserID)];
				case MyVar.TelegramPageType.RSSFeed:
					return UserTypeRssFeed[GetUserTypeID(UserID)];
				default:
					return 0;
			}
		}
		public static void FillByeType(DropDownList ByeTypeDL)
		{
			ByeTypeDL.DataSource = DAL.ViewData.MyDT(" SELECT TelegramBuyID, Name, Payment  FROM TelegramBuy  ORDER BY TelegramBuyID");
			ByeTypeDL.DataBind();
		}

		public static string GetContactMessage(string Message)
		{
			JObject d = JObject.Parse(Message);
			string ot = "";
			ot += Tools.CheckNotEmpty("شماره", d["phone_number"].ToString(), "", true);
			ot += Tools.CheckNotEmpty("نام", (string)d["first_name"], "", true);
			ot += Tools.CheckNotEmpty("فامیل", (string)d["last_name"], "", true);
			ot += Tools.CheckNotEmpty("کد کاربری", (string)d["user_id"], "", true);
			return ot;
		}

		public static string GetLocationMessage(string Message, int MessID)
		{
			//{"longitude": 51.685166,  "latitude": 35.546455}
			JObject d = JObject.Parse(Message);
			string ot = "";
			ot += Tools.CheckNotEmpty("طول جغرافیایی", d["longitude"].ToString(), "", false);
			ot += Tools.CheckNotEmpty("عرض جغرافیایی", (string)d["latitude"], "", true);
			ot += "<img style=\"cursor:pointer\" onclick=\"SelectPrepMsg('ViewRecive.aspx?ID=" + MessID + "&lat=" + d["latitude"].ToString() + "&lam=" + d["longitude"].ToString() + "')\" src='/Images/Telegram/Map.png'>";
			return ot;
		}

		public static string GetPhotoMessage(string Message, int MessID)
		{
			try
			{
				Message = Message.Replace("]", "").Replace("[", "").Replace("\r\n", "");
				string[] Mess = Regex.Split(Message, "},", RegexOptions.None);
				JObject d = JObject.Parse(Mess[1] + "}");
				string ot = "";
				ot += "<img style=\"cursor:pointer\" onclick=\"SelectPrepMsg('ViewRecive.aspx?ID=" + MessID + "&photoid=" + d["file_id"] + "')\" src='/Images/Telegram/photo.png'>";
				return ot;
			}
			catch (Exception e)
			{
				DAL.Logging.ErrorLog("photomessagetelegram", "", e.Message);
				return "";
			}
		}

		public static void DownloadImage(string FileID, int TelegramUserID)
		{
			//Telegram.Bot.Api mm = new Telegram.Bot.Api(UserToken(TelegramUserID));
			//mm.GetUserProfilePhotos(77697166);
		}

		public static string GetVideoMessage(string Message, int MessID)
		{
			//Message = Message.Replace("]", "").Replace("[", "").Replace("\r\n", "");
			//string[] Mess = Regex.Split(Message, "},", RegexOptions.None);
			//JObject d = JObject.Parse(Mess[1] + "}");
			string ot = "";
			ot += "<img style=\"cursor:pointer\" onclick=\"SelectPrepMsg('ViewRecive.aspx?ID=" + MessID + "&photoid=" + MessID + "')\" src='/Images/Telegram/video.png'>";
			return ot;
		}

		public static string GetVoiceMessage(string Message, int MessID)
		{
			string ot = "";
			ot += "<img style=\"cursor:pointer\" onclick=\"SelectPrepMsg('ViewRecive.aspx?ID=" + MessID + "&photoid=" + MessID + "')\" src='/Images/Telegram/voice.png'>";
			return ot;
		}

		public static string GetDocumentMessage(string Message, int MessID)
		{
			string ot = "";
			ot += "<img style=\"cursor:pointer\" onclick=\"SelectPrepMsg('ViewRecive.aspx?ID=" + MessID + "&photoid=" + MessID + "')\" src='/Images/Telegram/file.png'>";
			return ot;
		}
		public static string GetStickerMessage(string Message, int MessID)
		{
			string ot = "";
			ot += "<img style=\"cursor:pointer\" onclick=\"SelectPrepMsg('ViewRecive.aspx?ID=" + MessID + "&photoid=" + MessID + "')\" src='/Images/Telegram/sticker.png'>";
			return ot;
		}
		
	}

public enum MessageType
		{
			UnknownMessage = 0,
			TextMessage,
			PhotoMessage,
			AudioMessage,
			VideoMessage,
			VoiceMessage,
			DocumentMessage,
			StickerMessage,
			LocationMessage,
			ContactMessage,
			ServiceMessage,
			VenueMessage,
		}
	internal static class MessageTypeExtension
	{
		internal static KeyValuePair<string, string> ToKeyValue(this MessageType type)
		{
			switch (type)
			{
				case MessageType.TextMessage:
					return new KeyValuePair<string, string>("sendMessage", "text");
				case MessageType.PhotoMessage:
					return new KeyValuePair<string, string>("sendPhoto", "photo");
				case MessageType.AudioMessage:
					return new KeyValuePair<string, string>("sendAudio", "audio");
				case MessageType.VideoMessage:
					return new KeyValuePair<string, string>("sendVideo", "video");
				case MessageType.VoiceMessage:
					return new KeyValuePair<string, string>("sendVoice", "voice");
				case MessageType.DocumentMessage:
					return new KeyValuePair<string, string>("sendDocument", "document");
				case MessageType.StickerMessage:
					return new KeyValuePair<string, string>("sendSticker", "sticker");
				case MessageType.LocationMessage:
					return new KeyValuePair<string, string>("sendLocation", "latitude");
				case MessageType.ContactMessage:
					return new KeyValuePair<string, string>("sendContact", "phone_number");
				case MessageType.VenueMessage:
					return new KeyValuePair<string, string>("sendVenue", "latitude");

				default:
					throw new NotImplementedException();
			}
		}
	}
}