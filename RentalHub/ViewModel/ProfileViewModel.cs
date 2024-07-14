using Microsoft.Extensions.Primitives;

using RentalHub.Model;

using System.IO;
using System.Security;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RentalHub.ViewModel
{
    public class ProfileViewModel : ViewModelBase
    {
        private UserModel _user;

        public UserModel User
        {
            get => _user;
            set => _user = value;
        }

        private BitmapImage _profilePicture;
        public BitmapImage ProfilePicture
        {
            get => _profilePicture;
            set => _profilePicture = value;
        }

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

        private bool _isUsernameValid = true;
        private bool _isPasswordValid = true;
        private bool _isFirstNameValid = true;
        private bool _isLastNameValid = true;
        private bool _isEmailValid = true;
        private bool _isPhoneNumberValid = true;

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

        private string _createDate;
        public string CreateDate
        {
            get => _createDate;
            set => _createDate = value;
        }

        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                _isReadOnly = value;
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        public ICommand EditProfileCommand { get; set; }
        public ICommand SaveEditProfileCommand { get; set; }
        public ICommand CancelEditProfileCommand { get; set; }
        public ICommand DeleteProfileCommand { get; set; }

        public ProfileViewModel(UserModel user)
        {
            // Initialize properties and commands here
            _user = user;
            LoadUserProfileImage(user);
            FirstName = user.FirstName ?? "";
            LastName = user.LastName ?? "";
            Username = user.Username ?? "";
            Password = new SecureString();
            Email = user.Email ?? "";
            Phonenumber = user.PhoneNumber ?? "";
            Usertype = user.UserType ?? "";
            CreateDate = user.CreateDate ?? "";

            IsReadOnly = true;

            EditProfileCommand = new RelayCommand<object>(EditProfileExecute, CanEditProfileExecute);
            SaveEditProfileCommand = new RelayCommand<object>(ExecuteSaveEditProfile, CanExecuteSaveEditProfile);
            CancelEditProfileCommand = new RelayCommand<object>(ExecuteCancelEditProfile, CanExecuteCancelEditProfile);
            DeleteProfileCommand = new RelayCommand<object>(ExecuteDeleteProfile, CanExecuteDeleteProfile);
        }

        private void EditProfileExecute(object obj)
        {
            IsReadOnly = false;
        }

        private bool CanEditProfileExecute(object arg)
        {
            return true;
        }

        private void ExecuteSaveEditProfile(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteSaveEditProfile(object arg)
        {
            throw new NotImplementedException();
        }

        private void ExecuteCancelEditProfile(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteCancelEditProfile(object arg)
        {
            throw new NotImplementedException();
        }

        private void ExecuteDeleteProfile(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteDeleteProfile(object arg)
        {
            throw new NotImplementedException();
        }

        private void LoadUserProfileImage(UserModel User)
        {
            ImageRepository imageRepository = new ImageRepository();

            BitmapImage ProfilePhotoRetrieved = null;
            if (!string.IsNullOrEmpty(User.ImageID) && int.TryParse(User.ImageID, out int imageId))
            {
                ProfilePhotoRetrieved = imageRepository.GetImageById(imageId);
            }
            else
            {
                // Handle case where user.ImageID is null or not parseable to int
                BitmapImage ConvertFromLocal = ConvertPngToBitmapImage("user_icon.png");

                ProfilePhotoRetrieved = ConvertFromLocal;
            }

            if (User != null)
            {

                ProfilePicture = ProfilePhotoRetrieved;
            }
            else
            {
                MessageBox.Show("Invalid Shithead appeared");
                Application.Current.Shutdown();
            }
        }

        public BitmapImage ConvertPngToBitmapImage(string relativePath)
        {
            // Get the base directory of the current application domain
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Combine the base directory with the relative path to the images folder
            string imagesDirectory = Path.Combine(baseDirectory, "Icons"); // Replace "Images" with your actual folder name

            // Combine the images directory with the relative path to the image file
            string absolutePath = Path.Combine(imagesDirectory, relativePath);

            // Ensure the file exists
            if (!File.Exists(absolutePath))
            {
                throw new FileNotFoundException($"File not found: {absolutePath}");
            }

            // Create a BitmapImage from the file
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(absolutePath);
            bitmap.EndInit();

            return bitmap;
        }
    }
}