using Microsoft.EntityFrameworkCore;
using API.Models;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace API.Contex
{
    public class DB003 : DbContext
    {
        private string env = "P";
        

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
        public DbSet<Error> Errors { get; set; }
        public DbSet<TransactionError> TransactionErrors { get; set; }
        public DbSet<Hosted_Service_Url> Hosted_Service_Urls { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Monitor> Monitors { get; set; }
        public DbSet<C_Transaction> C_Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string con = "";
            if (env == "P")
            {
                con = "Server=database-1.cuace85fwem4.ap-south-1.rds.amazonaws.com;Database=uat_DB003;User Id=admin; Password=buzz#2008;";
            }
            else if(env == "U")
            {
                con = "Server=database-1.cuace85fwem4.ap-south-1.rds.amazonaws.com;Database=uat_DB003;User Id=admin; Password=buzz#2008;";
            }
            else
            {
                con = "Server=NIKHIL\\SQLEXPRESS;Database=DB003;Trusted_Connection=True;";
            }
         
            optionsBuilder.UseSqlServer(con);
        }

        public static string GetConnectionString()
        {
            return Startup.ConnectionString;
        }
    }
}
