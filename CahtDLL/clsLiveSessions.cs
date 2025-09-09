namespace ChatDLL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.IO;

    public class clsLiveSessions
    {
        private DataTable dtUsers = new DataTable();
        private string strDirectory = string.Empty;
        private string strXMLFile = string.Empty;

		public clsLiveSessions(string strAppPhysicalPath)
        {
            string str = ConfigurationManager.AppSettings["schemaDir"].ToString();
            string str2 = ConfigurationManager.AppSettings["LiveChats"].ToString();
            string fileName = strAppPhysicalPath + str + str2;
            this.dtUsers.ReadXmlSchema(fileName);
            fileName = strAppPhysicalPath + ConfigurationManager.AppSettings["XMLDir"].ToString() + "\\" + ConfigurationManager.AppSettings["LiveXML"].ToString();
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
		public string CreateChatSession(string strUserId, string strUserName, string strDeptId,string StrPath)
        {
            clsAdminUsers users = new clsAdminUsers(StrPath);
            int num = Tools.Tools.ConvertToInt32(ConfigurationManager.AppSettings["maxChats"]);
            string str = string.Empty;
            string str2 = string.Empty;
            int num2 = -1;
            int length = 0;
            string strEXECid = "";
            if (users._ResultTable.Select("chatstatus=1 and deptid=" + strDeptId).Length == 0)
            {
                str = "911!";
            }
            foreach (DataRow row in users._ResultTable.Select("chatstatus=1 and deptid=" + strDeptId))
            {
                length = this.dtUsers.Select("ExecId=" + row[0]).Length;
                if (num2 == -1)
                {
                    num2 = length;
                    strEXECid = row[0].ToString();
                    str2 = row["Name"].ToString();
                }
                else if (length < num2)
                {
                    num2 = length;
                    strEXECid = row[0].ToString();
                    str2 = row["Name"].ToString();
                }
            }
            if (str != "911!")
            {
                if (num2 == num)
                {
                    return "wait";
                }
                string str4 = new clsChatDetails(StrPath).SaveChatDetails(strUserId, strEXECid, DateTime.Now.Date.ToString("dd-MM-yyyy"), strDeptId,StrPath);
                DataRow row2 = this.dtUsers.NewRow();
                row2[0] = str4;
                row2[1] = strUserId;
                row2[2] = strUserName;
                row2[3] = strEXECid;
                row2[4] = DateTime.Now.Date.ToString("dd-MM-yyyy");
                row2[5] = str4.ToString() + ".xml";
                row2[6] = strDeptId;
                this.dtUsers.Rows.Add(row2);
                this.dtUsers.AcceptChanges();
				System.IO.FileStream myFileStream = new System.IO.FileStream(strXMLFile, System.IO.FileMode.Create);
				System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.UTF8);
				this.dtUsers.WriteXml(myXmlWriter);
				myXmlWriter.Close();
				myFileStream.Close();
                str = str4.ToString() + "|" + strEXECid + "|" + strUserId + "|" + str2;
                clsChatMessages messages = new clsChatMessages(StrPath, str4.ToString() + ".xml");
            }
            return str;
        }

        public void CreateChatTransferSession(string strChatId, string strUserId, string strUserName, string ChatEXEC, string strDeptId, string XMLFileName)
        {
            DataRow row = this.dtUsers.NewRow();
            row[0] = strChatId;
            row[1] = strUserId;
            row[2] = strUserName;
            row[3] = ChatEXEC;
            row[4] = DateTime.Now.Date.ToString("dd-MM-yyyy");
            row[5] = XMLFileName;
            row[6] = strDeptId;
            this.dtUsers.Rows.Add(row);
            this.dtUsers.AcceptChanges();
			System.IO.FileStream myFileStream = new System.IO.FileStream(strXMLFile, System.IO.FileMode.Create);
			System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.UTF8);
			this.dtUsers.WriteXml(myXmlWriter);
			myXmlWriter.Close();
			myFileStream.Close();
        }

        public bool DisconnectUser(string strChatID)
        {
            bool flag = true;
            try
            {
                string filterExpression = "ChatId='" + strChatID + "'";
                DataRow row = this.dtUsers.Select(filterExpression)[0];
                this.dtUsers.Rows.Remove(row);
                this.dtUsers.AcceptChanges();
				System.IO.FileStream myFileStream = new System.IO.FileStream(strXMLFile, System.IO.FileMode.Create);
				System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.UTF8);
				this.dtUsers.WriteXml(myXmlWriter);
				myXmlWriter.Close();
				myFileStream.Close();
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public string getChatFileName(string strChatId)
        {
            string str = string.Empty;
            if (this.dtUsers.Select("ChatID='" + strChatId + "'").Length > 0)
            {
                str = this.dtUsers.Select("ChatID='" + strChatId + "'")[0]["chatFile"].ToString();
            }
            return str;
        }

        public string getChatSessions(string strID)
        {
            string str = string.Empty;
            string filterExpression = "Execid='" + strID + "'";
            foreach (DataRow row in this.dtUsers.Select(filterExpression))
            {
                if (str == string.Empty)
                {
                    str = string.Concat(new object[] { row[2], "~", row[0], "|", row[3], "|", row[1] });
                }
                else
                {
                    str = string.Concat(new object[] { str, ";", row[2], "~", row[0], "|", row[3], "|", row[1] });
                }
            }
            return str;
        }

        public string getExecIdFromXMLFile(string strXMLChatFileName)
        {
            string str = string.Empty;
            if (this.dtUsers.Select("chatFile='" + strXMLChatFileName + "'").Length > 0)
            {
                str = this.dtUsers.Select("chatFile='" + strXMLChatFileName + "'")[0]["ExecId"].ToString();
            }
            return str;
        }

        //private string _Directory
        //{
        //    get
        //    {
        //        return DAL.A_CheckData.GetFilesRoot();
        //    }
        //    set
        //    {
        //        this.strDirectory = value;
        //    }
        //}
    }
}

