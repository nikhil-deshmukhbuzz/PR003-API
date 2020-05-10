using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Month
    {
        [Key]
        public long MonthID { get; set; }
        public string MonthName { get; set; }
    }
}
