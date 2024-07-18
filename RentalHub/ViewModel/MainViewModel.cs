using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using RentalHub.Model;
using RentalHub.Repositories;

namespace RentalHub.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        // Static fields
        public static MainViewModel Instance { get; set; }
        private static Stack<ViewModelBase> ViewsStack = new Stack<ViewModelBase>();

        public event EventHandler LogoutRequested;

        // Fields
        private ViewModelBase _currentChildView;
        private UserModel _user;
        private UserAccountModel? _currentUserAccount;
        private string _caption;
        private string _icon_source;

        private IUserRepository? _userRepository;

        // Properties
        public UserModel User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public UserAccountModel CurrentUserAccount
        {
            get { return _currentUserAccount; }
            set 
            { 
                _currentUserAccount = value; 
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public ViewModelBase CurrentChildView
        {
            get => _currentChildView;
            set
            {
                if (_currentChildView != value)
                {
                    _currentChildView = value;
                    OnPropertyChanged(nameof(CurrentChildView));
                }
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

        private bool _isUserOptionActive;

        public bool IsUserOptionActive
        {
            get => _isUserOptionActive;
            set
            {
                _isUserOptionActive = value;
                OnPropertyChanged(nameof(IsUserOptionActive));
            }
        }

        private bool _userProfileOpened;
        public bool UserProfileOpened
        {
            get => _userProfileOpened;
            set
            {
                _userProfileOpened = value;
                OnPropertyChanged(nameof(UserProfileOpened));
            }
        }

        //--> Commands
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowSearchViewCommand { get; }
        public ICommand ShowBookingsViewCommand { get; }
        public ICommand ShowProfileViewCommand { get; }
        public ICommand ShowSupportViewCommand { get; }
        public ICommand ToggleUserOption {  get; }
        public ICommand UserLoggout {  get; }
        public ICommand OpenSetting {  get; }


        public ICommand MouseLeftClickCommand { get; }

        public MainViewModel(UserModel user)
        {
            // Ensure static reference to this class
            Instance = this;

            User = user;

            // Init View Stack
            ViewsStack = new Stack<ViewModelBase>();

            _userRepository = new UserRepository();
            _currentUserAccount = new UserAccountModel();

            // Initialise commands
            ShowHomeViewCommand = new RelayCommand<object>(ExecuteShowHomeViewCommand);
            ShowSearchViewCommand = new RelayCommand<object>(ExecuteShowSearchViewCommand);
            ShowBookingsViewCommand = new RelayCommand<object>(ExecuteShowBookingsViewCommand);
            ShowProfileViewCommand = new RelayCommand<object>(ExecuteShowProfileViewCommand);
            ShowSupportViewCommand = new RelayCommand<object>(ExecuteShowSupportViewCommand);

            // Command which make the settings & loggout box visible and actionable
            ToggleUserOption = new RelayCommand<object>(ExecuteToggleUserOption);

            // Loggout option
            UserLoggout = new RelayCommand<object>(ExecuteLogoutCommand);

            // Hide user option on mouse click
            MouseLeftClickCommand = new RelayCommand<object>(ExecuteHideUserOption);

            // Open settings from the user dropdown
            OpenSetting = new RelayCommand<object>(ExecuteOpenSettings);

            // Default view
            ExecuteShowHomeViewCommand(null);

            CurrentChildView ??= new HomeViewModel();

            LoadCurrentUserData();

            IsUserOptionActive = false;
            UserProfileOpened = false;
        }

        private void ExecuteHideUserOption(object obj)
        {
            IsUserOptionActive = false;
        }

        public event EventHandler OnLogout;

        private void ExecuteLogoutCommand(object obj)
        {
            _user = null;
            _currentUserAccount = null;

            LogoutRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ExecuteToggleUserOption(object obj)
        {
            IsUserOptionActive = !IsUserOptionActive;
        }

        public void PushView(ViewModelBase viewToApply)
        {
            ViewsStack.Push(CurrentChildView);
            CurrentChildView = viewToApply;
        }

        public void PopView()
        {
            if (ViewsStack.TryPop(out ViewModelBase? previousView))
            {
                CurrentChildView = previousView;
            }
            else
            {
                throw new Exception("No views left in the stack");
            }
        }

        private void ExecuteShowSearchViewCommand(object obj)
        {
            CurrentChildView = new SearchViewModel();
            Caption = "Search";
            IconSource = "/Icons/search_icon.png";
            UserProfileOpened = false;
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Dashboard";
            IconSource = "/Icons/home_icon.png";
            UserProfileOpened = false;
        }
        private void ExecuteShowBookingsViewCommand(object obj)
        {
            CurrentChildView = new BookingsViewModel(User);
            Caption = "Bookings";
            IconSource = "/Icons/book_icon.png";
            UserProfileOpened = false;
        }

        private void ExecuteOpenSettings(object obj)
        {
            if (!UserProfileOpened)
            {
                UserProfileOpened = true;
                ExecuteShowProfileViewCommand(obj);
            }
        }

        private void ExecuteShowProfileViewCommand(object obj)
        {

            CurrentChildView = new ProfileViewModel(User);
            Caption = "Profile";
            IconSource = "/Icons/user_icon.png";

            IsUserOptionActive = false;

        }
        private void ExecuteShowSupportViewCommand(object obj)
        {
            CurrentChildView = new SupportViewModel(User);
            Caption = "Support";
            IconSource = "/Icons/support_icon.png";
            UserProfileOpened = false;
        }

        private void LoadCurrentUserData()
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

            if (User!=null)
            {
                CurrentUserAccount = new UserAccountModel()
                {
                    Username = User.Username,
                    DisplayName = $"{User.FirstName}" + " " + $"{User.LastName}",
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
