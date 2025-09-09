namespace ChatDLL
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.IO;

    internal class clsDeptMaster
    {
        private bool blnResult;
        private DataTable dtDepartments = new DataTable();
        private object objReturnObject = new object();
        private string strAppPhyPath = string.Empty;
        private string strDepartmentName = string.Empty;
        private string strErrorResult = string.Empty;
        private string strXMLFile = string.Empty;

		public clsDeptMaster(string strAppPath)
        {
            this._Directory = strAppPath;
            string str = ConfigurationManager.AppSettings["schemaDir"].ToString();
            string str2 = ConfigurationManager.AppSettings["Departments"].ToString();
            string fileName = this._Directory + str + str2;
            this.dtDepartments.ReadXmlSchema(fileName);
            fileName = this._Directory + ConfigurationManager.AppSettings["XMLDir"].ToString()+"\\" + ConfigurationManager.AppSettings["DepartmentXML"].ToString();
            this.strXMLFile = fileName;
            if (!File.Exists(this.strXMLFile))
            {
				System.IO.FileStream myFileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
				System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.UTF8);
				this.dtDepartments.WriteXml(myXmlWriter);
				myXmlWriter.Close();
				myFileStream.Close();
            }
            else
            {
                this.dtDepartments.ReadXml(fileName);
            }
        }

        private void CreateDepartment()
        {
            if (this.ValidateDepartmentName())
            {
                try
                {
                    DataRow row = this.dtDepartments.NewRow();
                    row["DeptId"] = this.dtDepartments.Rows.Count + 1;
                    row["DeptName"] = this._DepartmentName;
                    row["status"] = 1;
                    row["isSystem"] = 0;
                    row["CreateDate"] = DateTime.Now;
                    this.dtDepartments.Rows.Add(row);
                    this.dtDepartments.AcceptChanges();
					System.IO.FileStream myFileStream = new System.IO.FileStream(strXMLFile, System.IO.FileMode.Create);
					System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.UTF8);
					dtDepartments.WriteXml(myXmlWriter);
					myXmlWriter.Close();
					myFileStream.Close();
                    this._Result = true;
                    this._ErrorResult = "Department Added Successfully";
                }
                catch (Exception exception)
                {
                    this._Result = false;
                    this._ErrorResult = exception.ToString();
                }
            }
        }

        public void Execute(SubRequest enmSub, string strParams)
        {
            if (strParams.Length > 0)
            {
                this._DepartmentName = strParams.Split(new char[] { ',' })[0].ToString();
            }
            switch (enmSub)
            {
                case SubRequest.Create:
                    this.CreateDepartment();
                    break;

                case SubRequest.GetList:
                    this.GetDepartmentList();
                    break;

                default:
                    this._Result = false;
                    this._ErrorResult = "Not a Valid Sub Category for this Operation";
                    break;
            }
        }

        private void GetDepartmentList()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id");
            table.Columns.Add("Name");
            table.AcceptChanges();
            try
            {
                foreach (DataRow row in this.dtDepartments.Rows)
                {
                    if (row["status"].ToString() == "1")
                    {
                        DataRow row2 = table.NewRow();
                        row2["id"] = row["DeptId"];
                        row2["Name"] = row["DeptName"];
                        table.Rows.Add(row2);
                    }
                }
                this._Result = true;
            }
            catch
            {
                this._Result = false;
            }
            table.AcceptChanges();
            this._ReturnObject = table;
        }

        private bool ValidateDepartmentName()
        {
            this.blnResult = true;
            if (this._DepartmentName.Trim().Length == 0)
            {
                this.blnResult = false;
                this._Result = false;
                this._ErrorResult = "Department Name cannot be left Blank";
            }
            if (this._DepartmentName.Trim().ToLower() == "default")
            {
                this.blnResult = false;
                this._Result = false;
                this._ErrorResult = "You cannot name Department as Default";
            }
            return this.blnResult;
        }

        public string _DepartmentName
        {
            get
            {
                return this.strDepartmentName;
            }
            set
            {
                this.strDepartmentName = value;
            }
        }

        private string _Directory
        {
            get
            {
                return this.strAppPhyPath;
            }
            set
            {
                this.strAppPhyPath = value;
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

