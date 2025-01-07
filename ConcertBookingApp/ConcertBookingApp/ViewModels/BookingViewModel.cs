using ConcertBookingApp.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace ConcertBookingApp.ViewModels
{
    public class BookingViewModel : INotifyPropertyChanged
    {
        // Singleton instance for the ViewModel
        public static BookingViewModel Instance { get; } = new BookingViewModel();

        // Implementing the INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Performance> _performances;
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

        private ObservableCollection<Concert> _concerts;
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

        public ObservableCollection<Booking> Bookings { get; set; } = new ObservableCollection<Booking>();

        private string _newCustomerName;
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

        private string _newCustomerEmail;
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

        private Performance _selectedPerformance;
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

        private Concert _selectedConcert;
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

        public BookingViewModel()
        {
            // Initialize commands
            AddBookingCommand = new Command(OnAddBooking);
            DeleteBookingCommand = new Command<Booking>(OnDeleteBooking);
            BackCommand = new Command(OnBack);

            // Temporary sample data for testing
            Concerts = new ObservableCollection<Concert>
            {
                new Concert
                {
                    Id = 1,
                    Title = "Rock Fest",
                    Performances = new ObservableCollection<Performance>
                    {
                        new Performance { Id = 1, Location = "Arena 1", DateTime = DateTime.Now },
                        new Performance { Id = 2, Location = "Arena 2", DateTime = DateTime.Now.AddDays(1) }
                    }
                },
                new Concert
                {
                    Id = 2,
                    Title = "Jazz Night",
                    Performances = new ObservableCollection<Performance>
                    {
                        new Performance { Id = 3, Location = "Jazz Arena", DateTime = DateTime.Now.AddDays(2) }
                    }
                }
            };

            // Adding some initial bookings for testing
            Bookings.Add(new Booking { CustomerName = "John Doe", CustomerEmail = "john@example.com", Performance = Concerts[0].Performances.First(), Concert = Concerts[0] });
            Bookings.Add(new Booking { CustomerName = "Jane Smith", CustomerEmail = "jane@example.com", Performance = Concerts[1].Performances.First(), Concert = Concerts[1] });
        }

        // Method to update the Performances list when a concert is selected
        private void UpdatePerformances()
        {
            if (SelectedConcert != null)
            {
                Performances = new ObservableCollection<Performance>(SelectedConcert.Performances);

                // Automatically select the first performance if available
                if (Performances.Any())
                    SelectedPerformance = Performances.First();
            }
            else
            {
                Performances = new ObservableCollection<Performance>();
                SelectedPerformance = null;
            }
        }

        private void OnAddBooking()
        {
            if (SelectedPerformance == null || string.IsNullOrWhiteSpace(NewCustomerName) || string.IsNullOrWhiteSpace(NewCustomerEmail))
            {
                // Show error if performance or customer details are missing
                Application.Current.MainPage.DisplayAlert("Error", "Please fill in all fields and select a performance", "OK");
                return;
            }

            // Add new booking
            var newBooking = new Booking
            {
                CustomerName = NewCustomerName,
                CustomerEmail = NewCustomerEmail,
                Performance = SelectedPerformance,
                Concert = SelectedConcert // Set the concert for the booking
            };

            Bookings.Add(newBooking);
            ClearBookingForm();
        }

        private async void OnDeleteBooking(Booking booking)
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

        private void ClearBookingForm()
        {
            NewCustomerName = string.Empty;
            NewCustomerEmail = string.Empty;
            SelectedPerformance = null;
            SelectedConcert = null; // Clear concert selection as well
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
