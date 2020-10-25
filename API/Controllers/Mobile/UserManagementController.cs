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
    public class UserManagementController : ControllerBase
    {
        [Route("UpdatePushNotification")]
        [HttpPost]
        public IActionResult UpdatePushNotification(User user)
        {
            try
            {
                int output;
                using (var context = new DB003())
                {
                    var input = context.Users
                    .Where(w => w.UserID == user.UserID)
                    .FirstOrDefault();

                    input.PushNotificationToken = user.PushNotificationToken;
                    input.ModifiedOn = DateTime.Now;
                    output = context.SaveChanges();
                }
                return Ok(output);
            }
            catch (Exception ex)
            {
                Exception_C.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return StatusCode(500);
            }
            finally
            {
                
            }
        }

        [Route("UpdateDeviceID")]
        [HttpPost]
        public IActionResult UpdateDeviceID(User user)
        {
            try
            {
                int output;
                using (var context = new DB003())
                {
                    var input = context.Users
                    .Where(w => w.UserID == user.UserID)
                    .FirstOrDefault();

                    input.DeviceID = user.DeviceID;
                    input.ModifiedOn = DateTime.Now;
                    output = context.SaveChanges();
                }
                return Ok(output);
            }
            catch (Exception ex)
            {
                Exception_C.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return StatusCode(500);
            }
            finally
            {

            }
        }
    }
}