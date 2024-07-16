using RentalHub.Model;
using RentalHub.Repositories;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class SearchViewModel : ViewModelBase
    {
        // Properties for search results and date ranges
        public ObservableCollection<ApartmentModel> SearchResults { get; set; }
        private DateTime _checkInDate;
        private DateTime _checkOutDate;
        private DateTime _minDate;
        private DateTime _maxDate;

        public DateTime MinDate
        {
            get => _minDate;
            set
            {
                _minDate = value;
                OnPropertyChanged(nameof(MinDate));
            }
        }

        public DateTime MaxDate
        {
            get => _maxDate;
            set
            {
                _maxDate = value;
                OnPropertyChanged(nameof(MaxDate));
            }
        }

        // Search query property with update trigger
        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged(nameof(SearchQuery));
                    UpdateCityOptions(); // Update city options based on search query
                    OnSearchQueryChanged(); // Trigger search based on query change
                }
            }
        }

        // Date properties with validation and update logic
        public DateTime CheckInDate
        {
            get => _checkInDate;
            set
            {
                if (_checkInDate != value)
                {
                    _checkInDate = value;
                    OnPropertyChanged(nameof(CheckInDate));
                    UpdateCheckOutDate();
                    CommandManager.InvalidateRequerySuggested(); // Notify command manager for SearchCommand
                    OnSearchQueryChanged(); // Trigger search based on date change
                }
            }
        }

        public DateTime CheckOutDate
        {
            get => _checkOutDate;
            set
            {
                if (_checkOutDate != value)
                {
                    _checkOutDate = value;
                    OnPropertyChanged(nameof(CheckOutDate));
                    UpdateCheckOutDate();
                    CommandManager.InvalidateRequerySuggested(); // Notify command manager for SearchCommand
                    OnSearchQueryChanged(); // Trigger search based on date change
                }
            }
        }

        // Command properties for executing search and viewing details
        public ICommand SearchCommand { get; set; }
        public ICommand ViewDetailsCommand { get; set; }
        public ICommand EnterCommand { get; set; }

        // Collection for city options and visibility flag for city ListBox
        public ObservableCollection<string> CityOptions { get; set; }
        private bool _cityListBoxVisible;
        public bool CityListBoxVisible
        {
            get => _cityListBoxVisible;
            set
            {
                if (_cityListBoxVisible != value)
                {
                    _cityListBoxVisible = value;
                    OnPropertyChanged(nameof(CityListBoxVisible));
                }
            }
        }

        // Constructor to initialize properties and commands
        public SearchViewModel()
        {
            SearchResults = new ObservableCollection<ApartmentModel>();
            CityOptions = new ObservableCollection<string>();

            // Set minimum and maximum date ranges
            MinDate = DateTime.Now.Date;
            MaxDate = _minDate.AddYears(1);
            CheckInDate = _minDate;
            CheckOutDate = _minDate.AddDays(1);

            // Initialize commands
            SearchCommand = new RelayCommand<object>(ExecuteSearchCommand, CanExecuteSearchCommand);
            EnterCommand = new RelayCommand<object>(ExecuteEnterCommand, CanExecuteSearchCommand);
            ViewDetailsCommand = new RelayCommand<ApartmentModel>(ExecuteViewDetailsCommand);
        }

        private void ExecuteEnterCommand(object obj)
        {
            CityListBoxVisible = false;
            SearchCommand.Execute(null);
        }



        // Method to execute search based on current search query and date range
        private void ExecuteSearchCommand(object obj)
        {
            SearchResults.Clear(); // Clear previous search results

            // Call repository method to fetch apartments based on city and date range
            var results = ApartmentRepository.Instance.GetApartmentsByCityAndDays(SearchQuery, CheckInDate, CheckOutDate);

            foreach (var apartment in results)
            {
                SearchResults.Add(apartment); // Add fetched apartments to results collection
            }
        }

        // Method to determine if search can be executed based on current date range validity
        private bool CanExecuteSearchCommand(object obj)
        {
            return CheckInDate < CheckOutDate; // Ensure check-in is before check-out
        }

        // Method to handle viewing details of a selected apartment
        private void ExecuteViewDetailsCommand(ApartmentModel selectedApartment)
        {
            // Example implementation to navigate to details view
            if (MainViewModel.Instance != null)
            {
                MainViewModel.Instance.PushView(new ApartmentViewModel(selectedApartment));
            }
        }

        // Method to update check-out date if check-in date is adjusted
        private void UpdateCheckOutDate()
        {
            if (CheckOutDate <= CheckInDate)
            {
                CheckOutDate = CheckInDate.AddDays(1); // Set check-out date to be at least one day after check-in
            }
        }

        // Method to update city options based on search query
        private void UpdateCityOptions()
        {
            var allCities = GetCityList(); // Get all available city options (replace with actual data retrieval)

            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                CityOptions.Clear(); // Clear options if search query is empty
                CityListBoxVisible = false; // Hide ListBox if no options to display
            }
            else
            {
                var filteredCities = allCities.Where(city => city.ToLower().Contains(SearchQuery.ToLower())).ToList();
                CityOptions.Clear();

                foreach (var city in filteredCities)
                {
                    CityOptions.Add(city); // Add filtered city options to collection
                }

                CityListBoxVisible = CityOptions.Count > 0; // Show ListBox if options are available
            }
        }

        // Method to simulate fetching city list (replace with actual data retrieval)
        private List<string> GetCityList()
        {
            return LocationRepository.Instance.GetCitiesByPartial(SearchQuery);
        }

        // Method to trigger search based on search query or date change
        private void OnSearchQueryChanged()
        {
            if (CanExecuteSearchCommand(null))
            {
                ExecuteSearchCommand(null); // Execute search if conditions are met
            }
        }
    }
}
