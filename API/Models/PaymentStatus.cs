using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class PaymentStatus
    {
        [Key]
        public long PaymentStatusID { get; set; }
        public string Status { get; set; }
    }
}
