using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Configuration_SMS
    {
        [Key]
        public long Configuration_SMSID { get; set; }
        public string Vendor_Name { get; set; }
        public string URL { get; set; }
        public string APIKey { get; set; }
        public string Username { get;set; }
        public string Password { get; set; }
        public string Sender { get; set; }
        public bool IsActive { get; set; }
    }
}
