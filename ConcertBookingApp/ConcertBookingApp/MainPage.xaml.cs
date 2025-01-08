using Microsoft.Maui.Controls;
using ConcertBookingApp.ViewModels;

namespace ConcertBookingApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
    }
}
