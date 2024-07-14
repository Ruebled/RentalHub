using System.Windows;

namespace RentalHub.View
{
    /// <summary>
    /// Interaction logic for InputDialogWindow.xaml
    /// </summary>
    public partial class InputDialogWindow : Window
    {
        public ViewModel.InputDialogWindowViewModel ViewModel { get; set; }

        public InputDialogWindow()
        {
            InitializeComponent();
            ViewModel = new ViewModel.InputDialogWindowViewModel();
            DataContext = ViewModel;
        }
    }
}
