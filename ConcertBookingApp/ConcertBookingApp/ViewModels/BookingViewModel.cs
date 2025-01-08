using ConcertBookingApp.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Threading.Tasks;
using ConcertBookingApp.Services;

namespace ConcertBookingApp.ViewModels
{
    public class BookingViewModel : INotifyPropertyChanged
    {
        // Singleton instance for the ViewModel
        public static BookingViewModel Instance { get; } = new BookingViewModel(new ApiService(new HttpClient()));

        // Implementing the INotifyPropertyChanged interface
        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<Performance> _performances = new ObservableCollection<Performance>();
        public ObservableCollection<Performance> Performances
        {
            get => _performances;
            set
            {
                if (_performances != value)
                {
                    _performances = value;
                    OnPropertyChanged(nameof(Performances)); // Notify that Performances has changed
                }
            }
        }

        private ObservableCollection<Concert> _concerts = new ObservableCollection<Concert>();
        public ObservableCollection<Concert> Concerts
        {
            get => _concerts;
            set
            {
                if (_concerts != value)
                {
                    _concerts = value;
                    OnPropertyChanged(nameof(Concerts)); // Notify that Concerts has changed
                }
            }
        }

        private ObservableCollection<Booking> _bookings = new ObservableCollection<Booking>();
        public ObservableCollection<Booking> Bookings
        {
            get => _bookings;
            set
            {
                if (_bookings != value)
                {
                    _bookings = value;
                    OnPropertyChanged(nameof(Bookings)); // Notify that Bookings has changed
                }
            }
        }

        private string _newCustomerName = string.Empty;
        public string NewCustomerName
        {
            get => _newCustomerName;
            set
            {
                if (_newCustomerName != value)
                {
                    _newCustomerName = value;
                    OnPropertyChanged(nameof(NewCustomerName)); // Notify that NewCustomerName has changed
                }
            }
        }

        private string _newCustomerEmail = string.Empty;
        public string NewCustomerEmail
        {
            get => _newCustomerEmail;
            set
            {
                if (_newCustomerEmail != value)
                {
                    _newCustomerEmail = value;
                    OnPropertyChanged(nameof(NewCustomerEmail)); // Notify that NewCustomerEmail has changed
                }
            }
        }

        private Performance _selectedPerformance = null!;
        public Performance SelectedPerformance
        {
            get => _selectedPerformance;
            set
            {
                if (_selectedPerformance != value)
                {
                    _selectedPerformance = value;
                    OnPropertyChanged(nameof(SelectedPerformance)); // Notify that SelectedPerformance has changed
                }
            }
        }

        private Concert _selectedConcert = null!;
        public Concert SelectedConcert
        {
            get => _selectedConcert;
            set
            {
                if (_selectedConcert != value)
                {
                    _selectedConcert = value;
                    OnPropertyChanged(nameof(SelectedConcert)); // Notify that SelectedConcert has changed
                    UpdatePerformances(); // Update Performances when the concert is changed
                }
            }
        }

        // Commands for adding, deleting, and navigating
        public ICommand AddBookingCommand { get; set; }
        public ICommand DeleteBookingCommand { get; set; }
        public ICommand BackCommand { get; set; }

        // Injecting IApiService
        private readonly IApiService _apiService;

        public BookingViewModel(IApiService apiService)
        {
            _apiService = apiService;

            // Initialize commands
            AddBookingCommand = new Command(OnAddBooking);
            DeleteBookingCommand = new Command<Booking>(OnDeleteBooking);
            BackCommand = new Command(OnBack);

            // Fetch Concerts from the API
            LoadConcerts();
        }

        // Fetch Concerts from API
        private async void LoadConcerts()
        {
            try
            {
                var concerts = await _apiService.GetConcertsAsync();
                Concerts = new ObservableCollection<Concert>(concerts);
            }
            catch (Exception ex)
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Failed to load concerts: {ex.Message}", "OK");
                }
            }
        }

        private async void UpdatePerformances()
        {
            if (SelectedConcert != null)
            {
                try
                {
                    var performances = await _apiService.GetPerformancesForConcertAsync(SelectedConcert.Id);
                    Performances = new ObservableCollection<Performance>(performances);

                    if (Performances.Any())
                        SelectedPerformance = Performances.First();
                }
                catch (Exception ex)
                {
                    if (Application.Current?.MainPage != null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", $"Failed to load performances: {ex.Message}", "OK");
                    }
                }
            }
            else
            {
                Performances = new ObservableCollection<Performance>();
                SelectedPerformance = null!;
            }
        }

        private async void OnAddBooking()
        {
            if (SelectedPerformance == null || string.IsNullOrWhiteSpace(NewCustomerName) || string.IsNullOrWhiteSpace(NewCustomerEmail))
            {
                // Show error if performance or customer details are missing
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Please fill in all fields and select a performance", "OK");
                }
                return;
            }

            // Create new booking object
            var newBooking = new Booking
            {
                CustomerName = NewCustomerName,
                CustomerEmail = NewCustomerEmail,
                Performance = SelectedPerformance
            };

            try
            {
                // Send booking data via API
                var createdBooking = await _apiService.CreateBookingAsync(newBooking);
                Bookings.Add(createdBooking);
                ClearBookingForm();
            }
            catch (Exception ex)
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Failed to create booking: {ex.Message}", "OK");
                }
            }
        }

        private async void OnDeleteBooking(Booking booking)
        {
            if (Application.Current?.MainPage != null)
            {
                bool isConfirmed = await Application.Current.MainPage.DisplayAlert(
                    "Delete Booking",
                    $"Are you sure you want to delete the booking for {booking.CustomerName}?",
                    "Yes",
                    "No");

                if (isConfirmed)
                {
                    Bookings.Remove(booking);
                }
            }
        }

        private void ClearBookingForm()
        {
            NewCustomerName = string.Empty;
            NewCustomerEmail = string.Empty;
            SelectedPerformance = null!;
            SelectedConcert = null!;
        }

        private async void OnBack()
        {
            await Shell.Current.GoToAsync("//ConcertsPage");
        }

        // Method to notify when a property changes
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
