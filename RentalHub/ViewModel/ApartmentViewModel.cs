using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

using RentalHub.Model;
using RentalHub.Repositories;

namespace RentalHub.ViewModel
{
    public class ApartmentViewModel : ViewModelBase
    {
        private ApartmentModel _apartmentSelected;
        private PhotoModel _selectedPhoto;

        public ApartmentModel ApartmentSelected
        {
            get => _apartmentSelected;
            set
            {
                if (_apartmentSelected != value)
                {
                    _apartmentSelected = value;
                    OnPropertyChanged(nameof(ApartmentSelected));
                }
            }
        }

        public ObservableCollection<PhotoModel> ApartmentPhotos { get; set; }
        public ObservableCollection<ReviewModel> ReviewsList { get; set; }
        public ObservableCollection<CalendarDateRange> BlackoutDates { get; set; }

        public ICommand BackCommand { get; set; }

        public PhotoModel SelectedPhoto
        {
            get => _selectedPhoto;
            set
            {
                if (_selectedPhoto != value)
                {
                    _selectedPhoto = value;
                    OnPropertyChanged(nameof(SelectedPhoto));
                    // Update main photo URL when selected photo changes
                    if (_selectedPhoto != null)
                    {
                        ApartmentSelected.MainPhotoURL = _selectedPhoto.PhotoURL;
                        OnPropertyChanged(nameof(ApartmentSelected));
                    }
                }
            }
        }

        public ApartmentViewModel(ApartmentModel selectedApartment)
        {
            if (selectedApartment == null)
                throw new ArgumentNullException(nameof(selectedApartment));

            ApartmentPhotos = new ObservableCollection<PhotoModel>();
            ReviewsList = new ObservableCollection<ReviewModel>();
            BlackoutDates = new ObservableCollection<CalendarDateRange>();

            ApartmentSelected = selectedApartment;
            LoadApartmentDetails();

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            BackCommand = new RelayCommand<object>(ExecuteBackCommand);
        }

        private void LoadApartmentDetails()
        {
            LoadPhotos();
            LoadReviews();
            LoadBlackoutDates();
        }

        private void LoadPhotos()
        {
            ApartmentPhotos.Clear();
            var photos = ApartmentRepository.Instance.GetPhotosList(ApartmentSelected.ApartmentID);

            foreach (PhotoModel photo in photos)
            {
                ApartmentPhotos.Add(photo);
            }
        }

        private void LoadReviews()
        {
            ReviewsList.Clear();
            var reviews = ApartmentRepository.Instance.GetReviewsList(ApartmentSelected.ApartmentID);

            foreach (ReviewModel review in reviews)
            {
                ReviewsList.Add(review);
            }
        }

        private void LoadBlackoutDates()
        {
            BlackoutDates.Clear();

            var reviews = BookingRepository.Instance.RetrieveBookingsOfApartments(ApartmentSelected.ApartmentID);

            // Assuming `reviews` is a collection of bookings
            var blockingBookings = reviews.Where(r => r.Status == "Active" || r.Status == "CheckedIn").ToList();

            foreach (var blocking in blockingBookings)
            {
                if (blocking.CheckInDate == null || blocking.CheckOutDate == null)
                {
                    throw new NotImplementedException();
                }

                DateTime checkInDateTime = DateTime.Parse(blocking.CheckInDate);
                DateTime checkOutDateTime = DateTime.Parse(blocking.CheckOutDate);

                BlackoutDates.Add(new CalendarDateRange(checkInDateTime, checkOutDateTime));
            }
        }

        private void ExecuteBackCommand(object obj)
        {
            MainViewModel.Instance?.PopView();
        }
    }
}
