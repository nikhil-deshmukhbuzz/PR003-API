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
                return Ok(registration.RegistrationID);
            }
            catch (Exception ex)
            {
                Exception_C.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return StatusCode(500);
            }
        }

        [Route("RegistrationCheck")]
        [HttpGet]
        public IActionResult RegistrationCheck(string mobileNo)
        {
            try
            {
                long output = 0;

                var registration = context.Registrations
                    .Where(w => w.MobileNo == mobileNo)
                    .FirstOrDefault();

                var users = context.Users
                    .Where(w => w.MobileNo == mobileNo)
                    .FirstOrDefault();

                if (users != null)
                    throw new Exception("No registration. User already exists");
                else if (registration == null)
                    output = 0;
                else
                    output = registration.RegistrationID;

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