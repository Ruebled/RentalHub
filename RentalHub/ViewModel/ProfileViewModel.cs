using RentalHub.Model;
using RentalHub.Repositories;
using RentalHub.Utils;
using RentalHub.View;

using System.Diagnostics;
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
        private IUserRepository _userRepository;

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

        private string _editSaveButtonText;
        public string EditSaveButtonText
        {
            get => _editSaveButtonText;
            set
            {
                _editSaveButtonText = value;
                OnPropertyChanged(nameof(EditSaveButtonText));
            }
        }

        public ICommand EditSaveProfileCommand { get; set; }
        public ICommand CancelEditProfileCommand { get; set; }
        public ICommand DeleteProfileCommand { get; set; }

        public ProfileViewModel(UserModel userReceived)
        {
            // Init a userRepository instance
            _userRepository = new UserRepository();
            // Initialize properties and commands here
            User = userReceived;
            // Set Default state for the window from the User Received Data
            RestoreUserSettingsState(User);

            // Set Edit State 
            IsReadOnly = true;
            EditSaveButtonText = "Edit";

            EditSaveProfileCommand = new RelayCommand<object>(EditSaveProfileExecute);
            CancelEditProfileCommand = new RelayCommand<object>(ExecuteCancelEditProfile);
            DeleteProfileCommand = new RelayCommand<object>(ExecuteDeleteProfile);
        }

        private async void EditSaveProfileExecute(object obj)
        {
            if (!IsReadOnly)
            {
                // If we are in the edit mode

                // Check if data has changed
                if (!HasUserDataChanged())
                {
                    StatusMessage = "No changes detected.";
                    await UpdateStatusMessageAsync(StatusMessage);
                    IsReadOnly = true;
                    EditSaveButtonText = "Edit";
                    return;
                }

                // Generate validation code
                string validationCode = PasswordGenerator.GenerateCode();
                if (string.IsNullOrEmpty(validationCode))
                {
                    StatusMessage = "Failed to generate validation code.";
                    await UpdateStatusMessageAsync(StatusMessage);
                    Debug.Print("Password Generator generated an empty code");
                    return;
                }

                // Send validation code to the user's email
                bool status = EmailSender.Instance.SendValidationCodeEmail(User.Email, validationCode);

                if (!status)
                {
                    StatusMessage = "Failed to send validation code to email.";
                    await UpdateStatusMessageAsync(StatusMessage);
                    return;
                }

                string codeFromUser = GetConfirmationCodeFromUser();

                if (codeFromUser == validationCode)
                {
                    UpdateUserSettings();
                    StatusMessage = "User Updated Successfully";
                    await UpdateStatusMessageAsync(StatusMessage);
                }
                else
                {
                    RestoreUserSettingsState(User);
                    StatusMessage = "Validation code mismatch. Changes reverted.";
                    await UpdateStatusMessageAsync(StatusMessage);
                }

                IsReadOnly = true;
                EditSaveButtonText = "Edit";
            }
            else
            {
                // If we are in the view mode, switch to edit mode
                IsReadOnly = false;
                EditSaveButtonText = "Save";
                StatusMessage = "Edit mode enabled.";
                await UpdateStatusMessageAsync(StatusMessage);
            }
        }

        private bool HasUserDataChanged()
        {
            return _user.FirstName != FirstName ||
                   _user.LastName != LastName ||
                   _user.Username != Username ||
                   _user.Email != Email ||
                   _user.PhoneNumber != Phonenumber ||
                   _user.ImageID != Profilephotoid ||
                   (_password.Length > 0 && !string.IsNullOrEmpty(new System.Net.NetworkCredential(string.Empty, Password).Password));
        }



        private void RestoreUserSettingsState(UserModel LocalUser)
        {
            if (LocalUser != null)
            {
                LoadUserProfileImage(User);
                FirstName = User.FirstName ?? "";
                LastName = User.LastName ?? "";
                Username = User.Username ?? "";
                Password = new SecureString();
                Email = User.Email ?? "";
                Phonenumber = User.PhoneNumber ?? "";
                Usertype = User.UserType ?? "";
                Profilephotoid = User.ImageID ?? "";
                CreateDate = User.CreateDate ?? "";
            }
            else
            {
                throw new Exception("User received is null something was changed");
            }
        }

        private void UpdateUserSettings()
        {
            if (User != null)
            {
                System.Net.NetworkCredential userCredential = new System.Net.NetworkCredential(Username, Password);

                User.Username = Username;
                User.FirstName = FirstName;
                User.LastName = LastName;
                // Update password only if it's not null or empty
                if (!string.IsNullOrEmpty(userCredential.Password))
                {
                    User.PasswordHash = _userRepository.HashPassword(userCredential.Password);
                }
                else
                {
                    User.PasswordHash = null;
                }
                User.Email = Email;
                User.PhoneNumber = Phonenumber;
                User.UserType = Usertype;
                User.ImageID = Profilephotoid;

                // Update User data
                _userRepository.Update(User);

                StatusMessage = "User Updated Successfully";
                UpdateStatusMessageAsync(StatusMessage);
            }
            else
            {
                throw new Exception("Something really messy happened");
            }
        }


        private string GetConfirmationCodeFromUser()
        {
            string confirmationCode = string.Empty;

            InputDialogWindow inputDialog = new InputDialogWindow();
            bool? result = inputDialog.ShowDialog();

            if (result == true) // User clicked OK
            {
                var viewModel = inputDialog.DataContext as InputDialogWindowViewModel;
                if (viewModel != null)
                {
                    confirmationCode = viewModel.UserInput;
                }
            }

            return confirmationCode;
        }

        private void ExecuteCancelEditProfile(object obj)
        {
            IsReadOnly = true;
            EditSaveButtonText = "Edit";
        }

        private async void ExecuteDeleteProfile(object obj)
        {
            // Generate validation code
            string validationCode = PasswordGenerator.GenerateCode();
            if (string.IsNullOrEmpty(validationCode))
            {
                StatusMessage = "Failed to generate validation code.";
                await UpdateStatusMessageAsync(StatusMessage);
                Debug.Print("Password Generator generated an empty code");
                return;
            }

            // Send validation code to the user's email
            bool status = EmailSender.Instance.SendDeletionConfirmationEmail(User.Email, validationCode);

            if (!status)
            {
                StatusMessage = "Failed to send validation code to email.";
                await UpdateStatusMessageAsync(StatusMessage);
                return;
            }

            string codeFromUser = GetConfirmationCodeFromUser();

            if (codeFromUser == validationCode)
            {
                DeleteUser();
                StatusMessage = "User Deleted Successfully, soon will be prompted to login page...";
                await UpdateStatusMessageAsync(StatusMessage);
                Logout();
            }
            else
            {
                StatusMessage = "Validation code mismatch. Changes reverted.";
                await UpdateStatusMessageAsync(StatusMessage);
            }
        }

        private void Logout()
        {
            MainViewModel.Instance.UserLoggout.Execute(null);
        }

        private void DeleteUser()
        {
            _userRepository.Remove(User);
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

        public async Task UpdateStatusMessageAsync(string message)
        {
            // Update StatusMessage
            StatusMessage = message;

            // Run a parallel task to clear StatusMessage after 5 seconds
            await Task.Run(async () =>
            {
                await Task.Delay(5000); // 5 seconds delay
                StatusMessage = string.Empty;
            });
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