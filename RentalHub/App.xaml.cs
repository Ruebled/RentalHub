using RentalHub.Model;
using RentalHub.Repositories;
using RentalHub.View;
using RentalHub.ViewModel;

using System.Security;
using System.Windows;

namespace RentalHub
{
    public partial class App : Application
    {
        // View models and views
        private CheckAccountViewModel checkAccountViewModel;
        private CheckAccountView checkAccountView;
        private MainViewModel mainViewModel;
        private MainWindow mainView;

        // Flags and secure storage
        private bool rememberMe;  // Flag to remember user login
        private SecureString securePassword;  // Securely store password if needed

        private UserRepository userRepository;  // User repository for authentication

        public App()
        {
            userRepository = new UserRepository();  // Initialize the user repository
        }

        // Application startup event handler
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            // Attempt to retrieve saved credentials
            (string savedUsername, string savedPassword) = CredentialManager.RetrieveCredentials();

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

        // Authenticate user with saved credentials
        private void AuthenticateSavedCredentials(string username, string savedPassword)
        {
            try
            {
                UserModel authenticatedUser = userRepository.AuthenticateUser(username, savedPassword);

                if (authenticatedUser != null)
                {
                    // User authenticated successfully, initialize main window
                    InitializeMainWindow(authenticatedUser);
                }
                else
                {
                    // Authentication failed, fallback to normal login flow
                    InitializeLogin();
                }
            }
            catch (Exception ex)
            {
                // Log and show error message
                MessageBox.Show("An error occurred during authentication: " + ex.Message);
                InitializeLogin();
            }
        }

        // Initialize the login view and handle its events
        private void InitializeLogin()
        {
            checkAccountViewModel = new CheckAccountViewModel();
            checkAccountView = new CheckAccountView { DataContext = checkAccountViewModel };

            // Subscribe to events from the login view model
            checkAccountViewModel.LoginSuccessful += HandleLoginSuccess;
            checkAccountViewModel.RememberMeCheckBoxChecked += HandleRememberMeCheckBoxChecked;

            // Show the login view
            checkAccountView.Show();
        }

        // Handle successful login event
        private void HandleLoginSuccess(object sender, UserModel user)
        {
            // Initialize main window with authenticated user
            InitializeMainWindow(user);

            // Save or clear credentials based on remember me option
            if (rememberMe)
            {
                CredentialManager.SaveCredentials(user.Username, user.PasswordHash);
            }
            else
            {
                CredentialManager.ClearSavedCredentials();
            }

            // Close the login view
            checkAccountView.Close();
        }

        // Initialize the main window with given user
        private void InitializeMainWindow(UserModel user)
        {
            mainViewModel = new MainViewModel(user);
            mainView = new MainWindow { DataContext = mainViewModel };

            // Subscribe to logout event from main view model
            mainViewModel.LogoutRequested += HandleLogoutRequested;

            // Show the main window
            mainView.Show();
        }

        // Handle logout requested event
        private void HandleLogoutRequested(object sender, EventArgs e)
        {
            // Clear saved credentials and reinitialize login
            CredentialManager.ClearSavedCredentials();
            InitializeLogin();

            // Close the main window
            mainView.Close();
        }

        // Handle remember me checkbox checked event
        private void HandleRememberMeCheckBoxChecked(object sender, bool remember)
        {
            // Update rememberMe flag
            rememberMe = remember;
        }
    }
}
