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

namespace API.Controllers.Mobile
{
    [EnableCors("CorsPolicy")]
    [Route("api/Mobile/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        DB003 context = new DB003();

        [HttpPost]
        public IActionResult Add([FromBody] Registration registration)
        {
            try
            {
                SerialNumber serialNumber = new SerialNumber();
                registration.IsRegister = false;
                registration.CreatedOn = DateTime.Now;
                registration.ModifiedOn = DateTime.Now;
                registration.RegistrationNo = serialNumber.GenerateRegistrationNumber();


                long output;
                using (var context = new DB003())
                {
                    context.Registrations.Add(registration);
                    output = context.SaveChanges();
                }
                return Ok(output);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Route("RegistrationCheck")]
        [HttpGet]
        public IActionResult RegistrationCheck(string mobileNo)
        {
            try
            {
                bool output = false;

                var registration = context.Registrations
                    .Where(w => w.MobileNo == mobileNo)
                    .FirstOrDefault();


                if (registration == null)
                    output = true;
                else
                    output = false;

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