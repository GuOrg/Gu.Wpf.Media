namespace Gu.Wpf.Media.Demo
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Windows.Data;

    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public class SecondsToTimeSpanConverter : IValueConverter
    {
        public static readonly SecondsToTimeSpanConverter Default = new SecondsToTimeSpanConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.FromSeconds((double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((TimeSpan)value).TotalSeconds;
        }
    }
}