using RentalHub.Model;

using System.Windows;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class InputDialogWindowViewModel : ViewModelBase
    {
        private string _codePart1;
        private string _codePart2;
        private string _codePart3;
        private string _codePart4;
        private string _codePart5;
        private string _codePart6;

        public string CodePart1
        {
            get => _codePart1;
            set
            {
                _codePart1 = value;
                OnPropertyChanged(nameof(CodePart1));
                UpdateUserInput();
            }
        }

        public string CodePart2
        {
            get => _codePart2;
            set
            {
                _codePart2 = value;
                OnPropertyChanged(nameof(CodePart2));
                UpdateUserInput();
            }
        }

        public string CodePart3
        {
            get => _codePart3;
            set
            {
                _codePart3 = value;
                OnPropertyChanged(nameof(CodePart3));
                UpdateUserInput();
            }
        }

        public string CodePart4
        {
            get => _codePart4;
            set
            {
                _codePart4 = value;
                OnPropertyChanged(nameof(CodePart4));
                UpdateUserInput();
            }
        }

        public string CodePart5
        {
            get => _codePart5;
            set
            {
                _codePart5 = value;
                OnPropertyChanged(nameof(CodePart5));
                UpdateUserInput();
            }
        }

        public string CodePart6
        {
            get => _codePart6;
            set
            {
                _codePart6 = value;
                OnPropertyChanged(nameof(CodePart6));
                UpdateUserInput();
            }
        }

        private void UpdateUserInput()
        {
            UserInput = $"{CodePart1}{CodePart2}{CodePart3}{CodePart4}{CodePart5}{CodePart6}";
            OnPropertyChanged(nameof(UserInput));
        }

        private string _userInput;
        public string UserInput
        {
            get { return _userInput; }
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
            return !string.IsNullOrWhiteSpace(UserInput) && UserInput.Length == 6;
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
