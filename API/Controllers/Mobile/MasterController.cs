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
    public class MasterController : ControllerBase
    {
        DB003 context = new DB003();

        [Route("Load")]
        [HttpGet]
        public IActionResult Load(long pgId)
        {
            PGMaster output = new PGMaster();
            try
            {
                var rooms = context.Rooms
                    .Include(i => i.RoomSharing)
                    .Where(w => w.PGID == pgId)
                    .ToList();

                var roomsharings = context.RoomSharings
                        .ToList();

                var tenants = context.Tenants
                    .Include(i => i.Room)
                    .Where(w => w.PGID == pgId && w.IsCheckOut == false)
                    .ToList();

                var rents = context.Rents
                    .Include(i => i.PaymentStatus)
                    .Where(w => w.PGID == pgId && w.MonthID == DateTime.Now.Month && w.Year == DateTime.Now.Year)
                    .ToList();

                var noticeperiods = context.Tenants
                    .Include(i => i.Room)
                    .Where(w => w.PGID == pgId && w.CheckOutDate != null && w.IsCheckOut == false)
                    .ToList();

                var paymentstatus = context.PaymentStatuss
                    .ToList();

                foreach (var item in noticeperiods)
                {
                    TimeSpan ts = item.CheckOutDate.Value - DateTime.Now;
                    item.NoOfDaysleft = ts.Days;
                }
                var pgownerdashboard = GetPGOwnerDashboard(pgId);

                output.Rooms = rooms;
                output.RoomSharings = roomsharings;
                output.Tenants = tenants;
                output.Rents = rents;
                output.NoticePeriods = noticeperiods;
                output.PaymentStatus = paymentstatus;
                output.PGOwnerDashboard = pgownerdashboard;

                return Ok(output);
            }
            catch (Exception ex)
            {
                Exception_C.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return StatusCode(500);
            }
            finally
            {
                output = null;
                context = null;
            }
        }

        private PGOwnerDashboard GetPGOwnerDashboard(long pgId)
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
                        availableBeds.Add(new BedAvailable
                        {
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
                    dueRents.Add(new DueRent
                    {
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

                return output;
            }
            catch (Exception ex)
            {
                Exception_C.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                return null;
            }
            finally
            {
                context = null;
            }
        }
    }
}