using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Skadoosh.Store.Common
{
    public sealed class BoolToSelectionModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var isMulti = (bool) value;
            if (isMulti)
            {
                return ListViewSelectionMode.Multiple;
            }
            return ListViewSelectionMode.Single;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}