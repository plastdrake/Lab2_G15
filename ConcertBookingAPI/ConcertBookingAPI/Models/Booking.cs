﻿namespace ConcertBookingAPI.Models
{
    public class Booking
    {
        public int Id { get; set; } // Primary Key
        public string CustomerName { get; set; } // Name of the customer
        public string CustomerEmail { get; set; } // Email of the customer

        // Foreign Key and Navigation property for Performance
        public int PerformanceId { get; set; } // Foreign Key
        public Performance Performance { get; set; } // Navigation Property
    }
}
