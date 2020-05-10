using System;
using System.Collections.Generic;
using System.Linq;
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

        [Route("CalculateForAllPG")]
        [HttpPost]
        public IActionResult CalculateForAllPG()
        {
            try
            {
                var output = context.PGs
                    .Where( w => w.IsActive == true)
                    .ToList();

                foreach (var item in output)
                {
                    calculateRentPGID(item.PGID);
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

        [Route("CalculateForPG")]
        [HttpPost]
        public IActionResult CalculateForPG(long pgId)
        {
            try
            {

                calculateRentPGID(pgId);

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

        private void calculateRentPGID(long pgId)
        {
            var output = context.Tenants
                   .Where(w => w.IsActive == true && w.IsCheckOut == false && w.PGID == pgId)
                   .ToList();

            foreach(var item in output)
            {
                calculateRentTenantID(item.TenantID,pgId);
            }
        }

        private void calculateRentTenantID(long tenantId,long pgId)
        {
            long month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            var output = context.Rents
               .Where(w => w.Year == year && w.MonthID == month && w.TenantID == tenantId && w.PGID == pgId)
               .FirstOrDefault();

            if(output == null)
            {
                insertRentTenantID(tenantId,pgId);
            }


        }


        private void insertRentTenantID(long tenantId,long pgId)
        {
            try
            {
                var tenant = context.Tenants
                            .Include(i => i.Room)
                            .Where(w => w.PGID == pgId && w.TenantID == tenantId)
                            .FirstOrDefault();

                SerialNumber serialNumber = new SerialNumber();

                long output;
                Rent rent = new Rent()
                {
                    InvoiceNumber = serialNumber.GenerateInvoiceNumber(),
                    FullName = tenant.FullName,
                    RoomNo = tenant.Room.RoomNo,
                    RentAmount = tenant.RentAmount,
                    TotalAmount = tenant.RentAmount,
                    PGID = pgId,
                    TenantID = tenantId,
                    PaymentStatusID = Convert.ToInt64(Status.Unpaid),
                    MonthID = DateTime.Now.Month,
                    Year = DateTime.Now.Year,
                    CreatedBy = null,
                    CreatedOn = null,
                    ModifiedBy = null,
                    ModifiedOn = null
                };

                using (var context = new DB003())
                {
                    context.Rents.Add(rent);
                    output = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}