namespace Gu.Wpf.Media.Demo.UiTestWindows
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(object), typeof(object))]
    public sealed class NullConverter : IValueConverter
    {
        public static readonly NullConverter Default = new NullConverter();

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return "null";
            }

            return value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (Equals(value, "null"))
            {
                return null;
            }

            return value;
        }
    }
}
