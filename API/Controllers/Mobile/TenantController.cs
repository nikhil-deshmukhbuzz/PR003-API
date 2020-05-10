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
    public class TenantController : ControllerBase
    {
        DB003 context = new DB003();

        [HttpPost]
        public IActionResult Add(Tenant tenant)
        {
            try
            {
                long output;
                using (var context = new DB003())
                {
                    tenant.CreatedOn = DateTime.Now;
                    tenant.ModifiedOn = DateTime.Now;

                    context.Tenants.Add(tenant);
                    output = context.SaveChanges();



                    var user_exists = context.Users
                        .Where(w => w.MobileNo == tenant.MobileNo)
                        .FirstOrDefault();

                    if (user_exists == null)
                    {
                        var profile_output = context.ProfileMasters
                          .Where(w => w.ProfileName == "Tenant")
                          .FirstOrDefault();

                        User user = new User()
                        {
                            Name = tenant.FullName,
                            Username = tenant.MobileNo,
                            Password = tenant.MobileNo,
                            MobileNo = tenant.MobileNo,
                            OTP = null,
                            Email = tenant.Email,
                            IsActive = true,
                            PGID = null,
                            ProfileMasterID = profile_output.ProfileID,

                            CreatedOn = DateTime.Now,
                            ModifiedOn = DateTime.Now
                        };

                        long user_output;
                        using (var user_context = new DB003())
                        {
                            user_context.Users.Add(user);
                            user_output = user_context.SaveChanges();
                        }
                    }
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

                    input.FullName = tenant.FullName;
                    input.RoomID = tenant.RoomID;
                    input.MobileNo = tenant.MobileNo;
                    input.Email = tenant.Email;
                    input.RentAmount = tenant.RentAmount;
                    input.DepositAmount = tenant.DepositAmount;
                    input.CheckInDate = tenant.CheckInDate;
                    input.IsActive = tenant.IsActive;
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

        [Route("GetList")]
        [HttpGet]
        public IActionResult GetList(long pgId)
        {
            try
            {
                var output = context.Tenants
                    .Include(i => i.Room)
                    .Where(w => w.PGID == pgId && w.IsCheckOut == false)
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

        [Route("Get")]
        [HttpGet]
        public IActionResult Get(long pgId, long tenantId)
        {
            try
            {
                var output = context.Tenants
                    .Where(w => w.PGID == pgId && w.TenantID == tenantId)
                    .FirstOrDefault();

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