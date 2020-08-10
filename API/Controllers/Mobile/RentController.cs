using System;
using System.Collections.Generic;
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
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Mobile
{
    [EnableCors("CorsPolicy")]
    [Route("api/Mobile/[controller]")]
    [ApiController]
    public class RentController : ControllerBase
    {
        DB003 context = new DB003();
       
        [Route("GetList")]
        [HttpGet]
        public IActionResult GetList(long pgId)
        {
            try
            {
                var output = context.Rents
                    .Include(i => i.PaymentStatus)
                    .Where(w => w.PGID == pgId && w.MonthID == DateTime.Now.Month && w.Year == DateTime.Now.Year)
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

        [Route("Update")]
        [HttpPost]
        public IActionResult Update(Rent rent)
        {
            try
            {
                int output;
                using (var context = new DB003())
                {
                    var input = context.Rents
                    .Where(w => w.RentID == rent.RentID && w.PGID == rent.PGID && w.TenantID == rent.TenantID)
                    .FirstOrDefault();

                    input.RentAmount = rent.RentAmount;
                    input.PaymentStatusID = rent.PaymentStatusID;
                    input.MonthID = rent.MonthID;
                    input.Year = rent.Year;
                    input.ModifiedOn = DateTime.Now;
                    output = context.SaveChanges();
                }
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

        [Route("GetFilterList")]
        [HttpGet]
        public IActionResult GetFilterList(long pgId, long monthId,long year)
        {
            try
            {
                var output = context.Rents
                    .Include(i => i.PaymentStatus)
                    .Where(w => w.PGID == pgId && w.MonthID == monthId && w.Year == year)
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

        [Route("GetPaymentStatus")]
        [HttpGet]
        public IActionResult GetPaymentStatus()
        {
            try
            {
                var output = context.PaymentStatuss
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

        

        [Route("GetTenantReceipts")]
        [HttpGet]
        public IActionResult GetTenantReceipts(long tenantId, long pgId)
        {
            try
            {
                var output = context.Rents
                            .Include(i => i.PaymentStatus)
                            .Include(i => i.Month)
                            .Where(w => w.TenantID == tenantId && w.PGID == pgId)
                            .OrderByDescending(o => o.CreatedOn)
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


        [Route("CalculateForAllPG")]
        [HttpPost]
        public IActionResult CalculateForAllPG()
        {
            Rent_Calculus rent_calculus = new Rent_Calculus();
            try
            {
                rent_calculus.ForAllPG();
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                rent_calculus = null;
            }
        }

        [Route("CalculateForPG")]
        [HttpPost]
        public IActionResult CalculateForPG(long pgId)
        {
            Rent_Calculus rent_calculus = new Rent_Calculus();
            try
            {
                rent_calculus.ForPG(pgId);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                rent_calculus = null;
            }
        }

        //////////////// Online Transaction ///////////////

        [Route("CheckInvoiceTransaction")]
        [HttpPost]
        public async Task<IActionResult> CheckInvoiceTransaction(SuscriptionRequest request)
        {
            bool result = false;
            try
            {
                var c_transaction = context.C_Transactions
                            .Where(w => w.InvoiceNumber == request.InvoiceNumber)
                            .FirstOrDefault();

                if(c_transaction != null)
                {
                    if (c_transaction.PaymentStatus.ToLower() == "failure")
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                } 
                else
                {
                    C_Transaction transaction = await GetTransactionByReceipt(request);
                    if (transaction != null)
                    {
                        if (transaction.PaymentStatus == "failure")
                        {
                            result = false;
                        }
                        else
                        {
                            result = true;
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }


                
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                context = null;
            }

            return Ok(result);
        }


        [Route("AddTransaction")]
        [HttpPost]
        public async Task<IActionResult> AddTransaction(SuscriptionRequest request)
        {
            try
            {
                var trans = await GetTransactionByReceipt(request);
                bool output = add_C_Transaction(trans);

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



        [Route("UpdateRentPaymnetStatusToPaid")]
        [HttpPost]
        public IActionResult UpdateRentPaymnetStatusToPaid(Rent rent)
        {
            bool result = false;
            try
            {
                var paymrntStatus = context.PaymentStatuss
                                    .Where(w => w.Status == "Paid")
                                    .FirstOrDefault();


                using (var context = new DB003())
                {
                    var input = context.Rents
                    .Where(w => w.RentID == rent.RentID && w.PGID == rent.PGID && w.TenantID == rent.TenantID && w.InvoiceNumber == rent.InvoiceNumber)
                    .FirstOrDefault();

                    input.PaymentStatusID = paymrntStatus.PaymentStatusID;
                    input.ModifiedOn = DateTime.Now;
                    var output = context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                context = null;
            }
            return Ok(result);
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
                transactionError.Message =  error.Message;
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

        private bool add_C_Transaction(C_Transaction transaction)
        {
            bool result = false;
            try
            {
                using (var context = new DB003())
                {
                    context.C_Transactions.Add(transaction);
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

        private async Task<C_Transaction> GetTransactionByReceipt(SuscriptionRequest request)
        {
            HttpClient client = new HttpClient();
            C_Transaction output = null;
            try
            {
                Host host = new Host();
                string payURL = host.GetHostedURL("Pay");
                host = null;

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync(payURL + "api/Payment/GetTransactionByReceipt", request);
                output = await response.Content.ReadAsAsync<C_Transaction>();
            }
            catch (Exception ex)
            {
                output = null;
            }
            finally
            {
                client = null;
            }
            return output;
        }
    }
}