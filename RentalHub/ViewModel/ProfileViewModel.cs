using RentalHub.Model;
using RentalHub.ViewModel;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
namespace RentalHub.ViewModel
{
    public class ProfileViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }

        private string _profilePicture;
        public string ProfilePicture
        {
            get { return _profilePicture; }
            set { _profilePicture = value; OnPropertyChanged(); }
        }

        public ICommand EditProfileCommand { get; set; }

        public ProfileViewModel()
        {
            // Initialize properties and commands here
            UserName = "John Doe";
            Email = "john.doe@example.com";
            ProfilePicture = "Images/profile.jpg"; // Example path to profile picture

            EditProfileCommand = new RelayCommand<object>(EditProfileExecute, CanEditProfileExecute);
        }

        private void EditProfileExecute(object obj)
        {
            // Implement edit profile logic
        }

        private bool CanEditProfileExecute(object obj)
        {
            // Implement logic to determine if editing profile is allowed
            return true; // For demonstration purposes, always allow editing
        }

        // Implement INotifyPropertyChanged interface for property change notification
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}