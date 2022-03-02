namespace Gu.Wpf.Media.Demo.UiTestWindows
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(bool), typeof(bool))]
    public sealed class InvertBoolConverter : IValueConverter
    {
        public static readonly InvertBoolConverter Default = new();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                bool b => !b,
                _ => throw new ArgumentException("Expected bool."),
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                bool b => !b,
                _ => throw new ArgumentException("Expected bool."),
            };
        }
    }
}
