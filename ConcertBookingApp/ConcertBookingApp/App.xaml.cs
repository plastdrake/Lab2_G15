using ConcertBookingApp.Services;
using ConcertBookingApp.ViewModels;

namespace ConcertBookingApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ConcertsViewModel.Initialize(new ApiService(new HttpClient()));

            MainPage = new AppShell();
        }
    }
}
