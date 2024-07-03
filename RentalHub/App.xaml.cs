using RentalHub.View;
using RentalHub.ViewModel;

using System.Windows;

namespace RentalHub
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var loginViewModel = new LoginViewModel();
            var loginView = new LoginView { DataContext = loginViewModel };

            loginViewModel.LoginSuccessful += (s, user) =>
            {
                var mainViewModel = new MainViewModel(user);
                var mainView = new MainWindow { DataContext = mainViewModel };
                mainView.Show();
                loginView.Close();
            };

            loginView.Show();
        }
    }
}
