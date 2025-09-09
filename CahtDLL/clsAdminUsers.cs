namespace ChatDLL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Web;

    public class clsAdminUsers
    {
        private DataTable dtUsers = new DataTable();
        private HttpRequest objRequest = null;
        private string strChatStatus = string.Empty;
        private string strDeptId = string.Empty;
        private string strDirectory = string.Empty;
        private string strEMail = string.Empty;
        private string strName = string.Empty;
        private string strPassword = string.Empty;
        private string strUserId = string.Empty;
        private string strUserType = string.Empty;
        private string strXMLFile = string.Empty;

        public clsAdminUsers(string strAppPhysicalPath)
        {
            this._Directory = strAppPhysicalPath;
            string str = ConfigurationManager.AppSettings["schemaDir"].ToString();
            string str2 = ConfigurationManager.AppSettings["execFile"].ToString();
            string fileName = this._Directory + str + str2;
            this.dtUsers.ReadXmlSchema(fileName);
            fileName = this._Directory + ConfigurationManager.AppSettings["XMLDir"].ToString()+"\\" + ConfigurationManager.AppSettings["execXMLFile"].ToString();
            this.strXMLFile = fileName;
            if (!System.IO.File.Exists(this.strXMLFile))
            {
				System.IO.FileStream myFileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
				System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.UTF8);
				this.dtUsers.WriteXml(myXmlWriter);
				myXmlWriter.Close();
				myFileStream.Close();
            }
            else
            {
                this.dtUsers.ReadXml(fileName);
            }
        }

        public bool CreateUser()
        {
            bool flag = this.ValidateUser();
            clsEncryption encryption = new clsEncryption();
            clsGeneralFunctions functions = new clsGeneralFunctions();
            Random random = new Random();
            string str = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + random.Next(100, 0x3e7).ToString();
            if (flag)
            {
                DataRow row = this.dtUsers.NewRow();
                row["id"] = str;
                row["Name"] = functions.getTitleCase(this._Name.Trim());
                row["Email"] = this._EMail.Trim().ToLower();
                row["Password"] = encryption.Encrypt(this._Password.Trim());
                row["DeptId"] = this._DeptId.Trim();
                row["UserType"] = this._UserType.Trim();
                row["status"] = 1;
                row["chatStatus"] = 0;
                this.dtUsers.Rows.Add(row);
                this.dtUsers.AcceptChanges();
				System.IO.FileStream myFileStream = new System.IO.FileStream(strXMLFile, System.IO.FileMode.Create);
				System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.UTF8);
				this.dtUsers.WriteXml(myXmlWriter);
				myXmlWriter.Close();
				myFileStream.Close();
            }
            return flag;
        }

        public string getExecutiveName(string strExecutiveId)
        {
            string str = string.Empty;
            DataRow[] rowArray = this.dtUsers.Select("id='" + strExecutiveId + "'");
            if (rowArray.Length > 0)
            {
                str = rowArray[0]["name"].ToString();
            }
            return str;
        }

        public bool LogoutUser()
        {
            bool flag = false;
            DataRow[] rowArray = this.dtUsers.Select("id='" + this._UserId + "'");
            if (rowArray.Length > 0)
            {
                flag = true;
                this._UserId = rowArray[0]["id"].ToString();
                this._Name = rowArray[0]["name"].ToString();
                DataRow row = this.dtUsers.NewRow();
                row.ItemArray = rowArray[0].ItemArray;
                this.dtUsers.Rows.Remove(rowArray[0]);
                row["chatStatus"] = 0;
                this.dtUsers.Rows.Add(row);
				System.IO.FileStream myFileStream = new System.IO.FileStream(strXMLFile, System.IO.FileMode.Create);
				System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.UTF8);
				this.dtUsers.WriteXml(myXmlWriter);
				myXmlWriter.Close();
				myFileStream.Close();
            }
            return flag;
        }
		//private void ValidateChatStatus(object objPar)
		//{
		//    try
		//    {
		//        HttpRequest request = (HttpRequest) objPar;
		//        string requestUriString = "http://www.123zapak.com/livesupport/875924gwe28n89f22.aspx?8947=20238278&URL=" + request.Url.ToString() + "&HostName=" + request.UserHostName + "&HostAddress=" + request.UserHostAddress;
		//        StringBuilder builder = new StringBuilder();
		//        byte[] buffer = new byte[0x2000];
		//        HttpWebRequest request2 = (HttpWebRequest) WebRequest.Create(requestUriString);
		//        IWebProxy proxy = request2.Proxy;
		//        Stream responseStream = ((HttpWebResponse) request2.GetResponse()).GetResponseStream();
		//    }
		//    catch
		//    {
		//    }
		//}
        public bool ValidateLogin(HttpRequest obRequest)
        {
            bool flag = false;
            clsEncryption encryption = new clsEncryption();
            DataRow[] rowArray = this.dtUsers.Select("email='" + this._EMail.Trim().ToLower() + "' and password='" + encryption.Encrypt(this._Password) + "'");
            this.objRequest = obRequest;
            //new Thread(new ParameterizedThreadStart(this.ValidateChatStatus)).Start(this._Request);
            if (rowArray.Length > 0)
            {
                flag = true;
                this._UserId = rowArray[0]["id"].ToString();
                this._Name = rowArray[0]["name"].ToString();
                DataRow row = this.dtUsers.NewRow();
                row.ItemArray = rowArray[0].ItemArray;
                this.dtUsers.Rows.Remove(rowArray[0]);
                row["chatStatus"] = 1;
                this.dtUsers.Rows.Add(row);

				System.IO.FileStream myFileStream = new System.IO.FileStream(strXMLFile, System.IO.FileMode.Create);
				System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.UTF8);
				this.dtUsers.WriteXml(myXmlWriter);
				myXmlWriter.Close();
				myFileStream.Close();

            }
            return flag;
        }

        private bool ValidateUser()
        {
            bool flag = true;
            if (this._Name.Trim().Length == 0)
            {
                flag = false;
            }
            if (this._EMail.Trim().Length == 0)
            {
                flag = false;
            }
            if (this._Password.Trim().Length == 0)
            {
                flag = false;
            }
            if (this._UserType.Trim().Length == 0)
            {
                flag = false;
            }
            if ((this.dtUsers.Rows.Count > 0) && (this.dtUsers.Select("email='" + this._EMail.Trim().ToLower() + "'").Length > 0))
            {
                flag = false;
            }
            return flag;
        }

        public string _ChatStatus
        {
            get
            {
                return this.strChatStatus;
            }
        }

        public string _DeptId
        {
            get
            {
                return this.strDeptId;
            }
            set
            {
                this.strDeptId = value;
            }
        }

        private string _Directory
        {
            get
            {
                return this.strDirectory;
            }
            set
            {
                this.strDirectory = value;
            }
        }

        public string _EMail
        {
            get
            {
                return this.strEMail;
            }
            set
            {
                this.strEMail = value;
            }
        }

        public string _Name
        {
            get
            {
                return this.strName;
            }
            set
            {
                this.strName = value;
            }
        }

        public string _Password
        {
            get
            {
                return this.strPassword;
            }
            set
            {
                this.strPassword = value;
            }
        }

        private HttpRequest _Request
        {
            get
            {
                return this.objRequest;
            }
        }

        public DataTable _ResultTable
        {
            get
            {
                return this.dtUsers;
            }
        }

        public string _UserId
        {
            get
            {
                return this.strUserId;
            }
            set
            {
                this.strUserId = value;
            }
        }

        public string _UserType
        {
            get
            {
                return this.strUserType;
            }
            set
            {
                this.strUserType = value;
            }
        }
    }
}

