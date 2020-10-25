using API.Contex;
using API.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace API.Core
{
    public class SMS
    {
        private Configuration_SMS objSMS { get; set;}
        public SMS()
        {
            try
            {
                using (var context = new DB003())
                {
                    var config_sms = context.Configuration_SMSs
                                .Where(w => w.IsActive == true)
                                .FirstOrDefault();

                    if (config_sms != null)
                    {
                        objSMS = new Configuration_SMS();
                        objSMS.URL = config_sms.URL;
                        objSMS.APIKey = config_sms.APIKey;
                        objSMS.Sender = config_sms.Sender;
                    }
                }
            }
            catch (Exception ex)
            {
                Exception_C.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);

            }
        }

        public string SendSMS(string message,string mobileNumber)
        {
             message = HttpUtility.UrlEncode(message);
            using (var wb = new WebClient())
            {
                byte[] response = wb.UploadValues(objSMS.URL, new NameValueCollection()
                {
                {"apikey" , objSMS.APIKey},
                {"numbers" , "91" + mobileNumber},
                {"message" , message},
                {"sender" , objSMS.Sender}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                return result;
            }
        }
    }
}
