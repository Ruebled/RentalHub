using RentalHub.Model;
using RentalHub.Repositories;

using System.Security;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class SignUpViewModel : ViewModelBase
    {
        public ICommand OpenSignInViewCommand { get; }

        public SignUpViewModel()
        {
           OpenSignInViewCommand = new ViewModelCommand(ExecuteOpenSignInViewCommand);
        }

        private void ExecuteOpenSignInViewCommand(object obj)
        {
            if (CheckAccountViewModel.Instance != null)
            {
                CheckAccountViewModel.Instance.NavigateToSignIn();
            }
        }
    }
}
