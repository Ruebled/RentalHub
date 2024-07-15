using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RentalHub.View
{
    public partial class InputDialogWindow : Window
    {
        public ViewModel.InputDialogWindowViewModel ViewModel { get; set; }

        public InputDialogWindow()
        {
            InitializeComponent();
            ViewModel = new ViewModel.InputDialogWindowViewModel();
            DataContext = ViewModel;

            Loaded += InputDialogWindow_Loaded;
        }

        private void InputDialogWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox1.Focus();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (textBox.Text.Length > 0)
                {
                    MoveFocusToNextTextBox(textBox);
                }
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                if (sender is TextBox textBox && string.IsNullOrEmpty(textBox.Text))
                {
                    MoveFocusToPreviousTextBox(textBox);
                }
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            return Regex.IsMatch(text, "[0-9]");
        }

        private void MoveFocusToNextTextBox(TextBox currentTextBox)
        {
            if (currentTextBox == TextBox1) TextBox2.Focus();
            else if (currentTextBox == TextBox2) TextBox3.Focus();
            else if (currentTextBox == TextBox3) TextBox4.Focus();
            else if (currentTextBox == TextBox4) TextBox5.Focus();
            else if (currentTextBox == TextBox5) TextBox6.Focus();
        }

        private void MoveFocusToPreviousTextBox(TextBox currentTextBox)
        {
            if (currentTextBox == TextBox2) TextBox1.Focus();
            else if (currentTextBox == TextBox3) TextBox2.Focus();
            else if (currentTextBox == TextBox4) TextBox3.Focus();
            else if (currentTextBox == TextBox5) TextBox4.Focus();
            else if (currentTextBox == TextBox6) TextBox5.Focus();
        }
    }
}
