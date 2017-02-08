namespace Gu.Wpf.Media.Demo.UiTestWindows
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Windows.Data;

    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class InvertBoolConverter : IValueConverter
    {
        public static readonly InvertBoolConverter Default = new InvertBoolConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}