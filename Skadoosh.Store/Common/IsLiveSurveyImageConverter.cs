using System;
using Windows.UI.Xaml.Data;

namespace Skadoosh.Store.Common
{
    public sealed class IsLiveSurveyImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var isLive = (bool) value;
            if (isLive)
            {
                return "ms-appx:///Assets/LiveSurvey.png";
            }
            return "ms-appx:///Assets/Static.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}