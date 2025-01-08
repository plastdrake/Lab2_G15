using System.Threading.Tasks;

namespace ConcertBookingAPI.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IConcertRepository Concerts { get; }
        IPerformanceRepository Performances { get; }
        IBookingRepository Bookings { get; }
        Task<int> SaveChangesAsync();
    }
}
