using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class User
    {
        [Key]
        public long UserID { get; set; }

        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public string OTP { get; set; }
        public string Email { get; set; }
        public string PushNotificationToken { get; set; }
        public string DeviceID { get; set; }
        public bool IsActive { get; set; }


        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public PG PG { get; set; }
        public long? PGID { get; set; }

        public ProfileMaster ProfileMaster { get; set; }
        public long ProfileMasterID { get; set; }
    }
}
