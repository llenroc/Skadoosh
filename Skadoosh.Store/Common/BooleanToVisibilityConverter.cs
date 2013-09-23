using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

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
            var isMulti = (bool)value;
            if (isMulti)
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


    public sealed class BoolToSelectionModeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var isMulti = (bool)value;
            if (isMulti)
            {
                return ListViewSelectionMode.Multiple;
            }
            else
            {
                return ListViewSelectionMode.Single;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

    public sealed class IsActiveHeaderStyleConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            LinearGradientBrush brush = new LinearGradientBrush();
            brush.StartPoint =new Point(1,0.5);
            brush.EndPoint = new Point(0,0.5);

            var isActive = (bool)value;
            if (isActive)
            {
                brush.GradientStops.Add(new GradientStop() { 
                    Offset= 0.184,
                    Color = Color.FromArgb(255,255,92,0)
                });
                brush.GradientStops.Add(new GradientStop() {
                    Offset = 389,
                    Color = Color.FromArgb(255, 255,187,0)
                });
                return brush;
            }
            else
            {
                brush.GradientStops.Add(new GradientStop() {
                    Offset = 0.184,
                    Color = Color.FromArgb(255,255,187,0)
                });
                brush.GradientStops.Add(new GradientStop() {
                    Offset = 389,
                    Color = Color.FromArgb(255,255,255,255)
                });
                return brush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }

    public static class StringExtenstions
    {
        public static bool IsValidEmail(this string str)
        {
            var exp = "^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$;";
            return Regex.IsMatch(str, exp);
        }
    }

}
