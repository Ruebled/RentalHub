using RentalHub.Model;
using RentalHub.Repositories;

using System.Runtime.InteropServices;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class SignUpViewModel : ViewModelBase
    {
        // Private Fields
        private string _username;
        private SecureString _password;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phonenumber;
        private string _usertype;
        private string _profilephotoid;
        private string _errorMessage;
        private string _statusMessage;

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

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
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

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }


        public ICommand SignUpCommand { get; }
        public ICommand OpenSignInViewCommand { get; }

        public SignUpViewModel()
        {
            // Set default values for variables
            _username = string.Empty;
            _password = new SecureString();
            _firstName = string.Empty;
            _lastName = string.Empty;
            _email = string.Empty;
            _phonenumber = string.Empty;
            _usertype = "Guest";
            _profilephotoid = "1";
            _errorMessage = string.Empty;
            _statusMessage = string.Empty;

            // Initialise user repository to access CRUD functionality
            userRepository = new UserRepository();

            // Set Up triggerable commands
            SignUpCommand = new RelayCommand(ExecuteSignUpCommand, CanExecuteSignUpCommand);
            OpenSignInViewCommand = new RelayCommand(ExecuteOpenSignInViewCommand);
        }
        private void ExecuteOpenSignInViewCommand(object obj)
        {
            CheckAccountViewModel.Instance?.NavigateToSignIn();
        }

        private void ExecuteSignUpCommand(object obj)
        {
            // Reset Status Message Text
            StatusMessage = string.Empty;

            UserModel userModel = new UserModel();

            System.Net.NetworkCredential userCredential = new System.Net.NetworkCredential(Username, Password);

            userModel.Username = Username;
            userModel.PasswordHash = userRepository.HashPassword(userCredential.Password);
            userModel.Email = Email;
            userModel.PhoneNumber = Phonenumber;
            userModel.FullName = FirstName + " " + LastName;
            userModel.UserType = Usertype;
            userModel.ImageID = Profilephotoid;

            userRepository.Add(userModel);

            // Set Status Message to successfull
            StatusMessage = "User Added Successfully";
        }

        private bool CanExecuteSignUpCommand(object obj)
        {
            bool status = 
                !CheckUsername(Username, out _errorMessage) ||
                !CheckPassword(Password, out _errorMessage) ||
                !CheckFirstName(FirstName, out _errorMessage) ||
                !CheckLastName(LastName, out _errorMessage) ||
                !CheckEmail(Email, out _errorMessage) ||
                !CheckPhoneNumber(Phonenumber, out _errorMessage);

            OnPropertyChanged(nameof(ErrorMessage));

            return !status;
        }

        private static bool CheckUsername(string username, out string errorMessage)
        {
            if (string.IsNullOrEmpty(username))
            {
                errorMessage = string.Empty;
                return false;
            }

            if (username.Length < 3 || username.Length > 30)
            {
                errorMessage = "Username must be between 3 and 30 characters.";
                return false;
            }

            // Check if username contains only alphanumeric characters and underscores
            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
            {
                errorMessage = "Username must only contain alphanumeric characters and underscores.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        private static bool CheckPassword(SecureString password, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (password == null || password.Length == 0)
            {
                errorMessage = string.Empty;
                return false;
            }

            // Check length
            if (password.Length < 8)
            {
                errorMessage = "Password must be at least 8 characters long.";
                return false;
            }

            // Check complexity requirements
            bool hasUpperCase = false;
            bool hasLowerCase = false;
            bool hasDigit = false;
            bool hasSpecialChar = false;

            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.SecureStringToGlobalAllocUnicode(password);
                string plainPassword = Marshal.PtrToStringUni(ptr);

                foreach (char c in plainPassword)
                {
                    if (char.IsUpper(c))
                        hasUpperCase = true;
                    else if (char.IsLower(c))
                        hasLowerCase = true;
                    else if (char.IsDigit(c))
                        hasDigit = true;
                    else if (!char.IsLetterOrDigit(c)) // Special characters
                        hasSpecialChar = true;

                    // Early exit if all requirements met
                    if (hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar)
                        break;
                }
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(ptr);
                }
            }

            if (!(hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar))
            {
                errorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.";
                return false;
            }

            return true;
        }

        private static bool CheckFirstName(string firstName, out string errorMessage)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                errorMessage = string.Empty;
                return false;
            }

            if (firstName.Length < 2 || firstName.Length > 50)
            {
                errorMessage = "First name must be between 2 and 50 characters.";
                return false;
            }

            // Check if first name contains only letters and optionally spaces or hyphens
            if (!Regex.IsMatch(firstName, @"^[a-zA-Z]+(?:[-\s][a-zA-Z]+)*$"))
            {
                errorMessage = "First name must only contain letters and optionally spaces or hyphens.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        private static bool CheckLastName(string lastName, out string errorMessage)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                errorMessage = string.Empty;
                return false;
            }

            if (lastName.Length < 2 || lastName.Length > 50)
            {
                errorMessage = "Last name must be between 2 and 50 characters.";
                return false;
            }

            // Check if last name contains only letters and optionally spaces or hyphens
            if (!Regex.IsMatch(lastName, @"^[a-zA-Z]+(?:[-\s][a-zA-Z]+)*$"))
            {
                errorMessage = "Last name must only contain letters and optionally spaces or hyphens.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }


        private static bool CheckEmail(string email, out string errorMessage)
        {
            if (string.IsNullOrEmpty(email))
            {
                errorMessage = string.Empty;
                return false;
            }

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (Regex.IsMatch(email, emailPattern))
            {
                errorMessage = string.Empty;
                return true;
            }
            else
            {
                errorMessage = "Invalid email format.";
                return false;
            }
        }

        private static bool CheckPhoneNumber(string phoneNumber, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(phoneNumber))
            {
                errorMessage = string.Empty;
                return false;
            }

            // Remove non-digit characters from phone number
            string digitsOnly = new string(phoneNumber.Where(char.IsDigit).ToArray());

            // Romanian phone numbers should start with 0 or +40
            if (!digitsOnly.StartsWith("0") && !digitsOnly.StartsWith("40"))
            {
                errorMessage = "Invalid phone number format. Must start with '0' or '+40'.";
                return false;
            }

            // Check length constraints for Romanian phone numbers
            if (digitsOnly.StartsWith("0") && (digitsOnly.Length != 10 && digitsOnly.Length != 12))
            {
                errorMessage = "Invalid phone number length for local format (should be 10 or 12 digits).";
                return false;
            }
            else if (digitsOnly.StartsWith("40") && (digitsOnly.Length != 11 && digitsOnly.Length != 13))
            {
                errorMessage = "Invalid phone number length for international format (should be 11 or 13 digits).";
                return false;
            }

            return true;
        }

    }
}
