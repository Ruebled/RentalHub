using RentalHub.Model;
using RentalHub.Repositories;
using System.Windows.Input;

namespace RentalHub.ViewModel
{
    public class SupportViewModel : ViewModelBase
    {
        private UserModel _user;
        public UserModel User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private string _messageTitle;
        public string MessageTitle
        {
            get => _messageTitle;
            set
            {
                _messageTitle = value;
                OnPropertyChanged(nameof(MessageTitle));
            }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

        public ICommand ContactSupportCommand { get; set; }

        public SupportViewModel(UserModel User)
        {
            _user = User;

            // Initialize commands
            ContactSupportCommand = new RelayCommand<object>(ContactSupportExecute, CanContactSupportExecute);
        }

        private void ContactSupportExecute(object obj)
        {
            TicketModel ticket = new TicketModel();
            ticket.UserId = User.UserId;
            ticket.TicketTitle = MessageTitle;
            ticket.TicketMessage = Message;

            if(SupportRepository.Instance.InsertSupportTicket(ticket) == true)
            {
                StatusMessage = "Succesfully ticket delivery";
                Task.Run(async () =>
                {
                    await Task.Delay(3000);
                    StatusMessage = string.Empty;
                });

                MessageTitle = string.Empty;
                Message = string.Empty;
            }
            else
            {
                StatusMessage = "Ticket couldn't be send";

                Task.Run(async () =>
                {
                    await Task.Delay(4000);
                    StatusMessage = string.Empty;
                });
            }


        }

        private bool CanContactSupportExecute(object obj)
        {
            if (User== null 
                || string.IsNullOrEmpty(_messageTitle) 
                || string.IsNullOrEmpty(_message))
            {
                return false;
            }

            return true;
        }
    }
}
