namespace Gu.Wpf.Media
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    /// <inheritdoc/>
    [ValueConversion(typeof(TimeSpan?), typeof(double?))]
    [MarkupExtensionReturnType(typeof(IValueConverter))]
    public class NullableTimeSpanToSecondsConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// The default instance used like Converter="{x:Static NullableTimeSpanToSecondsConverter.Default}"
        /// </summary>
        public static readonly NullableTimeSpanToSecondsConverter Default = new NullableTimeSpanToSecondsConverter();

        /// <summary>
        /// Use this to configure how errors are handled at runtime.
        /// </summary>
        public ErrorHandlingStrategy RuntimeErrorHandling { get; set; } = ErrorHandlingStrategy.SilentFailure;

        /// <summary>
        /// The converted value to return when the value is null.
        /// </summary>
        public double? WhenNull { get; set; } = 0;

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return this.WhenNull;
            }

            if (value is TimeSpan)
            {
                return ((TimeSpan)value).TotalSeconds;
            }

            if (Is.InDesignMode || this.RuntimeErrorHandling == ErrorHandlingStrategy.Throw)
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

            if (Is.InDesignMode || this.RuntimeErrorHandling == ErrorHandlingStrategy.Throw)
            {
                throw new ArgumentException($"Expected value to be a double, was: {value}");
            }

            return Binding.DoNothing;
        }
    }
}
