namespace Gu.Wpf.Media.Demo.UiTestWindows
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Data;

    [ValueConversion(typeof(MediaElementWrapper), typeof(ReadOnlyObservableCollection<PropertyItem>))]
    public sealed class MediaElementToPropertyListConverter : IValueConverter
    {
        public static readonly MediaElementToPropertyListConverter Default = new();

        public static ReadOnlyObservableCollection<PropertyItem> GetPropertyItems(MediaElementWrapper wrapper)
        {
            var dps = typeof(MediaElementWrapper).GetFields(BindingFlags.Static | BindingFlags.Public)
                                                 .Where(f => f.FieldType == typeof(DependencyProperty))
                                                 .Select(f => f.GetValue(null))
                                                 .OfType<DependencyProperty>()
                                                 .OrderBy(x => x.Name)
                                                 .ToArray();
            var props = dps.Select(dp => PropertyItem.GetOrCreate(wrapper, dp))
                      .ToArray();
            return new ReadOnlyObservableCollection<PropertyItem>(new ObservableCollection<PropertyItem>(props));
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var wrapper = (MediaElementWrapper)value;
            return GetPropertyItems(wrapper);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"{nameof(MediaElementToPropertyListConverter)} can only be used in OneWay bindings");
        }
    }
}
