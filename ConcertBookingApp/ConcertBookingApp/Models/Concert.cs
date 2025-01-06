using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Models
{
    public class Concert
    {
        public int Id { get; set; } // Primary Key
        public string Title { get; set; } // Concert title
        public string Description { get; set; } // Concert description

        // Navigation property for related Performances
        public ICollection<Performance> Performances { get; set; }
    }
}