namespace ChatDLL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.IO;

    public class clsChatUser
    {
        private DataTable dtUsers = new DataTable();
        private string strDeptId = string.Empty;
        private string strDirectory = string.Empty;
        private string strEMail = string.Empty;
        private string strName = string.Empty;
        private string strUserId = string.Empty;
        private string strXMLFile = string.Empty;

		public clsChatUser(string strAppPhysicalPath)
        {
            this._Directory = strAppPhysicalPath;
            string str = ConfigurationManager.AppSettings["schemaDir"].ToString();
            string str2 = ConfigurationManager.AppSettings["UserFile"].ToString();
            string fileName = this._Directory + str + str2;
            this.dtUsers.ReadXmlSchema(fileName);
            fileName = this._Directory + ConfigurationManager.AppSettings["XMLDir"].ToString()+"\\" + ConfigurationManager.AppSettings["userXMLFile"].ToString();
            this.strXMLFile = fileName;
            if (!File.Exists(this.strXMLFile))
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

        public bool ChatRequest()
        {
            bool flag = true;
            clsGeneralFunctions functions = new clsGeneralFunctions();
            Random random = new Random();
            string str = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + random.Next(100, 0x3e7).ToString();
            if (this.dtUsers.Select("email='" + this._EMail.Trim().ToLower() + "'").Length == 0)
            {
                DataRow row = this.dtUsers.NewRow();
                this._UserId = str;
                row["id"] = str;
                row["Name"] = functions.getTitleCase(this._Name.Trim());
                row["Email"] = this._EMail.Trim().ToLower();
                this.dtUsers.Rows.Add(row);
                this.dtUsers.AcceptChanges();
				System.IO.FileStream myFileStream = new System.IO.FileStream(strXMLFile, System.IO.FileMode.Create);
				System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.UTF8);
				this.dtUsers.WriteXml(myXmlWriter);
				myXmlWriter.Close();
				myFileStream.Close();
                return flag;
            }
            this._UserId = this.dtUsers.Select("email='" + this._EMail.Trim().ToLower() + "'")[0][0].ToString();
            return flag;
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
    }
}

