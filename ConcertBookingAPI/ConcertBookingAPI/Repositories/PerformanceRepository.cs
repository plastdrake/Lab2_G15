using ConcertBookingAPI.Data;
using ConcertBookingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertBookingAPI.Repositories
{
    public class PerformanceRepository : Repository<Performance>, IPerformanceRepository
    {
        public PerformanceRepository(BookingContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Performance>> GetPerformancesByConcertIdAsync(int concertId)
        {
            return await _dbSet
                .Include(p => p.Concert)
                .Where(p => p.ConcertId == concertId)
                .ToListAsync();
        }
    }
}
