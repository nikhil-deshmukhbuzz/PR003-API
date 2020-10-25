using API.Contex;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace API.Core
{
    public class Rent_Calculus
    {
        DB003 context = new DB003();


        public void ForAllPG()
        {
            try
            {
                var output = context.PGs
                    .Where(w => w.IsActive == true)
                    .ToList();

                PushNotification_C pushNotification_C = new PushNotification_C();
                foreach (var item in output)
                {
                    calculateRentPGID(item.PGID);

                    /* Add Push Notification
                */
                    string title = "Mez";
                    string body =  TimeZone.GetCurrentDateTime().ToString("MMMM") + "-" + TimeZone.GetCurrentDateTime().Year + " rent details added";
                    pushNotification_C.AddPushNotificationPG(item.PGID, title, body);
                }

            }
            catch (Exception ex)
            {
                //Exception log
            }
            finally
            {
                context = null;
            }
        }

        public void ForPG(long pgId)
        {
            try
            {

                calculateRentPGID(pgId);
            }
            catch (Exception ex)
            {
                //Exception log
            }
            finally
            {
                context = null;
            }
        }

        public void ForTenant(Tenant tenant)
        {
            try
            {

                calculateRentTenantID(tenant);
            }
            catch (Exception ex)
            {
                //Exception log
            }
            finally
            {
                context = null;
            }
        }

        private void calculateRentPGID(long pgId)
        {
            var output = context.Tenants
                    .Include(i => i.Room)
                   .Where(w => w.IsActive == true && w.IsCheckOut == false && w.PGID == pgId)
                   .ToList();

            PushNotification_C pushNotification_c = new PushNotification_C(); ;
            foreach (var item in output)
            {
                calculateRentTenantID(item);
            }
        }

        private void calculateRentTenantID(Tenant tenant)
        {
            long month = TimeZone.GetCurrentDateTime().Month;
            int year = TimeZone.GetCurrentDateTime().Year;

            var output = context.Rents
               .Where(w => w.Year == year && w.MonthID == month && w.TenantID == tenant.TenantID && w.PGID == tenant.PGID)
               .FirstOrDefault();

            if (output == null)
            {
                insertRentTenantID(tenant);
            }
        }


        private void insertRentTenantID(Tenant tenant)
        {
            try
            {

                SerialNumber serialNumber = new SerialNumber();

                long output;
                Rent rent = new Rent()
                {
                    InvoiceNumber = serialNumber.GenerateInvoiceNumber(),
                    FullName = tenant.FullName,
                    RoomNo = tenant.Room.RoomNo,
                    RentAmount = tenant.RentAmount,
                    TotalAmount = tenant.RentAmount,
                    PGID = tenant.PGID,
                    TenantID = tenant.TenantID,
                    PaymentStatusID = Convert.ToInt64(Status.Unpaid),
                    MonthID = TimeZone.GetCurrentDateTime().Month,
                    Year = TimeZone.GetCurrentDateTime().Year,
                    CreatedBy = null,
                    CreatedOn = TimeZone.GetCurrentDateTime(),
                    ModifiedBy = null,
                    ModifiedOn = TimeZone.GetCurrentDateTime()
                };

                using (var context = new DB003())
                {
                    context.Rents.Add(rent);
                    output = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //Exception log
            }
        }
    }
}
