using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Contex;
using API.Core;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Mobile
{

    [EnableCors("CorsPolicy")]
    [Route("api/Mobile/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        DB003 context = new DB003();

        [Route("GetTenantList")]
        [HttpGet]
        public IActionResult GetTenantList(long tenantId)
        {
            try
            {
                var output = context.PushNotifications
                    .Where(w => w.Visible == true)
                    .OrderByDescending(o => o.CreatedOn)
                    .Where(w => w.TenantID == tenantId)
                    .Take(25)
                    .ToList();

                return Ok(output);
            }
            catch (Exception ex)
            {
                Exception_C.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return StatusCode(500);
            }
            finally
            {
                context = null;
            }
        }


        [Route("GetPGList")]
        [HttpGet]
        public IActionResult GetPGList(long pgId)
        {
            try
            {
                var output = context.PushNotifications
                    .Where(w => w.Visible == true)
                    .OrderByDescending(o => o.CreatedOn)
                    .Where(w => w.PGID == pgId)
                    .Take(25)
                    .ToList();

                return Ok(output);
            }
            catch (Exception ex)
            {
                Exception_C.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return StatusCode(500);
            }
            finally
            {
                context = null;
            }
        }
    }
}