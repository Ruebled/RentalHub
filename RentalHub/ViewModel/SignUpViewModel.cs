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

        private bool _isUsernameValid       = true;
        private bool _isPasswordValid       = true;
        private bool _isFirstNameValid      = true;
        private bool _isLastNameValid       = true;
        private bool _isEmailValid          = true;
        private bool _isPhoneNumberValid    = true;

        private IUserRepository userRepository;

        // Properties
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                _isUsernameValid = false;
                OnPropertyChanged(nameof(Username));
            }
        }

        public SecureString Password
        {
            get => _password;
            set
            {
                _password = value;
                _isPasswordValid = false;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                _isFirstNameValid = false;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                _isLastNameValid = false;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                _isEmailValid = false;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Phonenumber
        {
            get => _phonenumber;
            set
            {
                _phonenumber = value;
                _isPhoneNumberValid = false;
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
            userModel.FirstName = FirstName;
            userModel.LastName = LastName;
            userModel.PasswordHash = userRepository.HashPassword(userCredential.Password);
            userModel.Email = Email;
            userModel.PhoneNumber = Phonenumber;
            userModel.UserType = Usertype;
            userModel.ImageID = Profilephotoid;

            userRepository.Add(userModel);

            // Reset data
            Username = "";
            FirstName = "";
            LastName = "";
            Password.Clear();
            Email = "";
            Phonenumber = "";
            Usertype = "Guest";
            Profilephotoid = "1";

            // Set Status Message to successfull
            StatusMessage = "User Added Successfully";
        }

        private bool CanExecuteSignUpCommand(object obj)
        {
            bool isValid = true;

            // Check each field individually and update errorMessage only if validation fails
            if (!_isUsernameValid && !CheckUsername(Username, out _errorMessage))
            {
                isValid = false;
            }
            else if (!_isPasswordValid && !CheckPassword(Password, out _errorMessage))
            {
                isValid = false;
            }
            else if (!_isFirstNameValid && !CheckFirstName(FirstName, out _errorMessage))
            {
                isValid = false;
            }
            else if (!_isLastNameValid && !CheckLastName(LastName, out _errorMessage))
            {
                isValid = false;
            }
            else if (!_isEmailValid && !CheckEmail(Email, out _errorMessage))
            {
                isValid = false;
            }
            else if (!_isPhoneNumberValid && !CheckPhoneNumber(Phonenumber, out _errorMessage))
            {
                isValid = false;
            }

            OnPropertyChanged(nameof(ErrorMessage));

            return isValid;
        }


        private bool CheckUsername(string username, out string errorMessage)
        {
            if (string.IsNullOrEmpty(username))
            {
                _isUsernameValid = true;
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

            if(userRepository.GetByUsername(username) != null)
            {
                errorMessage = "Username already exist";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        private bool CheckPassword(SecureString password, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (password == null || password.Length == 0)
            {
                _isPasswordValid = true;
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

        private bool CheckFirstName(string firstName, out string errorMessage)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                _isFirstNameValid = true;
                errorMessage = string.Empty;
                return false;
            }

            if (firstName.Length < 3 || firstName.Length > 50)
            {
                errorMessage = "First name must be between 3 and 50 characters.";
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

        private bool CheckLastName(string lastName, out string errorMessage)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                _isLastNameValid = true;
                errorMessage = string.Empty;
                return false;
            }

            if (lastName.Length < 3 || lastName.Length > 50)
            {
                errorMessage = "Last name must be between 3 and 50 characters.";
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

        private bool CheckEmail(string email, out string errorMessage)
        {
            if (string.IsNullOrEmpty(email))
            {
                _isEmailValid = true;
                errorMessage = string.Empty;
                return false;
            }

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(email, emailPattern))
            {
                errorMessage = "Invalid email format.";
                return false;
            }

            if (userRepository.GetByEmail(email) != null)
            {
                errorMessage = "Email already used";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        private bool CheckPhoneNumber(string phoneNumber, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(phoneNumber))
            {
                _isPhoneNumberValid = true;
                errorMessage = "";
                return false;
            }

            // Check for invalid characters
            if (phoneNumber.Any(c => !char.IsDigit(c) && c != '-' && c != '+'))
            {
                errorMessage = "Phone number should only contain digits, hyphens (-), or a leading plus sign (+).";
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

            // Check hyphen format
            if (phoneNumber.Count(c => c == '-') > 2)
            {
                errorMessage = "Invalid hyphen format. Only two hyphen (-) allowed.";
                return false;
            }
            else if (phoneNumber.Contains("--"))
            {
                errorMessage = "Invalid hyphen format. Only one consecutive hyphen (-) allowed.";
                return false;
            }
            else if (phoneNumber.EndsWith("-"))
            {
                errorMessage = "Hyphen (-) should not be at the end of the phone number.";
                return false;
            }

            return true;
        }

    }
}
