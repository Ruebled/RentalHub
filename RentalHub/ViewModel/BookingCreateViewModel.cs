using RentalHub.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    class BookingCreateViewModel : ViewModelBase
    {
        public ICommand BackCommand { get; set; }

        public BookingCreateViewModel()
        {
            InitCommands();
        }

        public BookingCreateViewModel(ApartmentModel apartmentModel)
        {
            InitCommands();
        }

        private void InitCommands()
        {
            BackCommand = new RelayCommand<object>(ExecuteBackCommand);
        }

        private void ExecuteBackCommand(object obj)
        {
            MainViewModel.Instance?.PopView();
        }
    }
}
