namespace ChatDLL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.IO;

    internal class clsExecutives
    {
        private bool blnResult;
        private DataTable dtUsers = new DataTable();
        private object objReturnObject = new object();
        private string strDeptId = string.Empty;
        private string strDirectory = string.Empty;
        private string strErrorResult = string.Empty;
        private string strXMLFile = string.Empty;

		public clsExecutives(string strAppPhysicalPath)
        {
            this._Directory = strAppPhysicalPath;
            string str = ConfigurationManager.AppSettings["schemaDir"].ToString();
            string str2 = ConfigurationManager.AppSettings["execFile"].ToString();
            string fileName = this._Directory + str + str2;
            this.dtUsers.ReadXmlSchema(fileName);
            fileName = this._Directory + ConfigurationManager.AppSettings["XMLDir"].ToString()+"\\" + ConfigurationManager.AppSettings["execXMLFile"].ToString();
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

        public void Execute(SubRequest enmSub, string strParams)
        {
            if (enmSub == SubRequest.GetActiveAgentsForDepartment)
            {
                this.GetOnlineUsersForDepartment(strParams);
            }
        }

        private void GetOnlineUsersForDepartment(string strParam)
        {
            this._DeptId = strParam;
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.AcceptChanges();
            try
            {
                foreach (DataRow row in this.dtUsers.Select("DeptId='" + this._DeptId + "' and chatstatus=1"))
                {
                    DataRow row2 = table.NewRow();
                    row2["id"] = row["id"];
                    row2["name"] = row["name"];
                    table.Rows.Add(row2);
                }
                table.AcceptChanges();
                this._Result = true;
                this._ReturnObject = table;
            }
            catch (Exception exception)
            {
                this._ErrorResult = exception.ToString();
                this._Result = false;
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

        public string _ErrorResult
        {
            get
            {
                return this.strErrorResult;
            }
            set
            {
                this.strErrorResult = value;
            }
        }

        public bool _Result
        {
            get
            {
                return this.blnResult;
            }
            set
            {
                this.blnResult = value;
            }
        }

        public DataTable _ResultTable
        {
            get
            {
                return this.dtUsers;
            }
        }

        public object _ReturnObject
        {
            get
            {
                return this.objReturnObject;
            }
            set
            {
                this.objReturnObject = value;
            }
        }
    }
}

