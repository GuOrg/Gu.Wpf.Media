namespace Gu.Wpf.Media.Demo
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(double), typeof(TimeSpan))]
    public sealed class SecondsToTimeSpanConverter : IValueConverter
    {
        public static readonly SecondsToTimeSpanConverter Default = new();

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
