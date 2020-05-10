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
    [Migration("20200312024545_Tenant")]
    partial class Tenant
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<long>("PGID");

                    b.HasKey("RoomSharingID");

                    b.HasIndex("PGID");

                    b.ToTable("RoomSharings");
                });

            modelBuilder.Entity("API.Models.Tenant", b =>
                {
                    b.Property<long>("TenantID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CheckInDate");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<decimal>("DepositAmount");

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsMoveOut");

                    b.Property<string>("MobileNo");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<DateTime?>("MoveOutDate");

                    b.Property<long>("PGID");

                    b.Property<decimal>("RentAmount");

                    b.Property<long>("RoomID");

                    b.HasKey("TenantID");

                    b.HasIndex("PGID");

                    b.HasIndex("RoomID");

                    b.ToTable("Tenants");
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
                        .HasForeignKey("PGID")
                        .OnDelete(DeleteBehavior.Cascade);
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
