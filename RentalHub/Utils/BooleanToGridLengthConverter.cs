using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RentalHub.Utils
{
    public class BooleanToGridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
            {
                throw new ArgumentException("Value must be a boolean.");
            }

            bool isVisible = (bool)value;
            return isVisible ? GridLength.Auto : new GridLength(1, GridUnitType.Star);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
