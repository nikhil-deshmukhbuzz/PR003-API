﻿// <auto-generated />
using System;
using API.Contex;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(DB003))]
    [Migration("20200618112148_monitor")]
    partial class monitor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API.Models.Email", b =>
                {
                    b.Property<long>("EmailID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Attachement");

                    b.Property<byte[]>("Attachement1");

                    b.Property<string>("Attachement1Name");

                    b.Property<byte[]>("Attachement2");

                    b.Property<string>("Attachement2Name");

                    b.Property<string>("AttachementName");

                    b.Property<int>("Attempt");

                    b.Property<string>("Body");

                    b.Property<string>("CC");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("CustomerCode");

                    b.Property<string>("ErrorMessage");

                    b.Property<bool>("IsError");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("ProductCode");

                    b.Property<string>("Subject");

                    b.Property<string>("To");

                    b.HasKey("EmailID");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("API.Models.Error", b =>
                {
                    b.Property<long>("ErrorID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Message");

                    b.Property<string>("Type");

                    b.HasKey("ErrorID");

                    b.ToTable("Errors");
                });

            modelBuilder.Entity("API.Models.Hosted_Service_Url", b =>
                {
                    b.Property<long>("Hosted_Service_Url_ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Enviorment");

                    b.Property<bool>("IsActive");

                    b.Property<string>("ServiceName");

                    b.Property<string>("URL");

                    b.HasKey("Hosted_Service_Url_ID");

                    b.ToTable("Hosted_Service_Urls");
                });

            modelBuilder.Entity("API.Models.MenuMaster", b =>
                {
                    b.Property<long>("MenuID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsMobile");

                    b.Property<string>("MenuName");

                    b.Property<long>("ParentMenuID");

                    b.Property<int>("SequenceNo");

                    b.Property<string>("Url");

                    b.HasKey("MenuID");

                    b.ToTable("MenuMasters");
                });

            modelBuilder.Entity("API.Models.MenuProfileLink", b =>
                {
                    b.Property<long>("MenuProfileLinkID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("MenuID");

                    b.Property<long>("ProfileID");

                    b.HasKey("MenuProfileLinkID");

                    b.HasIndex("MenuID");

                    b.HasIndex("ProfileID");

                    b.ToTable("MenuProfileLinks");
                });

            modelBuilder.Entity("API.Models.Monitor", b =>
                {
                    b.Property<long>("MonitorID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ExecutionTime");

                    b.Property<string>("Message");

                    b.Property<string>("ServiceName");

                    b.HasKey("MonitorID");

                    b.ToTable("Monitors");
                });

            modelBuilder.Entity("API.Models.Month", b =>
                {
                    b.Property<long>("MonthID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MonthName");

                    b.HasKey("MonthID");

                    b.ToTable("Months");
                });

            modelBuilder.Entity("API.Models.PaymentStatus", b =>
                {
                    b.Property<long>("PaymentStatusID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Status");

                    b.HasKey("PaymentStatusID");

                    b.ToTable("PaymentStatuss");
                });

            modelBuilder.Entity("API.Models.PG", b =>
                {
                    b.Property<long>("PGID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("Email");

                    b.Property<bool>("IsActive");

                    b.Property<string>("MobileNo");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<string>("OwnerName");

                    b.Property<string>("PGNo");

                    b.HasKey("PGID");

                    b.ToTable("PGs");
                });

            modelBuilder.Entity("API.Models.ProfileMaster", b =>
                {
                    b.Property<long>("ProfileID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProfileName");

                    b.HasKey("ProfileID");

                    b.ToTable("ProfileMasters");
                });

            modelBuilder.Entity("API.Models.Registration", b =>
                {
                    b.Property<long>("RegistrationID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsRegister");

                    b.Property<string>("MobileNo");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("PGName");

                    b.Property<string>("RegistrationNo");

                    b.HasKey("RegistrationID");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("API.Models.Rent", b =>
                {
                    b.Property<long>("RentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("FullName");

                    b.Property<string>("InvoiceNumber");

                    b.Property<long?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<long>("MonthID");

                    b.Property<long>("PGID");

                    b.Property<long>("PaymentStatusID");

                    b.Property<decimal>("RentAmount");

                    b.Property<string>("RoomNo");

                    b.Property<long>("TenantID");

                    b.Property<decimal>("TotalAmount");

                    b.Property<int>("Year");

                    b.HasKey("RentID");

                    b.HasIndex("MonthID");

                    b.HasIndex("PGID");

                    b.HasIndex("PaymentStatusID");

                    b.HasIndex("TenantID");

                    b.ToTable("Rents");
                });

            modelBuilder.Entity("API.Models.Room", b =>
                {
                    b.Property<long>("RoomID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("DepositAmount");

                    b.Property<bool>("IsActive");

                    b.Property<long>("PGID");

                    b.Property<decimal>("RentAmount");

                    b.Property<string>("RoomNo");

                    b.Property<long>("RoomSharingID");

                    b.HasKey("RoomID");

                    b.HasIndex("PGID");

                    b.HasIndex("RoomSharingID");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("API.Models.RoomSharing", b =>
                {
                    b.Property<long>("RoomSharingID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<int>("NoOfBed");

                    b.Property<long?>("PGID");

                    b.HasKey("RoomSharingID");

                    b.HasIndex("PGID");

                    b.ToTable("RoomSharings");
                });

            modelBuilder.Entity("API.Models.Suscription", b =>
                {
                    b.Property<long>("SuscriptionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<string>("SerialNumber");

                    b.Property<int>("ValidityInDays");

                    b.HasKey("SuscriptionID");

                    b.ToTable("Suscriptions");
                });

            modelBuilder.Entity("API.Models.Tenant", b =>
                {
                    b.Property<long>("TenantID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CheckInDate");

                    b.Property<DateTime?>("CheckOutDate");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<decimal>("DepositAmount");

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsCheckOut");

                    b.Property<string>("MobileNo");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<long>("PGID");

                    b.Property<decimal>("RentAmount");

                    b.Property<long>("RoomID");

                    b.HasKey("TenantID");

                    b.HasIndex("PGID");

                    b.HasIndex("RoomID");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("API.Models.Transaction", b =>
                {
                    b.Property<long>("TransactionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("CustomerCode");

                    b.Property<string>("Email");

                    b.Property<string>("MobileNo");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("OrderID");

                    b.Property<string>("PayeeName");

                    b.Property<string>("PaymentID");

                    b.Property<string>("PaymentStatus");

                    b.Property<string>("PaymentType");

                    b.Property<string>("ProductCode");

                    b.Property<string>("Signature");

                    b.Property<string>("SuscriptionNumber");

                    b.Property<DateTime>("TransactionDate");

                    b.Property<string>("TransactionStep");

                    b.Property<int?>("ValidityInMonth");

                    b.HasKey("TransactionID");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("API.Models.TransactionError", b =>
                {
                    b.Property<long>("TransactionErrorID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("ErrorCode");

                    b.Property<string>("ErrorType");

                    b.Property<string>("Message");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<long>("PGID");

                    b.Property<long?>("TenantID");

                    b.Property<string>("TransactionSessionID");

                    b.HasKey("TransactionErrorID");

                    b.HasIndex("PGID");

                    b.HasIndex("TenantID");

                    b.ToTable("TransactionErrors");
                });

            modelBuilder.Entity("API.Models.User", b =>
                {
                    b.Property<long>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("Email");

                    b.Property<bool>("IsActive");

                    b.Property<string>("MobileNo");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<string>("OTP");

                    b.Property<long?>("PGID");

                    b.Property<string>("Password");

                    b.Property<long>("ProfileMasterID");

                    b.Property<string>("Username");

                    b.HasKey("UserID");

                    b.HasIndex("PGID");

                    b.HasIndex("ProfileMasterID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Models.MenuProfileLink", b =>
                {
                    b.HasOne("API.Models.MenuMaster", "MenuMaster")
                        .WithMany()
                        .HasForeignKey("MenuID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.ProfileMaster", "ProfileMaster")
                        .WithMany()
                        .HasForeignKey("ProfileID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.Rent", b =>
                {
                    b.HasOne("API.Models.Month", "Month")
                        .WithMany()
                        .HasForeignKey("MonthID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.PG", "PG")
                        .WithMany()
                        .HasForeignKey("PGID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.PaymentStatus", "PaymentStatus")
                        .WithMany()
                        .HasForeignKey("PaymentStatusID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.Room", b =>
                {
                    b.HasOne("API.Models.PG", "PG")
                        .WithMany()
                        .HasForeignKey("PGID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.RoomSharing", "RoomSharing")
                        .WithMany()
                        .HasForeignKey("RoomSharingID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.RoomSharing", b =>
                {
                    b.HasOne("API.Models.PG", "PG")
                        .WithMany()
                        .HasForeignKey("PGID");
                });

            modelBuilder.Entity("API.Models.Tenant", b =>
                {
                    b.HasOne("API.Models.PG", "PG")
                        .WithMany()
                        .HasForeignKey("PGID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.TransactionError", b =>
                {
                    b.HasOne("API.Models.PG", "PG")
                        .WithMany()
                        .HasForeignKey("PGID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantID");
                });

            modelBuilder.Entity("API.Models.User", b =>
                {
                    b.HasOne("API.Models.PG", "PG")
                        .WithMany()
                        .HasForeignKey("PGID");

                    b.HasOne("API.Models.ProfileMaster", "ProfileMaster")
                        .WithMany()
                        .HasForeignKey("ProfileMasterID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
