using RentalHub.Model;

using System.Security;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class CheckAccountViewModel : ViewModelBase
    {
        public static CheckAccountViewModel Instance { get; private set; }

        private ViewModelBase _currentChildView;
        public event EventHandler<UserModel> LoginSuccessful;
        public event EventHandler<bool> RememberMeCheckBoxChecked;
        public CheckAccountViewModel()
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

        public virtual void OnCheckBoxChecked(bool State)
        {
            RememberMeCheckBoxChecked?.Invoke(this, State);
        }
    }
}
