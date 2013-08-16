using System;
using Windows.UI.Xaml.Data;

namespace Skadoosh.Store.Common
{

    public sealed class OneZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool result = false;
            switch ((int)value)
            {
                case 1:
                    result = false;
                    break;
                case 2:
                    result = true;
                    break;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            bool result = (bool)value;
            if (result == false)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }


    /// <summary>
    /// Value converter that translates true to false and vice versa.
    /// </summary>
    public sealed class BooleanNegationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !(value is bool && (bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return !(value is bool && (bool)value);
        }
    }
}
