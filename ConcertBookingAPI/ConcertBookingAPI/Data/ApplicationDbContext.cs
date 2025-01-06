namespace ConcertBookingAPI.Data
{
    using ConcertBookingAPI.Models;
    using Microsoft.EntityFrameworkCore;

    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {
        }

        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships
            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Concert)
                .WithMany(c => c.Performances)
                .HasForeignKey(p => p.ConcertId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Performance)
                .WithMany()
                .HasForeignKey(b => b.PerformanceId);

            // Seed data for Concerts
            modelBuilder.Entity<Concert>().HasData(
                new Concert { Id = 1, Title = "Concert A", Description = "Description A" },
                new Concert { Id = 2, Title = "Concert B", Description = "Description B" }
            );

            // Seed Performances or Bookings
            modelBuilder.Entity<Performance>().HasData(
                new Performance { Id = 1, ConcertId = 1, DateTime = DateTime.Now.AddDays(1), Location = "Venue 1" },
                new Performance { Id = 2, ConcertId = 2, DateTime = DateTime.Now.AddDays(2), Location = "Venue 2" }
            );
        }
    }
}
