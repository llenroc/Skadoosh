using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace Skadoosh.Phone.Common
{
    public class BoolToSelectionModeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isMulti = (bool)value;
            if (isMulti)
            {
                return SelectionMode.Multiple;
            }
            else
            {
                return SelectionMode.Single;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
