using Microsoft.Maui.Controls;

namespace ConcertBookingApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Optional: Set up additional logic or initialize ViewModel
            BindingContext = new MainPageViewModel();
        }
    }
}