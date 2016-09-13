namespace Gu.Wpf.Media.Demo
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class DurationToSecondsConverter : IValueConverter
    {
        public static readonly DurationToSecondsConverter Default = new DurationToSecondsConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var duration = (Duration) value;
            return duration.HasTimeSpan ? duration.TimeSpan.TotalSeconds : 0;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}