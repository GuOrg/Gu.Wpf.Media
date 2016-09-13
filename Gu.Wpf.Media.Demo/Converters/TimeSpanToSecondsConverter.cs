namespace Gu.Wpf.Media.Demo
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class TimeSpanToSecondsConverter : IValueConverter
    {
        public static readonly TimeSpanToSecondsConverter Default = new TimeSpanToSecondsConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return 0.0;
            }

            return ((TimeSpan)value).TotalSeconds;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.FromSeconds((double)value);
        }
    }
}
