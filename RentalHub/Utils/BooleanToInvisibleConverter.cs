﻿using System.Windows;
using System.Windows.Data;

namespace RentalHub.Utils
{
    public class BooleanToInvisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool booleanValue)
                return booleanValue ? Visibility.Collapsed : Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Visibility visibilityValue)
                return visibilityValue == Visibility.Collapsed;
            return false;
        }
    }
}