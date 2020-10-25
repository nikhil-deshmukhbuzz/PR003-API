using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
    public class PGMaster
    {
        public List<Room> Rooms { get; set; }
        public List<RoomSharing> RoomSharings { get; set; }
        public List<Tenant> Tenants { get; set; }
        public List<Rent> Rents { get; set; }
        public List<Tenant> NoticePeriods { get; set; }
        public List<PaymentStatus> PaymentStatus { get; set; }
        public PGOwnerDashboard PGOwnerDashboard { get; set; }
    }
}
