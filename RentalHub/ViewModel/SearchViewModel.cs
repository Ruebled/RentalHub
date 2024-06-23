using RentalHub.Model;
using RentalHub.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class SearchViewModel : ViewModelBase
    {
        public ObservableCollection<ApartmentModel> SearchResults { get; set; }

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand { get; set; }

        public SearchViewModel()
        {
            SearchResults = new ObservableCollection<ApartmentModel>();
            SearchCommand = new RelayCommand(async (parameter) => await SearchButton_Click());
        }

        private async Task SearchButton_Click()
        {
            // Clear previous results
            SearchResults.Clear();

            ApartmentRepository apartmentRepository = new ApartmentRepository();
            var results = await Task.Run(() => apartmentRepository.GetApartments(SearchQuery));

            foreach (var apartment in results)
            {
                SearchResults.Add(apartment);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}