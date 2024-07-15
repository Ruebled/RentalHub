using RentalHub.Model;
using RentalHub.Repositories;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class ApartmentViewModel : ViewModelBase
    {
        // Lists
        public ObservableCollection<PhotoModel> ApartmentPhotos { get; set; }
        public ObservableCollection<ReviewModel> ReviewsList { get; set; }

        private ApartmentModel _apartmentSelected;

        public ApartmentModel ApartmentSelected
        {
            get => _apartmentSelected;
            set
            {
                _apartmentSelected = value;
                OnPropertyChanged(nameof(ApartmentSelected));
            }
        }

        public ICommand BackCommand { get; set; }

        public ApartmentViewModel(ApartmentModel selectedApartment)
        {
            ArgumentNullException.ThrowIfNull(selectedApartment);

            _apartmentSelected = selectedApartment;
            ApartmentPhotos = new ObservableCollection<PhotoModel>();
            AquireApartmentPhotosData();

            ReviewsList = new ObservableCollection<ReviewModel>();
            AquireReviewsList();

            // Setting up relay commands
            BackCommand = new RelayCommand<object>(ExecuteBackCommand, null);
        }

        private void AquireReviewsList()
        {
            ApartmentRepository apartmentRepository = new ApartmentRepository();
            ReviewsList.Clear();
            var reviews = apartmentRepository.GetReviewsList(ApartmentSelected.ApartmentID);

            foreach (ReviewModel review in reviews)
            {
                ReviewsList.Add(review);
            }
        }

        private void AquireApartmentPhotosData()
        {
            //throw new NotImplementedException();
        }

        private void ExecuteBackCommand(object obj)
        {
            MainViewModel.Instance?.PopView();
        }
    }
}
