﻿// <auto-generated />
using System;
using ConcertBookingAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConcertBookingAPI.Migrations
{
    [DbContext(typeof(BookingContext))]
    [Migration("20250108131727_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ConcertBookingAPI.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PerformanceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PerformanceId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("ConcertBookingAPI.Models.Concert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Concerts");
                });

            modelBuilder.Entity("ConcertBookingAPI.Models.Performance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ConcertId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConcertId");

                    b.ToTable("Performances");
                });

            modelBuilder.Entity("ConcertBookingAPI.Models.Booking", b =>
                {
                    b.HasOne("ConcertBookingAPI.Models.Performance", "Performance")
                        .WithMany()
                        .HasForeignKey("PerformanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Performance");
                });

            modelBuilder.Entity("ConcertBookingAPI.Models.Performance", b =>
                {
                    b.HasOne("ConcertBookingAPI.Models.Concert", "Concert")
                        .WithMany("Performances")
                        .HasForeignKey("ConcertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Concert");
                });

            modelBuilder.Entity("ConcertBookingAPI.Models.Concert", b =>
                {
                    b.Navigation("Performances");
                });
#pragma warning restore 612, 618
        }
    }
}
