namespace ChatDLL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Threading;

    public class clsChatMessages
    {
        private DataTable dtMessages = new DataTable();
        private string strDirectory = string.Empty;
        private string strEMail = string.Empty;
        private string strName = string.Empty;
        private string strUserId = string.Empty;
        private string strXMLFile = string.Empty;

        public clsChatMessages(string strAppPhysicalPath, string strXMLFileName)
        {
            //this._Directory = strAppPhysicalPath;
            string str = ConfigurationManager.AppSettings["schemaDir"].ToString();
            string str2 = ConfigurationManager.AppSettings["chatMessages"].ToString();
            string fileName = strAppPhysicalPath + str + str2;
            this.dtMessages.ReadXmlSchema(fileName);
            fileName = strAppPhysicalPath + ConfigurationManager.AppSettings["XMLDir"].ToString() + "\\" + strXMLFileName;
            this.strXMLFile = fileName;
            if (!File.Exists(this.strXMLFile))
            {
				System.IO.FileStream myFileStream = new System.IO.FileStream   (fileName, System.IO.FileMode.Create);
				System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.UTF8);
				this.dtMessages.WriteXml(myXmlWriter);
				myXmlWriter.Close();
				myFileStream.Close();
              //  this.dtMessages.WriteXml(fileName);
            }
            else
            {
				System.IO.FileStream fsReadXml = new System.IO.FileStream(fileName, System.IO.FileMode.Open);
				// Create an XmlTextReader to read the file.
				System.Xml.XmlTextReader myXmlReader = new System.Xml.XmlTextReader(fsReadXml);

				this.dtMessages.ReadXml(myXmlReader);
				fsReadXml.Close();
				myXmlReader.Close();
            }
        }

        public bool AddMessage(string strFromUser, string strToId, msgTokens enmToken, string strMessage, bool isAgent)
        {
			strMessage = strMessage.Replace("\\", "");
            DataRow row = this.dtMessages.NewRow();
            if (this.dtMessages.Rows.Count == 0)
            {
				string str = "";//$@~Chat Powered By: <a target='_blank' href='http://www.arlivesupport.com' style='font-family:Verdana; font-size:11px; color:#0000CC'>A R Live Support</a>";
                row["MsgId"] = this.dtMessages.Rows.Count + 1;
                row["FromId"] = "0";
                row["ToId"] = strToId;
                row["Token"] = enmToken.ToString();
                row["Message"] = str;
                row["ExecStatus"] = "1";
                row["dateTime"] = DateTime.Now;
                row["UserStatus"] = "1";
                row["isByAgent"] = true;
                this.dtMessages.Rows.Add(row);
            }
            bool flag = true;
            row = this.dtMessages.NewRow();
            int num = 0;
            int num2 = 0;
            if (isAgent)
            {
                num = 0;
                num2 = 1;
            }
            else
            {
                num = 1;
                num2 = 0;
            }
            row["MsgId"] = this.dtMessages.Rows.Count + 1;
            row["FromId"] = strFromUser;
            row["ToId"] = strToId;
            row["Token"] = enmToken.ToString();
            row["Message"] = strMessage;
            row["ExecStatus"] = num;
            row["dateTime"] = DateTime.Now;
            row["UserStatus"] = num2;
            row["isByAgent"] = isAgent.ToString();
            this.dtMessages.Rows.Add(row);
            this.dtMessages.AcceptChanges();
            try
            {
				System.IO.FileStream myFileStream = new System.IO.FileStream(strXMLFile, System.IO.FileMode.Create);
				System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.UTF8);
				this.dtMessages.WriteXml(myXmlWriter);
				myXmlWriter.Close();
				myFileStream.Close();
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public DataTable getAllUnReadMessages(bool isAgent)
        {
            string filterExpression = string.Empty;
            if (isAgent)
            {
                filterExpression = "ExecStatus=1";
            }
            else
            {
                filterExpression = "UserStatus=1";
            }
            DataTable table = this.dtMessages.Clone();
            foreach (DataRow row in this.dtMessages.Select(filterExpression))
            {
                DataRow row2 = table.NewRow();
                row2.ItemArray = row.ItemArray;
                table.Rows.Add(row2);
            }
            return table;
        }
		public string getConversation(string strChatId, string strFromID, bool isAgentRequested,string StrPath)
        {
            string str = string.Empty;
            clsAdminUsers users = new clsAdminUsers(StrPath);
            foreach (DataRow row in this.dtMessages.Rows)
            {
                if (row["isByAgent"].ToString().ToLower() == "false")
                {
                    str = str + "<font color=red><b>مشتري:</b></font> " + row[4].ToString() + "<br>";
                }
                else if (row["fromid"].ToString() == "0")
                {
					 if(row["message"].ToString()!="")
						str = str + "<font color=green>" + row["message"].ToString().Split(new char[] { '~' })[1] + "</font><br>";
                }
                else
                {
                    string str3 = str;
                    str = str3 + "<font color=blue><b>" + users.getExecutiveName(row["fromid"].ToString()) + ":</b></font> " + row[4].ToString() + "<br>";
                }
            }
            return str;
        }
		public static string getUnReadMessages(string strAppPhysicalPath, string strXMLFileName, string strUserId, bool isAgent, string strCustName, string strExecName)
        {
            string str = string.Empty;
            DataTable table = new DataTable();
            string fileName = "";
            bool flag = false;
            Mutex mutex = new Mutex();
            try
            {
                mutex.WaitOne();
                string str3 = ConfigurationManager.AppSettings["schemaDir"].ToString();
                string str4 = ConfigurationManager.AppSettings["chatMessages"].ToString();
                fileName = strAppPhysicalPath + str3 + str4;
                table.ReadXmlSchema(fileName);
                fileName = strAppPhysicalPath + ConfigurationManager.AppSettings["XMLDir"].ToString()+"" + strXMLFileName;
                table.ReadXml(fileName);
                string filterExpression = string.Empty;
                string str6 = string.Empty;
                if (isAgent)
                {
                    filterExpression = "ExecStatus=1 and toid='" + strUserId + "'";
                    str6 = "ExecStatus";
                }
                else
                {
                    filterExpression = "UserStatus=1 and toid='" + strUserId + "'";
                    str6 = "UserStatus";
                }
                foreach (DataRow row in table.Select(filterExpression))
                {
                    string str8;
                    if (isAgent)
                    {
                        str8 = str;
                        str = str8 + "<font color=blue><b>" + strCustName + " :</b></font> " + row["message"].ToString() + "<br>";
                    }
                    else if ((row["message"].ToString().Split(new char[] { '~' })[0] == "$@") && (row["message"].ToString().Split(new char[] { '~' }).Length == 2))
                    {
                        str = str + "<font color=green>" + row["message"].ToString().Split(new char[] { '~' })[1] + "</b></font><br>";
                        flag = true;
                    }
                    else
                    {
                        str8 = str;
                        str = str8 + "<font color=blue><b>" + strExecName + " :</b></font> " + row["message"].ToString() + "<br>";
                    }
                    DataRow row2 = table.NewRow();
                    int pos = Tools.Tools.ConvertToInt32(row[0]);
                    row2.ItemArray = row.ItemArray;
                    table.Rows.Remove(row);
                    row2[str6] = 0;
                    pos--;
                    table.Rows.InsertAt(row2, pos);
                }
            }
            catch
            {
                str = "";
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            try
            {
                try
                {
					System.IO.FileStream myFileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
					System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.UTF8);
					table.WriteXml(myXmlWriter);
					myXmlWriter.Close();
					myFileStream.Close();
                }
                catch
                {
                    str = string.Empty;
                    str = "";
                }
            }
            finally
            {
            }
            mutex = null;
            if (flag)
            {
                str = str + "$trns";
            }
            return str;
        }

        //private string _Directory
        //{
        //    get
        //    {
        //        return DAL.CheckData.GetFilesRoot(true);
        //    }
        //    set
        //    {
        //        this.strDirectory = value;
        //    }
        //}

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
                return this.dtMessages;
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

