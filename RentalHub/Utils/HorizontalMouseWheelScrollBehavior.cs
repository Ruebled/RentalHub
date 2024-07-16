using Microsoft.Xaml.Behaviors;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RentalHub.Utils
{
    public class HorizontalMouseWheelScrollBehavior : Behavior<ListBox>
    {
        private ScrollViewer _scrollViewer;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += ListBox_Loaded;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= ListBox_Loaded;
        }

        private void ListBox_Loaded(object sender, RoutedEventArgs e)
        {
            _scrollViewer = GetScrollViewer(AssociatedObject);
            if (_scrollViewer != null)
            {
                AssociatedObject.PreviewMouseWheel += OnPreviewMouseWheel;
            }
        }

        private void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (_scrollViewer != null)
            {
                double delta = e.Delta;
                double scrollAmount = 50; // Adjust as needed
                _scrollViewer.ScrollToHorizontalOffset(_scrollViewer.HorizontalOffset + scrollAmount * Math.Sign(delta));
                e.Handled = true;
            }
        }

        private ScrollViewer GetScrollViewer(DependencyObject depObj)
        {
            if (depObj is ScrollViewer scrollViewer)
            {
                return scrollViewer;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                ScrollViewer childScrollViewer = GetScrollViewer(child);
                if (childScrollViewer != null)
                {
                    return childScrollViewer;
                }
            }

            return null;
        }
    }
}
