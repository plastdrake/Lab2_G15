namespace ConcertBookingAPI.Models
{
    public class Performance
    {
        public int Id { get; set; } // Primary Key
        public DateTime DateTime { get; set; } // Performance date and time
        public required string Location { get; set; } // Performance location

        // Foreign Key and Navigation property for Concert
        public int ConcertId { get; set; } // Foreign Key
        public required Concert Concert { get; set; } // Navigation Property
    }
}
