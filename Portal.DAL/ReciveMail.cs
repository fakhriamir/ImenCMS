using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.WebSockets;
using System.Net;
using ActiveUp.Net;

using ActiveUp.Net.Mail;

namespace Tools
{
	public class ReciveMail
	{

		public static MessageCollection ECEReciveMailPop3()
		{
			Pop3Client pop = new Pop3Client();
			MessageCollection mc = new MessageCollection();
			try
			{
				pop.Connect(Tools.GetSetting(401), Tools.ConvertToInt32(Tools.GetSetting(405, "143")), Tools.GetSetting(400), Tools.GetSetting(402));

				for (int n = 1; n < pop.MessageCount + 1; n++)
				{
					Message newMessage = pop.RetrieveMessageObject(n);
					mc.Add(newMessage);
				}
			}
			/*catch// (Pop3Exception pexp)
			{
				//this.AddLogEntry(string.Format("Pop3 Error: {0}", pexp.Message));
			}*/
			catch// (Exception ex)
			{
				//this.AddLogEntry(string.Format("Failed: {0}", ex.Message));
			}

			finally
			{
				if (pop.IsConnected)
				{
					pop.Disconnect();
				}
			}
			return mc;
		}
		public static MessageCollection ECEReciveMailImap()
		{
			Imap4Client imap = new Imap4Client();
			MessageCollection mc = new MessageCollection();
			try
			{

				imap.Connect(Tools.GetSetting(401), Tools.ConvertToInt32(Tools.GetSetting(405, "143")), Tools.GetSetting(400), Tools.GetSetting(402));
			
				for (int n = 0; n < imap.AllMailboxes.Count; n++)
				{
					Mailbox inbox = imap.SelectMailbox(imap.Mailboxes[n].Name);

					for (int b = 1; b < inbox.MessageCount + 1; b++)
					{
						ActiveUp.Net.Mail.Message newMessage = inbox.Fetch.MessageObject(b);
						mc.Add(newMessage);
					}
				}
			}

			

			catch
			{
				//this.AddLogEntry(string.Format("Failed: {0}", ex.Message));
			}

			finally
			{
				if (imap.IsConnected)
				{
					imap.Disconnect();
				}
			}

			return 	 mc;

			//Imap4Client MyImap = new Imap4Client();
			//Mailbox mailbox = MyImap.SelectMailbox("INBOX");
			//Fetch fetcha = mailbox.Fetch;
			//ActiveUp.Net.Mail.Message message;
			//message = fetcha.MessageObject(message_number);
			//string subject = message.Subject;
			//MimeBody body = message.BodyHtml;
			//return "";
		}
	}
}