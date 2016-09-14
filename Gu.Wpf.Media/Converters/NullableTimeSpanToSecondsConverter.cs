namespace Gu.Wpf.Media
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <inheritdoc/>
    [ValueConversion(typeof(TimeSpan?), typeof(double?))]
    public class NullableTimeSpanToSecondsConverter : IValueConverter
    {
        public static readonly NullableTimeSpanToSecondsConverter Default = new NullableTimeSpanToSecondsConverter();

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return 0.0;
            }

            if (value is TimeSpan)
            {
                return ((TimeSpan)value).TotalSeconds;
            }

            if (Is.InDesignMode)
            {
                throw new ArgumentException($"Expected value to be a TimeSpan, was: {value}");
            }

            return Binding.DoNothing;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (value is double)
            {
                return TimeSpan.FromSeconds((double)value);
            }

            if (Is.InDesignMode)
            {
                throw new ArgumentException($"Expected value to be a double, was: {value}");
            }

            return Binding.DoNothing;
        }
    }
}
