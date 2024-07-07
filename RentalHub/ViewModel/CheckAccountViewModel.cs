using RentalHub.Model;

namespace RentalHub.ViewModel
{
    public class CheckAccountViewModel : ViewModelBase
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static CheckAccountViewModel Instance { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private ViewModelBase _currentChildView;
        public event EventHandler<UserModel> LoginSuccessful;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CheckAccountViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Instance = this;
            NavigateToSignIn();
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

        public void NavigateToSignIn()
        {
            CurrentChildView = new SignInViewModel();
        }

        public void NavigateToSignUp()
        {
            CurrentChildView = new SignUpViewModel();
        }

        public void NavigatePasswordReset()
        {
            CurrentChildView = new PasswordResetViewModel();
        }

        public virtual void OnLoginSuccessful(UserModel user)
        {
            LoginSuccessful?.Invoke(this, user);
        }
    }
}
