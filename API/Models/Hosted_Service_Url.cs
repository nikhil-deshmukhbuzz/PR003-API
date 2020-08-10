using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Hosted_Service_Url
    {
        [Key]
        public long Hosted_Service_Url_ID { get; set; }
        public string ServiceName { get; set; }
        public string URL { get; set; }
        public string Enviorment { get; set; }
        public bool IsActive { get; set; }
    }
}
