using API.Contex;
using API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.Core;

namespace API.Controllers.Mobile
{
    [EnableCors("CorsPolicy")]
    [Route("api/Mobile/[controller]")]
    [ApiController]
    public class SuscriptionController : ControllerBase
    {
       
        private readonly HttpClient client = new HttpClient();
        DB003 context = new DB003();

        [Route("GetList")]
        [HttpGet]
        public IActionResult GetList()
        {
            try
            {
                var output = context.Suscriptions
                   
                    .Where(w => w.IsActive == true)
                    .ToList();
                
                return Ok(output);
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
                        Status = "trial",
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
                                DateTime lastDateOfSuscription = trans.TransactionDate.AddDays(suscription.ValidityInDays);
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
                                    Message = "payment due"
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
           

            var pg = context.PGs
                   .Where(w => w.PGID == request.PGID)
                   .FirstOrDefault();

            var TrialDuration = DateTime.Now.Subtract(pg.CreatedOn.Value).Days;
            DateTime lastDateOfSuscription = pg.CreatedOn.Value.AddMonths(1);

            if (TrialDuration <= 30)
            {
                suscriptionResponse = new SuscriptionResponse()
                {
                    Status = "trial",
                    Message = "trial subscription",
                    LastDateOfSuscription = lastDateOfSuscription
                };
            }
            else
            {
                var trasaction = context.Transactions
                    .Where(w => w.CustomerCode == request.CustomerCode)
                    .OrderByDescending(o => o.TransactionDate)
                    .FirstOrDefault();

                if (trasaction != null)
                {
                    var suscription = context.Suscriptions
                        .Where(w => w.SerialNumber == trasaction.SuscriptionNumber)
                        .FirstOrDefault();

                    lastDateOfSuscription = trasaction.TransactionDate.AddDays(suscription.ValidityInDays);
                    if (lastDateOfSuscription > DateTime.Now)
                    {
                        suscriptionResponse = new SuscriptionResponse()
                        {
                            Status = "valid",
                            Message = "subscription",
                            LastDateOfSuscription = lastDateOfSuscription

                        };
                    }
                    else
                    {
                        suscriptionResponse = new SuscriptionResponse()
                        {
                            Status = "expire",
                            Message = "subscription is expired",
                            LastDateOfSuscription = null
                        };
                    }

                    
                }
                else
                {
                    suscriptionResponse = new SuscriptionResponse()
                    {
                        Status = "expire",
                        Message = "subscription is expired",
                        LastDateOfSuscription = null

                    };
                   
                }

            }
            
            return Ok(suscriptionResponse);
        }

        [Route("AddTransaction")]
        [HttpPost]
        public async Task<IActionResult> AddTransaction(SuscriptionRequest request)
        {
            try
            {
                var lastTrans = await GetLastTransaction(request);
                bool output = addTransaction(lastTrans);

                return Ok(output);
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

        [Route("TransactionErrorLog")]
        [HttpPost]
        public IActionResult TransactionErrorLog(TransactionError transactionError)
        {
            try
            {
                var error = context.Errors
                    .Where(w => w.Code == transactionError.ErrorCode)
                    .FirstOrDefault();

                transactionError.ErrorType = error.Type;
                transactionError.Message = error.Message;
                transactionError.CreatedOn = DateTime.Now;
                transactionError.ModifiedOn = DateTime.Now;

                using (var context = new DB003())
                {
                    context.TransactionErrors.Add(transactionError);
                    var output = context.SaveChanges();
                }

                return Ok();
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
                        DateTime lastDateOfSuscription = transaction.TransactionDate.AddDays(suscription.ValidityInDays);
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
            Transaction output = null;
            try
            {
                Host host = new Host();
                string payURL = host.GetHostedURL("Pay");
                host = null;

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync(payURL + "api/Payment/GetLatestTransaction", request);
                 output = await response.Content.ReadAsAsync<Transaction>();
            }
            catch(Exception ex)
            {
                output = null;
            }
            finally
            {

            }
            return output;
        }
    }
}