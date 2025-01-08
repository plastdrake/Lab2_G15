using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ConcertBookingApp.Models;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace ConcertBookingApp.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Get all concerts
        public async Task<List<Concert>> GetConcertsAsync()
        {
            var response = await _httpClient.GetAsync("api/concerts");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Concert>>(content);
        }

        // Get a specific concert by ID
        public async Task<Concert> GetConcertByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/concerts/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Concert>(content);
        }

        // Get performances by concert ID (new)
        public async Task<List<Performance>> GetPerformancesForConcertAsync(int concertId)
        {
            var response = await _httpClient.GetAsync($"api/performances/ByConcert/{concertId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Performance>>(content);
        }

        // Create a booking
        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            var response = await _httpClient.PostAsJsonAsync("api/bookings", booking);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Booking>(content);
        }
    }

}
