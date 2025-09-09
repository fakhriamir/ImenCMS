namespace ChatDLL
{
    using System;
    using System.Configuration;
    using System.Data;

    public class clsSchemas
    {
        private string strDirectory;

        public clsSchemas(string strDirectoryPath)
        {
            this._Directory = strDirectoryPath;
        }

        private void CreateDefaultDept(string UnitID)
        {
            string fileName = this._Directory + ConfigurationManager.AppSettings["XMLDir"].ToString()+UnitID+"\\" + ConfigurationManager.AppSettings["DepartmentXML"].ToString();
            string str2 = this._Directory + ConfigurationManager.AppSettings["schemaDir"].ToString() + ConfigurationManager.AppSettings["Departments"].ToString();
            DataTable table = new DataTable();
            table.ReadXmlSchema(str2);
            DataRow row = table.NewRow();
            row["DeptId"] = 1;
            row["DeptName"] = "Default";
            row["status"] = 1;
            row["isSystem"] = 1;
            row["CreateDate"] = DateTime.Now;
            table.Rows.Add(row);
            table.AcceptChanges();
			System.IO.FileStream myFileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
			System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.UTF8);
			table.WriteXml(myXmlWriter);
			myXmlWriter.Close();
			myFileStream.Close();
        }

        public bool CreateSchemas(string UnitID)
        {
            bool flag = true;
            try
            {
                DataTable table = new DataTable("Executive");
                string fileName = this._Directory + ConfigurationManager.AppSettings["schemaDir"].ToString() + ConfigurationManager.AppSettings["execFile"].ToString();
                table.Columns.Add("ID", typeof(string));
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Email", typeof(string));
                table.Columns.Add("DeptId", typeof(int));
                table.Columns.Add("Password", typeof(string));
                table.Columns.Add("userType", typeof(short));
                table.Columns.Add("status", typeof(short));
                table.Columns.Add("chatStatus", typeof(short));
                table.WriteXmlSchema(fileName);
                table = new DataTable("chatUser");
                fileName = this._Directory + ConfigurationManager.AppSettings["schemaDir"].ToString() + ConfigurationManager.AppSettings["UserFile"].ToString();
                table.Columns.Add("ID", typeof(string));
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Email", typeof(string));
                table.WriteXmlSchema(fileName);
                table = new DataTable("chatDetails");
                fileName = this._Directory + ConfigurationManager.AppSettings["schemaDir"].ToString() + ConfigurationManager.AppSettings["chatDetails"].ToString();
                table.Columns.Add("ChatID", typeof(string));
                table.Columns.Add("UID", typeof(string));
                table.Columns.Add("ExecId", typeof(string));
                table.Columns.Add("Date", typeof(string));
                table.Columns.Add("chatFile", typeof(string));
                table.Columns.Add("DeptId", typeof(int));
                table.WriteXmlSchema(fileName);
                table = new DataTable("chatMessages");
                fileName = this._Directory + ConfigurationManager.AppSettings["schemaDir"].ToString() + ConfigurationManager.AppSettings["chatMessages"].ToString();
                table.Columns.Add("MsgId", typeof(int));
                table.Columns.Add("FromId", typeof(string));
                table.Columns.Add("ToId", typeof(string));
                table.Columns.Add("Token", typeof(string));
                table.Columns.Add("Message", typeof(string));
                table.Columns.Add("ExecStatus", typeof(short));
                table.Columns.Add("dateTime", typeof(DateTime));
                table.Columns.Add("UserStatus", typeof(short));
                table.Columns.Add("isByAgent", typeof(string));
                table.WriteXmlSchema(fileName);
                table = new DataTable("liveChats");
                fileName = this._Directory + ConfigurationManager.AppSettings["schemaDir"].ToString() + ConfigurationManager.AppSettings["LiveChats"].ToString();
                table.Columns.Add("ChatID", typeof(string));
                table.Columns.Add("UID", typeof(string));
                table.Columns.Add("UName", typeof(string));
                table.Columns.Add("ExecId", typeof(string));
                table.Columns.Add("Date", typeof(string));
                table.Columns.Add("chatFile", typeof(string));
                table.Columns.Add("DeptId", typeof(int));
                table.WriteXmlSchema(fileName);
                table = new DataTable("DeptMaster");
                fileName = this._Directory + ConfigurationManager.AppSettings["schemaDir"].ToString() + ConfigurationManager.AppSettings["Departments"].ToString();
                table.Columns.Add("DeptId", typeof(int));
                table.Columns.Add("DeptName", typeof(string));
                table.Columns.Add("status", typeof(int));
                table.Columns.Add("isSystem", typeof(int));
                table.Columns.Add("CreateDate", typeof(DateTime));
                table.WriteXmlSchema(fileName);
                this.CreateDefaultDept(UnitID);
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public string _Directory
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

