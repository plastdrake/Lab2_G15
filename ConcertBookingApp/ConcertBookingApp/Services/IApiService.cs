using System.Collections.Generic;
using System.Threading.Tasks;
using ConcertBookingApp.Models;

namespace ConcertBookingApp.Services
{
    public interface IApiService
    {
        // Methods for Concerts
        Task<List<Concert>> GetConcertsAsync();
        Task<Concert> GetConcertByIdAsync(int id);

        // Methods for Performances
        Task<List<Performance>> GetPerformancesAsync();
        Task<Performance> GetPerformanceByIdAsync(int id);

        // Methods for Bookings
        Task<Booking> CreateBookingAsync(Booking booking);
    }
}
