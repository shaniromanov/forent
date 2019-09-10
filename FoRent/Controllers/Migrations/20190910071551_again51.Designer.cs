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
    [Migration("20190910071551_again51")]
    partial class again51
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

                    b.Property<int?>("ImageId");

                    b.Property<int?>("LocationId");

                    b.Property<int?>("PolicyId");

                    b.Property<decimal>("PriceAdult");

                    b.Property<decimal>("PriceChild");

                    b.Property<int?>("RenterId");

                    b.HasKey("Id");

                    b.HasIndex("AmenitiesId")
                        .IsUnique()
                        .HasFilter("[AmenitiesId] IS NOT NULL");

                    b.HasIndex("ImageId")
                        .IsUnique()
                        .HasFilter("[ImageId] IS NOT NULL");

                    b.HasIndex("LocationId")
                        .IsUnique()
                        .HasFilter("[LocationId] IS NOT NULL");

                    b.HasIndex("PolicyId")
                        .IsUnique()
                        .HasFilter("[PolicyId] IS NOT NULL");

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

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("HotWater");

                    b.Property<int>("NumOfPersons");

                    b.Property<double>("NumOfRooms");

                    b.Property<bool>("Parking");

                    b.Property<bool>("Plata");

                    b.Property<bool>("Wifi");

                    b.HasKey("Id");

                    b.ToTable("ApartmentAmenities");
                });

            modelBuilder.Entity("FoRent.Models.ApartmentAvailability", b =>
                {
                    b.Property<int>("AvailabilityId");

                    b.Property<int>("ApartmentId");

                    b.HasKey("AvailabilityId", "ApartmentId");

                    b.HasIndex("ApartmentId");

                    b.ToTable("ApartmentAvailability");
                });

            modelBuilder.Entity("FoRent.Models.Availability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("NotAvailable");

                    b.HasKey("Id");

                    b.ToTable("Availability");
                });

            modelBuilder.Entity("FoRent.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BedRoom");

                    b.Property<string>("DiningRoom");

                    b.Property<string>("Ketchen");

                    b.Property<string>("LivingRoom");

                    b.HasKey("Id");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("FoRent.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<double?>("x");

                    b.Property<double?>("y");

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

                    b.Property<int?>("OrdersId");

                    b.Property<int?>("PaymentId");

                    b.Property<int>("QuantityAdult");

                    b.Property<int>("QuantityChild");

                    b.Property<int?>("RenterId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.HasIndex("OrdersId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("RenterId");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("FoRent.Models.OrderPayment", b =>
                {
                    b.Property<int>("OrderPaymentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CC_CVV");

                    b.Property<string>("CC_Exp");

                    b.Property<string>("CC_Number");

                    b.HasKey("OrderPaymentId");

                    b.ToTable("OrderPayments");
                });

            modelBuilder.Entity("FoRent.Models.Policy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cancellations");

                    b.Property<DateTime>("Checkin");

                    b.Property<DateTime>("Checkout");

                    b.Property<bool>("Extrabeds");

                    b.HasKey("Id");

                    b.ToTable("Policy");
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
                        .WithOne("Apartment")
                        .HasForeignKey("FoRent.Models.Apartment", "AmenitiesId");

                    b.HasOne("FoRent.Models.Image", "Image")
                        .WithOne("Apartment")
                        .HasForeignKey("FoRent.Models.Apartment", "ImageId");

                    b.HasOne("FoRent.Models.Location", "Location")
                        .WithOne("Apartment")
                        .HasForeignKey("FoRent.Models.Apartment", "LocationId");

                    b.HasOne("FoRent.Models.Policy", "Policy")
                        .WithOne("Apartment")
                        .HasForeignKey("FoRent.Models.Apartment", "PolicyId");

                    b.HasOne("FoRent.Models.Renter", "Renter")
                        .WithMany("Apartments")
                        .HasForeignKey("RenterId");
                });

            modelBuilder.Entity("FoRent.Models.ApartmentAvailability", b =>
                {
                    b.HasOne("FoRent.Models.Apartment", "Apartment")
                        .WithMany("ApartmentAvailabilities")
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FoRent.Models.Availability", "Availability")
                        .WithMany("ApartmentAvailabilities")
                        .HasForeignKey("AvailabilityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FoRent.Models.Order", b =>
                {
                    b.HasOne("FoRent.Models.Apartment")
                        .WithMany("Orders")
                        .HasForeignKey("ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FoRent.Models.Order", "Orders")
                        .WithMany()
                        .HasForeignKey("OrdersId");

                    b.HasOne("FoRent.Models.OrderPayment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId");

                    b.HasOne("FoRent.Models.Renter", "Renter")
                        .WithMany()
                        .HasForeignKey("RenterId");

                    b.HasOne("FoRent.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
