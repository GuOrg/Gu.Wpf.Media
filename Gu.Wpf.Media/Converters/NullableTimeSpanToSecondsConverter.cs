namespace Gu.Wpf.Media
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;
    using System.Windows.Markup;

    /// <summary>
    /// An <see cref="IValueConverter"/> that returns <see cref="TimeSpan.TotalSeconds"/>.
    /// </summary>
    [ValueConversion(typeof(TimeSpan?), typeof(double?))]
    [MarkupExtensionReturnType(typeof(NullableTimeSpanToSecondsConverter))]
    public class NullableTimeSpanToSecondsConverter : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// The default instance used like Converter="{x:Static NullableTimeSpanToSecondsConverter.Default}".
        /// </summary>
        public static readonly NullableTimeSpanToSecondsConverter Default = new NullableTimeSpanToSecondsConverter();

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

            if (value is TimeSpan timeSpan)
            {
                return timeSpan.TotalSeconds;
            }

            if (Is.InDesignMode)
            {
                var message = this.CreateErrorMessage($"Expected value to be a Nullable<TimeSpan>, was: {value}");
                throw new ArgumentException(message, nameof(value));
            }

            // Returning raw value letting the binding fail the standard way
            return value;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (value is double d)
            {
                return TimeSpan.FromSeconds(d);
            }

            if (Is.InDesignMode)
            {
                var message = this.CreateErrorMessage($"Expected value to be a Nullable<double>, was: {value}");
                throw new ArgumentException(message, nameof(value));
            }

            // Returning raw value letting the binding fail the framework way
            return value;
        }

        private string CreateErrorMessage(string message, [CallerMemberName] string caller = null)
        {
            return $"{this.GetType().FullName}.{caller} failed\r\n" + message;
        }
    }
}
