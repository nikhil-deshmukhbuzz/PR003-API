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
    [Migration("20200302030013_User")]
    partial class User
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<string>("Username");

                    b.HasKey("UserID");

                    b.HasIndex("PGID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Models.User", b =>
                {
                    b.HasOne("API.Models.PG", "PG")
                        .WithMany()
                        .HasForeignKey("PGID");
                });
#pragma warning restore 612, 618
        }
    }
}
