using ConcertBookingApp.Models;
using ConcertBookingApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace ConcertBookingApp.ViewModels
{
    public class ConcertsViewModel
    {
        // Singleton Instance
        public static ConcertsViewModel Instance { get; private set; }

        // ObservableCollection for Concerts
        public ObservableCollection<Concert> Concerts { get; set; }

        // Command for navigating to Performance page
        public ICommand ViewPerformanceCommand { get; set; }

        // Command for back navigation
        public ICommand BackCommand { get; set; }

        private readonly IApiService _apiService;

        // Constructor with IApiService dependency injection
        public ConcertsViewModel(IApiService apiService)
        {
            _apiService = apiService;  // Save the injected IApiService instance

            // Initialize commands
            ViewPerformanceCommand = new Command<int>(OnViewPerformance);
            BackCommand = new Command(OnBack);

            // Initialize the Concerts collection (this will be updated with API calls)
            Concerts = new ObservableCollection<Concert>();

            // Fetch concerts from the API
            LoadConcertsAsync();
        }

        // Static method to initialize the singleton instance
        public static void Initialize(IApiService apiService)
        {
            if (Instance == null)
            {
                Instance = new ConcertsViewModel(apiService);
            }
        }

        // Method to load concerts from the API
        private async void LoadConcertsAsync()
        {
            var concerts = await _apiService.GetConcertsAsync();
            if (concerts != null)
            {
                foreach (var concert in concerts)
                {
                    Concerts.Add(concert);
                }
            }
        }

        // Method to navigate to the PerformancePage
        private async void OnViewPerformance(int id)
        {
            await Shell.Current.GoToAsync($"///PerformancePage?concertId={id}");
        }

        // Method to navigate back to the Home page
        private async void OnBack()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
