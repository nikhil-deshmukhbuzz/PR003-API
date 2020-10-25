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
    public class DashboardController : ControllerBase
    {
        DB003 context = new DB003();

        [Route("PGOwner")]
        [HttpGet]
        public IActionResult PGOwner(long pgId)
        {
            try
            {
                PGOwnerDashboard output = new PGOwnerDashboard();

                var rooms = context.Rooms
                    .Include(i => i.RoomSharing)
                    .Where(w => w.PGID == pgId)
                    .ToList();

                var rents = context.Rents
                    .Include(i => i.PaymentStatus)
                    .Where(w => w.PGID == pgId && w.MonthID == DateTime.Now.Month && w.Year == DateTime.Now.Year && w.PaymentStatus.Status == "Unpaid")
                    .ToList();

                var notice = context.Tenants
                    .Include(i => i.Room)
                    .Where(w => w.PGID == pgId && w.CheckOutDate != null && w.IsCheckOut == false)
                    .ToList();

                //Calculate Available Beds
                List<BedAvailable> availableBeds = new List<BedAvailable>();
                int noOfBedAvailable = 0;
                foreach (var item in rooms)
                {
                    var tCount = context.Tenants
                        .Where(w => w.PGID == pgId && w.RoomID == item.RoomID)
                        .Count();

                    int available_bed = item.RoomSharing.NoOfBed - tCount;

                    if (available_bed >= 0)
                    {
                        availableBeds.Add(new BedAvailable {
                            RoomNo = item.RoomNo,
                            SharingType = item.RoomSharing.Name,
                            Count = available_bed
                        });
                        noOfBedAvailable = noOfBedAvailable + available_bed;
                    }
                    else
                    {
                        availableBeds.Add(new BedAvailable
                        {
                            RoomNo = item.RoomNo,
                            SharingType = item.RoomSharing.Name,
                            Count = 0
                        });
                    }
                }
                //End Calculate Available Beds

                //Due Rent
                List<DueRent> dueRents = new List<DueRent>();
                foreach (var item in rents)
                {
                    dueRents.Add(new DueRent {
                        RoomNo = item.RoomNo,
                        TenantName = item.FullName,
                        RentAmount = item.RentAmount
                    });
                }

                //End Due Rent

                //Due Rent
                List<OnNotice> onNotice = new List<OnNotice>();
                foreach (var item in notice)
                {
                    onNotice.Add(new OnNotice
                    {
                        RoomNo = item.Room.RoomNo,
                        TenantName = item.FullName,
                        CheckOutDate = item.CheckOutDate
                    });
                }

                //End Due Rent

                output.NoOfBedAvailable = noOfBedAvailable;
                output.NoOfDueRent = rents.Count();
                output.NoOfOnNotice = notice.Count();

                output.BedAvailable = availableBeds;
                output.DueRent = dueRents;
                output.OnNotice = onNotice;

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

        [Route("Admin")]
        [HttpGet]
        public IActionResult Admin()
        {
            try
            {
                AdminDashboard output = new AdminDashboard();

                var pgs = context.PGs
                    .ToList();
                

                output.NoOfPGOwner = pgs.Count;
                output.NoOfActive = pgs.Where(w => w.IsActive == true).ToList().Count;
                output.NoOfInActive = pgs.Where(w => w.IsActive == false).ToList().Count;

                output.PGs = pgs;
                output.ActivePGs = pgs.Where(w => w.IsActive == true).ToList();
                output.InActivePGs = pgs.Where(w => w.IsActive == false).ToList();

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