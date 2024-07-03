using RentalHub.Model;
using RentalHub.Repositories;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;
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
        public event EventHandler<UserModel>? LoginSuccessful;

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

            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPassswordCommand = new RelayCommand(p => ExecuteRecoverPassCommand("", ""));
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
            UserModel user = userRepository.AuthenticateUser(new System.Net.NetworkCredential(Username, Password));
            if (user != null)
            {
                OnLoginSuccessful(user);
            }
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
        }

        protected virtual void OnLoginSuccessful(UserModel user)
        {
            LoginSuccessful?.Invoke(this, user);
        }
    }
}
