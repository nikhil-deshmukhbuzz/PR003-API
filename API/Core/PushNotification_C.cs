using API.Contex;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Core
{
    public class PushNotification_C
    {
        DB003 context = null;
        private string webAddr = "https://fcm.googleapis.com/fcm/send";


        public void AddPushNotificationPG(long pgId,string title,string body)
        {
            try
            {
                context = new DB003();
                var user = context.Users
                    .Where(w => w.PGID == pgId)
                    .FirstOrDefault();

                PushNotification pushNotification = new PushNotification()
                {
                    PGID = user.PGID,
                    Name = user.Name,
                    MobileNo = user.MobileNo,
                    Token = user.PushNotificationToken,
                    DeviceID = user.DeviceID,
                    Titile = title,
                    Body = body,
                    Push = false,
                    Visible = true,
                    ExpiryDate = TimeZone.GetCurrentDateTime().AddDays(1),
                    CreatedOn = TimeZone.GetCurrentDateTime(),
                    ModifiedOn = TimeZone.GetCurrentDateTime()
                };

                long output;
                using (var context = new DB003())
                {
                    context.PushNotifications.Add(pushNotification);
                    output = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Exception_C.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                context = null;
            }
        }

        public void AddPushNotificationTenant(long tenantId, string title, string body)
        {
            try
            {
                context = new DB003();
                var tenant = context.Tenants
                    .Where(w => w.TenantID == tenantId)
                    .FirstOrDefault();

                var user = context.Users
                    .Where(w => w.MobileNo == tenant.MobileNo)
                    .FirstOrDefault();

                PushNotification pushNotification = new PushNotification()
                {
                    TenantID = tenant.TenantID,
                    Name = user.Name,
                    MobileNo = user.MobileNo,
                    Token = user.PushNotificationToken,
                    DeviceID = user.DeviceID,
                    Titile = title,
                    Body = body,
                    Push = false,
                    Visible = true,
                    ExpiryDate = TimeZone.GetCurrentDateTime().AddDays(1),
                    CreatedOn = TimeZone.GetCurrentDateTime(),
                    ModifiedOn = TimeZone.GetCurrentDateTime()
                };

                long output;
                using (var context = new DB003())
                {
                    context.PushNotifications.Add(pushNotification);
                    output = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Exception_C.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            finally
            {
                context = null;
            }
        }

        public void Send()
        {
            try
            {
                context = new DB003();
                var config = context.Configurations
                    .Where(w => w.Name == "PR003")
                    .FirstOrDefault();

                var pushNotifications = context.PushNotifications
                    .Where(w => w.Push == false && w.ExpiryDate >= TimeZone.GetCurrentDateTime())
                    .ToList();

                foreach (var pushNotification in pushNotifications)
                {
                    string result = SendNotificationAsync(pushNotification, config.PushNotify_Server_Key, config.PushNotify_Sender_Key);

                    bool isPush = true;
                    if (result == "-1")
                        isPush = false;

                    int output;
                    using (var context = new DB003())
                    {
                        var input = context.PushNotifications
                        .Where(w => w.PushNotificationID == pushNotification.PushNotificationID)
                        .FirstOrDefault();

                        input.Push = isPush;
                        input.ModifiedOn = TimeZone.GetCurrentDateTime();
                        output = context.SaveChanges();
                    }
                }
            }

            catch (Exception ex)
            {

                string str = ex.Message;

            }
            finally
            {
                context = null;
            }
        }

        public void Resend(PushNotification pushNotification)
        {
            try
            {
                context = new DB003();
                var config = context.Configurations
                    .Where(w => w.Name == "PR003")
                    .FirstOrDefault();

                var listPushNotification = context.PushNotifications
                    .Where(w => w.ExpiryDate.Value.Date >= DateTime.Now.Date)
                    .ToList();

                foreach(var item in listPushNotification)
                {
                    string result = SendNotificationAsync(item, config.PushNotify_Server_Key, config.PushNotify_Sender_Key);

                    if(result != "-1")
                    {
                        using (var r_context = new DB003())
                        {
                            r_context.PushNotifications.Remove(item);
                            r_context.SaveChanges();
                        }
                    }
                }
            }

            catch (Exception ex)
            {

                string str = ex.Message;

            }
            finally
            {
                context = null;
            }
        }

        public string SendNotificationAsync(PushNotification pushNotification,string serverKey,string senderId)
        {
            var result = "-1";
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                httpWebRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                httpWebRequest.Method = "POST";

                var payload = new
                {
                    to = pushNotification.Token,
                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        body = pushNotification.Body,
                        title = pushNotification.Titile
                    },
                };

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(payload);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }

            catch (Exception ex)
            {

                string str = ex.Message;

            }
            finally
            {

            }
            return result;
        }

    }
}
