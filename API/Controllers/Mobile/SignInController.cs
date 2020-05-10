using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Contex;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Mobile
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        DB003 context = new DB003();

        [Route("Validate")]
        [HttpGet]
        public IActionResult Validate(string mobileNo)
        {
            try
            {
                var output = context.Users
                    .Where(w => w.MobileNo.Trim() == mobileNo.Trim())
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

    }
}