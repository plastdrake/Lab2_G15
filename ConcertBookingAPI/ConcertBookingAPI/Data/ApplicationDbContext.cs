namespace ConcertBookingAPI.Data
{
    using ConcertBookingAPI.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Microsoft.EntityFrameworkCore.Diagnostics;

    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships with delete behaviors
            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Concert)
                .WithMany(c => c.Performances)
                .HasForeignKey(p => p.ConcertId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Concerts to Performances

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Performance)
                .WithMany()
                .HasForeignKey(b => b.PerformanceId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete from Performances to Bookings
        }

        // Call SeedData to initialize data
        public static void Seed(BookingContext context)
        {
            SeedData.Initialize(context);  // Call the SeedData method to populate the DB
        }
    }
}
