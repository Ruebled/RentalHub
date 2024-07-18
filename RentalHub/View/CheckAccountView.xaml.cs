using System.Windows;
using System.Windows.Input;

namespace RentalHub.View
{
    /// <summary>
    /// Interaction logic for CheckAccount.xaml
    /// </summary>
    public partial class CheckAccountView : Window
    {
        public CheckAccountView()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
