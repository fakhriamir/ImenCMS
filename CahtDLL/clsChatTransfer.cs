namespace ChatDLL
{
    using System;

    internal class clsChatTransfer
    {
        private bool blnResult;
        private object objParams;
        private object objReturnObject = new object();
        private string strAppPhyPath = string.Empty;
        private string strErrorResult = string.Empty;
        private string strXMLFile = string.Empty;

        public clsChatTransfer(string strAppPath)
        {
            this._Directory = strAppPath;
        }

		public void Execute(SubRequest enmSub, string strParams)
        {
            this._Params = strParams;
            if (enmSub == SubRequest.Transfer)
            {
                this.TrabsferChat();
            }
        }

        private void TrabsferChat()
        {
            string[] strArray = this._Params.ToString().Split(new char[] { ',' });
            string strChatId = strArray[0];
            string strEXECid = strArray[1];
            string strDeptId = strArray[2];
            string strUserName = strArray[3];
            clsChatDetails details = new clsChatDetails(this._Directory);
            clsLiveSessions sessions = new clsLiveSessions(this._Directory);
            clsAdminUsers users = new clsAdminUsers(this._Directory);
            string strFileName = details.getChatFileName(strChatId);
            string strUserid = details.getChatUserId(strChatId);
            if (strFileName == string.Empty)
            {
                this._Result = false;
            }
            else
            {
                string str7 = details.SaveChatTransferDetails(strUserid, strEXECid, strDeptId, strFileName);
                sessions.CreateChatTransferSession(str7, strUserid, strUserName, strEXECid, strDeptId, strFileName);
                clsChatMessages messages = new clsChatMessages(this._Directory, strFileName);
                string strMessage = "$@~شما در حال اتصال به " + users.getExecutiveName(strEXECid)+" هستيد";
                messages.AddMessage("0", strUserid, msgTokens.Message, strMessage, true);
				strMessage = "$@~";// "Chat Powered By: <a target='_blank' href='http://www.arlivesupport.com' style='font-family:Verdana; font-size:11px; color:#0000CC'>A R Live Support</a>";
                messages.AddMessage("0", strUserid, msgTokens.Message, strMessage, true);
                messages.AddMessage("0", strUserid, msgTokens.Message, "$@~لطفا صبر کنيد..", true);
                this._Result = true;
            }
        }

        private string _Directory
        {
            get
            {
                return ADAL.A_CheckData.GetFilesRoot();
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

        private object _Params
        {
            get
            {
                return this.objParams;
            }
            set
            {
                this.objParams = value;
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

