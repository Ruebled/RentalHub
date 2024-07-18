using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace RentalHub.View
{
    public partial class PhotoGalleryControl : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<string>), typeof(PhotoGalleryControl), new PropertyMetadata(null, OnItemsSourceChanged));

        public IEnumerable<string> ItemsSource
        {
            get { return (IEnumerable<string>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public PhotoGalleryControl()
        {
            InitializeComponent();
            Loaded += PhotoGalleryControl_Loaded;
        }

        private void PhotoGalleryControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadImages();
        }

        private void LoadImages()
        {
            if (ItemsSource != null)
            {
                foreach (string url in ItemsSource)
                {
                    BitmapImage image = new BitmapImage(new System.Uri(url));
                    Image img = new Image
                    {
                        Source = image,
                        Width = 100,
                        Height = 100,
                        Stretch = Stretch.UniformToFill,
                        Margin = new Thickness(5)
                    };

                    // Apply animation to images
                    img.MouseEnter += Img_MouseEnter;
                    img.MouseLeave += Img_MouseLeave;

                    photoListBox.Items.Add(img);
                }
            }
        }

        private void Img_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Image image = sender as Image;
            if (image != null)
            {
                ScaleTransform scaleTransform = new ScaleTransform(1.2, 1.2);
                image.RenderTransform = scaleTransform;
            }
        }

        private void Img_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Image image = sender as Image;
            if (image != null)
            {
                ScaleTransform scaleTransform = new ScaleTransform(1, 1);
                image.RenderTransform = scaleTransform;
            }
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PhotoGalleryControl control = d as PhotoGalleryControl;
            if (control != null)
            {
                control.photoListBox.Items.Clear();
                control.LoadImages();
            }
        }
    }
}
