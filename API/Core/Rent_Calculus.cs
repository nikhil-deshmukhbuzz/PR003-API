using API.Contex;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

                foreach (var item in output)
                {
                    calculateRentPGID(item.PGID);
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

            foreach (var item in output)
            {
                calculateRentTenantID(item);
            }
        }

        private void calculateRentTenantID(Tenant tenant)
        {
            long month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

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
                    MonthID = DateTime.Now.Month,
                    Year = DateTime.Now.Year,
                    CreatedBy = null,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = null,
                    ModifiedOn = DateTime.Now
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
