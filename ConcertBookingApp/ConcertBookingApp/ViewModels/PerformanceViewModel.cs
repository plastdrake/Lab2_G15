using ConcertBookingApp.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace ConcertBookingApp.ViewModels
{
    public class PerformanceViewModel : INotifyPropertyChanged
    {
        // Singleton instance
        public static PerformanceViewModel Instance { get; } = new PerformanceViewModel();

        public ObservableCollection<Performance> Performances { get; set; } = new ObservableCollection<Performance>();
        public ICommand BookPerformanceCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public int ConcertId { get; set; }

        // Default constructor for singleton
        public PerformanceViewModel()
        {
            // Initialize commands
            BookPerformanceCommand = new Command<int>(OnBookPerformance);
            BackCommand = new Command(OnBack);
        }

        // Constructor for specific concert
        public PerformanceViewModel(int concertId) : this()
        {
            ConcertId = concertId;

            // Fetch performances related to the concertId
            var concert = ConcertsViewModel.Instance.Concerts.FirstOrDefault(c => c.Id == concertId);

            if (concert != null)
            {
                Performances = new ObservableCollection<Performance>(concert.Performances);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
