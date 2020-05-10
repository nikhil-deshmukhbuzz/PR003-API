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
                throw ex;
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
                throw ex;
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
                   .Include(i => i.PG)
                   .Include(i => i.ProfileMaster)
                   .FirstOrDefault();

                if (user != null)
                {
                    bResult = true;  
                }
                
                return Ok(bResult);
            }
            catch (Exception ex)
            {
                throw ex;
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

                    lastDateOfSuscription = trasaction.TransactionDate.AddMonths(suscription.ValidityInMonth);
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
    }
}