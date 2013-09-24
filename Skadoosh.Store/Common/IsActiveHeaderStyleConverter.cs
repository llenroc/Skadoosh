using System;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Skadoosh.Store.Common
{
    public sealed class IsActiveHeaderStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var brush = new LinearGradientBrush();
            brush.StartPoint = new Point(1, 0.5);
            brush.EndPoint = new Point(0, 0.5);

            var isActive = (bool) value;
            if (isActive)
            {
                brush.GradientStops.Add(new GradientStop
                {
                    Offset = 0.184,
                    Color = Color.FromArgb(255, 255, 92, 0)
                });
                brush.GradientStops.Add(new GradientStop
                {
                    Offset = 389,
                    Color = Color.FromArgb(255, 255, 187, 0)
                });
                return brush;
            }
            brush.GradientStops.Add(new GradientStop
            {
                Offset = 0.184,
                Color = Color.FromArgb(255, 255, 187, 0)
            });
            brush.GradientStops.Add(new GradientStop
            {
                Offset = 389,
                Color = Color.FromArgb(255, 255, 255, 255)
            });
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}