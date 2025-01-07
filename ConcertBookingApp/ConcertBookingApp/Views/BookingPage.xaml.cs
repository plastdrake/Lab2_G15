using Microsoft.Maui.Controls;
using ConcertBookingApp.Models;
using ConcertBookingApp.ViewModels;
using System.Linq;

namespace ConcertBookingApp.Views
{
    [QueryProperty(nameof(ConcertId), "concertId")]
    [QueryProperty(nameof(PerformanceId), "performanceId")]
    public partial class BookingPage : ContentPage
    {
        public int ConcertId { get; set; }
        public int PerformanceId { get; set; }

        public BookingPage()
        {
            InitializeComponent();
            BindingContext = BookingViewModel.Instance;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Log to see the passed parameters
            Console.WriteLine($"ConcertId: {ConcertId}, PerformanceId: {PerformanceId}");
        }
    }
}
