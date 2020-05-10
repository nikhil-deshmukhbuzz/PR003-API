using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ProfileMaster
    {
        [Key]
        public long ProfileID { get; set; }
        public string ProfileName { get; set; }
    }
}
