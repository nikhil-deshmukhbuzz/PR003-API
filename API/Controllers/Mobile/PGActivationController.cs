﻿using System;
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
    public class PGActivationController : ControllerBase
    {
        DB003 context = new DB003();

        [Route("RegistrationList")]
        [HttpGet]
        public IActionResult RegistrationList()
        {
            try
            {
                var output = context.Registrations
                    .Where(w => w.IsRegister == false)
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

        [Route("Activation")]
        [HttpPost]
        public IActionResult Activation(Registration registration)
        {
            try
            {
                var output = context.Registrations
                    .Where(w => w.RegistrationID == registration.RegistrationID)
                    .FirstOrDefault();

                SerialNumber serialNumber = new SerialNumber();

                PG pg = new PG()
                {
                    PGNo = serialNumber.GeneratePGNumber(),
                    Name = output.PGName,
                    OwnerName = output.FullName,
                    MobileNo = output.MobileNo,
                    Email = output.Email,
                    Address = output.Address,
                    City = output.City,
                    IsActive = true,

                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                };

                long pg_output;
                using (var pg_context = new DB003())
                {
                    pg_context.PGs.Add(pg);
                    pg_output = pg_context.SaveChanges();
                }

                
                long registration_output;
                using (var registration_context = new DB003())
                {
                    var input = registration_context.Registrations
                                       .Where(w => w.RegistrationID == registration.RegistrationID)
                                       .FirstOrDefault();

                    input.IsRegister = true;

                    registration_output = registration_context.SaveChanges();
                }

                User user = new User()
                {
                    Name = pg.Name,
                    Username = pg.MobileNo,
                    Password = pg.MobileNo,
                    MobileNo = pg.MobileNo,
                    OTP = null,
                    Email = pg.Email,
                    PGID = pg.PGID
                };

                //Add User
                User_Mgmnt user_Mgmnt = new User_Mgmnt();
                bool user_output = user_Mgmnt.AddUser(user, "PG");

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