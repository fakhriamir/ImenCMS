namespace ChatDLL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.IO;

    public class clsChatDetails
    {
        private DataTable dtUsers = new DataTable();
        private string strDirectory = string.Empty;
        private string strXMLFile = string.Empty;

        public clsChatDetails(string strAppPhysicalPath)
        {
            this._Directory = strAppPhysicalPath;
            string str = ConfigurationManager.AppSettings["schemaDir"].ToString();
            string str2 = ConfigurationManager.AppSettings["chatDetails"].ToString();
            string fileName = this._Directory + str + str2;
            this.dtUsers.ReadXmlSchema(fileName);
            fileName = this._Directory + ConfigurationManager.AppSettings["XMLDir"].ToString()+"\\" + ConfigurationManager.AppSettings["chatDetailsXML"].ToString();
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
				this.dtUsers.ReadXml(strXMLFile);
				
            }
        }
        public string getChatFileName(string strChatId)
        {
            string str = string.Empty;
            if (this._ChatDataTable.Select("ChatID='" + strChatId + "'").Length > 0)
            {
                str = this._ChatDataTable.Select("ChatID='" + strChatId + "'")[0]["chatFile"].ToString();
            }
            return str;
        }

        public string getChatUserId(string strChatId)
        {
            string str = string.Empty;
            if (this._ChatDataTable.Select("ChatID='" + strChatId + "'").Length > 0)
            {
                str = this._ChatDataTable.Select("ChatID='" + strChatId + "'")[0]["UID"].ToString();
            }
            return str;
        }

        public string SaveChatDetails(string strUserid, string strEXECid, string strDate, string strDeptId,string StrPath)
        {
            string str = "0";
            clsGeneralFunctions functions = new clsGeneralFunctions();
            Random random = new Random();
            str = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + random.Next(100, 0x3e7).ToString();
            DataRow row = this.dtUsers.NewRow();
            row[0] = str;
            row[1] = strUserid;
            row[2] = strEXECid;
            row[3] = strDate;
            row[4] = str + ".xml";
            row[5] = strDeptId.Trim();
            this.dtUsers.Rows.Add(row);
            this.dtUsers.AcceptChanges();

			System.IO.FileStream myFileStream = new System.IO.FileStream(strXMLFile, System.IO.FileMode.Create);
			System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.UTF8);
			
            this.dtUsers.WriteXml(myXmlWriter);
			myXmlWriter.Close();
			myFileStream.Close();
            return str;
        }

        public string SaveChatTransferDetails(string strUserid, string strEXECid, string strDeptId, string strFileName)
        {
            clsGeneralFunctions functions = new clsGeneralFunctions();
            Random random = new Random();
            string str = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + random.Next(100, 0x3e7).ToString();
            DataRow row = this.dtUsers.NewRow();
            row[0] = str;
            row[1] = strUserid;
            row[2] = strEXECid;
            row[3] = DateTime.Now.ToString("dd-MM-yyyy");
            row[4] = strFileName;
            row[5] = strDeptId.Trim();
            this.dtUsers.Rows.Add(row);
            this.dtUsers.AcceptChanges();

			System.IO.FileStream myFileStream = new System.IO.FileStream(strXMLFile, System.IO.FileMode.Create);
			System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.UTF8);
			
            this.dtUsers.WriteXml(myXmlWriter);
			myXmlWriter.Close();
			myFileStream.Close();
            return str;
        }
        private DataTable _ChatDataTable
        {
            get
            {
                return this.dtUsers;
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
    }
}

