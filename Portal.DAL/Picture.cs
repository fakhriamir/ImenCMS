using System.Web;
using System.IO;
namespace Tools
{
	public class Picture
	{
		public static bool CheckPic(HttpPostedFile MyFile)
		{
			string str = Path.GetExtension(MyFile.FileName).ToLower();
			if (((str != ".jpg") && (str != ".gif")) && (str != ".png") && (str != ".tif"))
			{
				return false;
			}
			str = MyFile.FileName.Substring(MyFile.FileName.IndexOf(".")).ToLower() ;
			if (((str != ".jpg") && (str != ".gif")) && (str != ".png") && (str != ".tif"))
			{
				return false;
			}
			return true;
		}
		public static void PicSize(HttpPostedFile MyFile, string FilePath)
		{

		}
	}
}