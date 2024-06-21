using System.Windows;
using System.Windows.Input;

using FontAwesome.Sharp;

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
        public ICommand ShowHomeViewComand { get; }
        public ICommand ShowSearchViewCommand { get; }

        public MainViewModel()
        {
            _userRepository = new UserRepository();
            _currentUserAccount = new UserAccountModel();

            // Initialise commands
            ShowHomeViewComand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowSearchViewCommand = new ViewModelCommand(ExecuteShowSearchViewCommand);

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

        private void LoadCurrentUserData()
        {
            var user = _userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if(user!=null)
            {
                CurrentUserAccont = new UserAccountModel()
                {
                    Username = user.Username,
                    DisplayName = $"{user.Name} {user.LastName}",
                    ProfilePicture = null
                };

            }
            else
            {
                MessageBox.Show("Invalid Shithead appeared");
                Application.Current.Shutdown();
            }
        }
    }
}
