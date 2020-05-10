using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class RoomSharing
    {
        [Key]
        public long RoomSharingID { get; set; }
        public string Name { get; set; }
        public int NoOfBed { get; set; }
        public bool IsActive { get; set; }

        public PG PG { get; set; }
        public long? PGID { get; set; }
    }
}
