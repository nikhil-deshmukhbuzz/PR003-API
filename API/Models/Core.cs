using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Core
    {
    }

    public class UserManagement
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public string Status { get; set; }
        public User User { get; set; }
        public List<Tenant> Tenants { get; set; }
        public List<MenuMaster> ListOfMenuMaster { get; set; }
    }

    public class PGOwnerDashboard
    {
        public int NoOfBedAvailable { get; set; }
        public int NoOfDueRent { get; set; }
        public int NoOfOnNotice { get; set; }
        public int NoOfIssue { get; set; }

        public List<BedAvailable> BedAvailable { get; set; }
        public List<DueRent> DueRent { get; set; }
        public List<OnNotice> OnNotice { get; set; }
    }

    public class AdminDashboard
    {
        public int NoOfPGOwner { get; set; }
        public int NoOfActive { get; set; }
        public int NoOfInActive { get; set; }
        public int NoOfIssue { get; set; }

        public List<PG> PGs { get; set; }
        public List<PG> ActivePGs { get; set; }
        public List<PG> InActivePGs { get; set; }
 
    }


    public class BedAvailable
    {
        public string RoomNo { get; set; }
        public string SharingType { get; set; }
        public int Count { get; set; }
    }

    public class DueRent
    {
        public string RoomNo { get; set; }
        public string TenantName { get; set; }
        public decimal RentAmount { get; set; }
    }

    public class OnNotice
    {
        public string RoomNo { get; set; }
        public string TenantName { get; set; }
        public DateTime? CheckOutDate { get; set; }
    }

    public class SuscriptionRequest
    {
        public long PGID { get; set; }
        public string CustomerCode { get; set; }
        public string ProductCode { get; set; }
        public string InvoiceNumber { get; set; }
    }
    public class SuscriptionResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public DateTime? LastDateOfSuscription { get; set; }
    }
}
