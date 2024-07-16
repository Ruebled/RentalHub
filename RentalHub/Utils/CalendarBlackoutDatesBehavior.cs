using Microsoft.Xaml.Behaviors;

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace RentalHub.Utils
{
    public class CalendarBlackoutDatesBehavior : Behavior<Calendar>
    {
        public static readonly DependencyProperty BlackoutDatesProperty =
            DependencyProperty.Register(nameof(BlackoutDates), typeof(ObservableCollection<CalendarDateRange>), typeof(CalendarBlackoutDatesBehavior), new PropertyMetadata(null, OnBlackoutDatesChanged));

        public ObservableCollection<CalendarDateRange> BlackoutDates
        {
            get { return (ObservableCollection<CalendarDateRange>)GetValue(BlackoutDatesProperty); }
            set { SetValue(BlackoutDatesProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (BlackoutDates != null)
            {
                UpdateBlackoutDates();
                BlackoutDates.CollectionChanged += OnBlackoutDatesCollectionChanged;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (BlackoutDates != null)
            {
                BlackoutDates.CollectionChanged -= OnBlackoutDatesCollectionChanged;
            }
        }

        private static void OnBlackoutDatesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CalendarBlackoutDatesBehavior behavior)
            {
                behavior.UpdateBlackoutDates();
                if (e.OldValue is ObservableCollection<CalendarDateRange> oldCollection)
                {
                    oldCollection.CollectionChanged -= behavior.OnBlackoutDatesCollectionChanged;
                }
                if (e.NewValue is ObservableCollection<CalendarDateRange> newCollection)
                {
                    newCollection.CollectionChanged += behavior.OnBlackoutDatesCollectionChanged;
                }
            }
        }

        private void OnBlackoutDatesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateBlackoutDates();
        }

        private void UpdateBlackoutDates()
        {
            if (AssociatedObject == null) return;

            AssociatedObject.BlackoutDates.Clear();
            if (BlackoutDates != null)
            {
                foreach (var dateRange in BlackoutDates)
                {
                    AssociatedObject.BlackoutDates.Add(dateRange);
                }
            }
        }
    }
}