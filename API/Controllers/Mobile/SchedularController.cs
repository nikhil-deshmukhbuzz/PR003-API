using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.Contex;
using API.Core;
using API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Mobile
{

    [EnableCors("CorsPolicy")]
    [Route("api/Mobile/[controller]")]
    [ApiController]
    public class SchedularController : ControllerBase
    {
        Host host = null;
        DB003 context = null;
        string Email_Url = null;

        [Route("SendEmailToPG")]
        [HttpGet]
        public async Task<IActionResult> SendEmailToPG()
        {
            try
            {
                Monitor_C.Add("Hourly_SendEmailToPG", "");
                context = new DB003();
                var emails = context.Emails
                    .Where(w => w.IsError == false && w.IsSent == false)
                    .ToList();

                foreach (var e in emails)
                {
                    long EmailID = e.EmailID;
                    bool send = await SendEmail(e);

                    e.EmailID = EmailID;
                    if (send)
                    {
                        using (context = new DB003())
                        {
                            var input = context.Emails
                                .Where(w => w.EmailID == e.EmailID)
                                .FirstOrDefault();

                            input.IsSent = true;
                            var o_update_sent_flag = context.SaveChanges();
                        }
                    }
                    else
                    {
                        using (context = new DB003())
                        {
                            var input = context.Emails
                                .Where(w => w.EmailID == e.EmailID)
                                .FirstOrDefault();

                            input.IsError = true;
                            input.ErrorMessage = "Sending Error..";
                            var o_update_error_flag = context.SaveChanges();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Monitor_C.Add("Hourly_SendEmailToPG", "Exception : " + ex.Message);

            }
            finally
            {

            }
            return Ok(true);
        }

        [Route("Save_Monthly_RentDetails_XL")]
        [HttpGet]
        public IActionResult Save_Monthly_RentDetails_XL()
        {
            try
            {
                Monitor_C.Add("Monthly_Save_Monthly_RentDetails_XL", "");
                Excel excel = null;
                dynamic attachment = null;
                Email email = null;
                try
                {
                    context = new DB003();
                    var pgs = context.PGs
                            .ToList();

                    foreach (var p in pgs)
                    {
                        excel = new Excel();

                        string MonthName = "";
                        int Year = DateTime.Now.Year;
                        if (DateTime.Now.Month == 1)
                        {
                            attachment = excel.Rent(p.PGID, 12, Year - 1);
                            MonthName = "Dec";
                        }
                        else
                        {
                            attachment = excel.Rent(p.PGID, DateTime.Now.Month - 1, Year);
                            MonthName = DateTime.Now.AddMonths(-1).ToString("MMMM");
                        }

                        if (attachment != null)
                        {
                            email = new Email()
                            {
                                ProductCode = "PR003",
                                CustomerCode = p.PGNo,
                                To = p.Email.Trim(),
                                CC = null,
                                Subject = "Monthly Rent Details: [" + MonthName + "] " + "-[" + Year + "]",
                                Body = GetRentDetailsBody(MonthName, Year, p.Name),
                                Attachement = attachment,
                                AttachementName = "Monthly_Rent_Details_" + MonthName + "_" + Year + ".xlx",
                                CreatedOn = DateTime.Now,
                                ModifiedOn = DateTime.Now
                            };

                            using (var context = new DB003())
                            {
                                context.Emails.Add(email);
                                var output = context.SaveChanges();
                            }

                        }

                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    excel = null;
                    attachment = null;
                    email = null;
                }

            }
            catch (Exception ex)
            {
                Monitor_C.Add("Monthly_Save_Monthly_RentDetails_XL", "Exception : " + ex.Message);

            }
            finally
            {

            }
            return Ok(true);
        }


        [Route("RentCalculation")]
        [HttpGet]
        public IActionResult RentCalculation()
        {
            Rent_Calculus rent_calculus = new Rent_Calculus();
            try
            {
                Monitor_C.Add("Monthly_RentCalculation", "");
                rent_calculus.ForAllPG();

            }
            catch (Exception ex)
            {
                Monitor_C.Add("Monthly_RentCalculation", "Exception : " + ex.Message);

            }
            finally
            {
                rent_calculus = null;
            }
            return Ok(true);
        }

        private async Task<bool> SendEmail(Email request)
        {
            HttpClient client = new HttpClient();
            bool output = false;
            request.EmailID = 0;
            try
            {
                if (Email_Url == null)
                {
                    host = new Host();
                    Email_Url = host.GetHostedURL("Email");
                    host = null;
                }

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync(Email_Url + "api/Email/Add", request);
                output = await response.Content.ReadAsAsync<bool>();
            }
            catch (Exception ex)
            {
                output = false;
            }
            finally
            {
                client = null;
            }
            return output;
        }

        private string GetRentDetailsBody(string month, int year, string pg)
        {
            string body = "";
            body += "Dear PG Owner,";
            body += "<br/>&&nbsp;&nbsp;nbsp;Please find the attached XL of " + pg + " Rent Details in <b>" + month + "-" + year + "</b>.";
            body += "<br/><br/><br/><br/>";
            body += "<br/>Regards,";
            body += "Maintain-easy"; ;
            return body;
        }
    }
}