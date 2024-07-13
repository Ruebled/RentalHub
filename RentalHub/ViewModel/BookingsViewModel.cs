using RentalHub.Model;
using RentalHub.Repositories;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class BookingsViewModel : ViewModelBase
    {
        BookingRepository _bookingRepository;
        private UserModel user;
        public UserModel User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }

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

        public BookingsViewModel(UserModel user)
        {
            User = user;
            _bookingRepository = new BookingRepository();

            // Initialize properties and commands
            Bookings = new ObservableCollection<BookingModel>();
            LoadBookings();

            AddBookingCommand = new RelayCommand<object>(AddBookingExecute);
            EditBookingCommand = new RelayCommand<object>(EditBookingExecute, CanEditBookingExecute);
        }

        private async Task LoadBookings()
        {
            Bookings.Clear();

            BookingRepository bookingRepository = new BookingRepository();
            var results = await Task.Run(() => bookingRepository.RetrieveBookings(user.UserId));

            foreach (var booking in results)
            {
                Bookings.Add(booking);
            }
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