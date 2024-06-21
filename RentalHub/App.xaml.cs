using RentalHub.View;

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
            //var mainView = new MainWindow();
            //mainView.Show();
            var loginview = new LoginView();
            loginview.Show();
            loginview.IsVisibleChanged += (s, ev) =>
            {
                if (loginview.IsVisible == false && loginview.IsLoaded)
                {
                    var mainView = new MainWindow();
                    mainView.Show();
                    loginview.Close();
                }
            };
        }
    }
}
