﻿// <auto-generated />
using FoRent.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace FoRent.Migrations
{
    [DbContext(typeof(FoRentContext))]
    [Migration("20190710071910_again2")]
    partial class again2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FoRent.Models.Apartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AmenitiesId");

                    b.Property<int?>("LocationId");

                    b.Property<int?>("PolicyId");

                    b.Property<decimal>("PriceAdult");

                    b.Property<decimal>("PriceChild");

                    b.Property<int?>("RenterId");

                    b.HasKey("Id");

                    b.HasIndex("AmenitiesId");

                    b.HasIndex("LocationId");

                    b.HasIndex("PolicyId");

                    b.HasIndex("RenterId");

                    b.ToTable("Apartment");
                });

            modelBuilder.Entity("FoRent.Models.ApartmentAmenities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Accessibility");

                    b.Property<bool>("AirConditioning");

                    b.Property<bool>("Balcony");

                    b.Property<string>("Comment");

                    b.Property<string>("Description");

                    b.Property<bool>("HotWater");

                    b.Property<int>("NumOfPersons");

                    b.Property<double>("NumOfRooms");

                    b.Property<bool>("Parking");

                    b.Property<bool>("Plata");

                    b.Property<bool>("Wifi");

                    b.HasKey("Id");

                    b.ToTable("ApartmentAmenities");
                });

            modelBuilder.Entity("FoRent.Models.Availability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ApartmentId");

                    b.Property<DateTime>("NotAvailable");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.ToTable("Availability");
                });

            modelBuilder.Entity("FoRent.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adress");

                    b.Property<double>("Lat");

                    b.Property<double>("Lng");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("FoRent.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ApartmentId");

                    b.Property<DateTime>("CheckIn");

                    b.Property<DateTime>("CheckOut");

                    b.Property<int>("QuantityAdult");

                    b.Property<int>("QuantityChild");

                    b.Property<int?>("RenterId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RenterId");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("FoRent.Models.Policy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cancellations");

                    b.Property<string>("Checkin");

                    b.Property<string>("Checkout");

                    b.Property<string>("Creditcardsaccepted");

                    b.Property<string>("Extrabeds");

                    b.HasKey("Id");

                    b.ToTable("Policy");
                });

            modelBuilder.Entity("FoRent.Models.Reviews", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ApartmentId");

                    b.Property<string>("review");

                    b.Property<int>("stars");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("FoRent.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName");

                    b.Property<string>("Mail")
                        .IsRequired();

                    b.Property<string>("Phone");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.Property<string>("password")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("FoRent.Models.Renter", b =>
                {
                    b.HasBaseType("FoRent.Models.User");


                    b.ToTable("Renter");

                    b.HasDiscriminator().HasValue("Renter");
                });

            modelBuilder.Entity("FoRent.Models.Apartment", b =>
                {
                    b.HasOne("FoRent.Models.ApartmentAmenities", "Amenities")
                        .WithMany("Apartments")
                        .HasForeignKey("AmenitiesId");

                    b.HasOne("FoRent.Models.Location", "Location")
                        .WithMany("Apartments")
                        .HasForeignKey("LocationId");

                    b.HasOne("FoRent.Models.Policy", "Policy")
                        .WithMany("Apartments")
                        .HasForeignKey("PolicyId");

                    b.HasOne("FoRent.Models.Renter", "Renter")
                        .WithMany("Apartments")
                        .HasForeignKey("RenterId");
                });

            modelBuilder.Entity("FoRent.Models.Availability", b =>
                {
                    b.HasOne("FoRent.Models.Apartment", "Apartment")
                        .WithMany("Availability")
                        .HasForeignKey("ApartmentId");
                });

            modelBuilder.Entity("FoRent.Models.Order", b =>
                {
                    b.HasOne("FoRent.Models.Renter", "Renter")
                        .WithMany()
                        .HasForeignKey("RenterId");

                    b.HasOne("FoRent.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FoRent.Models.Reviews", b =>
                {
                    b.HasOne("FoRent.Models.Apartment", "Apartment")
                        .WithMany("Reviews")
                        .HasForeignKey("ApartmentId");
                });
#pragma warning restore 612, 618
        }
    }
}
