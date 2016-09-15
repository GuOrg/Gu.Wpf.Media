namespace Gu.Wpf.Media.Demo.UiTestWindows
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Data;

    public class MediaElementToPropertyListConverter : IValueConverter
    {
        public static readonly MediaElementToPropertyListConverter Default = new MediaElementToPropertyListConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var wrapper = (MediaElementWrapper)value;
            var dps = typeof(MediaElementWrapper).GetFields(BindingFlags.Static | BindingFlags.Public)
                                            .Where(f => f.FieldType == typeof(DependencyProperty))
                                            .Select(f => f.GetValue(null))
                                            .OfType<DependencyProperty>()
                                            .ToArray();
            return dps.Select(dp => new PropertyItem(wrapper, dp))
                      .ToArray();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
