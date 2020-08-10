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
    public class TenantController : ControllerBase
    {
        DB003 context = new DB003();

        [HttpPost]
        public IActionResult Add(Tenant tenant)
        {
            SerialNumber serialNumber = new SerialNumber();

            try
            {
                long output;
               
                using (var context = new DB003())
                {
                    tenant.TenantNo = serialNumber.GenerateTenantNumber();
                    tenant.IsActive = true;
                    tenant.CreatedOn = DateTime.Now;
                    tenant.ModifiedOn = DateTime.Now;

                    context.Tenants.Add(tenant);
                    output = context.SaveChanges();
                    

                    User user = new User()
                    {
                        Name = tenant.FullName,
                        Username = tenant.MobileNo,
                        Password = tenant.MobileNo,
                        MobileNo = tenant.MobileNo,
                        OTP = null,
                        Email = tenant.Email,
                        PGID = null
                    };


                    //Add User
                    User_Mgmnt user_Mgmnt = new User_Mgmnt();
                    bool user_output =user_Mgmnt.AddUser(user,"TNT");

  
                    //Add current month rent to rent table
                    var t = context.Tenants
                            .Include(i => i.Room)
                            .Where(w => w.PGID == tenant.PGID &&  w.TenantID == tenant.TenantID && w.IsCheckOut == false)
                            .FirstOrDefault();

                    Rent_Calculus rent_Calculus = new Rent_Calculus();
                    rent_Calculus.ForTenant(t);
                    
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
                serialNumber = null;
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