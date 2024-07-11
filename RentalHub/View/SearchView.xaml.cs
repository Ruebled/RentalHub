using RentalHub.ViewModel;

using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace RentalHub.View
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
            DataContext = new SearchViewModel(); // Set the DataContext to an instance of SearchViewModel
        }

        private void Button_View_Details_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenCalendar_Click(object sender, RoutedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            if (datePicker != null)
            {
                datePicker.IsDropDownOpen = true;
            }
        }
    }
}
