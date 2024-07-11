using RentalHub.Model;
using RentalHub.Repositories;

using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class SearchViewModel : ViewModelBase
    {
        public ObservableCollection<ApartmentModel> SearchResults { get; set; }

        private string _searchQuery;
        private DateTime _checkinday;
        private DateTime _checkoutday;
        private DateTime _minDate;
        private DateTime _maxDate;

        public DateTime MinDate
        {
            get => _minDate;
            set
            {
                if (_minDate != value)
                {
                    _minDate = value;
                    OnPropertyChanged(nameof(MinDate));
                }
            }
        }

        public DateTime MaxDate
        {
            get => _maxDate;
            set
            {
                if (_maxDate != value)
                {
                    _maxDate = value;
                    OnPropertyChanged(nameof(MaxDate));
                }
            }
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged(nameof(SearchQuery));
                }
            }
        }

        public DateTime CheckInDay
        {
            get => _checkinday;
            set
            {
                if (_checkinday != value)
                {
                    _checkinday = value;
                    OnPropertyChanged(nameof(CheckInDay));
                    UpdateCheckOutDay();
                }
            }
        }

        public DateTime CheckOutDay
        {
            get => _checkoutday;
            set
            {
                if (_checkoutday != value)
                {
                    _checkoutday = value;
                    OnPropertyChanged(nameof(CheckOutDay));
                    UpdateCheckOutDay();
                }
            }
        }

        public ICommand SearchCommand { get; set; }

        public SearchViewModel()
        {
            // Set Min and Max dates for the calendar at hand
            MinDate = DateTime.Now.Date;
            MaxDate = DateTime.Now.Date.AddYears(1);

            // Set default checkin & checkout days (today and tomorrow)
            CheckInDay = DateTime.Now.Date;
            CheckOutDay = DateTime.Now.Date.AddDays(1);

            SearchResults = new ObservableCollection<ApartmentModel>();

            SearchCommand = new RelayCommand(ExecuteSearchCommand, CanExecuteSearchCommand);
        }

        private void ExecuteSearchCommand(object obj)
        {
            // Clear previous results
            SearchResults.Clear();

            ApartmentRepository apartmentRepository = new ApartmentRepository();
            var results = apartmentRepository.GetApartments(SearchQuery);

            foreach (var apartment in results)
            {
                SearchResults.Add(apartment);
            }
        }

        private bool CanExecuteSearchCommand(object obj)
        {
            // Ensure valid check-in and check-out dates
            if (_checkinday == DateTime.MinValue || _checkoutday == DateTime.MinValue)
            {
                return false;
            }

            // Check if check-in date is before check-out date
            return _checkinday < _checkoutday;
        }

        private void UpdateCheckOutDay()
        {
            if (_checkinday >= _checkoutday)
            {
                CheckOutDay = _checkinday.AddDays(1);
            }
        }
    }
}
