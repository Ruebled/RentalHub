using RentalHub.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class ApartmentViewModel : ViewModelBase
    {
        private ApartmentModel _apartmentSelected;

        public ApartmentModel Apartment
        {
            get => _apartmentSelected;
            set
            {
                _apartmentSelected = value;
                OnPropertyChanged(nameof(Apartment));
            }
        }

        public string Name { get; set; }

        public ICommand BackCommand { get; set; }

        public ApartmentViewModel(ApartmentModel selectedApartment)
        {
            _apartmentSelected = selectedApartment ?? new ApartmentModel();
            Name = _apartmentSelected.Name;

            BackCommand = new RelayCommand<object>(ExecuteBackCommand, null);
        }

        private void ExecuteBackCommand(object obj)
        {
            MainViewModel.Instance?.PopView();
        }
    }
}
