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
                }
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

        // -> Commands
        public ICommand SignInCommand { get; }
        public ICommand OpenSignUpViewCommand { get; }
        //public ICommand RecoverPassswordCommand { get; }
        //public ICommand ShowPasswordCommand { get; }
        //public ICommand RememberPasswordCommand { get; }

        // Contructor
        public SignInViewModel()
        {
            userRepository = new UserRepository();

            //LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            SignInCommand = new RelayCommand(ExecuteSignInCommand, CanExecuteSignInCommand);
            OpenSignUpViewCommand = new ViewModelCommand(ExecuteOpenSignUpViewCommand);

            //OpenSignUpViewCommand = new RelayCommand(ExecuteOpenSignUpViewCommand, CanExecuteOpenSignUpViewCommand);
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
            UserModel user = userRepository.AuthenticateUser(new System.Net.NetworkCredential(Username, Password));
            if (user != null)
            {
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
