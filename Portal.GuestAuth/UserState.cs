using System;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;
namespace Portal.GuestAuth
{
	public class UserState
	{
		public static Boolean CheckGuestUserLogin()
		{
			if (HttpContext.Current.Request.Cookies["GuestUserID"] != null)
			{
				try
				{
					string UID = CheckTokenGuestUserID().ToString();
					return true;
				}
				catch
				{
					return false;
				}
			}
			else
				return false; 
		}
		public static string SignOutUrl = "http://internal.samen.ir/Members/Logout.aspx";
		public static string RedirectUrl = "http://internal.samen.ir";
		public static int GuestUserID()
		{
			if (HttpContext.Current.Request.Cookies["GuestUserID"] != null)
			{
				try
				{
					return CheckTokenGuestUserID();
				}
				catch
				{
					return 0;
					
				}
			}
			else
				return 0;
		}
		private static int CheckTokenGuestUserID()
		{
			if (HttpContext.Current.Request.Cookies["GuestUserID"] == null)
				return -1;
			string KeyStr = MyDecry(HttpContext.Current.Request.Cookies["GuestUserID"].Value);
			if (KeyStr.IndexOf("@") == -1)
				return -1;
			try
			{
				DateTime MyD = Convert.ToDateTime(KeyStr.Substring(KeyStr.IndexOf("@") + 1));
				TimeSpan TS = MyD-DateTime.Now;

				int comp = TS.Minutes;
				if (comp > 15)
					return -1;
				if (comp < -15)
					return -1;
				
				HttpCookie httpCookie1 = new HttpCookie("GuestUserID", MyEncry(KeyStr.Substring(0, KeyStr.IndexOf("@")) + "@" + DateTime.Now.ToString().Replace("ب.ظ", "PM").Replace("ق.ظ", "AM")));
				httpCookie1.Expires = DateTime.Now.AddMinutes(15);
				httpCookie1.Domain = "samen.ir";
				HttpContext.Current.Response.Cookies.Add(httpCookie1);
				
				return ConvertToInt32(KeyStr.Substring(0, KeyStr.IndexOf("@")));
			}
			catch
			{
				return -1;
			}			
		}
		private static int ConvertToInt32(object InNumber)
		{
			int OutNumber = -1;
			int.TryParse(InNumber.ToString(), out OutNumber);
			return OutNumber;
		}
		private static string MyDecry(string InText)
		{
			if (InText == "" || InText == "null")
				return "";
			try
			{
				return Decrypt(InText, CryptSTR);
			}
			catch
			{
				return "";
			}
		}private static string CryptSTR = "amir";
		private static string Decrypt(string cipherSTR, string key)
		{
			byte[] buffer = Convert.FromBase64String(cipherSTR);
			byte[] buffer2 = MakeMD5(key);
			byte[] rgbKey = Key24bit(buffer2);
			byte[] rgbIV = Key8bit(buffer2);
			byte[] bytes = null;
			TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
			MemoryStream stream = new MemoryStream();
			CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
			stream2.Write(buffer, 0, buffer.Length);
			stream2.Close();
			bytes = stream.ToArray();
			return Encoding.ASCII.GetString(bytes);
		}
		private static byte[] Key24bit(byte[] md5)
		{
			byte[] buffer = new byte[0x18];
			for (int i = 0; i < 0x18; i++)
			{
				if (i < 0x10)
				{
					buffer[i] = md5[i];
				}
				else
				{
					buffer[i] = md5[i - 0x10];
				}
			}
			return buffer;
		}
		private static byte[] Key8bit(byte[] md5)
		{
			byte[] buffer = new byte[8];
			for (int i = 0; i < 8; i++)
			{
				buffer[i] = md5[i];
			}
			return buffer;
		}
		private static byte[] MakeMD5(string variable)
		{
			MD5 md = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.ASCII.GetBytes(variable);
			byte[] inArray = md.ComputeHash(bytes);
			Convert.ToBase64String(inArray);
			return inArray;
		}
		public static string MyEncry(string InText)
		{
			return Encrypt(InText, CryptSTR);
		}
		private static string Encrypt(string str, string key)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(str);
			byte[] buffer2 = MakeMD5(key);
			byte[] rgbKey = Key24bit(buffer2);
			byte[] rgbIV = Key8bit(buffer2);
			TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
			MemoryStream stream = new MemoryStream();
			CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
			stream2.Write(bytes, 0, bytes.Length);
			stream2.Close();
			return Convert.ToBase64String(stream.ToArray());
		}		
	}	
}