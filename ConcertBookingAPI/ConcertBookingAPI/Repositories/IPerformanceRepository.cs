using ConcertBookingAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConcertBookingAPI.Repositories
{
    public interface IPerformanceRepository : IRepository<Performance>
    {
        Task<IEnumerable<Performance>> GetPerformancesByConcertIdAsync(int concertId);
    }
}
