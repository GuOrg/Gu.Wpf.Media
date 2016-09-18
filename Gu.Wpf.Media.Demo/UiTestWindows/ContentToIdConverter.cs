namespace Gu.Wpf.Media.Demo.UiTestWindows
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ContentToIdConverter: IValueConverter
    {
        public static readonly ContentToIdConverter Default = new ContentToIdConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
