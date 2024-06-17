using System.Windows;

using RentalHub.Model;
using RentalHub.Repositories;

namespace RentalHub.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        // Fields
        private UserAccountModel? _currentUserAccount;
        private IUserRepository? _userRepository;

        public UserAccountModel CurrentUserAccont
        {
            get { return _currentUserAccount; }
            set 
            { 
                _currentUserAccount = value; 
                OnPropertyChanged(nameof(CurrentUserAccont));
            }
        }

        public MainViewModel()
        {
            //_userRepository = new UserRepository();
            //_currentUserAccount = new UserAccountModel();
            //LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            var user = _userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if(user!=null)
            {
                CurrentUserAccont = new UserAccountModel()
                {
                    Username = user.Username,
                    DisplayName = $"Welcome {user.Name} {user.LastName} ;)",
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
