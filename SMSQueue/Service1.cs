using System;
using System.Data.SqlClient;
using System.ServiceProcess;
using SMSQueue.com.sahandsms;
using System.Collections;
using System.Timers;
namespace SMSQueue
{
	public partial class SMSQueueS : ServiceBase
	{
		Timer timer1 = new Timer();
		int SendCNTEventLog = 0;
		public SMSQueueS()
		{
			InitializeComponent();
		}
		string ConnStr = @"Server=.\;Integrated Security=True;database=portal;Connect Timeout=25;";
		bool ISWorking = false;
		private void timer1_Elapsed(object sender, EventArgs e)
		{
			SendCNTEventLog = 0;
			try
			{
				SendSMS();
			}
			catch (Exception ee)
			{
				EventLog.WriteEntry("error1:" + ee.Message + " -\n" + ee.StackTrace + " -\n date time:" + DateTime.Now.ToString(), System.Diagnostics.EventLogEntryType.Error);
			}
		}
		protected override void OnStart(string[] args)
		{
			EventLog.WriteEntry("Start", System.Diagnostics.EventLogEntryType.Warning);
			timer1.Elapsed += new ElapsedEventHandler(timer1_Elapsed);
			timer1.Interval = 600000;
			timer1.Enabled = true;
			timer1.Start();
			timer1_Elapsed(null, null);
			//EventLog.WriteEntry("Start Time", System.Diagnostics.EventLogEntryType.Warning);
		}
		protected override void OnStop()
		{
			timer1.Enabled = false;
			timer1.Stop();
		}
		private void SendSMS()
		{
			SqlConnection MyConn = new SqlConnection(ConnStr);
			SqlConnection MyConn1 = new SqlConnection(ConnStr);
			MyConn.Open();
			MyConn1.Open();
			//EventLog.WriteEntry("SQLOPEN1", System.Diagnostics.EventLogEntryType.Warning);

			SqlCommand MyComm = new SqlCommand("SELECT count(*)  FROM SMS_SMSLog  WHERE (Result = - 1) AND (Date < GETDATE())", MyConn);
			//int CNT = Convert.ToInt32(MyComm.ExecuteScalar());
			//EventLog.WriteEntry("select count"+CNT);
			SqlCommand MyCom1 = new SqlCommand("SELECT FromLineNo, SMS_UserID, COUNT(*) AS Expr1  FROM SMS_SMSLog  WHERE  (Result = - 1) AND (Date < GETDATE())  GROUP BY FromLineNo, SMS_UserID order by newid()", MyConn1);
			SqlDataReader MyRead1 = MyCom1.ExecuteReader();
			while (MyRead1.Read())
			{
				int CNT = MGInt(MyRead1, 2);
				int SMSUSERID = MGInt(MyRead1, 1);
				for (float i = 0; i <= (float)CNT / 80; i++)
				{
					//EventLog.WriteEntry("loop start");			
					ArrayList MyText = new ArrayList(); ArrayList MobNo = new ArrayList(); ArrayList LineNo = new ArrayList(); ArrayList SMS_LogID = new ArrayList();
					MyComm = new SqlCommand("SELECT top 80 SMS_SMSLogID, Text, MobNo,FromLineNo  FROM SMS_SMSLog  WHERE (Result = -1)  AND (Date < GETDATE()) and sms_userid=" + SMSUSERID, MyConn);
					SqlDataReader MyRead = MyComm.ExecuteReader();
					while (MyRead.Read())
					{
						//EventLog.WriteEntry("while start");
						SMS_LogID.Add(MGInt(MyRead, 0));
						MyText.Add(MGStr(MyRead, 1));
						MobNo.Add(MGStr(MyRead, 2));
						if (MGStr(MyRead, 3) == "")
							LineNo.Add(GetCurSMSNumber);
						else
						{
							if (MGStr(MyRead, 3).TrimStart('+').TrimStart('9').TrimStart('8').Substring(0, 1) == "3")
								LineNo.Add(MGStr(MyRead, 3).TrimStart('+'));
							else
								LineNo.Add(MGStr(MyRead, 3));
						}
					}
					MyRead.Close();
					try
					{
						SendUserSMS(ConvertArr(MyText), ConvertArr(MobNo), ConvertArr(LineNo), ConvertIntArr(SMS_LogID));
					}
					catch (Exception yy)
					{
						EventLog.WriteEntry("error send " + yy.Message + "ee" + yy.StackTrace + "00000000" + SMS_LogID.Count + "-" + MyText.Count + "-" + MobNo.Count + "-" + LineNo.Count);

					}
				}

			}
			MyConn.Close();
			MyConn1.Close();
			//send all
			//	SendTahmandeh();		
		}

		private string GetCurSMSNumber
		{
			get
			{
				SqlConnection MyConn = new SqlConnection(ConnStr);
				SqlCommand MyComm = new SqlCommand("", MyConn);
				MyConn.Open();
				MyComm.CommandText = "SELECT top 1 [LineNo] FROM SMS_LineNo  WHERE (SMS_UserID = - 1) order by sort";
				string outt = MyComm.ExecuteScalar().ToString();
				MyConn.Close();
				return outt;
			}
		}
		private void SendNewUserSMS(string[] MyText, string[] MobNo, string[] LineNo, int[] SMS_LogID)
		{
			string[] GUID;
			try
			{
				GUID = NewSahandSamanehSend(MyText, MobNo, LineNo);
			}
			catch
			{
				return;
			}
			SqlConnection MyConn = new SqlConnection(ConnStr);
			SqlCommand MyComm = new SqlCommand("", MyConn);
			MyConn.Open();
			for (int i = 0; i < SMS_LogID.Length; i++)
			{
				if (null != MobNo[i])
				{
					if (!string.IsNullOrEmpty(MobNo[i].Trim()))
					{
						object res = GUID[i];
						if (res != null && res.ToString() != "")
						{
							//SqlParameterCollection SP = new SqlCommand().Parameters;
							MyComm.CommandText = "UPDATE SMS_SMSLog SET Result=0 ,ResultGUID=@Result  WHERE (SMS_SMSLogID = @SMS_LogID)";
							MyComm.Parameters.AddWithValue("@Result", res);
							MyComm.Parameters.AddWithValue("@SMS_LogID", SMS_LogID[i]);
							MyComm.ExecuteNonQuery();
							MyComm.Parameters.Clear();
							SendCNTEventLog++;
						}
					}
				}
			}
			MyConn.Close();
		}
		private long[] CandooSend(string[] MyText, string[] MobNo, string[] LineNo, int FlashSend = 0)
		{
			string[] flash = SetArrayLen(new string[] { FlashSend.ToString() }, MobNo.Length);
			LineNo = SetArrayLen(LineNo, MobNo.Length);
			com.candoosms.panel.SMSAPI myS = new com.candoosms.panel.SMSAPI();
			com.candoosms.panel.MultipleSendResult[] myres = myS.SendMultiple("imencms", "Ali110Ali", LineNo, MyText, MobNo, flash, SetArrayLen(new long[] { 0 }, MobNo.Length));
			return CandoResultConvert(myres);
		}

		private void SendUserSMS(string[] MyText, string[] MobNo, string[] LineNo, int[] SMS_LogID)
		{
			long[] Result = null;
			try
			{
				if (LineNo[0].TrimStart('+').TrimStart('9').TrimStart('8').Substring(0, 1) == "1")
				{
					SendNewUserSMS(MyText, MobNo, LineNo, SMS_LogID);
					return;
				}
				else
					Result = CandooSend(MyText, MobNo, LineNo);
			}
			catch (Exception yy)
			{
				EventLog.WriteEntry("error send " + yy.Message + "ee" + yy.StackTrace + "00000000 + ");
				return;
			}
			SqlConnection MyConn = new SqlConnection(ConnStr);
			SqlCommand MyComm = new SqlCommand("", MyConn);
			MyConn.Open();
			for (int i = 0; i < SMS_LogID.Length; i++)
			{
				if (null != MobNo[i])
				{
					if (!string.IsNullOrEmpty(MobNo[i].Trim()))
					{
						object res = Result[i];
						if (res != null && res.ToString() != "")
						{
							//SqlParameterCollection SP = new SqlCommand().Parameters;
							MyComm.CommandText = "UPDATE SMS_SMSLog SET Result=@Result  WHERE (SMS_SMSLogID = @SMS_LogID)";
							MyComm.Parameters.AddWithValue("@Result", res);
							MyComm.Parameters.AddWithValue("@SMS_LogID", SMS_LogID[i]);
							MyComm.ExecuteNonQuery();
							MyComm.Parameters.Clear();
							SendCNTEventLog++;
						}
					}
				}
			}
			MyConn.Close();
		}
		public static string[] NewSahandSamanehSend(string[] MyText, string[] MobNo, string[] LineNo)
		{
			NewSahandwebservice.NewSmsWebservice MyS = new NewSahandwebservice.NewSmsWebservice();

			MyS.Url = "http://webservice.sahandsms.com/newsmswebservice.asmx";
			string[] MyResult = MyS.ArraySendQeue(MyText, MobNo, LineNo, "imencms", "45645645");
			return MyResult;
		}
		private long[] SahandSamaneSend(string[] SMSText, string[] SMSMob, string[] LineNo)
		{
			String Domin = "Magfa";
			// طول آرایه ها بیشتر از 79 نباشد
			long[] MyResult;
			String[] messages = SMSText;
			String[] mobiles = SMSMob;
			String[] origs = LineNo;
			int[] encodings = new int[1];
			encodings[0] = 2;
			String[] UDH = new string[1];
			UDH[0] = "";
			int[] mclass = new int[1];
			mclass[0] = 1;
			int[] priorities = new int[1];
			priorities[0] = -1;
			int[] i = new int[1];
			i[0] = 1;
			long[] checkingIds = new long[1];
			checkingIds[0] = 200 + i[0];
			//int arrmessagestatus;
			int[] typesend = new int[1]; typesend[0] = -1;
			//آرایه کد پیامک های ارسالی برای پیگیری وضعیت پیامک
			Service MyWS = new com.sahandsms.Service();
			MyResult = MyWS.Send(Domin, messages, mobiles, origs, encodings, UDH, mclass, priorities, checkingIds, "Im3ncm$");
			return MyResult;
		}
		private int MGInt(SqlDataReader MyRead, int ID)
		{
			if (MyRead.IsDBNull(ID))
				return 0;
			else
				return Convert.ToInt32(MyRead.GetValue(ID));
		}
		private string MGStr(SqlDataReader Myred, int ID)
		{
			if (!Myred.IsDBNull(ID))
				return Myred.GetString(ID).Replace("ي", "ي").Trim();
			else
				return "";
		}
		public long MGLong(SqlDataReader MyRead, int ID)
		{
			if (MyRead.IsDBNull(ID))
				return 0;
			else
				return Convert.ToInt64(MyRead.GetValue(ID));
		}
		private string[] ConvertArr(ArrayList InArray)
		{
			string[] MyMonNo = new string[InArray.Count];
			for (int i = 0; i < InArray.Count; i++)
				MyMonNo[i] = InArray[i].ToString();
			return MyMonNo;
		}
		private int[] ConvertIntArr(ArrayList InArray)
		{
			int[] MyMonNo = new int[InArray.Count];
			for (int i = 0; i < InArray.Count; i++)
				MyMonNo[i] = Convert.ToInt32(InArray[i].ToString());
			return MyMonNo;
		}
		string MGGuID(SqlDataReader Myred, int ID)
		{
			if (!Myred.IsDBNull(ID))
				return Myred.GetSqlValue(ID).ToString();
			else
				return "";
		}
		private static string[] SetArrayLen(string[] intext, int len)
		{
			if (intext.Length == len)
				return intext;

			string[] retVal = new string[len];
			for (int i = 0; i < len; i++)
			{
				retVal[i] = intext[0].TrimStart('+');
			}
			return retVal;
		}
		private static long[] SetArrayLen(long[] intext, int len)
		{
			if (intext.Length == len)
				return intext;

			long[] retVal = new long[len];
			for (int i = 0; i < len; i++)
			{
				retVal[i] = intext[0];
			}
			return retVal;
		}
		private long[] CandoResultConvert(com.candoosms.panel.MultipleSendResult[] myres)
		{
			long[] retVal = new long[myres.Length];
			for (int i = 0; i < myres.Length; i++)
			{
				retVal[i] = ConvertToInt64(myres[i].ID, -1);
			}
			return retVal;
		}
		public static long ConvertToInt64(object InNumber)
		{
			long OutNumber = -1;
			long.TryParse(InNumber.ToString(), out OutNumber);
			return OutNumber;
		}
		public static long ConvertToInt64(object InNumber, long DefVal)
		{
			if (ConvertToInt64(InNumber) <= 0)
				return DefVal;
			return ConvertToInt64(InNumber);

		}
	}
}
