using ConcertBookingApp.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;
using ConcertBookingApp.Services;

namespace ConcertBookingApp.Views
{
    public partial class ConcertsPage : ContentPage
    {
        private ConcertsViewModel _viewModel;

        public ConcertsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var mauiContext = App.Current?.Handler?.MauiContext;
            if (mauiContext == null)
            {
                throw new InvalidOperationException("MauiContext is null.");
            }

            _viewModel = mauiContext.Services.GetService<ConcertsViewModel>() ?? throw new InvalidOperationException("ConcertsViewModel could not be resolved.");

            if (_viewModel == null)
            {
                Console.WriteLine("ConcertsViewModel is null!");
            }
            else
            {
                Console.WriteLine("ConcertsViewModel is successfully injected.");
            }

            // Set the BindingContext to the resolved view model
            BindingContext = _viewModel;
        }
    }
}
