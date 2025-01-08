using ConcertBookingApp.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using ConcertBookingApp.Services;

namespace ConcertBookingApp.ViewModels
{
    public class PerformanceViewModel : INotifyPropertyChanged
    {
        // Singleton instance
        public static PerformanceViewModel Instance { get; } = new PerformanceViewModel(new ApiService(new HttpClient()));

        public ObservableCollection<Performance> Performances { get; set; } = new ObservableCollection<Performance>();
        public ICommand BookPerformanceCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public int ConcertId { get; set; }

        private readonly IApiService _apiService;

        // Default constructor for singleton
        public PerformanceViewModel(IApiService apiService)
        {
            _apiService = apiService;

            // Initialize commands
            BookPerformanceCommand = new Command<int>(OnBookPerformance);
            BackCommand = new Command(OnBack);
        }

        // Constructor for specific concert
        public PerformanceViewModel(int concertId, IApiService apiService) : this(apiService)
        {
            ConcertId = concertId;

            // Fetch performances related to the concertId from the API
            LoadPerformancesAsync(concertId);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void LoadPerformancesAsync(int concertId)
        {
            var performances = await _apiService.GetPerformancesForConcertAsync(concertId);
            if (performances != null)
            {
                Performances.Clear();
                foreach (var performance in performances)
                {
                    Performances.Add(performance);
                }
            }
        }

        private async void OnBookPerformance(int performanceId)
        {
            // Retrieve the concertId from the current concert
            var concertId = ConcertId;

            // Navigate to the BookingPage, passing both concertId and performanceId
            await Shell.Current.GoToAsync($"///BookingPage?concertId={concertId}&performanceId={performanceId}");
        }

        private async void OnBack()
        {
            await Shell.Current.GoToAsync("//ConcertsPage");
        }
    }
}
