using RentalHub.Model;
using RentalHub.Repositories;

using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class PasswordResetViewModel : ViewModelBase
    {
        private string _usernameOrEmail;
        private string _errorMessage;
        private IUserRepository userRepository;

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
            userRepository = new UserRepository();

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
            //if (string.IsNullOrEmpty(UsernameOrEmail))
            //{
            //    ErrorMessage = "Username or Email cannot be empty.";
            //}
            UserModel user;

            if (UsernameOrEmail.Contains('@'))
            {
                user = userRepository.GetByEmail(UsernameOrEmail);
            }
            else
            {
                user = userRepository.GetByUsername(UsernameOrEmail);
            }

            if (user == null || user.UserId == null)
            {
                ErrorMessage = "Unknown user";
                return;
            }
            
            ErrorMessage = string.Empty;

            string Password = PasswordGenerator.GeneratePassword(16);
            if (string.IsNullOrEmpty(Password))
            {
                Debug.Print("Password Generator Generater an empty password");
                return;
            }

            userRepository.UpdateUserPassword(user.UserId, Password);

            // Put anywhere else
            string smtpUsername = "";
            string smtpPassword = "";

            EmailSender emailSender = new EmailSender(smtpUsername, smtpPassword);
            emailSender.SendPasswordResetEmail(user.Email, Password + " " + userRepository.HashPassword(Password));
        }
    }
}
