using ConcertBookingApp.ViewModels;
using Microsoft.Maui.Controls;

namespace ConcertBookingApp.Views
{
    public partial class ConcertsPage : ContentPage
    {
        private readonly ConcertsViewModel _viewModel;

        public ConcertsPage(ConcertsViewModel viewModel)
        {
            InitializeComponent();

            // Assign the injected ViewModel to the local variable
            _viewModel = viewModel;

            // Set the BindingContext to the ViewModel
            BindingContext = _viewModel;
        }
    }
}
