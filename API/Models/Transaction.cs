using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Transaction
    {
            [Key]
            public long TransactionID { get; set; }

            public string ProductCode { get; set; }
            public string CustomerCode { get; set; }

            public string PaymentID { get; set; }
            public string OrderID { get; set; }
            public string Signature { get; set; }
            public decimal Amount { get; set; }
            public string PayeeName { get; set; }
            public string MobileNo { get; set; }
            public string Email { get; set; }
            public string PaymentStatus { get; set; }
            public string PaymentType { get; set; }
            public string TransactionStep { get; set; }
            public string SuscriptionNumber { get; set; }
            public int? ValidityInMonth { get; set; }

            public DateTime TransactionDate { get; set; }
            public DateTime? CreatedOn { get; set; }
            public DateTime? ModifiedOn { get; set; }
    }
}
