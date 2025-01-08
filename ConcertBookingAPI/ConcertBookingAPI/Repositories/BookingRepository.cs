using ConcertBookingAPI.Data;
using ConcertBookingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertBookingAPI.Repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(BookingContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Booking>> GetBookingsByPerformanceIdAsync(int performanceId)
        {
            return await _dbSet
                .Include(b => b.Performance)
                .Where(b => b.PerformanceId == performanceId)
                .ToListAsync();
        }
    }
}
