using ConcertBookingAPI.Data;
using ConcertBookingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConcertBookingAPI.Repositories
{
    public class ConcertRepository : Repository<Concert>, IConcertRepository
    {
        public ConcertRepository(BookingContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Concert>> GetConcertsWithPerformancesAsync()
        {
            return await _dbSet.Include(c => c.Performances).ToListAsync();
        }
    }
}
