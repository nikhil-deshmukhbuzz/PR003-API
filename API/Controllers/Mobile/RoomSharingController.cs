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
    public class RoomSharingController : ControllerBase
    {
        DB003 context = new DB003();

        [HttpPost]
        public IActionResult Add(RoomSharing roomSharing)
        {
            try
            {
                long output;
                using (var context = new DB003())
                {
                    context.RoomSharings.Add(roomSharing);
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


        [Route("Update")]
        [HttpPost]
        public IActionResult Update(RoomSharing roomSharing)
        {
            try
            {
                int output;
                using (var context = new DB003())
                {
                    var input = context.RoomSharings
                    .Where(w => w.RoomSharingID == roomSharing.RoomSharingID)
                    .FirstOrDefault();

                    input.Name = roomSharing.Name;
                    input.NoOfBed = roomSharing.NoOfBed;
                    input.IsActive = roomSharing.IsActive;
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
        public IActionResult GetList()
        {
            try
            {
                var output = context.RoomSharings
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

        [Route("Get")]
        [HttpGet]
        public IActionResult Get(long pgId, long roomSharingId)
        {
            try
            {
                var output = context.RoomSharings
                    .Where(w => w.PGID == pgId && w.RoomSharingID == roomSharingId)
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