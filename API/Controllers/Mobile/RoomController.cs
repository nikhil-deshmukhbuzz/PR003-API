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
    public class RoomController : ControllerBase
    {
        DB003 context = new DB003();

        [HttpPost]
        public IActionResult Add(Room room)
        {
            try
            {
                long output;
                using (var context = new DB003())
                {
                    context.Rooms.Add(room);
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
                context = null;
            }
        }
        
        [Route("Update")]
        [HttpPost]
        public IActionResult Update(Room room)
        {
            try
            {
                int output;
                using (var context = new DB003())
                {
                    var input = context.Rooms
                    .Where(w => w.RoomID == room.RoomID)
                    .FirstOrDefault();

                    input.RoomNo = room.RoomNo;
                    input.RoomSharingID = room.RoomSharingID;
                    input.RentAmount = room.RentAmount;
                    input.DepositAmount = room.DepositAmount;
                    input.IsActive = room.IsActive;
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
                context = null;
            }
        }

        [Route("GetList")]
        [HttpGet]
        public IActionResult GetList(long pgId)
        {
            try
            {
                var output = context.Rooms
                    .Include(i => i.RoomSharing)
                    .Where(w => w.PGID == pgId)
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
        public IActionResult Get(long pgId, long roomId)
        {
            try
            {
                var output = context.Rooms
                    .Where(w => w.PGID == pgId && w.RoomID == roomId)
                    .FirstOrDefault();

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