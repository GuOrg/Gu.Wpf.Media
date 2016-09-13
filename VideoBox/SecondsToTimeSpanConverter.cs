namespace VideoBox
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

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