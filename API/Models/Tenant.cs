using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Tenant
    {
        [Key]
        public long TenantID { get; set; }
        public string TenantNo { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public decimal RentAmount { get; set; }
        public decimal DepositAmount { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public bool IsCheckOut { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        public int NoOfDaysleft { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public Room Room { get; set; }
        public long RoomID { get; set; }

        public PG PG { get; set; }
        public long PGID { get; set; }
    }
}
