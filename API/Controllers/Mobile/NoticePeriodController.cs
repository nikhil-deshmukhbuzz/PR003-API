using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Contex;
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
    public class NoticePeriodController : ControllerBase
    {
        DB003 context = new DB003();

        [Route("GetList")]
        [HttpGet]
        public IActionResult GetList(long pgId)
        {
            try
            {
                var output = context.Tenants
                    .Include(i => i.Room)
                    .Where(w => w.PGID == pgId && w.CheckOutDate != null && w.IsCheckOut == false)
                    .ToList();

                foreach(var item in output)
                {
                    TimeSpan ts = item.CheckOutDate.Value - DateTime.Now;
                    item.NoOfDaysleft = ts.Days;
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

        [Route("Update")]
        [HttpPost]
        public IActionResult Update(Tenant tenant)
        {
            try
            {
                int output;
                using (var context = new DB003())
                {
                    var input = context.Tenants
                    .Where(w => w.TenantID == tenant.TenantID)
                    .FirstOrDefault();
                    
                    input.CheckOutDate = tenant.CheckOutDate;
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

        [Route("Checkout")]
        [HttpPost]
        public IActionResult Checkout(Tenant tenant)
        {
            try
            {
                int output;
                using (var context = new DB003())
                {
                    var input = context.Tenants
                    .Where(w => w.TenantID == tenant.TenantID)
                    .FirstOrDefault();

                   if (tenant.CheckOutDate == null && input.CheckOutDate == null)
                    {

                        input.CheckOutDate = DateTime.Now;
                    }

                    input.IsCheckOut = true;
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

        [Route("Cancellation")]
        [HttpPost]
        public IActionResult Cancellation(Tenant tenant)
        {
            try
            {
                int output;
                using (var context = new DB003())
                {
                    var input = context.Tenants
                    .Where(w => w.TenantID == tenant.TenantID)
                    .FirstOrDefault();

                    input.CheckOutDate = null;
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
    }
}