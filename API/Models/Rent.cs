using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Rent
    {
        [Key]
        public long RentID { get; set; }
        public string InvoiceNumber { get; set; }
        public string FullName { get; set; }
        public string RoomNo { get; set; }
        public decimal RentAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public long PGID { get; set; }
        public PG PG { get; set; }

        public long TenantID { get; set; }
        public Tenant Tenant { get; set; }

        public long PaymentStatusID { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public int Year { get; set; }

        public long MonthID { get; set; }
        public Month Month { get; set; }

        [NotMapped]
        public string MonthName { get; set; }

        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }

    }
}
