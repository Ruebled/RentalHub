using RentalHub.View;
using RentalHub.ViewModel;

using System;
using System.Security;
using System.Windows;

namespace RentalHub
{
    public partial class App : Application
    {
        private CheckAccountViewModel checkAccountViewModel;
        private CheckAccountView checkAccountView;
        private MainViewModel mainViewModel;
        private MainWindow mainView;

        private bool rememberMe;
        private SecureString securePassword;

        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            InitializeLogin();
        }

        private void InitializeLogin()
        {
            checkAccountViewModel = new CheckAccountViewModel();
            checkAccountView = new CheckAccountView { DataContext = checkAccountViewModel };

            checkAccountViewModel.LoginSuccessful += (s, user) =>
            {
                mainViewModel = new MainViewModel(user);
                mainView = new MainWindow { DataContext = mainViewModel };

                if (rememberMe)
                {
                    // Convert SecureString to plain text password for saving (if needed)
                    string password = ConvertSecureStringToString(securePassword);

                    // Save credentials if remember me is checked
                    CredentialManager.SaveCredentials(user.Username, password);
                }
                else
                {
                    // Clear saved credentials if remember me is not checked
                    CredentialManager.ClearSavedCredentials();
                }
                /* Unsubscribe before it causes application shutdown
                 * which is not desired in this case
                 */
                checkAccountView.Closed -= CheckAccountView_Closed;

                mainView.Closed += MainView_Closed;
                mainView.Show();
                checkAccountView.Close();
            };

            checkAccountViewModel.RememberMeCheckBoxChecked += (s, args) =>
            {
                rememberMe = args.RememberMe;
                securePassword = args.Password;
            };

            checkAccountView.Closed += CheckAccountView_Closed;
            checkAccountView.Show();
        }

        private void CheckAccountView_Closed(object sender, EventArgs e)
        {
            Shutdown();
        }

        private void MainView_Closed(object sender, EventArgs e)
        {
            InitializeLogin();
        }

        private string ConvertSecureStringToString(SecureString secureString)
        {
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(secureString);
            try
            {
                return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
        }
    }
}
