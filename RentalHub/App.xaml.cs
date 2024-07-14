using RentalHub.Model;
using RentalHub.Repositories;
using RentalHub.View;
using RentalHub.ViewModel;

using System;
using System.Net;
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

        private UserRepository userRepository;

        public App()
        {
            userRepository = new UserRepository();
        }

        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            // Attempt to retrieve saved credentials
            (string savedUsername, SecureString savedPassword) = CredentialManager.RetrieveCredentials();

            if (!string.IsNullOrEmpty(savedUsername) && savedPassword.Length != 0)
            {
                // Saved credentials found, attempt authentication
                AuthenticateSavedCredentials(savedUsername, savedPassword);
            }
            else
            {
                // No saved credentials found, initialize normal login flow
                InitializeLogin();
            }
        }

        private void AuthenticateSavedCredentials(string username, SecureString savedPassword)
        {
            try
            {
                UserModel authenticatedUser = userRepository.AuthenticateUser(new NetworkCredential(username, savedPassword));

                if (authenticatedUser != null)
                {
                    // User authenticated successfully, proceed to main window
                    mainViewModel = new MainViewModel(authenticatedUser);
                    mainView = new MainWindow { DataContext = mainViewModel };

                    // Register logout event
                    mainViewModel.LogoutRequested += MainViewModel_LogoutRequested;

                    mainView.Show();
                }
                else
                {
                    // Authentication failed, fallback to normal login flow
                    InitializeLogin();
                }
            }
            catch (Exception ex)
            {
                // Log the exception (if you have a logging mechanism)
                MessageBox.Show("An error occurred during authentication: " + ex.Message);
                InitializeLogin();
            }
        }

        private void InitializeLogin()
        {
            checkAccountViewModel = new CheckAccountViewModel();
            checkAccountView = new CheckAccountView { DataContext = checkAccountViewModel };

            checkAccountViewModel.LoginSuccessful += (s, user) =>
            {
                mainViewModel = new MainViewModel(user);
                mainView = new MainWindow { DataContext = mainViewModel };

                // Save credentials if remember me is checked
                if (rememberMe)
                {
                    CredentialManager.SaveCredentials(user.Username, securePassword);
                }
                else
                {
                    CredentialManager.ClearSavedCredentials();
                }

                // Register logout event
                mainViewModel.LogoutRequested += MainViewModel_LogoutRequested;

                mainView.Show();
                checkAccountView.Close();
            };

            checkAccountViewModel.RememberMeCheckBoxChecked += (s, args) =>
            {
                rememberMe = args.RememberMe;
                securePassword = args.Password;
            };

            checkAccountView.Show();
        }

        private void MainViewModel_LogoutRequested(object sender, EventArgs e)
        {
            

                InitializeLogin();


            // Close main window and reinitialize login
            mainView.Close();
        }

        // Remove this method to ensure the application closes when the main window is closed
        // private void MainView_Closed(object sender, EventArgs e)
        // {
        //     InitializeLogin();
        // }
    }
}
