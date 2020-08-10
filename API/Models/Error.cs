using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Error
    {
      [Key]
      public long ErrorID { get; set; }
      public string Code { get; set; }
      public string Type { get; set; }
      public string Message { get; set; }
    }
}
