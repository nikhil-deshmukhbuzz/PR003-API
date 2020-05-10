using Microsoft.EntityFrameworkCore;
using API.Models;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace API.Contex
{
    public class DB003 : DbContext
    {
        public IConfiguration Configuration { get; }

        public DB003()
        {
        }

        public DB003(DbContextOptions<DB003> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<Registration> Registrations { get; set; }
        public DbSet<PG> PGs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProfileMaster> ProfileMasters { get; set; }
        public DbSet<MenuMaster> MenuMasters { get; set; }
        public DbSet<MenuProfileLink> MenuProfileLinks { get; set; }
        public DbSet<RoomSharing> RoomSharings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<PaymentStatus> PaymentStatuss { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Suscription> Suscriptions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // string con = "Server=twittinest.c3gce2a7fa8f.ap-south-1.rds.amazonaws.com;Database=uat_DB003;User Id=admin; Password=buzz#2008;";//GetConnectionString();
            string con = "Server=NIKHIL\\SQLEXPRESS;Database=DB003;Trusted_Connection=True;";//GetConnectionString();
            optionsBuilder.UseSqlServer(con);
        }

        public static string GetConnectionString()
        {
            return Startup.ConnectionString;
        }
    }
}
