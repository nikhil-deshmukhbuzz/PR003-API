using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ExceptionText
    {
        [Key]
        public long ExceptionTextID { get; set; }
        public string FunctionName { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
