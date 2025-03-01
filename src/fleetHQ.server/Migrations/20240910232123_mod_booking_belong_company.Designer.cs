﻿// <auto-generated />
using System;
using System.Collections.Generic;
using FleetHQ.Server.Repository;
using FleetHQ.Server.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace fleetHQ.server.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20240910232123_mod_booking_belong_company")]
    partial class mod_booking_belong_company
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FleetHQ.Server.Repository.Models.BookingModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("CustomerContact")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CustomerDestination")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CustomerLocation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("DriverId")
                        .HasColumnType("uuid");

                    b.Property<int>("PassengerCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("VehicleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("DriverId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("FleetHQ.Server.Repository.Models.CompanyModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("FleetHQ.Server.Repository.Models.DriverModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("VehicleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("VehicleId")
                        .IsUnique();

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("FleetHQ.Server.Repository.Models.RoleModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<Dictionary<string, Permission>>>("Permissions")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("FleetHQ.Server.Repository.Models.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OnBoarding")
                        .HasColumnType("integer");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FleetHQ.Server.Repository.Models.VehicleModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Seats")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Year")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("FleetHQ.Server.Repository.Models.BookingModel", b =>
                {
                    b.HasOne("FleetHQ.Server.Repository.Models.CompanyModel", "Company")
                        .WithMany("Bookings")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FleetHQ.Server.Repository.Models.DriverModel", "Driver")
                        .WithMany("Bookings")
                        .HasForeignKey("DriverId");

                    b.HasOne("FleetHQ.Server.Repository.Models.VehicleModel", "Vehicle")
                        .WithMany("Bookings")
                        .HasForeignKey("VehicleId");

                    b.Navigation("Company");

                    b.Navigation("Driver");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("FleetHQ.Server.Repository.Models.DriverModel", b =>
                {
                    b.HasOne("FleetHQ.Server.Repository.Models.CompanyModel", "Company")
                        .WithMany("Drivers")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FleetHQ.Server.Repository.Models.VehicleModel", "Vehicle")
                        .WithOne("Driver")
                        .HasForeignKey("FleetHQ.Server.Repository.Models.DriverModel", "VehicleId");

                    b.Navigation("Company");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("FleetHQ.Server.Repository.Models.UserModel", b =>
                {
                    b.HasOne("FleetHQ.Server.Repository.Models.CompanyModel", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId");

                    b.HasOne("FleetHQ.Server.Repository.Models.RoleModel", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("FleetHQ.Server.Repository.Models.VehicleModel", b =>
                {
                    b.HasOne("FleetHQ.Server.Repository.Models.CompanyModel", "Company")
                        .WithMany("Vehicles")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("FleetHQ.Server.Repository.Models.CompanyModel", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Drivers");

                    b.Navigation("Users");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("FleetHQ.Server.Repository.Models.DriverModel", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("FleetHQ.Server.Repository.Models.VehicleModel", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Driver");
                });
#pragma warning restore 612, 618
        }
    }
}
