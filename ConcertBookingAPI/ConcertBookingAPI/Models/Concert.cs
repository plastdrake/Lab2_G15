namespace ConcertBookingAPI.Models
{
    public class Concert
    {
        public int Id { get; set; } // Primary Key
        public required string Title { get; set; } // Concert title
        public required string Description { get; set; } // Concert description

        // Navigation property for related Performances
        public ICollection<Performance> Performances { get; set; }
    }
}
