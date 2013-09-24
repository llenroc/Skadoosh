using System;
using Windows.UI.Xaml.Data;

namespace Skadoosh.Store.Common
{
    public sealed class IsMultiQuestionImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var isMulti = (bool) value;
            if (isMulti)
            {
                return "ms-appx:///Assets/Multi.png";
            }
            return "ms-appx:///Assets/Single.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}