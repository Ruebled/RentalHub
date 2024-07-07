using RentalHub.Model;

using System.Text.RegularExpressions;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class PasswordResetViewModel : ViewModelBase
    {
        private string _usernameOrEmail;
        private string _errorMessage;

        public string UsernameOrEmail
        {
            get { return _usernameOrEmail; }
            set
            {
                _usernameOrEmail = value;
                OnPropertyChanged(nameof(UsernameOrEmail));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand ResetPasswordCommand { get; }
        public ICommand OpenSignInViewCommand { get; }

        public PasswordResetViewModel()
        {
            ResetPasswordCommand = new RelayCommand(ExecuteResetPasswordCommand, CanExectuteResetPasswordCommand);
            OpenSignInViewCommand = new RelayCommand(ExecuteOpenSignInViewCommand);
        }

        private void ExecuteOpenSignInViewCommand(object obj)
        {
            if (CheckAccountViewModel.Instance != null)
            {
                CheckAccountViewModel.Instance.NavigateToSignIn();
            }
        }

        private bool CanExectuteResetPasswordCommand(object arg)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (string.IsNullOrEmpty(UsernameOrEmail))
            {
                return false;
            }

            if (UsernameOrEmail.Contains("@"))
            {
                if (Regex.IsMatch(UsernameOrEmail, emailPattern))
                {
                    ErrorMessage = string.Empty;
                    return true;
                }
                else
                {
                    ErrorMessage = "Invalid email format.";
                    return false;
                }
            }
            else
            {
                if (UsernameOrEmail.Length >= 3)
                {
                    ErrorMessage = string.Empty;
                    return true;
                }
                else
                {
                    ErrorMessage = "Username must be at least 3 characters long.";
                    return false;
                }
            }
        }


        private void ExecuteResetPasswordCommand(object obj)
        {
            // Check if the UsernameOrEmail property is null or empty
            if (string.IsNullOrEmpty(UsernameOrEmail))
            {
                ErrorMessage = "Username or Email cannot be empty.";
            }
        }
    }
}
