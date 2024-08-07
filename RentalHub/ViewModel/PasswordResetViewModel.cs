﻿using Microsoft.Extensions.Configuration;

using RentalHub.Model;
using RentalHub.Repositories;
using RentalHub.Utils;

using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class PasswordResetViewModel : ViewModelBase
    {
        private string _usernameOrEmail;
        private string _errorMessage;
        private string _statusMessage;
        private IUserRepository userRepository;

        private bool textChanged = false;

        public string UsernameOrEmail
        {
            get { return _usernameOrEmail; }
            set
            {
                _usernameOrEmail = value;
                OnPropertyChanged(nameof(UsernameOrEmail));
                textChanged = true;
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

        public ICommand ResetPasswordCommand { get; }
        public ICommand OpenSignInViewCommand { get; }

        public PasswordResetViewModel()
        {
            userRepository = new UserRepository();

            ResetPasswordCommand = new RelayCommand<object>(ExecuteResetPasswordCommand, CanExectuteResetPasswordCommand);
            OpenSignInViewCommand = new RelayCommand<object>(ExecuteOpenSignInViewCommand);

            IsTextBoxFocused = true;
        }

        private void ExecuteOpenSignInViewCommand(object obj)
        {
            if (CheckAccountViewModel.Instance != null)
            {
                CheckAccountViewModel.Instance.NavigateToSignIn();
            }
        }

        private bool CanExectuteResetPasswordCommand(object arg)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (string.IsNullOrEmpty(UsernameOrEmail))
            {
                return false;
            }

            if (!textChanged)
            {
                return false;
            }

            if (UsernameOrEmail.Contains("@"))
            {
                if (Regex.IsMatch(UsernameOrEmail, emailPattern))
                {
                    ErrorMessage = string.Empty; 
                    return true;
                }
                else
                {
                    ErrorMessage = "Invalid email format.";
                    return false;
                }
            }
            else
            {
                if (UsernameOrEmail.Length >= 3)
                {
                    ErrorMessage = string.Empty;
                    return true;
                }
                else
                {
                    ErrorMessage = "Username must be at least 3 characters long.";
                    return false;
                }
            }
        }

        private bool _isTextBoxFocused;
        public bool IsTextBoxFocused
        {
            get { return _isTextBoxFocused; }
            set
            {
                _isTextBoxFocused = value;
                OnPropertyChanged(nameof(IsTextBoxFocused));
            }
        }

        private void ExecuteResetPasswordCommand(object obj)
        {
            UserModel user;
            StatusMessage = string.Empty;

            if (UsernameOrEmail.Contains('@'))
            {
                user = userRepository.GetByEmail(UsernameOrEmail);
            }
            else
            {
                user = userRepository.GetByUsername(UsernameOrEmail);
            }

            if (user == null || user.UserId == null)
            {
                ErrorMessage = "Unknown user";
                textChanged = false;
                return;
            }
            
            ErrorMessage = string.Empty;

            string Password = PasswordGenerator.GeneratePassword(16);
            if (string.IsNullOrEmpty(Password))
            {
                Debug.Print("Password Generator generated an empty password");
                return;
            }

            // Send new password to the user email
            bool status = EmailSender.Instance.SendPasswordResetEmail(user.Email, Password);

            if (status)
            {
                // Update password in database
                userRepository.UpdateUserPassword(user.UserId, PasswordHasher.HashPassword(Password));
                textChanged = false;
                StatusMessage = "Password updated successfully\nCheck your email for the new password";
            }
            else
            {
                textChanged = false;
                StatusMessage = "Password could not be generated\nCall in the support team";
            }
            UsernameOrEmail = string.Empty;
        }
    }
}
