using RentalHub.Model;
using RentalHub.Repositories;

using System.Security;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class SignUpViewModel : ViewModelBase
    {
        // Private Fields
        private string _username;
        private SecureString _password;
        private string _email;
        private string _phonenumber;
        private string _usertype;
        private string _profilephotoid;
        private string _errorMessage;


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

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Phonenumber
        {
            get => _phonenumber;
            set
            {
                _phonenumber = value;
                OnPropertyChanged(nameof(Phonenumber));
            }
        }

        public string Usertype
        {
            get => _usertype;
            set
            {
                _usertype = value;
                OnPropertyChanged(nameof(Usertype));
            }
        }

        public string Profilephotoid
        {
            get => _profilephotoid;
            set
            {
                _profilephotoid = value;
                OnPropertyChanged(nameof(Profilephotoid));
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


        public ICommand SignUpCommand { get; }
        public ICommand OpenSignInViewCommand { get; }

        public SignUpViewModel()
        {
            SignUpCommand = new RelayCommand(ExecuteSignUpCommand, CanExecuteSignUpCommand);
            OpenSignInViewCommand = new ViewModelCommand(ExecuteOpenSignInViewCommand);
        }

        private void ExecuteSignUpCommand(object obj)
        {

        }

        private bool CanExecuteSignUpCommand(object obj)
        {
            return false;
        }

        private void ExecuteOpenSignInViewCommand(object obj)
        {
            if (CheckAccountViewModel.Instance != null)
            {
                CheckAccountViewModel.Instance.NavigateToSignIn();
            }
        }
    }
}
