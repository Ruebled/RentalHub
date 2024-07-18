using Microsoft.Xaml.Behaviors;

using System.Windows;

namespace RentalHub.Utils
{
    public class FocusBehavior : Behavior<UIElement>
    {
        public bool IsFocused
        {
            get { return (bool)GetValue(IsFocusedProperty); }
            set { SetValue(IsFocusedProperty, value); }
        }

        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.Register("IsFocused", typeof(bool), typeof(FocusBehavior), new PropertyMetadata(false, OnIsFocusedChanged));

        private static void OnIsFocusedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FocusBehavior behavior && behavior.IsFocused && behavior.AssociatedObject != null)
            {
                behavior.AssociatedObject.Focus();
            }
        }
    }
}