using ConcertBookingApp.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace ConcertBookingApp.ViewModels
{
    public class ConcertsViewModel
    {
        // Singleton Instance
        public static ConcertsViewModel Instance { get; } = new ConcertsViewModel();

        // ObservableCollection for Concerts
        public ObservableCollection<Concert> Concerts { get; set; }

        // Command for navigating to Performance page
        public ICommand ViewPerformanceCommand { get; set; }

        // Command for back navigation
        public ICommand BackCommand { get; set; }

        public ConcertsViewModel()
        {
            // Initialize the Concerts collection with some sample data
            Concerts = new ObservableCollection<Concert>
            {
                new Concert
                {
                    Id = 1,
                    Title = "Rock Fest",
                    Performances = new ObservableCollection<Performance>
                    {
                        new Performance { Id = 1, DateTime = DateTime.Now.AddDays(1), Location = "Arena 1" },
                        new Performance { Id = 2, DateTime = DateTime.Now.AddDays(2), Location = "Arena 2" }
                    }
                },
                new Concert
                {
                    Id = 2,
                    Title = "Jazz Night",
                    Performances = new ObservableCollection<Performance>
                    {
                        new Performance { Id = 3, DateTime = DateTime.Now.AddDays(3), Location = "Jazz Arena" }
                    }
                }
            };

            // Initialize commands
            ViewPerformanceCommand = new Command<int>(OnViewPerformance);
            BackCommand = new Command(OnBack);
        }

        // Method to navigate to the PerformancePage
        private async void OnViewPerformance(int id)
        {
            // Debug
            Console.WriteLine("Navigating to PerformancePage...");
            Console.WriteLine($"Concert ID: {id}");

            await Shell.Current.GoToAsync($"///PerformancePage?concertId={id}");
        }


        // Method to navigate back to the Home page
        private async void OnBack()
        {
            // Navigate back to the Home page
            await Shell.Current.GoToAsync("//MainPage");
        }

    }
}
