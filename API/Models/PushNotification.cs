using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class PushNotification
    {
        [Key]
        public long PushNotificationID { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Token { get; set; }
        public string DeviceID { get; set; }
        public string Titile { get; set; }
        public string Body { get; set; }
        public bool Push { get; set; }
        public bool Visible { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public PG PG { get; set; }
        public long? PGID { get; set; }

        public Tenant Tenant { get; set; }
        public long? TenantID { get; set; }
    }
}
