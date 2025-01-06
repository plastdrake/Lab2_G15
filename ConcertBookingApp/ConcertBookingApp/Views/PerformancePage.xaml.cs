using ConcertBookingApp.Models;
using ConcertBookingApp.ViewModels;
using Microsoft.Maui.Controls;
using System.Linq;

namespace ConcertBookingApp.Views
{
    [QueryProperty(nameof(ConcertId), "concertId")]
    public partial class PerformancePage : ContentPage
    {
        public int ConcertId { get; set; }

        public PerformancePage()
        {
            InitializeComponent();
            // Set the ViewModel here so that Performances is properly bound
            BindingContext = new PerformanceViewModel(ConcertId);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadPerformances();
        }

        private void LoadPerformances()
        {
            Console.WriteLine($"ConcertId: {ConcertId}");

            var concert = ConcertsViewModel.Instance.Concerts.FirstOrDefault(c => c.Id == ConcertId);

            if (concert != null)
            {
                Console.WriteLine($"Found concert: {concert.Title}");

                // Use the ViewModel's Performances collection to update the UI
                var viewModel = BindingContext as PerformanceViewModel;
                if (viewModel != null)
                {
                    viewModel.Performances.Clear();  // Clear existing list
                    foreach (var performance in concert.Performances)
                    {
                        Console.WriteLine($"Adding performance: {performance.Location}, {performance.DateTime}");
                        viewModel.Performances.Add(performance);
                    }

                    Console.WriteLine($"Total performances loaded: {viewModel.Performances.Count}");
                }
            }
            else
            {
                Console.WriteLine($"No concert found with ID: {ConcertId}");
            }
        }
    }
}
