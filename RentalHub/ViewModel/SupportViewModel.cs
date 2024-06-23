using RentalHub.Model;
using RentalHub.ViewModel;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class SupportViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ICommand ContactSupportCommand { get; set; }

        public SupportViewModel()
        {
            // Initialize commands
            ContactSupportCommand = new RelayCommand(ContactSupportExecute, CanContactSupportExecute);
        }

        private void ContactSupportExecute(object obj)
        {
            // Implement logic to contact support
            MessageBox.Show("Contacting support...");
            // Example: Navigate to a support page or open a support chat window
        }

        private bool CanContactSupportExecute(object obj)
        {
            // Implement logic to determine if contacting support is allowed
            return true; // For demonstration purposes, always allow contacting support
        }

        // Implement INotifyPropertyChanged interface for property change notification
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
