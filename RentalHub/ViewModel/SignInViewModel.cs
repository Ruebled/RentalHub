using RentalHub.Model;
using RentalHub.Repositories;

using System.Security;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class SignInViewModel : ViewModelBase
    {
        // Fields
        private string _username;
        private SecureString _password;
        private string _errorMessage;

        private IUserRepository userRepository;

        // Properties
        public string Username
        {
            get => _username;
            set
            {
                if(_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                    ErrorMessage = string.Empty;
                }
            }
        }
        public SecureString Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                    ErrorMessage = string.Empty;
                }
            }
        }

        private bool _isTextBoxFocused;
        public bool IsTextBoxFocused
        {
            get { return _isTextBoxFocused; }
            set
            {
                _isTextBoxFocused = value;
                OnPropertyChanged(nameof(IsTextBoxFocused));
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

        private bool _rememberMeCheck;
        public bool RememberMeCheck
        {
            get => _rememberMeCheck;
            set
            {
                if (_rememberMeCheck != value)
                {
                    _rememberMeCheck = value;
                    OnPropertyChanged(nameof(RememberMeCheck));
                }
            }
        }

        // -> Commands
        public ICommand SignInCommand { get; }
        public ICommand OpenSignUpViewCommand { get; }
        public ICommand OpenResetPasswordViewCommand { get; }

        // Contructor
        public SignInViewModel()
        {
            // Set default values for class variables
            Username = string.Empty;
            Password = new SecureString();
            ErrorMessage = string.Empty;

            userRepository = new UserRepository();

            // Configure commands
            SignInCommand = new RelayCommand<object>(ExecuteSignInCommand, CanExecuteSignInCommand);
            OpenSignUpViewCommand = new RelayCommand<object>(ExecuteOpenSignUpViewCommand);
            OpenResetPasswordViewCommand = new RelayCommand<object>(ExecuteOpenResetPasswordViewCommand);

            IsTextBoxFocused = true;
        }

        private void ExecuteOpenResetPasswordViewCommand(object obj)
        {
            if(CheckAccountViewModel.Instance != null)
            {
                CheckAccountViewModel.Instance.NavigatePasswordReset();
            }
        }

        private void ExecuteOpenSignUpViewCommand(object obj)
        {
            if (CheckAccountViewModel.Instance != null)
            {
                CheckAccountViewModel.Instance.NavigateToSignUp();
            }
        }
 
            

        private void ExecuteSignInCommand(object obj)
        {
            string PasswordHashed = PasswordHasher.HashSecureString(Password);
            UserModel user = userRepository.AuthenticateUser(Username, PasswordHashed);
            if (user != null)
            {
                // Save credentials in case checkbox checked
                CheckAccountViewModel.Instance.OnCheckBoxChecked(RememberMeCheck);

                // Raise login successful event to mark opening of the main window
                CheckAccountViewModel.Instance.OnLoginSuccessful(user);
            }
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
        }

        private bool CanExecuteSignInCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrEmpty(Username) ||
               Username.Length < 3 || Password == null || Password.Length < 3)
            {
                validData = false;
            }
            else
            {
                validData = true;
            }

            return validData;
        }
    }
}
