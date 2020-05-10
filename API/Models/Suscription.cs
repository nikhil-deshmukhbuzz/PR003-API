using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{

    public class Suscription
    {
        [Key]
        public long SuscriptionID { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public int ValidityInMonth { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
