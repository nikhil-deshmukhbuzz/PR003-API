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
    public class AuthenticateController : ControllerBase
    {
        DB003 context = new DB003();

        [Route("ValidateUser")]
        [HttpPost]
        public IActionResult ValidateUser([FromBody] User oUser)
        {
            try
            {
                UserManagement userManagement = new UserManagement();


                var user = context.Users
                   .Where(u => u.MobileNo.Trim() == oUser.MobileNo.Trim() && u.OTP.Trim() == oUser.OTP.Trim())
                   .Include(i => i.PG)
                   .Include(i => i.ProfileMaster)
                   .FirstOrDefault();

                if (user != null)
                {
                    if (user.ProfileMaster.ProfileName == "PGOwner")
                    {
                        SuscriptionResponse suscriptionResponse = ExpireOn(user);
                        if (suscriptionResponse != null && suscriptionResponse.Status == "expire")
                        {
                            userManagement.Status = "expire";
                        }
                        else
                        {
                            userManagement.Status = "success";
                        }
                    }
                    else
                    {
                        var tenants = new List<Tenant>();
                        if (user.ProfileMaster.ProfileName == "Tenant")
                        {
                            tenants = context.Tenants
                                   .Include(i => i.Room)
                                   .Include(i => i.PG)
                                   .Where(u => u.MobileNo == user.MobileNo)
                                   .ToList();
                        }

                        userManagement.Tenants = tenants;
                        userManagement.Status = "success";
                    }

                    userManagement.User = user;
                       
                    var menu = context.MenuProfileLinks
                        .Where(w => w.ProfileID == user.ProfileMasterID)
                            .Include(i => i.MenuMaster)
                            .OrderBy(o => o.MenuMaster.SequenceNo)
                            .ToList();

                    List<MenuMaster> listOfMenuMaster = new List<MenuMaster>();
                    foreach (var item in menu)
                    {
                        if (item.MenuMaster.IsActive)
                            listOfMenuMaster.Add(item.MenuMaster);
                    }

                    userManagement.ListOfMenuMaster = listOfMenuMaster;
                   
                }
                else
                {
                    userManagement.Status = "wrong otp";
                }


                return Ok(userManagement);
            }
            catch (Exception ex)
            {
                Exception_C.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return StatusCode(500);
            }

        }

        [Route("ValidateUser2")]
        [HttpPost]
        public IActionResult ValidateUser2([FromBody] User oUser)
        {
            try
            {
                UserManagement userManagement = new UserManagement();


                var user = context.Users
                   .Where(u => u.MobileNo.Trim() == oUser.MobileNo.Trim() && u.PGID == oUser.PGID)
                   .Include(i => i.PG)
                   .Include(i => i.ProfileMaster)
                   .FirstOrDefault();

                if (user != null)
                {
                    if (user.ProfileMaster.ProfileName == "PGOwner")
                    {
                        SuscriptionResponse suscriptionResponse = ExpireOn(user);
                        if (suscriptionResponse != null && suscriptionResponse.Status == "expire")
                        {
                            userManagement.Status = "expire";
                        }
                        else
                        {
                            userManagement.Status = "success";
                        }
                    }
                    else
                    {
                        var tenants = new List<Tenant>();
                        if (user.ProfileMaster.ProfileName == "Tenant")
                        {
                            tenants = context.Tenants
                                   .Include(i => i.PG)
                                   .Include(i => i.Room)
                                   .Where(u => u.MobileNo == user.MobileNo)
                                   .OrderByDescending( o => o.CreatedOn)
                                   .ToList();
                        }

                        userManagement.Tenants = tenants;
                        userManagement.Status = "success";
                    }

                    userManagement.User = user;

                    var menu = context.MenuProfileLinks
                        .Where(w => w.ProfileID == user.ProfileMasterID)
                            .Include(i => i.MenuMaster)
                            .OrderBy(o => o.MenuMaster.SequenceNo)
                            .ToList();

                    List<MenuMaster> listOfMenuMaster = new List<MenuMaster>();
                    foreach (var item in menu)
                    {
                        if (item.MenuMaster.IsActive)
                            listOfMenuMaster.Add(item.MenuMaster);
                    }

                    userManagement.ListOfMenuMaster = listOfMenuMaster;

                }
                else
                {
                    userManagement.Status = "user_not_exists";
                }


                return Ok(userManagement);
            }
            catch (Exception ex)
            {
                Exception_C.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return StatusCode(500);
            }
        }
        

        [Route("UserExist")]
        [HttpPost]
        public IActionResult UserExist([FromBody] User oUser)
        {
            bool bResult = false;
            try
            {
                var user = context.Users
                   .Where(u => u.MobileNo.Trim() == oUser.MobileNo.Trim())
                   .FirstOrDefault();

                if (user != null)
                {
                    SendOTP(user.UserID);
                    bResult = true;  
                }
                
                return Ok(bResult);
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


     
        private SuscriptionResponse ExpireOn(User request)
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
                    .OrderByDescending(o => o.TransactionDate)
                    .Where(w => w.CustomerCode == request.PG.PGNo)
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

            return suscriptionResponse;
        }

        private string SendOTP(long userId)
        {
            Random random = new Random();
            try
            {
                int otp = random.Next(1000, 9999);
                string mobileNumber = String.Empty;

                using (var context = new DB003())
                {
                    var input = context.Users
                    .Where(w => w.UserID == userId)
                    .FirstOrDefault();

                    mobileNumber = input.MobileNo;

                    input.OTP = otp.ToString();
                    input.ModifiedOn = DateTime.Now;
                    context.SaveChanges();
                }

                SMS objSMS = new SMS();
                string message = "Your one time password for login is " + otp.ToString();
                objSMS.SendSMS(message, mobileNumber);
                return otp.ToString();
            }
            catch (Exception ex)
            {
                Exception_C.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return "ERROR";
            }
            finally
            {
                random = null;
                context = null;
            }
        }
    }
}