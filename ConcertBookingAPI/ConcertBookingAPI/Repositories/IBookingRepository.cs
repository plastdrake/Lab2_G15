using ConcertBookingAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConcertBookingAPI.Repositories
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetBookingsByPerformanceIdAsync(int performanceId);
    }
}
