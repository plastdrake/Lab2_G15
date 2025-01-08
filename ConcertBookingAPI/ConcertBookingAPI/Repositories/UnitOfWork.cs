using ConcertBookingAPI.Data;

namespace ConcertBookingAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingContext _context;

        public IConcertRepository Concerts { get; private set; }
        public IPerformanceRepository Performances { get; private set; }
        public IBookingRepository Bookings { get; private set; }

        public UnitOfWork(BookingContext context)
        {
            _context = context;
            Concerts = new ConcertRepository(_context);
            Performances = new PerformanceRepository(_context);
            Bookings = new BookingRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // Implement IDisposable to dispose the context when done
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
