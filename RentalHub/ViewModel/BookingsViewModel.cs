using RentalHub.Model;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class BookingsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<BookingModel> _bookings;
        public ObservableCollection<BookingModel> Bookings
        {
            get { return _bookings; }
            set { _bookings = value; OnPropertyChanged(); }
        }

        private BookingModel _selectedBooking;
        public BookingModel SelectedBooking
        {
            get { return _selectedBooking; }
            set { _selectedBooking = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsDetailsVisible)); }
        }

        public bool IsDetailsVisible => SelectedBooking != null;

        public ICommand AddBookingCommand { get; set; }
        public ICommand EditBookingCommand { get; set; }

        public BookingsViewModel()
        {
            // Initialize properties and commands
            Bookings = new ObservableCollection<BookingModel>();
            LoadBookings(); // Load initial bookings (for example)

            AddBookingCommand = new RelayCommand(AddBookingExecute);
            EditBookingCommand = new RelayCommand(EditBookingExecute, CanEditBookingExecute);
        }

        private void LoadBookings()
        {
            // Example: Load bookings from a data source
            Bookings.Add(new BookingModel { ApartmentName = "Apartment A", CheckInDate = DateTime.Today, CheckOutDate = DateTime.Today.AddDays(7) });
            Bookings.Add(new BookingModel { ApartmentName = "Apartment B", CheckInDate = DateTime.Today.AddDays(10), CheckOutDate = DateTime.Today.AddDays(17) });
        }

        private void AddBookingExecute(object obj)
        {
            // Implement logic to add a new booking
            MessageBox.Show("Adding new booking...");
            // Example: Open a dialog/window to add a new booking
        }

        private void EditBookingExecute(object obj)
        {
            // Implement logic to edit the selected booking
            MessageBox.Show($"Editing booking: {SelectedBooking.ApartmentName}");
            // Example: Open a dialog/window to edit the selected booking
        }

        private bool CanEditBookingExecute(object obj)
        {
            // Implement logic to determine if editing booking is allowed
            return SelectedBooking != null;
        }

        // Implement INotifyPropertyChanged interface for property change notification
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}