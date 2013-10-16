using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Skadoosh.Store.Common
{
    /// <summary>
    ///     Value converter that translates true to <see cref="Visibility.Visible" /> and false to
    ///     <see cref="Visibility.Collapsed" />.
    /// </summary>
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public bool IsReversed { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (IsReversed)
            {
                return (value is bool && (bool) value) ? Visibility.Collapsed : Visibility.Visible;
            }
            return (value is bool && (bool) value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value is Visibility && (Visibility) value == Visibility.Visible;
        }
    }


    public sealed class BoolToImageConverter : IValueConverter
    {
        public bool IsReversed { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = (bool)value;
            if (result == true)
            {
                BitmapImage image = new BitmapImage();
                image.UriSource = new Uri("ms-appx:///Assets/Delete.png");
                return image;
            }
            else
            {
                BitmapImage image = new BitmapImage();
                image.UriSource = new Uri("ms-appx:///Assets/Good.png");
                return image;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
            //return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }
}
