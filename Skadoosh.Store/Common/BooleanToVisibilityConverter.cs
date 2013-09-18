using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Skadoosh.Store.Common
{
    /// <summary>
    /// Value converter that translates true to <see cref="Visibility.Visible"/> and false to
    /// <see cref="Visibility.Collapsed"/>.
    /// </summary>
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public bool IsReversed { get; set; }
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (IsReversed)
            {
                return (value is bool && (bool)value) ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }



    public sealed class IsLiveSurveyImageConverter : IValueConverter
    {
  
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var isLive = (bool)value;
            if (isLive)
            {
                return "ms-appx:///Assets/LiveSurvey.png";
            }
            else
            {
                return "ms-appx:///Assets/Static.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
    public sealed class IsMultiQuestionImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var isLive = (bool)value;
            if (isLive)
            {
                return "ms-appx:///Assets/Multi.png";
            }
            else
            {
                return "ms-appx:///Assets/Single.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

}
