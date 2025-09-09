namespace Tools
{
	using System;
	using System.Data;
	using System.Data.Common;
	using System.Text;
	using System.Data.SqlClient;
	using System.Web;
	using System.IO;
	using System.Text.RegularExpressions;

	public class Forum
	{
		public static string GetLastForumMessage(string Level)
		{
			string OutText = "";
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT TOP (1) ForumMessages.ForumMessagesID, ForumMessages.ForumTopicID, ForumMessages.Title, ForumMessages.Body, ForumMessages.CreationDate, GuestInfo.Name, GuestInfo.Family, ForumMessages.UserID FROM ForumMessages LEFT OUTER JOIN GuestInfo ON ForumMessages.UserID = GuestInfo.GuestID WHERE (ForumMessages.ForumTopicID IN (SELECT ForumTopicID FROM ForumTopic WHERE (ForumID IN (SELECT ForumID FROM Forum WHERE ([Level] LIKE '"+Level+"%'))))) AND (ForumMessages.Visible = 1) AND (ForumMessages.UnitID = "+MyClass.GetViewUnitID+") ORDER BY ForumMessages.ForumMessagesID DESC ");
			while (MyRead.Read())
			{
				OutText += "" +MyCL.MGStr(MyRead,2)+ "";
				OutText += "<br>" +Calender.MyPDate( MyCL.MGval(MyRead, 4) )+ "";
			}
			MyRead.Close();	MyRead.Dispose();
			return OutText;
		}
		public static string GetLastTopicMessage(string TopicID)
		{
			string OutText = "";
			SqlDataReader MyRead = DAL.ViewData.MyDR1("SELECT TOP (1) ForumMessages.ForumMessagesID, ForumMessages.ForumTopicID, ForumMessages.Title, ForumMessages.Body, ForumMessages.CreationDate, GuestInfo.Name, GuestInfo.Family, ForumMessages.UserID FROM ForumMessages LEFT OUTER JOIN GuestInfo ON ForumMessages.UserID = GuestInfo.GuestID WHERE (ForumMessages.ForumTopicID=" + TopicID + " ) AND (ForumMessages.Visible = 1) AND (ForumMessages.UnitID = " + MyClass.GetViewUnitID + ") ORDER BY ForumMessages.ForumMessagesID DESC ");
			while (MyRead.Read())
			{
				OutText += "" + MyCL.MGStr(MyRead, 2) + "";
				OutText += "<br>" + Calender.MyPDate(MyCL.MGval(MyRead, 4)) + "";
			}
			MyRead.Close(); MyRead.Dispose();
			return OutText;
		}		
		public static string FilterBadWords(string input)
		{
			string FilterWord = Tools.GetSetting(157);
			if (FilterWord == null)
				return input;
			input = input.Replace("<br /><br/>", "<br />").Replace("</p><br/>", "</p>").Replace("<p><br/>", "<p>");
			input = input.Replace("[Answer]", "<div class=\"ForumAnswerQute\">").Replace("[/Answer]","</div>");
			string str = input;
			Regex mysp = new Regex(" ");
			string[] BW = mysp.Split(FilterWord);
			foreach (string str2 in BW)
			{
				if (str2 != null)
				{
					string str3 = str2.Trim();
					if (str3.Length > 0)
						str = new Regex(@"(^|\W)(" + str3 + @")($|\W)", RegexOptions.IgnoreCase).Replace(str, new MatchEvaluator(BadWordsMatchEvaluator));
				}
			}

			return str;
		}
		private static string BadWordsMatchEvaluator(Match m)
		{
			return string.Format("{0}{1}{2}", m.Groups[1].Value, new string('*', m.Groups[2].Value.Length), m.Groups[3].Value);
		}
		public static string FormatMessageHTML(string input)
		{
			input = input.Trim();
			if (input.Length == 0)
				return string.Empty;
			input = input.Replace("&lt;", "<").Replace("&gt;", ">");
			Regex regex = new Regex(@"(\[url=([^\]]+)\])", RegexOptions.IgnoreCase);
			Regex regex2 = new Regex(@"(\[color=([^\]]+)\])", RegexOptions.IgnoreCase);
			Regex regex3 = new Regex(@"(\[size=([^\]]+)\])", RegexOptions.IgnoreCase);
			Regex regex4 = new Regex(@"(\[quote=?([^\]]*)\])", RegexOptions.IgnoreCase);
			Regex regex5 = new Regex(@"(\s|^)(http://([^\s]+))", RegexOptions.IgnoreCase);
			Regex regex6 = new Regex(@"(\s|^)(http://www\.youtube\.com/watch\?v=([^\s^&]+)[^\s]*)", RegexOptions.IgnoreCase);
			string str = input;
			str = str.Replace("[b]", "<b>").Replace("[/b]", "</b>").Replace("[/p]", "</p>").Replace("[p]", "<p>").Replace("[B]", "<b>").Replace("[/B]", "</b>").Replace("[ul]", "<ul>").Replace("[/ul]", "</ul>").Replace("[li]", "<li>").Replace("[/li]", "</li>").Replace("[i]", "<i>").Replace("[/i]", "</i>").Replace("[I]", "<i>").Replace("[/I]", "</i>").Replace("[u]", "<u>").Replace("[/u]", "</u>").Replace("[U]", "<u>").Replace("[/U]", "</u>").Replace("[code]", "<pre>").Replace("[/code]", "</pre>").Replace("[CODE]", "<pre>").Replace("[/CODE]", "</pre>").Replace("[img]", "<img src=\"").Replace("[/img]", "\" border=\"0\">").Replace("[IMG]", "<img src=\"").Replace("[/IMG]", "\" border=\"0\">");
			for (Match match = regex.Match(str); match.Success; match = match.NextMatch())
			{
				str = str.Replace(match.Groups[0].ToString(), "<a href=\"" + match.Groups[2].ToString() + "\" target=\"_blank\" rel=\"nofollow\">");
			}
			str = str.Replace("[/url]", "</a>").Replace("[/URL]", "</a>");
			for (Match match2 = regex2.Match(str); match2.Success; match2 = match2.NextMatch())
			{
				str = str.Replace(match2.Groups[0].ToString(), "<span style=\"color:" + match2.Groups[2].ToString() + "\">");
			}
			str = str.Replace("[/color]", "</span>").Replace("[/COLOR]", "</span>");
			for (Match match3 = regex3.Match(str); match3.Success; match3 = match3.NextMatch())
			{
				str = str.Replace(match3.Groups[0].ToString(), "<span style=\"font-size:" + match3.Groups[2].ToString() + "pt\">");
			}
			str = str.Replace("[/size]", "</span>").Replace("[/]", "</span>");
			for (Match match4 = regex4.Match(str); match4.Success; match4 = match4.NextMatch())
			{
				string str2;
				if (match4.Groups[2].Length > 0)
					str2 = "<b>" + match4.Groups[2].ToString() + "</b> نویسنده:<br/><div class=quote>";
				else
					str2 = "<div class=quote>";

				str = str.Replace(match4.Groups[0].ToString(), str2);
			}
			str = str.Replace("[/quote]", "</div>").Replace("[/QUOTE]", "</div>");
			for (Match match5 = regex6.Match(str); match5.Success; match5 = match5.NextMatch())
			{
				string str3 = match5.Groups[3].ToString();
				string newValue = string.Format("<object width=\"425\" height=\"344\"><param name=\"movie\" value=\"http://www.youtube.com/v/{0}\"></param><param name=\"allowFullScreen\" value=\"true\"></param><param name=\"allowscriptaccess\" value=\"always\"></param><embed src=\"http://www.youtube.com/v/{1}\" type=\"application/x-shockwave-flash\" allowscriptaccess=\"always\" allowfullscreen=\"true\" width=\"425\" height=\"344\"></embed></object>", str3, str3);
				str = str.Replace(match5.Groups[2].ToString(), newValue);
			}
			for (Match match6 = regex5.Match(str); match6.Success; match6 = match6.NextMatch())
			{
				str = str.Replace(match6.Groups[2].ToString(), "<a href=\"" + match6.Groups[2].ToString() + "\" target=\"_blank\" rel=\"nofollow\">" + match6.Groups[2].ToString() + "</a>");
			}
			str = str.Replace("\r\n", "<br/>");
			if (Tools.GetSetting(159,"1") == "1")
				str = FormatSmilies(str);

			return str;
		}
		public static string UserAvatarPath()
		{
			if (DAL.CheckData.GuestUserLoginID() == 0)
				return "/images/userAvatar.png";
			if (File.Exists(DAL.CheckData.GetFilesRoot(true) + "\\Images\\Members\\" + DAL.CheckData.GuestUserLoginID() + ".jpg"))
				return DAL.CheckData.GetFilesRoot() + "/Images/Members/" + DAL.CheckData.GuestUserLoginID() + ".jpg";
			return "/images/guestavatar.gif";
		}
		public static string UserAvatarPath(string UserIDS)
		{
			int UserID = Tools.ConvertToInt32(UserIDS);
			if (UserID <=0)
				return "/images/userAvatar.png";
			if (File.Exists(DAL.CheckData.GetFilesRoot(true) + "\\Images\\Members\\" + UserID + ".jpg"))
				return DAL.CheckData.GetFilesRoot() + "/Images/Members/" + UserID + ".jpg";
			return "/images/userAvatar.png";
		}
		public static string FormatSmilies(string input)
		{
			string str = input;
			return str.Replace(":)", "<img src=\"/images/smilies/smile.gif\" border=0 />")
				.Replace(";)", "<img src=\"/images/smilies/wink.gif\" border=0 />")
				.Replace(":(", "<img src=\"/images/smilies/upset.gif\" border=0 />")
				.Replace(":beer:", "<img src=\"/images/smilies/beer.gif\" border=\"0\" alt=\"beer\" />")
				.Replace(":bong:", "<img src=\"/images/smilies/bongL3i8.gif\" border=\"0\" alt=\"Bong\" />")
				.Replace(":cheers:", "<img src=\"/images/smilies/28208052075.gif\" border=\"0\" alt=\"Toast\" />")
				.Replace(":coffee:", "<img src=\"/images/smilies/coffee26at.gif\" border=\"0\" alt=\"Drink\" />")
				.Replace(":drop:", "<img src=\"/images/smilies/wiggle.gif\" border=\"0\" alt=\"Wiggle\" />")
				.Replace(":gossip:", "<img src=\"/images/smilies/5822044956.gif\" border=\"0\" alt=\"Gossip\" />")
				.Replace(":popcorn:", "<img src=\"/images/smilies/5821542753.gif\" border=\"0\" alt=\"Eat popcorn\" />")
				.Replace(":report:", "<img src=\"/images/smilies/5822282438.gif\" border=\"0\" alt=\"Read Report\" />")
				.Replace(":secret:", "<img src=\"/images/smilies/ssst.gif\" border=\"0\" alt=\"Quiet\" />")
				.Replace(":sleep:", "<img src=\"/images/smilies/27418101277.gif\" border=\"0\" alt=\"Sleepy\" />")
				.Replace(":smoke:", "<img src=\"/images/smilies/spliff.gif\" border=\"0\" alt=\"Cool Smoke\" />")
				.Replace(":sugar:", "<img src=\"/images/smilies/5822332967.gif\" border=\"0\" alt=\"Sugar High\" />")
				.Replace(":wave:", "<img src=\"/images/smilies/wavey.gif\" border=\"0\" alt=\"Wave\" />")
				.Replace(":wings:", "<img src=\"/images/smilies/angel2.gif\" border=\"0\" alt=\"Angel Wings\" />")
				.Replace(":applause:", "<img src=\"/images/smilies/32308364851.gif\" border=\"0\" alt=\"Applause\" />")
				.Replace(":bow:", "<img src=\"/images/smilies/bowdown.gif\" border=\"0\" alt=\"bow\" />")
				.Replace(":buddies:", "<img src=\"/images/smilies/dri0047.gif\" border=\"0\" alt=\"Buddies\" />")
				.Replace(":buttstr:", "<img src=\"/images/smilies/5821472746.gif\" border=\"0\" alt=\"Buttstroke\" />")
				.Replace(":good:", "<img src=\"/images/smilies/goodpost7td.gif\" border=\"0\" alt=\"Good Posting\" />")
				.Replace(":iagree:", "<img src=\"/images/smilies/iagree.gif\" border=\"0\" alt=\"i agree\" />")
				.Replace(":iws:", "<img src=\"/images/smilies/stupid.gif\" border=\"0\" alt=\"i'm with stupid\" />")
				.Replace(":logic:", "<img src=\"/images/smilies/13523300456.gif\" border=\"0\" alt=\"Logic\" />")
				.Replace(":pathead:", "<img src=\"/images/smilies/pat9xu.gif\" border=\"0\" alt=\"Pat on the head\" />")
				.Replace(":thumb:", "<img src=\"/images/smilies/thumb.gif\" border=\"0\" alt=\"Thumbs Up\" />")
				.Replace(":whs:", "<img src=\"/images/smilies/whs0be.gif\" border=\"0\" alt=\"What He Said\" />")
				.Replace(":worship:", "<img src=\"/images/smilies/worshippy.gif\" border=\"0\" alt=\"worship\" />")
				.Replace(":badrazz:", "<img src=\"/images/smilies/badrazz.gif\" border=\"0\" alt=\"bad razz\" />")
				.Replace(":bang:", "<img src=\"/images/smilies/suicide.gif\" border=\"0\" alt=\"Suicide\" />")
				.Replace(":blah:", "<img src=\"/images/smilies/metallicblue.gif\" border=\"0\" alt=\"blah\" />")
				.Replace(":bricks:", "<img src=\"/images/smilies/sterb0734ps.gif\" border=\"0\" alt=\"Ton of Bricks\" />")
				.Replace(":finger:", "<img src=\"/images/smilies/5700274341.gif\" border=\"0\" alt=\"Go ____ yourself\" />")
				.Replace(":forkoff:", "<img src=\"/images/smilies/fork_off.gif\" border=\"0\" alt=\"Fork Off\" />")
				.Replace(":lamer:", "<img src=\"/images/smilies/lamer2845nh.gif\" border=\"0\" alt=\"Lamer noob\" />")
				.Replace(":mf:", "<img src=\"/images/smilies/thefinger.gif\" border=\"0\" alt=\"Middle finger\" />")
				.Replace(":nono:", "<img src=\"/images/smilies/hsnono.gif\" border=\"0\" alt=\"nono\" />")
				.Replace(":owned:", "<img src=\"/images/smilies/owned.gif\" border=\"0\" alt=\"Owned\" />")
				.Replace(":shakehead:", "<img src=\"/images/smilies/shakehead.gif\" border=\"0\" alt=\"shake head\" />")
				.Replace(":shutup:", "<img src=\"/images/smilies/5822291454.gif\" border=\"0\" alt=\"Shut It\" />")
				.Replace(":slap:", "<img src=\"/images/smilies/wtcslap.gif\" border=\"0\" alt=\"slap\" />")
				.Replace(":smash:", "<img src=\"/images/smilies/smash.gif\" border=\"0\" alt=\"Gavel\" />")
				.Replace(":thumb-:", "<img src=\"/images/smilies/thumbdowncopy1up.gif\" border=\"0\" alt=\"Thumbs Down\" />")
				.Replace(":yas:", "<img src=\"/images/smilies/urstupid.gif\" border=\"0\" alt=\"You're Stupid\" />")
				.Replace(":argue:", "<img src=\"/images/smilies/argue.gif\" border=\"0\" alt=\"Argument\" />")
				.Replace(":boxer:", "<img src=\"/images/smilies/boxer.gif\" border=\"0\" alt=\"boxer\" />")
				.Replace(":fencing:", "<img src=\"/images/smilies/6804382843.gif\" border=\"0\" alt=\"En Garde!\" />")
				.Replace(":flame:", "<img src=\"/images/smilies/sterb2457li.gif\" border=\"0\" alt=\"Fart/Flame the noob\" />")
				.Replace(":goaway:", "<img src=\"/images/smilies/30416221069.gif\" border=\"0\" alt=\"Go Away\" />")
				.Replace(":guns:", "<img src=\"/images/smilies/sterb1842sg.gif\" border=\"0\" alt=\"Gun\" />")
				.Replace(":gunsling:", "<img src=\"/images/smilies/sterb1908nd.gif\" border=\"0\" alt=\"Gunslinger\" />")
				.Replace(":headbite:", "<img src=\"/images/smilies/5122424636.gif\" border=\"0\" alt=\"Bite your head off\" />")
				.Replace(":lightsab:", "<img src=\"/images/smilies/sterb0298yz.gif\" border=\"0\" alt=\"Darth Lightsabers\" />")
				.Replace(":mob:", "<img src=\"/images/smilies/5422184119.gif\" border=\"0\" alt=\"Angry Mob\" />")
				.Replace(":needmod:", "<img src=\"/images/smilies/582145129.gif\" border=\"0\" alt=\"Belittle\" />")
				.Replace(":nutkick:", "<img src=\"/images/smilies/nutkick5ur.gif\" border=\"0\" alt=\"Nut Kick\" />")
				.Replace(":pillow:", "<img src=\"/images/smilies/30700402927.gif\" border=\"0\" alt=\"Pillow Fight\" />")
				.Replace(":smack:", "<img src=\"/images/smilies/5700365222.gif\" border=\"0\" alt=\"Smack!\" />")
				.Replace(":stooge:", "<img src=\"/images/smilies/sterb1918mv.gif\" border=\"0\" alt=\"3 Stooges\" />")
				.Replace(":whack:", "<img src=\"/images/smilies/sterb1881bq.gif\" border=\"0\" alt=\"Hammer Time\" />")
				.Replace(":wuss:", "<img src=\"/images/smilies/5822392499.gif\" border=\"0\" alt=\"Wuss Fight\" />")
				.Replace(":blush:", "<img src=\"/images/smilies/5821461258.gif\" border=\"0\" alt=\"Blush\" />")
				.Replace(":boink:", "<img src=\"/images/smilies/boink.gif\" border=\"0\" alt=\"boink\" />")
				.Replace(":crazy:", "<img src=\"/images/smilies/confused7nt.gif\" border=\"0\" alt=\"Bit Wonky..\" />")
				.Replace(":doggystyl:", "<img src=\"/images/smilies/xxx.gif\" border=\"0\" alt=\"doggy style\" />")
				.Replace(":eek:", "<img src=\"/images/smilies/eek.gif\" border=\"0\" alt=\"EEK!\" />")
				.Replace(":foilhat:", "<img src=\"/images/smilies/tinfoil.gif\" border=\"0\" alt=\"Tin Foil Hat\" />")
				.Replace(":gotcha:", "<img src=\"/images/smilies/gotcha.gif\" border=\"0\" alt=\"Gotcha!\" />")
				.Replace(":greenp:", "<img src=\"/images/smilies/crazy3.gif\" border=\"0\" alt=\"Green Tongue\" />")
				.Replace(":grngreedy:", "<img src=\"/images/smilies/grngreedy.gif\" border=\"0\" alt=\"Greedy Guts\" />")
				.Replace(":hahano:", "<img src=\"/images/smilies/hahano.gif\" border=\"0\" alt=\"hahano\" />")
				.Replace(":ignore:", "<img src=\"/images/smilies/ignore.gif\" border=\"0\" alt=\"Ignored\" />")
				.Replace(":loo:", "<img src=\"/images/smilies/loo.gif\" border=\"0\" alt=\"Loo Flush\" />")
				.Replace(":looks:", "<img src=\"/images/smilies/spyme.gif\" border=\"0\" alt=\"Creeped out\" />")
				.Replace(":menace:", "<img src=\"/images/smilies/menacegrin.gif\" border=\"0\" alt=\"Menacing\" />")
				.Replace(":omg:", "<img src=\"/images/smilies/omg9hi.gif\" border=\"0\" alt=\"OMG\" />")
				.Replace(":peace:", "<img src=\"/images/smilies/Peace!.gif\" border=\"0\" alt=\"Peace Sign\" />")
				.Replace(":poke:", "<img src=\"/images/smilies/stickpoke.gif\" border=\"0\" alt=\"poke\" />")
				.Replace(":puppy:", "<img src=\"/images/smilies/sdb60030.gif\" border=\"0\" alt=\"puppy dog eyes\" />")
				.Replace(":rolleyes:", "<img src=\"/images/smilies/rolleyes.gif\" border=\"0\" alt=\"Roll Eyes (Sarcastic)\" />")
				.Replace(":shocked:", "<img src=\"/images/smilies/SHOCKED.gif\" border=\"0\" alt=\"Shock\" />")
				.Replace(":sick:", "<img src=\"/images/smilies/ill.gif\" border=\"0\" alt=\"Sick\" />")
				.Replace(":silly:", "<img src=\"/images/smilies/silly.gif\" border=\"0\" alt=\"Goofus\" />")
				.Replace(":twitch:", "<img src=\"/images/smilies/twitch2.gif\" border=\"0\" alt=\"Twitchy\" />")
				.Replace(":/:", "<img src=\"/images/smilies/_sure.gif\" border=\"0\" alt=\"Riiiight.\" />")
				.Replace(@":\:", "<img src=\"/images/smilies/eek7.gif\" border=\"0\" alt=\"Whaaaaa?\" />")
				.Replace(":confused:", "<img src=\"/images/smilies/confused.gif\" border=\"0\" alt=\"Confused\" />")
				.Replace(":doh:", "<img src=\"/images/smilies/4915593391.gif\" border=\"0\" alt=\"slap forehead\" />")
				.Replace(":duh:", "<img src=\"/images/smilies/5700272664.gif\" border=\"0\" alt=\"Duhh\" />")
				.Replace(":dunno:", "<img src=\"/images/smilies/dunno.gif\" border=\"0\" alt=\"dunno\" />")
				.Replace(":headscrat:", "<img src=\"/images/smilies/headscratch.gif\" border=\"0\" alt=\"hmm\" />")
				.Replace(":nosmile:", "<img src=\"/images/smilies/nosmile.gif\" border=\"0\" alt=\"Blank stare\" />")
				.Replace(":shrug:", "<img src=\"/images/smilies/icon_darin.gif\" border=\"0\" alt=\"shrug\" />")
				.Replace(":squint:", "<img src=\"/images/smilies/squint.gif\" border=\"0\" alt=\"squint\" />")
				.Replace(":werd:", "<img src=\"/images/smilies/werd.gif\" border=\"0\" alt=\"werd\" />")
				.Replace(":wtf:", "<img src=\"/images/smilies/wtf.gif\" border=\"0\" alt=\"wtf\" />")
				.Replace(":D", "<img src=\"/images/smilies/biggrin.gif\" border=\"0\" alt=\"Big Grin\" />")
				.Replace(":fingersx:", "<img src=\"/images/smilies/fingersx.gif\" border=\"0\" alt=\"fingers crossed\" />")
				.Replace(":gh:", "<img src=\"/images/smilies/grouphug.gif\" border=\"0\" alt=\"grouphug\" />")
				.Replace(":glomp:", "<img src=\"/images/smilies/5822051440.gif\" border=\"0\" alt=\"Glomp, Hi!\" />")
				.Replace(":hitit:", "<img src=\"/images/smilies/hitit.gif\" border=\"0\" alt=\"hitit\" />")
				.Replace(":hug:", "<img src=\"/images/smilies/hug.gif\" border=\"0\" alt=\"hug\" />")
				.Replace(":inlove:", "<img src=\"/images/smilies/5822214714.gif\" border=\"0\" alt=\"In Love\" />")
				.Replace(":kissyou:", "<img src=\"/images/smilies/kissyou.gif\" border=\"0\" alt=\"Kissing\" />")
				.Replace(":naughty:", "<img src=\"/images/smilies/naughty.gif\" border=\"0\" alt=\"naughty\" />")
				.Replace(":-P", "<img src=\"/images/smilies/tongue.gif\" border=\"0\" alt=\"Stick Out Tongue\" />")
				.Replace(":woohoo:", "<img src=\"/images/smilies/woohoo.gif\" border=\"0\" alt=\"Woo Hoo!\" />")
				.Replace(":argh:", "<img src=\"/images/smilies/sd3.gif\" border=\"0\" alt=\"RRRGH\" />")
				.Replace(":banghead:", "<img src=\"/images/smilies/banghead.gif\" border=\"0\" alt=\"Brick Wall\" />")
				.Replace(":cry:", "<img src=\"/images/smilies/908572171.gif\" border=\"0\" alt=\"Cry\" />")
				.Replace(":curse:", "<img src=\"/images/smilies/28510172849.gif\" border=\"0\" alt=\"Cursing\" />")
				.Replace(":mad:", "<img src=\"/images/smilies/po.gif\" border=\"0\" alt=\"Mad\" />")
				.Replace(":rant:", "<img src=\"/images/smilies/rant2.gif\" border=\"0\" alt=\"rant\" />")
				.Replace(":sweat:", "<img src=\"/images/smilies/newbluesweatdrop.gif\" border=\"0\" alt=\"Anime Sweat\" />")
				.Replace(":hahabow:", "<img src=\"/images/smilies/bowrofl.gif\" border=\"0\" alt=\"bowrofl\" />")
				.Replace(":hahaha:", "<img src=\"/images/smilies/hahaha.gif\" border=\"0\" alt=\"hahaha\" />")
				.Replace(":heh_heh:", "<img src=\"/images/smilies/heh_heh.gif\" border=\"0\" alt=\"heh heh\" />")
				.Replace(":hehe:", "<img src=\"/images/smilies/kekekegay.gif\" border=\"0\" alt=\"hehe\" />")
				.Replace(":jk:", "<img src=\"/images/smilies/5903011026.gif\" border=\"0\" alt=\"Just Kidding\" />")
				.Replace(":lolhit:", "<img src=\"/images/smilies/5700293539.gif\" border=\"0\" alt=\"lol hit\" />")
				.Replace(":rofl:", "<img src=\"/images/smilies/laugh.gif\" border=\"0\" alt=\"laugh\" />")
				.Replace(":roflmao:", "<img src=\"/images/smilies/13501381245.gif\" border=\"0\" alt=\"ROFLMAO\" />")
				.Replace(":404:", "<img src=\"/images/smilies/404.gif\" border=\"0\" alt=\"Not Found\" />")
				.Replace(":abuse:", "<img src=\"/images/smilies/28517070443.gif\" border=\"0\" alt=\"S&M Abuse\" />")
				.Replace(":damnpc:", "<img src=\"/images/smilies/damnpc.gif\" border=\"0\" alt=\"Damn Computer..\" />")
				.Replace(":milk:", "<img src=\"/images/smilies/milk.gif\" border=\"0\" alt=\"Drink Milk\" />")
				.Replace(":skull:", "<img src=\"/images/smilies/bgmad.gif\" border=\"0\" alt=\"Skull\" />")
				.Replace(":unclesam:", "<img src=\"/images/smilies/UNCLESAM.gif\" border=\"0\" alt=\"Uncle Sam\" />")
				.Replace(":usa:", "<img src=\"/images/smilies/usa.gif\" border=\"0\" alt=\"Yankee\" />")
				.Replace(":violin:", "<img src=\"/images/smilies/7314474053.gif\" border=\"0\" alt=\"Violin\" />")
				.Replace(":dance:", "<img src=\"/images/smilies/dance.gif\" border=\"0\" alt=\"Boogy Dance\" />")
				.Replace(":dawave:", "<img src=\"/images/smilies/dawave.gif\" border=\"0\" alt=\"The Wave\" />")
				.Replace(":hb:", "<img src=\"/images/smilies/birthday.gif\" border=\"0\" alt=\"happy birthday\" />")
				.Replace(":jammin:", "<img src=\"/images/smilies/jammin.gif\" border=\"0\" alt=\"Jammin'\" />")
				.Replace(":music:", "<img src=\"/images/smilies/music-smiley-026.gif\" border=\"0\" alt=\"Rock Band\" />")
				.Replace(":party:", "<img src=\"/images/smilies/party.gif\" border=\"0\" alt=\"Party\" />")
				.Replace(":rockon:", "<img src=\"/images/smilies/ylsuper.gif\" border=\"0\" alt=\"rock on\" />")
				.Replace(":cool:", "<img src=\"/images/smilies/1cool.gif\" border=\"0\" alt=\"Cool\" />")
				.Replace(":educated:", "<img src=\"/images/smilies/educate.gif\" border=\"0\" alt=\"Educated\" />")
				.Replace(":flasher:", "<img src=\"/images/smilies/flasher.gif\" border=\"0\" alt=\"Flasher\" />")
				.Replace(":froot:", "<img src=\"/images/smilies/fruit.gif\" border=\"0\" alt=\"Froot\" />")
				.Replace(":king:", "<img src=\"/images/smilies/king.gif\" border=\"0\" alt=\"King-dude\" />")
				.Replace(":king2:", "<img src=\"/images/smilies/knee7rm.gif\" border=\"0\" alt=\"Kneel!\" />")
				.Replace(":master:", "<img src=\"/images/smilies/overlord.gif\" border=\"0\" alt=\"King-tron\" />")
				.Replace(":pimp:", "<img src=\"/images/smilies/pimp.gif\" border=\"0\" alt=\"P.I.M.P.\" />")
				.Replace(":pirate:", "<img src=\"/images/smilies/pir8.gif\" border=\"0\" alt=\"Avast!\" />")
				.Replace(":police:", "<img src=\"/images/smilies/police.gif\" border=\"0\" alt=\"Police\" />")
				.Replace(":spock:", "<img src=\"/images/smilies/spock.gif\" border=\"0\" alt=\"Spock\" />")
				.Replace(":storm:", "<img src=\"/images/smilies/5420285018.gif\" border=\"0\" alt=\"Stormtrooper\" />").Replace(":faq:", "<img src=\"/images/smilies/faqnice.gif\" border=\"0\" alt=\"FAQ Nice\" />").Replace(":google:", "<img src=\"/images/smilies/google.gif\" border=\"0\" alt=\"google\" />").Replace(":rtfm:", "<img src=\"/images/smilies/rtfm.gif\" border=\"0\" alt=\"RTFM\" />").Replace(":rulez:", "<img src=\"/images/smilies/rulez.gif\" border=\"0\" alt=\"Rulez Nice\" />").Replace(":wiki:", "<img src=\"/images/smilies/wiki.gif\" border=\"0\" alt=\"Wikipedia\" />").Replace(":banhim:", "<img src=\"/images/smilies/banhim.gif\" border=\"0\" alt=\"banhim\" />").Replace(":banned:", "<img src=\"/images/smilies/banned.gif\" border=\"0\" alt=\"Ban Stamp\" />").Replace(":bump:", "<img src=\"/images/smilies/bump.gif\" border=\"0\" alt=\"Bump\" />").Replace(":double:", "<img src=\"/images/smilies/dbl.gif\" border=\"0\" alt=\"Double Thread/Post\" />").Replace(":edit:", "<img src=\"/images/smilies/edited.gif\" border=\"0\" alt=\"Edit\" />").Replace(":feedback:", "<img src=\"/images/smilies/feedback.gif\" border=\"0\" alt=\"Feedback Requested\" />").Replace(":flogging:", "<img src=\"/images/smilies/flogging.gif\" border=\"0\" alt=\"Flog dead topic\" />").Replace(":hijack:", "<img src=\"/images/smilies/threadjacked.gif\" border=\"0\" alt=\"Thread Hijack!\" />").Replace(":locked:", "<img src=\"/images/smilies/lockd.gif\" border=\"0\" alt=\"lockd\" />").Replace(":mods:", "<img src=\"/images/smilies/7309300734.gif\" border=\"0\" alt=\"Beware of the Mods...they come in the Niiiiiiight!\" />").Replace(":offtopic:", "<img src=\"/images/smilies/offtopic.gif\" border=\"0\" alt=\"Off Topic\" />").Replace(":ontopic:", "<img src=\"/images/smilies/ontopic.gif\" border=\"0\" alt=\"On Topic\" />").Replace(":qfe:", "<img src=\"/images/smilies/qfe.gif\" border=\"0\" alt=\"Quoted For Emphasis\" />").Replace(":repost:", "<img src=\"/images/smilies/repost.gif\" border=\"0\" alt=\"repost\" />").Replace(":spam:", "<img src=\"/images/smilies/spam4ot.gif\" border=\"0\" alt=\"Spam Alert!\" />")
				.Replace(":spamkill:", "<img src=\"/images/smilies/4913420063.gif\" border=\"0\" alt=\"Die die die!\" />");
		}
		public static string FormatSignature(string signature)
		{
			signature = signature.Trim();
			if (signature.Length == 0)
			{
				return string.Empty;
			}
			return ("<br/><br/>--<br/>" + FormatMessageHTML(signature));
		}
		public static void AddItemCount(int type, int forumtopicid)
		{
			if (type == 1)//message add
			{
				string Level = DAL.ExecuteData.CNTDataStr("SELECT [Level] FROM Forum WHERE (ForumID = (SELECT ForumID FROM ForumTopic WHERE (ForumTopicID = "+forumtopicid+"))) ");
				for (int i = 0; i < Level.Length/2; i++)
				{
					string CurLev = Level.Substring(0, Level.Length - (i * 2));
					DAL.ExecuteData.ExecData("UPDATE Forum  SET PostCNT = PostCNT + 1  WHERE ([Level] = '" + CurLev + "') ");
				}
				DAL.ExecuteData.ExecData("UPDATE ForumTopic SET RepliesCount =RepliesCount +1  WHERE (ForumTopicID = " + forumtopicid + ")");
			}
			else if (type == 2)
			{
				string Level = DAL.ExecuteData.CNTDataStr("SELECT [Level] FROM Forum WHERE (ForumID = " + forumtopicid + ") ");
				for (int i = 0; i < Level.Length / 2; i++)
				{
					string CurLev = Level.Substring(0, Level.Length - (i * 2));
					DAL.ExecuteData.ExecData("UPDATE Forum  SET SubCNT = SubCNT + 1  WHERE ([Level] = '" + CurLev + "') ");
				}
			}
		}

		public static int GetUserPostLike(string UserID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@userid", UserID);

			return DAL.ExecuteData.CNTData("SELECT COUNT(*) FROM ForumMessages INNER JOIN ForumMessagesLike ON ForumMessages.ForumMessagesID = ForumMessagesLike.ForumMessagesID  WHERE (ForumMessages.UserID = @userid)",SP);
		}
		public static string GetUserName(string UserID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@userid", UserID);

			string myname= DAL.ExecuteData.CNTDataStr("SELECT rtrim(ltrim(Name))+' '+rtrim(ltrim(Family)) FROM GuestInfo  WHERE (GuestID = @userid)", SP);
			if (myname.Trim() == "")
				return "کاربر مهمان";
			return myname;
		}
		public static string GetUserType(string UserID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@userid", UserID);
			int cnt = DAL.ExecuteData.CNTData("SELECT COUNT(*) FROM ForumMessages INNER JOIN ForumMessagesLike ON ForumMessages.ForumMessagesID = ForumMessagesLike.ForumMessagesID  WHERE (ForumMessages.UserID = @userid)", SP);
			if (cnt < 100)
				return "کاربر معمولی";
			else if (cnt < 200 )
				return "کاربر برنزی";
			else if (cnt < 300)
				return "کاربر نقره ای";
			return "کاربر طلایی";
		}
		public static int GetUserPostSend(string UserID)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@userid", UserID);

			return DAL.ExecuteData.CNTData("SELECT COUNT(*) AS Expr1  FROM ForumMessages WHERE (ForumMessages.UserID = @userid)", SP);
		}
	}
}

