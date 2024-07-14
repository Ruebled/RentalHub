using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RentalHub.Utils
{
    public class BooleanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                // Define your string representations for true and false here
                return boolValue ? "True" : "False";
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return stringValue == "True";
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
