using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace ConcertBookingApp
{
    public class MainPageViewModel
    {
        public ICommand NavigateToConcertsCommand { get; }
        public ICommand NavigateToBookingsCommand { get; }

        public MainPageViewModel()
        {
            NavigateToConcertsCommand = new Command(OnNavigateToConcerts);
            NavigateToBookingsCommand = new Command(OnNavigateToBookings);
        }

        private async void OnNavigateToConcerts()
        {
            await Shell.Current.GoToAsync("//ConcertsPage");
        }

        private async void OnNavigateToBookings()
        {
            await Shell.Current.GoToAsync("//BookingsPage");
        }
    }
}
