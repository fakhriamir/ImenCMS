namespace ChatDLL
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading;

    public class clsGeneralFunctions
    {
        public string getTitleCase(string strString)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(strString.Trim());
        }

        public bool isValidEMail(string strEMail)
        {
            return Regex.IsMatch(strEMail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}

