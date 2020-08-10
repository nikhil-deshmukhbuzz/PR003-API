using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Monitor
    {
        [Key]
        public long MonitorID { get; set; }
        public string ServiceName { get; set; }
        public DateTime ExecutionTime { get; set; }
        public string Message { get; set; }
    }
}
