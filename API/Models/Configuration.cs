using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Configuration
    {
        [Key]
        public long ConfigurationID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SmtpServer { get; set; }
        public string PushNotify_Server_Key { get; set; }
        public string PushNotify_Sender_Key { get; set; }
        public string PushNotify_Title { get; set; }
        public int? Port { get; set; }
        public bool IsActive { get; set; }
    }
}
