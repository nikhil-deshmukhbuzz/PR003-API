using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class TransactionError
    {
        [Key]
        public long TransactionErrorID { get; set; }

        public PG PG { get; set; }
        public long PGID { get; set; }

        public Tenant Tenant { get; set; }
        public long? TenantID { get; set; }

        public string TransactionSessionID { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorType { get; set; }
        public string Message { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
