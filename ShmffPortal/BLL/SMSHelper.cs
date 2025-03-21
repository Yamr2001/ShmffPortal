using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Xml;

namespace ShmffPortal.Helpers
{
    public class SMSHelper
    {
        private HttpWebRequest CreateWebRequest(string endPoint)
        {
            var request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = "Get";
            request.ContentLength = 0;
            request.ContentType = "text/xml";
            return request;
        }




        //public string sendMessageWithDLR(string smstext, string phoneNumber)
        //{

        //    string messag = smstext;
        //    string phone = Regex.Replace(phoneNumber, @"(\s+|-|&|'|\(|\)|<|>|#)", "");
        //    string uri = string.Format("https://smsvas.vlserv.com/KannelSending/service.asmx/SendSMSWithDLR?username={0}&password={1}&SMSText={2}&SMSLang={3}&SMSSender={4}&SMSReceiver={5}", "MortgageFinance", "eR9q9LlB59", messag, "a", "MFF Egypt", phone);

        //    HttpWebRequest request = CreateWebRequest(uri);
        //    using (var response = (HttpWebResponse)request.GetResponse())
        //    {
        //        var responseValue = string.Empty; if (response.StatusCode != HttpStatusCode.OK)
        //        {
        //            string message = string.Format("POST failed. Received HTTP {0}", response.StatusCode);
        //            throw new ApplicationException(message);
        //        }
        //        //grab the response
        //        using (var responseStream = response.GetResponseStream())
        //        {
        //            using (var reader = new StreamReader(responseStream))
        //            {
        //                responseValue = reader.ReadToEnd();
        //            }
        //            try
        //            {
        //                SaveSMStype("", smstext, responseValue, phone);
        //            }
        //            catch
        //            {

        //            }
        //            return responseValue;
        //        }

        //    }

        //}


        public string sendMessage(string smstext, string phoneNumber)
        {
            try
            {
                string ProdState = WebConfigurationManager.AppSettings["ProdState"];
                if (ProdState == "true")
                {

                    string messag = smstext;
                    string phone = Regex.Replace(phoneNumber, @"(\s+|-|&|'|\(|\)|<|>|#)", "");
                    string uri = string.Format("https://smsvas.vlserv.com/KannelSending/service.asmx/SendSMS?username={0}&password={1}&SMSText={2}&SMSLang={3}&SMSSender={4}&SMSReceiver={5}", "MortgageFinance", "eR9q9LlB59", messag, "a", "MFF Egypt", phone);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    HttpWebRequest request = CreateWebRequest(uri);
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        var responseValue = string.Empty; if (response.StatusCode != HttpStatusCode.OK)
                        {
                            string message = string.Format("POST failed. Received HTTP {0}", response.StatusCode);
                            throw new ApplicationException(message);
                        }
                        //grab the response
                        using (var responseStream = response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                            try
                            {
                                SaveSMStype("", smstext, responseValue, phone);
                            }
                            catch
                            {

                            }
                            return responseValue;
                        }

                    }
                }
                else
                {
                    return "";
                }
            }
            catch 
            {
                return "";
            }

        }
        private void SaveSMStype(string ssn, string message, string smsStat, string mobile)
        {
            // var xdoc = XDocument.Parse(smsStat);
            //
            var doc = new XmlDocument();
            //var x=xdoc.Descendants("int").Single();
            doc.LoadXml(smsStat);
            string CancelUrl = doc.GetElementsByTagName("int")[0].InnerText;
            int ststs = Convert.ToInt32(CancelUrl);
            //xdoc.FirstChild;
            //FAQQuestion faq = db.FAQQuestion.Find(id);
            //faq.SentMessageSuccess = ststs;
            //db.Entry(faq).State = EntityState.Modified;
            //db.SaveChanges();
            string ConnString = ConfigurationManager.ConnectionStrings["DefaultConnectionSMS"].ConnectionString;
            // /string query = "insert into SentMessagesSSN(SSN,FAQQuestion_ID,Message,SMSstatus) values('" + ssn + "','" + id + "','" + message + "','" + smsStat + "')";
            // string query = "update ";
            //  string query = "update FAQQuestions set SentMessageSuccess=" + ststs + " where id=" + id;
            string query = string.Format("insert into Comp_SMS(Mobile,Message,Status,Response,SSN,SendDate) values('{0}',N'{1}','{2}','{3}','{4}','{5}')", mobile, message, ststs, smsStat, ssn, DateTime.Now.ToString());
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(); ;
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query; //"insert into DOCUMENT(NUM_D_ORDRE_DOC,ID_PERSONE,TYPE_DOC,PRIX_DOC,DATE_DE_MISE_EN_CHARGE,DATE_CREATION)values('" + TextBox1.Text + "','1','" + TextBox2.Text + "','" + TextBox4.Text + "','" + TextBox3.Text + "','" + @DateTime.Now + "');";
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        private string getMessage(string smstext)
        {
            string message = "";
            //if (smstext == "2")
            //{
            //    message = "تم رفض تظلمكم بالإعلان العاشر، لعمل تظلم جديد يمكنكم الدخول على الرابط https://complaints.shmff.gov.eg حتى 30/4/2019 وفي حالة عدم الرغبة يُمكنكم سحب مُقدم الحجز من أى مكتب بريد مُميكن";
            //}
            //else
            //{
            //    message = "تم قبول تظلمكم بالإعلان العاشر، يمكنكم التوجه لسداد الدفعة الأولى للأقساط بمكاتب البريد المُميكن من 1 إبريل 2019 حتـى 30 يونيو 2019، وفي حالة عدم السداد خلال الفتـرة المحددة يتم سداد غرامة نسبة 1.5 % من قيمة الدفعة عن كل شهر تأخير أو جزء من الشهر";
            //}
            return message;
        }
    }
}