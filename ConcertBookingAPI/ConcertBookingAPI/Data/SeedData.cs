namespace ConcertBookingAPI.Data
{
    using ConcertBookingAPI.Models;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class SeedData
    {
        public static void Initialize(BookingContext context)
        {
            // Check if the database is already seeded to avoid reseeding every time
            if (context.Concerts.Any() || context.Performances.Any())
                return;

            // Seed data for Concerts
            var concertA = new Concert { Title = "Concert A", Description = "Description A" };
            var concertB = new Concert { Title = "Concert B", Description = "Description B" };

            context.Concerts.AddRange(concertA, concertB);
            context.SaveChanges();

            // Seed Performances
            var performance1 = new Performance { ConcertId = concertA.Id, DateTime = DateTime.Now.AddDays(1), Location = "Venue 1" , Concert = concertA};
            var performance2 = new Performance { ConcertId = concertB.Id, DateTime = DateTime.Now.AddDays(2), Location = "Venue 2" , Concert = concertB};

            context.Performances.AddRange(performance1, performance2);
            context.SaveChanges();
        }
    }
}
