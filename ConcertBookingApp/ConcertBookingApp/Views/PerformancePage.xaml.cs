using ConcertBookingApp.Models;
using ConcertBookingApp.ViewModels;
using Microsoft.Maui.Controls;

namespace ConcertBookingApp.Views
{
    [QueryProperty(nameof(ConcertId), "concertId")]
    public partial class PerformancePage : ContentPage
    {
        public int ConcertId { get; set; }

        public PerformancePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = BindingContext as PerformanceViewModel;
            if (viewModel != null)
            {
                viewModel.ConcertId = ConcertId;
                viewModel.LoadPerformancesAsync(ConcertId); // Pass the ConcertId to the method
            }
        }
    }
}