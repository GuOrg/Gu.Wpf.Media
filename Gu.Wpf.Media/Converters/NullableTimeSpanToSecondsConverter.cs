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
    public sealed class NullableTimeSpanToSecondsConverter : MarkupExtension, IValueConverter
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
        public override object ProvideValue(IServiceProvider? serviceProvider)
        {
            return this;
        }

        /// <inheritdoc/>
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value switch
            {
                null => this.WhenNull,
                TimeSpan timeSpan => timeSpan.TotalSeconds,
                _ when Is.InDesignMode
                  => throw new ArgumentException(this.CreateErrorMessage($"Expected value to be a Nullable<TimeSpan>, was: {value}"), nameof(value)),
                //// Returning raw value letting the binding fail the standard way
                _ => value,
            };
        }

        /// <inheritdoc/>
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value switch
            {
                null => null,
                double d => TimeSpan.FromSeconds(d),
                _ when Is.InDesignMode
                    => throw new ArgumentException(this.CreateErrorMessage($"Expected value to be a Nullable<TimeSpan>, was: {value}"), nameof(value)),
                //// Returning raw value letting the binding fail the standard way
                _ => value,
            };
        }

        private string CreateErrorMessage(string message, [CallerMemberName] string? caller = null)
        {
            return $"{nameof(NullableTimeSpanToSecondsConverter)}.{caller} failed\r\n" + message;
        }
    }
}
