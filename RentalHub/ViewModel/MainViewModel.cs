using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using RentalHub.Model;
using RentalHub.Repositories;

namespace RentalHub.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        // Fields
        private UserAccountModel? _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private string _icon_source;

        private IUserRepository? _userRepository;

        // Properties
        public UserAccountModel CurrentUserAccont
        {
            get { return _currentUserAccount; }
            set 
            { 
                _currentUserAccount = value; 
                OnPropertyChanged(nameof(CurrentUserAccont));
            }
        }

        public ViewModelBase CurrentChildView 
        { 
            get => _currentChildView;
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public string Caption 
        { 
            get => _caption;
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public string IconSource
        { 
            get => _icon_source; 
            set 
            {
                _icon_source = value;
                OnPropertyChanged(nameof(IconSource));
            } 
        }

        //--> Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowSearchViewCommand { get; }
        public ICommand ShowBookingsViewCommand { get; }
        public ICommand ShowProfileViewCommand { get; }
        public ICommand ShowSupportViewCommand { get; }

        public MainViewModel()
        {
            _userRepository = new UserRepository();
            _currentUserAccount = new UserAccountModel();

            // Initialise commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowSearchViewCommand = new ViewModelCommand(ExecuteShowSearchViewCommand);
            ShowBookingsViewCommand = new ViewModelCommand(ExecuteShowBookingsViewCommand);
            ShowProfileViewCommand = new ViewModelCommand(ExecuteShowProfileViewCommand);
            ShowSupportViewCommand = new ViewModelCommand(ExecuteShowSupportViewCommand);

            // Default view
            ExecuteShowHomeViewCommand(null);

            LoadCurrentUserData();
        }

        private void ExecuteShowSearchViewCommand(object obj)
        {
            CurrentChildView = new SearchViewModel();
            Caption = "Search";
            IconSource = "/Icons/search_icon.png";
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Dashboard";
            IconSource = "/Icons/home_icon.png";
        }
        private void ExecuteShowBookingsViewCommand(object obj)
        {
            CurrentChildView = new BookingsViewModel();
            Caption = "Bookings";
            IconSource = "/Icons/book_icon.png";
        }
        private void ExecuteShowProfileViewCommand(object obj)
        {
            CurrentChildView = new ProfileViewModel();
            Caption = "Profile";
            IconSource = "/Icons/user_icon.png";
        }
        private void ExecuteShowSupportViewCommand(object obj)
        {
            CurrentChildView = new SupportViewModel();
            Caption = "Support";
            IconSource = "/Icons/support_icon.png";
        }

        private void LoadCurrentUserData()
        {
            var user = _userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);

            ImageRepository imageRepository = new ImageRepository();

            BitmapImage ProfilePhotoRetrieved = null;
            if (!string.IsNullOrEmpty(user.ImageID) && int.TryParse(user.ImageID, out int imageId))
            {
                ProfilePhotoRetrieved = imageRepository.GetImageById(imageId);
            }
            else
            {
                // Handle case where user.ImageID is null or not parseable to int
                BitmapImage ConvertFromLocal = ConvertPngToBitmapImage("user_icon.png");

                ProfilePhotoRetrieved = ConvertFromLocal;
            }

            if (user!=null)
            {
                CurrentUserAccont = new UserAccountModel()
                {
                    Username = user.Username,
                    DisplayName = $"{user.FullName}",
                    ProfilePicture = ProfilePhotoRetrieved
                };
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
