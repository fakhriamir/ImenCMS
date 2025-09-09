namespace ChatDLL
{
    using System;
    using System.Web;

    public class clsExecute
    {
        private bool blnResult;
        private object objReturnObject = new object();
        private string strErrorResult = string.Empty;
        private string strParams = string.Empty;
        private string strPhyAppPath = string.Empty;

        public clsExecute(string strAppPhysicalPath)
        {
            this._PhyApplicationPath =ADAL.A_CheckData.GetFilesRoot()+"\\" ;
        }

		public void ExecuteRequest(RequestType enmReq, SubRequest enmSub,string StrPath)
        {
            switch (enmReq)
            {
                case RequestType.Department:
                {
                    clsDeptMaster master = new clsDeptMaster(StrPath);
                    master.Execute(enmSub, this._Params);
                    this._Result = master._Result;
                    this._ErrorResult = master._ErrorResult;
                    this._ReturnObject = master._ReturnObject;
                    break;
                }
                case RequestType.Executives:
                {
                    clsExecutives executives = new clsExecutives(StrPath);
                    executives.Execute(enmSub, this._Params);
                    this._Result = executives._Result;
                    this._ErrorResult = executives._ErrorResult;
                    this._ReturnObject = executives._ReturnObject;
                    break;
                }
                case RequestType.ChatTransfer:
                {
                    clsChatTransfer transfer = new clsChatTransfer(StrPath);
                    transfer.Execute(enmSub, this._Params);
                    this._Result = transfer._Result;
                    this._ErrorResult = transfer._ErrorResult;
                    this._ReturnObject = transfer._ReturnObject;
                    break;
                }
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

        public string _Params
        {
            get
            {
                return this.strParams;
            }
            set
            {
                this.strParams = value;
            }
        }

        private string _PhyApplicationPath
        {
            get
            {
                return this.strPhyAppPath;
            }
            set
            {
                this.strPhyAppPath = value;
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

