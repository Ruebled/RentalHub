using RentalHub.Model;

using System.Windows;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class InputDialogWindowViewModel : ViewModelBase
    {
        private string _userInput;
        public string UserInput
        {
            get => _userInput;
            set
            {
                _userInput = value;
                OnPropertyChanged(nameof(UserInput));
            }
        }

        public ICommand OKCommand { get; }
        public ICommand CancelCommand { get; }

        public InputDialogWindowViewModel()
        {
            OKCommand = new RelayCommand<object>(OKCommandExecute, OKCommandCanExecute);
            CancelCommand = new RelayCommand<object>(CancelCommandExecute);
        }

        private void OKCommandExecute(object parameter)
        {
            // Close the dialog and return DialogResult true
            CloseDialog(parameter as Window, true);
        }

        private bool OKCommandCanExecute(object parameter)
        {
            // Can execute command logic (optional)
            return !string.IsNullOrWhiteSpace(UserInput);
        }

        private void CancelCommandExecute(object parameter)
        {
            // Close the dialog and return DialogResult false
            CloseDialog(parameter as Window, false);
        }

        private void CloseDialog(Window window, bool dialogResult)
        {
            if (window != null)
            {
                window.DialogResult = dialogResult;
                window.Close();
            }
        }
    }
}