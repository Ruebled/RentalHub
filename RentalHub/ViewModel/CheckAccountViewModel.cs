using RentalHub.Model;

namespace RentalHub.ViewModel
{
    public class CheckAccountViewModel : ViewModelBase
    {
        public static CheckAccountViewModel Instance = new CheckAccountViewModel();
        private ViewModelBase _currentChildView;
        public event EventHandler<UserModel>? LoginSuccessful;

        public CheckAccountViewModel()
        {
            CurrentChildView = new LoginViewModel();
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

        protected virtual void OnLoginSuccessful(UserModel user)
        {
            LoginSuccessful?.Invoke(this, user);
        }
    }
}
