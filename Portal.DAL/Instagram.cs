using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Tools
{
	public class Instagram
	{
		public static string[] UserTypeName = { "غیر فعال", "معمولی", "برنزی", "برنزی ویژه", "نقره ای", "نقره ای ویژه", "طلایی", "طلایی ویژه" };
		
		public static string GetUserState(int InstagramUserID)
		{
			return UserTypeName[DAL.ExecuteData.CNTData("SELECT UserType  FROM InstagramUser  WHERE (InstagramUserID = " + InstagramUserID + ")")];
		}
		public static int GetDataInstagramToken(string code,int InstagramUserID)
		{
			//string json = "";
			int RetID = 0;
			NameValueCollection parameters = new NameValueCollection();
			parameters.Add("client_id", "27d1dfff796b4f5abc2dfdfc9f50e270");
			parameters.Add("client_secret", "45b2d6c329a94e009853752ce1f4d967");
			parameters.Add("grant_type", "authorization_code");
			parameters.Add("redirect_uri", "http://www.imencms.com/Instagram/default");
			parameters.Add("code", code);

			WebClient client = new WebClient();
			var result = client.UploadValues("https://api.instagram.com/oauth/access_token", "POST", parameters);
			var response = System.Text.Encoding.Default.GetString(result);
			WriteTemp(response);

			//HttpContext.Current.Response.Write(response);
			// deserializing nested JSON string to object  
			var jsResult = (JObject)JsonConvert.DeserializeObject(response);

			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@AccessToken", (string)jsResult["access_token"]);
			SP.AddWithValue("@UserBio", jsResult["user"]["bio"]);
			SP.AddWithValue("@UName", jsResult["user"]["username"]);
			SP.AddWithValue("@FullName", jsResult["user"]["full_name"]);
			SP.AddWithValue("@ProfilePicture", jsResult["user"]["profile_picture"]);
			SP.AddWithValue("@Website", jsResult["user"]["website"]);
			SP.AddWithValue("@UID", jsResult["user"]["id"]);
			SP.AddWithValue("@UnitID", Tools.GetViewUnitID);
			int CNT = DAL.ExecuteData.CNTData("SELECT InstagramUserID  FROM InstagramUser  WHERE (UID = @UID)",SP);
			if (CNT != 0)
				InstagramUserID = CNT;

			if (InstagramUserID ==-1)
			{
				RetID = DAL.ExecuteData.CNTData("INSERT INTO InstagramUser(AccessToken, UID, UserBio, UName, FullName, ProfilePicture, Website, UnitID) OUTPUT INSERTED.InstagramUserID VALUES (@AccessToken, @UID, @UserBio, @UName, @FullName, @ProfilePicture, @Website, @UnitID)", SP);
				return RetID;
			}
			
			DAL.ExecuteData.InsertData("UPDATE InstagramUser SET AccessToken=@AccessToken, UID =@UID, UserBio =@UserBio, UName =@UName, FullName =@FullName, ProfilePicture =@ProfilePicture, Website =@Website, UnitID =@UnitID  where InstagramUserID=" + InstagramUserID, SP);
			GetUserFollowersCount(InstagramUserID);
			return InstagramUserID;

		}
		public static void WriteTemp(string tt)
		{
			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@txt", tt);
			DAL.ExecuteData.InsertData("INSERT INTO InstagramTemp(txt) VALUES(@txt)", SP);
		}
		public static bool GetUserFollowersCount(int InstageramUserID)
		{
			//{"meta": {"error_type": "OAuthAccessTokenException", "error_message": "The access_token provided is invalid.", "code": 400}}
			//{"meta": {"code": 200}, "data": {"full_name": "Intellectualquotes", "bio": "Technology company", "counts": {"follows": 4510, "media": 18, "followed_by": 1715}, "username": "nikcms", "id": "4514810115", "website": "https://goo.gl/fHAbtZ"}}
			//{"meta": {"code": 200}, "data": {"counts": {"": 4510, "media": 18, "": 1715}, "website": "https://goo.gl/fHAbtZ"}}
			if (InstageramUserID ==-1)
				return false;
			string userid = DAL.ExecuteData.CNTDataStr("SELECT UID  FROM InstagramUser WHERE (InstagramUserID = "+InstageramUserID+")");
			WebClient client = new WebClient();
			var response = client.DownloadString("https://api.instagram.com/v1/users/"+userid+"/?access_token="+ GetAccessToken(InstageramUserID));
			//string response = "{\"data\": {\"full_name\": \"Intellectualquotes\", \"profile_picture\": \"https://scontent.cdninstagram.com/t51.2885-19/s150x150/16789460_640816252776241_4858808175962357760_n.jpg\", \"username\": \"nikcms\", \"id\": \"4514810115\", \"counts\": {\"followed_by\": 1652, \"follows\": 4511, \"media\": 18}, \"bio\": \"Technology company\", \"website\": \"https://goo.gl/fHAbtZ\"}, \"meta\": {\"code\": 200}}";

			WriteTemp(response);
			// deserializing nested JSON string to object  
			var jsResult = (JObject)JsonConvert.DeserializeObject(response);
			if (jsResult == null)
				return false;
			if ((string)jsResult["meta"]["code"] != "200")
				return false;

			SqlParameterCollection SP = new SqlCommand().Parameters;
			SP.AddWithValue("@UserBio", jsResult["data"]["bio"]);
			SP.AddWithValue("@UName", jsResult["data"]["username"]);
			SP.AddWithValue("@FullName", jsResult["data"]["full_name"]);
			SP.AddWithValue("@ProfilePicture", jsResult["data"]["profile_picture"]);
			SP.AddWithValue("@Website", jsResult["data"]["website"]);
			SP.AddWithValue("@PostCNT", jsResult["data"]["counts"]["media"]);
			SP.AddWithValue("@FollowersCNT", jsResult["data"]["counts"]["followed_by"]);
			SP.AddWithValue("@FollowingCNT", jsResult["data"]["counts"]["follows"]);
			DAL.ExecuteData.InsertData("UPDATE InstagramUser SET PostCNT =@PostCNT, FollowersCNT =@FollowersCNT, FollowingCNT =@FollowingCNT, UserBio =@UserBio, Website =@Website, ProfilePicture =@ProfilePicture, FullName =@FullName, UName =@UName WHERE(InstagramUserID = " + InstageramUserID+")",SP);
			return true;
		}
		public static string GetAccessToken(int UserID)
		{
			return DAL.ExecuteData.CNTDataStr("SELECT AccessToken  FROM InstagramUser WHERE (InstagramUserID = " + UserID + ")");

		}
	}
}