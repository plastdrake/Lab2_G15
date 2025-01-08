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
            // Ensure that we add the correct headers, for example for JSON content
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Get all concerts
        public async Task<List<Concert>> GetConcertsAsync()
        {
            var response = await _httpClient.GetAsync("api/concerts");
            response.EnsureSuccessStatusCode();  // Throws exception if status code is not successful
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

        // Create a booking
        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            var response = await _httpClient.PostAsJsonAsync("api/bookings", booking);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Booking>(content);
        }

        // Get all performances
        public async Task<List<Performance>> GetPerformancesAsync()
        {
            var response = await _httpClient.GetAsync("api/performances");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Performance>>(content);
        }

        // Get a specific performance by ID
        public async Task<Performance> GetPerformanceByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/performances/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Performance>(content);
        }
    }
}
