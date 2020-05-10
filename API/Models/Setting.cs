using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Setting
    {
        [Key]
        public long SettingID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
