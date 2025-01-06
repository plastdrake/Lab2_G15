using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace ConcertBookingApp
{
    public class MainPageViewModel
    {
        public ICommand NavigateToConcertsCommand { get; }
        public ICommand NavigateToBookingsCommand { get; }
        public ICommand NavigateToAboutCommand { get; }

        public MainPageViewModel()
        {
            NavigateToConcertsCommand = new Command(OnNavigateToConcerts);
            NavigateToBookingsCommand = new Command(OnNavigateToBookings);
            NavigateToAboutCommand = new Command(OnNavigateToAbout);
        }

        private async void OnNavigateToConcerts()
        {
            await Shell.Current.GoToAsync("//ConcertsPage");
        }

        private async void OnNavigateToBookings()
        {
            await Shell.Current.GoToAsync("//BookingsPage");
        }

        private async void OnNavigateToAbout()
        {
            await Shell.Current.GoToAsync("//AboutPage");
        }
    }
}
