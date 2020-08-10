using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;
using API.Contex;

namespace API.Controllers.Mobile
{
    [EnableCors("CorsPolicy")]
    [Route("api/Mobile/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly string payURL = "https://b131stolj4.execute-api.ap-south-1.amazonaws.com/Prod/";
        private readonly HttpClient client = new HttpClient();
        DB003 context = new DB003();

        [Route("Verify")]
        [HttpPost]
        public async Task<IActionResult> Verify(SuscriptionRequest request)
        {
            SuscriptionResponse suscriptionResponse = null;
            try
            {
                var pg = context.PGs
                    .Where(w => w.PGID == request.PGID)
                    .FirstOrDefault();

                var TrialDuration = DateTime.Now.Subtract(pg.CreatedOn.Value).Days;
                if (TrialDuration <= 30)
                {
                    suscriptionResponse = new SuscriptionResponse()
                    {
                        Status = "valid",
                        Message = "trial subscription"

                    };
                }
                else
                {
                    bool bSuscription = IsSuscriptionValid(request.CustomerCode);

                    if (bSuscription)
                    {
                        suscriptionResponse = new SuscriptionResponse()
                        {
                            Status = "valid",
                            Message = "valid subscription"

                        };
                    }
                    else
                    {
                        Transaction trans = await GetLastTransaction(request);

                        if (trans != null && trans.SuscriptionNumber != null)
                        {
                            context = new DB003();
                            var suscription = context.Suscriptions
                                .Where(w => w.SerialNumber == trans.SuscriptionNumber)
                                .FirstOrDefault();

                            if (suscription != null)
                            {
                                DateTime lastDateOfSuscription = trans.TransactionDate.AddMonths(suscription.ValidityInDays);
                                if (lastDateOfSuscription > DateTime.Now)
                                {
                                    bool addTrans = addTransaction(trans);

                                    if (addTrans)
                                    {
                                        suscriptionResponse = new SuscriptionResponse()
                                        {
                                            Status = "valid",
                                            Message = "new trans entry added"
                                        };
                                    }
                                    else
                                    {
                                        suscriptionResponse = new SuscriptionResponse()
                                        {
                                            Status = "valid",
                                            Message = "entry is not added"
                                        };
                                    }
                                }
                                else
                                {
                                    suscriptionResponse = new SuscriptionResponse()
                                    {
                                        Status = "due",
                                        Message = "payment due"
                                    };
                                }


                            }
                            else
                            {
                                suscriptionResponse = new SuscriptionResponse()
                                {
                                    Status = "due",
                                    Message = "payment Due"
                                };
                            }
                        }
                        else
                        {
                            suscriptionResponse = new SuscriptionResponse()
                            {
                                Status = "due",
                                Message = "payment Due"
                            };
                        }
                    }
                }

                return Ok(suscriptionResponse);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                context = null;
            }
        }


        [Route("ExpireOn")]
        [HttpPost]
        public IActionResult ExpireOn(SuscriptionRequest request)
        {
            SuscriptionResponse suscriptionResponse = null;
            try
            {
                var suscription = context.Suscriptions
                .Where(w => w.SerialNumber == request.CustomerCode)
                .FirstOrDefault();

                var trasaction = context.Transactions
                    .Where(w => w.SuscriptionNumber == request.CustomerCode)
                    .FirstOrDefault();

                if (suscription != null && trasaction != null)
                {
                    DateTime lastDateOfSuscription = trasaction.TransactionDate.AddMonths(suscription.ValidityInDays);
                    if (lastDateOfSuscription > DateTime.Now)
                    {

                    }
                    else
                    {

                    }
                }
                else
                {
                    suscriptionResponse = new SuscriptionResponse()
                    {
                        Status = "due",
                        Message = "payment Due"
                    };
                }
                return Ok(suscriptionResponse);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                context = null;
            }
        }

        private bool IsSuscriptionValid(string customerCode)
        {

            bool result = false;
            try
            {
                var transaction = context.Transactions
                    .OrderByDescending(o => o.TransactionDate)
                    .Where(w => w.CustomerCode == customerCode)
                    .FirstOrDefault();

                if (transaction != null)
                {
                    var suscription = context.Suscriptions
                        .Where(w => w.SerialNumber == transaction.SuscriptionNumber)
                        .FirstOrDefault();

                    if (suscription != null)
                    {
                        DateTime lastDateOfSuscription = transaction.TransactionDate.AddMonths(suscription.ValidityInDays);
                        if (lastDateOfSuscription > DateTime.Now)
                            result = true;

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                context = null;
            }

            return result;
        }

        private bool addTransaction(Transaction transaction)
        {
            bool result = false;
            try
            {
                using (var context = new DB003())
                {
                    context.Transactions.Add(transaction);
                    var o = context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                context = null;
            }
            return result;
        }

        private async Task<Transaction> GetLastTransaction(SuscriptionRequest request)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync(payURL + "api/Payment/GetLatestTransaction", request);
            var output = await response.Content.ReadAsAsync<Transaction>();
            return output;
        }
    }
}