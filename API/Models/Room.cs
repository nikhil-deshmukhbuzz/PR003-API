using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Room
    {
        [Key]
        public long RoomID { get; set; }
        public string RoomNo { get; set; }
        public decimal RentAmount { get; set; }
        public decimal DepositAmount { get; set; }
        public bool IsActive { get; set; }


        public RoomSharing RoomSharing { get; set; }
        public long RoomSharingID { get; set; }

        public PG PG { get; set; }
        public long PGID { get; set; }
    }
}
