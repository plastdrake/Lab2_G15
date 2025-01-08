using ConcertBookingAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConcertBookingAPI.Repositories
{
    public interface IConcertRepository : IRepository<Concert>
    {
        Task<IEnumerable<Concert>> GetConcertsWithPerformancesAsync();
    }
}
