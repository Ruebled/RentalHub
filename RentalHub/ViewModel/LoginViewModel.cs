using RentalHub.Model;
using RentalHub.Repositories;

using System.Security;
using System.Security.Principal;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        // Fields
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private IUserRepository userRepository;

        // Properties
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public SecureString Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
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
        public bool IsViewVisible
        {
            get => _isViewVisible;
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        // -> Commands
        public ICommand LoginCommand { get; }
        public ICommand RecoverPassswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }

        // Contructor
        public LoginViewModel()
        {
            userRepository = new UserRepository();

            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPassswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));
        }

        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteLoginCommand(object obj)
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

        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser(new System.Net.NetworkCredential(Username, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
        }
    }
}
