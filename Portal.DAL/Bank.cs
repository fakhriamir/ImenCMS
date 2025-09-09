using System;
using System.Web.UI;
using System.Data.SqlClient;
using DAL;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web.UI.WebControls.WebParts;
using Facebook.Client;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;

namespace Tools
{
    public class Bank
    {
        public static string GetBankName(string BankCode)
        {
            string[] BankName = { "", "ملت", "اقتصاد نوین", "پارسیان", "پاسارگاد", "ملی" };
            return BankName[Tools.ConvertToInt32(BankCode)];
        }
        //public static string PgwSite;
        public static void VerifyPasargad(string Mon, string RezNo)
        {
            string TerminalId = "", UserName = "", UserPassword = "";// PayCallBackUrl = "";//, PayOrderId = "", PayDate = "", PayAmount = "", PayTime = "", PayAdditionalData = "", PayPayerId = "";

            SqlDataReader MyRead = ViewData.MyDR1("SELECT Action, MID, RedirectURL, UserName, Password,RedirectURLShop  FROM Bank  WHERE (EngName = '" + MyVar.BankType.Pasargad + "') AND (UnitID = " + MyClass.GetViewUnitID + ")", null);
            if (MyRead.Read())
            {
                TerminalId = MyCL.MGStr(MyRead, 1);
                UserName = MyCL.MGStr(MyRead, 3);
                UserPassword = MyCL.MGStr(MyRead, 4);
                /*if (BRT == MyVar.BankRedirectType.Shop)
					PayCallBackUrl = MyCL.MGStr(MyRead, 5);
				else if (BRT == MyVar.BankRedirectType.Telegram)
					PayCallBackUrl = "http://imencms.com/Telegram/Payment";
				else if (BRT == MyVar.BankRedirectType.OtherPay)
				{
					string RedirectURL = DAL.ExecuteData.CNTDataStr(" SELECT TOP (1) Redirect FROM Site  WHERE (UnitID = " + MyClass.GetViewUnitID + ") AND (Redirect <> '')");
					PayCallBackUrl = RedirectURL + "/Shop/OtherPayBack";
				}
				else
					PayCallBackUrl = MyCL.MGStr(MyRead, 2);
				PgwSite = MyCL.MGStr(MyRead, 0);*/
            }
            MyRead.Close(); MyRead.Dispose();

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(UserPassword);
            string merchantCode = UserName; // کد پذیرنده
            string terminalCode = TerminalId; // کد ترمینال
            string amount = Mon; //  مبلغ فاکتور

            string invoiceNumber = RezNo;// شماره فاکتور
            string timeStamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string invoiceDate = DateTime.Now.ToString("yyyy/MM/dd");
            string data = "#" + merchantCode + "#" + terminalCode + "#" + invoiceNumber + "#" + invoiceDate + "#" + amount + "#" + timeStamp + "#";

            byte[] signedData = rsa.SignData(Encoding.UTF8.GetBytes(data), new
            SHA1CryptoServiceProvider());

            string signedString = Convert.ToBase64String(signedData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://pep.shaparak.ir/VerifyPayment.aspx");
            string text = "InvoiceNumber=" + invoiceNumber + "&InvoiceDate=" +
                        invoiceDate + "&MerchantCode=" + merchantCode + "&TerminalCode=" +
                        terminalCode + "&Amount=" + amount + "&TimeStamp=" + timeStamp + "&Sign=" + signedString;
            byte[] textArray = Encoding.UTF8.GetBytes(text);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = textArray.Length;
            request.GetRequestStream().Write(textArray, 0, textArray.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string result = reader.ReadToEnd();
            SqlParameterCollection SP = new SqlCommand().Parameters;
            SP.AddWithValue("@factorid", RezNo);
            SP.AddWithValue("@BankRes", result);
            ExecuteData.ExecData("update factor set BankRes=@BankRes where factorid=@factorid", SP);
            //xmlResult.DocumentContent = result;
        }
        public static void Payment(Page MyPage, string BankSel, string RezNo, string Mon, ref string PgwSite, MyVar.BankRedirectType BRT = MyVar.BankRedirectType.SMSSystem)
        {
            if (Tools.GetSetting(499, "0") != "0")
            {
                SqlParameterCollection SP = new SqlCommand().Parameters;
                SP.AddWithValue("@UnitID", Tools.GetSetting(499, "0"));
                string RedirectURL = DAL.ExecuteData.CNTDataStr(" SELECT TOP (1) Redirect FROM Site  WHERE (UnitID = @UnitID) AND (Redirect <> '')", SP);
                MyPage.Response.Redirect(RedirectURL + "/Shop/OtherPay?ID=" + RezNo + "&BS=" + BankSel);
                return;
            }

            string TerminalId = "", UserName = "", UserPassword = "", PayCallBackUrl = "";//, PayOrderId = "", PayDate = "", PayAmount = "", PayTime = "", PayAdditionalData = "", PayPayerId = "";
                                                                                          //string result;
            SqlDataReader MyRead = ViewData.MyDR1("SELECT Action, MID, RedirectURL, UserName, Password,RedirectURLShop  FROM Bank  WHERE (EngName = '" + BankSel + "') AND (UnitID = " + MyClass.GetViewUnitID + ")", null);
            if (MyRead.Read())
            {
                TerminalId = MyCL.MGStr(MyRead, 1);
                UserName = MyCL.MGStr(MyRead, 3);
                UserPassword = MyCL.MGStr(MyRead, 4);
                if (BRT == MyVar.BankRedirectType.Shop)
                    PayCallBackUrl = MyCL.MGStr(MyRead, 5);
                else if (BRT == MyVar.BankRedirectType.Telegram)
                    PayCallBackUrl = "http://imencms.com/Telegram/Payment";
                else if (BRT == MyVar.BankRedirectType.OtherPay)
                {
                    string RedirectURL = DAL.ExecuteData.CNTDataStr(" SELECT TOP (1) Redirect FROM Site  WHERE (UnitID = " + MyClass.GetViewUnitID + ") AND (Redirect <> '')");
                    PayCallBackUrl = RedirectURL + "/Shop/OtherPayBack";
                }
                else
                    PayCallBackUrl = MyCL.MGStr(MyRead, 2);
                PgwSite = MyCL.MGStr(MyRead, 0);
            }
            MyRead.Close(); MyRead.Dispose();
            if (TerminalId == "")
            {
                MyClass.Alert(MyPage, "مشکل در تنظيمات بانک", "", true);
                return;
            }
            if (BankSel.ToLower() == "melat")
            {
                try
                {
                    DAL.ir.shaparak.bpm.PaymentGatewayImplService bpService = new DAL.ir.shaparak.bpm.PaymentGatewayImplService();
                    bpService.Url = "https://bpm.shaparak.ir/pgwchannel/services/pgw";
                    string result = bpService.bpPayRequest(Int64.Parse(TerminalId),
                    UserName,
                    UserPassword,
                    Int64.Parse(RezNo),
                    Int64.Parse(Mon),
                    DateTime.Now.ToString("yyyyMMdd"),
                    DateTime.Now.ToString("HHmmss"),
                    "",
                    PayCallBackUrl,
                    Int64.Parse("0"));

                    //PayOutputLabel.Text = result;
                    String[] resultArray = result.Split(',');
                    if (resultArray[0] == "0")
                    {
                        AddResaultPayment(resultArray[1], RezNo, (int)MyVar.BankType.Mellat);
                        MyPage.ClientScript.RegisterStartupScript(typeof(Page), "ClientScript", "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script> ", false);
                    }
                }
                catch (Exception exp)
                {
                    //PayOutputLabel.Text = "Error: " + exp.Message;
                    MyClass.Alert(MyPage, exp.Message, "", true);
                }
            }
            else if (BankSel.ToLower() == "nevin")
            {
                try
                {
                    //Tools.CreateDynamicForm(PgwSite, "MID=" + TerminalId + "&ResNum=150");
                    AddResaultPayment("", RezNo, (int)MyVar.BankType.Parsiyan);
                    MyPage.ClientScript.RegisterStartupScript(typeof(Page), "ClientScript", "<script language='javascript' type='text/javascript'> postNevinRefId('" + PayCallBackUrl + "','" + TerminalId + "');</script> ", false);
                }
                catch (Exception exp)
                {
                    //PayOutputLabel.Text = "Error: " + exp.Message;
                    MyClass.Alert(MyPage, exp.Message, "", true);
                }
            }
            else if (BankSel.ToLower() == "parsian")
            {
                try
                {
                    long authority = 0;
                    byte status = 0;
                    int amount = int.Parse(Mon);
                    DAL.ir.shaparak.pec.EShopService service = new DAL.ir.shaparak.pec.EShopService();
                    service.PinPaymentRequest(TerminalId, amount, Tools.ConvertToInt32(RezNo), PayCallBackUrl, ref authority, ref status);

                    // here eShops has to register the authority, for future needs, such as integrity 
                    // checks in settlement time.
                    if (status == 0)
                    {
                        AddResaultPayment(authority.ToString(), RezNo, (int)MyVar.BankType.Parsiyan);
                        //DAL.ExecuteData.ExecData("UPDATE Factor SET Authority='" + authority + "' WHERE (FactorID = " + RezNo + ")");
                        //MyPage.Response.Redirect(PgwSite + "?au=" + authority.ToString(), true);
                        MyPage.Response.Redirect("https://pec.shaparak.ir/pecpaymentgateway?au=" + authority.ToString(), true);
                    }
                    else
                    {
                        MyClass.Alert(MyPage, "مشکل در تنظيمات بانک کد خطا" + status.ToString(), "", true);
                    }
                    //string bb = "Status : " + ((PgwStatus)status).ToString();
                }
                catch (Exception exp)
                {
                    //PayOutputLabel.Text = "Error: " + exp.Message;
                    MyClass.Alert(MyPage, exp.Message, "", true);
                }
            }
            else if (BankSel.ToLower() == "pasargad")
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                string privateKeyText = UserPassword;//متغیری جهت تعریف کلید اختصاصی در سیستم
                                                     //------------------------------------------------------------------------
                                                     //در این قسمت اطلاعاتی که در صفحه 
                                                     //Default 
                                                     //وارد نموده اید جایگذاری می گردد
                string merchantCode = UserName; // کد پذیرنده
                string terminalCode = TerminalId; // کد ترمینال
                string amount = Mon; //  مبلغ فاکتور
                string redirectAddress = PayCallBackUrl;
                string invoiceNumber = RezNo;// شماره فاکتور
                string action = "1003";//Page.Request.QueryString["action"];
                                       //تاریخ فاکتور و زمان اجرای عملیات از سیستم گرفته می شود
                                       //شما می توانید تاریخ فاکتور را به صورت دستی وارد نمایید 
                string timeStamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                string invoiceDate = DateTime.Now.ToString("yyyy/MM/dd"); // تاریخ فاکتور
                                                                          //-------------------------------------------------------------------------


                // در این قسمت تولید می شود و برای بانک ارسال می گرددsign
                rsa.FromXmlString(privateKeyText);
                string data = "#" + merchantCode + "#" + terminalCode + "#" + invoiceNumber + "#" + invoiceDate + "#" + amount + "#" + redirectAddress + "#" + action + "#" + timeStamp + "#";
                byte[] signMain = rsa.SignData(Encoding.UTF8.GetBytes(data), new SHA1CryptoServiceProvider());
                string sign = Convert.ToBase64String(signMain);


                NameValueCollection dp = new NameValueCollection();
                dp.Add("merchantCode", merchantCode);
                dp.Add("terminalCode", terminalCode);
                dp.Add("amount", amount);
                dp.Add("redirectAddress", redirectAddress);
                dp.Add("invoiceNumber", invoiceNumber);
                dp.Add("invoiceDate", invoiceDate);
                dp.Add("action", action);
                dp.Add("sign", sign);
                dp.Add("timeStamp", timeStamp);
                Tools.PostData(MyPage, dp, "https://pep.shaparak.ir/gateway.aspx");

                //MyPage.ClientScript.RegisterStartupScript(typeof(Page), "ClientScript", "<script language='javascript' type='text/javascript'> PostPasargad('" + merchantCode + "','" + terminalCode + "','" + amount + "','" + invoiceNumber + "','" + action + "','" + sign + "','" + timeStamp + "','" + PayCallBackUrl + "');</script> ", false);

                /*
								string pForm = "";
								pForm = string.Concat("<form method=\"post\" action=\"https://epayment.bankpasargad.com/gateway.aspx\" name=\"form1\">",
								"merchantCode<input type=\"text\" name=\"merchantCode\" value=\"",
								 merchantCode,
								 "\" /><br />",
								 "terminalCode<input type=\"text\" name=\"terminalCode\" value=\"",
								 terminalCode,
								 "\" /><br />",
								 "amount<input type=\"text\" name=\"amount\" value=\"",
								 amount,
								 "\" /><br />",
								 "redirectAddress<input type=\"text\" name=\"redirectAddress\" value=\"",
								 redirectAddress,
								 "\" /><br />",
								 "invoiceNumber<input type=\"text\" name=\"invoiceNumber\" value=\"",
								 invoiceNumber,
								 "\" /><br />",
								 "invoiceDate<input type=\"text\" name=\"invoiceDate\" value=\"",
								 invoiceDate,
								 "\" /><br />",
								 "action<input type=\"text\" name=\"action\" value=\"",
								 action,
								 "\" /><br />",
								 "sign<input type=\"text\" name=\"sign\" value=\"",
								 sign,
								 "\" /><br />",
								 "timeStamp<input type=\"text\" name=\"timeStamp\" value=\"",
								 timeStamp,
								 "\" /><br />",
								"<input type=\"submit\" name=\"submit\" value=\"submit\"  id=\"submit\" value=\"ok\" /></form>");
				
								using (WebClient client = new WebClient())
								{merchantCode,terminalCode,amount,invoiceNumber,action,sign,timeStamp,

									NameValueCollection vals = new NameValueCollection();
									vals.Add("merchantCode", merchantCode);
									vals.Add("terminalCode", terminalCode);
									vals.Add("amount", amount);
									vals.Add("invoiceNumber", invoiceNumber);
									vals.Add("action", action);
									vals.Add("sign", sign);
									vals.Add("timeStamp", timeStamp);
									client.UploadValues(PgwSite, vals);
								}*/
            }
            else if (BankSel.ToLower() == "sadad")
            {
                try
                {
                    PaymentRequest request = new PaymentRequest();
                    request.TerminalId = TerminalId;
                    request.OrderId = RezNo;
                    request.MerchantId = UserName;
                    request.MerchantKey = UserPassword;
                    request.Amount = Tools.ConvertToInt64(Mon);
                    request.PurchasePage = "https://sadad.shaparak.ir/";
                    request.ReturnUrl = PayCallBackUrl;

                    var dataBytes = Encoding.UTF8.GetBytes(string.Format("{0};{1};{2}", request.TerminalId, request.OrderId, request.Amount));

                    var symmetric = SymmetricAlgorithm.Create("TripleDes");
                    symmetric.Mode = CipherMode.ECB;
                    symmetric.Padding = PaddingMode.PKCS7;

                    var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(request.MerchantKey), new byte[8]);

                    request.SignData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));


                    var ipgUri = string.Format("{0}/api/v0/Request/PaymentRequest", "https://sadad.shaparak.ir/");


                    HttpCookie merchantTerminalKeyCookie = new HttpCookie("Data", JsonConvert.SerializeObject(request));
                    MyPage.Response.Cookies.Add(merchantTerminalKeyCookie);

                    var data = new
                    {
                        request.TerminalId,
                        request.MerchantId,
                        request.Amount,
                        request.SignData,
                        request.ReturnUrl,
                        LocalDateTime = DateTime.Now,
                        request.OrderId,
                        //MultiplexingData = request.MultiplexingData
                    };


                    var res = CallApi<PayResultData>(ipgUri, data);
                    res.Wait();

                    if (res != null && res.Result != null)
                    {
                        if (res.Result.ResCode == "0")
                        {
                            SqlParameterCollection SP = new SqlCommand().Parameters;
                            SP.AddWithValue("@Token", res.Result.Token);
                            SP.AddWithValue("@FID", request.OrderId);
                            DAL.ExecuteData.ExecData("UPDATE Factor  SET Token=@Token WHERE (FactorID = @FID)", SP);
                            MyPage.Response.Redirect(string.Format("{0}/Purchase/Index?token={1}", request.PurchasePage, res.Result.Token));
                        }
                        else
                        {
                            DAL.Logging.ErrorLog("bank11Error", "sadadBank" + res.Result.ResCode, res.Result.Token + "-" + res.Result.Description);

                        }


                    }

                }
                catch (Exception ex)
                {
                    MyClass.Alert(MyPage, ex.Message, "", true);
                    DAL.Logging.ErrorLog("bankError", "sadadBank", ex.Message + "-----------" + ex.StackTrace);
                }
            }

            /*else if (BankSel.ToLower() == "saman")
			{

			}*/

        }
        public class PayResultData
        {
            public string ResCode { get; set; }
            public string Token { get; set; }
            public string Description { get; set; }
        }

        public class PaymentRequest
        {
            public PaymentRequest()
            {
                //MultiplexingData = new MultiplexingData();
            }

            [Display(Name = @"شماره ترمینال")]
            [Required(ErrorMessage = "شماره پایانه اجباری است ")]
            public string TerminalId { get; set; }
            [Display(Name = @"شماره پذیرنده")]
            [Required(ErrorMessage = "شماره پذیرنده اجباری است ")]
            public string MerchantId { get; set; }

            [Required(ErrorMessage = "مبلغ اجباری است ")]
            [Display(Name = @"مبلغ")]
            public long Amount { get; set; }
            public string OrderId { get; set; }
            public string AdditionalData { get; set; }
            public DateTime LocalDateTime { get; set; }
            public string ReturnUrl { get; set; }
            public string SignData { get; set; }
            [Display(Name = @"پرداخت تسهیم")]
            public bool EnableMultiplexing { get; set; }
            //public MultiplexingData MultiplexingData { get; set; }

            [Display(Name = @"کلید پذیرنده")]
            [Required(ErrorMessage = "کلیدپذیرنده اجباری است ")]
            public string MerchantKey { get; set; }

            [Display(Name = @"آدرس درگاه")]
            [Required(ErrorMessage = "آدرس درگاه اجباری است ")]
            public string PurchasePage { get; set; }
        }
        public static Task<T> CallApi<T>(string apiUrl, object value)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                //JsonContent content = JsonContent.Create(value);
                var stringPayload = JsonConvert.SerializeObject(value);

                // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                DAL.Logging.ErrorLog("bankError", "sadadBk", stringPayload);
                var w = client.PostAsync(apiUrl, httpContent);
                w.Wait();
                HttpResponseMessage response = w.Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<T>();
                    result.Wait();
                    return Task.FromResult(result.Result);
                }
                return Task.FromResult(default(T));
            }
        }

        public string PurchasePage { get; set; }
        //public VerifyResultData VerifyResultData { get; set; }
        public class VerifyResultData
        {
            public bool Succeed { get; set; }
            public string ResCode { get; set; }
            public string Description { get; set; }
            public string Amount { get; set; }
            public string RetrivalRefNo { get; set; }
            public string SystemTraceNo { get; set; }
            public string OrderId { get; set; }
        }
        public static void AddResaultPayment(string resu, string FaktorID, int BankTypeID)
        {
            DAL.ExecuteData.ExecData("UPDATE Factor SET Authority='" + resu + "',BankType=" + BankTypeID + " WHERE (FactorID = " + FaktorID + ")");
        }
        public static double CheckNevinPay(string RefId)
        {
            string TerminalId = "";
            SqlDataReader MyRead = ViewData.MyDR1("SELECT Action, MID, RedirectURL, UserName, Password  FROM Bank  WHERE (EngName = 'nevin') AND (UnitID = " + MyClass.GetViewUnitID + ")", null);
            if (MyRead.Read())
            {
                TerminalId = MyCL.MGStr(MyRead, 1);
            }
            MyRead.Close(); MyRead.Dispose();
            DAL.ir.shaparak.pna.PaymentWebServiceService bpService = new DAL.ir.shaparak.pna.PaymentWebServiceService();
            DAL.ir.shaparak.pna.verifyResponseResult[] MyRR;
            //DAL.ir.shaparak.pna.wsContextEntry MyWSCE = new DAL.ir.shaparak.pna.wsContextEntry();
            //MyWSCE.key = "SESSION_ID";
            //MyWSCE.value = TerminalId;

            DAL.ir.shaparak.pna.wsContext MyWSC = new DAL.ir.shaparak.pna.wsContext();
            MyWSC.data.SetValue(TerminalId, 0);

            MyRR = bpService.verifyTransaction(MyWSC, new string[] { RefId });
            return 00000000000000;
        }
        public static string CheckMelatPay(string RefId, string ResCode, string SaleOrderId, string SaleReferenceId)
        {
            string TerminalId = "", UserName = "", UserPassword = "", PayCallBackUrl = "";
            SqlDataReader MyRead = ViewData.MyDR1("SELECT Action, MID, RedirectURL, UserName, Password  FROM Bank  WHERE (EngName = 'melat') AND (UnitID = " + MyClass.GetViewUnitID + ")", null);
            if (MyRead.Read())
            {
                TerminalId = MyCL.MGStr(MyRead, 1);
                UserName = MyCL.MGStr(MyRead, 3);
                UserPassword = MyCL.MGStr(MyRead, 4);
                PayCallBackUrl = MyCL.MGStr(MyRead, 2);
            }
            MyRead.Close(); MyRead.Dispose();
            DAL.ir.shaparak.bpm.PaymentGatewayImplService bpService = new DAL.ir.shaparak.bpm.PaymentGatewayImplService();
            bpService.Url = "https://bpm.shaparak.ir/pgwchannel/services/pgw";

            string result = bpService.bpVerifyRequest(Int64.Parse(TerminalId),
                UserName,
                UserPassword,
                Int64.Parse(SaleOrderId),
                Int64.Parse(SaleOrderId),
                Int64.Parse(SaleReferenceId));
            return result;
        }
        public static string OKMelatPay(string RefId, string ResCode, string SaleOrderId, string SaleReferenceId)
        {
            string TerminalId = "", UserName = "", UserPassword = "", PayCallBackUrl = "";
            SqlDataReader MyRead = ViewData.MyDR1("SELECT Action, MID, RedirectURL, UserName, Password  FROM Bank  WHERE (EngName = 'melat') AND (UnitID = " + MyClass.GetViewUnitID + ")", null);
            if (MyRead.Read())
            {
                TerminalId = MyCL.MGStr(MyRead, 1);
                UserName = MyCL.MGStr(MyRead, 3);
                UserPassword = MyCL.MGStr(MyRead, 4);
                PayCallBackUrl = MyCL.MGStr(MyRead, 2);
            }
            MyRead.Close(); MyRead.Dispose();
            string result;
            DAL.ir.shaparak.bpm.PaymentGatewayImplService bpService = new DAL.ir.shaparak.bpm.PaymentGatewayImplService();
            bpService.Url = "https://bpm.shaparak.ir/pgwchannel/services/pgw";

            result = bpService.bpSettleRequest(Int64.Parse(TerminalId),
                UserName,
                UserPassword,
                Int64.Parse(SaleOrderId),
                Int64.Parse(SaleOrderId),
                Int64.Parse(SaleReferenceId));

            return result;
        }

        public static void OKParsiyanPay(long authority, ref byte status)
        {
            string TerminalId = DAL.ExecuteData.CNTDataStr("SELECT top 1 MID FROM Bank  WHERE (EngName = 'parsian') AND (UnitID = " + MyClass.GetViewUnitID + ")", null);
            DAL.ir.shaparak.pec.EShopService service = new DAL.ir.shaparak.pec.EShopService();
            service.PinPaymentEnquiry(TerminalId, authority, ref status);
        }

        public static void GetListBank(DropDownList myDL)
        {
            if (Tools.GetSetting(499, "0") != "0")
            {
                SqlParameterCollection SP = new SqlCommand().Parameters;
                SP.AddWithValue("@UnitID", Tools.GetSetting(499, "0"));
                myDL.DataSource = DAL.ViewData.MyDT("SELECT BankID, Name FROM Bank WHERE (UnitID = @UnitID) order by sort", SP);
            }
            else
                myDL.DataSource = DAL.ViewData.MyDT("SELECT BankID, Name FROM Bank WHERE (UnitID = " + MyClass.GetViewUnitID + ") order by sort");
            myDL.DataBind();
        }

        public static int MelliCallBack(Page MyPage)
        {
            try
            {
                var cookie = MyPage.Request.Cookies["Data"].Value;
                var model = JsonConvert.DeserializeObject<PaymentRequest>(cookie);
                DAL.Logging.ErrorLog("bank11Error", "sadadBank", "coociesret"+cookie);
                PurchaseResult result = JsonConvert.DeserializeObject<PurchaseResult>(cookie);

                SqlParameterCollection SP = new SqlCommand().Parameters;
                SP.AddWithValue("@FID", model.OrderId);
                result.Token = DAL.ExecuteData.CNTDataStr("SELECT Token  FROM Factor  WHERE (FactorID = @FID)",SP);


                var dataBytes = Encoding.UTF8.GetBytes(result.Token);
                var symmetric = SymmetricAlgorithm.Create("TripleDes");
                symmetric.Mode = CipherMode.ECB;
                symmetric.Padding = PaddingMode.PKCS7;

                var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(model.MerchantKey), new byte[8]);

                var signedData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

                var data = new
                {
                    token = result.Token,
                    SignData = signedData
                };

                var ipgUri = string.Format("{0}/api/v0/Advice/Verify", model.PurchasePage);

                var res = CallApi<VerifyResultData>(ipgUri, data);
                if (res != null && res.Result != null)
                {
                    DAL.Logging.ErrorLog("bank11Error", "sadadBank", res.Result + "--dis:" + res.Result.Description + "--amount:" + res.Result.Amount + "--success:" + res.Result.Succeed + "--strano:" + res.Result.SystemTraceNo + "--orid:" + res.Result.OrderId + "--rrefno:" + res.Result.RetrivalRefNo );
                    if (res.Result.ResCode == "0")
                    {
                        //result.VerifyResultData = res.Result;
                        //res.Result.Succeed = true;
                        //ViewBag.Success = res.Result.Description;
                        //return View("Verify", result);
                       // res.Result.SystemTraceNo
                        //    res.Result.Amount

                        //SqlParameterCollection SP = new SqlCommand().Parameters;
                        SP = new SqlCommand().Parameters;
                        SP.AddWithValue("@FID", model.OrderId);
                        string facMoney = ExecuteData.CNTDataStr("SELECT Money  FROM Factor WHERE (FactorID = @FID)", SP);
                        if (facMoney == "0" || facMoney == "")
                        {
                            MyClass.Alert(MyPage, "شماره فاکتور مورد تاييد نمي باشد در صورتی که وجه از حساب شما کسر شده است بعد از 15 دقیقه به حساب شما برگشت داده می شود", "", true);
                            return -1;
                        }
                        if(facMoney != res.Result.Amount)
                        {
                            MyClass.Alert(MyPage, "مبلغ پرداخت شده با مبلغ فاکتور مطابقت ندارد با پشتیبانی تماس بگیرید", "", true);
                            return -1;
                        }
                        //چک کردن کد خرید
                        SP.AddWithValue("@RefId", res.Result.RetrivalRefNo);
                        //RefId = res.Result.SystemTraceNo;
                        int CNT = DAL.ExecuteData.CNTData("select count(*) from Factor where Bankid=@RefId", SP);
                        if (CNT > 0)
                        {
                            MyClass.Alert(MyPage, "رسید دیجیتال تکراری می باشد", "", true);
                            return -1;
                        }

                        SP.AddWithValue("@factorid", model.OrderId);
                        SP.AddWithValue("@BankRes", res.Result.RetrivalRefNo);
                        ExecuteData.ExecData("update factor set Bankid=@BankRes where factorid=@factorid", SP);

                        //ActiveShopFactor(FactorIDcheck, Factorid.ToString(), Tools.MyVar.BankType.Pasargad);
                        return Tools.ConvertToInt32(model.OrderId);
                    }
                    else
                    {
                        MyClass.Alert(MyPage, res.Result.Description, "", true);
                        MyClass.Alert(MyPage, "شماره فاکتور مورد تاييد نمي باشد در صورتی که وجه از حساب شما کسر شده است بعد از مدتی به حساب شما برگشت داده می شود", "", true);
                        return -1;
                    }
                    //ViewBag.Message = res.Result.Description;
                    //return View("Verify");
                }
                return -1;
            }
            catch (Exception ex)
            {
                DAL.Logging.ErrorLog("bank11Error", "sadadBank", ex.ToString());
                return -1;
                //ViewBag.Message = ex.ToString();
            }
        }
        public class PurchaseResult
        {
            public string OrderId { get; set; }
            public string Token { get; set; }
            public string ResCode { get; set; }
            public VerifyResultData VerifyResultData { get; set; }
        }
    }
}