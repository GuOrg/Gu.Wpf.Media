namespace Gu.Wpf.Media.Demo.UiTestWindows
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(object), typeof(string))]
    public sealed class ContentToIdConverter : IValueConverter
    {
        public static readonly ContentToIdConverter Default = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        object IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"{nameof(ContentToIdConverter)} can only be used in OneWay bindings");
        }
    }
}
