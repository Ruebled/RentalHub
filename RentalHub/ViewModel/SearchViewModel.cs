using RentalHub.Model;
using RentalHub.Repositories;

using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class SearchViewModel : ViewModelBase
    {
        public ObservableCollection<ApartmentModel> SearchResults { get; set; }

        private string _searchQuery;
        private DateTime _checkindate;
        private DateTime _checkoutdate;
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
                    OnSearchQueryChanged();
                }
            }
        }

        public DateTime CheckInDate
        {
            get => _checkindate;
            set
            {
                if (_checkindate != value)
                {
                    _checkindate = value;
                    OnPropertyChanged(nameof(CheckInDate));
                    UpdateCheckOutDay();
                    CommandManager.InvalidateRequerySuggested();
                    OnSearchQueryChanged();
                }
            }
        }

        public DateTime CheckOutDate
        {
            get => _checkoutdate;
            set
            {
                if (_checkoutdate != value)
                {
                    _checkoutdate = value;
                    OnPropertyChanged(nameof(CheckOutDate));
                    UpdateCheckOutDay();
                    CommandManager.InvalidateRequerySuggested();
                    OnSearchQueryChanged();
                }
            }
        }

        public ICommand SearchCommand { get; set; }
        public ICommand ViewDetailsCommand { get; set; }

        public SearchViewModel()
        {
            SearchResults = new ObservableCollection<ApartmentModel>();

            // Set Min and Max dates for the calendar at hand
            MinDate = DateTime.Now.Date;
            MaxDate = DateTime.Now.Date.AddYears(1);

            // Set default check-in & check-out days (today and tomorrow)
            CheckInDate = DateTime.Now.Date;
            CheckOutDate = DateTime.Now.Date.AddDays(1);

            // Setting Command object
            ViewDetailsCommand = new RelayCommand<ApartmentModel>(ExecuteViewDetailsCommand);
            SearchCommand = new RelayCommand<object>(ExecuteSearchCommand, CanExecuteSearchCommand);

            // Set Search Query to a value to trigger the search
            SearchQuery = string.Empty;
        }

        private void ExecuteViewDetailsCommand(ApartmentModel selectedApartment)
        {
            if (MainViewModel.Instance != null)
            {
                MainViewModel.Instance.PushView(new ApartmentViewModel(selectedApartment));
            }
        }

        private void ExecuteSearchCommand(object obj)
        {
            // Clear previous results
            SearchResults.Clear();

            ApartmentRepository apartmentRepository = new ApartmentRepository();
            var results = apartmentRepository.GetApartmentsByCityAndDays(SearchQuery, CheckInDate, CheckOutDate);

            foreach (var apartment in results)
            {
                SearchResults.Add(apartment);
            }
        }

        private bool CanExecuteSearchCommand(object obj)
        {
            // Ensure valid check-in and check-out dates
            if (_checkindate == DateTime.MinValue || _checkoutdate == DateTime.MinValue)
            {
                return false;
            }

            // Check if check-in date is before check-out date
            return _checkindate < _checkoutdate;
        }

        private void UpdateCheckOutDay()
        {
            if (_checkindate >= _checkoutdate)
            {
                CheckOutDate = _checkindate.AddDays(1);
            }
        }

        private void OnSearchQueryChanged()
        {
            if (CanExecuteSearchCommand(null))
            {
                ExecuteSearchCommand(null);
            }
        }
    }
}
