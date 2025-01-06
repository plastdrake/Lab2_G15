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
        public ObservableCollection<Performance> Performances { get; set; } = new ObservableCollection<Performance>();
        public ICommand BookPerformanceCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public int ConcertId { get; set; }

        public PerformanceViewModel(int concertId)
        {
            ConcertId = concertId;

            // Fetch performances related to the concertId
            var concert = ConcertsViewModel.Instance.Concerts.FirstOrDefault(c => c.Id == concertId);

            if (concert != null)
            {
                Performances = new ObservableCollection<Performance>(concert.Performances);
            }

            // Initialize commands
            BookPerformanceCommand = new Command<int>(OnBookPerformance);
            BackCommand = new Command(OnBack);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnBookPerformance(int performanceId)
        {
            // Implement booking logic here
            Console.WriteLine($"Booking performance with ID: {performanceId}");
        }

        private async void OnBack()
        {
            // Navigate back to the previous page (ConcertsPage)
            await Shell.Current.GoToAsync("//ConcertsPage");
        }
    }
}
